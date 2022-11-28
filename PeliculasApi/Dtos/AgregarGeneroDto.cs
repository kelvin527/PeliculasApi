using System.ComponentModel.DataAnnotations;

namespace PeliculasApi.Dtos
{
    public class AgregarGeneroDto
    {
        [Required(ErrorMessage ="campo requerido")]
        public string Nombre { get; set; }

    }
}
