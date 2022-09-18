using CardStorageService.Data;

namespace SecureDevelopment.Services
{
    public interface ICardRepositoryService : IRepository<Card, string>
    {
        IList<Card> GetByClientId(string id);
    }
}
