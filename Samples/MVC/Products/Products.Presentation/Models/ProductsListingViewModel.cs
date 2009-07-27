using System.Collections.Generic;
using Products.Core.Domain;

namespace Products.Presentation.Models
{
    public class ProductsListingViewModel
    {
        public IEnumerable<Product> Products { get; set; }
    }
}