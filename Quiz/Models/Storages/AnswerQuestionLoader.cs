using Quiz.Datatbase.Entities;
using Quiz.Datatbase.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Models.Storages
{
    class AnswerQuestionLoader
    {
        private UnitOfWork repository;
        private const int numberQuestionsInTest = 20;
        private List<Answer> answers;
        public AnswerQuestionLoader(UnitOfWork repos)
        {
            answers = new List<Answer>();
            repository = repos;
        }

        public List<Question> LoadRandQuestions(int categoryId)
        {
            List<Question> resultQuestions = new List<Question>();
            List<Question> allQuestions;
            if (repository.CategoryRepository.Get(categoryId).Name.Equals("Random questions"))
            {
                allQuestions = repository.QuestionRepository.GetAll().ToList();
            }
            else
            {
                allQuestions = repository.QuestionRepository.GetAll(quest => quest.CategoryId == categoryId).ToList();
            }

            if (allQuestions.Count() > numberQuestionsInTest)
            {
                int min = 0;
                int max = allQuestions.Count();
                Random rnd = new Random();

                for (int i = 0; i < numberQuestionsInTest; i++)
                {
                    Question question = allQuestions[rnd.Next(min, max)];

                    if (resultQuestions.Count > 0 && resultQuestions.Count(quest => quest.Id == question.Id) > 0)
                    {
                        i--;
                        continue;
                    }

                    resultQuestions.Add(question);
                }
            }
            else
            {
                resultQuestions = allQuestions;
            }
            LoadAnswers(resultQuestions);
            return resultQuestions;
        }

        public List<Answer> LoadAnswers()
        {
            return answers;
        }

        private void LoadAnswers(List<Question> questions)
        {
            if (questions.Count() > 0)
            {
                foreach (var question in questions)
                {
                    var curAnswers = repository.AnswerRepository.GetAll(answ => answ.QuestionId == question.Id);
                    answers.AddRange(curAnswers);
                }
            }
        }
    }
}
