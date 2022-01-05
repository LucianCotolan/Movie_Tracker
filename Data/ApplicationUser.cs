using Microsoft.AspNetCore.Identity;
using Movie_Tracker.Models;
using System.Collections.Generic;

namespace Movie_Tracker.Areas.Identity.Data
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<WatchedMovie> WatchedMovies { get; set; }
    }
}
