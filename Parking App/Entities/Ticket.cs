using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Parking_App.Entities
{
	public class Ticket
	{
		public int Id { get; set; }
		[Required(ErrorMessage = "El campo {0} es requerido")]
		public DateTime HoraEmision { get; set; } = DateTime.Now;

		[Required(ErrorMessage = "El campo {0} es requerido")]
		public decimal ImporteCobrado { get; set; }

		[Required(ErrorMessage = "El campo {0} es requerido")]
		public int MinutosCobrados { get; set; }

		[ForeignKey("Vehiculo")]
		public int VehiculoId { get; set; }

		public Vehiculo Vehiculo { get; set; }

		public List<Estancia> Estancia { get; set; }

	}
}