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
        public DbSet<Category> CategoriesTable { get; set; }


        //Fixing Entity Framework Validation 30000 No Type Specified for the Decimal Column
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Event>()
                .Property(p => p.Price)
                .HasColumnType("decimal");
        }
    }
}