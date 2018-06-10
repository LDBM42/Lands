namespace Lands.Backend.Models
{
    using Domain; // User
    using System.ComponentModel.DataAnnotations; // par todo el codigo dentro de la clase
    using System.ComponentModel.DataAnnotations.Schema; // NotMapped

    [NotMapped] // esto es para que el EntityFramework
    //no se use es el UserView en como base de datos
    public class UserView : User 
    {
        //ESTO ES PARA CREAR ESTOS DOS CAMPOS EN EL USER    

        //DataType es para que nos e vea el password digitado
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(20, ErrorMessage = "The length for field {0} must be betwen {1} and {2} characters", MinimumLength = 6)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "The field {0} is required")]
        //Compare es para comparar el PasswordConfig con el campo "Password"
        // si no son iguales no funciona
        [Compare("Password", ErrorMessage = "The password and confirm does not match")]
        [Display(Name = "Password confirm")]
        public string PasswordConfirm { get; set; }
    }
}