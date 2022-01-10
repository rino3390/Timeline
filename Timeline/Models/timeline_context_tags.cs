using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Timeline.Models;

public class timeline_context_tags
{
	[Key]
	[Column(Order = 1)]
	public string timec_id { get; set; }
	[Key]
	[Column(Order = 2)]
	public string timec_tags { get; set; }
    
	public virtual timeline_context timeline_context { get; set; }
}