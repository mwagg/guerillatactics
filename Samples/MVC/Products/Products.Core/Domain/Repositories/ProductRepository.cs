using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Linq;

namespace Products.Core.Domain.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ISession _session;

        public ProductRepository(ISession session)
        {
            _session = session;
        }

        public IEnumerable<Product> GetAll()
        {
            return _session.Linq<Product>().ToList();
        }
    }
}