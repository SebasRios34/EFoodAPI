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
    public class ProcesadorPagoController : ApiController
    {
        public string Get()
        {
            return new ProcesadorPago().cargarProcesadorPago();
        }

        public string Post([FromBody]ProcesadorPago procesadorPago)
        {
            return procesadorPago.insertarProcesadorPago("Insertar") ? "Se añadieron con exito" : "No se logro guardar el procesador pago";
        }

        public string Put(int id, [FromBody]ProcesadorPago procesadorPago)
        {
            return procesadorPago.insertarProcesadorPago("Modificar") ? "Se añadieron con exito" : "No se logro modificar el procesador pago";
        }

        public string Delete(int id)
        {
            return new ProcesadorPago().eliminarProcesadorPago(id) ? "Se elimino con exito" : "No se eliminio el dato";
        }
    }
}
