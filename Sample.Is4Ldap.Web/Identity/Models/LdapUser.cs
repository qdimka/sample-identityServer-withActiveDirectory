using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.Is4Ldap.Web.Identity.Models
{
    public class LdapUser
    {
        public string Uid { get; set; }

        public string DN { get; set; }

        public string GivenName { get; set; }

        public string SN { get; set; }
    }
}
