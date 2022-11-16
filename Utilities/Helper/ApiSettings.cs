using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Helper
{
    public class ApiSettings
    {
        public string Secret { get; set; }

        // refresh token expiry time(in days)
        public int RefreshTokenExpiryTime { get; set; }

    }
}
