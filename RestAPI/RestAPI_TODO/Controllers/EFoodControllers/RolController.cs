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
    public class RolController : ApiController
    {
        public string Get()
        {
            return new Rol().cargarRol();
        }

        public string Post([FromBody]Rol rol)
        {
            return rol.agregarRol("Insertar") ? "Se añadió con exito" : "No se logro guardar un nuevo rol";
        }
    }
}