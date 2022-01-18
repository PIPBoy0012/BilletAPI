using System;
using BilletAPI.Models.Repository;
using System.Collections.Generic;
using System.Linq;

namespace BilletAPI.Models.DataManager
{
    public class UserManager : IDataRepository<User>
    {
        readonly BilletSystemAPIContext _skpDbContext;

        public UserManager(BilletSystemAPIContext sKPDbContext)
        {
            _skpDbContext = sKPDbContext;
        }

        public IEnumerable<User> GetAll()
        {
            return _skpDbContext.Users.ToList();
        }

        public User Get(long id)
        {
            return _skpDbContext.Users.FirstOrDefault(e => e.UserId == id);
        }

        public void Add(User user)
        {
            _skpDbContext.Add(user);
            _skpDbContext.SaveChanges();
        }


        //Figure out how to create the login for user with API.
        /*public void Login(string email, string password)
        {

        }*/

        public void Update(User userToUpdate, User user)
        {
            userToUpdate.Username = user.Username;
            userToUpdate.Email = user.Email;
            userToUpdate.Password = user.Password;

            _skpDbContext.SaveChanges();
        }

        public void Delete(User user)
        {
            _skpDbContext.Remove(user);
            _skpDbContext.SaveChanges();
        }
    }
}
