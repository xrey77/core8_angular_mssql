using System;
using System.Collections.Generic;
using System.Linq;
using core8_angular_mssql.Entities;
using core8_angular_mssql.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace core8_angular_mssql.Services
{
    public interface IUserService {
        IEnumerable<User> GetAll();
        User GetById(int id);
        void UpdateProfile(User user);
        void Delete(int id);
        void ActivateMfa(int id, bool opt, string qrcode_url);
        void UpdatePicture(int id, string file);
        void UpdatePassword(User user, string password = null);
    }

    public class UserService : IUserService
    {
        private DataDbContext _context;
        private readonly AppSettings _appSettings;

         IConfiguration config = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .AddEnvironmentVariables()
        .Build();

        public UserService(DataDbContext context,IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }

        public void Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
            else {
               throw new AppException("User not found");
            }   
        }

        public IEnumerable<User> GetAll()
        {
            var users = _context.Users.ToList();
            return users;

            // throw new NotImplementedException();
        }

        public User GetById(int id)
        {
                var user = _context.Users.Find(id);
                if (user == null) {
                    throw new AppException("User does'not exists....");
                }
                return user;
        }

        public void UpdateProfile(User userParam)
        {
            var user = _context.Users.Find(userParam.Id);
            if (user == null)
                throw new AppException("User not found");

            if (!string.IsNullOrWhiteSpace(userParam.FirstName)) {
                user.FirstName = userParam.FirstName;
            }

            if (!string.IsNullOrWhiteSpace(userParam.LastName)) {
                user.LastName = userParam.LastName;
            }

            if (!string.IsNullOrWhiteSpace(userParam.Mobile)) {
                user.Mobile = userParam.Mobile;
            }

            DateTime now = DateTime.Now;
            user.UpdatedAt = now;
            _context.Users.Update(user);
            _context.SaveChanges();            
        }

        public void UpdatePassword(User userParam, string password = null)
        {
            var user = _context.Users.Find(userParam.Id);
            if (user == null)
                throw new AppException("User not found");

            if (!string.IsNullOrWhiteSpace(userParam.Password))
            {
                 user.Password = BCrypt.Net.BCrypt.HashPassword(userParam.Password);

            }
            DateTime now = DateTime.Now;
            user.UpdatedAt = now;
            _context.Users.Update(user);
            _context.SaveChanges();            
        }


        public void ActivateMfa(int id, bool opt, string qrcode_url)
        {
           var user = _context.Users.Find(id);
            if (user != null)
            {
                if (opt == true ) {

                    user.Qrcodeurl = qrcode_url;
                } else {
                    user.Qrcodeurl = null;
                }
                _context.Users.Update(user);
                _context.SaveChanges();
            }
            else {
               throw new AppException("User not found");
            }                    }

        public void UpdatePicture(int id, string file)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                user.Profilepic = file;
                _context.Users.Update(user);
                _context.SaveChanges();
            }
            else {
               throw new AppException("User not found");
            }                    
        }
    }

}