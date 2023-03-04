using System.ComponentModel.DataAnnotations;

namespace L01_PAOLA_2020CT650.Models
{
    public class usuarios
    {
        //usuarios
        [Key]
        public int usuarioId { get; set; }

        public int rolId { get; set; }

        public string nombreUsuario { get; set; }

        public string clave { get; set; }

        public string nombre { get; set; }

        public string apellido { get; set; }

    }
}
