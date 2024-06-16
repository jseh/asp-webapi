
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace WebApi_JWT
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);



            // Add services to the container.


            // Para validar el jwt con authorize

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]!)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = false
                };
            });


            // jwt policies

            builder.Services.AddAuthorization(x =>
            {
                x.AddPolicy("OnlyAdmin", p => p.RequireClaim("Rol", "Admin") );


                // x.AddPolicy("GetConfigurationPolicy", p => p.Requirements.Add(new SameCountryRequirement()));


                x.AddPolicy(
                    "CanAccessVIPArea",
                    p => p.RequireAssertion(
                        context =>
                            {
                                /*
                                if (context.User != null && context.User?.Identity?.IsAuthenticated == true)
                                {
                                    var ucc = context.User.Claims;
                                }
                                */

                                if (context.User?.Identity?.IsAuthenticated == true)
                                {
                                    var ucc = context.User.Claims;
                                }


                                //var u = context.User.Claims.Where(c => c.Type == "Color").Select(c => c.Value).SingleOrDefault();

                                return true;
                            } 
                        )
                );



            });





            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication(); // Para hacer funcionar JWt
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
