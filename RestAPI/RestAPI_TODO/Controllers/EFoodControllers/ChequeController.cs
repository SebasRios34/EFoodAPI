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
    public class ChequeController : ApiController
    {
        public string Get()
        {
            return new Cheque().cargarCheque();
        }

        public string Post([FromBody]Cheque cheque)
        {
            return cheque.insertarCheque("Insertar") ? "Se añadieron con exito" : "No se logro guardar el cheque";
        }
    }
}
