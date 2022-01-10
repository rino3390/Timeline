using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Timeline.Models;

namespace Timeline.ViewModel;

public class LoginViewModel
{
	[Required(ErrorMessage = "欄位不得為空。")]
	[Display(Name = "帳號")]
	public string account { get; set; }
	[Required(ErrorMessage = "欄位不得為空。")]
	[Display(Name = "密碼")]
	public string password { get; set; }

	//檢查密碼配對
	public bool CheckLogin(users _user){
		var _logic = new Utility.HashedCode();
		return _logic.VerifyHashed(_user.user_password, password);
	}
}