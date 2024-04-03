﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QAndASite.Data
{
    public class Answer
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int UserId { get; set; }
        public int QuestionId { get; set; }
        public DateTime DatePosted { get; set; }

        public User User { get; set; }
    }
}