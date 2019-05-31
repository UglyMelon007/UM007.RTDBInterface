using Microsoft.Extensions.Configuration;

namespace Utils.Attributes
{
    public static class GlobalAttributes
    {
        public static string RepositoryName { get; set; }
        public static IConfiguration Configuration { get; set; }
    }
}
