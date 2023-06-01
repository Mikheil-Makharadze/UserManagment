using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using UserManagment.Core.Entities;

namespace UserManagment.infrastructure.Data.DB
{
    public class AppDbContextSeed
    {
        public static async Task SeedUsersAndProfilesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                try
                {
                    var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();
                    var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                    if (!context.Users.Any() && !context.UserProfiles.Any())
                    {
                        var userProfiles = new List<UserProfile>
                        {

                            new UserProfile { FirstName = "Mikheil", LastName = "Makharadze", PersonalNumber = "61001082000" },
                            new UserProfile { FirstName = "John", LastName = "Doe", PersonalNumber = "12345678901" },
                            new UserProfile { FirstName = "Jane", LastName = "Smith", PersonalNumber = "98765432109" },
                            new UserProfile { FirstName = "example", LastName = "example", PersonalNumber = "11111111111" }
                        };

                        foreach (var userProfile in userProfiles)
                        {
                            var user = new User
                            {
                                UserName = userProfile.FirstName,
                                Email = userProfile.FirstName + "@example.com"
                            };

                            await userManager.CreateAsync(user, "Coding@1234?");

                            userProfile.UserId = user.Id;

                            context.UserProfiles.Add(userProfile);
                        }

                        await context.SaveChangesAsync();
                    }
                }
                catch (Exception ex)
                {
                    Exception innerException = ex.InnerException;

                }
            }
        }
    }
}
