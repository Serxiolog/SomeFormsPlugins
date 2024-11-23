using Database.Implements;
using FormForLab.IModels.BindingModel;
using FormForLab.IModels.FullModel;
using FormForLab.IModels.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormForLab.BisnesLogic
{
    public class FullLogic
    {
        CategoryStorage categoryStorage;
        ProductStorage productStorage;

        public FullLogic()
        {
            categoryStorage = new();
            productStorage = new();
        }
        public List<CategoryModel>? GetCategories()
        {
            var list = categoryStorage.GetFullList();
            return list;
        }
        public CategoryModel? GetCategory(int id)
        {
            var category = categoryStorage.GetElement(id);
            return category;
        }
        public CategoryModel? CreateCategory(CategoryModel category)
        {
            return categoryStorage.Insert(category);
        }

        public CategoryModel? UpdateCategory(CategoryModel category)
        {
            return categoryStorage.Update(category);
        }

        public CategoryModel? DeleteCategory(CategoryModel category)
        {
            return categoryStorage?.Delete(category);
        }

        public List<ProductViewModel>? GetProducts()
        {
            var list = productStorage.GetFullList();
            return list;
        }

        public ProductViewModel? GetProduct(int id)
        {
            var product = productStorage.GetElement(id);
            return product;
        }

        public ProductViewModel? CreateProduct(ProductModel product)
        {
            return productStorage.Insert(product);
        }

        public ProductViewModel? UpdateProduct(ProductModel product)
        {
            return productStorage.Update(product);
        }

        public ProductViewModel? DeleteProduct(ProductModel product)
        {
            return productStorage?.Delete(product);
        }

    }
}
