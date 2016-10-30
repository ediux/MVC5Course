using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;

namespace MVC5Course.Models
{
    public class ProductRepository : EFRepository<Product>, IProductRepository
    {
        public override IQueryable<Product> All()
        {
            return base.All().Where(w => w.IsDeleted == false).Take(25);
        }
        public System.Threading.Tasks.Task<IEnumerable<Product>> AllAsync()
        {
            return System.Threading.Tasks.Task.Run<IEnumerable<Product>>(() =>
            {
                return base.All().AsEnumerable();
            });
        }
        public Product Find(int id)
        {
            return base.All().FirstOrDefault(model => model.ProductId == id);
        }

        public override void Delete(Product entity)
        {
            entity.IsDeleted = true;
            this.UnitOfWork.Context.Entry<Product>(entity).State = EntityState.Modified;  
        }
    }

    public interface IProductRepository : IRepository<Product>
    {
        System.Threading.Tasks.Task<IEnumerable<Product>> AllAsync();
        Product Find(int id);
    }
}