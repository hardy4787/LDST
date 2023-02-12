using LDST.Domain.EFModels;
using Microsoft.EntityFrameworkCore;

namespace LDST.Application.Interfaces.Persistance;

public interface IAppDbContext
{
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<PlaygroundEntity> Playgrounds { get; set; }
    public DbSet<BillEntity> Bills { get; set; }
    public DbSet<GameReservationEntity> GameReservations { get; set; }
    public DbSet<GameTimeSlotEntity> GameTimeSlots { get; set; }
    public DbSet<CityEnity> Cities { get; set; }
    public DbSet<CountryEntity> Countries { get; set; }
    public DbSet<SportEntity> Sports { get; set; }
    public DbSet<CitySportEntity> CitySports { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}

