using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dulce.Heladeria.Services.Dtos
{
    public class CreateUserDto
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Dni { get; set; }
        public string Email { get; set; }

        [Required(ErrorMessage = "password is required")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "password should be minimum 8 characters and a maximum of 100 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public int Rol { get; set; }
    }
}
