﻿using Foodies.DataAccess.Data.IRepository;
using Foodies.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Foodies.DataAccess.Data.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetCategoryListForDropDown()
        {
            return _db.Categories.Select(s => new SelectListItem
            {
                Text = s.Name,
                Value = s.Id.ToString()
            });
        }

        public void Update(Category category)
        {
            Category updateCategory = _db.Categories.FirstOrDefault(f => f.Id == category.Id);

            updateCategory.Name = category.Name;
            updateCategory.DisplayOrder = category.DisplayOrder;

            _db.SaveChanges();
        }
    }
}
