using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Timeline.Models;

public class timeline
{
	[Key]
	public string timeline_id { get; set; }
	public string user_id { get; set; }
	public string timeline_name { get; set; }
	public string timeline_category { get; set; }
	public bool timeline_ispublic { get; set; }
	public bool timeline_everyedit { get; set; }
	public System.DateTime timeline_create_date { get; set; }
	public System.DateTime timeline_edit_date { get; set; }
	public int timeline_views { get; set; }
	public int timeline_likes { get; set; }
	public int timeline_stored { get; set; }
	
	[ForeignKey("timeline_id")]
	public virtual ICollection<timeline_context> timeline_context { get; set; }
	public virtual users users { get; set; }
}