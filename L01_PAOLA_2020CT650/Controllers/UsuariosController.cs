using L01_PAOLA_2020CT650.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L01_PAOLA_2020CT650.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly blogContext _blogContexto;

        public UsuariosController(blogContext usuariosContexto)
        {
            _blogContexto = usuariosContexto;
        }

        //CREATE 
        [HttpPost]
        [Route("add")]
        public IActionResult Crear([FromBody] usuarios usuarioNuevo)
        {

            try
            {
                _blogContexto.usuarios.Add(usuarioNuevo);
                _blogContexto.SaveChanges();

                return Ok(usuarioNuevo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
        //READ
        [HttpGet]
        [Route("getall")]
        public IActionResult ObtenerUsuarios()
        {
            List<usuarios> listadoUsuarios = (from db in _blogContexto.usuarios
                                              select db).ToList();

            if (listadoUsuarios.Count == 0) { return NotFound(); }

            return Ok(listadoUsuarios);
        }
        //UPDATE
        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult actualizarUsuario(int id, [FromBody] usuarios usuarioModificar)
        {
            usuarios? usuarioExiste = (from e in _blogContexto.usuarios
                                       where e.usuarioId == id
                                       select e).FirstOrDefault();
            if (usuarioExiste == null)
                return NotFound();

            usuarioExiste.nombreUsuario = usuarioModificar.nombreUsuario;
            usuarioExiste.rolId = usuarioModificar.rolId;
            usuarioExiste.nombre = usuarioModificar.nombre;
            usuarioExiste.apellido = usuarioModificar.apellido;

            _blogContexto.Entry(usuarioExiste).State = EntityState.Modified;
            _blogContexto.SaveChanges();

            return Ok(usuarioExiste);
        }
        //DELETE
        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult eliminarEquipo(int id)
        {
            usuarios? usuarioExiste = (from e in _blogContexto.usuarios
                                       where e.usuarioId == id
                                       select e).FirstOrDefault();
            if (usuarioExiste == null)
                return NotFound();

            _blogContexto.Remove(usuarioExiste);
            _blogContexto.SaveChanges();

            return Ok(usuarioExiste);
        }

        //Método filtrado por nombre y apellido
        [HttpGet]
        [Route("find/{nombre}/{apellido}")]
        public IActionResult buscar(String nombre, String apellido)
        {

            List<usuarios> usuariosList = (from e in _blogContexto.usuarios
                                           where e.nombre.Contains(nombre) || e.apellido.Contains(apellido)
                                           select e).ToList();

            if (usuariosList.Any())
            {
                return Ok(usuariosList);
            }
            return NotFound();
        }

        //Método filtrado por rol
        [HttpGet]
        [Route("find/{rol}")]
        public IActionResult buscar(int rol)
        {

            List<usuarios> usuariosList = (from e in _blogContexto.usuarios
                                           where e.rolId.Equals(rol)
                                           select e).ToList();

            if (usuariosList.Any())
            {
                return Ok(usuariosList);
            }
            return NotFound();
        }
    }
}
