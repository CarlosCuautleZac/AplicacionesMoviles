using RifasAPI.Models;

namespace RifasAPI.Repostitories
{
    public class BoletosRepository
    {
        private readonly Sistem21RifasContext context;

        public BoletosRepository(Sistem21RifasContext context)
        {
            this.context = context;
        }

        public IEnumerable<Boletos> GetAll()
        {
            return context.Boletos.OrderBy(x => x.NumeroBoleto).Where(x => x.Eliminado == 0);
        }

        public Boletos? GetById(int id)
        {
            return context.Boletos.Find(id);
        }


    }
}
