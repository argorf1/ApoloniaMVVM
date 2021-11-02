using System;
using System.Collections.Generic;
using System.Text;

namespace ApoloniaApp.Models
{
    public class SubUnidadModel : EntityModelBase
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int SubUnidadPadre { get; set; }
        public string RutUnidad { get; set; }
    }
}
