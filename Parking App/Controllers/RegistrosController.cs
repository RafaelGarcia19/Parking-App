using Microsoft.AspNetCore.Mvc;

namespace Parking_App.Controllers
{
	public class RegistrosController : Controller
	{

		private readonly ILogger<HomeController> _logger;

		public RegistrosController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		[HttpGet]
		public IActionResult Salida()
		{
			return View();
		}

		[HttpGet]
		public IActionResult Entrada()
		{
			return View();
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
