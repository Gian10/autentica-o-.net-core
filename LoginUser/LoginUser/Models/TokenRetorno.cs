﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginUser.Models
{
    public class TokenRetorno
    {
        public string Token { get; set; }
        public DateTime DataTokenGerado { get; set; }
    }
}
