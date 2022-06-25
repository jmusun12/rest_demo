using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using RestDemo.DTOs;
using RestDemo.Providers;

namespace RestDemo
{
    public partial class Empleados : System.Web.UI.Page
    {
        private EmpleadoProvider empleadoProvider;
        public Empleados()
        {
            ConfigProvider();
        }

        private void ConfigProvider()
        {
            string baseUrl = ConfigurationManager.AppSettings["baseUrl"];
            empleadoProvider = new EmpleadoProvider(baseUrl);
        }

        private void LoadData()
        {
            List<EmpleadoDto> lst = empleadoProvider.GetEmpleados();
            lst.ForEach(empleadoDto =>
            {
                empleadoDto.FechaNacimiento = DateTime.Parse(empleadoDto.FechaNacimiento).ToString("dd-MM-yyyy");
            });
            this.gvEmpleados.DataSource = lst;
            this.gvEmpleados.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private bool ValidateForm()
        {
            // TODO: Realizar validacion con JS
            bool valido = true;

            if (string.IsNullOrEmpty(txbNombres.Text.Trim())
                || string.IsNullOrEmpty(txbApellidos.Text.Trim())
                || string.IsNullOrEmpty(txbCorreo.Text.Trim())
                || string.IsNullOrEmpty(txbTelefono.Text.Trim())
                || string.IsNullOrEmpty(txbFechaNa.Text.Trim())
                || string.IsNullOrEmpty(txbSalario.Text.Trim()))
            {
                valido = false;
            }

            return valido;
        }

        /// <summary>
        /// Abre el modal para crear un nuevo empleado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNuevoEmpleado_Click(object sender, EventArgs e)
        {
            ClearForm();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Modal", "openModalNuevoEmpleado();", true);
        }

        /// <summary>
        /// Crea o edita un empleado desde el modal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGuardarEmpleado_Click(object sender, EventArgs e)
        {
            if (!ValidateForm()) return;

            // Obtener los datos del nuevo empleado
            string nombres, apellidos, telefono, correo, fechaNacimiento;
            decimal salario = 0;
            int id = Convert.ToInt32(this.txbId.Value);

            nombres = this.txbNombres.Text.Trim();
            apellidos = this.txbApellidos.Text.Trim();
            telefono = this.txbTelefono.Text.Trim();
            correo = this.txbCorreo.Text.Trim();
            fechaNacimiento = DateTime.Parse(this.txbFechaNa.Text.Trim()).ToString("yyyy/MM/dd");
            salario = Convert.ToDecimal(this.txbSalario.Text.Trim());

            DetalleEmpleadoDto empleado = new DetalleEmpleadoDto(nombres, apellidos, telefono, correo, fechaNacimiento, salario);

            if (id == 0)
            {
                // Crear empleado
                if (empleadoProvider.CrearCliente(empleado))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Modal", "closeModalEmpleado();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalConfirm",
                        "openConfirmModal('Empleado creado exitosamente!.');", true);
                    this.LoadData();

                } else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalError",
                        $"openErrorModal('No se pudo crear el usuario.');", true);
                    return;
                }
            }   
            else
            {
                // Update
                if (empleadoProvider.ActualizarCliente(id, empleado))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Modal", "closeModalEmpleado();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalConfirm",
                        "openConfirmModal('Empleado actualizado exitosamente!.');", true);
                    this.LoadData();
                } else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalError",
                        $"openErrorModal('No se pudo actualizar el usuario con ID { id }!.');", true);
                    return;
                }
            }
        }

        /// <summary>
        /// Limpia los datos del formulario empleado
        /// </summary>
        private void ClearForm()
        {
            txbNombres.Text = "";
            txbApellidos.Text = "";
            txbCorreo.Text = "";
            txbFechaNa.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txbSalario.Text = "";
            txbTelefono.Text = "";
        }


        /// <summary>
        /// Consulta los datos del empleado y los muestra en el modal
        /// </summary>
        /// <param name="empleado_id"></param>
        private void EditEmpleado(int empleado_id)
        {
            ClearForm();
            EmpleadoDto empleado = empleadoProvider.GetEmpleado(empleado_id);

            if (empleado == null)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalError",
                    $"openErrorModal('No existe un empleado con ID {empleado_id}!.');", true);
                return;
            }

            // cargando datos al modal de detalle
            txbId.Value = empleado.Id.ToString();
            txbNombres.Text = empleado.Nombres;
            txbApellidos.Text = empleado.Apellidos;
            txbTelefono.Text = empleado.Telefono;
            txbCorreo.Text = empleado.Correo;
            txbSalario.Text = empleado.Salario.ToString();
            txbFechaNa.Text = DateTime.Parse(empleado.FechaNacimiento).ToString("yyyy-MM-dd");

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Modal", "openModalNuevoEmpleado();", true);
        }

        /// <summary>
        /// Elimina un registro de la base de datos y confirma al usuario
        /// </summary>
        /// <param name="empleado_id"></param>
        private void EliminarEmpleado(int empleado_id)
        {
            EmpleadoDto empleado = empleadoProvider.GetEmpleado(empleado_id);

            if (empleado == null)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalError",
                    $"openErrorModal('No existe un empleado con ID {empleado_id}!.');", true);
                return;
            } else
            {
                if (empleadoProvider.EliminarCliente(empleado_id))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalDelete", "closeDeleteModal();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalConfirm",
                        "openConfirmModal('Empleado eliminado exitosamente!.');", true);
                    this.LoadData();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalError",
                        $"openErrorModal('No se pudo eliminar el usuario con ID { empleado_id }!.');", true);
                    return;
                }
            }
        }
        
        protected void gvEmpleados_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            int empleado_id = Convert.ToInt32(this.gvEmpleados.DataKeys[index].Value.ToString());

            switch (e.CommandName)
            {
                case "Editar":
                    EditEmpleado(empleado_id);
                    break;
                case "Eliminar": // Muestro el modal de connfirmacion para eliminar el empleado
                    this.txbEmpleadoId.Value = empleado_id.ToString();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalDelete",
                        $"openDeleteModal('Esta seguro de eliminar el registro?');", true);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Invoca el metodo para eliminar, accion realizada desde el boton "Si, eliminar" del modal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEliminarEmpleado_Click(object sender, EventArgs e)
        {
            int empleado_id = Convert.ToInt32(this.txbEmpleadoId.Value.ToString());
            EliminarEmpleado(empleado_id);
        }
    }
}