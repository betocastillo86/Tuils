using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Web.Models.Api
{
    public class ProductReviewModel
    {
        /// <summary>
        /// Gets or sets the customer identifier
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the product identifier
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the content is approved
        /// </summary>
        public bool IsApproved { get; set; }

        /// <summary>
        /// Gets or sets the title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the review text
        /// </summary>
        public string ReviewText { get; set; }

        /// <summary>
        /// Review rating
        /// </summary>
        public int Rating { get; set; }

        
        public string CustomerName { get; set; }

        public string ProductName { get; set; }

        public long CreatedOnUtcTicks { get; set; }

        public DateTime CreatedOnUtc { get; set; }
    }
}