namespace Lands.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Lands.Models;
    using Lands.Services;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using Xamarin.Forms;

    class LandsViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion
         
        #region Attributes
        //se define la lista como ObservableCollection
        //porque se va a pintar en un listView
        private ObservableCollection<LandItemViewModel> lands;
        private bool isRefreshing;
        private string filter;
        #endregion

        #region Properties
        public ObservableCollection<LandItemViewModel> Lands
        {
            get { return this.lands; }
            set { SetValue(ref this.lands, value); }
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { SetValue(ref this.isRefreshing, value); }
        }

        public string Filter
        {
            get { return this.filter; }
            set
            {
                SetValue(ref this.filter, value);
                this.Search(); //esto sirve para que se ejecute la busqueda
                //automaticamente cada vez que se modifica algo en el campo
            }
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
            this.IsRefreshing = true; // activa la carga del listView
            // verifica la coneccion (esto hay que hacerlo
            //cada vez que se consuma un servicio
            var connection = await this.apiService.CheckConnection();
            // si no hay coneccion
            if (!connection.IsSuccess)
            {
                this.IsRefreshing = false; // desactiva la carga del listView

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
                this.IsRefreshing = false; // desactiva la carga del listView
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                return;
            }

            //Convertir la lista obtenida en un ObservableColection para poder verla
            MainViewModel.GetInstance().LandsList = (List<Land>)response.Result;
            //crear observableCollection
            this.Lands = new ObservableCollection<LandItemViewModel>(
                this.ToLandItemViewModel());
            this.IsRefreshing = false; // desactiva la carga del listView
        }
        #endregion


        #region Methods
        // este metodo sirve para convertir de Land a LandItemViewModel
        private IEnumerable<LandItemViewModel> ToLandItemViewModel()
        {
            return MainViewModel.GetInstance().LandsList.Select(l => new LandItemViewModel
            {
                Alpha2Code = l.Alpha2Code,
                Alpha3Code = l.Alpha3Code,
                AltSpellings = l.AltSpellings,
                Area = l.Area,
                Borders = l.Borders,
                CallingCodes = l.CallingCodes,
                Capital = l.Capital,
                Cioc = l.Cioc,
                Currencies = l.Currencies,
                Demonym = l.Demonym,
                Flag = l.Flag,
                Gini = l.Gini,
                Languages = l.Languages,
                Latlng = l.Latlng,
                Name = l.Name,
                NativeName = l.NativeName,
                NumericCode = l.NumericCode,
                Population = l.Population,
                Region = l.Region,
                RegionalBlocs = l.RegionalBlocs,
                Subregion = l.Subregion,
                Timezones = l.Timezones,
                TopLevelDomain = l.TopLevelDomain,
                Translations = l.Translations,
            });
        } 
        #endregion


        #region Commands
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadLands);
            }
         }

        public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand(Search);
            }
        }
        #endregion



        private void Search()
        {
            if(string.IsNullOrEmpty(this.Filter))
            {
                this.Lands = new ObservableCollection<LandItemViewModel>(
                this.ToLandItemViewModel());
            }
            else
            {
                this.Lands = new ObservableCollection<LandItemViewModel>(
                    this.ToLandItemViewModel().Where(l => l.Name.ToLower().Contains(this.Filter.ToLower()) ||
                                              l.Capital.ToLower().Contains(this.Filter.ToLower())));
                // cargamos la lista donde: el nombre (convertido a minuscula) contenga
                // lo introducido en el filtro (convertido a minuscula)
                // O la Capital (convertido a minuscula) contenga
                // lo introducido en el filtro (convertido a minuscula)
                //ToLower es para poder comparar lo que está y lo que se
                //busca sin tener problemas con las mayusculas
            }
        }
    }
}
