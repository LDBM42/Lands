namespace Lands.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using System;
    using System.ComponentModel;
    using Views;
    using Services;
    using System.Windows.Input;
    using Xamarin.Forms;


    class LoginViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

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
            //  instanciacion del apiservice
            this.apiService = new ApiService();

            this.IsRemembered = true;
            this.IsEnabled = true;

            // esto es temporal
            this.Email = "juan@gmail.com";
            this.Password = "123456";
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

            //validar si hay conexion
            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                this.IsRunning = false; // volvemos a desabilitarlo
                this.IsEnabled = true; // volvemos a activar los controles

                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    connection.Message,
                    "Accept");
                return;
            }

            // para conseguir el token
            //1er parametro: UrlBase del Api, 2do: UserName, 3ro: Password
            var token = await this.apiService.GetToken(
                "http://landsapi1.azurewebsites.net", 
                this.Email, 
                this.Password);

            if (token == null)
            {
                this.IsRunning = false; // volvemos a desabilitarlo
                this.IsEnabled = true; // volvemos a activar los controles

                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Something was wrong, please try later.",
                    "Accept");
                return;
            }

            if (string.IsNullOrEmpty(token.AccessToken))
            {
                this.IsRunning = false; // volvemos a desabilitarlo
                this.IsEnabled = true; // volvemos a activar los controles

                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    token.ErrorDescription,
                    "Accept");
                this.Password = string.Empty;
                return;
            }

            //esto es un apuntador para no tener que estar
            //escribiendo MainViewModel.GetInstance()
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Token = token;
            // para mostrar una nueva pestaña hay que instanciarla usando el metodo Singleton
            mainViewModel.Lands = new LandsViewModel();
            //despues hay que hacer un PushAsync para mostrar esa pestaña
            await Application.Current.MainPage.Navigation.PushAsync(new LandsPage());


            this.IsRunning = false; // volvemos a desabilitarlo
            this.IsEnabled = true; // volvemos a activar los controles

            this.Email = string.Empty;
            this.Password = string.Empty;

        }
        #endregion
    }
}
