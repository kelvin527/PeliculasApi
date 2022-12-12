using PeliculasApi.Validaiones;
using System.ComponentModel.DataAnnotations;

namespace PeliculasApi.Dtos
{
    public class AgregarActorDto
    {
        [Required]
        [StringLength(120)]
        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
        [PesoArchivoValidacion(PesoMaximoEnMegabytes:4)]
        public IFormFile Foto { get; set; }//SIRRVE PARA RECIBIR UN ARCHIVO

    }
}
