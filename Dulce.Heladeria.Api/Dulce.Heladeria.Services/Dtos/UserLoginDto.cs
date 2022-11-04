using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dulce.Heladeria.Services.Dtos
{
    public class UserLoginDto
    {
        [Required(ErrorMessage = "User is required")]
        public string User { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
