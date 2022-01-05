using Movie_Tracker.Areas.Identity.Data;

namespace Movie_Tracker.Models
{
    public class WatchedMovie
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public string ApplicationUserId { get; set; }

        public Movie Movie { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
