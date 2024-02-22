using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLShortener.Business.Interfaces;
using URLShortener.Data.Entities;
using URLShortener.Data.Interfaces;

namespace URLShortener.Business.Services
{
    public class LinkService: ILinkService
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
            var link = new Link
            {
                OriginalURL = originalLink,
                ShortenedURL = GenerateShortCode(originalLink),
                UserId = _tokenService.GetUserIdFromToken()
            };
            await _linkRepository.CreateLinkAsync(link);
        }

        private string GenerateShortCode(string originalUrl)
        {
            byte[] hashBytes = System.Security.Cryptography.MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(originalUrl));
            string base64String = Convert.ToBase64String(hashBytes);
            base64String.TrimEnd('=').Replace("/", "_").Replace("+", "-");

            return base64String;
        }

        public async Task<string> GetOriginalLinkByShortenedURLAsync(string shortenedURL)
        {
            return await _linkRepository.GetOriginalLinkByShortenedURLAsync(shortenedURL);
        }

        public async Task<Link> GetLinkByIdAsync(int id)
        {
            return await _linkRepository.GetLinkByIdAsync(id);
        }

        public async Task DeleteLinkByIdAsync(int id)
        {
            await _linkRepository.DeleteLinkByIdAsync(id);
        }
    }
}
