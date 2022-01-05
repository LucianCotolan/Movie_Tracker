using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie_Tracker.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ReleaseYear { get; set; }
        public string Duration { get; set; }
        public string Poster { get; set; }
        public string Description { get; set; }
        public int DirectorId { get; set; }
        public Director Director { get; set; }
        public ICollection<ActorRole> Roles { get; set; }
        public ICollection<WatchedMovie> WatchedMovies { get; set;}
    }
}
