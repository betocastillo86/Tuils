using System;
using System.Runtime.Serialization;

namespace Nop.Core
{
    /// <summary>
    /// Represents errors that occur during application execution
    /// </summary>
    [Serializable]
    public class NopException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the Exception class.
        /// </summary>
        public NopException()
        {

        }

        public NopException(CodeNopException code)
        {
            this.Code = code;
        }

        public NopException(CodeNopException code, string message) : base(message)
        {
            this.Code = code;
        }

        /// <summary>
        /// Codigo relacionado con la excepcion que se desea mostrar
        /// </summary>
        public CodeNopException Code { get; set; }

        /// <summary>
        /// Initializes a new instance of the Exception class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public NopException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the Exception class with a specified error message.
        /// </summary>
		/// <param name="messageFormat">The exception message format.</param>
		/// <param name="args">The exception message arguments.</param>
        public NopException(string messageFormat, params object[] args)
			: base(string.Format(messageFormat, args))
		{
		}

        /// <summary>
        /// Initializes a new instance of the Exception class with serialized data.
        /// </summary>
        /// <param name="info">The SerializationInfo that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The StreamingContext that contains contextual information about the source or destination.</param>
        protected NopException(SerializationInfo
            info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// Initializes a new instance of the Exception class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public NopException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }

    public enum CodeNopException
    { 
        None = 0,

        #region Publish
        /// <summary>
        /// Cuando un usuario intenta publicar un producto que no le está permitido hacerlo
        /// Ej: Un usuario simple intentar vender un servicio
        /// </summary>
        UserTypeNotAllowedPublishProductType = 100,
        /// <summary>
        /// Categoría en la que se está intentando crear un prodcuto no existe
        /// </summary>
        CategoryDoesntExist = 101,

        /// <summary>
        /// El vendedor a alcanzado el limite de productos permitidos para vender
        /// </summary>
        UserHasReachedLimitOfProducts = 102,


        #endregion

        #region User
        HasSessionActive = 201,
        #endregion

        #region GetPlans
        CustomerHasOrderPending = 301,

        #endregion


    }
}
