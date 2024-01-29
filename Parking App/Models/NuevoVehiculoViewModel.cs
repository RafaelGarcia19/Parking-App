using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Parking_App.Models
{
	public class NuevoVehiculoViewModel
	{
		public IEnumerable<SelectListItem> TipoVehiculos { get; set; }
		[Required(ErrorMessage = "El campo {0} es requerido")]
		public string Placa { get; set; }
		[Required(ErrorMessage = "El campo {0} es requerido")]
		[Display(Name = "Tipo de Vehiculo")]
		public int IdTipoVehiculo { get; set; }
	}
}
