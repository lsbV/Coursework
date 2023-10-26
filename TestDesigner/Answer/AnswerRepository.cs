using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDesigner.Task;
using TestDesigner.Task.ChooseFromList;

namespace TestDesigner.Answer
{
    public class AnswerRepository
    {
        private static AnswerRepository instance = new();
        private static Dictionary<Type, List<BaseAnswerViewModel>> answerTypes;
        public static AnswerRepository Instance => instance;

        private AnswerRepository()
        {
            
        }
        static AnswerRepository()
        {
            var n1 = new KeyValuePair<Type, List<BaseAnswerViewModel>>(typeof(ChooseFromListTaskViewModel), new List<BaseAnswerViewModel>() { new TextAnswerViewModel() });
            answerTypes = new();
            answerTypes.Add(n1.Key, n1.Value);
        }

        public IEnumerable<BaseAnswerViewModel> GetAnswerTypes(Type taskType)
        {
            if(answerTypes.ContainsKey(taskType))
            {
                //List<BaseAnswerViewModel> clone = new();
                //answerTypes[taskType].ForEach(x => clone.Add((BaseAnswerViewModel)x.Clone()));
                //return clone;
                foreach (var view in answerTypes[taskType])
                {
                    yield return (BaseAnswerViewModel)view.Clone();
                }
            }
            else
            {
                throw new ArgumentException("No answer types for this task type");
            }
        }
    }
}
