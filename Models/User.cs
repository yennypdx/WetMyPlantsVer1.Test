﻿using System.Collections.Generic;

namespace Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Hash { get; set; }
        public List<int> Plants { get; set; } = new List<int>();
    }
}
