using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Handicraft.Models;

namespace Handicraft.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors("Cors")]
    public class ShoppingCarController : Controller
    {
        private readonly HandicraftContext db;

        public ShoppingCarController(HandicraftContext db)
        {
            this.db = db;
        }

        [HttpPost]
        public IActionResult AddShoppingCar([FromBody]ShoppingCar param)
        {
            bool checkUser = db.UserInfo.Where(e => e.Id == param.UserId).FirstOrDefault() == null;
            bool checkProduct = db.UserInfo.Where(e => e.Id == param.UserId).FirstOrDefault() == null;
            if (checkUser)
                return Json("without user");
            if (checkProduct)
                return Json("without product");

            var query = db.ShoppingCar.Where(e => e.ProductId ==param.ProductId && e.UserId == param.UserId).FirstOrDefault();
            if (query != null)
            {
                query.Count = query.Count + param.Count;
                query.Amount = db.Product.Where(e => e.Id == param.ProductId).FirstOrDefault().Price * query.Count;
                return db.SaveChanges() > 0 ?
                Json("commit success") :
                Json("commit fail");
            }
                
            db.ShoppingCar.Add(new ShoppingCar
            {
                UserId = param.UserId,
                ProductId=param.ProductId,
                Amount = db.Product.Where(e => e.Id == param.ProductId).FirstOrDefault().Price * param.Count,
                Count =  param.Count
            });

            return db.SaveChanges() > 0 ?
                Json("commit success") :
                Json("commit fail");
        }

        [HttpGet]
        public IActionResult AlterProductCount(Guid productId, Guid userId, int count)
        {
            var query = db.ShoppingCar.Where(e => e.ProductId == productId && e.UserId == userId).FirstOrDefault();
            if (query != null)
            {
                query.Count = count;
                query.Amount = db.Product.Where(e => e.Id == productId).FirstOrDefault().Price * count;
            }
            return db.SaveChanges() > 0 ?
                Json("commit success") :
                Json("commit fail");
        }

        [HttpGet]
        public IActionResult RemoveProductInShoppingCar(Guid productId,Guid userId)
        {
            var query = db.ShoppingCar.Where(e => e.ProductId == productId && e.UserId == userId).FirstOrDefault();
            if(query!=null)
                db.ShoppingCar.Remove(query);
            return db.SaveChanges() > 0 ?
                Json("commit success") :
                Json("commit fail");
        }

        [HttpGet]
        public IActionResult GetShoppingCarList(Guid userId)
        {
            var query = db.ShoppingCar.Where(e => e.UserId == userId).ToList();
            var list = query.Select(e => new
            {
                e.Id,
                e.Amount,
                e.Count,
                Product = db.Product.Where(p => p.Id == e.ProductId).Select(p => new
                {
                    p.Id,
                    p.Name,
                    p.Price,
                    p.ProductImage.FirstOrDefault().BannerImage.FirstOrDefault().ImagePath
                }).FirstOrDefault()
            }) ;
            return Json(new { List = list });
        }

        [HttpGet]
        public IActionResult GetShoppingCarTotalAmount(string ids,Guid userId)
        {
            if (ids == null)
                return Json(new { totalAmount = 0 });
            var list = ids.Split(',', StringSplitOptions.RemoveEmptyEntries);
            decimal totalAmount = 0;
            foreach (var item in list)
            {
                Guid productId = Guid.Parse(item);
                var amount = db.ShoppingCar.Where(s => s.ProductId == productId && s.UserId == userId).FirstOrDefault().Amount;
                totalAmount += amount;
            }
            return Json(new { totalAmount });
        }
    }
}