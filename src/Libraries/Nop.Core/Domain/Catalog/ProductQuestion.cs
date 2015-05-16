using Nop.Core.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Catalog
{
    public class ProductQuestion : BaseEntity
    {
        public int ProductId { get; set; }

        public int CustomerId { get; set; }
        
        public string QuestionText { get; set; }

        public int StatusId { get; set; }

        public QuestionStatus Status { get { return (QuestionStatus)StatusId; } set { StatusId = (int)value; } }

        public int? CustomerAnswerId { get; set; }

        public string AnswerText { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        public DateTime? AnsweredOnUtc { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Customer  CustomerAnswer { get; set; }

        public virtual Product Product { get; set; }

    }

}
