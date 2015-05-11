﻿using Nop.Core;
using Nop.Services.Customers;
//using Nop.Web.Models.ControlPanel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nop.Web.Extensions;
using Nop.Core.Domain.Common;
using Nop.Services.Directory;
using Nop.Web.Models.Customer;
using Nop.Web.Models.ControlPanel;
using Nop.Services.Catalog;
using Nop.Services.Common;
using Nop.Core.Domain.Customers;
using Nop.Services.Authentication;
using Nop.Core.Domain.Catalog;
using Nop.Services.Messages;
using Nop.Services.ControlPanel;
using Nop.Core.Domain.ControlPanel;
using Nop.Services.Vendors;
using Nop.Web.Extensions;
using Nop.Core.Domain.Vendors;

namespace Nop.Web.Controllers
{
    [Authorize]
    public class ControlPanelController : Controller
    {
        #region Fields
        private readonly ICustomerService _customerService;
        private readonly IWorkContext _workContext;
        private readonly TuilsSettings _tuilsSettings;
        private readonly IStateProvinceService _stateProvinceService;
        private readonly ICategoryService _categoryService;
        private readonly ICustomerRegistrationService _customerRegistrationService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly INewsLetterSubscriptionService _newsLetterSubscriptionService;
        private readonly IStoreContext _storeContext;
        private readonly IControlPanelService _controlPanelService;
        private readonly IVendorService _vendorService;
        #endregion

        #region Ctor
        public ControlPanelController(ICustomerService customerService,
            IWorkContext workContext,
            TuilsSettings tuilsSettings,
            IStateProvinceService stateProvinceService,
            ICategoryService categoryService,
            ICustomerRegistrationService customerRegistrationService,
            IAuthenticationService authenticationService,
            IGenericAttributeService genericAttributeService,
            INewsLetterSubscriptionService newsLetterSubscriptionService,
            IStoreContext storeContext,
            IControlPanelService controlPanelService,
            IVendorService vendorService)
        {
            this._customerService = customerService;
            this._workContext = workContext;
            this._tuilsSettings = tuilsSettings;
            this._stateProvinceService = stateProvinceService;
            this._categoryService = categoryService;
            this._customerRegistrationService = customerRegistrationService;
            this._authenticationService = authenticationService;
            this._genericAttributeService = genericAttributeService;
            this._newsLetterSubscriptionService = newsLetterSubscriptionService;
            this._storeContext = storeContext;
            this._controlPanelService = controlPanelService;
            this._vendorService = vendorService;
        }
        #endregion
        public ActionResult Index()
        {
            return View();
        }

        #region MyAccount

        public ActionResult MyAccount()
        {
            var model = GetModelMyAccount(null);
            return View(model);
        }

        [HttpPost]
        public ActionResult MyAccount(MyAccountModel model)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return new HttpUnauthorizedResult();

            var customer = _workContext.CurrentCustomer;

            try
            {
                if (ModelState.IsValid)
                {

                    //email
                    if (!customer.Email.Equals(model.Email.Trim(), StringComparison.InvariantCultureIgnoreCase))
                    {
                        _customerRegistrationService.SetEmail(customer, model.Email.Trim());
                        _authenticationService.SignIn(customer, true);
                    }
                    
                    //Actualiza el valor del genero
                    model.Gender = model.Gender == null ? "M" : "F";

                    //Guarda los atributos
                    _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.Gender, model.Gender); 
                    _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.FirstName, model.FirstName);
                    _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.LastName, model.LastName);
                    _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.StateProvinceId, model.StateProvinceId);
                    _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.Phone, model.Phone);
                    _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.BikeBrandId, model.BikeBrandId);
                    _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.BikeReferenceId, model.BikeReferenceId);
                    _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.DateOfBirth, model.DateOfBirth);
                    _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.BikeYear, model.BikeYear);
                    _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.BikeCarriagePlate, model.BikeCarriagePlate);

                    _newsLetterSubscriptionService.SwitchNewsletterByEmail(customer.Email, model.Newsletter, Core.Domain.Messages.NewsLetterSuscriptionType.General);
                    _newsLetterSubscriptionService.SwitchNewsletterByEmail(customer.Email, model.NewsletterBrand, Core.Domain.Messages.NewsLetterSuscriptionType.MyBrand);
                    _newsLetterSubscriptionService.SwitchNewsletterByEmail(customer.Email, model.NewsletterReference, Core.Domain.Messages.NewsLetterSuscriptionType.MyReference);
                }
            }
            catch (Exception exc)
            {
                ModelState.AddModelError("", exc.Message);
            }

            model = GetModelMyAccount(model);

            return View(model);
        }

        private MyAccountModel GetModelMyAccount(MyAccountModel model)
        {
            
            if(model == null)
                model = _workContext.CurrentCustomer.ToMyAccountModel();

            model.BikeReferences = model.BikeBrandId.HasValue ? _categoryService.GetAllCategoriesByParentCategoryId(model.BikeBrandId.Value) : new List<Category>();

            model.States = _stateProvinceService.GetStateProvincesByCountryId(_tuilsSettings.defaultCountry);
            model.BikeBrands = _categoryService.GetAllCategoriesByParentCategoryId(_tuilsSettings.productBaseTypes_bike);
            return model;
        }

        #endregion

        #region Offices
        public ActionResult Offices()
        {
            var model = new OfficesModel();
            model.States = _stateProvinceService.GetStateProvincesByCountryId(_tuilsSettings.defaultCountry);
            model.VendorId = _workContext.CurrentVendor.Id;
            return View(model);
        }
        #endregion

        #region Verdor Services
        public ActionResult VendorServices()
        {
            if (_workContext.CurrentVendor != null)
            {
                int vendorId = _workContext.CurrentVendor.Id;
                var model = new VendorServicesModel();
                var specialCategories = _vendorService.GetSpecialCategoriesByVendorId(vendorId);
                model.SpecializedCategories = specialCategories.Where(c => c.SpecialType == Core.Domain.Vendors.SpecialCategoryVendorType.SpecializedCategory)
                    .Select(c => c.CategoryId)
                    .ToList();
                model.BikeReferences = specialCategories.Where(c => c.SpecialType == Core.Domain.Vendors.SpecialCategoryVendorType.BikeBrand)
                    .Select(c => c.CategoryId)
                    .ToList();
                model.SpecializedCategoriesString = model.SpecializedCategories.ToStringSeparatedBy();
                model.BikeReferencesString = model.BikeReferences.ToStringSeparatedBy();

                return View(model);
            }
            else
                return HttpNotFound();
        }

        [HttpPost]
        public ActionResult VendorServices(VendorServicesModel model)
        {
            
            if(_workContext.CurrentVendor != null)
            {
                //Toma la cadena separada por comas y crea una lista de categorias relacinoadas
                var bikeReferences = model.BikeReferencesString
                .Split(new char[]{ ',' })
                .ToList()
                .Select(c => new SpecialCategoryVendor() { CategoryId = Convert.ToInt32(c), VendorId = _workContext.CurrentVendor.Id, SpecialType = SpecialCategoryVendorType.BikeBrand  });

                var specializedCategories = model.SpecializedCategoriesString
                .Split(new char[]{ ',' })
                .ToList()
                .Select(c => new SpecialCategoryVendor() { CategoryId = Convert.ToInt32(c), VendorId = _workContext.CurrentVendor.Id, SpecialType = SpecialCategoryVendorType.SpecializedCategory  });

                //Concatena las dos listas anteriores y las envía a ser actualizadas
                _vendorService.InsertUpdateVendorSpecialCategories(_workContext.CurrentVendor.Id, bikeReferences.Concat(specializedCategories).ToList());

                return View(model);
            }
            else
            {
                return this.HttpNotFound();
            }

        }
        #endregion


        #region Menu
        [ChildActionOnly]
        public ActionResult Menu() 
        {
            var model = new MenuModel();
            model.Modules = this._controlPanelService.GetModulesActiveUser();

            //Busca cual es el modulo actual
            string parentModule = string.Empty;
            model.SelectedModule = GetCurrentModule(model.Modules, ref parentModule);
            model.SelectedParentModule = parentModule;

            return PartialView("_Menu", model);
        }

        /// <summary>
        /// Retorna el modulo seleccionado actualmente, por referencia envia el modulo padre seleccionado
        /// </summary>
        /// <param name="modules"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        private string GetCurrentModule(List<ControlPanelModule> modules, ref string parent)
        {
            string currentAction = ControllerContext.ParentActionViewContext.RouteData.Values["action"].ToString();
            string currentController = ControllerContext.ParentActionViewContext.RouteData.Values["controller"].ToString();
            foreach (var module in modules)
            {
                parent = module.Name;
                if (module.Action.Equals(currentAction) && module.Controller.Equals(currentController))
                    return module.Name;
                else
                {
                    //Si no es de tipo padre recorre los submodulos
                    foreach (var sm in module.SubModules)
                    {
                        if (sm.Action.Equals(currentAction) && sm.Controller.Equals(currentController))
                            return module.Name;
                    }
                }

            }
            return string.Empty;
        }


        
        #endregion
    }
}