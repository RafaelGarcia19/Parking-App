using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Parking_App.Entities
{
	public class Estancia
	{
		public int Id { get; set; }
		[Required(ErrorMessage = "El campo {0} es requerido")]
		public DateTime HoraIngreso { get; set; }

		[Display(Name = "Hora de Salida")]
		public DateTime? HoraSalida { get; set; }

		[ForeignKey("Vehiculo")]
		public int VehiculoId { get; set; }
		public Vehiculo Vehiculo { get; set; }
		public Boolean Pagado { get; set; } = false;

	}
}