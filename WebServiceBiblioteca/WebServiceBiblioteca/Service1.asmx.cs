using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;

namespace WebServiceBiblioteca
{
    /// <summary>
    /// Descripción breve de Service1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class Service1 : System.Web.Services.WebService
    {

        //Metodo inicioSesion(String numero_control)
        //Su finalidad es validar si el numero de control del usuario (alumno) existe
        [WebMethod]
        public string descargarUsuarios()
        {
            String registro = "";
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Server=JONATHAN\\SQLEXPRESS;" +
                    "Database=bibliotecaitch;User Id=sa; Password=Jonhy1394Therz?;";
            try
            {
                conn.Open();
                SqlDataAdapter cmd = new SqlDataAdapter("SELECT [numero_control],[nombre],[apellido_paterno],"+
                    "[apellido_materno],[carrera] FROM [bibliotecaitch].[dbo].[usuario];", conn);
                DataSet data = new DataSet();
                cmd.Fill(data, "datos");
                DataTable tabla = data.Tables[0];

                for (int i = 0; i < tabla.Rows.Count; i++)
                {
                    Array a = tabla.Rows[i].ItemArray;
                    registro += a.GetValue(0).ToString() + ",";
                    registro += a.GetValue(1).ToString() + ",";
                    registro += a.GetValue(2).ToString() + ",";
                    registro += a.GetValue(3).ToString() + ",";
                    registro += a.GetValue(4).ToString() + "*";
                }

            }
            catch (Exception e) { registro = "Conexion No Exitosa"; }
            finally
            {
                conn.Close();
            }
            return registro;
        }



        //Metodo VerPrestamo(string numero_control)
        //Su finalidad es devolver todos los prestamos relacionados al numero_control
        [WebMethod]
        public string VerPrestamo(string numero_control)
        {
            String registro = "";
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Server=JONATHAN\\SQLEXPRESS;" +
                    "Database=bibliotecaitch;User Id=sa; Password=Jonhy1394Therz?;";

            try
            {
                conn.Open();
                SqlDataAdapter cmd = new SqlDataAdapter("select p.id, m.id, p.fecha_prestamo,"+
                    " p.fecha_vigencia from material m,prestamo p where p.id_usuario= '"+
                    numero_control+"' AND m.id = p.id_material;", conn);
                DataSet data = new DataSet();
                cmd.Fill(data, "datos");
                DataTable tabla = data.Tables[0];
                if (tabla.Rows.Count > 0)
                {
                    for (int i = 0; i < tabla.Rows.Count; i++)
                    {
                        Array a = tabla.Rows[i].ItemArray;
                        registro += a.GetValue(0).ToString() + ",";
                        registro += a.GetValue(1).ToString() + ",";
                        registro += a.GetValue(2).ToString() + ",";
                        registro += a.GetValue(3).ToString() + "*";
                    }
                }
                else
                {
                    registro = "sin datos";
                }
                

            }
            catch (Exception e) { registro = "Conexion No Exitosa"; }
            finally
            {
                conn.Close();
            }
            return registro;
        }

        [WebMethod]
        public string idPrestamo() 
        {
            String registro = "";
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Server=JONATHAN\\SQLEXPRESS;" +
                    "Database=bibliotecaitch;User Id=sa; Password=Jonhy1394Therz?;";

            try
            {
                conn.Open();
                SqlDataAdapter cmd = new SqlDataAdapter("select next value for secuencia;", conn);
                DataSet data = new DataSet();
                cmd.Fill(data, "datos");
                DataTable tabla = data.Tables[0];

                for (int i = 0; i < tabla.Rows.Count; i++)
                {
                    Array a = tabla.Rows[i].ItemArray;
                    registro += a.GetValue(0).ToString();
                }

            }
            catch (Exception e)
            {
                registro = "Conexion No Exitosa";
            }
            finally
            {
                conn.Close();
            }

            return registro;

        }

        //Metodo InsertarPrestamo
        //Su finalidad es agregar un nuevo registro en la tabla de prestamo
        //de acuerdo a los parametros de entrada
        [WebMethod]
        public string InsertarPrestamo(string id, string id_usuario, string id_material,
            string fecha_prestamo, string fecha_vigencia)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Server=JONATHAN\\SQLEXPRESS;" +
                    "Database=bibliotecaitch;User Id=sa; Password=Jonhy1394Therz?;";

            try
            {
                conn.Open();
                String instruccion =
                    "Insert into prestamo (id,id_usuario," +
                              "id_material,fecha_prestamo,fecha_vigencia) " +
                    "Values ('"+id+"'," +
                    "'" + id_usuario + "'," +
                    "'" + id_material + "'," +
                    "'" + fecha_prestamo + "'," +
                    "'" + fecha_vigencia + "'" +
                    ")";

                SqlCommand com = new SqlCommand(instruccion, conn);
                com.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                return e.ToString();
            }
            finally
            {
                conn.Close();
            }
            return "ok";
        }


        //Metodo VerMateriales
        //Su finalidad es mostrar todos los materiales biliograficos
        //de la base de datos.
        [WebMethod]
        public string VerMateriales()
        {
            String registro = "";
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Server=JONATHAN\\SQLEXPRESS;" +
                    "Database=bibliotecaitch;User Id=sa; Password=Jonhy1394Therz?;";

            try
            {
                conn.Open();
                SqlDataAdapter cmd = new SqlDataAdapter("SELECT id, titulo FROM material;", conn);
                DataSet data = new DataSet();
                cmd.Fill(data, "datos");
                DataTable tabla = data.Tables[0];

                for (int i = 0; i < tabla.Rows.Count; i++)
                {
                    Array a = tabla.Rows[i].ItemArray;
                    registro += a.GetValue(0).ToString() + ",";
                    registro += a.GetValue(1).ToString() + "*";
                }

            }
            catch (Exception e)
            {
                registro = "Conexion No Exitosa";
            }
            finally
            {
                conn.Close();
            }

            return registro;

        }
    }
}