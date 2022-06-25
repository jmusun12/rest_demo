using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using RestDemo.Models;
using RestDemo.Repositories;
using RestDemo.DTOs;
using AutoMapper;
using RestDemo.App_Start;

namespace RestDemo.Controllers
{
    public class EmpleadoController : ApiController
    {
        private EmpleadoRepository empleadoRep = new EmpleadoRepository();
        private IMapper _mapper = null;

        public EmpleadoController()
        {
            _mapper = AutoMapperConfig.GetMapper();
        }

        // Get api/empleados
        public IEnumerable<EmpleadoDto> Get()
        {
            List<EmpleadoInfo> empleados = empleadoRep.GetEmpleadoInfos();
            List<EmpleadoDto> empleadosDto = _mapper.Map<List<EmpleadoDto>>(empleados);
            return empleadosDto;
        }

        // Get api/empleados/{id}
        public EmpleadoDto Get(int id)
        {
            EmpleadoInfo empleado = empleadoRep.GetEmpleado(id);
            return _mapper.Map<EmpleadoDto>(empleado);
        }

        // Post api/empleados
        public bool Post([FromBody] DetalleEmpleadoDto empleado)
        {
            EmpleadoInfo empleadoInfo = _mapper.Map<EmpleadoInfo>(empleado);
            return empleadoRep.CrearEmpleado(empleadoInfo);
        }

        // PUT api/empleados/5
        public bool Put(int id, [FromBody] DetalleEmpleadoDto empleado)
        {
            EmpleadoInfo empleadoDB = empleadoRep.GetEmpleado(id);
            if (empleadoDB == null) return false;

            _mapper.Map<DetalleEmpleadoDto, EmpleadoInfo>(empleado, empleadoDB);
            return empleadoRep.UpdateEmpleado(empleadoDB);
        }

        // DELETE api/empleados/5
        public bool Delete(int id)
        {
            EmpleadoInfo empleadoDB = empleadoRep.GetEmpleado(id);
            if (empleadoDB == null) return false;

            return empleadoRep.DeleteEmpleado(id);
        }
    }
}