using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Vendors;
using System.Collections.Generic;

namespace Nop.Services.Customers
{
    /// <summary>
    /// Customer registration interface
    /// </summary>
    public partial interface ICustomerRegistrationService
    {
        /// <summary>
        /// Validate customer
        /// </summary>
        /// <param name="usernameOrEmail">Username or email</param>
        /// <param name="password">Password</param>
        /// <returns>Result</returns>
        CustomerLoginResults ValidateCustomer(string usernameOrEmail, string password);

        /// <summary>
        /// Register customer
        /// </summary>
        /// <param name="request">Request</param>
        /// <returns>Result</returns>
        CustomerRegistrationResult RegisterCustomer(CustomerRegistrationRequest request);

        /// <summary>
        /// Change password
        /// </summary>
        /// <param name="request">Request</param>
        /// <returns>Result</returns>
        PasswordChangeResult ChangePassword(ChangePasswordRequest request);

        /// <summary>
        /// Sets a user email
        /// </summary>
        /// <param name="customer">Customer</param>
        /// <param name="newEmail">New email</param>
        void SetEmail(Customer customer, string newEmail);

        /// <summary>
        /// Sets a customer username
        /// </summary>
        /// <param name="customer">Customer</param>
        /// <param name="newUsername">New Username</param>
        void SetUsername(Customer customer, string newUsername);


        /// <summary>
        /// Crea un usuario y la agrega los attributos que se envían
        /// </summary>
        /// <param name="customer">Datos del usuario</param>
        /// <param name="attributes">Attributos adicionales del usuario</param>
        /// <returns>true, proceso ejecutado. False si tiene error</returns>
        CustomerRegistrationResult Register(Customer customer, Dictionary<string, object> attributes, VendorType vendorType = VendorType.User, bool createPassword = false);
    }
}