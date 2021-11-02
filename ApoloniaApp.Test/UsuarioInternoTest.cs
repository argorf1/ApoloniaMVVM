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
    public class UsuarioInternoModelTests
    {
        [Test]
        public void CreateTest()

        {
            var UsuarioInternoModel = new UsuarioInternoModel();
            UsuarioInternoModel u = new UsuarioInternoModel()
            {
                Run = "70367343",
                Nombres = "Veronica",
                ApellidoP = "Prohens",
                ApellidoM = "Soissa",
                Email = "veroka.ps@gmail.com",
                Password = "12345678",
                PerfilDet = "Diseñador",
                EstadoDet = "Activo",
                IdPerfil = 1,
                IdEstado = 1
            };

            bool result = u.Create();
            Assert.That(result, Is.True);
        }

        [Test]
        public void ReadAllTest()
        {

            UsuarioInternoModel u = new UsuarioInternoModel()
            {
                Run = "167635059",
                Nombres = "Veronica",
                ApellidoP = "Chelen",
                ApellidoM = "Prohens",
                Email = "vero.c87@gmail.com",
                EstadoDet = "Activo",
                PerfilDet = "Desarrollador",
                IdPerfil = 1,
                IdEstado = 1

            };

            List<UsuarioInternoModel> response = u.ReadAll();
            Assert.AreEqual(false, response);
        }

        [Test]
        public void Read_LoginTest()
        {
            UsuarioInternoModel u = new UsuarioInternoModel()
            {
                Nombres = "",
                ApellidoP = "",
                ApellidoM = "",
                Email = "",
                IdPerfil = 0,
                Password = ""
            };

            bool response = u.Read_Login();
            Assert.AreEqual(false, response);
        }
              
       
        [Test]
        public void Update()
        {
            UsuarioInternoModel u = new UsuarioInternoModel()
            {
                Run = "76366163",
                Nombres = "Veronica",
                ApellidoP = "Prohens",
                ApellidoM = "Soissa",
                Email = "vprohens@process.cl",
                Password = "12345678",
                IdPerfil = 1,
                IdEstado = 1
            };
            bool result = u.Update();
            Assert.That(result, Is.True);
        }

    }
}