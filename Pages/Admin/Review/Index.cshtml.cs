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

        public async Task OnGetAsync(string eventName, int? rating)
        {
            Reviews = await GetLatestReviewDetails(eventName, rating);
        }

        public async Task<List<ReviewDetails>> GetLatestReviewDetails(string eventName, int? rating)
        {
            var reviewsQuery = _db.ReviewsTable.AsQueryable();

            if (rating.HasValue)
            {
                reviewsQuery = reviewsQuery.Where(r => r.Rating == rating.Value);
            }

            var latestReviews = await reviewsQuery
                .OrderByDescending(r => r.ReviewID)
                .ToListAsync();

            var reviewDetails = new List<ReviewDetails>();

            foreach (var review in latestReviews)
            {
                var user = await _db.UsersTable.FindAsync(review.UserID);
                var eventtName = (await _db.EventsTable.FindAsync(review.EventID)).Title;

                var reviewDetail = new ReviewDetails
                {
                    ReviewID = review.ReviewID,
                    UserName = $"{user.Name} {user.Surname}",
                    EventName = eventtName,
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
