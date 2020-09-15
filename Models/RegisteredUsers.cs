﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace projectWEB.Models
{
    public enum MemberType
    {
        BasicUser,
        SalesPerson,
        Supervisor,
        Admin
    }
    public class RegisteredUsers
    {
        public int id { get; set; }
        
        [Required(ErrorMessage = "This field is required")]
        [MinLengthAttribute(3)]
        [MaxLength(15)]
        public string UserName { get; set; }

        
        [MinLengthAttribute(8)]
        [Required(ErrorMessage = "This field is required")]
        public string Password { get; set; }

        [Required(ErrorMessage ="This field is required")]
        [MinLengthAttribute(8)]
        [MaxLength(30)]
       
        public string Email { get; set; }

        private string _Password { get; set; }

        
        public  MemberType MemberType { get; set; }


    }
}
