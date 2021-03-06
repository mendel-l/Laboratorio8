using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Laboratorio8
{
    public partial class _Default : Page
    {
        static List<Jugador> juagadores = new List<Jugador>();

        public void Read()
        {
            string fileName = Server.MapPath("~/zJugadores.txt");

            FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);

            juagadores.Clear();
             
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

        private void Save(Resultado datoRes)
        {
            string fileName = Server.MapPath("~/zResultados.txt");
            
            FileStream stream = new FileStream(fileName, FileMode.Append, FileAccess.Write);
            StreamWriter writer = new StreamWriter(stream);

            var AgregartxtRes = datoRes.IdJugador + "|" + datoRes.Fecha + "|" + datoRes.Team + "|" + datoRes.Goles;
            writer.WriteLine(AgregartxtRes);   
            
            writer.Close();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Read();

            DropDownList1.DataTextField = "Name";
            DropDownList1.DataValueField = "IdJugador";
            //DropDownList1.DataMember = "Name";
            DropDownList1.DataSource = juagadores;
            DropDownList1.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Resultado res = new Resultado();

            res.IdJugador = Convert.ToInt32(DropDownList1.SelectedValue);
            res.Fecha = Calendar1.SelectedDate;
            res.Team = TextBox1.Text;
            res.Goles = Convert.ToInt32(TextBox2.Text);

            Save(res);

            Calendar1.SelectedDate = DateTime.Now;
            TextBox1.Text = "";
            TextBox2.Text = "";
        }
    }
}