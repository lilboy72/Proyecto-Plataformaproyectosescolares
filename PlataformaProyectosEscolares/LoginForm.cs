using PlataformaProyectosEscolares.Forms;
using System;
using System.Windows.Forms;

namespace PlataformaProyectosEscolares.Forms
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string usuario = txtUsuario.Text.Trim();
                string password = txtPassword.Text;

                // Validación simple
                if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Por favor, ingrese usuario y contraseña", "Validación",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Aquí puedes agregar lógica de autenticación real
                // Por ahora, hacemos un login simple para demostración
                if (AutenticarUsuario(usuario, password))
                {
                    MessageBox.Show($"¡Bienvenido {usuario}!", "Login Exitoso",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Lógica simple de roles (puedes mejorarla después)
                    if (usuario.ToLower().Contains("profesor"))
                    {
                        // Abrir formulario de profesor
                        ProfesorForm profesorForm = new ProfesorForm();
                        profesorForm.Show();
                    }
                    else if (usuario.ToLower().Contains("padre"))
                    {
                        // Abrir formulario de padre (lo crearemos después)
                        MessageBox.Show("Formulario de Padre - Próximamente", "Información",
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);
                        EstudianteForm estudianteForm = new EstudianteForm();
                        estudianteForm.Show();
                    }
                    else
                    {
                        // Abrir formulario de estudiante
                        EstudianteForm estudianteForm = new EstudianteForm();
                        estudianteForm.Show();
                    }

                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña incorrectos", "Error de Login",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.Clear();
                    txtUsuario.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error durante el login: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool AutenticarUsuario(string usuario, string password)
        {
            // Lógica de autenticación simple - puedes reemplazar con autenticación real
            // Por ahora, acepta cualquier usuario que no esté vacío
            return !string.IsNullOrEmpty(usuario) && !string.IsNullOrEmpty(password);

            // Para una versión real, aquí conectarías con la base de datos:
            // return _proyectoService.ValidarLogin(usuario, password);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "¿Está seguro de que desea salir de la aplicación?",
                "Confirmar Salida",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void txtUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir Enter para navegar entre campos
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtPassword.Focus();
                e.Handled = true;
            }
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir Enter para hacer login
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnLogin.PerformClick();
                e.Handled = true;
            }
        }

        private void linkLabelRegistro_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Funcionalidad de registro se implementará próximamente", "Registro",
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Confirmar salida de la aplicación
            if (e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult result = MessageBox.Show(
                    "¿Está seguro de que desea salir de la aplicación?",
                    "Confirmar Salida",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}

