using Microsoft.Extensions.Options;

namespace WebAPI.OptionsSetup.Jwt
{
    public class JwtOptionsSetup : IConfigureOptions<JwtOptions>
    {
        const string SectionName = "Jwt";
        readonly IConfiguration configuration;

        public JwtOptionsSetup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void Configure(JwtOptions options)
        {
            configuration.GetSection(SectionName).Bind(options);
        }
    }
}
