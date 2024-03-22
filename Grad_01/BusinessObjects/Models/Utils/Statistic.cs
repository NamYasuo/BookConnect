using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Models.Utils
{
	public class Statistic
	{
		[Key]
		public Guid StatId { get; set; }
		public int View { get; set; }
		public int Interested { get; set; }
		public int Purchase { get; set; }
		public int Search { get; set; }
		//public int Share { get; set; }
	}
}


