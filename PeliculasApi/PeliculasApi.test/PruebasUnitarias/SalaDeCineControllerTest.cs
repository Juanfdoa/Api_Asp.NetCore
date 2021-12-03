using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using PeliculasApi.Controllers;
using PeliculasApi.DTOs;
using PeliculasApi.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeliculasApi.test.PruebasUnitarias
{
    [TestClass]
    public class SalaDeCineControllerTest : BasePruebas
    {
        [TestMethod]
        public async Task ObtenerSalasDeCineA5kilometrosOMenos()
        {
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

            using (var context = LocalDbDatabaseInitializer.GetDbContextLocalDb(false))
            {
                var salasDeCine = new List<SalaDeCine>()
                {
                    new SalaDeCine{ Nombre = "Cine Colombia", Ubicacion = geometryFactory.CreatePoint(new Coordinate(-75.592046, 6.177722)) }
                };

                context.AddRange(salasDeCine);
                await context.SaveChangesAsync();
            }

            var filtro = new SalaDeCineCercanoFiltroDTO()
            {
                DistanciaEnKms = 5,
                Latitud = 6.189929,
                Longitud = -75.594306
            };

            using (var context = LocalDbDatabaseInitializer.GetDbContextLocalDb(false))
            {
                var mapper = ConfigurarAutoMapper();
                var controller = new SalaDeCineController(context, mapper, geometryFactory);
                var respuesta = await controller.Cercanos(filtro);
                var valor = respuesta.Value;
                Assert.AreEqual(1, valor.Count);
            }

        }
    }
}
