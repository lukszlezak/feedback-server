using System;
using System.Collections.Generic;

namespace Gmtl.Feedback.Server.Models
{
    /// <summary>
    /// This class represents web domain
    /// </summary>
    public class Domain
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual List<Feedback> Feedbacks { get; set; }
    }
}