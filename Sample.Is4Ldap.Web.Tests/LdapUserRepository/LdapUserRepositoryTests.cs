using Sample.Is4Ldap.Web.Identity;
using Sample.Is4Ldap.Web.Identity.Models;
using Xunit;

namespace Sample.Is4Ldap.Web.Tests.LdapUserRepository
{
    //Test LdapServer in docker
    //docker pull rroemhild/test-openldap
    //docker run --privileged -d -p 389:389 rroemhild/test-openldap
    public class LdapUserRepositoryTests
    {
        private readonly IUserRepository<LdapUser> _userRepository;

        public LdapUserRepositoryTests()
        {
            _userRepository = new Identity.LdapUserRepository();
        }

        [Fact(DisplayName = "Получение пользователя по имени")]
        public void FindByName_Should_ReturnLdapUserByName()
        {
            var name = "Farnsworth";

            var user = _userRepository.FindByName(name);

            Assert.NotNull(user);
            Assert.Equal(name, user.SN);
            Assert.NotNull(user.GivenName);
            Assert.NotNull(user.Uid);
            Assert.NotNull(user.DN);
        }

        [Fact(DisplayName = "Получение пользователя по uid")]
        public void FindById_Should_ReturnLdapUserByUid()
        {
            var uid = "professor";

            var user = _userRepository.FindById(uid);

            Assert.NotNull(user);
            Assert.Equal(uid, user.Uid);
        }

        [Fact(DisplayName = "Получение пользователя по имени и паролю")]
        public void FindByCredentials_Should_ReturnLdapUserByUserCreds()
        {
            var userName = "Fry";
            var password = "{ssha}wL/Tm0HsZyOt+ocmykSotRJTFw3wFJ9dehE8xQ==";

            var user = _userRepository.FindByCredentials(userName, password);

            Assert.NotNull(user);
            Assert.Equal(userName, user.SN);
        }
    }
}
