using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gmtl.Feedback.Server.Models
{
    public enum Status
    {
        Valid = 0,
        Invalid = 1
    }

    public class ApiKey
    {
        public Guid Id { get; set; }
        public Guid DomainId { get; set; }
        public string KeyValue { get; set; }
        public DateTime CreateDate { get; set; }
        public Status Status { get; set; }
    }
}
