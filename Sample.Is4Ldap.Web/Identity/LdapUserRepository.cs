using System;
using System.Collections.Generic;
using Novell.Directory.Ldap;
using Sample.Is4Ldap.Web.Identity.Models;

namespace Sample.Is4Ldap.Web.Identity
{
    public class LdapUserRepository : IUserRepository<LdapUser>
    {
        private readonly string _hostName = "localhost";

        private readonly LdapConnection _ldapConnection;

        public LdapUserRepository()
        {
            _ldapConnection = new LdapConnection()
            {
                SecureSocketLayer = false
            };
            _ldapConnection.Connect(_hostName, 389);
            _ldapConnection.Bind("cn=admin,dc=planetexpress,dc=com", "GoodNewsEveryone");
        }

        public LdapUser FindByName(string name)
        {
            if(!_ldapConnection.Connected)
                throw new InvalidOperationException(nameof(_ldapConnection));

            var searchBase = "ou=people,dc=planetexpress,dc=com";
            var filter = $"(sn={name})";

            return Search(searchBase, filter);
        }

        public LdapUser FindById(string uid)
        {
            if (!_ldapConnection.Connected)
                throw new InvalidOperationException(nameof(_ldapConnection));

            var searchBase = "ou=people,dc=planetexpress,dc=com";
            var filter = $"(uid={uid})";

            return Search(searchBase, filter);
        }

        public LdapUser FindByCredentials(string userName, string password)
        {
            var user = FindByName(userName);

            if (user == null)
                return null;

            var passwordAttribute = new LdapAttribute("userPassword", password);
            var isValid = _ldapConnection.Compare(user.DN, passwordAttribute);

            if (!isValid)
                return null;

            return user;
        }

        private LdapUser Search(string searchBase, string filter)
        {
            var search = _ldapConnection.Search(searchBase, LdapConnection.SCOPE_SUB, filter, null, false);

            LdapUser user = null;

            while (search.HasMore())
            {
                var nextEntry = search.Next();
                user = new LdapUser
                {
                    SN = nextEntry.getAttribute("sn").StringValue,
                    Uid = nextEntry.getAttribute("uid").StringValue,
                    GivenName = nextEntry.getAttribute("givenName").StringValue,
                    DN = nextEntry.DN
                };
            }

            return user;
        }
    }
}
