using System;
using MetaX.Model;
using Microsoft.EntityFrameworkCore;

namespace MetaX.Data
{
	public class MetaxDbContext: DbContext
	{

		public MetaxDbContext(DbContextOptions options) : base(options)
		{

		}

        public DbSet<Event> EventsTable { get; set; }
        public DbSet<Reservation> ReservationsTable { get; set; }
        public DbSet<Review> ReviewsTable { get; set; }
        public DbSet<User> UsersTable { get; set; }
        public DbSet<User> CategoriesTable { get; set; }

    }

	
}