using PlataformaProyectosEscolares.Service;
using System;
using System.Data;
using System.Windows.Forms;

namespace PlataformaProyectosEscolares.Forms
{
    public partial class ProfesorForm : Form
    {
        private ProyectoService _proyectoService;

        public ProfesorForm()
        {
            InitializeComponent();
            _proyectoService = new ProyectoService();
        }

        private void ProfesorForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (_proyectoService.ProbarConexion())
                {
                    CargarDatos();
                    ConfigurarDataGridView();
                }
                else
                {
                    MessageBox.Show("No se pudo conectar a la base de datos", "Error de conexión",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el formulario: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarDatos()
        {
            try
            {
                _proyectoService.CargarProyectosEnDataGridViewProfesor(dgvProyectos);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los datos: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarDataGridView()
        {
            try
            {
                dgvProyectos.AutoGenerateColumns = true;
                dgvProyectos.ReadOnly = true;
                dgvProyectos.AllowUserToAddRows = false;
                dgvProyectos.AllowUserToDeleteRows = false;
                dgvProyectos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvProyectos.MultiSelect = false;
                dgvProyectos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al configurar la tabla: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCalificar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvProyectos.SelectedRows.Count > 0)
                {
                    int proyectoId = Convert.ToInt32(dgvProyectos.SelectedRows[0].Cells["Id"].Value);
                    string tituloProyecto = dgvProyectos.SelectedRows[0].Cells["Titulo"].Value.ToString();

                    // Abrir formulario de calificación
                    CalificarProyectoForm formCalificar = new CalificarProyectoForm(proyectoId, tituloProyecto);
                    if (formCalificar.ShowDialog() == DialogResult.OK)
                    {
                        CargarDatos(); // Recargar datos después de calificar
                    }
                }
                else
                {
                    MessageBox.Show("Por favor, seleccione un proyecto para calificar", "Selección requerida",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al calificar proyecto: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarDatos();
            MessageBox.Show("Datos actualizados correctamente", "Actualización",
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "¿Está seguro de que desea cerrar sesión?",
                "Cerrar Sesión",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void dgvProyectos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvProyectos.Rows.Count)
            {
                try
                {
                    DataGridViewRow row = dgvProyectos.Rows[e.RowIndex];

                    string titulo = row.Cells["Titulo"].Value?.ToString() ?? "Sin título";
                    string descripcion = row.Cells["Descripcion"].Value?.ToString() ?? "Sin descripción";
                    string estudiante = row.Cells["EstudianteNombre"].Value?.ToString() ?? "Desconocido";
                    string profesor = row.Cells["ProfesorNombre"].Value?.ToString() ?? "Desconocido";
                    string fecha = row.Cells["Fecha"].Value?.ToString() ?? "Sin fecha";
                    string calificacion = row.Cells["Calificacion"].Value?.ToString() ?? "Sin calificar";
                    string comentario = row.Cells["ComentarioProfesor"].Value?.ToString() ?? "Sin comentarios";

                    string mensaje = $"Título: {titulo}\n\n" +
                                   $"Descripción: {descripcion}\n\n" +
                                   $"Estudiante: {estudiante}\n" +
                                   $"Profesor: {profesor}\n" +
                                   $"Fecha: {fecha}\n" +
                                   $"Calificación: {calificacion}\n" +
                                   $"Comentarios: {comentario}";

                    MessageBox.Show(mensaje, "Detalles del Proyecto",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al mostrar detalles: {ex.Message}", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
