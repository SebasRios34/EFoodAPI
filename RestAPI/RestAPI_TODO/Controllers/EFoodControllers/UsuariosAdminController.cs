using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLLProyecto;

namespace RestAPI_TODO.Controllers.EFoodControllers
{
    public class UsuariosAdminController : Controller
    {
        public string Get()
        {
            return new UsuariosAdmin().cargarUsuariosAdmin();
        }
    }
}