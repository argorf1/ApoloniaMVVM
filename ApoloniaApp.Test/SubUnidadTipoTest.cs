using System;
using System.Collections.Generic;
using System.Text;
using ApoloniaApp.Models;
using ApoloniaApp.ViewModels;
using ApoloniaApp;
using NUnit.Framework;
using Oracle.ManagedDataAccess.Client;
using Azure;
using System.Linq;

namespace ApoloniaApp.Test
{
    [TestFixture]
    public class SubUnidadTipoTest

    {
        [Test]
        public void ReadAll()
        {
            List<SubUnidadTipo> datosBD = new SubUnidadTipo().ReadAll();
            
            
            Assert.AreEqual(datosBD.Any(), true);

            
        }
    }
}
