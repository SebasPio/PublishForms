using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublishForms
{
    class Objeto
    {

        [Serializable]
        public class Atributo
        {
            public string nombre { get; set; }
            public string valor { get; set; }
        }

        [Serializable]
        public class Atributos
        {
            public Atributo[] atributo { get; set; }
        }

        [Serializable]
        public class Inicial
        {
            public Control control { get; set; }
        }

        [Serializable]
        public class Controles
        {
            public Control[] control { get; set; }
        }

        [Serializable]
        public class Control
        {
            public Control() { }
            public string id { get; set; }
            public string tipoControl { get; set; }
            public string idEntidadPropiedad { get; set; }
            public string idPropiedad { get; set; }
            public string idTipoFlujo { get; set; }
            public string orden { get; set; }
            public Atributos atributos { get; set; }
            public Controles controles { get; set; }
        }
    }

    class ControlesAEliminar
    {
        public List<PorConcepto> ListaConceptos { get; set; }
    }

    class PorConcepto
    {
        public string Concepto { get; set; }
        public List<string> Controles { get; set; }
    }

}