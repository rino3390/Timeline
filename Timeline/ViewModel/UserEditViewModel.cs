using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Timeline.Data;
using Timeline.Models;

namespace Timeline.ViewModel;

public class UserEditViewModel
{
	[StringLength(20, ErrorMessage = "長度需界在1~20字之間。", MinimumLength = 1)]
	[Required(ErrorMessage = "欄位不得為空。"),
	 RegularExpression("[\u4e00-\u9fa5\\w]+$", ErrorMessage = "不可使用特殊字元。")]
	[Display(Name = "暱稱")]
	public string user_nickname { get; set; }

	[Required(ErrorMessage = "欄位不得為空。"), EmailAddress(ErrorMessage = "信箱格式不正確。")]
	[Remote(action: "VerifyEmail", controller: "Validation", HttpMethod = "POST")]
	[Display(Name = "信箱")]
	public string user_email { get; set; }
}