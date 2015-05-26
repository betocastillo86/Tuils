using Nop.Core.Domain.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.Catalog
{
    public class ProductQuestionMap : NopEntityTypeConfiguration<ProductQuestion>
    {
        public ProductQuestionMap()
        {
            ToTable("ProductQuestion");

            this.Property(q => q.AnswerText).IsOptional().HasMaxLength(400);
            this.Property(q => q.QuestionText).HasMaxLength(400);
            this.Ignore(c => c.Status);
        }
    }
}
