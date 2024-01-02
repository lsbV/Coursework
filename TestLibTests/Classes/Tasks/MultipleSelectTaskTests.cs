using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestLib.Classes.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestLib.Abstractions;
using TestLib.Classes.Answers;

namespace TestLib.Classes.Tasks.Tests
{
    [TestClass()]
    public class MultipleSelectTaskTests
    {
        [TestMethod()]
        public void GetGradeTest()
        {
            // Arrange
            var task = new MultipleSelectTask()
            {
                Id = 1,
                Point = 10,
                Answers = new List<Answer>()
                {
                    new TextAnswer() { IsCorrect = true, Text = "1" },
                    new TextAnswer() { IsCorrect = true, Text = "2" },
                    new TextAnswer() { IsCorrect = false, Text = "3" },
                    new TextAnswer() { IsCorrect = false, Text = "4" },
                    new TextAnswer() { IsCorrect = true, Text = "5" },
                }
            };
            var userAnswers = new List<Answer>()
            {
                new TextAnswer() { IsCorrect = true,Text = "1" },
                new TextAnswer() { IsCorrect = false, Text = "2" },
                new TextAnswer() { IsCorrect = true, Text = "3" },
                new TextAnswer() { IsCorrect = false, Text = "4" },
                new TextAnswer() { IsCorrect = true, Text = "5" },
            };

            // Act
            var expected = 6.67;
            var grade = task.GetGrade(userAnswers);

            // Assert
            Assert.AreEqual(expected, grade);
        }
    }
}