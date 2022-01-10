using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Timeline.Models;

namespace Timeline.ViewModel;

public class TimePointCreateViewModel{
	

	[Required(ErrorMessage = "欄位不得為空。")]
	[Display(Name = "時間")]
	public DateTime dateTime { get; set; }
	
	[StringLength(100)]
	[Required(ErrorMessage = "欄位不得為空。")]
	[Display(Name = "標題")]
	public string title { get; set; }

	[StringLength(200)]
	[Display(Name = "簡述")]
	public string? description { get; set; }
	
	[Display(Name = "內容")]
	public string? context { get; set; }

	[Required(ErrorMessage = "欄位不得為空。")]
	[Display(Name = "優先級(數字越高越為重要)")]
	public int rank { get; set; }

	[Required(ErrorMessage = "欄位不得為空。")]
	[Display(Name = "顯示到")]
	public int dateto { get; set; }
	
	public List<SelectListItem> rankEnum { get; } = new List<SelectListItem>
	{
		new SelectListItem { Value = "0", Text = "1" },
		new SelectListItem { Value = "1", Text = "2"  },
		new SelectListItem { Value = "2", Text = "3"  },
		new SelectListItem { Value = "3", Text = "4"  },
		new SelectListItem { Value = "4", Text = "5"  },
	};
	public List<SelectListItem> dateEnum { get; } = new List<SelectListItem>
	{
		new SelectListItem { Value = "0", Text = "年" },
		new SelectListItem { Value = "1", Text = "月"  },
		new SelectListItem { Value = "2", Text = "日"  },
		new SelectListItem { Value = "3", Text = "時"  },
		new SelectListItem { Value = "4", Text = "分"  },
	};

	
	public timeline_context CreateTimePoint(){
		var _context = new timeline_context();
		_context.timec_id = Guid.NewGuid().ToString();
		//RTODO 之後要換掉
		_context.timeline_id = "46a073cf-c888-44bb-ade2-33788d055f1b";
		_context.timec_date = dateTime;
		_context.timec_title = title;
		_context.timec_description = description;
		_context.timec_context = context;
		_context.timec_rank = rank;
		_context.timec_dateto = dateto;
		return _context;
	}
}