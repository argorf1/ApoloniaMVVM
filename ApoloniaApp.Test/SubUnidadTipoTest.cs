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
                       
            SubUnidadTipo u = new SubUnidadTipo();
            {
                Nombre = "Gerencia General";
                Descripcion = "Área que está relacionada con el proceso de la operación general de la empresa. En ella se definen los objetivos, se toman las decisiones más importantes y desde ahí se dirigen todas las operaciones de la organización. Dado que es la responsable de que todo funcione bien, se relaciona directamente con todas las otras áreas y las controla";
                Id = 1;
            };

            SubUnidadTipo response = u.ReadAll();
            Assert.AreEqual(u, response);

        }
    }
}
