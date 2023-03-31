using SQLite;
using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RifasMAUIApp.Models
{
    public class Boleto
    {
        [PrimaryKey]
        public int Id { get; set; }

        public uint NumeroBoleto { get; set; }

        public string NombrePersona { get; set; } = null!;

        public string NumeroTelefono { get; set; } = null!;

        public ulong Pagado { get; set; }



    }
}
