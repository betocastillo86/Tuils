using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Catalog
{
    public enum QuestionStatus
    {
        /// <summary>
        /// Pregunta recien creada
        /// </summary>
        Created = 1,

        /// <summary>
        /// Pregunta respondida
        /// </summary>
        Answered = 2,

        /// <summary>
        /// Pregunta eliminada
        /// </summary>
        Deleted = 0
    }
}
