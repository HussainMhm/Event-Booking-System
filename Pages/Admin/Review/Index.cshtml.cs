using MetaX.Data;
using MetaX.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace MetaX.Pages.Admin.Review
{
    public class IndexModel : PageModel
    {
        private readonly MetaxDbContext _db;

        public List<ReviewDetails> Reviews { get; set; }

        public IndexModel(MetaxDbContext db)
        {
            _db = db;
        }

        public async Task OnGetAsync()
        {
            Reviews = await GetLatestReviewDetails();
        }

        public async Task<List<ReviewDetails>> GetLatestReviewDetails()
        {
            var latestReviews = await _db.ReviewsTable
                .OrderByDescending(r => r.ReviewID)
                .Take(3)
                .ToListAsync();

            var reviewDetails = new List<ReviewDetails>();

            foreach (var review in latestReviews)
            {
                var user = await _db.UsersTable.FindAsync(review.UserID);
                var eventName = (await _db.EventsTable.FindAsync(review.EventID)).Title;

                var reviewDetail = new ReviewDetails
                {
                    ReviewID = review.ReviewID,
                    UserName = $"{user.Name} {user.Surname}",
                    EventName = eventName,
                    Rating = review.Rating,
                    Comment = review.Comment
                };

                reviewDetails.Add(reviewDetail);
            }

            return reviewDetails;
        }

        public IActionResult OnPostDelete(int reviewId)
        {
            var review = _db.ReviewsTable.Find(reviewId);
            if (review != null)
            {
                _db.ReviewsTable.Remove(review);
                _db.SaveChanges();
            }
            return RedirectToPage();
        }

        public class ReviewDetails
        {
            public int ReviewID { get; set; }
            public string UserName { get; set; }
            public string EventName { get; set; }
            public int Rating { get; set; }
            public string Comment { get; set; }
        }
    }
}
