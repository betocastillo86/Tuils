using Nop.Core.Domain.Orders;

namespace Nop.Services.Payments
{
    /// <summary>
    /// Represents a PostProcessPaymentRequest
    /// </summary>
    public partial class PostProcessPaymentRequest
    {
        /// <summary>
        /// Gets or sets an order. Used when order is already saved (payment gateways that redirect a customer to a third-party URL)
        /// </summary>
        public Order Order { get; set; }

        /// <summary>
        /// Fue necesario agregar la firma en este punto ya que el referenceCode es el numero de la 
        /// Orden y hasta que esta no este creado este valor no se puede actualizar
        /// </summary>
        public string Signature { get; set; }

        /// <summary>
        /// Fue necesario agregar la firma en este punto ya que el referenceCode es el numero de la 
        /// Orden y hasta que esta no este creado este valor no se puede actualizar
        /// </summary>
        public string ReferenceCode { get; set; }
    }
}
