using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using AddToMyCart.DomainModels;

namespace AddToMyCart.Repositories
{
    public interface IUsersRepository
    {
        void InsertUser(User user);
        void UpdateUserDetails(User user);
        void UpdateUserPassword(User user);
        List<User> GetUsers();
        List<User> GetUsersByEmailAndPassword(string email, string password);
        List<User> GetUsersByEmail(string email);
        List<User> GetUsersByUserID(int UserID);
        int GetLatestUserID();
        void DeleteUser(int userID);
    }


    public class UsersRepository : IUsersRepository
    {
        AddToMyWebCartDbContext db;

        public UsersRepository()
        {
            db = new AddToMyWebCartDbContext();
        }

        public void DeleteUser(int userID)
        {
            User user = db.Users.Where(u => u.UserID == userID).SingleOrDefault();
            if (user != null)
            {
                db.Users.Remove(user);
                db.SaveChanges();
            }
        }

        public int GetLatestUserID()
        {
            int uid = db.Users.Select(u => u.UserID).Max();
            return uid;
        }

        public List<User> GetUsers()
        {
            return db.Users.OrderBy(u => u.UserID).ToList();
        }

        public List<User> GetUsersByEmail(string email)
        {
            List<User> users = db.Users.Where(u => u.Email == email).ToList();
            return users;
        }

        public List<User> GetUsersByEmailAndPassword(string email, string password)
        {
            List<User> users = db.Users.Where(u => u.Email == email && u.PasswordHash == password).ToList();
            return users;
        }

        public List<User> GetUsersByUserID(int UserID)
        {
            List<User> users = db.Users.Where(u => u.UserID == UserID).ToList();
            return users;
        }

        public void InsertUser(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();
        }

        public void UpdateUserDetails(User user)
        {
            User u = db.Users.Where(x => x.UserID == user.UserID).SingleOrDefault();
            if (u != null)
            {
                u.FirstName = user.FirstName;
                u.LastName = user.LastName;
                u.Mobile = user.Mobile;
                u.ProfilePicture = user.ProfilePicture;
                db.SaveChanges();
            }
        }

        public void UpdateUserPassword(User user)
        {
            User u = db.Users.Where(x => x.Email == user.Email).SingleOrDefault();
            if (u != null)
            {
                u.PasswordHash = user.PasswordHash;
                db.Users.Add(u);
                db.SaveChanges();
            }
        }
    }
}
