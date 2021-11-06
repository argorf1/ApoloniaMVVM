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
                Run = "703673463",
                Nombres = "Veronica",
                ApellidoP = "Prohens",
                ApellidoM = "Soissa",
                Email = "veroka.ps@gmail.com",
                Password = "12345678",
                PerfilDet = "Dise�ador",
                EstadoDet = "Activo",
                IdPerfil = 1,
                IdEstado = 1
            };

            bool result = u.Create();

            Assert.AreEqual(false, result);
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
                PerfilDet = "Administrador",
                IdPerfil = 1,
                IdEstado = 1
            };

            List<UsuarioInternoModel> response = u.ReadAll();
            CollectionAssert.AreEqual(u, response);
        }

        [Test]
        public void ReadDesignerTest()
        {
            UsuarioInternoModel u = new UsuarioInternoModel()
            {
                Run = "122972569",
                NombreCompleto = "Andres Moya Guerra"
            };
            List<UsuarioInternoModel> response = u.ReadDesigner();
            CollectionAssert.AreEqual(u, response);

        }

        [Test]
        public void Read_LoginTest()
        {
            UsuarioInternoModel u = new UsuarioInternoModel()
            {
                Nombres = "Veronica",
                ApellidoP = "Chelen",
                ApellidoM = "Prohens",
                Email = "vchelen@process.cl",
                IdPerfil = 1,
                Password = "12345678"
            };

            bool response = u.Read_Login();
            Assert.AreEqual(false, response);
        }


        [Test]
        public void ReadByRunTest()
        {
            UsuarioInternoModel u = new UsuarioInternoModel()
            {
                Run = "167635059"
            };
            bool response = u.ReadByRun();
            Assert.AreEqual(false, response);
        }

        public bool ReadDesignerTest()
        {
            UsuarioInternoModel u = new UsuarioInternoModel()
            {
                Nombres = "Andres",
                ApellidoP = "Moya",
                ApellidoM = "Guerra",
                Email = "amoya@process.cl",
                IdPerfil = 2,
                Password = "13243254"
            };
            bool response = u.ReadDesigner();
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
                Email = "veronicap@process.cl",
                Password = "1234",
                IdPerfil = 2,
                IdEstado = 2
            };
            bool result = u.Update();
            Assert.AreEqual(false, result);
        }

    }
}