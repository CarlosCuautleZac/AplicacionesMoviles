using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PantallaVuelosMAUI.Models
{
    public class Vuelo
    {
        public int Id { get; set; }

        public string Destino { get; set; } = null!;

        public string Numerovuelo { get; set; } = null!;

        public int Puerta { get; set; }

        public string Estado { get; set; } = null!;

        public DateTime Fecha { get; set; }

        public DateTime UltimaEdicionFecha { get; set; }


    }
}
