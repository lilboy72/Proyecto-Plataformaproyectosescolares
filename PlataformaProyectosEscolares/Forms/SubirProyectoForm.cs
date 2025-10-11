using PlataformaProyectosEscolares.Service;
using System;
using System.Windows.Forms;

namespace PlataformaProyectosEscolares.Forms
{
    public partial class SubirProyectoForm : Form
    {
        private ProyectoService _proyectoService;

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

                // Validar datos
                if (_proyectoService.ValidarDatosProyecto(titulo, descripcion, estudianteId, profesorId))
                {
                    // Insertar proyecto
                    bool exito = _proyectoService.InsertarProyecto(titulo, descripcion, estudianteId, profesorId);

                    if (exito)
                    {
                        MessageBox.Show("Proyecto guardado exitosamente en la base de datos!", "Éxito",
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo guardar el proyecto", "Error",
                                      MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
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
