

namespace Lands.ViewModels
{
    class MainViewModel
    {
        #region ViewModels
        public LoginViewModel Login { get; set; }
        public LandsViewModel Lands { get; set; }
        public LandViewModel Land { get; set; }
        #endregion

        //al login ser la pagina inicial hay que instanciarla en el constructor
        #region Constructors
        public MainViewModel()
        {
            instance = this; // esto es parte del metodo Singleton
            this.Login = new LoginViewModel();
        }
        #endregion

        //permite hacer un llamado de la MainViewModel desde cualquier
        //clase sin necesidad de tener que instanciar otra MainViewModel
        #region Singleton
        private static MainViewModel instance;
        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }
            return instance;
        }
        #endregion
    }
}
