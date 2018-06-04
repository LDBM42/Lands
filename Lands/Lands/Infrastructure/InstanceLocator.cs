// PATRON DE DISEÑO LLAMADO LOCATOR
//Esto nos sirve para poder ligar la la pag login a la MainViewModel

namespace Lands.Infrastructure
{
    using ViewModels;

    class InstanceLocator
    {
        //Referencia a un objeto llamado Main,
        //que es un objeto de tipo MainViewModel
        #region Properties
        public MainViewModel Main
        {
            get;
            set;
        }
        #endregion

        #region Constructors
        public InstanceLocator()
        {
            //Instanciamos el objeto Main en el 
            //constuctor del InstanceLocator
            this.Main = new MainViewModel();
        }
        #endregion

        //Todo esto significa que cuando se ejecute la app
        //pasará por el diccionario de recursos del App.xaml primero.
    }
}
