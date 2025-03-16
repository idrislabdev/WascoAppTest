using System.ComponentModel.DataAnnotations;

namespace WascoAppTest.ViewModels.Categories
{
    public class CategoryFormViewModel
    {

        [MaxLength(100)]
        [Display(Name = "Category Name")]
        [Required(ErrorMessage = "Category Name is required.")]
        public string CategoryName { get; set; } = string.Empty;
        public int CategoryId { get; set; }
    }
}
