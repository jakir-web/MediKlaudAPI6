namespace MediKlaudAPI6.Infrastructure
{
    public interface IMediklaudDBConnection
    {
        Task<string> getDBConn();
    }
}
