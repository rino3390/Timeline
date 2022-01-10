using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Timeline.Data;
using Timeline.Models;

namespace Timeline.ViewModel
{
	public class UserCreateViewModel
	{
		[StringLength(25, ErrorMessage = "長度需界在6~25位元。", MinimumLength = 6)]
		[Required(ErrorMessage = "欄位不得為空。"), RegularExpression("^[a-zA-Z]\\w*$", ErrorMessage = "第一個字元需為英文，且只包含英文數字跟底線。")]
		[Remote(action: "VerifyAccount", controller: "Validation", HttpMethod = "POST")]
		[Display(Name = "帳號")]
		public string user_account { get; set; }

		[StringLength(25, ErrorMessage = "長度需界在6~25位元。", MinimumLength = 6)]
		[Required(ErrorMessage = "欄位不得為空。"), RegularExpression("\\w*$", ErrorMessage = "密碼只能包含英文數字跟底線。")]
		[Display(Name = "密碼")]
		public string user_password { get; set; }

		[StringLength(25)]
		[Required(ErrorMessage = "欄位不得為空。"), Compare("user_password", ErrorMessage = "和輸入密碼不同。")]
		[Display(Name = "密碼確認")]
		public string comparePassword { get; set; }

		
		[StringLength(20, ErrorMessage = "長度需界在1~20字之間。", MinimumLength = 1)]
		[Required(ErrorMessage = "欄位不得為空。"),
		 RegularExpression("[\u4e00-\u9fa5\\w]+$", ErrorMessage = "不可使用特殊字元。")]
		[Display(Name = "暱稱")]
		public string user_nickname { get; set; }

		[Required(ErrorMessage = "欄位不得為空。"), EmailAddress(ErrorMessage = "信箱格式不正確。")]
		[Remote(action: "VerifyEmail", controller: "Validation", HttpMethod = "POST")]
		[Display(Name = "信箱")]
		public string user_email { get; set; }

		public users CreateUser(){
			
			var _user = new users();
			_user.user_id = Guid.NewGuid().ToString();
			_user.user_account = user_account;
			var _logic = new Utility.HashedCode();
			_user.user_password = _logic.Hashed(user_password);
			_user.user_nickname = user_nickname;
			_user.user_email = user_email;
			_user.user_create_date = DateTime.Now;
			return _user;
		}
	}
}