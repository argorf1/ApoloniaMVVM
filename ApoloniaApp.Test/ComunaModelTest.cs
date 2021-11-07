using System;
using System.Collections.Generic;
using System.Text;
using ApoloniaApp.Models;
using ApoloniaApp.ViewModels;
using ApoloniaApp;
using NUnit.Framework;
using Oracle.ManagedDataAccess.Client;
using System.Linq;

namespace ApoloniaApp.Test
{
    [TestFixture]
    class ComunaModelTest

    {
        [Test]
        public void ReadAll()
        {
            List<ComunaModel> datosBD = new ComunaModel().ReadAll();
            
            Assert.AreEqual(datosBD.Any(), true);
        }
    }
}
