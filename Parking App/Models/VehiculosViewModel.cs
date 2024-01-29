namespace Parking_App.Models
{
	public class VehiculosViewModel
	{
		public int Id { get; set; }
		public string Placa { get; set; } = null!;
		public string TipoVehiculo { get; set; } = null!;
		public bool Estado { get; set; }
	}
}
