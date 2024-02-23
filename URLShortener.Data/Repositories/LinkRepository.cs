using Microsoft.EntityFrameworkCore;
using URLShortener.Data.Entities;
using URLShortener.Data.Interfaces;

namespace URLShortener.Data.Repositories
{
    public class LinkRepository: ILinkRepository
    {
        private readonly URLShortenerDbContext _context;

        public LinkRepository(URLShortenerDbContext context)
        {
            _context = context;
        }

        public async Task CreateLinkAsync(Link link)
        {
            await _context.Link.AddAsync(link);
            await _context.SaveChangesAsync();
        }

        public async Task<string> GetOriginalLinkByShortenedURLAsync(string shortenedURL)
        {
            var link = await _context.Link.FirstOrDefaultAsync(l => l.ShortenedURL == shortenedURL);

            if(link == null)
            {
                  return null;
            }

            return link.OriginalURL;
        }

        public async Task<Link> GetLinkByIdAsync(int id)
        {
            var link = await _context.Link.FirstOrDefaultAsync(l => l.Id == id);

            return link;
        }

        public async Task<Link> GetLinkByOriginalURLAsync(string originalURL)
        {
            var link = await _context.Link.FirstOrDefaultAsync(l => l.OriginalURL == originalURL);

            return link;
        }

        public async Task<IEnumerable<Link>> GetAllLinksAsync()
        {
            return await _context.Link.ToListAsync();
        }

        public async Task DeleteLinkByIdAsync(int id)
        {
            var link = await _context.Link.FirstOrDefaultAsync(l => l.Id == id);

            if (link != null)
            {
                _context.Link.Remove(link);
            }

            await _context.SaveChangesAsync();
        }
    }
}
