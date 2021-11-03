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
    public class PerfilModelTest
    {
        [Test]
        public void ReadAll()
        {
            PerfilModel u = new PerfilModel()
            {
                Detalle = "Diseñador",
                Id = 2
            };

            List<PerfilModel> response = u.ReadAll();
            Assert.AreEqual(false, response);
        }
            
    }
}
