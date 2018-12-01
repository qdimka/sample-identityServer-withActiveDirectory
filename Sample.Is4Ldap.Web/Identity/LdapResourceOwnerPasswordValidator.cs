using System;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Validation;
using Sample.Is4Ldap.Web.Identity.Models;

namespace Sample.Is4Ldap.Web.Identity
{
    public class LdapResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IUserRepository<LdapUser> _userRepository;

        public LdapResourceOwnerPasswordValidator(IUserRepository<LdapUser> userRepository)
        {
            _userRepository = userRepository;
        }

        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var user = _userRepository.FindByCredentials(context.UserName, context.Password);
            if (user != null)
            {
                context.Result = new GrantValidationResult(user.Uid, OidcConstants.AuthenticationMethods.Password);
            }

            return Task.FromResult(0);
        }
    }
}
