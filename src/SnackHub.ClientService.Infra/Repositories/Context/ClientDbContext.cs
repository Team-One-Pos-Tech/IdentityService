using Microsoft.EntityFrameworkCore;

namespace SnackHub.ClientService.Infra.Repositories.Context;

public class ClientDbContext : DbContext
{
    public ClientDbContext(DbContextOptions<ClientDbContext> dbContextOptions)
        : base(dbContextOptions)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClientDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}