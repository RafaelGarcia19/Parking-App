using System.ComponentModel.DataAnnotations;

namespace Parking_App.Models
{
	public class SalidaViewModel
	{
		public int Id { get; set; }
		public string Placa { get; set; }
		public DateTime HoraIngreso { get; set; }
		public DateTime HoraSalida { get; set; } = DateTime.Parse(DateTime.Now.ToString("g"));
		public string TipoVehiculo { get; set; }
		[DisplayFormat(DataFormatString = "{3:C2}")]
		public double ValorAPagar { get; set; }
		public int VehiculoId { get; set; }
		public int TiempoEstancia { get; set; }
	}
}
