using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KeimheanCafePOS.Desktop.Services;
using KeimheanCafePOS.Domain.Entities;

namespace KeimheanCafePOS.Desktop.ViewModels
{
	public partial class LoginViewModel : ViewModelBase
	{
		private readonly AuthenticationService _authService;

		[ObservableProperty]
		private string username = string.Empty;

		[ObservableProperty]
		private string password = string.Empty;

		[ObservableProperty]
		private string selectedRole = "Staff"; // Default selection

		[ObservableProperty]
		private string statusMessage = string.Empty;

		[ObservableProperty]
		private bool isBusy = false;

		[ObservableProperty]
		private bool hasError = false;

		[ObservableProperty]
		private string errorMessage = string.Empty;

		public event Action? LoginSucceeded;

		public LoginViewModel(AuthenticationService authService)
		{
			_authService = authService;
			Username = "staff";
			Password = "staff123";
		}

		public bool CanLogin => !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password) && !IsBusy;

		public string LoginButtonText => IsBusy ? "Logging in..." : "Login";

		[RelayCommand(CanExecute = nameof(CanLogin))]
		private async Task Login()
		{
			StatusMessage = string.Empty;
			IsBusy = true;
			LoginCommand.NotifyCanExecuteChanged();
			try
			{
				ClearError();
				Console.WriteLine($"[DEBUG] Login attempt - Username: '{Username}', Password length: {Password?.Length ?? 0}");
				// Don't validate role - let the server determine the user's role based on their account
				var success = await _authService.LoginAsync(Username, Password, null);
				Console.WriteLine($"[DEBUG] Login result: {success}");
				if (success)
				{
					StatusMessage = "Login successful";
					LoginSucceeded?.Invoke();
				}
				else
				{
					ShowError("Invalid credentials");
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"[DEBUG] Login exception: {ex}");
				ShowError("Error: " + ex.Message);
			}
			finally
			{
				IsBusy = false;
				LoginCommand.NotifyCanExecuteChanged();
			}
		}

		[RelayCommand]
		private void SelectRole(string role)
		{
			SelectedRole = role;
		}

		private void ShowError(string message)
		{
			ErrorMessage = message;
			HasError = true;
			StatusMessage = message;
		}

		private void ClearError()
		{
			HasError = false;
			ErrorMessage = string.Empty;
		}

		partial void OnIsBusyChanged(bool value) => LoginCommand.NotifyCanExecuteChanged();

		partial void OnUsernameChanged(string value) => LoginCommand.NotifyCanExecuteChanged();
		partial void OnPasswordChanged(string value) => LoginCommand.NotifyCanExecuteChanged();
	}
}