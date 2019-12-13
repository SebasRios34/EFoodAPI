using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using BLLProyecto;

namespace RestAPI_TODO.Controllers.ECommerceControllers
{
    public class DatosTarjetaController : ApiController
    {
        public string Get() 
        {
            return new DatosTarjetas().cargarDatosTarjeta();
        }

        public string Post([FromBody]DatosTarjetas datosTarjetas)
        {
            return datosTarjetas.insertarDatosTarjeta("Insertar") ? "Se añadieron con exito" : "No se logro guardar la tarjeta";
        }
    }
}
