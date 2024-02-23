using URLShortener.Data.Entities;

namespace URLShortener.Data.Interfaces
{
    public interface ILinkRepository
    {
        public Task CreateLinkAsync(Link link);
        public Task<string> GetOriginalLinkByShortenedURLAsync(string shortenedURL);
        public Task<Link> GetLinkByIdAsync(int id);
        public Task<Link> GetLinkByOriginalURLAsync(string originalURL);
        public Task<IEnumerable<Link>> GetAllLinksAsync();
        public Task DeleteLinkByIdAsync(int id);
    }
}
