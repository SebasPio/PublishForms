using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublishForms
{
    class EliminarControl
    {

        public string DeletedControls(object sender, EventArgs e, string txtRura, string txtRutaSalida, string[] campos)
        {
            string salida = "";
            try
            {          
                using (StreamReader r = new StreamReader(txtRura))
                {
                    var json = r.ReadToEnd();

                    //var resultado = new Inicial[] { };
                    foreach (var c in campos)
                    {
                        var objJson = JsonConvert.DeserializeObject<Inicial[]>(json);
                        var resultado = RecorrerJson(objJson, c);
                        salida = JsonConvert.SerializeObject(resultado, Formatting.Indented);
                        json = salida;
                    }
                }

                using (StreamWriter w = new StreamWriter(txtRutaSalida))
                {
                    w.Write(salida);
                }
                return "Eliminados";
            }
            catch (Exception ex)
            {
                return ex.Message;
                //this.rtxtSalida.AppendText("ERROR: " + ex.Message);
            }
        }

        private Inicial[] RecorrerJson(Inicial[] obj, string campo)
        {
            bool bandera = false;
            for (int i = 0; i < obj.Length; i++)
            {
                if (ExisteClave(obj[i].control.controles, campo))
                {
                    bandera = true;
                    break;
                }
            }
            if (!bandera)
            {
                Console.Write("No se encontro: " + campo);
                throw new Exception("Error en la eliminación de controles");
            }
            return obj;
        }

        private bool ExisteClave(Controles controles, string campo)
        {
            bool bandera = false;

            if (controles.control != null && controles.control.Length <= 0)
            {
                return bandera;
            }

            for (int i = 0; i < controles.control.Length; i++)
            {
                if (controles.control[i].idPropiedad == campo)
                {
                    controles.control = LimpiarCampo(controles, campo);
                    return true;
                }

                bandera = ExisteClave(controles.control[i].controles, campo);
            }
            return bandera;
        }

        private Control[] LimpiarCampo(Controles controles, string campo)
        {
            var listControl = new List<Control>();
            for (int i = 0; i < controles.control.Length; i++)
            {
                if (controles.control[i].idPropiedad != campo)
                {
                    listControl.Add(controles.control[i]);
                }
            }
            return listControl.ToArray();
        }
    }

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
