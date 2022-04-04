using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Laboratorio8
{
    public partial class Contact : Page
    {
        static List<Jugador> juagadores = new List<Jugador>();
        static List<Resultado> resultados = new List<Resultado>();
        static List<ReporteJuego> reportes = new List<ReporteJuego>();

        public void ReadJuagador()
        {
            string fileName = Server.MapPath("~/zJugadores.txt");

            FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);

            while (reader.Peek() > -1)
            {
                var linea = reader.ReadLine();
                var partes = linea.Split('|');

                Jugador jugador = new Jugador();
                jugador.IdJugador = Convert.ToInt32(partes[0]);
                jugador.Name = partes[1];
                jugador.Team = partes[2];

                juagadores.Add(jugador);
            }
            reader.Close();
        }
        public void ReadResultado()
        {
            string fileName = Server.MapPath("~/zResultados.txt");

            FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);

            while (reader.Peek() > -1)
            {
                var linea = reader.ReadLine();
                var partes = linea.Split('|');

                Resultado res = new Resultado();
                res.IdJugador = Convert.ToInt32(partes[0]);
                res.Fecha = Convert.ToDateTime(partes[1]);
                res.Team = partes[2];
                res.Goles = Convert.ToInt32(partes[3]);

                resultados.Add(res);
            }
            reader.Close();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            ReadJuagador();
            ReadResultado();

            for (int i = 0; i < resultados.Count; i++)
            {
                for (int j = 0; j < juagadores.Count; j++)
                {
                    if  (resultados[i].IdJugador == juagadores[j].IdJugador)
                    {
                        ReporteJuego reporte = new ReporteJuego();
                        reporte.Name = juagadores[j].Name;
                        reporte.Goles = resultados[i].Goles;

                        reportes.Add(reporte);
                    }
                }
            }
            //para ordenar
            reportes = reportes.OrderBy(g => g.Goles).ToList();

            GridView1.DataSource = reportes;
            GridView1.DataBind();

            //para sacar promedio de todos los goles
            double promedio = reportes.Average(g => g.Goles);
             Label1.Text = promedio.ToString();

        }
    }
}