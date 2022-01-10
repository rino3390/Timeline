using Microsoft.AspNetCore.Mvc;
using Timeline.Data;

namespace Timeline.Controllers;

public class ValidationController : Controller
{
	private readonly DataContext _context;

	public ValidationController(DataContext context){
		_context = context;
	}
	[HttpPost]
	public JsonResult VerifyAccount([Bind(Prefix = "user_account")] string account){
		if (_context.users.Any(x => x.user_account == account)){
			return Json("該帳號已註冊。");
		}
		return Json(true);
	}
	[HttpPost]
	public JsonResult VerifyEmail([Bind(Prefix = "user_email")] string email){
		if (_context.users.Any(x => x.user_email == email)){
			return Json("該信箱已註冊。");
		}
		return Json(true);
	}
}