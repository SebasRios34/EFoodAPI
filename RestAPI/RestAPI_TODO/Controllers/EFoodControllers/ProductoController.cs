using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using BLLProyecto;


namespace RestAPI_TODO.Controllers.EFoodControllers
{
    public class ProductoController : ApiController
    {
        public string Get()
        {
            return new Producto().cargarProducto();
        }

        public string Post([FromBody]Producto producto)
        {
            return producto.insertarProducto("Insertar") ? "Se añadieron con exito" : "No se logro guardar el producto";
        }

        public string Put(int id, [FromBody]Producto producto)
        {
            return producto.insertarProducto("Modificar") ? "Se añadieron con exito" : "No se logro modificar el producto";
        }

        public string Delete(int id)
        {
            return new Producto().eliminarProducto(id) ? "Se elimino con exito" : "No se eliminio el dato";
        }
    }
}
