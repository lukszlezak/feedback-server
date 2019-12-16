using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gmtl.Feedback.Server.ViewModels
{
    public class FeedbackListViewModel : BaseViewModel
    {
        public List<Models.Feedback> Feedbacks { get; set; }
        public string SearchPattern { get; set; }
        public string SearchSignature { get; set; }
        public Guid DomainId { get; set; }
        public DateTime CreateDate { get; set; }
        public string DomainName { get; set; }
    }
}