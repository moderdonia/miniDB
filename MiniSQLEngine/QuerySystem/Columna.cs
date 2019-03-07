using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQLEngine.QuerySystem
{
    class Columna
    {
        public string nombre { get; set; }
        public string tipo { get; set; }

        public Columna(string nombre, string tipo)
        {
            this.nombre = nombre;
            this.tipo = tipo;
        }
    }
}
