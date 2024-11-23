using Database.Models;
using FormForLab.IModels.FullModel;
using FormForLab.IModels.ViewModel;
using Microsoft.EntityFrameworkCore;


namespace Database.Implements
{
    public class ProductStorage
    {
        public List<ProductViewModel> GetFullList()
        {
            using var context = new ShopDatabase();
            return context.Products
                          .Include(x => x.Category)
                          .ToList()
                          .Select(p => p.GetModel)
                          .ToList();
        }

        public ProductViewModel? GetElement(int id)
        {
            using var context = new ShopDatabase();
            return context.Products
                          .Include(x => x.Category)
                          .FirstOrDefault(x => x.Id == id)?
                          .GetModel;
        }

        public ProductViewModel? Insert(ProductModel model)
        {
            using var context = new ShopDatabase();
            var newProduct = Product.Create(context, model);
            if (newProduct == null)
            {
                return null;
            }
            context.Products.Add(newProduct);
            context.SaveChanges();
            return newProduct.GetModel;
        }

        public ProductViewModel? Update(ProductModel model)
        {
            using var context = new ShopDatabase();
            var product = context.Products.FirstOrDefault(p => p.Id == model.Id);
            if (product == null)
            {
                return null;
            }
            product.Update(context, model);
            context.SaveChanges();
            return product.GetModel;
        }

        public ProductViewModel? Delete(ProductModel model)
        {
            using var context = new ShopDatabase();
            var product = context.Products.Include(x => x.Category).FirstOrDefault(x => x.Id == model.Id);
            if (product != null)
            {
                context.Products.Remove(product);
                context.SaveChanges();
                return product.GetModel;
            }
            return null;
        }
    }
}
