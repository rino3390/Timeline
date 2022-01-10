#nullable disable
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Timeline.Data;
using Timeline.Models;
using Timeline.ViewModel;

namespace Timeline.Controllers;

public class UserController : Controller{
#region Variables
	private readonly DataContext _context;
#endregion

#region Constructor
	public UserController(DataContext context){
		_context = context;
	}
#endregion

#region Methods
	// GET: User/Delete/5
	[Authorize]
	public async Task<IActionResult> Delete(string id){
		if (id == null) return NotFound();

		var users = await _context.users.FirstOrDefaultAsync(m => m.user_id == id);

		if (users == null) return NotFound();

		return View(users);
	}

	// POST: User/Delete/5
	[HttpPost]
	[ActionName("Delete")]
	[ValidateAntiForgeryToken]
	[Authorize]
	public async Task<IActionResult> DeleteConfirmed(string id){
		var users = await _context.users.FindAsync(id);
		_context.users.Remove(users);
		await _context.SaveChangesAsync();
		return RedirectToAction("Index","Home");
	}

	// GET: User/Details/5
	[Authorize]
	public async Task<IActionResult> Details(string id){
		if (id == null) return NotFound();

		var users = await _context.users.FirstOrDefaultAsync(m => m.user_id == id);

		if (users == null) return NotFound();

		return View(users);
	}

	// GET: User/Edit/5
	[Authorize]
	public async Task<IActionResult> Edit(string id){
		if (id == null) return NotFound();

		var users = await _context.users.FindAsync(id);

		if (users == null) return NotFound();

		return View(users);
	}

	// POST: User/Edit/5
	// To protect from overposting attacks, enable the specific properties you want to bind to.
	// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
	[HttpPost]
	[ValidateAntiForgeryToken]
	[Authorize]
	public async Task<IActionResult> Edit(
		string id, [Bind("user_id,user_account,user_password,user_nickname,user_email,user_create_date")] users users){
		if (id != users.user_id) return NotFound();

		if (ModelState.IsValid){
			try{
				_context.Update(users);
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException){
				if (!usersExists(users.user_id)) return NotFound();

				throw;
			}

			return RedirectToAction(nameof(Index));
		}

		return View(users);
	}

	// GET: User
	[Authorize(Roles = "Admin")]
	public async Task<IActionResult> Index(){
		return View(await _context.users.ToListAsync());
	}

	//POST: 登入
	[HttpPost]
	public async Task<IActionResult> Login(LoginViewModel request, string returnUrl = ""){
		ViewBag.ReturnUrl = returnUrl;

		if (ModelState.IsValid){
			var _user = _context.users.FirstOrDefault(x => x.user_account == request.account);

			if (_user != null){
				if (request.CheckLogin(_user)){
					await UserLogin(request);
					if (!string.IsNullOrEmpty(returnUrl)) return Redirect(returnUrl);
					return RedirectToAction("Index", "Home");
				}

				ViewBag.Result = "帳號或密碼錯誤。";
			}
			else{
				ViewBag.Result = "沒有此帳號。";
			}

			return View(request);
		}

		return View(request);
	}

	//GET: 登入
	public IActionResult LogIn(string returnUrl = ""){
		ViewBag.ReturnUrl = returnUrl;
		return View();
	}

	[HttpPost]
	public async Task<IActionResult> LogOut(){
		await HttpContext.SignOutAsync();
		return RedirectToAction("Index", "Home");
	}

	// GET: 註冊
	//登入狀態導向回首頁
	public IActionResult Register(){
		if (User.Identity!.IsAuthenticated) return RedirectToAction("Index", "Home");

		return View();
	}

	// POST: 註冊
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Register(
		[Bind("user_account,user_password,comparePassword,user_nickname,user_email")] UserCreateViewModel user){
		if (ModelState.IsValid){
			_context.users.Add(user.CreateUser());
			await _context.SaveChangesAsync();
			return RedirectToAction("Index", "Home");
		}

		return View(user);
	}

	//設定連線
	private async Task UserLogin(LoginViewModel request){
		var _user = _context.users.First(x => x.user_account == request.account);
		var claims = new List<Claim> {
			new(ClaimTypes.Sid, _user.user_id),
			new(ClaimTypes.NameIdentifier, _user.user_account),
			new(ClaimTypes.Name, _user.user_nickname),
			new(ClaimTypes.Email, _user.user_email),
			new(ClaimTypes.Role, "User")
		};

		if (_user.user_account == "rino3390"){
			claims.Add(new (ClaimTypes.Role,"Admin"));
		}
		var claimsIdentity = new ClaimsIdentity(claims,"Basic");
		var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
		await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
	}

	//檢查使用者
	private bool usersExists(string id){
		return _context.users.Any(e => e.user_id == id);
	}
#endregion
}