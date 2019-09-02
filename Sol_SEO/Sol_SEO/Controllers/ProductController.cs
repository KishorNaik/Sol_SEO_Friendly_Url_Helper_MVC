using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Sol_SEO.Models;
using Sol_SEO.ViewModel;

namespace Sol_SEO.Controllers
{
    public class ProductController : Controller
    {

        #region Constructor
        public ProductController()
        {
            ProductVM = new ProductViewModel();
        }
        #endregion 

        #region Property
        [BindProperty]
        public ProductViewModel ProductVM { get; set; }

        [BindProperty(SupportsGet =true)]
        public int? id { get; set; }

        [BindProperty(SupportsGet =true)]
        public string seocontent { get; set; }
        #endregion


        #region Private Method
        private async Task<IEnumerable<ProductModel>> GetProductsDataAsync()
        {
            return await Task.Run(() => {

                var productList = new List<ProductModel>()
                {
                    new ProductModel()
                    {
                        Id=1,
                        ProductName="mac book pro"
                    },
                    new ProductModel()
                    {
                        Id=2,
                        ProductName="hp elite notebook"
                    },
                    new ProductModel()
                    {
                        Id=3,
                        ProductName="lenova yoga"
                    }
                };

                return productList;
            });
        }
        #endregion 


        #region Actions
        public async  Task<IActionResult> Index()
        {
            // Get Products Data
            ProductVM.ListProducts = await this.GetProductsDataAsync();

            // Store Product List on Temp Data for Filtering
            TempData["ProductViewModel"] = JsonConvert.SerializeObject(ProductVM); // Serialize JSON
            TempData.Keep();
            return View(ProductVM);
        }

        [HttpGet("detail/{id}/{seocontent}")]
        public IActionResult Detail()
        {
            // deSerialize JSON
            ProductVM = JsonConvert.DeserializeObject<ProductViewModel>(TempData.Peek("ProductViewModel") as String);

            // Filter Data
            ProductVM.Product =
                                ProductVM
                                ?.ListProducts
                                ?.FirstOrDefault((leProductModel) => leProductModel.Id == id);

            return View(ProductVM);
        }

        #endregion 
    }
}