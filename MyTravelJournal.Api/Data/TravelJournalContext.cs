using Microsoft.EntityFrameworkCore;
using MyTravelJournal.Api.Models;

namespace MyTravelJournal.Api.Data;

public class TravelJournalContext : DbContext
{
    public TravelJournalContext(DbContextOptions<TravelJournalContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseSerialColumns();
    }

    public DbSet<Trip> Trips { get; set; }
}