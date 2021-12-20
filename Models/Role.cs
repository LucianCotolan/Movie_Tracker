using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie_Tracker.Models
{
    public class Role
    {
        public int RoleId { get; set; }
        public int MovieId { get; set; }
        public int ActorId { get; set; }
        public string RoleName { get; set; }
        
        public Movie Movie { get; set; }
        public Actor Actor { get; set; }
    }
}
