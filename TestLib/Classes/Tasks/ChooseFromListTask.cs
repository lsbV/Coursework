﻿using System.Collections.ObjectModel;
using TestLib.Abstractions;
using TestLib.Classes.Bodies;
using Task = TestLib.Abstractions.Task;

namespace TestLib.Classes.Tasks
{
    public class ChooseFromListTask : Task
    {
        public ChooseFromListTask(string description, double point, ICollection<Answer> answers, Body body)
        {
            Description = "Choose from list";
            Point = point;
            Answers = new ObservableCollection<Answer>(answers.ToList());
            Body = body;
        }
        public ChooseFromListTask()
        {
            Description = "Choose from list";
            Point = 0;
        }

        public object Clone()
        {
            var answers = new ObservableCollection<Answer>();
            foreach (var answer in Answers)
            {
                answers.Add((Answer)answer.Clone());
            }
            return new ChooseFromListTask(Description, Point, answers, (Body)Body.Clone());
        }

        public override bool CheckAnswers(ICollection<Answer> answers)
        {
            throw new NotImplementedException();
        }
    }
}
