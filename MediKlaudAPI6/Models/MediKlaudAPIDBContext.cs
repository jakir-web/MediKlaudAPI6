using Microsoft.EntityFrameworkCore;

namespace MediKlaudAPI6.Models
{

  public class MediKlaudAPIDBContext : DbContext, IMediKlaudAPIDBContext
    {
        public MediKlaudAPIDBContext(DbContextOptions<MediKlaudAPIDBContext> options) : base(options)
        {

        }
        public DbSet<MemberMaster> MemberMasters { get; set; }
        public DbSet<UserAuthen> UserAuthens { get; set; }
    }
}
