using System;
using System.Collections.Generic;
using Dapper;
using StarterKit.Data.Config;
using StarterKit.Data.Models;


namespace StarterKit.Data.Repository
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository()
        {
        }

        public User getUserByEmailPass(User _user)
        {
            try
            {
                var lookup = new Dictionary<int, User>();
                var LoggedUser = conn.QueryFirstOrDefault<User>(@"SELECT * FROM [User] WHERE email = '" + _user.Email + "' and pass = '" + _user.Pass + "'");
                return LoggedUser;
            }
            catch (Exception ex)
            {
                return null;
            }
           

        }
        public User getUserByEmail(User _user)
        {
            try
            {
                var lookup = new Dictionary<int, User>();
                var LoggedUser = conn.QueryFirstOrDefault<User>(@"SELECT * FROM [User] WHERE email = '" + _user.Email + "'");
                return LoggedUser;
            }
            catch (Exception ex)
            {
                return null;
            }


        }
    }
}
