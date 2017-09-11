using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_EmojiAPI.Models
{
    public class LoginParameters
    {
        [Required(ErrorMessage ="Please enter Username")]
        public string Username { get; set; }
        [Required(ErrorMessage="Please enter Password")]
        public string Password { get; set; }
    }

    public class Images
    {
        public string URL { get; set; }
    }
}