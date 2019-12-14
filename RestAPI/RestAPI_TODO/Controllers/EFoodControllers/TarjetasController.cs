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
    public class TarjetasController : ApiController
    {
        public string Get()
        {
            return new Tarjetas().cargarTarjetas();
        }

        public string Post([FromBody]Tarjetas tarjetas)
        {
            return tarjetas.insertarTarjetas("Insertar") ? "Se añadieron con exito" : "No se logro guardar la tarjeta";
        }

        public string Put(int id, [FromBody]Tarjetas tarjetas)
        {
            return tarjetas.insertarTarjetas("Modificar") ? "Se añadieron con exito" : "No se logro modificar la tarjeta";
        }

        public string Delete(int id)
        {
            return new Tarjetas().eliminarTarjetas(id) ? "Se elimino con exito" : "No se eliminio el dato";
        }
    }
}
   