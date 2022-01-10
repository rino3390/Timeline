using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Timeline.Data;
using Timeline.ViewModel;

namespace Timeline.Controllers;

public class TimelineController : Controller{
#region Variables
	private readonly DataContext _context;
#endregion

#region Constructor
	public TimelineController(DataContext context){
		_context = context;
	}
#endregion

	// GET: 創建時間點
	[Authorize]
	public IActionResult CreateEvent(){
		return View(new TimePointCreateViewModel());
	}

	//POST: 創建時間點
	[HttpPost]
	[Authorize]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> CreateEvent(
		[Bind("dateTime,title,description,context,rank,dateto")] TimePointCreateViewModel _timepoint){
		if (ModelState.IsValid){
			_context.timeline_context.Add(_timepoint.CreateTimePoint());
			await _context.SaveChangesAsync();
			return RedirectToAction("Index", "Home");
		}

		return View(_timepoint);
	}
	
	// GET: User/Delete/5
	[Authorize]
	public async Task<IActionResult> Delete(string id){
		if (id == null) return NotFound();

		var _timepoint = await _context.timeline_context.FirstOrDefaultAsync(m => m.timec_id == id);

		if (_timepoint == null) return NotFound();

		return View(_timepoint);
	}

	// POST: User/Delete/5
	[HttpPost]
	[ActionName("Delete")]
	[ValidateAntiForgeryToken]
	[Authorize]
	public async Task<IActionResult> DeleteConfirmed([Bind("timec_id")]string id){
		var _timepoint = await _context.timeline_context.FindAsync(id);
		_context.timeline_context.Remove(_timepoint);
		await _context.SaveChangesAsync();
		return RedirectToAction("Index","Home");
	}
	
	public async Task<IActionResult> Details(string id){
		if (id == null) return NotFound();

		var _timepoint = await _context.timeline_context.FirstOrDefaultAsync(m => m.timec_id == id);

		if (_timepoint == null) return NotFound();

		return View(_timepoint);
	}
	
	[HttpPost]
	[ActionName("Details")]
	[ValidateAntiForgeryToken]
	[Authorize]
	public async Task<IActionResult> DetailsPost([Bind("timec_id")]string id){
		var _timepoint = await _context.timeline_context.FindAsync(id);
		_context.timeline_context.Remove(_timepoint);
		await _context.SaveChangesAsync();
		return RedirectToAction("Index","Home");
	}
	
	//檢查使用者
	private bool timepointExist(string id){
		return _context.timeline_context.Any(e => e.timec_id == id);
	}
}