using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLLProyecto;


namespace RestAPI_TODO.Controllers.EFoodControllers
{
    public class PreguntaSeguridadController : Controller
    {
        public string Get()
        {
            return new PreguntaSeguridad().cargarPreguntaSeguridad();
        }
    }
}