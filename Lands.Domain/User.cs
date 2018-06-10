namespace Lands.Domain
{
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class User
    {
        [Key] // campo key autoincrementable
        public int UserId { get; set; }

        [Display(Name = "First Name")] // Esto es para que se muestre el nombre Fist Name
        // Esto indica que el campo es requerido y el error dice que el campo {Name} es requerido
        [Required(ErrorMessage = "The field {0} is requiered.")]
        // Esto indica el tamaño maximo de caracteres. (50) en este caso.
        // el error dice que el campo {Name} solo puede tener un maximo de {MaLenght} caracteres.
        [MaxLength(50, ErrorMessage = "The field {0} only can contains a maximum of {1} characters lenght.")]
        public string FirstName { get; set; } // campo del nombre

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "The field {0} is requiered.")]
        [MaxLength(50, ErrorMessage = "The field {0} only can contains a maximum of {1} characters lenght.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "The field {0} is requiered.")]
        [MaxLength(100, ErrorMessage = "The field {0} only can contains a maximum of {1} characters lenght.")]
        // esto significa que no hay dos usuarios con el mismo email
        [Index("User_Email_Index", IsUnique = true)] 
        [DataType(DataType.EmailAddress)]
        //cada vez que se cree un usuario, se crea en la tabla y en la tabla de seguridad de asp.net
        // Lo ligamos por el campo email
        public string Email { get; set; } 

        [MaxLength(20, ErrorMessage = "The field {0} only can contains a maximum of {1} characters lenght.")]
        [DataType(DataType.PhoneNumber)]
        public string Telephone { get; set; }

        [Display(Name = "Image")] 
        public string ImagePath { get; set; }

        [Display(Name = "Image")]
        public string ImageFullPath
        {
            get
            {
                if (string.IsNullOrEmpty(ImagePath))
                {
                    return "noimage";
                }

                return string.Format(
                    "http://landsapi1.azurewebsites.net/{0}",
                    ImagePath.Substring(1));
            }
        }

        [Display(Name = "User")]
        public string FullName // esta propieda junta el FistName con el LastName
        {
            get
            {
                return string.Format("{0} {1}", this.FirstName, this.LastName);
            }
        }
    }
}
