using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Handicraft.Models;
using Handicraft.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Handicraft.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors("Cors")]
    public class ProductController : Controller
    {
        private HandicraftContext db;
        private ISystemService systemService;

        public ProductController(HandicraftContext db, ISystemService systemService)
        {
            this.db = db;
            this.systemService = systemService;
        }

        [HttpGet]
        public IActionResult GetProduct()
        {
            var query = db.Product.Select(e => new
            {
                e.Id,
                e.Introduction,
                e.Name,
                e.Price,
                e.Type,
                BannerImage = e.ProductImage.FirstOrDefault().BannerImage.Select(b=>b.ImagePath),
                DetailImage = e.ProductImage.FirstOrDefault().DetailImage.Select(b => b.ImagePath)
            });
            return Json(new { List= query });
        }
        [HttpGet]
        public IActionResult GetProductByType(string type)
        {
            var query=db.Product.Where(e=>e.Type==type).Select(e => new
            {
                e.Id,
                e.Introduction,
                e.Name,
                e.Price,
                e.Type,
                BannerImage = e.ProductImage.FirstOrDefault().BannerImage.Select(b => b.ImagePath),
                DetailImage = e.ProductImage.FirstOrDefault().DetailImage.Select(b => b.ImagePath)
            });
            return Json(new { List = query });
        }
        [HttpGet]
        public IActionResult GetSingleProduct(Guid productId)
        {
            var query = db.Product.Where(e=>e.Id==productId).Select(e => new
            {
                e.Id,
                e.Introduction,
                e.Name,
                e.Price,
                e.Type,
                BannerImage = e.ProductImage.FirstOrDefault().BannerImage.Select(b => b.ImagePath),
                DetailImage = e.ProductImage.FirstOrDefault().DetailImage.Select(b => b.ImagePath)
            });
            return Json(query);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody]Product param)
        {
            db.Product.Add(new Product
            {
                Type=param.Type,
                Introduction=param.Introduction,
                Name=param.Name,
                Price=param.Price,
                Tips=param.Tips
            });
            await db.SaveChangesAsync();

            db.ProductImage.Add(new ProductImage
            {
                ProductId = db.Product.Where(e => e.Name == param.Name).FirstOrDefault().Id
            });

            return await db.SaveChangesAsync() > 0 ?
                Json("Commit success") :
                Json("Commit fail");
        }

        [HttpGet]
        public IActionResult AddBannerImage(Guid productId,string imageList)
        {
            var productImageId = db.ProductImage.Where(e => e.ProductId == productId).FirstOrDefault().Id;
            foreach (var item in imageList.Split(','))
            {
                db.BannerImage.Add(new BannerImage
                {
                    ProductImageId=productImageId,
                    ImagePath = item
                });
            }
            return db.SaveChanges() > 0 ?
                Json("Commit success") :
                Json("Commit fail");
        }

        [HttpGet]
        public IActionResult AddDetailImage(Guid productId,string imageList)
        {
            var productImageId = db.ProductImage.Where(e => e.ProductId == productId).FirstOrDefault().Id;
            foreach (var item in imageList.Split(','))
            {
                db.DetailImage.Add(new DetailImage
                {
                    ProductImageId = productImageId,
                    ImagePath = item
                });
            }
            return db.SaveChanges() > 0 ?
                Json("Commit success") :
                Json("Commit fail");
        }

        [HttpGet]
        public IActionResult RemoveProduct(Guid productId)
        {
            var query = db.Product.Where(e => e.Id == productId).FirstOrDefault();
            if (query == null)
                return Json("Without product");
            db.Product.Remove(query);
            return db.SaveChanges() > 0 ?
                Json("Commit success") :
                Json("Commit fail");
        }

        [HttpPost]
        public IActionResult UploadFile([FromForm(Name = "files")]List<IFormFile> files,string productType,string productName)
        {
            return Json(systemService.UploadIForm(files, productType,productName));
        }

    }
}