using Microsoft.EntityFrameworkCore;

namespace BlazorApp1_Backend.Data
{
    public class NotepadDbContext(DbContextOptions<NotepadDbContext> options) : DbContext(options)
    {
        public DbSet<Notepad> Notepads => Set<Notepad>();
    }
}
