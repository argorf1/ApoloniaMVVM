using System;
using System.Collections.Generic;
using System.Text;

namespace ApoloniaApp.Models
{
    public class DireccionModel : EntityModelBase
    {
        public DireccionModel()
        {
            Calle = "";
            Numero = "";
            Complemento = "";
            Region = new RegionModel();
            Provincia = new ProvinciaModel();
            Comuna = new ComunaModel();
        }

        public string Calle { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public RegionModel Region { get; set; }
        public ProvinciaModel Provincia { get; set; }
        public ComunaModel Comuna { get; set; }


    }
}
