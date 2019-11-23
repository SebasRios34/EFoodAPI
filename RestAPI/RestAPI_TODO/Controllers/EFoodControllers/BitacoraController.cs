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
    public class BitacoraController : ApiController
    {
        public string Get()
        {
            return new Bitacora().cargarBitacora();
        }

        public string Post([FromBody]Bitacora bitacora)
        {
            return bitacora.agregarBitacora("Insertar") ? "Se añadieron con exito" : "No se logro guardar un nueva bitacora ";
        }
    }
}
