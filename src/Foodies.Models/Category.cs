using System.ComponentModel.DataAnnotations;

namespace Foodies.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }

        [Required]
        [Display(Name = "Category Order")]
        public int DisplayOrder { get; set; }
    }
}
