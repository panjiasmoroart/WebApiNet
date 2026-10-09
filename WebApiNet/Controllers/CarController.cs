using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiNet.Models;

namespace WebApiNet.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CarController : ControllerBase
	{
		private readonly CarDbContext _context;

		public CarController(CarDbContext context)
		{
			_context = context;
		}

		// GET: api/car
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Car>>> GetCars()
		{
			return await _context.Cars.ToListAsync();
		}

		// GET: api/car/1
		[HttpGet("{id}")]
		public async Task<ActionResult<Car>> GetCar(int id)
		{
			var car = await _context.Cars.FindAsync(id);
			if (car == null)
				return NotFound("Mobil tidak ditemukan.");
			return Ok(car);
		}

		// POST: api/car
		[HttpPost]
		public async Task<ActionResult<Car>> CreateCar([FromBody] Car newCar)
		{
			_context.Cars.Add(newCar);
			await _context.SaveChangesAsync();
			return CreatedAtAction(nameof(GetCar), new { id = newCar.Id }, newCar);
		}

		// PUT: api/car/1
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateCar(int id, [FromBody] Car updatedCar)
		{
			var car = await _context.Cars.FindAsync(id);
			if (car == null)
				return NotFound("Mobil tidak ditemukan.");

			car.Name = updatedCar.Name;
			car.Brand = updatedCar.Brand;
			car.Year = updatedCar.Year;
			await _context.SaveChangesAsync();
			return NoContent();
		}

		// DELETE: api/car/1
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteCar(int id)
		{
			var car = await _context.Cars.FindAsync(id);
			if (car == null)
				return NotFound("Mobil tidak ditemukan.");

			_context.Cars.Remove(car);
			await _context.SaveChangesAsync();
			return NoContent();
		}
	}
}
