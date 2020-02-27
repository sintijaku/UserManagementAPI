using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagementAPI.Models;

namespace UserManagementAPI
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers();
        User Add(User user);
        User GetById(int id);
        void Remove(int id);


    }
}
