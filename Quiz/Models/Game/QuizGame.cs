using Quiz.Datatbase.Entities;
using Quiz.Datatbase.UnitOfWork;
using Quiz.Models.Console;
using Quiz.Models.Storages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Models.Game
{
    // Class for playing quiz
    class QuizGame
    {
        private QuestionStorage questionStorage;
        private AnswerStorage answerStorage;
        private AnswerQuestionLoader loader;
        private UnitOfWork repository;
        public QuizGame (UnitOfWork repos)
        {
            repository = repos;
            loader = new AnswerQuestionLoader(repository);
        }

        public int Start(int categoryId)
        {
            int numCorrectAnswers = 0;
            ConsoleManager.ClearConsole();
            ConsoleManager.PrintMessage("\n\tQuiz game\n");

            if (categoryId >= 0)
            {
                InitAnswersQuestion(categoryId);
                numCorrectAnswers = AskEachQuestion();
                
            }
            return numCorrectAnswers;
        }

        private void InitAnswersQuestion(int categoryid)
        {
            questionStorage = new QuestionStorage(loader.LoadRandQuestions(categoryid));
            answerStorage = new AnswerStorage(loader.LoadAnswers());
        }

        private int AskEachQuestion()
        {
            List<int> playersAnswers;
            int counter = 0;
            int correctAnswer = 0;
            foreach(var q in questionStorage)                           // Ask each loaded question
            {
                (List<Answer>, int) answersWithNumRight = answerStorage.GetAnswersToQuestion(q.Id);
                if(answersWithNumRight.Item2 == 0)                      // If we do not have right answers for the question
                {
                    continue;
                }

                ConsoleManager.PrintMessage($"{++counter}) {q}");       // Show each question

                playersAnswers = GetPlayersAnswers(answersWithNumRight.Item1, answersWithNumRight.Item2);   // Get players answers from the all answers for the question

                if (IsCorrectAnswers(answersWithNumRight.Item1, playersAnswers) == true)                    // If player choose correct answer - count number of the correct answers
                {
                    correctAnswer++;
                }
            }
            return correctAnswer;
        }

        private List<int> GetPlayersAnswers(List<Answer> answersForQuestion, int numRightAnswers)
        {
            int count = 0;
            foreach (var answ in answersForQuestion)            // Show all answers for the question
            {
                ConsoleManager.PrintMessage($"{++count}) {answ}");
            }
            return ConsoleManager.AskQuestion(numRightAnswers); // Return players answers from the console
        }

        bool IsCorrectAnswers(List<Answer> answers, List<int> playersAnswers)
        {
            int countRightAnswers = 0;
            int nextRightAnswer = 0;
            for(int i = 0; i < answers.Count(); i++) 
            {
                if(answers[i].IsCorrect == true && playersAnswers[nextRightAnswer++] == i + 1)      // If it is the correct answer and player choosed it
                {
                    countRightAnswers++;
                }
            }
            if(countRightAnswers == playersAnswers.Count())     // if number of the players answers are equal to the counted answers we return true
            {
                return true;
            }
            return false;
        }
    }
}
