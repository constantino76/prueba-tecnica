using PruebaTest.Models;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Text;
using System.Security.Cryptography.X509Certificates;

namespace PruebaTest.Data
{
    public class BaseDatos
    {
        static string Cadena = @"Data Source=.\sqlexpress;Initial Catalog=BDCrudTest;Integrated Security=True";



        public SqlConnection Conexion()
        {
            SqlConnection cn = null;
            try
            {
                cn = new SqlConnection(Cadena);
                cn.Open();

            }
            catch (SqlException EX)
            {


            }

            return cn;
        }

        ///  listamos  los productos de una categoria correspondiente 

        public List<Producto> ListarProductos(int id)
        {
            //var tablaintermedia= GetTablaItermedia();
            //var categorias = GetCategorias();
            //GetCategorias();
            List<Producto> productos = new List<Producto>();
            var conexion = Conexion();
            using (SqlCommand comando = new SqlCommand("Usp_Sel_Co_Productos", conexion))
            {


                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@IdCategory", id));

                using (SqlDataReader reader = comando.ExecuteReader())
                {


                    while (reader.Read())
                    {
                        Producto p = new Producto();
                        p.IdProducto = Convert.ToInt32(reader[0]);
                        p.Nombre = Convert.ToString(reader[1]);
                        p.Precio = Convert.ToDouble(reader[2]);
                        p.NombreCategoria = Convert.ToString(reader[4]);
                        productos.Add(p);

                    }

                }

                //EstablecerNombreProduct_Categoria(productos, tablaintermedia, categorias);
                return productos;

            }



            conexion.Close();
        }

        public int InsertarCategoria(Categoria categoria)
        {
            int valor = 0;
            var sqlconexion = Conexion();
            using (SqlCommand cmd = new SqlCommand("Usp_Ins_Co_Categoria", sqlconexion))
            {

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NameCategory", categoria.NombreCategoria);
                cmd.Parameters.AddWithValue("@Estado", categoria.Activo);
                valor = cmd.ExecuteNonQuery();
                sqlconexion.Close();
            }
            return 0;


        }
        public Producto Buscar(int id)
        {
            var sqlconexion = Conexion();
            Producto p = new Producto();
            String sentencia = "SELECT * FROM coProducto WHERE nIdProduct = " + id;
            SqlCommand cmd = new SqlCommand(sentencia, sqlconexion);
            cmd.CommandType = CommandType.Text;
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    p.IdProducto = Convert.ToInt32(reader[0]);
                    p.Nombre = Convert.ToString(reader[1]);
                    p.Precio = Convert.ToDouble(reader[2]);


                }
            }
            return p;

        }

        public int ActualizarProducto(Producto producto, int id)
        {
            var sqlconexion = Conexion();

            String sentencia = "UPDATE coProducto SET cNombProdu='" + producto.Nombre + "',nPrecioProd='" + producto.Precio + "',nIdCategori='" + id + "'WHERE nIdProduct = " + producto.IdProducto;
            using (SqlConnection sql = new SqlConnection(Cadena))

            {
                SqlCommand cmd = new SqlCommand(sentencia, sqlconexion);

                int resultado = cmd.ExecuteNonQuery();

            }
            sqlconexion.Close();
            return 0;

        }
        public void EliminarProducto(int id) {


            var sqlconexion = Conexion();
           
                string sentencia = "delete from coProducto where nIdProduct=" + id;

            SqlCommand cmd = new SqlCommand(sentencia, sqlconexion);

            int resultado=cmd.ExecuteNonQuery();
            
            

        }

        public List<Categoria> GetCategorias()
        {

            List<Categoria> categorias = new List<Categoria>();
            var sqlconexion = Conexion();
            String sentencia = "SELECT * FROM coCategoria";
            SqlCommand cmd = new SqlCommand(sentencia, sqlconexion);
            cmd.CommandType = CommandType.Text;
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Categoria c = new Categoria();
                    c.IdCategoria = Convert.ToInt32(reader[0]);
                    c.NombreCategoria = Convert.ToString(reader[1]);
                    c.Activo = Convert.ToBoolean(reader[2]);

                    categorias.Add(c);
                }



            }
            return categorias;
        }
    }
}