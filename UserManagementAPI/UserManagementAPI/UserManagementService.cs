using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagementAPI.Models;


namespace UserManagementAPI
{
    public class UserManagementService : IUserService
    {

        private readonly List<User> _user;


        public UserManagementService()
        {

            _user = new List<User>()
            {
                new User{ID=1, FirstName="John", LastName="Doe", IsActive=true},
                new User{ID=2, FirstName="Sam", LastName="Willson", IsActive=true},
                new User{ID=3, FirstName="Tim", LastName="Miller", IsActive=true},
                new User{ID=4, FirstName="Sara", LastName="Kelly", IsActive=true},
                new User{ID=5, FirstName="Jen", LastName="Jhonson", IsActive=true},
                new User{ID=6, FirstName="Tom", LastName="Henkins", IsActive=true},
                new User{ID=7, FirstName="Chris", LastName="Brown", IsActive=true}
            };
        }

        public User Add(User user)
        {
            _user.Add(user);
            return user;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _user;
        }

        public User GetById(int id)
        {
            return _user.Where(x => x.ID == id)
                .FirstOrDefault();
        }

        public void Remove(int id)
        {
            var userToBeDeleted = _user.First(x => x.ID == id);
            _user.Remove(userToBeDeleted);
        }

    }
}
