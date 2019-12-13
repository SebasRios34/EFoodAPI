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
    public class ConsecutivoController : ApiController
    {
        public string Get() {
            return new Consecutivo().cargarConsecutivo();
        }

        public string Put(int id, [FromBody]Consecutivo consecutivo)
        {
            return consecutivo.modificarConsecutivo("Modificar") ? "Se añadieron con exito" : "No se logro modificar el consecutivo";
        }
    }
}
