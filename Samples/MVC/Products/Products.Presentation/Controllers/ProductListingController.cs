using System.Web.Mvc;
using Products.Core.Domain.Repositories;
using Products.Presentation.Models;

namespace Products.Presentation.Controllers
{
    public class ProductListingController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ProductListingController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Index()
        {
            var productsListing = new ProductsListingViewModel {Products = _productRepository.GetAll()};
            return View(productsListing);
        }
    }
}