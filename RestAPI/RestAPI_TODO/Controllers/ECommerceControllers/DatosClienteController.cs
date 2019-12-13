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
    public class DatosClienteController : ApiController
    {
        public string Get() 
        {
            return new DatosCliente().cargarDatosCliente();
        }

        public string Post([FromBody]DatosCliente datosCliente)
        {
            return datosCliente.insertarDatosCliente("Insertar") ? "Se añadieron con exito" : "No se logro guardar un nuevo usuario";
        }
    }
}
