using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PantallaVuelosMAUI.Models
{
    public class Vuelo
    {
        [PrimaryKey]
        public int Id { get; set; }

        public string Destino { get; set; } = null!;

        public string Numerovuelo { get; set; } = null!;

        public int Puerta { get; set; }

        public string Estado { get; set; } = "";

        public DateTime Fecha { get; set; } = DateTime.Now;

        [JsonIgnore, Ignore]
        public DateTime Date
        {
            get { return Fecha.Date; }

            set 
            { 
                Fecha = new DateTime(value.Year,value.Month,value.Day,Fecha.Hour, Fecha.Minute,Fecha.Second);
            }
        }

        [JsonIgnore, Ignore]
        public TimeSpan Hour
        {
            get { return new TimeSpan(Fecha.Hour, Fecha.Minute, Fecha.Second); }

            set
            {
                Fecha = new DateTime(Fecha.Year, Fecha.Month, Fecha.Day, value.Hours, value.Minutes, value.Seconds);
            }
        }

        public DateTime UltimaEdicionFecha { get; set; }  


    }
}
