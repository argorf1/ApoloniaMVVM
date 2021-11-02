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
                Rut = "",
                RazonSocial = " ",
                RubroId = "",
                Calle = "",
                Numero = "",
                Complemento = "",
                ComunaId = "",
                PersonaContacto = "",
                TelefonoContacto = "",
                EmailContacto = "",
                ResponsableRun = ""
            };

            bool result = u.Create();
            Assert.That(result, Is.True);

        }

        [Test]
        public void ReadAll()
        {
            UnidadModel u = new UnidadModel()
            {
                Rut = "",
                RazonSocial = "",
                Rubro = "",
                Calle = "",
                Numero = "",
                Complemento = "",
                Region = "",
                Provincia = " ",
                Comuna = "",
                PersonaContacto = "",
                TelefonoContacto = "",
                EmailContacto = "",
                ResponsableRun = "",
                ResponsableNombre = "",
                Estado = "",
                EstadoId = 0,
                DireccionId = 0
            };

            List<UnidadModel> response = u.ReadAll();
            Assert.AreEqual(false, response);
        }







    }
}
