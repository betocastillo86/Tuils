using Nop.Core;
using Nop.Services.Customers;
using Nop.Web.Models.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Nop.Core.Domain.Customers;
using Nop.Web.Extensions.Api;
using Nop.Services.Authentication;
using Nop.Services.Logging;
using Nop.Services.Localization;

namespace Nop.Web.Controllers.Api
{
    [Route("api/auth")]
    public class AuthenticationController : ApiController
    {
        #region Fields
        private readonly ICustomerService _customerService;
        private readonly IWorkContext _workContext;
        private readonly CustomerSettings _customerSettings;
        private readonly ICustomerRegistrationService _customerRegistrationService;
        private readonly IAuthenticationService _authenticationService;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly ILocalizationService _localizationService;
        #endregion

        #region Ctor

        public AuthenticationController(ICustomerService customerService,
            IWorkContext workContext,
            CustomerSettings customerSettings,
            ICustomerRegistrationService customerRegistrationService,
            IAuthenticationService authenticationService,
            ICustomerActivityService customerActivityService,
            ILocalizationService localizationService)
        {
            this._customerService = customerService;
            this._workContext = workContext;
            this._customerRegistrationService = customerRegistrationService;
            this._customerSettings = customerSettings;
            this._authenticationService = authenticationService;
            this._customerActivityService = customerActivityService;
            this._localizationService = localizationService;
        }
        #endregion
            
        [HttpPost]
        [Route("api/auth/register")]
        public IHttpActionResult Register(CustomerBaseModel model)
        {
            //Si hay un usuario autenticado no permite la creación
            if (_workContext.CurrentCustomer.IsRegistered())
            {
                ModelState.AddModelError("errorCode", Convert.ToInt32(CodeNopException.HasSessionActive).ToString());
                ModelState.AddModelError("errorMessage", CodeNopException.HasSessionActive.GetLocalizedEnum(_localizationService, _workContext));
                return BadRequest(ModelState); 
            }
                

            if (ModelState.IsValid)
            {
                //Convierte a entidad e intenta realizar el registro
                var attributes = new Dictionary<string, object>();
                var entityCustomer = model.ToEntity(out attributes);
                var result = _customerRegistrationService.Register(entityCustomer, attributes, model.VendorType);
                if (result.Success)
                {
                    //Si el registro es exitoso se autentca
                    _authenticationService.SignIn(entityCustomer, true);
                    return Ok(new { Email = model.Email, Name = entityCustomer.GetFullName() });
                }
                else
                {
                    return BadRequest(result.Errors.FirstOrDefault());
                }
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpPost]
        [Route("api/auth")]
        public IHttpActionResult Login(CustomerBaseModel model)
        {
            //Si hay un usuario autenticado no permite la creación
            if (_workContext.CurrentCustomer.IsRegistered())
            {
                ModelState.AddModelError("errorCode", Convert.ToInt32(CodeNopException.HasSessionActive).ToString());
                ModelState.AddModelError("errorMessage", CodeNopException.HasSessionActive.GetLocalizedEnum(_localizationService, _workContext));
                return BadRequest(ModelState);
            }

            if (!string.IsNullOrEmpty(model.Email) && !string.IsNullOrEmpty(model.Password))
            {
                var loginResult = _customerRegistrationService.ValidateCustomer(model.Email, model.Password);
                switch (loginResult)
                {
                    case CustomerLoginResults.Successful:
                        {
                            var customer = _customerService.GetCustomerByEmail(model.Email);
                            //Crea la sesion
                            _authenticationService.SignIn(customer, false);
                            _customerActivityService.InsertActivity("PublicStore.Login", _localizationService.GetResource("ActivityLog.PublicStore.Login"), customer);

                            return Ok(new { Email = model.Email, Name = customer.GetFullName() });
                        }
                    //Si hay algún error retorna un BadRequest
                    case CustomerLoginResults.CustomerNotExist:
                    case CustomerLoginResults.Deleted:
                    case CustomerLoginResults.NotActive:
                    case CustomerLoginResults.NotRegistered:
                    case CustomerLoginResults.WrongPassword:
                        return BadRequest(_localizationService.GetResource("Account.Login.WrongCredentials." + CustomerLoginResults.CustomerNotExist.ToString()));
                    default:
                        return BadRequest(_localizationService.GetResource("Account.Login.WrongCredentials"));
                }
                
            }
            else
            {
                return Conflict();
            }

        }

        [HttpPost]
        [Route("api/auth/verify")]
        public IHttpActionResult IsSessionActive()
        {
            return Ok(
                new { 
                    Active = _workContext.CurrentCustomer != null && !_workContext.CurrentCustomer.IsGuest(), 
                    Name = _workContext.CurrentCustomer != null && !_workContext.CurrentCustomer.IsGuest() ? _workContext.CurrentCustomer.GetFullName() : string.Empty 
            });
        }
    }
}
