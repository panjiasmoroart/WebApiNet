using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiNet.Models;

namespace WebApiNet.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BookController : ControllerBase
	{
		// jadi kali ini tidak menggunakan database, tapi kita akan menyimpan data buku di memory saja
		private static List<BookModel> _books = new List<BookModel>();
		private readonly IWebHostEnvironment _webHostEnvironment;

		public BookController(IWebHostEnvironment webHostEnvironment)
		{
			_webHostEnvironment = webHostEnvironment;
		}

		// GET: api/book
		[HttpGet]
		public IActionResult GetBooks()
		{
			return Ok(_books);
		}

		// GET: api/book/1
		[HttpGet("{id}")]
		public IActionResult GetBook(int id)
		{
			var book = _books.Find(b => b.Id == id);
			if (book == null)
				return NotFound("Buku tidak ditemukan.");
			return Ok(book);
		}

		// POST: api/book/upload
		[HttpPost("upload")]
		[Consumes("multipart/form-data")]
		public IActionResult UploadBook([FromForm] BookRequest o)
		{
			if (o == null)
				return BadRequest("Semua field harus diisi!");

			if (o.CoverFile == null || o.CoverFile.Length == 0)
			{
				return BadRequest("File tidak di upload.");
			}

			string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");

			// Buat folder jika belum ada
			if (!Directory.Exists(uploadsFolder))
			{
				Directory.CreateDirectory(uploadsFolder);
			}

			// Simpan file dengan nama unik
			string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(o.CoverFile.FileName);
			string filePath = Path.Combine(uploadsFolder, uniqueFileName);

			using (var fileStream = new FileStream(filePath, FileMode.Create))
			{
				o.CoverFile.CopyTo(fileStream);
			}

			// Simpan metadata buku ke list
			var book = new BookModel
			{
				Id = _books.Count + 1,
				Title = o.Title,
				Author = o.Author,
				CoverImagePath = "/uploads/" + uniqueFileName
			};

			_books.Add(book);
			return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
		}
	}
}
