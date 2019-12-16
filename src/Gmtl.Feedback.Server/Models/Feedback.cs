using System;

namespace Gmtl.Feedback.Server.Models
{
    /// <summary>
    /// collects feedback from the users
    /// </summary>
    public class Feedback
    {
        public Guid Id { get; set; }
        public Guid DomainId { get; set; }
        public string Message { get; set; }
        public string Signature { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual Domain Domain { get; set; }
    }
}