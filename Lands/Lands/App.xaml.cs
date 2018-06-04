using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Lands.Views;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace Lands
{
	public partial class App : Application
	{

        #region Constructor
        public App ()
		{
			InitializeComponent();

			this.MainPage = new NavigationPage(new LoginPage());
            //NavigationPage sirve para que la pagina se apile en un tap o pestaña
		}
        #endregion


        #region Methods
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        } 
        #endregion
    }
}
