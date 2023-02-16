using AerolineaTECAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AerolineaTECAPI.Repositories
{
    public class Repository<T> where T:class 
    {
        private readonly Sistem21AerolineadbContext context;
        public Repository(Sistem21AerolineadbContext context)
        {
            this.context = context;
        }
        public DbSet<T> Get()
        {
            return context.Set<T>();
        }
        public T? Get(object id)
        {
            return context.Find<T>(id);
        }
        public void Insert(T entity)
        {
            context.Add(entity);
            context.SaveChanges();
        }
        public void Update(T entity)
        {
            context.Update(entity);
            context.SaveChanges();
        }
        public void Delete(T entity)
        {
            context.Remove(entity);
            context.SaveChanges();
        }
    }
}
