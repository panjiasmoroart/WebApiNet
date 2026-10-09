namespace WebApiNet.Models
{
	public class BookRequest
	{
		public string Title { get; set; }
		public string Author { get; set; }
		public IFormFile CoverFile { get; set; }
	}
}
