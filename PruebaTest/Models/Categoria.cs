namespace PruebaTest.Models
{
    public class Categoria
    {
        public int IdCategoria { get; set; }
        public String NombreCategoria { get; set; }
        public bool Activo { get; set; }
        public List<Producto>? productos = new List<Producto>();
    }
}
