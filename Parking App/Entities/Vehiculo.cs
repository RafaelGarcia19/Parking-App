using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Parking_App.Entities
{
	public class Vehiculo
	{
		public int Id { get; set; }
		[Required(ErrorMessage = "El campo {0} es requerido")]
		[MaxLength(7, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres")]
		public string Placa { get; set; }

		[Required(ErrorMessage = "El campo TipoVehiculo es requerido")]
		[ForeignKey("TipoVehiculo")]
		public int IdTipoVehiculo { get; set; }
		public virtual TipoVehiculo TipoVehiculo { get; set; } = null!;

		[Display(Name = "Estado")]
		[DefaultValue(true)]
		public bool Estado { get; set; }

		public List<Estancia> Estancia { get; set; }
	}
}
