using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApoloniaApp.Models
{
    public class EntityModelBase : ModelBase
    {
        public string NombreEntidad { get; set; }
        public string Mensaje { get; set; }
    }
}
