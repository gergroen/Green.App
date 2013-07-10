using Green.App.Model;
using NHibernate;

namespace Green.App.Dao.NHibernate
{
    public class NHibernateRepository : IRepository
    {
        private ISessionFactory _sessionFactory;

        public NHibernateRepository(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }
    }
}
