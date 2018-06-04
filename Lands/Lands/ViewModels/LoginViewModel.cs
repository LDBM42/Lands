namespace Lands.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using System;
    using System.ComponentModel;
    using System.Windows.Input;
    using Xamarin.Forms;

    class LoginViewModel : BaseViewModel
    {

        #region Atributes
        private string password;
        private bool isRunning;
        private bool isEnabled;
        #endregion

        #region Properties
        public string Email { get; set; }
        public string Password
        {
            get { return this.password; }
            set { SetValue(ref this.password, value); }
        }
        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }
        public bool IsRemembered { get; set; }
        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { SetValue(ref this.isEnabled, value); }
        }
        #endregion

        #region Constructors
        public LoginViewModel()
        {
            this.IsRemembered = true;
            this.IsEnabled = true;
        }
        #endregion

        #region Commands
        public ICommand LoginCommand
        {
            get
            {
                return new RelayCommand(Login);
            }
        }


        private async void Login() // los metodos async necesitan un await para hacer una pausar al ejecutarse
        {
            if (string.IsNullOrEmpty(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error", // Titulo del Error
                    "You mustt enter an email", // Mensage
                    "Accept"); // Nombre del botón

                return;
            }

            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error", // Titulo del Error
                    "You mustt enter a Password", // Mensage
                    "Accept"); // Nombre del botón

                return;
            }

            this.IsRunning = true; //activar la ruedita de carga
            this.IsEnabled = false; // desactivar controles con esta propiedad

            if (this.Email != "david_bm42@hotmail.com" || this.Password != "1234")
             {
                this.IsRunning = false; // volvemos a desabilitarlo
                this.IsEnabled = true; // volvemos a activar los controles

                await Application.Current.MainPage.DisplayAlert(
                    "Error", // Titulo del Error
                    "Email or Password incorrect.", // Mensage
                    "Accept"); // Nombre del botón

                this.Password = string.Empty;
                return;
            }

            this.IsRunning = false; // volvemos a desabilitarlo
            this.IsEnabled = true; // volvemos a activar los controles

            await Application.Current.MainPage.DisplayAlert(
                    "Ok", // Titulo del Error
                    "Todo está Bien", // Mensage
                    "Accept"); // Nombre del botón
        }
        #endregion
    }
}
