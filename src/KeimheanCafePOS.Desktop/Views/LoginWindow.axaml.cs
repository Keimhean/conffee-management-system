using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using KeimheanCafePOS.Desktop.ViewModels;
using System;

namespace KeimheanCafePOS.Desktop.Views
{
	public partial class LoginWindow : Window
	{
		public event Action? LoginSucceeded;

		public LoginWindow()
		{
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			AvaloniaXamlLoader.Load(this);
		}

		protected override void OnDataContextChanged(EventArgs e)
		{
			base.OnDataContextChanged(e);
			if (DataContext is LoginViewModel vm)
			{
				vm.LoginSucceeded += HandleLoginSucceeded;
			}
		}

		private void HandleLoginSucceeded()
		{
			LoginSucceeded?.Invoke();
			Close();
		}
	}
}