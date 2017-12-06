using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database
{
    public partial class User
    {
        public User()
        {
            Routes = new HashSet<Route>();
            UserPlaces = new HashSet<UserPlace>();
        }
        
        public int Id { get; private set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int? Age { get; set; }
        public string Tags { get; set; }

        public ICollection<Route> Routes { get; set; }
        public ICollection<UserPlace> UserPlaces { get; set; }
    }
}
