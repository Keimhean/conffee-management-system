using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using KeimheanCafePOS.Domain.Entities;
using KeimheanCafePOS.Infrastructure.Data;

namespace KeimheanCafePOS.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AuthController : ControllerBase
	{
		private readonly ApplicationDbContext _context;
		public AuthController(ApplicationDbContext context)
		{
			_context = context;
		}

		[HttpPost("login")]
		public async Task<ActionResult<UserDto>> Login([FromBody] LoginRequest request)
		{
			if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
				return BadRequest("Username and password are required");

			// Password is already hashed by the client
			var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username && u.PasswordHash == request.Password && u.IsActive);
			if (user == null) return Unauthorized();
			if (request.ExpectedRole.HasValue && user.Role != request.ExpectedRole.Value) return Unauthorized();

			user.LastLoginAt = DateTime.UtcNow;
			await _context.SaveChangesAsync();

			return Ok(new UserDto
			{
				Id = user.Id,
				Username = user.Username,
				FullName = user.FullName,
				Role = user.Role,
				Email = user.Email,
				LastLoginAt = user.LastLoginAt
			});
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<UserDto>> GetUser(int id)
		{
			var user = await _context.Users.FindAsync(id);
			if (user == null || !user.IsActive) return NotFound();
			return Ok(new UserDto
			{
				Id = user.Id,
				Username = user.Username,
				FullName = user.FullName,
				Role = user.Role,
				Email = user.Email,
				LastLoginAt = user.LastLoginAt
			});
		}

		[HttpGet]
		public async Task<ActionResult<System.Collections.Generic.IEnumerable<UserDto>>> GetAll()
		{
			var users = await _context.Users.Where(u => u.IsActive).ToListAsync();
			return Ok(users.Select(u => new UserDto
			{
				Id = u.Id,
				Username = u.Username,
				FullName = u.FullName,
				Role = u.Role,
				Email = u.Email,
				LastLoginAt = u.LastLoginAt
			}));
		}

		private static string HashPassword(string password)
		{
			using var sha = SHA256.Create();
			var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
			return BitConverter.ToString(bytes).Replace("-", "").ToLowerInvariant();
		}
	}

	public class LoginRequest
	{
		public string Username { get; set; } = string.Empty;
		public string Password { get; set; } = string.Empty;
		public UserRole? ExpectedRole { get; set; }
	}

	public class UserDto
	{
		public int Id { get; set; }
		public string Username { get; set; } = string.Empty;
		public string FullName { get; set; } = string.Empty;
		public UserRole Role { get; set; }
		public string? Email { get; set; }
		public DateTime? LastLoginAt { get; set; }
	}
}