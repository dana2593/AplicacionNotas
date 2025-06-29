using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace AplicacionNotas.Models
{
    [Table("notas")]
    public class Nota
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(250)]
        public string Texto { get; set; }

        public DateTime Fecha { get; set; }
    }
}