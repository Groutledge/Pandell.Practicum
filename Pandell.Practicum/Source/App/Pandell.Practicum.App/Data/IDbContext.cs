using Microsoft.EntityFrameworkCore;
using Pandell.Practicum.App.Domain;

namespace Pandell.Practicum.App.Data
{
    public interface IDbContext
    {
        DbSet<RandomSequence> RandomSequences { get; set; }
    }
}