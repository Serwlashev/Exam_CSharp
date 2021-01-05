using Quiz.Datatbase.Entities;
using Quiz.Datatbase.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Models.Game
{
    class QuestionStorage : IEnumerator<Question>, IEnumerable<Question>
    {
        private List<Question> questions;
        int index = -1;
        public int NumberQuestion
        {
            get
            {
                return questions.Count();
            }
        }

        public Question Current => questions[index];

        object System.Collections.IEnumerator.Current => questions[index];

        public QuestionStorage(List<Question> questions)
        {
            this.questions = new List<Question>(questions);
        }

        public void Dispose()
        {
            questions.Clear();
        }

        public bool MoveNext()
        {
            if(++index < NumberQuestion)
            {
                return true;
            }
            Reset();
            return false;
        }

        public void Reset()
        {
            index = -1;
        }

        public IEnumerator<Question> GetEnumerator()
        {
            return this;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this;
        }
    }
}


