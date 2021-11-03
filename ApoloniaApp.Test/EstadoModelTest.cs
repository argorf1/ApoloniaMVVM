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
    public class EstadoModelTest
    {
        [Test]
        public void ReadAll()
        {
            EstadoModel e = new EstadoModel()
            {
                Detalle = "Activo",
                Id = 1
            };

            List<EstadoModel> response = e.ReadAll();
            Assert.AreEqual(false, response);
        }
    }
}
