using Database;
using FormForLab.IModels.BindingModel;
using FormForLab.IModels.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models
{
    public class Category : ICategory
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [ForeignKey("CategoryId")]
        public virtual List<Product> Products { get; set; } = new();
        public static Category Create(ShopDatabase context, CategoryModel model)
        {
            return new Category()
            {
                Id = model.Id,
                Name = model.Name
            };
        }
        public void Update(CategoryModel model)
        {
            Name = model.Name;
        }
        public CategoryModel GetModel => new()
        {
            Id = Id,
            Name = Name
        };

    }
}
