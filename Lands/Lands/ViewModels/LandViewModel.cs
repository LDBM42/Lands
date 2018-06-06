namespace Lands.ViewModels
{
    using Models;
    using System;
    using System.Collections.ObjectModel; //using del ObservableCollection
    using System.Linq;

    public class LandViewModel : BaseViewModel
    {

        #region Attributes
        // un atributo ObservableCollection de la clase Border
        private ObservableCollection<Border> borders;
        private ObservableCollection<Currency> currencies;
        private ObservableCollection<Language> languages;
        #endregion


        #region Propperties
        public Land Land { get; set; }

        public ObservableCollection<Border> Borders
        {
            get { return this.borders; }
            set { SetValue(ref this.borders, value); }
        }
        public ObservableCollection<Currency> Currencies
        {
            get { return this.currencies; }
            set { SetValue(ref this.currencies, value); }
        }
        public ObservableCollection<Language> Languages
        {
            get { return this.languages; }
            set { SetValue(ref this.languages, value); }
        }
        #endregion


        #region Constructors 
        public LandViewModel(Land land)
        {
            this.Land = land;
            this.LoadBorders();
            //convirtiendo una lista en una observable collection
            this.Currencies = new ObservableCollection<Currency>(this.Land.Currencies);
            this.Languages = new ObservableCollection<Language>(this.Land.Languages);
        }
        #endregion

        #region Methods
        private void LoadBorders()
        {
            // colección vacía de Border
            this.Borders = new ObservableCollection<Border>();

            foreach (var border in this.Land.Borders)
            {
                /*Recorremos las iniciales de Border que vienen en Lands.
                 Buscamos la inicial en la lista que está en la MainViewModel de todos
                 los paises y si la encontramos la adicionamos (add) como Border.
                 (ESTO ES PARA BUSCAR CADA INICIAL DE LOS BORDES Y CAMBIARLO POR EL
                 CODIGO Y NOMBRE DE CADA UNO DE ESOS BORDES).*/
                var land = MainViewModel.GetInstance().LandsList.
                                         Where(l => l.Alpha3Code == border).
                                         FirstOrDefault();

                // si lo encuentra, le agrega un nuevo
                //objeto de Border con el nombre y el
                // codigo.
                if (land != null)
                {
                    this.Borders.Add(new Border
                    {
                        Code = land.Alpha3Code,
                        Name = land.Name
                    });
                }
            }
        } 
        #endregion
    }
}
