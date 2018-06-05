namespace Lands.ViewModels
{
    using Lands.Models;
    using Lands.Services;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Xamarin.Forms;

    class LandsViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion
         
        #region Attributes
        //se define la lista como ObservableCollection
        //porque se va a pintar en un listView
        private ObservableCollection<Land> lands;
        #endregion

        #region Properties
        public ObservableCollection<Land> Lands
        {
            get { return this.lands; }
            set { SetValue(ref this.lands, value); }
        }
        #endregion

        #region Constructors
        public LandsViewModel()
        {
            //los servicios tienen que ser instanciados
            //en el constructor, por lo que instanciamos
            // this.apiService
            this.apiService = new ApiService();
            this.LoadLands();
        }
        #endregion

        #region Methods
        //aquí pintaremos los paises
        // al ser asincronos los metodos que vamos a usar
        // tenemos que marcar el metodo como async
        private async void LoadLands()
        {
            // verifica la coneccion (esto hay que hacerlo
            //cada vez que se consuma un servicio
            var connection = await this.apiService.CheckConnection();
            // si no hay coneccion
            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    connection.Message,
                    "Accept");
                //esto hace que vuelva hacia la pestaña anterior
                await Application.Current.MainPage.Navigation.PopAsync();
                return;
            }
            
            //para cargar las lands hay que instanciar el servicio
            var response = await this.apiService.GetList<Land>(
                "http://restcountries.eu",
                "/rest",
                "/v2/all");

            // si dio algun error
            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                return;
            }

            //Convertir la lista obtenida en un ObservableColection para poder verla
            var list = (List<Land>)response.Result;
            //crear observableCollection
            this.Lands = new ObservableCollection<Land>(list);
        } 
        #endregion
    }
}
