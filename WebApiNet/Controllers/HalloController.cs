using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiNet.Models;

namespace WebApiNet.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class HalloController : ControllerBase
	{
		private static List<HalloModel> _messages = new List<HalloModel>
		{
			new HalloModel { Id = 1, Message = "Hallo REST API!" },
			new HalloModel { Id = 2, Message = "Selamat datang di Web API." }
		};

		// GET: api/hallo
		[HttpGet]
		public ActionResult<IEnumerable<HalloModel>> GetMessages()
		{
			return Ok(_messages);
		}

		// GET: api/hallo/1
		[HttpGet("{id}")]
		public ActionResult<HalloModel> GetMessage(int id)
		{
			var message = _messages.FirstOrDefault(m => m.Id == id);
			if (message == null)
				return NotFound("Pesan tidak ditemukan.");
			return Ok(message);
		}

		// POST: api/hallo
		[HttpPost]
		public ActionResult<HalloModel> CreateMessage([FromBody] HalloModel newMessage)
		{
			newMessage.Id = _messages.Count + 1; // Auto-increment ID
			_messages.Add(newMessage);
			return CreatedAtAction(nameof(GetMessage), new { id = newMessage.Id }, newMessage);
		}

		// PUT: api/hallo/1
		[HttpPut("{id}")]
		public ActionResult UpdateMessage(int id, [FromBody] HalloModel updatedMessage)
		{
			var message = _messages.FirstOrDefault(m => m.Id == id);
			if (message == null)
				return NotFound("Pesan tidak ditemukan.");

			message.Message = updatedMessage.Message;
			return NoContent();
		}

		// DELETE: api/hallo/1
		[HttpDelete("{id}")]
		public ActionResult DeleteMessage(int id)
		{
			var message = _messages.FirstOrDefault(m => m.Id == id);
			if (message == null)
				return NotFound("Pesan tidak ditemukan.");

			_messages.Remove(message);
			return NoContent();
		}
	}
}
