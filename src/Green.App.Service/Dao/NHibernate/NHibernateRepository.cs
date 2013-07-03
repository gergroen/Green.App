using Green.App.Service.Model;
using NHibernate;

namespace Green.App.Service.Dao.NHibernate
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
