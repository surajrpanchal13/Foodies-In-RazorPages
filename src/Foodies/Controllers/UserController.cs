using System;
using System.Collections.Generic;
using System.Linq;
using Foodies.DataAccess.Data.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace Taste.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var categoryList = _unitOfWork.ApplicationUser.GetAll().ToList();
            return Json(new { data = categoryList });
        }


        [HttpPost("{id}")]
        public IActionResult LockUnlock([FromBody]string id)
        {
            var objFromDb = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while Locking/Unlocking" });
            }
            if(objFromDb.LockoutEnd.HasValue && objFromDb.LockoutEnd > DateTimeOffset.Now)
            {
                objFromDb.LockoutEnd = DateTimeOffset.Now;
            }
            else
            {
                objFromDb.LockoutEnd = DateTimeOffset.Now.AddDays(1);
            }

            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete successful" });
        }

    }
}