namespace Lands.Domain
{
    public class Response
    {
        //devuelve verdadero o falso dependiendo de 
        //si la respuesta fue exitosa o no
        public bool IsSuccess
        {
            get;
            set;
        }

        // si devuelve false, se envía este Message
        public string Message
        {
            get;
            set;
        }

        // si devuelve verdadero, carga la lista
        public object Result
        {
            get;
            set;
        }

    }
}
