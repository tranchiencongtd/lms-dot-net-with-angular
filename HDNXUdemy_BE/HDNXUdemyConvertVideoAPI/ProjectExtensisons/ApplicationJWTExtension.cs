using HDNXUdemyModel.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace HDNXUdemyConvertVideoAPI.ProjectExtensisons
{
    /// <summary>
    /// ApplicationJWTExtensions
    /// </summary>
    public static class ApplicationJWTExtensions
    {
        /// <summary>
        /// CustomerApplicationJWTExtension
        /// </summary>
        /// <param name="services"></param>
        public static void CustomerApplicationJWTExtension(this IServiceCollection services)
        {
            var key = Encoding.ASCII.GetBytes("AudioStoryAPI_20231028");

            services.AddAuthentication(a =>
            {
                a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(a =>
            {
                a.RequireHttpsMetadata = false;
                a.SaveToken = true;
                a.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            }).AddCookie(c =>
            {
                c.Cookie.HttpOnly = true;
                c.ExpireTimeSpan = TimeSpan.FromDays(ProjectConfig.ExpiresDate);
                c.SlidingExpiration = true;
            });
        }
    }
}