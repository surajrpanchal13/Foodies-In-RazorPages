using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Foodies.Models.ViewModels
{
    public class MenuItemViewModel
    {

        public MenuItem MenuItem { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
        public IEnumerable<SelectListItem> FoodType { get; set; }
    }
}
