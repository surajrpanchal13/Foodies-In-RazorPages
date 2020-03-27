using Foodies.DataAccess.Data.IRepository;
using Foodies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Foodies.DataAccess.Data.Repository
{
    public class MenuItemRepository : Repository<MenuItem>, IMenuItemRepository
    {
        private readonly ApplicationDbContext _db;

        public MenuItemRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(MenuItem menuItem)
        {
            MenuItem updateMenuItem = _db.MenuItems.FirstOrDefault(f => f.Id == menuItem.Id);

            updateMenuItem.Name = menuItem.Name;
            updateMenuItem.CategoryId = menuItem.CategoryId;
            updateMenuItem.Description = menuItem.Description;
            updateMenuItem.FoodTypeId = menuItem.FoodTypeId;
            updateMenuItem.Price = menuItem.Price;
            if (!string.IsNullOrWhiteSpace(menuItem.Image))
            {
                updateMenuItem.Image = menuItem.Image;
            }
            _db.SaveChanges();
        }
    }
}
