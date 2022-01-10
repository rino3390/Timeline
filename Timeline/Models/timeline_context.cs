using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Timeline.Models;

public class timeline_context
{
	[Key]
	public string timec_id { get; set; }
	public string timeline_id { get; set; }
	public string timec_title { get; set; }
	public System.DateTime timec_date { get; set; }
	public string? timec_description { get; set; }
	public string? timec_context { get; set; }
	public int timec_rank { get; set; }
	public int timec_dateto { get; set; }
	public virtual timeline timeline { get; set; }
	[ForeignKey("timec_id")]
	public virtual ICollection<timeline_context_tags> timeline_context_tags { get; set; }
}