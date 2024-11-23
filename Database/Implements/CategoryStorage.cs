using Database.Models;
using FormForLab.IModels.BindingModel;

namespace Database.Implements
{
    public class CategoryStorage
    {
        public List<CategoryModel> GetFullList()
        {
            using var context = new ShopDatabase();
            return context.Categories.ToList().Select(c => c.GetModel).ToList();
        }
        public CategoryModel? GetElement(int id)
        {
            using var context = new ShopDatabase();
            return context.Categories.FirstOrDefault(x => x.Id == id)?.GetModel;
        }

        public CategoryModel? Insert(CategoryModel model)
        {
            using var context = new ShopDatabase();
            var newCategory = Category.Create(context, model);
            if (newCategory == null)
            {
                return null;
            }
            context.Categories.Add(newCategory);
            context.SaveChanges();
            return newCategory.GetModel;
        }

        public CategoryModel? Update(CategoryModel model)
        {
            using var context = new ShopDatabase();
            var category = context.Categories.FirstOrDefault(c => c.Id == model.Id);
            if (category == null)
            {
                return null;
            }
            category.Update(model);
            context.SaveChanges();
            return category.GetModel;
        }

        public CategoryModel? Delete(CategoryModel model)
        {
            using var context = new ShopDatabase();
            var category = context.Categories.FirstOrDefault(x => x.Id == model.Id);
            if (category != null)
            {
                context.Categories.Remove(category);
                context.SaveChanges();
                return category.GetModel;
            }
            return null;

        }
    }
}
