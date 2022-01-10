using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;

namespace Timeline.Models;

public class users
{
	[Key]
	public string user_id { get; set; }
	public string user_account { get; set; }
	public string user_password { get; set; }
	public string user_nickname { get; set; }
	public string user_email { get; set; }
	public System.DateTime user_create_date { get; set; }
	[ForeignKey("user_id")]
	public virtual ICollection<timeline> timeline { get; set; }

	public void FindUser(string id){
	}
	
}