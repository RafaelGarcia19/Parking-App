using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parking_App.Entities;
using Parking_App.Models;

namespace Parking_App.Controllers
{
	public class RegistrosController : Controller
	{

		private readonly ILogger<HomeController> _logger;
		private readonly ApplicationDbContext context;

		public RegistrosController(ILogger<HomeController> logger, ApplicationDbContext context)
		{
			_logger = logger;
			this.context = context;
		}

		[HttpGet]
		public IActionResult Index()
		{
			var model = context.Estancias.Include(x => x.Vehiculo).Include(x => x.Vehiculo.TipoVehiculo).Where(x => x.HoraSalida == null).ToList().Select(x => new EstanciaViewModel
			{
				Id = x.Id,
				Placa = x.Vehiculo.Placa,
				HoraIngreso = x.HoraIngreso,
				HoraSalida = x.HoraSalida,
				TipoVehiculo = x.Vehiculo.TipoVehiculo.Nombre
			});
			return View(model);
		}

		[HttpGet]
		public IActionResult Salida(int id)
		{
			var estancia = context.Estancias.Include(x => x.Vehiculo).Include(x => x.Vehiculo.TipoVehiculo).FirstOrDefault(x => x.Id == id);

			if (estancia == null)
				return RedirectToAction("Index");
			estancia.HoraSalida = DateTime.Now;
			// Calcula las horas totales de estancia
			TimeSpan horasTotales = (TimeSpan)(estancia.HoraSalida - estancia.HoraIngreso);

			// Obtiene la tarifa por hora según el tipo de vehículo
			double tarifaPorHora = (double)estancia.Vehiculo.TipoVehiculo.Tarifa;

			// Calcula el valor a pagar multiplicando las horas totales por la tarifa por hora
			double valorAPagar = Math.Round((int)horasTotales.TotalHours * tarifaPorHora, 2);

			var model = new SalidaViewModel
			{
				Id = estancia.Id,
				HoraIngreso = estancia.HoraIngreso,
				HoraSalida = estancia.HoraSalida.Value,
				TipoVehiculo = estancia.Vehiculo.TipoVehiculo.Nombre,
				Placa = estancia.Vehiculo.Placa,
				ValorAPagar = valorAPagar,
				VehiculoId = estancia.VehiculoId
			};

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Salida(SalidaViewModel model)
		{
			if (!ModelState.IsValid)
				return View(model);
			var estancia = context.Estancias.FirstOrDefault(x => x.Id == model.Id);
			if (estancia == null)
				return RedirectToAction("Index");
			TimeSpan horasTotales = (TimeSpan)(model.HoraSalida - model.HoraIngreso);
			var estanciasList = new List<Estancia>
			{
				estancia
			};
			var ticket = new Ticket
			{
				HoraEmision = DateTime.Now,
				ImporteCobrado = (decimal)model.ValorAPagar,
				HorasCobradas = (int)horasTotales.TotalHours,
				VehiculoId = estancia.VehiculoId,
				Estancia = estanciasList
			};
			context.Ticket.Add(ticket);
			await context.SaveChangesAsync();
			estancia.HoraSalida = model.HoraSalida;
			context.Estancias.Update(estancia);
			await context.SaveChangesAsync();
			return RedirectToAction("Index");
		}


		[HttpGet]
		public IActionResult Entrada()
		{
			var model = new RegistroEntradaViewModel();
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Entrada(RegistroEntradaViewModel model)
		{
			if (!ModelState.IsValid)
				return View(model);
			var vehiculo = await ObtenerOVCrearVehiculo(model.Placa.ToUpper());
			if (vehiculo == null)
			{
				ModelState.AddModelError(string.Empty, "Ocurrió un error al registrar la entrada.");
				return View(model);
			}
			try
			{
				await RegistrarEntrada(vehiculo.Id, model.HoraIngreso);
				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{
				ModelState.AddModelError(string.Empty, $"Ocurrió un error al registrar la entrada: {ex.Message}");
				return View(model);
			}

		}

		private async Task<Vehiculo?> ObtenerOVCrearVehiculo(string placa)
		{
			var vehiculo = await context.Vehiculos.FirstOrDefaultAsync(v => v.Placa == placa);
			if (vehiculo == null)
			{
				var vehiculoNoResidente = await context.TipoVehiculos.FirstOrDefaultAsync(v => v.Nombre == "No Residente");
				vehiculo = new Vehiculo
				{
					Placa = placa,
					IdTipoVehiculo = vehiculoNoResidente?.Id ?? 0, // Asignar un valor por defecto en caso de que no se encuentre el tipo de vehículo
					Estado = true
				};

				context.Vehiculos.Add(vehiculo);
				await context.SaveChangesAsync();
			}
			return vehiculo;
		}

		private async Task RegistrarEntrada(int vehiculoId, DateTime horaIngreso)
		{
			var estancia = new Estancia
			{
				VehiculoId = vehiculoId,
				HoraIngreso = horaIngreso
			};
			context.Estancias.Add(estancia);
			await context.SaveChangesAsync();
		}

		[HttpGet]
		public IActionResult PagoResidentes()
		{
			return View();
		}

		[HttpGet]
		public IActionResult InicioMes()
		{
			return View();
		}
	}
}
