using Foodies.DataAccess.Data.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Foodies.Pages.Admin.FoodType
{
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public Models.FoodType FoodType { get; set; }

        public UpsertModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult OnGet(int? id)
        {
            if (id.HasValue)
            {
                FoodType = _unitOfWork.FoodType.GetFirstOrDefault(u => u.Id == id);
                if (FoodType == null)
                {
                    return NotFound();
                }
            }
            else
            {
                FoodType = new Models.FoodType();
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (FoodType.Id == 0)
            {
                _unitOfWork.FoodType.Add(FoodType);
            }
            else
            {
                _unitOfWork.FoodType.Update(FoodType);
            }
            _unitOfWork.Save();
            return RedirectToPage("./Index");
        }
    }
}