﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Domain.Enums;

namespace WebAPI.OptionsSetup.Authorization
{
    public class AuthorizationOptionsSetup : IConfigureOptions<AuthorizationOptions>
    {
        public const string UnverifiedCustomer = "UnverifiedCustomer";
        public const string VerifiedCustomer = "VerifiedCustomer";
        public const string CourtAdministrator = "CourtCreator";

        public void Configure(AuthorizationOptions options)
        {
            options.AddPolicy(UnverifiedCustomer, policy =>
            {
                policy.RequireClaim("Role", UserRole.Customer.ToString());
                policy.RequireClaim("Status", UserStatus.Unverified.ToString());
            });

            options.AddPolicy(VerifiedCustomer, policy =>
            {
                policy.RequireClaim("Role", UserRole.Customer.ToString());
                policy.RequireClaim("Status", UserStatus.Active.ToString());
            });

            options.AddPolicy(CourtAdministrator, policy =>
            {
                policy.RequireClaim("Role", UserRole.SystemAdmin.ToString(), UserRole.CourtManager.ToString());
            });
        }
    }
}
