using MetaX.Data;
using MetaX.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetaX.Pages
{
    public class IndexModel : PageModel
    {
        private readonly MetaxDbContext _db;

        public List<Model.Event> EarlyEvents { get; set; }
        public List<string> PopulerKategoriIds { get; set; }
        public List<ReviewDetails> Reviews { get; set; }

        public IndexModel(MetaxDbContext db)
        {
            _db = db;
        }

        public async Task OnGetAsync()
        {
            EarlyEvents = await _db.EventsTable
                .Where(e => e.Date.Date >= DateTime.Now.Date)
                .OrderBy(e => e.Date)
                .Take(3)
                .ToListAsync();

            PopulerKategoriIds = await _db.EventsTable
                .GroupBy(e => e.Category)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .Take(4)
                .ToListAsync();

            Reviews = await GetLatestReviewDetails();
        }

        public async Task<List<ReviewDetails>> GetLatestReviewDetails()
        {
            var latestReviews = await _db.ReviewsTable
                .OrderByDescending(r => r.ReviewID)
                .Take(5)
                .ToListAsync();

            var reviewDetails = new List<ReviewDetails>();

            foreach (var review in latestReviews)
            {
                var user = await _db.UsersTable.FindAsync(review.UserID);
                var eventName = (await _db.EventsTable.FindAsync(review.EventID)).Title;

                var reviewDetail = new ReviewDetails
                {
                    UserID = review.UserID,
                    UserName = $"{user.Name} {user.Surname}",
                    EventName = eventName,
                    Rating = review.Rating,
                    Comment = review.Comment
                };

                reviewDetails.Add(reviewDetail);
            }

            return reviewDetails;
        }
    }

    public class ReviewDetails
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string EventName { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}
