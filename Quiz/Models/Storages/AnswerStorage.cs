using Quiz.Datatbase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Models.Game
{
    class AnswerStorage
    {
        private List<Answer> answers;
        public AnswerStorage(List<Answer> answers)
        {
            this.answers = new List<Answer>(answers);
        }

        public (List<Answer>, int) GetAnswersToQuestion(int questionId)
        {
            List<Answer> currentAnswers = answers.Where(answ => answ.QuestionId == questionId).ToList();
            int countAnswers = 0;
            foreach(var answer in currentAnswers)
            {
                if(answer.IsCorrect == true)
                {
                    countAnswers++;
                }
            }

            return (currentAnswers, countAnswers);
        }
    }
}
