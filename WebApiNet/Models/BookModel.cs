using System.ComponentModel.DataAnnotations;

namespace WebApiNet.Models
{
	public class BookModel
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Judul wajib diisi")]
		public string Title { get; set; }

		[Required(ErrorMessage = "Penulis wajib diisi")]
		public string Author { get; set; }

		public string CoverImagePath { get; set; }
	}
}
