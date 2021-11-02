using System;
using System.Collections.Generic;
using System.Text;
using ApoloniaApp.Models;
using ApoloniaApp.ViewModels;
using ApoloniaApp;
using NUnit.Framework;
using Oracle.ManagedDataAccess.Client;
using Azure;

namespace ApoloniaApp.Test
{
    [TestFixture]
    class SubUnidadTipoTest

    {
        [Test]
        public void ReadAll()
        {
            SubUnidadTipo s = new SubUnidadTipo()
            {
                Id = 0,
                Nombre = "",
                Descripcion ""
            };
            List<SubUnidadTipo> response = u.ReadAll();
            Assert.AreEqual(false, response);

        }
    }
}
