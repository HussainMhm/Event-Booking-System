using System;
using Microsoft.EntityFrameworkCore;

namespace MetaX.Data
{
	public class MetaxDbContext: DbContext
	{
		public MetaxDbContext(DbContextOptions options) : base(options)
		{ }
	}

	//public DbSet<> MyProperty { get; set; }
}

