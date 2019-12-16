using System;

namespace Gmtl.Feedback.Server.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}