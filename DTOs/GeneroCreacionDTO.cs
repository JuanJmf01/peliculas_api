using PeliculasApii.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace PeliculasApii.DTOs
{

    //No se hace en 'GeneroDTO' ya que este DTO no tiene Id. Esta es la parte de creacion, no es conveniente Id
    public class GeneroCreacionDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 50)]
        [PrimeraLetraMayuscula]
        public string Nombre { get; set; }
    }
}
