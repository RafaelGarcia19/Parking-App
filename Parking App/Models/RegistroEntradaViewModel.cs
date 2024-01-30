using System.ComponentModel.DataAnnotations;

namespace Parking_App.Models
{
	public class RegistroEntradaViewModel
	{
		[Required(ErrorMessage = "El campo {0} es requerido")]
		public string Placa { get; set; }
		[Required(ErrorMessage = "El campo {0} es requerido")]
		[DataType(DataType.DateTime)]
		[Display (Name = "Fecha y hora de ingreso")]
        public DateTime HoraIngreso { get; set; } = DateTime.Parse(DateTime.Now.ToString("g"));
    }
}
