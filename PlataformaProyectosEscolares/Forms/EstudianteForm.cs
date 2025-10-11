using PlataformaProyectosEscolares.Service;
using System;
using System.Data;
using System.Windows.Forms;

namespace PlataformaProyectosEscolares.Forms
{
    public partial class EstudianteForm : Form
    {
        private ProyectoService _proyectoService;

        public EstudianteForm()
        {
            InitializeComponent();
            _proyectoService = new ProyectoService();
        }

        private void EstudianteForm_Load(object sender, EventArgs e)
        {
            try
            {
                // Probar conexión primero
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
                // Cargar proyectos en el DataGridView
                DataTable proyectos = _proyectoService.ObtenerTodosLosProyectos();
                dgvProyectos.DataSource = proyectos;
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
                // Configurar el DataGridView
                dgvProyectos.AutoGenerateColumns = true;
                dgvProyectos.ReadOnly = true;
                dgvProyectos.AllowUserToAddRows = false;
                dgvProyectos.AllowUserToDeleteRows = false;
                dgvProyectos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvProyectos.MultiSelect = false;
                dgvProyectos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                // Ocultar columnas de ID si existen
                if (dgvProyectos.Columns.Contains("Id"))
                    dgvProyectos.Columns["Id"].Visible = false;

                if (dgvProyectos.Columns.Contains("EstudianteId"))
                    dgvProyectos.Columns["EstudianteId"].Visible = false;

                if (dgvProyectos.Columns.Contains("ProfesorId"))
                    dgvProyectos.Columns["ProfesorId"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al configurar la tabla: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSubirProyecto_Click(object sender, EventArgs e)
        {
            try
            {
                SubirProyectoForm formSubirProyecto = new SubirProyectoForm();
                formSubirProyecto.ShowDialog();

                // Recargar datos después de cerrar el formulario
                CargarDatos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir formulario: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

                    string mensaje = $"Título: {titulo}\n\n" +
                                   $"Descripción: {descripcion}\n\n" +
                                   $"Estudiante: {estudiante}\n" +
                                   $"Profesor: {profesor}\n" +
                                   $"Fecha: {fecha}";

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

        // Método para actualizar datos
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarDatos();
            MessageBox.Show("Datos actualizados correctamente", "Actualización",
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Método para buscar proyectos
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string criterio = txtBuscar?.Text?.Trim() ?? "";

                if (!string.IsNullOrEmpty(criterio))
                {
                    DataTable proyectos = _proyectoService.ObtenerTodosLosProyectos();
                    DataTable proyectosFiltrados = proyectos.Clone();

                    foreach (DataRow row in proyectos.Rows)
                    {
                        if (row["Titulo"].ToString().ToLower().Contains(criterio.ToLower()) ||
                            row["Descripcion"].ToString().ToLower().Contains(criterio.ToLower()) ||
                            (row.Table.Columns.Contains("EstudianteNombre") &&
                             row["EstudianteNombre"].ToString().ToLower().Contains(criterio.ToLower())) ||
                            (row.Table.Columns.Contains("ProfesorNombre") &&
                             row["ProfesorNombre"].ToString().ToLower().Contains(criterio.ToLower())))
                        {
                            proyectosFiltrados.ImportRow(row);
                        }
                    }

                    dgvProyectos.DataSource = proyectosFiltrados;
                }
                else
                {
                    CargarDatos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
