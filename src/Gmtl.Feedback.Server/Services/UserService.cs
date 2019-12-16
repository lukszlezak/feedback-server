using System;
using System.Collections.Generic;
using System.Linq;
using Gmtl.Feedback.Server.Data;
using Gmtl.Feedback.Server.Models;

namespace Gmtl.Feedback.Server.Services
{
    public class UserService
    {
        private readonly FeedbackDbContext context;

        public UserService(FeedbackDbContext context)
        {
            this.context = context;
        }

        public User AuthenticateUser(string login, string password)
        {
            return context.Users.FirstOrDefault(u => u.Login == login && u.Password == password);
        }

        public bool AddUser(User user)
        {
            context.Add(user);
            context.SaveChanges();

            return true;
        }

        public bool IsLoginFree(string login)
        {
            return context.Users.FirstOrDefault(u => u.Login == login) == null;
        }
    }
}