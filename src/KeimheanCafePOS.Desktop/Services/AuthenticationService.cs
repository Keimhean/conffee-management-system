using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using KeimheanCafePOS.Domain.Entities;

namespace KeimheanCafePOS.Desktop.Services
{
	public class AuthenticationService
	{
		private readonly HttpClient _httpClient;
		private User? _currentUser;

		public User? CurrentUser => _currentUser;
		public bool IsAuthenticated => _currentUser != null;

		public AuthenticationService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<bool> LoginAsync(string username, string password, UserRole? expectedRole)
		{
			var hashedPassword = HashPassword(password);
			Console.WriteLine($"[DEBUG AuthService] Hashed password: {hashedPassword}");
			var request = new LoginRequest
			{
				Username = username,
				Password = hashedPassword,
				ExpectedRole = expectedRole
			};

			try
			{
				Console.WriteLine($"[DEBUG AuthService] Sending request to: {_httpClient.BaseAddress}api/auth/login");
				var response = await _httpClient.PostAsJsonAsync("api/auth/login", request);
				Console.WriteLine($"[DEBUG AuthService] Response status: {response.StatusCode}");
				if (!response.IsSuccessStatusCode) return false;
				var dto = await response.Content.ReadFromJsonAsync<UserDto>();
				if (dto == null) return false;

				_currentUser = new User
				{
					Id = dto.Id,
					Username = dto.Username,
					FullName = dto.FullName,
					Role = dto.Role,
					Email = dto.Email,
					LastLoginAt = dto.LastLoginAt
				};
				Console.WriteLine($"[DEBUG AuthService] Login successful for user: {dto.Username} ({dto.Role})");
				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"[DEBUG AuthService] Exception: {ex.Message}");
				return false;
			}
		}

		public void Logout() => _currentUser = null;
		public bool IsAdmin() => _currentUser?.Role == UserRole.Admin;
		public bool IsStaff() => _currentUser?.Role == UserRole.Staff;

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