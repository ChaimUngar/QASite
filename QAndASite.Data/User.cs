﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QAndASite.Data
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }

        public List<Question> Questions { get; set; }
        public List<Answer> Answers { get; set; }
    }
}
