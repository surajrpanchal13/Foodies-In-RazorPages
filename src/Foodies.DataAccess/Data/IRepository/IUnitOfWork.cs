using System;
using System.Collections.Generic;
using System.Text;

namespace Foodies.DataAccess.Data.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Category { get; }
        IFoodTypeRepository FoodType { get; }
        IMenuItemRepository MenuItem { get; }
        IApplicationUserRepository ApplicationUser { get; }
        void Save();
    }
}
