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
    public class UsuariosAdminController : ApiController
    {
        public string Get()
        {
            return new UsuariosAdmin().cargarUsuariosAdmin();
        }

        public string Get(int id) 
        {
            return new UsuariosAdmin().cargarUsuariosAdminId();
        }

        public string Post([FromBody]UsuariosAdmin usuariosAdmin)
        {
            return usuariosAdmin.agregarUsuariosAdmin("Insertar") ? "Se añadieron con exito" : "No se logro guardar un nuevo usuario";
        }


    }
}