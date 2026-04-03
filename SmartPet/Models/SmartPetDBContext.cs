using System.Data.Entity;

namespace SmartPet.Models
{
	public class SmartPetDbContext : DbContext
	{
		public SmartPetDbContext() : base("name=SmartPetConnection")
		{
		}

		public DbSet<User> Users { get; set; }
		public DbSet<Pet> Pets { get; set; }
		public DbSet<Vaccines> Vaccines { get; set; }
		public DbSet<ReportRequest> ReportRequests { get; set; }
		public DbSet<Activity> Activities { get; set; }
		public DbSet<FeedingLogs> FeedingLogs { get; set; }
		public DbSet<Medication> Medications { get; set; }
		public DbSet<MedicationLogs> MedicationLogs { get; set; }
		public DbSet<Note> Notes { get; set; }
		public DbSet<Reminder> Reminders { get; set; }
	}
}