using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.Is4Ldap.Web.Identity
{
    public interface IUserRepository<out T>
    {
        T FindByName(string name);

        T FindById(string uid);

        T FindByCredentials(string userName, string password);
    }
}
