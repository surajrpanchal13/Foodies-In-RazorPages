using Foodies.DataAccess.Data.IRepository;
using Foodies.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.IO;

namespace Foodies.Pages.Admin.MenuItem
{
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        [BindProperty]
        public MenuItemViewModel MenuItemObj { get; set; }

        public UpsertModel(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult OnGet(int? id)
        {
            MenuItemObj = new MenuItemViewModel
            {
                Categories = _unitOfWork.Category.GetCategoryListForDropDown(),
                FoodType = _unitOfWork.FoodType.GetFoodTypeListForDropDown()
            };
            if (id.HasValue)
            {
                MenuItemObj.MenuItem = _unitOfWork.MenuItem.GetFirstOrDefault(u => u.Id == id);
                if (MenuItemObj.MenuItem == null)
                {
                    return NotFound();
                }
            }
            else
            {
                MenuItemObj.MenuItem = new Models.MenuItem();
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            var files = HttpContext.Request.Form.Files;

            if (!ModelState.IsValid)
            {
                return Page();
            }
            string menuItemFolderPath = "\\images\\menuitems\\";
            string fileName = Guid.NewGuid().ToString();
            var upload = Path.Combine(_webHostEnvironment.WebRootPath, menuItemFolderPath.TrimStart('\\'));
            if (MenuItemObj.MenuItem.Id == 0)
            {
                var extension = Path.GetExtension(files[0].FileName);

                using (FileStream fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                };

                MenuItemObj.MenuItem.Image = menuItemFolderPath + fileName + extension;

                _unitOfWork.MenuItem.Add(MenuItemObj.MenuItem);
            }
            else
            {
                var menuItem = _unitOfWork.MenuItem.Get(MenuItemObj.MenuItem.Id);
                if(files.Count > 0)
                {
                    var extension = Path.GetExtension(files[0].FileName);
                    string oldImage = Path.Combine(_webHostEnvironment.WebRootPath, menuItem.Image.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImage))
                    {
                        System.IO.File.Delete(oldImage);
                    }
                    using (FileStream fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    };
                    MenuItemObj.MenuItem.Image = menuItemFolderPath + fileName + extension;
                }
                else
                {
                    MenuItemObj.MenuItem.Image = menuItem.Image;
                }
                _unitOfWork.MenuItem.Update(MenuItemObj.MenuItem);
            }
            _unitOfWork.Save();
            return RedirectToPage("./Index");
        }
    }
}