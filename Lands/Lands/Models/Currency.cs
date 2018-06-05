namespace Lands.Models
{
    using Newtonsoft.Json;

    public class Currency
    {
        //esto sirve para mantener el estandar de iniciar con mayuscula
        //internamente usa el nombre de la propiedad (osea, como yo la voy a manejar)
        //, pero se recibe usa el del codigo JsonProperty
        // de esta manera podemos manejar el codigo con el nombre que querramos.
        [JsonProperty(PropertyName = "code")]
        public string Code { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "symbol")]
        public string Symbol { get; set; }
    }
}
