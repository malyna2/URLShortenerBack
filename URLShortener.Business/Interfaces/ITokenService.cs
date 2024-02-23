using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLShortener.Data.Entities;

namespace URLShortener.Business.Interfaces
{
    public interface ITokenService
    {
        public string CreateToken(User user);
        public bool ValidateToken(string token);
        public int GetUserIdFromToken();
        public bool IsAdminFromToken();
    }
}
