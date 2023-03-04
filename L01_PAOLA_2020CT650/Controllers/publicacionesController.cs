using L01_PAOLA_2020CT650.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L01_PAOLA_2020CT650.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class publicacionesController : ControllerBase
    {
        private readonly blogContext _blogContexto;

        public publicacionesController(blogContext publicacionesContexto)
        {
            _blogContexto = publicacionesContexto;
        }


        //CREATE 
        [HttpPost]
        [Route("add")]
        public IActionResult Crear([FromBody] publicaciones publicacionesNuevo)
        {

            try
            {
                _blogContexto.publicaciones.Add(publicacionesNuevo);
                _blogContexto.SaveChanges();

                return Ok(publicacionesNuevo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
        //READ
        [HttpGet]
        [Route("getall")]
        public IActionResult ObtenerPublicaciones()
        {
            List<publicaciones> listadoPublicaciones = (from db in _blogContexto.publicaciones
                                                        select db).ToList();

            if (listadoPublicaciones.Count == 0) { return NotFound(); }

            return Ok(listadoPublicaciones);
        }
        //UPDATE
        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult actualizarPublicaciones(int id, [FromBody] publicaciones publicacionesModificar)
        {
            publicaciones? publicacioneExiste = (from e in _blogContexto.publicaciones
                                                 where e.publicacionId == id
                                                 select e).FirstOrDefault();
            if (publicacioneExiste == null)
                return NotFound();

            publicacioneExiste.titulo = publicacionesModificar.titulo;
            publicacioneExiste.descripcion = publicacionesModificar.descripcion;
            publicacioneExiste.usuarioId = publicacionesModificar.usuarioId;

            _blogContexto.Entry(publicacioneExiste).State = EntityState.Modified;
            _blogContexto.SaveChanges();

            return Ok(publicacioneExiste);
        }
        //DELETE
        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult eliminarPublicacion(int id)
        {
            publicaciones? publicacionExiste = (from e in _blogContexto.publicaciones
                                                where e.publicacionId == id
                                                select e).FirstOrDefault();
            if (publicacionExiste == null)
                return NotFound();

            _blogContexto.Remove(publicacionExiste);
            _blogContexto.SaveChanges();

            return Ok(publicacionExiste);
        }

        //Método filtrado por un usuario en especifico
        [HttpGet]
        [Route("findbyuser/{usuario}")]
        public IActionResult buscar(int usuario)
        {

            List<publicaciones> publicacionesList = (from e in _blogContexto.publicaciones
                                                     where e.usuarioId.Equals(usuario)
                                                     select e).ToList();

            if (publicacionesList.Any())
            {
                return Ok(publicacionesList);
            }
            return NotFound();
        }
    }
}
