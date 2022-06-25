using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using RestDemo.Models;

namespace RestDemo.Repositories
{
    public class EmpleadoRepository
    {
        private Connection _cnn;

        public EmpleadoRepository()
        {
            _cnn = new Connection();
        }

        private EmpleadoInfo crearEmpleado(SqlDataReader reader)
        {
            EmpleadoInfo empleado = new EmpleadoInfo();
            empleado.Id = Convert.ToInt32(reader["empleado_id"]);
            empleado.Nombres = reader["nombres"].ToString();
            empleado.Apellidos = reader["apellidos"].ToString();
            empleado.Telefono = reader["telefono"].ToString();
            empleado.Correo = reader["correo"].ToString();
            empleado.FechaNacimiento = reader["fecha_nacimiento"].ToString();
            empleado.Salario = Convert.ToDecimal(reader["sueldo"]);

            return empleado;
        }

        /// <summary>
        /// Consulta y retorna el listado de empleados
        /// </summary>
        /// <returns></returns>
        public List<EmpleadoInfo> GetEmpleadoInfos()
        {
            List<EmpleadoInfo> lstEmpleados = new List<EmpleadoInfo>();

            using (SqlConnection cnn = _cnn.GetSqlConnection())
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Empleado", cnn);
                cmd.CommandType = CommandType.Text;

                try
                {
                    cnn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    EmpleadoInfo empleado = null;

                    while (reader.Read())
                    {
                        empleado = crearEmpleado(reader);
                        lstEmpleados.Add(empleado);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Ha ocurrido un erro al intentar consultar el servido DB", ex);
                }
                finally
                {
                    _cnn.CloseSqlConnection();
                }
            }

            return lstEmpleados;
        }

        /// <summary>
        /// Consulta y retorna el registro de empleado
        /// </summary>
        /// <param name="id">Identificador del empleado</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public EmpleadoInfo GetEmpleado(int id)
        {
            EmpleadoInfo empleado = null;

            using (SqlConnection cnn = _cnn.GetSqlConnection())
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Empleado WHERE empleado_id=@id", cnn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.CommandType = CommandType.Text;

                try
                {
                    cnn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        empleado = crearEmpleado(reader);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Ha ocurrido un erro al intentar consultar el servidor DB", ex);
                }
                finally
                {
                    _cnn.CloseSqlConnection();
                }
            }

            return empleado;
        }

        public bool DeleteEmpleado(int id)
        {
            bool eliminado = false;

            using (SqlConnection cnn = _cnn.GetSqlConnection())
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Empleado WHERE empleado_id=@id", cnn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.CommandType = CommandType.Text;

                try
                {
                    cnn.Open();
                    int rs = cmd.ExecuteNonQuery();
                    eliminado = rs > -1;
                }
                catch (Exception ex)
                {
                    throw new Exception("Ha ocurrido un erro al intentar conectar con el servidor DB", ex);
                }
                finally
                {
                    _cnn.CloseSqlConnection();
                }
            }

            return eliminado;
        }

        public bool CrearEmpleado(EmpleadoInfo empleado)
        {
            bool creado = false;

            using (SqlConnection cnn = _cnn.GetSqlConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_insert_empleado", cnn);
                cmd.Parameters.AddWithValue("@nombres", empleado.Nombres);
                cmd.Parameters.AddWithValue("@apellidos", empleado.Apellidos);
                cmd.Parameters.AddWithValue("@telefono", empleado.Telefono);
                cmd.Parameters.AddWithValue("@correo", empleado.Correo);
                cmd.Parameters.AddWithValue("@fecha_nacimiento", empleado.FechaNacimiento);
                cmd.Parameters.AddWithValue("@sueldo", empleado.Salario);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    cnn.Open();
                    cmd.ExecuteNonQuery();
                    creado = true;
                }
                catch (Exception ex)
                {
                    throw new Exception("Ha ocurrido un erro al intentar conectar el servidor DB", ex);
                }
                finally
                {
                    _cnn.CloseSqlConnection();
                }
            }

            return creado;
        }

        public bool UpdateEmpleado(EmpleadoInfo empleado)
        {
            bool actualizado = false;

            using (SqlConnection cnn = _cnn.GetSqlConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_update_empleado", cnn);
                cmd.Parameters.AddWithValue("@id", empleado.Id);
                cmd.Parameters.AddWithValue("@nombres", empleado.Nombres);
                cmd.Parameters.AddWithValue("@apellidos", empleado.Apellidos);
                cmd.Parameters.AddWithValue("@telefono", empleado.Telefono);
                cmd.Parameters.AddWithValue("@correo", empleado.Correo);
                cmd.Parameters.AddWithValue("@fecha_nacimiento", empleado.FechaNacimiento);
                cmd.Parameters.AddWithValue("@sueldo", empleado.Salario);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    cnn.Open();
                    cmd.ExecuteNonQuery();
                    actualizado = true;
                }
                catch (Exception ex)
                {
                    throw new Exception("Ha ocurrido un erro al intentar conectar el servidor DB", ex);
                }
                finally
                {
                    _cnn.CloseSqlConnection();
                }
            }

            return actualizado;
        }
    }
}