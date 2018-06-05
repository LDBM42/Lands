namespace Lands.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using System;
    using System.ComponentModel;
    using Views;
    using System.Windows.Input;
    using Xamarin.Forms;

    class LoginViewModel : BaseViewModel
    {

        #region Atributes
        private string email;
        private string password;
        private bool isRunning;
        private bool isEnabled;
        #endregion

        #region Properties
        public string Email
        {
            get { return this.email; }
            set { SetValue(ref this.email, value); }
        }
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

            this.Email = "david_bm42@hotmail.com";
            this.Password = "1234";

            /* http://restcountries.eu/rest/v2/all */
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
                    "You must enter an email", // Mensage
                    "Accept"); // Nombre del botón

                return;
            }

            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error", // Titulo del Error
                    "You must enter a Password", // Mensage
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

            this.Email = string.Empty;
            this.Password = string.Empty;

            // para mostrar una nueva pestaña hay que instanciarla usando el metodo Singleton
            MainViewModel.GetInstance().Lands = new LandsViewModel();
            //despues hay que hacer un PushAsync para mostrar esa pestaña
            await Application.Current.MainPage.Navigation.PushAsync(new LandsPage());
        }
        #endregion
    }
}
