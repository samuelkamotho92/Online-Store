﻿namespace Online_Store.Extensions
{
    public static class AddPolicies
    {

        public static WebApplicationBuilder AddAdminPolicy(this WebApplicationBuilder builder)
        {

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminPolicy", options =>
                {
                    options.RequireAuthenticatedUser();
                    options.RequireClaim("Roles", "Admin");
                });
            });

            return builder;
        }
    }
}
