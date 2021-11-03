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
    public class RubroModelTest
    {
        [Test]
        public void ReadAll()
        { 

            RubroModel rubro = new RubroModel()
            {
                Detalle = "Construcción",
                Id = 11113
            };

            List<RubroModel> response = rubro.ReadAll();
            Assert.AreEqual(false, response);
        }
    }
}
