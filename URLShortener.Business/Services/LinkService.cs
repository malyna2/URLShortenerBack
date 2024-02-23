using System.Text;
using URLShortener.Business.Interfaces;
using URLShortener.Data.Entities;
using URLShortener.Data.Interfaces;

namespace URLShortener.Business.Services
{
    public class LinkService : ILinkService
    {
        private readonly ILinkRepository _linkRepository;
        private readonly ITokenService _tokenService;

        public LinkService(ILinkRepository linkRepository, ITokenService tokenService)
        {
            _linkRepository = linkRepository;
            _tokenService = tokenService;
        }

        public async Task CreateLinkAsync(string originalLink)
        {
            if (await _linkRepository.GetLinkByOriginalURLAsync(originalLink) != null)
            {
                throw new InvalidDataException("Link already exists");
            }
            var link = new Link
            {
                OriginalURL = originalLink,
                ShortenedURL = GenerateShortCode(originalLink),
                UserId = _tokenService.GetUserIdFromToken(),
                CreatedAt = DateTime.Now
            };
            await _linkRepository.CreateLinkAsync(link);
        }

        public async Task<string> GetOriginalLinkByShortenedURLAsync(string shortenedURL)
        {
            var originalLink = await _linkRepository.GetOriginalLinkByShortenedURLAsync(shortenedURL);

            if (originalLink == null)
            {
                throw new NullReferenceException("Link not found");
            }

            return originalLink;
        }

        public async Task<Link> GetLinkByIdAsync(int id)
        {
            var link = await _linkRepository.GetLinkByIdAsync(id);

            if (link == null)
            {
                throw new NullReferenceException("Link not found");
            }

            return link;
        }

        public async Task<IEnumerable<Link>> GetAllLinksAsync()
        {
            return await _linkRepository.GetAllLinksAsync();
        }

        public async Task DeleteLinkByIdAsync(int id)
        {
            var link = await _linkRepository.GetLinkByIdAsync(id);

            if (link == null)
            {
                throw new NullReferenceException("Link not found");
            }

            if (_tokenService.GetUserIdFromToken() == link.UserId || _tokenService.IsAdminFromToken())
            {
                await _linkRepository.DeleteLinkByIdAsync(id);
            }
            else
            {
                throw new UnauthorizedAccessException("You don't have permisssion to delete this link");
            }
        }

        private string GenerateShortCode(string originalUrl)
        {
            byte[] hashBytes = System.Security.Cryptography.SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(originalUrl));
            string base64String = Convert.ToBase64String(hashBytes);
            base64String = base64String.TrimEnd('=').Replace("/", "_").Replace("+", "-");

            return base64String.Substring(0, 8);
        }
    }
}
