using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Sample.Is4Ldap.Web.Identity;
using Sample.Is4Ldap.Web.Identity.Models;
using Sample.Is4Ldap.Web.Logic;

namespace Sample.Is4Ldap.Web
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IUserRepository<LdapUser>, LdapUserRepository>();

            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryClients(InMemory.Clients)
                .AddInMemoryIdentityResources(InMemory.IdentityResources)
                .AddProfileService<LdapProfileService>()
                .AddResourceOwnerValidator<LdapResourceOwnerPasswordValidator>();

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}
