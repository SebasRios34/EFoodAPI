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
    public class TipoPrecioController : ApiController
    {
        public string Get()
        {
            return new TiposPrecio().cargarTiposPrecio();
        }

        public string Post([FromBody]TiposPrecio tiposPrecio)
        {
            return tiposPrecio.insertarTiposPrecio("Insertar") ? "Se añadieron con exito" : "No se logro guardar el tipo precio";
        }

        public string Put(int id, [FromBody]TiposPrecio tiposPrecio)
        {
            return tiposPrecio.insertarTiposPrecio("Modificar") ? "Se añadieron con exito" : "No se logro modificar el tipo precio";
        }

        public string Delete(int id)
        {
            return new TiposPrecio().eliminarTiposPrecio(id) ? "Se elimino con exito" : "No se eliminio el dato";
        }
    }
}
