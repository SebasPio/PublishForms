using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace PublishForms
{
    class EliminarControl
    {
        public string DeletedControls(object sender, EventArgs e, string txtRuta, string txtRutaSalida, string[] campos)
        {
            string salida = "";
            try
            {
                using (StreamReader r = new StreamReader(txtRuta))
                {
                    var json = r.ReadToEnd();

                    foreach (var c in campos)
                    {
                        Objeto.Inicial[] objJson = JsonConvert.DeserializeObject<Objeto.Inicial[]>(json);
                        var resultado = RecorrerJson(objJson, c);
                        salida = JsonConvert.SerializeObject(resultado, Formatting.Indented);
                        json = salida;
                    }
                }

                using (StreamWriter w = new StreamWriter(txtRuta))
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

        private Objeto.Inicial[] RecorrerJson(Objeto.Inicial[] obj, string campo)
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
                throw new Exception("No se encontro: " + campo);
            }
            return obj;
        }

        private bool ExisteClave(Objeto.Controles controles, string campo)
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

        private Objeto.Control[] LimpiarCampo(Objeto.Controles controles, string campo)
        {
            var listControl = new List<Objeto.Control>();
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
}