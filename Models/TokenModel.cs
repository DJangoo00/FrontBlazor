using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontBlazor.Models
{
    public class TokenModel
    {
        public int id { get; set; }
        public string message { get; set; }
        public bool isAuthenticated { get; set; }
        public string userName { get; set; }
        public string email { get; set; }
        public List<string> roles { get; set; }
        public string token { get; set; }
        public DateTime refreshTokenExpiration { get; set; }
    }
}
