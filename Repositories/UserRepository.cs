using System;
using System.Collections.Generic;
using System.Data;
using burger_shack.Models;
using Dapper;

namespace burger_shack.Repositories
{
    public class UserRepository
    {
        private readonly IDbConnection _db;

        public UserRepository(IDbConnection db)
        {
            _db = db;
        }

        public UserReturnModel Register(UserCreateModel userData)
        {
            Guid g = Guid.NewGuid();
            string id = g.ToString();
            User user = new User()
            {
                Id = id,
                Name = userData.Name,
                Email = userData.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(userData.Password),
            };
            var success = _db.Execute(@"
            INSERT INTO users(
                id,
                name,
                email,
                password
            ) Values (
                @Id,
                @Name,
                @Email,
                @Password
            )
            ", user);
            if (success < 1)
            {
                throw new Exception("EMAIL IN USE");
            }
            return new UserReturnModel()
            {
                Name = user.Name,
                Email = user.Email,
                Id = user.Id
            };
        }

        public UserReturnModel Login(UserLoginModel userData)
        {
            User user = _db.QueryFirstOrDefault<User>(@"
                SELECT * FROM users WHERE email = @Email
                ", userData);
            Boolean valid = BCrypt.Net.BCrypt.Verify(userData.Password, user.Password);
            if (valid)
            {
                return new UserReturnModel()
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email
                };
            }
            return null;
        }

        public UserReturnModel GetUserById(string id)
        {
            User user = _db.QueryFirstOrDefault<User>(@"
            SELECT * FROM users WHERE id = @Id
            ", new User { Id = id });
            // if (user == null) { throw new Exception("Something really bad happened"); }
            return new UserReturnModel()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            };
        }

        internal UserReturnModel UpdateAccount(UserReturnModel user, UserReturnModel userData)
        {
            var i = _db.Execute(@"
            UPDATE users SET
            email = @Email
            name = @Name
            WHERE id = @Id
            ", userData);
            if (i > 0)
            {
                return user;
            }
            return null;
        }
    }
}