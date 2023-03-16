using PantallaVuelosMAUI.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PantallaVuelosMAUI.Repositories
{
    public class VuelosRepository<T> where T : new()
    {
        SQLiteConnection context;

        public VuelosRepository()
        {
            context = new SQLiteConnection(Helpers.DatabaseHelper.RutaBD);
            context.CreateTable<Vuelo>();
            context.CreateTable<VueloBuffer>();
        }

        public TableQuery<T> GetAll()
        {
            return context.Table<T>();
        }

        public T Get(int id)
        {
            return context.Find<T>(id);
        }

        public void Insert(T t)
        {
            context.Insert(t);
        }

        public void Update(T t)
        {
            context.Update(t);
        }

        public void Delete(int id)
        {
            context.Delete<Vuelo>(id);
        }
    }
}
