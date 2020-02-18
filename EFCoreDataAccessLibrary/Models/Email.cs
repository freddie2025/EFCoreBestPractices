using System.ComponentModel.DataAnnotations;

namespace EFCoreDataAccessLibrary.Models
{
	public class Email
	{
		public int Id { get; set; }

		[Required]
		[MaxLength(200)]
		public string EmailAddress { get; set; }
	}
}
