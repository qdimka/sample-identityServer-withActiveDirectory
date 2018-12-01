using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Sample.Is4Ldap.Web.Identity.Models;

namespace Sample.Is4Ldap.Web.Identity
{
    public class LdapProfileService : IProfileService
    {
        private readonly IUserRepository<LdapUser> _userRepository;

        public LdapProfileService(IUserRepository<LdapUser> userRepository)
        {
            _userRepository = userRepository;
        }

        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject.GetSubjectId();

            var user = _userRepository.FindById(sub);

            var claims = new List<Claim>
            {
                new Claim("givenName", user.GivenName),
                new Claim("userName", user.SN),
            };

            context.IssuedClaims = claims;

            return Task.CompletedTask;
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = _userRepository.FindById(sub);

            context.IsActive = user != null;

            return Task.CompletedTask;
        }
    }
}
