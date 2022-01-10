using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Timeline.Data;
using Timeline.Models;

namespace Timeline.Controllers;

public class HomeController : Controller
{
	private readonly ILogger<HomeController> _logger;
#region Variables
	private readonly DataContext _context;
#endregion

	public HomeController(ILogger<HomeController> logger, DataContext _context){
		_logger = logger;
		this._context = _context;
	}

	public async Task<IActionResult> Index(){
		var _timecontext = await _context.timeline_context.OrderBy(x=> x.timec_date).ToListAsync(); 
		return View(_timecontext);
	}

	public IActionResult Privacy(){
		return View();
	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error(){
		return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}
}