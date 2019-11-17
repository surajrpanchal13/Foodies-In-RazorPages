using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Foodies.DataAccess.Data.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace Taste.Pages.Admin.Category
{
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public Foodies.Models.Category Category { get; set; }

        public UpsertModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        

        public IActionResult OnGet(int? id)
        {
            if (id.HasValue)
            {
                Category = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
                if (Category == null)
                {
                    return NotFound();
                }
            }
            else
            {
                Category = new Foodies.Models.Category();
            }
            return Page();

        }


        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if(Category.Id == 0)
            {
                _unitOfWork.Category.Add(Category);
            }
            else
            {
                _unitOfWork.Category.Update(Category);
            }
            _unitOfWork.Save();
            return RedirectToPage("./Index");
        }
    }
}