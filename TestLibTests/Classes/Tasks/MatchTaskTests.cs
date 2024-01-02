using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestLib.Classes.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestLib.Classes.Answers;
using TestLib.Abstractions;

namespace TestLib.Classes.Tasks.Tests
{
    [TestClass()]
    public class MatchTaskTests
    {
        [TestMethod()]
        public void GetGradeTest()
        {
            // Arrange
            MatchTask task = new MatchTask() { Point = 3 };
            var answer1 = new MatchAnswer() { Text = "1", Side = Enums.MatchSide.Left };
            var answer2 = new MatchAnswer() { Text = "2", Side = Enums.MatchSide.Left };
            var answer3 = new MatchAnswer() { Text = "3", Side = Enums.MatchSide.Left };
            var answer4 = new MatchAnswer() { Text = "1", Side = Enums.MatchSide.Right, Partner = answer1 };
            var answer5 = new MatchAnswer() { Text = "2", Side = Enums.MatchSide.Right, Partner = answer2 };
            var answer6 = new MatchAnswer() { Text = "3", Side = Enums.MatchSide.Right, Partner = answer3 };
            answer1.Partner = answer4;
            answer2.Partner = answer5;
            answer3.Partner = answer6;
            var answers = new List<Answer>() { answer1, answer2, answer3, answer4, answer5, answer6 };
            task.Answers = answers;

            var userAnswer1 = new MatchAnswer() { Text = "1", Side = Enums.MatchSide.Left };
            var userAnswer2 = new MatchAnswer() { Text = "2", Side = Enums.MatchSide.Left };
            var userAnswer3 = new MatchAnswer() { Text = "3", Side = Enums.MatchSide.Left };
            var userAnswer4 = new MatchAnswer() { Text = "1", Side = Enums.MatchSide.Right, Partner = userAnswer2 };
            var userAnswer5 = new MatchAnswer() { Text = "2", Side = Enums.MatchSide.Right, Partner = userAnswer1 };
            var userAnswer6 = new MatchAnswer() { Text = "3", Side = Enums.MatchSide.Right, Partner = userAnswer3 };
            userAnswer2.Partner = userAnswer4; // incorrect
            userAnswer1.Partner = userAnswer5; // incorrect
            userAnswer3.Partner = userAnswer6; // correct
            var userAnswers = new List<Answer>() { userAnswer1, userAnswer2, userAnswer3, userAnswer4, userAnswer5, userAnswer6 };

            // Act
            var grade = task.GetGrade(userAnswers);
            var expected = 1;

            // Assert
            Assert.AreEqual(expected, grade);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetGradeTest_IncorrectAnswersCount()
        {
            // Arrange
            Answer answer = new MatchAnswer();
            MatchTask task = new MatchTask() { Point = 3, Answers = new() { answer } };
            var userAnswers = new List<Answer>() { answer, answer };

            // Act
            task.GetGrade(userAnswers); // ArgumentException
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void GetGradeTest_IncorrectAnswerType()
        {
            // Arrange
            var answers = new List<Answer>() { new MatchAnswer() };
            MatchTask task = new() { Point = 3, Answers = answers };

            var userAnswers = new List<Answer>() { new TextAnswer() };

            // Act
            task.GetGrade(userAnswers);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetGradeTest_NullUserAnswers()
        {
            // Arrange
            MatchTask task = new();

            // Act
            task.GetGrade(null);
        }


    }
}