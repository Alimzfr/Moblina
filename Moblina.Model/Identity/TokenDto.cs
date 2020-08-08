using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Moblina.Model.Identity
{
    public class TokenDto
    {
        [Required]
        public string RefreshToken { get; set; }
    }
}
