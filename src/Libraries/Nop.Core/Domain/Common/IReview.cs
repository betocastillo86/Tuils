using Nop.Core.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Common
{
    public interface IReview
    {
        /// <summary>
        /// Gets or sets the customer identifier
        /// </summary>
        int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the content is approved
        /// </summary>
        bool IsApproved { get; set; }

        /// <summary>
        /// Gets or sets the title
        /// </summary>
        string Title { get; set; }

        /// <summary>
        /// Gets or sets the review text
        /// </summary>
        string ReviewText { get; set; }

        /// <summary>
        /// Review rating
        /// </summary>
        int Rating { get; set; }

        /// <summary>
        /// Review helpful votes total
        /// </summary>
        int HelpfulYesTotal { get; set; }

        /// <summary>
        /// Review not helpful votes total
        /// </summary>
        int HelpfulNoTotal { get; set; }

        /// <summary>
        /// Gets or sets the date and time of instance creation
        /// </summary>
        DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the product
        /// </summary>
        Customer Customer { get; set; }
    }
}
