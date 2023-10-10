using Microsoft.EntityFrameworkCore;

namespace MediKlaudAPI6.Models
{
    public interface IMediKlaudAPIDBContext
    {
        DbSet<MemberMaster> MemberMasters { get; set; }
        DbSet<UserAuthen> UserAuthens { get; set; }
    }
}
