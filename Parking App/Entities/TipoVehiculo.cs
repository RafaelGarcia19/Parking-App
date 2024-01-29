using System.ComponentModel.DataAnnotations;

namespace Parking_App.Entities
{
	public class TipoVehiculo
    {
		public int Id { get; set; }
		[Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres")]
        [Display(Name = "Tipo de Vehiculo")]
        public string Nombre { get; set; }
        [Required (ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Tarifa")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal Tarifa { get; set; }

	}
}
