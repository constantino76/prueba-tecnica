using Microsoft.AspNetCore.Mvc;
using PruebaTest.Models;
using System.Diagnostics;
using  PruebaTest.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace PruebaTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
         private BaseDatos bd;
        public HomeController(ILogger<HomeController> logger)
        {bd= new BaseDatos();


            _logger = logger;
        }

       
      //recibimos el id de la categoria para filtrar el resultado  en el View
        public  IActionResult ListarProductos(String id = "") {
           
            if (!String.IsNullOrEmpty(id))
            {
                List<Producto> productos = new List<Producto>();
                int id_x = Convert.ToInt32(id);
                productos = bd.ListarProductos(id_x);

                return View(productos);
            }

            return View();




        }
        public ActionResult AgregarCategoria() {

            return View();
        
        
        }


        [HttpPost]
        public ActionResult AgregarCategoria(Categoria categoria)
        {
           bd. InsertarCategoria(categoria);
            return View();


        }

        public IActionResult ActualizarProducto(int id ) {
            Producto p = new Producto();
            p = bd.Buscar(id);
            var categorias = new List<Categoria>();
            categorias= bd.GetCategorias();

            ViewBag.Category = categorias.Select(p => new SelectListItem() { Value = Convert.ToString(p.IdCategoria), Text = p.NombreCategoria }).ToList<SelectListItem>();
            return View(p);
        
        }
        [HttpPost]
        public IActionResult ActualizarProducto(Producto p, string idcategoria) {
            int idcategoria_x = Convert.ToInt32(idcategoria);
            bd.ActualizarProducto(p, idcategoria_x);
            return RedirectToAction("ListarProductos");
        
        
        }

        public IActionResult EliminarProducto(int id) {
            //  int idcategoria_x = Convert.ToInt32(idcategoria);

            Producto p = new Producto();
            p = bd.Buscar(id);
            ViewBag.Id = id;
            bd.EliminarProducto(id);
            return  RedirectToAction("ListarProductos");
        
        }

       



        [HttpPost]
        public IActionResult EliminarProducto(Producto producto,String id)
        {
          
            return View();

        }

        [HttpPost]

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}