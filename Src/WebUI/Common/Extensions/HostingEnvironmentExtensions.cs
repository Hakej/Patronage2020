using Microsoft.AspNetCore.Hosting;

namespace Patronage2020.WebUI.Common
{
    public static class HostingEnvironmentExtensions
    {
        public static bool IsDocker(this IWebHostEnvironment env)
        {
            return env.EnvironmentName == "Docker";
        }
    }
}
