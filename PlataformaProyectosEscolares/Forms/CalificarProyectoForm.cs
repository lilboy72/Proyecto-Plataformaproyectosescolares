using PlataformaProyectosEscolares.Service;
using System;
using System.Windows.Forms;

namespace PlataformaProyectosEscolares.Forms
{
    public partial class CalificarProyectoForm : Form
    {
        private ProyectoService _proyectoService;
        private int _proyectoId;
        private string _tituloProyecto;

        public CalificarProyectoForm(int proyectoId, string tituloProyecto)
        {
            InitializeComponent();
            _proyectoService = new ProyectoService();
            _proyectoId = proyectoId;
            _tituloProyecto = tituloProyecto;
        }

        private void CalificarProyectoForm_Load(object sender, EventArgs e)
        {
            lblTituloProyecto.Text = _tituloProyecto;
            numCalificacion.Value = 0;
            txtComentarios.Text = "";
        }

        private void btnGuardarCalificacion_Click(object sender, EventArgs e)
        {
            try
            {
                decimal calificacion = numCalificacion.Value;
                string comentarios = txtComentarios.Text.Trim();

                if (calificacion == 0)
                {
                    MessageBox.Show("Por favor, ingrese una calificación", "Validación",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                bool exito = _proyectoService.CalificarProyecto(_proyectoId, calificacion, comentarios);

                if (exito)
                {
                    MessageBox.Show("Calificación guardada exitosamente", "Éxito",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No se pudo guardar la calificación", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar calificación: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void numCalificacion_ValueChanged(object sender, EventArgs e)
        {
            // Mostrar mensaje según la calificación
            decimal calificacion = numCalificacion.Value;
            if (calificacion >= 9) lblMensaje.Text = "¡Excelente!";
            else if (calificacion >= 7) lblMensaje.Text = "Muy bien";
            else if (calificacion >= 5) lblMensaje.Text = "Aprobado";
            else if (calificacion > 0) lblMensaje.Text = "Necesita mejorar";
            else lblMensaje.Text = "";
        }
    }
}
