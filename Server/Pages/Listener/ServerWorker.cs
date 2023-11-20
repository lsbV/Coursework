using CommunityToolkit.Mvvm.Messaging;
using DALTestsDB.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Repository;
using Server.Infrastructure;
using Server.Ninject;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestLib;
using TestLib.Abstractions;
using TestLib.Classes.Network;
using TestLib.Classes.Test;

namespace Server.Pages.Listener
{
    public class ServerWorker : IDisposable
    {
        public string Id { get; } = Guid.NewGuid().ToString();
        public string? IP => client.Client.RemoteEndPoint?.ToString();
        private TcpClient client;
        private NetworkStream stream;
        private StreamWriter writer;
        private StreamReader reader;
        private int userId;
        private ISerializer serializer;
        private ILogger logger;
        private IEncryptor encryptor;
        private bool working = true;

        public ServerWorker(TcpClient client,
                            ISerializer serializer,
                            ILogger logger,
                            IEncryptor encryptor)
        {
            this.client = client;
            this.serializer = serializer;
            this.logger = logger;
            this.encryptor = encryptor;
            stream = client.GetStream();
            writer = new StreamWriter(client.GetStream());
            reader = new StreamReader(client.GetStream());
            userId = -1;
            logger?.Log($"Client {client.Client.RemoteEndPoint} connected");
        }


        public async System.Threading.Tasks.Task Work(CancellationToken token)
        {
            while (working)
            {
                var msg = await ReciveMessage(token);
                if (msg == null)
                {
                    continue;
                }
                var answer = Procces(msg);
                token.ThrowIfCancellationRequested();
                if (answer != null)
                    SendMessage(answer);
            }

        }

        private Message? Procces(Message? msg)
        {
            if (ValidateMessage(msg, out string err) == false)
            {
                logger?.Log($"Client {client.Client.RemoteEndPoint} sent incorrect message", err);
                return CreateMessage(ResponseCode.ERROR, err);
            }
            return msg switch
            {
                { Header: RequestMessage.LOG_IN } => LogIn(msg),
                { Header: RequestMessage.LOG_OUT } => LogOut(msg),
                { Header: RequestMessage.GET } => Get(msg),
                { Header: RequestMessage.PUT } => Put(msg),
                { Header: RequestMessage.START_TEST } => StartTest(msg),
                _ => null
            };
        }

        private Message? StartTest(Message msg)
        {
            if (ValidateMessage(msg, out string err) == false)
            {
                return CreateMessage(ResponseCode.ERROR, err);
            }
            try
            {
                if (int.TryParse(msg.Body, out int testId) == false)
                {
                    return CreateMessage(ResponseCode.BAD_REQUEST, "Bad request. Test not found!");
                }
                using var uow = DI.Create<IGenericUnitOfWork>();
                var testAssinedRepo = uow.Repository<TestAssigned>();

                var tau = uow.Repository<TestAssignedUser>().FindAll(ta => ta.TestAssignedId == testId && ta.UserId == userId && ta.IsActive == true).ToList();
                if(tau.Count == 0)
                {
                    return CreateMessage(ResponseCode.NOT_FOUND, "Test not found");
                }
                var newestTestDate = tau.Max(t => t.AppointmentDate);
                var newestTest = tau.FirstOrDefault(t => t.AppointmentDate == newestTestDate);
                if (newestTest == null)
                {
                    return CreateMessage(ResponseCode.NOT_FOUND, "Test not found");
                }

                var test = testAssinedRepo.FindById(newestTest.TestAssignedId);
                if (test == null)
                {
                    return CreateMessage(ResponseCode.NOT_FOUND, "Test not found");
                }

                testAssinedRepo.LoadAssociatedProperty(test, t => t.Test);
                uow.Repository<Test>().LoadAssociatedCollection(test.Test, t => t.Tasks);
                foreach (var task in test.Test.Tasks)
                {
                    uow.Repository<TestLib.Abstractions.Task>().LoadAssociatedCollection(task, t => t.Answers);
                    uow.Repository<TestLib.Abstractions.Task>().LoadAssociatedProperty(task, t => t.Body);
                }

                var clearTest = test.GetClearTestAssigned();
                logger?.Log($"Client {client.Client.RemoteEndPoint} started test {testId}");
                return CreateMessage(ResponseCode.OK, clearTest);
            }
            catch (Exception ex)
            {
                logger.Log(ex.Message);
            }
            return null;
        }

        private Message? Put(Message msg)
        {
            if (ValidateUserAndBody(msg, out Message? outputMessage) == false)
            {
                return outputMessage;
            }

            try
            {
                var test = serializer.Deserialize<TestAssigned>(msg.Body!);
                if (test == null)
                {
                    logger?.Log($"Client {client.Client.RemoteEndPoint} sent incorrect message");
                    return CreateMessage(ResponseCode.ERROR, "Incorrect message");
                }

                using var uow = DI.Create<IGenericUnitOfWork>();
                UserTestResult userTestResult = new UserTestResult() { PassageDate = DateTime.Now };
                var tau = uow.Repository<TestAssignedUser>().FindAll(tau => tau.TestAssignedId == test.Id && tau.UserId == userId && tau.IsActive == true).FirstOrDefault();
                if (tau == null)
                {
                    logger?.Log($"Client {client.Client.RemoteEndPoint} sent incorrect message");
                    return CreateMessage(ResponseCode.ERROR, "Test not found");
                }
                tau.IsActive = false;
                uow.Repository<TestAssignedUser>().Update(tau);
                logger?.Log($"Client {client.Client.RemoteEndPoint} sent test {test.Id}");
                userTestResult.TestAssignedUserId = tau.Id;
                UploadTestResultIntoDB(test, uow, userTestResult);

                uow.Repository<UserTestResult>().LoadAssociatedCollection(userTestResult, t => t.UserTaskResults, x => ((UserTaskResult)x).Task);
                var taskRepo = uow.Repository<TestLib.Abstractions.Task>();
                foreach (var task in userTestResult.UserTaskResults)
                {
                    taskRepo.LoadAssociatedCollection(task.Task, t => t.Answers);
                }
                var testResult = userTestResult.GetTestGrade();

                logger?.Log($"Client {client.Client.RemoteEndPoint} test result {testResult}");
                return CreateMessage(ResponseCode.OK, testResult);
            }
            catch (Exception ex)
            {
                logger?.Log(ex.ToString());
            }
            return null;
        }


        private static void UploadTestResultIntoDB(TestAssigned test, IGenericUnitOfWork uow, UserTestResult userTestResult)
        {
            var testReslutRepo = uow.Repository<UserTestResult>();
            var taskResultRepo = uow.Repository<UserTaskResult>();
            var answerResultRepo = uow.Repository<UserAnswerResult>();
            testReslutRepo.Add(userTestResult);
            testReslutRepo.LoadAssociatedCollection(userTestResult, t => t.UserTaskResults);
            foreach (var task in test.Test.Tasks)
            {
                UserTaskResult userTaskResult = new UserTaskResult() { UserTestResultId = userTestResult.Id, TaskId = task.Id };
                taskResultRepo.Add(userTaskResult);
                taskResultRepo.LoadAssociatedCollection(userTaskResult, t => t.UserAnswerResults);
                foreach (var item in task.Answers)
                {
                    UserAnswerResult userTaskAnswer = new UserAnswerResult() { UserTaskResultId = userTaskResult.Id, Answer = item.GetNewUserAnswer() };
                    answerResultRepo.Add(userTaskAnswer);

                }
            }
            testReslutRepo.Update(userTestResult);
        }

        private Message? LogOut(Message msg)
        {
            logger?.Log($"Client {client.Client.RemoteEndPoint} logged out");
            client.Close();
            working = false;
            return null;
        }

        private Message? LogIn(Message message)
        {
            if (ValidateMessage(message, out string outputMessage) == false)
            {
                return CreateMessage(ResponseCode.ERROR, outputMessage);
            }
            try
            {
                var LP = message.Body.Split(" ");
                var login = LP[0];
                var password = LP[1];
                var sha = encryptor.Encrypt(password);
                using var unitOfWork = DI.Create<IGenericUnitOfWork>();
                var user = unitOfWork.Repository<User>().FindAll(u => u.Login == login && u.Password == sha).FirstOrDefault();
                if (user == null)
                {
                    logger?.Log($"Client {client.Client.RemoteEndPoint} sent incorrect authorization data\n");
                    return CreateMessage(ResponseCode.ERROR, "Incorrect authorization data");
                }
                UserPool userPool = UserPool.Instance;
                if (userPool.GetByUserId(user.Id) != null)
                {
                    logger?.Log($"Client {client.Client.RemoteEndPoint} already logged in");
                    return CreateMessage(ResponseCode.ERROR, "The user is already logged in");
                }
                var serverUser = userPool.GetByWorkerId(Id);
                if (serverUser != null)
                {
                    userId = user.Id;
                    serverUser.UserId = userId;
                    serverUser.Login = login;
                }
                logger?.Log($"Client {client.Client.RemoteEndPoint} logged in as {login}");
                return CreateMessage(ResponseCode.OK, "You are logged in");
            }
            catch (Exception ex)
            {
                logger?.Log(ex.Message);
                return CreateMessage(ResponseCode.ERROR, "Please try again later");
            }
        }



        private Message? Get(Message message)
        {
            if (ValidateUserAndBody(message, out Message? outputMessage) == false)
            {
                return outputMessage;
            }

            try
            {
                if (message.Body == RequestMessage.LIST_TESTS)
                {
                    using var uow = DI.Create<IGenericUnitOfWork>();
                    if (message.Body == RequestMessage.LIST_TESTS)
                    {
                        logger?.Log($"Client {client.Client.RemoteEndPoint} requested tests list");
                        var assignedUserTests = uow.Repository<TestAssignedUser>().FindAll(tau => tau.User.Id == userId && tau.IsActive == true).Select(t => t.TestAssignedId).ToList();

                        if (assignedUserTests.Count == 0)
                        {
                            logger?.Log($"Client {client.Client.RemoteEndPoint} has no active tests");
                            return CreateMessage(ResponseCode.NOT_FOUND, "No active tests");
                        }

                        var tests = uow.Repository<TestAssigned>().FindAll(at => assignedUserTests.Contains(at.Id)).ToList();
                        foreach (var test in tests)
                        {
                            uow.Repository<TestAssigned>().LoadAssociatedProperty(test, t => t.Test);
                        }

                        var body = tests.Select(t => GetAssignedTestWithoutTasks(t)).ToList();
                        return CreateMessage(ResponseCode.OK, body);

                    }
                }
                else
                {
                    logger?.Log($"Client {client.Client.RemoteEndPoint} sent bed request");
                    return CreateMessage(ResponseCode.ERROR, "Bad request");
                }

            }
            catch (Exception ex)
            {
                logger?.Log(ex.Message);
                return CreateMessage(ResponseCode.ERROR, "Please try again later");
            }
            return null;
        }

        private static TestAssigned GetAssignedTestWithoutTasks(TestAssigned ta)
        {
            return new TestAssigned() { Id = ta.Id, Test = new Test() { Id = ta.Test.Id, Title = ta.Test.Title, Description = ta.Test.Description }, StartAt = ta.StartAt, EndAt = ta.EndAt, TimeToTake = ta.TimeToTake };
        }

        private bool ValidateUserAndBody(Message message, out Message? outputMessage)
        {
            if (userId == -1)
            {
                logger?.Log($"Client {client.Client.RemoteEndPoint} is not logged in");
                outputMessage = CreateMessage(ResponseCode.ERROR, "You are not logged in");
                return false;
            }
            if (message.Body == null)
            {
                logger?.Log($"Client {client.Client.RemoteEndPoint} sent incorrect message");
                outputMessage = CreateMessage(ResponseCode.ERROR, "Message body is null");
                return false;
            }
            else
            {
                outputMessage = null;
                return true;
            }
        }

        private Message CreateMessage(string header, string body)
        {
            return new Message() { Header = header, Body = body };
        }
        private Message CreateMessage(string header, object body)
        {
            var serializedBody = serializer.Serialize(body);
            return new Message() { Header = header, Body = serializedBody };
        }

        private bool ValidateMessage(Message? msg, out string errorText)
        {
            if (msg == null)
            {
                errorText = "Message is null";
                return false;
            }
            if (msg.Header == null)
            {
                errorText = "Message header is null";
                return false;
            }
            errorText = string.Empty;
            return true;
        }
        private void SendMessage(Message message)
        {
            var serializedMessage = serializer.Serialize(message);
            writer.Write(serializedMessage.Length.ToString());
            writer.Flush();
            writer.Write(serializedMessage);
            writer.Flush();
        }


        public async Task<Message?> ReciveMessage(CancellationToken token)
        {
            const byte maxMessageLength = 16;
            StringBuilder sb = new StringBuilder();

            var messageLength = new byte[maxMessageLength];
            await stream.ReadAsync(messageLength, 0, messageLength.Length, token);
            var lengthStr = Encoding.UTF8.GetString(messageLength);
            if (int.TryParse(lengthStr, out int bufferSize) == false)
            {
                return null;
            }
            var buffer = new byte[bufferSize];
            await stream.ReadAsync(buffer, 0, buffer.Length, token);
            sb.Append(Encoding.UTF8.GetString(buffer));

            if (sb.Length == 0)
                return null;
            return serializer.Deserialize<Message>(sb.ToString());
        }
        public void Dispose()
        {
            client.Close();
            client.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
