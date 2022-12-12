using System.ComponentModel.DataAnnotations;

namespace PeliculasApi.Validaiones
{
    public class PesoArchivoValidacion:ValidationAttribute
    {
        private readonly int pesoMaximoEnMegabytes;

        public PesoArchivoValidacion(int PesoMaximoEnMegabytes)
        {
            pesoMaximoEnMegabytes = PesoMaximoEnMegabytes;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value is null) { return ValidationResult.Success;}

            IFormFile formFile = value as IFormFile;

            if(formFile is null) { return ValidationResult.Success; }

            if(formFile.Length > pesoMaximoEnMegabytes * 1024 * 1024)
            {
                return new ValidationResult($"El peso del archivo no puede ser mayor a {pesoMaximoEnMegabytes} mb");
            }

            return ValidationResult.Success;
        }
    }
}
