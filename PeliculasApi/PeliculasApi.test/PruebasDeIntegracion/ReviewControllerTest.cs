using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using PeliculasApi.DTOs;
using PeliculasApi.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasApi.test.PruebasDeIntegracion
{
    [TestClass]
    public class ReviewControllerTest : BasePruebas
    {
        private static readonly string url = "/api/peliculas/1/reviews";

        [TestMethod]
        public async Task ObtenerReviewsDevuelve404PeliculaInexistente()
        {
            var nombreBD = Guid.NewGuid().ToString();
            var factory = ConstruirWebApplicationFactory(nombreBD);

            var cliente = factory.CreateClient();
            var respuesta = await cliente.GetAsync(url);
            Assert.AreEqual(404, (int)respuesta.StatusCode);
        }

    }
}
