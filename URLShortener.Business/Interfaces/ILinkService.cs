using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLShortener.Data.Entities;

namespace URLShortener.Business.Interfaces
{
    public interface ILinkService
    {
        public Task CreateLinkAsync(string originalLink);
        public Task<string> GetOriginalLinkByShortenedURLAsync(string shortenedURL);
        public Task<Link> GetLinkByIdAsync(int id);
        public Task DeleteLinkByIdAsync(int id);
    }
}
