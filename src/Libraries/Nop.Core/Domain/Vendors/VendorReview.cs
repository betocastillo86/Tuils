using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Vendors
{
    /// <summary>
    /// Represents a vendor review
    /// </summary>
    public partial class VendorReview : BaseEntity, IReview
    {
        private ICollection<VendorReviewHelpfulness> _vendorReviewHelpfulnessEntries;

        /// <summary>
        /// Gets or sets the customer identifier
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the product identifier
        /// </summary>
        public int VendorId { get; set; }

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

        /// <summary>
        /// Review helpful votes total
        /// </summary>
        public int HelpfulYesTotal { get; set; }

        /// <summary>
        /// Review not helpful votes total
        /// </summary>
        public int HelpfulNoTotal { get; set; }

        /// <summary>
        /// Gets or sets the date and time of instance creation
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the product
        /// </summary>
        public virtual Customer Customer { get; set; }

        /// <summary>
        /// Gets the product
        /// </summary>
        public virtual Vendor Vendor { get; set; }


        /// <summary>
        /// Gets the entries of product review helpfulness
        /// </summary>
        public virtual ICollection<VendorReviewHelpfulness> VendorReviewHelpfulnessEntries
        {
            get { return _vendorReviewHelpfulnessEntries ?? (_vendorReviewHelpfulnessEntries = new List<VendorReviewHelpfulness>()); }
            protected set { _vendorReviewHelpfulnessEntries = value; }
        }
    }
}
