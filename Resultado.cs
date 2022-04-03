using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Laboratorio8
{
    public class Resultado
    {
        public int IdJugador { get; set; }
        public DateTime Fecha { get; set; }
        public string Team { get; set; }
        public int Goles { get; set; }
    }
}