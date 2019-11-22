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
    public class PreguntaSeguridadController : ApiController
    {
        public string Get()
        {
            return new PreguntaSeguridad().cargarPreguntaSeguridad();
        }

        public string Post([FromBody]PreguntaSeguridad preguntaSeguridad)
        {
            return preguntaSeguridad.agregarPreguntaSeguridad("Insertar") ? "Se añadió con exito" : "No se logro guardar una nueva pregunta de seguridad";
        }
    }
}