using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddToMyCart.Repositories;
using AddToMyCart.DomainModels;
using AddToMyCart.ViewModels;
using AutoMapper;
using AutoMapper.Configuration;
using System.Web;

namespace AddToMyCart.ServiceLayer
{
    public interface IUsersService
    {
        int InsertUser(RegisterViewModel user);
        void UpdateUserDetails(EditUserDetailsViewModel user);
        void UpdateUserPassword(EditUserPasswordViewModel user);
        List<UserViewModel> GetUsers();
        UserViewModel GetUsersByEmailAndPassword(string email, string password);
        UserViewModel GetUsersByEmail(string email);
        UserViewModel GetUsersByUserID(int UserID);
        void DeleteUser(int userID);
    }

    public class UsersService : IUsersService
    {
        IUsersRepository ur;

        public UsersService()
        {
            ur = new UsersRepository();
        }

        public void DeleteUser(int userID)
        {
            ur.DeleteUser(userID);
        }

        public List<UserViewModel> GetUsers()
        {
            List<User> users = ur.GetUsers();
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<User, UserViewModel>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            List<UserViewModel> u = mapper.Map<List<User>, List<UserViewModel>>(users);
            return u;
        }

        public UserViewModel GetUsersByEmail(string email)
        {
            User users = ur.GetUsersByEmail(email).SingleOrDefault();
            UserViewModel uvm = null;
            if(users != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<User, UserViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                uvm = mapper.Map<User, UserViewModel>(users);
            }
            return uvm;
        }

        public UserViewModel GetUsersByEmailAndPassword(string email, string password)
        {
            User users = ur.GetUsersByEmailAndPassword(email,SHA256HashGenerator.GenerateHash(password)).SingleOrDefault();
            UserViewModel uvm = null;
            if(users != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<User, UserViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                uvm = mapper.Map<User, UserViewModel>(users);
            }
            return uvm;
        }

        public UserViewModel GetUsersByUserID(int UserID)
        {
            User users = ur.GetUsersByUserID(UserID).SingleOrDefault();
            UserViewModel uvm = null;
            if(users != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<User, UserViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                uvm = mapper.Map<User, UserViewModel>(users);
            }
            return uvm;
        }

        public int InsertUser(RegisterViewModel rvm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<RegisterViewModel, User>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            User u = mapper.Map<RegisterViewModel, User>(rvm);
            u.PasswordHash = SHA256HashGenerator.GenerateHash(rvm.Password);
            ur.InsertUser(u);
            int uid = ur.GetLatestUserID();
            return uid;
        }

        public void UpdateUserDetails(EditUserDetailsViewModel user)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<EditUserDetailsViewModel, User>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            User u = mapper.Map<EditUserDetailsViewModel, User>(user);
            ur.UpdateUserDetails(u);
        }


        public void UpdateUserPassword(EditUserPasswordViewModel user)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<EditUserPasswordViewModel, User>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            User u = mapper.Map<EditUserPasswordViewModel, User>(user);
            u.PasswordHash = SHA256HashGenerator.GenerateHash(user.Password);
            ur.UpdateUserPassword(u);
        }

        
    }
}
