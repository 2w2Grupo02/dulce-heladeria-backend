using Dulce.Heladeria.Models.BaseEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dulce.Heladeria.Models.Entities
{
    public class UserEntity : BaseEntity
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "First Name should be minimum 3 characters and a maximum of 100 characters")]
        [DataType(DataType.Text)]                                                                                       
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Last name is required")]                                                              
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Last Name should be minimum 3 characters and a maximum of 100 characters")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Dni is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "DNI should be minimum 3 characters and a maximum of 100 characters")]
        [DataType(DataType.Text)]
        public string Dni { get; set; }

        [Required(ErrorMessage = "email is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Last Name should be minimum 3 characters and a maximum of 100 characters")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "passwordhash is required")]
        public byte[] PasswordHash { get; set; }

        [Required(ErrorMessage = "passwordsalt is required")]
        public byte[] PasswordSalt { get; set; }

        [Required(ErrorMessage = "Rol is required")]
        public int Rol { get; set; }
    }
}
