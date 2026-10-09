using System.ComponentModel.DataAnnotations;

namespace WebApiNet.Models
{
	public class Car
	{
		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = "Nama mobil wajib diisi")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Merek wajib diisi")]
		public string Brand { get; set; }

		[Range(1886, 2100, ErrorMessage = "Tahun harus antara 1886 - 2100")]
		public int Year { get; set; }
	}
}
