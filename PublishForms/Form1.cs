using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PublishForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        #region CambioId
        private List<string> GetEntities(string file)
        {
            using (StreamReader stream = File.OpenText(file))
            {
                var json = JArray.Parse(stream.ReadToEnd());
                List<string> lista = new List<string>();
                foreach (var item in json)
                {
                    lista.Add(item["id"].ToString());
                }
                lista.Reverse();
                return lista;
            }
        } //Obtiene las entidades que existen 

        private void replaceJson(string file, List<string> source, List<string> target, string directory)
        {
            string json = "";
            string fileName = "";
            try
            {
                using (StreamReader stream = File.OpenText(file))
                {
                    fileName = Path.GetFileName(file);
                    json = stream.ReadToEnd();
                }
                for (int i = 0; i < source.Count; i++)
                {
                    json = json.Replace(source[i], target[i]);
                    listBoxIds.Items.Insert(0,"Reemplazado: " + source[i] + "   con   " + target[i]);
                }
                using (StreamWriter writer = new StreamWriter(directory + "\\" + fileName))
                {
                    writer.Write(json);
                    listBoxIds.Items.Insert(0,"Escrito en: " + directory + "\\" + fileName);
                }
            }
            catch(DirectoryNotFoundException e)
            {
                listBoxIds.Items.Insert(0,e.Message);
            }
            catch (Exception e)
            {
                listBoxIds.Items.Insert(0,e.Message);
            }
        }  //Reemplaza los ids de los formularios

        private void ObtenerCamposEliminar()
        {
            try
            {
                string path = Path.GetDirectoryName(Application.ExecutablePath.ToString());
                string file = Directory.EnumerateFiles(path, "*.fbdc").FirstOrDefault();
                string Clave = formId(txtDataModel.Text);
                var objConfig = new ControlesAEliminar();
                DeleteControls.Rows.Clear();
                if (file != "" && file != null)
                {
                    using (StreamReader reader = new StreamReader(file))
                    {
                        string stringConfig = reader.ReadToEnd();
                        objConfig = JsonConvert.DeserializeObject<ControlesAEliminar>(stringConfig);
                        foreach (var item in objConfig.ListaConceptos)
                        {
                            if (item.Concepto == Clave)
                            {
                                foreach (var control in item.Controles)
                                {

                                    DeleteControls.Rows.Add(control.ToString());
                                }
                            }
                        }
                    }
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        } //Obtiene los campos a eliminar del grid

        private string ClaveImpuesto(string file)
        {
            if (file != "" && file != null)
            {
                using (StreamReader stream = File.OpenText(file))
                {
                    var json = JArray.Parse(stream.ReadToEnd());
                    return json[0]["atributos"]["atributo"][2]["valor"].ToString();
                }
            }
            return "";
        } //Obtiene el numero de concepto que trae la determinacion del impuesto

        private string ClavePago(string file)
        {
            if (file != "" && file != null)
            {
                using (StreamReader stream = File.OpenText(file))
                {
                    var json = JArray.Parse(stream.ReadToEnd());
                    foreach (var item in json)
                    {
                        var n = item["atributos"]["atributo"][0]["valor"].ToString();
                        if (n == "SAT_DETERMINACION_PAGO")
                        {
                            return item["atributos"]["atributo"][2]["valor"].ToString();
                        }
                    }
                }
            }
            return "";
        }  //Obtiene el numero de concepto que trae la determinacion del pago

        private string formId(string file)
        {
            if (file != "" && file != null)
            {
                List<string> Original = GetEntities(file); //Obtiene la lista de ids de las entidades que existen, de DI y DP
                return Original[Original.Count() - 1].Substring(0, 3);
            }
            else return "";
        }

        private string SelectedFile()
        {
            using (OpenFileDialog openFileDialog1 = new OpenFileDialog())
            {
                //openFileDialog1.InitialDirectory = "c:\\";
                openFileDialog1.FilterIndex = 10;
                openFileDialog1.RestoreDirectory = true;
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    return openFileDialog1.FileName;
                }
                return "";
            }
        }  //Abre el dilogo para obtener archivos

        private string SelectedBrowserDialog()
        {
            using (var fldrDlg = new FolderBrowserDialog())
            {
                if (fldrDlg.ShowDialog() == DialogResult.OK)
                {
                    var a = fldrDlg.SelectedPath;
                    return a;
                }
                return "";
            }
        } //Abre el diaogo para obtenre carpetas

        private List<string> SetList(int id, string formId)
        {
            List<string> final = new List<string>();
            for (int i = 0; i < id; i++)
            {
                final.Add("\"" + formId + (1 + i).ToString("D4") + "\"");
            }
            final.Reverse();
            return final;
        }  //Genera la lista de entodades nuevas secuencialmente

        private void GenerateList(object sender, EventArgs e)
        {
            try
            {                
                if (Path.GetExtension(txtDataModel.Text) != "")
                {
                    txtFolder.Text = Path.GetDirectoryName(txtDataModel.Text);
                }
                else
                {
                    txtFolder.Text = txtDataModel.Text;
                }
                string ci = ClaveImpuesto(txtDataModel.Text); //Obtiene el numero de concepto
                string cp = ClavePago(txtDataModel.Text);
                List<string> Original = GetEntities(txtDataModel.Text); //Obtiene la lista de ids de las entidades que existen, de DI y DP
                List<string> aux = new List<string>();
                int listLenght = Original.Count();
                string formId = Original[Original.Count() - 1].Substring(0, 3);
                List<string> final = SetList(Original.Count(), formId); //Genera una lista con los nuevos ids que se van a usar
                //___Reorder: Genera la lista de reemplazos de ids. Deja la primer entidad de la determinacion de pago en la segunda entidad de DI siempre
                for (int i = 0; i < listLenght; i++)
                {
                    if (i == 3) //Si la primer entidad de la determinacion de pago la agrega a la segunda de DI
                    {
                        aux.Add( "\"" + Original[Original.Count() - 2] + "\""); //Necesita las comillas dobles para hacer la búsqueda precisa y no haga reemplazos indebidos en cadenas incompletas
                    }
                    else if (i == (Original.Count() - 2)) // si la segunda entidad de DI .. le agrega la primera de pago
                    {
                        aux.Add("\"" + Original[3] + "\"");
                    }
                    else // Agrega el resto en orden
                    {
                        aux.Add("\"" + Original[i] + "\"");
                    }
                }
                //___EndReoder
                //Genera la lista de textos que siempre se reemplazan
                aux.Add("CATEG0149");
                aux.Add("206maincontainer");
                aux.Add("206section");
                aux.Add("206column");
                aux.Add("206row");
                aux.Add("206textbox");
                aux.Add("206select");
                aux.Add("206detail");
                aux.Add("206date");
                aux.Add("20600");
                final.Add(string.Format("CATEG" + ci));
                string idForm = final[0].Substring(1, 3);
                final.Add(string.Format(idForm + "maincontainer1000"));
                final.Add(string.Format(idForm + "section1000"));
                final.Add(string.Format(idForm + "column1000"));
                final.Add(string.Format(idForm + "row1000"));
                final.Add(string.Format(idForm + "textbox1000"));
                final.Add(string.Format(idForm + "select1000"));
                final.Add(string.Format(idForm + "detail1000"));
                final.Add(string.Format(idForm + "date1000"));
                final.Add(string.Format("10" + idForm + "00"));
                Data.Columns.Clear();
                Data.Columns.Add("1", "Origen");
                Data.Columns.Add("2", "Reemplazo");
                for (int i = 0; i < final.Count(); i++)
                {
                    Data.Rows.Add(aux[i], final[i]);
                }
                if (cp != ci)
                {
                    Data.Rows.Add(cp, ci);
                }
            }
            catch (Exception error)
            {
                Data.Columns.Clear();
                string mensaje = "FORMATO INCORRECTO: SELECCIONDE 'MODELO DE DATOS' DEL CONJUNTO DE PLANTILLAS PUBLICADAS POR FORMSBUILDER \n\n\n Error:  ";
                MessageBox.Show(mensaje + error.Message);
                // Suele fallar porque no encuentra las entidades o atributos correspondientes
            }
        } //Obtiene ids, ordena y pinta las listas de reemplazo

        private void button1_Click(object sender, EventArgs e)
        {
            string listFile = SelectedFile();
            txtDataModel.Text = listFile;
            GenerateList(sender,e);
            ObtenerCamposEliminar();
            SelectFile.Hide();
            listBoxIds.Items.Clear();
        } //Abre el explorador de archivos y ejecuta enerar lista de reemplazo y obtienen los campos a eliminar

        private void SelectFile_Click(object sender, EventArgs e)
        {
            try
            {
                EliminarControl eliminarControl = new EliminarControl();
                string diagramacion = "";
                if(txtDataModel.Text != "")
                {
                    string aux = txtDataModel.Text.Replace("_001_", "_001_0_");
                    string aux2 = txtFolder.Text + "\\" + Path.GetFileName(aux.Replace("_MD_", "_D_")); // Usa el nombre del modelo de datos y lo cambia a diagramación
                    diagramacion = aux2;
                }
                else
                {
                    diagramacion = SelectedFile();
                }
                string salida = txtFolder.Text;
                if (salida == "")
                {
                    throw new Exception("Elija una ruta de salida");
                }
                if (diagramacion == "")
                {
                    throw new Exception("Elija un archivo de Modelo de Datos");
                }
                List<string> controls = new List<string>();
                controls.Add(DeleteControls.Rows[0].Cells[0].Value.ToString());
                for (int i = 1; i < DeleteControls.Rows.Count - 1; i++) //Genera la lista de reemplazos a partir del DataGrid del Forms
                {
                    controls.Add(DeleteControls.Rows[i].Cells[0].Value.ToString());
                }
                listBoxIds.Items.Insert(0,eliminarControl.DeletedControls(sender, e, diagramacion, salida, controls.ToArray()));      
            }
            catch(DirectoryNotFoundException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        } //Boton eliminar campos

        private void button2_Click(object sender, EventArgs e)
        {
            string outputList = SelectedBrowserDialog();
            txtFolder.Text = outputList;
        } //Abre el explorador de archivos para elegir uno y ponerlo en el textbox

        private void button3_Click(object sender, EventArgs e)
        {
            bool ejecutar = true; //la bandera se desactiva en caso de cancelar el reemplazo de ids
            listBoxIds.Items.Clear();
            string sourceFile = txtDataModel.Text;
            string sourceDirectory = sourceFile == "" ? "" : Path.GetDirectoryName(sourceFile); // Si no hay archivo seleccionado, el directorio es cadena vacia, esto mandara un de alerta txtbox después
            string targetDirectory = txtFolder.Text;
            List<string> sourceList = new List<string>();
            List<string> targetList = new List<string>();
            if(targetDirectory == "")
            {
                targetDirectory = sourceDirectory; //Si no hay carpeta destino, sobreescribe.
            }
            if(sourceFile == "")
            {
                MessageBox.Show("Elija una carpeta de origen y una carpeta de destino");
                ejecutar = false;
            }
            else if (sourceDirectory == targetDirectory) //Si las carpteas origen-destino son las mismas, pregunta sobre el reemplazo
            {
                DialogResult dialogResult = MessageBox.Show("ALERTA!\nLos archivos de origen se sobreescibirán.\n¿Desea continuar?", "Sobreescribir", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes) //Reemplaza los archivos, 
                {
                    ejecutar = true;
                }
                else if (dialogResult == DialogResult.No)
                {
                    ejecutar = false;
                    MessageBox.Show("Elija una carpeta destino diferente a la original","CANCELADO");
                }
            }
            if(ejecutar)
            {
                SelectFile.Show();
                listBoxIds.Items.Insert(0,"\tLista de reemplazo:\n");
                for (int i = 0; i < Data.Rows.Count - 1; i++) //Genera la lista de reemplazos a partir del DataGrid del Forms
                {
                    var x = Data.Rows[i].Cells[0].Value.ToString();
                    sourceList.Add(x);
                    var y = Data.Rows[i].Cells[1].Value.ToString();
                    targetList.Add(y);
                    var z = x + "   -   " + y;
                    listBoxIds.Items.Insert(0,z); //Pinta la lista de reemplazos en el ListBox
                }
                foreach (string file in Directory.EnumerateFiles(sourceDirectory, "*.json"))
                {
                    listBoxIds.Items.Insert(0,string.Format("__________________________________________ "));
                    listBoxIds.Items.Insert(0,string.Format("Abriendo archivo: " + file));
                    replaceJson(file, sourceList, targetList, targetDirectory); //Llama la funcion de reemplazo de arhivos
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (txtDataModel.Text != "" && txtDataModel.Text != null)
            {
                string path = Path.GetDirectoryName(Application.ExecutablePath.ToString());
                string file = Directory.EnumerateFiles(path, "*.fbdc").FirstOrDefault();
                string Clave = formId(txtDataModel.Text);
                string salida = "";
                List<string> lista = new List<string>();
                var objConfig = new ControlesAEliminar(); //Instancia una clase del tipo del archivo donde se guarda la configuracion de los campos a eliminar
                bool encontrado = false;
                for (int i = 0; i < DeleteControls.Rows.Count - 1; i++)
                {
                    lista.Add(DeleteControls.Rows[i].Cells[0].Value.ToString());  //Obtienne todos los campos de la lista de controles a eliminar
                }
                if (file != "" && file != null)  //Si existe el archivo, busca en el el concepto que se va aescribir
                {
                    using (StreamReader reader = new StreamReader(file))
                    {
                        string stringConfig = reader.ReadToEnd();
                        objConfig = JsonConvert.DeserializeObject<ControlesAEliminar>(stringConfig);
                        foreach (var item in objConfig.ListaConceptos)
                        {
                            if (item.Concepto == Clave)
                            {
                                item.Controles = lista;  //Si existe el concepto en la lista, asigna los valores
                                encontrado = true;
                            }
                        }
                        if (!encontrado) //Si no exste, lo crea y le asigna los vaores
                        {
                            var objConcepto = new PorConcepto();
                            objConcepto.Concepto = Clave;
                            objConcepto.Controles = lista;
                            objConfig.ListaConceptos.Add(objConcepto);
                        }
                    }
                }
                else //Si no existe el archico, crea uno nuevo y asigna los valores
                {
                    var listObjConcepto = new List<PorConcepto>();
                    var objConcepto = new PorConcepto();
                    objConcepto.Concepto = Clave;
                    objConcepto.Controles = lista;
                    listObjConcepto.Add(objConcepto);
                    objConfig.ListaConceptos = listObjConcepto;
                }
                salida = JsonConvert.SerializeObject(objConfig, Formatting.Indented);

                using (StreamWriter writer = new StreamWriter(path + "\\config.fbdc"))
                {
                    writer.Write(salida);
                }

                MessageBox.Show("Guardado!", "Guardar Configuracion");
            }
            else
            {
                MessageBox.Show("No se puede obtener la clave de impuesto", "ORIGEN VACÍO");
            }
        } //Boton guardar camapos a eliminar

        private void button5_Click(object sender, EventArgs e)
        {
            if (txtDataModel.Text != "" && txtDataModel.Text != null)
            {
                ObtenerCamposEliminar();
            }
            else
            {
                MessageBox.Show("No se puede obtener la clave de impuesto", "ORIGEN VACÍO");
            }
        } //Boton cargar, trae los campos a eliminar de .fbdc

        private void txtDataModel_DragDrop(object sender, DragEventArgs e)
        {
            
            listBoxIds.Items.Insert(0,e.Data);
        }
    }
}
#endregion CambioId
