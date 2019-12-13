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
    public class LineaComidaController : ApiController
    {
        public string Get() 
        {
            return new LineaComida().cargarLineaComida();
        }

        public string Post([FromBody]LineaComida lineaComida)
        {
            return lineaComida.insertarLineaComida("Insertar") ? "Se añadieron con exito" : "No se logro guardar la linea de comida";
        }

        public string Put(int id, [FromBody]LineaComida lineaComida)
        {
            return lineaComida.insertarLineaComida("Modificar") ? "Se añadieron con exito" : "No se logro modificar la linea de comida";
        }

        public string Delete(int id)
        {
            return new LineaComida().eliminarLineaComida(id) ? "Se elimino con exito" : "No se eliminio el dato";
        }
    }
}
