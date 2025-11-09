// Forms/SubirProyectoForm.cs
using PlataformaProyectosEscolares.Service;
using System;
using System.IO;
using System.Windows.Forms;

namespace PlataformaProyectosEscolares.Forms
{
    public partial class SubirProyectoForm : Form
    {
        private ProyectoService _proyectoService;

        // Mantiene la ruta elegida por el usuario
        private string _rutaArchivoSeleccionado;

        public SubirProyectoForm()
        {
            InitializeComponent();
            _proyectoService = new ProyectoService();
        }

        private void SubirProyectoForm_Load(object sender, EventArgs e)
        {
            try
            {
                // Probar conexión primero
                if (_proyectoService.ProbarConexion())
                {
                    // Cargar comboboxes
                    _proyectoService.CargarEstudiantesEnComboBox(cmbEstudiante);
                    _proyectoService.CargarProfesoresEnComboBox(cmbProfesor);
                    // Establecer fecha actual
                    dtpFecha.Value = DateTime.Now;
                }
                else
                {
                    MessageBox.Show("No se pudo conectar a la base de datos", "Error de conexión",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el formulario: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSeleccionarArchivo_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Title = "Seleccionar archivo del proyecto";
                ofd.Filter = "Documentos|*.pdf;*.doc;*.docx;*.ppt;*.pptx;*.zip|Todos los archivos|*.*";
                ofd.Multiselect = false;

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    var fileInfo = new FileInfo(ofd.FileName);

                    // Validaciones básicas
                    const long maxBytes = 50L * 1024 * 1024; // 50 MB
                    if (fileInfo.Length > maxBytes)
                    {
                        MessageBox.Show("El archivo supera los 50 MB permitidos.", "Validación",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string[] permitidas = { ".pdf", ".doc", ".docx", ".ppt", ".pptx", ".zip" };
                    if (Array.IndexOf(permitidas, fileInfo.Extension.ToLowerInvariant()) < 0)
                    {
                        MessageBox.Show("Tipo de archivo no permitido.", "Validación",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    _rutaArchivoSeleccionado = ofd.FileName;
                    txtArchivo.Text = _rutaArchivoSeleccionado;
                }
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener datos del formulario
                string titulo = txtTitulo.Text.Trim();
                string descripcion = txtDescripcion.Text.Trim();

                // Validar que se hayan seleccionado valores
                if (cmbEstudiante.SelectedValue == null || cmbProfesor.SelectedValue == null)
                {
                    MessageBox.Show("Debe seleccionar un estudiante y un profesor", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int estudianteId = (int)cmbEstudiante.SelectedValue;
                int profesorId = (int)cmbProfesor.SelectedValue;

                // Validación de datos existente
                if (!_proyectoService.ValidarDatosProyecto(titulo, descripcion, estudianteId, profesorId))
                    return;

                // Validar archivo
                if (string.IsNullOrWhiteSpace(_rutaArchivoSeleccionado) || !File.Exists(_rutaArchivoSeleccionado))
                {
                    MessageBox.Show("Debe seleccionar un archivo del proyecto.", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Copia local del archivo (para no depender de rutas externas)
                string carpetaUploads = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Uploads");
                if (!Directory.Exists(carpetaUploads))
                    Directory.CreateDirectory(carpetaUploads);

                string nombreSeguro = $"{DateTime.Now:yyyyMMdd_HHmmss}_{Path.GetFileName(_rutaArchivoSeleccionado)}";
                string destino = Path.Combine(carpetaUploads, nombreSeguro);

                // Copiar (no sobrescribir)
                File.Copy(_rutaArchivoSeleccionado, destino, overwrite: false);

                // Insertar proyecto con archivo
                bool exito = _proyectoService.InsertarProyecto(titulo, descripcion, estudianteId, profesorId, destino);

                if (exito)
                {
                    MessageBox.Show("Proyecto guardado exitosamente en la base de datos.", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    // Si falla, puedes revertir la copia
                    if (File.Exists(destino))
                        File.Delete(destino);

                    MessageBox.Show("No se pudo guardar el proyecto.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar el proyecto: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtTitulo_TextChanged(object sender, EventArgs e)
        {
            // Validación en tiempo real
            if (txtTitulo.Text.Length > 200)
            {
                MessageBox.Show("El título no puede tener más de 200 caracteres", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTitulo.Text = txtTitulo.Text.Substring(0, 200);
                txtTitulo.SelectionStart = txtTitulo.Text.Length;
            }
        }

        private void txtDescripcion_TextChanged(object sender, EventArgs e)
        {
            // Contador de caracteres para descripción
            lblContadorDescripcion.Text = $"{txtDescripcion.Text.Length}/1000 caracteres";
        }
    }
}
