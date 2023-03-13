using PantallaVuelosMAUI.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PantallaVuelosMAUI.Repositories
{
    public class VuelosRepository
    {
        SQLiteConnection context;

        public VuelosRepository()
        {
            context = new SQLiteConnection(Helpers.DatabaseHelper.RutaBD);
            context.CreateTable<Vuelo>();
        }

        public List<Vuelo> GetAll()
        {
            return new List<Vuelo>();
        }

        public List<Vuelo> Get(int id)
        {
            return new List<Vuelo>(id);
        }

        public void Insert(Vuelo v)
        {
            context.Insert(v);
        }

        public void Update(Vuelo v)
        {
            context.Update(v);
        }

        public void Delete(int id)
        {
            context.Delete<Vuelo>(id);
        }
    }
}
