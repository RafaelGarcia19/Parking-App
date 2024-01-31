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
			var totalMinutes = 0;
			if (estancia.Vehiculo.TipoVehiculo.Nombre == "Residente")
			{
				var estancias = context.Estancias.Include(x => x.Vehiculo).Include(x => x.Vehiculo.TipoVehiculo).Where(x => x.VehiculoId == estancia.VehiculoId).Where(x => x.HoraSalida != null).ToList();
				foreach (var item in estancias)
				{
					totalMinutes += calculateMinutes(item.HoraIngreso, item.HoraSalida.Value);
				}
			}
			estancia.HoraSalida = DateTime.Now;
			totalMinutes += calculateMinutes(estancia.HoraIngreso, estancia.HoraSalida.Value);
			double tarifaPorMinuto = (double)estancia.Vehiculo.TipoVehiculo.Tarifa;
			double valorAPagar = Math.Round(totalMinutes * tarifaPorMinuto, 2);
			var model = new SalidaViewModel
			{
				Id = estancia.Id,
				HoraIngreso = estancia.HoraIngreso,
				HoraSalida = estancia.HoraSalida.Value,
				TipoVehiculo = estancia.Vehiculo.TipoVehiculo.Nombre,
				Placa = estancia.Vehiculo.Placa,
				ValorAPagar = valorAPagar,
				VehiculoId = estancia.VehiculoId,
				TiempoEstancia = totalMinutes
			};
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Salida(SalidaViewModel model)
		{
			var NoResidente = "No Residente";
			var Residente = "Residente";
			if (!ModelState.IsValid)
				return View(model);
			try
			{
				var estancia = context.Estancias.Include(x => x.Vehiculo).Include(x => x.Vehiculo.TipoVehiculo).FirstOrDefault(x => x.Id == model.Id);
				if (estancia == null)
					return RedirectToAction("Index");
				if (estancia.Vehiculo.TipoVehiculo.Nombre == NoResidente)
					CrearTicket(model, estancia);
				estancia.HoraSalida = model.HoraSalida;
				if (estancia.Vehiculo.TipoVehiculo.Nombre != Residente)
					estancia.Pagado = true;
				context.Estancias.Update(estancia);
				await context.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{
				ModelState.AddModelError(string.Empty, $"Ocurrió un error al registrar la salida: {ex.Message}");
				return View(model);
			}
		}

		private void CrearTicket(SalidaViewModel model, Estancia estancia)
		{
			var totalMinutes = calculateMinutes(estancia.HoraIngreso, model.HoraSalida);
			var estanciasList = new List<Estancia> { estancia };
			var ticket = new Ticket
			{
				HoraEmision = DateTime.Now,
				ImporteCobrado = (decimal)model.ValorAPagar,
				MinutosCobrados = totalMinutes,
				VehiculoId = estancia.VehiculoId,
				Estancia = estanciasList
			};
			context.Ticket.Add(ticket);
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

		private int calculateMinutes(DateTime horaIngreso, DateTime horaSalida)
		{
			TimeSpan horasTotales = horaSalida - horaIngreso;
			return (int)horasTotales.TotalMinutes;
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
			var model = new InicioMesViewModel();
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> InicioMes(InicioMesViewModel model)
		{
			var oficial = "Oficial";
			var residente = "Residente";
			if (!ModelState.IsValid)
				return View(model);
			try
			{
				var estancias = context.Estancias.Include(x => x.Vehiculo).Include(x => x.Vehiculo.TipoVehiculo).Where(x => x.Vehiculo.TipoVehiculo.Nombre == oficial).ToList();
				foreach (Estancia item in estancias)
				{
					context.Estancias.Remove(item);
				}

				var estanciasResidentes = context.Estancias.Include(x => x.Vehiculo).Include(x => x.Vehiculo.TipoVehiculo).Where(x => x.Vehiculo.TipoVehiculo.Nombre == residente).ToList();
				foreach (Estancia item in estanciasResidentes)
				{
					if (item.HoraSalida == null)
						item.HoraSalida = DateTime.Now;
					item.Pagado = true;
					context.Estancias.Update(item);
				}
				await context.SaveChangesAsync();

				return RedirectToAction("Index", "Home");
			}
			catch (Exception ex)
			{
				ModelState.AddModelError(string.Empty, $"Ocurrió un error al registrar la entrada: {ex.Message}");
				return View(model);
			}
		}

		[HttpPost]
		public IActionResult GenerarInforme(string nombreArchivo)
		{
			var informeData = ObtenerDatosInforme();
			var filePath = GuardarInformeEnArchivo(nombreArchivo, informeData);

			// Retorna un FileResult para que el navegador descargue el archivo
			return File(filePath, "text/plain", $"{nombreArchivo}.txt");
		}

		private IEnumerable<InformeViewModel> ObtenerDatosInforme()
		{
			throw new NotImplementedException();
		}

		private string GuardarInformeEnArchivo(string nombreArchivo, IEnumerable<InformeViewModel> informeData)
		{
			// Lógica para guardar los datos en el archivo con el formato especificado
			var filePath = Path.Combine("wwwroot/informes", $"{nombreArchivo}.txt");

			try
			{
				using (StreamWriter sw = new StreamWriter(filePath))
				{
					sw.WriteLine("Núm. placa\tTiempo estacionado (min.)\tCantidad a pagar");

					foreach (var informe in informeData)
					{
						sw.WriteLine($"{informe.NumPlaca}\t{informe.TiempoEstacionado}\t{informe.CantidadPagar}");
					}
				}
				ViewBag.Message = "Informe generado correctamente.";
			}
			catch (Exception ex)
			{
				ViewBag.Message = $"Error al generar el informe: {ex.Message}";
			}
			return filePath;
		}
	}
}
