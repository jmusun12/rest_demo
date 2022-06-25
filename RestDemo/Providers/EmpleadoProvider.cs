using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using Newtonsoft.Json;
using RestDemo.DTOs;

namespace RestDemo.Providers
{
    public class EmpleadoProvider
    {
        private HttpClient httpClient;

        public EmpleadoProvider(string baseUrl)
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(baseUrl);
        }

        /// <summary>
        /// Realiza la peticion get del api y retorna el objeto listado de empleado
        /// </summary>
        /// <returns></returns>
        public List<EmpleadoDto> GetEmpleados()
        {
            var request = httpClient.GetAsync("api/empleado").Result;
            if (!request.IsSuccessStatusCode) return null;

            string json = request.Content.ReadAsStringAsync().Result;
            List<EmpleadoDto> lst = JsonConvert.DeserializeObject<List<EmpleadoDto>>(json);

            return lst;
        }

        /// <summary>
        /// Realiza la peticion GET del api para obtener los empleados
        /// </summary>
        /// <param name="id">Identificador de empleado</param>
        /// <returns></returns>
        public EmpleadoDto GetEmpleado(int id)
        {
            var request = httpClient.GetAsync($"api/empleado/{ id }").Result;
            if (!request.IsSuccessStatusCode) return null;

            string json = request.Content.ReadAsStringAsync().Result;
            EmpleadoDto empleado = JsonConvert.DeserializeObject<EmpleadoDto>(json);

            return empleado;
        }

        /// <summary>
        /// Realiza la peticion POST del api para crear un cliente
        /// </summary>
        /// <param name="empleado"></param>
        /// <returns></returns>
        public bool CrearCliente(DetalleEmpleadoDto empleado)
        {
            var request = httpClient.PostAsJsonAsync("api/empleado", empleado).Result;
            if (!request.IsSuccessStatusCode) return false;

            return true;
        }

        /// <summary>
        /// Realiza la peticion PUT de api para actualizar un cliente
        /// </summary>
        /// <param name="id"></param>
        /// <param name="empleado"></param>
        /// <returns></returns>
        public bool ActualizarCliente(int id, DetalleEmpleadoDto empleado)
        {
            var request = httpClient.PutAsJsonAsync($"api/empleado/{ id }", empleado).Result;
            if (!request.IsSuccessStatusCode) return false;

            return true;
        }


        /// <summary>
        /// Realiza la peticion DELETE del api para eliminar el empleado
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool EliminarCliente(int id)
        {
            var request = httpClient.DeleteAsync($"api/empleado/{id}").Result;
            if (!request.IsSuccessStatusCode) return false;

            return true;
        }
    }
}