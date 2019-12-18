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
    public class TiqueteDescuentoController : ApiController
    {
        public string Get()
        {
            return new TiquetesDescuento().cargarTiquetesDescuento();
        }

        public string Post([FromBody]TiquetesDescuento tiquetesDescuento)
        {
            return tiquetesDescuento.insertarTiquetesDescuento("Insertar") ? "Se añadieron con exito" : "No se logro guardar el tiquete";
        }

        public string Put(int id, [FromBody]TiquetesDescuento tiquetesDescuento)
        {
            return tiquetesDescuento.insertarTiquetesDescuento("Modificar") ? "Se añadieron con exito" : "No se logro modificar el tiquete";
        }

        public string Delete(int id)
        {
            return new TiquetesDescuento().eliminarTiposPrecio(id) ? "Se elimino con exito" : "No se eliminio el dato";
        }
    }
}