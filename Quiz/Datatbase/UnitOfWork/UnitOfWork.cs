using Quiz.Datatbase.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Datatbase.UnitOfWork
{
    public class UnitOfWork : IDisposable
    {
        private DatabaseContext context;

        private AccountRepository accountRepository;
        private AnswerRepository answerRepository;
        private QuestionRepository questionRepository;
        private StatisticRepository statisticRepository;
        private UserRepository userRepository;
        private CategoryRepository categoryRepository;

        public AccountRepository AccountRepository => accountRepository ?? (accountRepository = new AccountRepository(context));
        public AnswerRepository AnswerRepository => answerRepository ?? (answerRepository = new AnswerRepository(context));
        public QuestionRepository QuestionRepository => questionRepository ?? (questionRepository = new QuestionRepository(context));
        public StatisticRepository StatisticRepository => statisticRepository ?? (statisticRepository = new StatisticRepository(context));
        public UserRepository UserRepository => userRepository ?? (userRepository = new UserRepository(context));
        public CategoryRepository CategoryRepository => categoryRepository ?? (categoryRepository = new CategoryRepository(context));

        public UnitOfWork()
        {
            context = new DatabaseContext();
        }
        public void Save()
        {
            context.SaveChanges();
        }

        bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
                disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
