using Application.Utilities.OptionsSetup;
using Microsoft.Extensions.Options;

namespace WebAPI.OptionsSetup.MoMo
{
    public class MoMoOptionsSetup : IConfigureOptions<MoMoOptions>
    {
        const string SectionName = "MoMoConfig";
        readonly IConfiguration configuration;

        public MoMoOptionsSetup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void Configure(MoMoOptions options)
        {
            configuration.GetSection(SectionName).Bind(options);
        }
    }
}
