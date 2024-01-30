using Parking_App.Entities;
using System.ComponentModel.DataAnnotations;

namespace Parking_App.Models
{
    internal class EstanciaViewModel
    {
        public int Id { get; set; }
        public string Placa { get; set; }
        [Display(Name = "Hora de Ingreso")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm tt}")]
        public DateTime HoraIngreso { get; set; }
        [Display(Name = "Hora de Salida")]
		[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm tt}")]
		public DateTime? HoraSalida { get; set; } = DateTime.Parse(DateTime.Now.ToString("g"));
		public string TipoVehiculo { get; set; }
    }
}