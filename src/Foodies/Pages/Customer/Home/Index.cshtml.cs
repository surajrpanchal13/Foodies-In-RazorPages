using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Foodies.DataAccess.Data.IRepository;
using Foodies.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Foodies
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<MenuItem> MenuItemList { get; set; }

        public IEnumerable<Category> Categories { get; set; }

        public void OnGet()
        {
            MenuItemList = _unitOfWork.MenuItem.GetAll(null, null, "Category,FoodType");
            Categories = _unitOfWork.Category.GetAll(null, c => c.OrderBy(o => o.DisplayOrder), null);
        }
    }
}