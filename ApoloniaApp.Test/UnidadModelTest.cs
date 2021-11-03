using System;
using System.Collections.Generic;
using System.Text;
using ApoloniaApp.Models;
using ApoloniaApp.ViewModels;
using ApoloniaApp;
using NUnit.Framework;
using Oracle.ManagedDataAccess.Client;

namespace ApoloniaApp.Test
{
    [TestFixture]
    public class UnidadModelTests
    {
        [Test]
        public void CreateTest()
        {

            var UnidadModel = new UnidadModel();
            UnidadModel u = new UnidadModel()
            {
                Rut = "96.575.280-3",
                RazonSocial = "Natura cosméticos ",
                RubroId = 0,
                Calle = "Av. Padre Hurtado Sur",
                Numero = "875",
                Complemento = "",
                ComunaId = 13114,
                PersonaContacto = "Pia Fernandez",
                TelefonoContacto = 22343454,
                EmailContacto = "pfernandez@natura.cl",
                ResponsableRun = "100787989"
            };

            bool result = u.Create();
            Assert.That(result, Is.True);

        }

        [Test]
        public void ReadAll()
        {
            UnidadModel u = new UnidadModel()
            {
                Rut = "760254940",
                RazonSocial = "Rotter & Krauss",
                Rubro = "Industria Manufacturera",
                Calle = "Nueva Tobalaba",
                Numero = "12",
                Complemento = "",
                Region = "Región Metropolitana",
                Provincia = "Santiago",
                Comuna = "Providencia",
                PersonaContacto = "Lorena Alvarez",
                TelefonoContacto = 25716260,
                EmailContacto = "lalvarez@Rotter&Krauss.cl",
                ResponsableRun = "10712019K",
                ResponsableNombre = "Ivan Nikoi",
                Estado = "Activa",
                EstadoId = 1,
                DireccionId = 18
            };

            List<UnidadModel> response = u.ReadAll();
            Assert.AreEqual(false, response);
        }

    }
}
