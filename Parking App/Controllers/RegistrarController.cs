using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Parking_App.Entities;
using Parking_App.Models;

namespace Parking_App.Controllers
{
	public class RegistrarController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly ApplicationDbContext context;

		public RegistrarController(ILogger<HomeController> logger, ApplicationDbContext context)
		{
			_logger = logger;
			this.context = context;
		}

		[HttpGet]
		public IActionResult Index()
		{
			var model = context.Vehiculos.Include(x => x.TipoVehiculo).ToList().Select(x => new VehiculosViewModel
			{
				Id = x.Id,
				Placa = x.Placa,
				TipoVehiculo = x.TipoVehiculo.Nombre,
				Estado = x.Estado
			});
			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> Registrar()
		{
			IEnumerable<SelectListItem> tipoVehiculosSelectList = await GetTipoVehiculos();
			var model = new NuevoVehiculoViewModel
			{
				TipoVehiculos = tipoVehiculosSelectList
			};
			return View(model);
		}

		private async Task<IEnumerable<SelectListItem>> GetTipoVehiculos()
		{
			var tipoVehiculos = await context.TipoVehiculos.ToListAsync();
			var tipoVehiculosSelectList = tipoVehiculos.Select(x => new SelectListItem(x.Nombre, x.Id.ToString()));
			return tipoVehiculosSelectList;
		}

		[HttpPost]
		public async Task<IActionResult> Registrar(NuevoVehiculoViewModel model)
		{
			if (!ModelState.IsValid)
			{
				model.TipoVehiculos = await GetTipoVehiculos();
				return View(model);
			}
			if (context.Vehiculos.Any(v => v.Placa == model.Placa))
			{
				ModelState.AddModelError("Placa", "La placa ya está registrada.");
				model.TipoVehiculos = await GetTipoVehiculos();
				return View(model);
			}
			var vehiculo = new Vehiculo
			{
				Placa = model.Placa,
				IdTipoVehiculo = model.IdTipoVehiculo,
				Estado = true
			};
			try
			{
				context.Vehiculos.Add(vehiculo);
				await context.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", "Error al guardar en la base de datos.");
				model.TipoVehiculos = await GetTipoVehiculos();
				return View(model);
			}

		}
	}
}
