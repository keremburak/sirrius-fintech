using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sirrius.WebUI.Models
{
    public class DefaultProfile
    {
        public string User { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
    }

    public class AppSettings
    {
        public const string SectionName = nameof(AppSettings);

        public string Version { get; set; }
        public string App { get; set; }
        public string AppName { get; set; }
        public string AppFlavor { get; set; }
        public string AppFlavorSubscript { get; set; }
        public string Logo { get; set; }
        public string ApiBaseURL { get; set; }
        public string ClientSecret { get; set; }
        public int RefreshTokenExpiryTime { get; set; }
        public DefaultProfile  DefaultProfile{ get; set; }
    }
}
