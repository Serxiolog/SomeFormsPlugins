using Database;
using FormForLab.IModels.FullModel;
using FormForLab.IModels.Interfaces;
using FormForLab.IModels.ViewModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models
{
    public class Product : IProduct
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public int CategoryId { get; set; }
        public int? Count { get; set; }
        public virtual Category Category { get; set; }
        public static Product Create(ShopDatabase context, ProductModel model)
        {
            return new()
            {
                Id = model.Id,
                Title = model.Title,
                Description = model.Description,
                CategoryId = model.CategoryId,
                Count = model.Count,
                Category = context.Categories.FirstOrDefault(c => c.Id == model.CategoryId)!,
            };
        }
        public void Update(ShopDatabase context, ProductModel model)
        {
            Title = model.Title;
            Description = model.Description;
            CategoryId = model.CategoryId;
            Count = model.Count;
            Category = context.Categories.FirstOrDefault(c => c.Id == model.CategoryId)!;
        }
        public ProductViewModel GetModel => new()
        {
            Id = Id,
            Title = Title,
            Description = Description,
            Category = Category.Name,
            Count = Count.HasValue ? Count.Value.ToString() : "Отсутствует",
        };
    }
}
