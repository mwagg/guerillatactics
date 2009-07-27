using System.Collections.Generic;

namespace Products.Core.Domain.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
    }
}