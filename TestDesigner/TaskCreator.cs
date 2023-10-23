using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TestLib.Classes.Bodies;
using TestLib.Classes.Tasks;
using TestLib.Interfaces;

namespace TestDesigner
{
    public static class TaskCreator
    {
        public static ITask CreateTask(string taskType, string bodyType)
        {
            var task = CreateTask(taskType);
            task.Body = CreateBody(bodyType);
            return task;
        }
        private static ITask CreateTask(string taskType)
        {
            switch (taskType)
            {
                case "Choose answer":
                    return new ChooseFromListTask();
                case "Enter text":
                    return new EnterTextTask();                
                default:
                    throw new ArgumentException("Invalid task type");
            }
        }
        private static ITaskBody CreateBody(string bodyType)
        {
            switch (bodyType)
            {
                case "Video":
                    return new VideoTaskBody();
                case "Audio":
                    return new AudioTaskBody();
                case "Text":
                    return new TextTaskBody();
                case "Image":
                    return new ImageTaskBody();
                default:
                    throw new ArgumentException("Invalid body type");
            }
        }
    }
}
