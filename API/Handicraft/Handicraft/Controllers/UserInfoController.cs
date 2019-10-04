using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Handicraft.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Handicraft.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors("Cors")]
    public class UserInfoController : Controller
    {
        private HandicraftContext db;

        public UserInfoController(HandicraftContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public IActionResult Test()
        {
            return Ok("Test successful");
        }

        [HttpGet]
        public IActionResult GetUserInfo()
        {
            return Json(db.UserInfo.ToList());
        }

        [HttpPost]
        public IActionResult AddUserInfo([FromBody]UserInfo param)
        {
            var query = db.UserInfo.Where(e => e.OpenId == param.OpenId || e.Id==param.Id).FirstOrDefault();
            if (query != null)
                return Json(new { wrong = "User already exists" });
            db.UserInfo.Add(new UserInfo
            {
                FirstName = param.FirstName,
                LastName=param.LastName,
                Address=param.Address,
                City=param.City,
                OpenId=param.OpenId
            });
            return db.SaveChanges() > 0 ?
                Json("Commit success") :
                Json("Commit fail");
        }

        [HttpGet]
        public IActionResult RemoveUserInfo(Guid id)
        {
            db.UserInfo.Remove(db.UserInfo.Where(e => e.Id == id).SingleOrDefault());
            return db.SaveChanges() > 0 ?
                Json("Commit success") :
                Json("Commit fail");
        }

        [HttpPost]
        public IActionResult AlterUserInfo([FromBody]UserInfo param)//26950d65-e061-4f22-947b-b49c8df10c5b
        {
            var data = db.UserInfo.Where(e => e.Id == param.Id).FirstOrDefault();
            if (data == null)
                return Json(new {error="Without this User"});

            data.FirstName = param.FirstName == null ? data.FirstName : param.FirstName;
            data.City = param.City == null ? data.City : param.City;
            data.Address = param.Address == null ? data.Address : param.Address;
            data.LastName = param.LastName == null ? data.LastName : param.LastName;
            
            return db.SaveChanges() > 0 ?
                Json("Commit success") :
                Json("Commit fail");
        }

        [HttpGet]
        public IActionResult GetSingleUserInfo(string openId)
        {
            return Json(db.UserInfo.Where(e => e.OpenId == openId).FirstOrDefault());
        }

    }
}