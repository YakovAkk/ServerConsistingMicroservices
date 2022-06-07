﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketData.Data.Models
{
    public class UserModel
    {
        public string? Id { get; set; }
        public string NickName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public DateTime DataRegistration { get; set; }
        public string? MessageThatWrong { get; set; }

        public UserModel()
        {
            DataRegistration = DateTime.Now;
        }
    }
}