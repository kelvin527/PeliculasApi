using System.ComponentModel.DataAnnotations;

namespace PeliculasApi.Dtos
{
    public class GenerosDto
    {
        public int Id { get; set; }
        [Required]
        [StringLength(40)]
        public string Nombre { get; set; }
    }
}
