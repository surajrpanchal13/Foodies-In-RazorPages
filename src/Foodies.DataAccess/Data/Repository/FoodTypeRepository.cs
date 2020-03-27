using Foodies.DataAccess.Data.IRepository;
using Foodies.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Foodies.DataAccess.Data.Repository
{
    public class FoodTypeRepository : Repository<FoodType>, IFoodTypeRepository
    {
        private readonly ApplicationDbContext _db;

        public FoodTypeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetFoodTypeListForDropDown()
        {
            return _db.FoodTypes.Select(s => new SelectListItem
            {
                Text = s.Name,
                Value = s.Id.ToString()
            });
        }

        public void Update(FoodType foodType)
        {
            FoodType updateFoodType = _db.FoodTypes.FirstOrDefault(f => f.Id == foodType.Id);

            updateFoodType.Name = foodType.Name;

            _db.SaveChanges();
        }
    }
}
