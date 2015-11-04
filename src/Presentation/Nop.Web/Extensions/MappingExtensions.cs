﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Directory;
using Nop.Services.Common;
using Nop.Services.Directory;
using Nop.Services.Localization;
using Nop.Services.Seo;
using Nop.Web.Models.Catalog;
using Nop.Web.Models.Common;

using Nop.Web.Models.Customer;
using Nop.Web.Models.ControlPanel;
using Nop.Services.Messages;
using Nop.Web.Models.Media;
using Nop.Core.Infrastructure;
using Nop.Core.Caching;
using Nop.Services.Media;
using Nop.Core;
using Nop.Core.Domain.Media;
using Nop.Services.Helpers;
using Nop.Core.Domain.Vendors;
using Nop.Services.Vendors;

namespace Nop.Web.Extensions
{
    public static class MappingExtensions
    {

        #region MyAccount

        public static MyAccountModel ToMyAccountModel(this Customer entity, bool getNewsletter = true)
        {
            var model = new MyAccountModel();

            model.FirstName = entity.GetAttribute<string>(SystemCustomerAttributeNames.FirstName);
            model.LastName = entity.GetAttribute<string>(SystemCustomerAttributeNames.LastName);
            model.Gender = entity.GetAttribute<string>(SystemCustomerAttributeNames.Gender);
            model.DateOfBirth = entity.GetAttribute<DateTime?>(SystemCustomerAttributeNames.DateOfBirth);
            model.StateProvinceId = entity.GetAttribute<int>(SystemCustomerAttributeNames.StateProvinceId);
            model.StateProvinceChildId = entity.GetAttribute<int>(SystemCustomerAttributeNames.StateProvinceChildId);
            model.BikeBrand.CategoryId = entity.GetAttribute<int?>(SystemCustomerAttributeNames.BikeBrandId);
            model.BikeReferenceId = entity.GetAttribute<int?>(SystemCustomerAttributeNames.BikeReferenceId);
            model.BikeYear = entity.GetAttribute<int?>(SystemCustomerAttributeNames.BikeYear);
            model.BikeCarriagePlate = entity.GetAttribute<string>(SystemCustomerAttributeNames.BikeCarriagePlate);
            model.Phone = entity.GetAttribute<string>(SystemCustomerAttributeNames.Phone);
            model.Email = entity.Email;

            if (getNewsletter)
            {
                var newsletterService = Nop.Core.Infrastructure.EngineContext.Current.Resolve<INewsLetterSubscriptionService>();
                model.NewsletterBrand = newsletterService.IsEmailSubscribed(model.Email, Core.Domain.Messages.NewsLetterSuscriptionType.MyBrand);
                model.Newsletter = newsletterService.IsEmailSubscribed(model.Email, Core.Domain.Messages.NewsLetterSuscriptionType.General);
                model.NewsletterReference = newsletterService.IsEmailSubscribed(model.Email, Core.Domain.Messages.NewsLetterSuscriptionType.MyReference);
            }

            //NEWSLETEEEER*********************************

            return model;
        }
        #endregion

        #region Question
        
        #endregion

        #region Category
        //category
        public static CategoryModel ToModel(this Category entity,
            bool loadPictures = false,
            ILocalizationService localizationService = null,
            MediaSettings mediaSettings = null,
            IPictureService pictureService = null,
            int? pictureSize = null)
        {
            if (entity == null)
                return null;

            var model = new CategoryModel
            {
                Id = entity.Id,
                Name = entity.GetLocalized(x => x.Name),
                Description = entity.GetLocalized(x => x.Description),
                MetaKeywords = entity.GetLocalized(x => x.MetaKeywords),
                MetaDescription = entity.GetLocalized(x => x.MetaDescription),
                MetaTitle = entity.GetLocalized(x => x.MetaTitle),
                SeName = entity.GetSeName(),
                ChildrenCategories = entity.SubCategories
                                            .OrderBy(c => c.Name)
                                            .ToList()
                                            .ToBaseModels()
            };


            if (loadPictures)
            {
                if (localizationService == null)
                    localizationService = EngineContext.Current.Resolve<ILocalizationService>();
                if (mediaSettings == null)
                    mediaSettings = EngineContext.Current.Resolve<MediaSettings>();
                if (pictureService == null)
                    pictureService = EngineContext.Current.Resolve<IPictureService>();

                model.PictureModel = entity.GetPicture(localizationService, mediaSettings, pictureService);
            }

            return model;
        }

        public static CategoryBaseModel ToBaseModel(this Category entity)
        {
            if (entity == null)
                return null;

            var model = new CategoryBaseModel
            {
                Id = entity.Id,
                Name = entity.GetLocalized(x => x.Name),
                Description = entity.GetLocalized(x => x.Description),
                MetaKeywords = entity.GetLocalized(x => x.MetaKeywords),
                SeName = entity.GetSeName()
            };

            if (entity.SubCategories.Count > 0)
                model.ChildrenCategories = entity.SubCategories.ToList().ToBaseModels();

            return model;
        }

        public static List<CategoryModel> ToModels(this IList<Category> entities, 
            bool loadPictures = false,
            ILocalizationService localizationService = null,
            MediaSettings mediaSettings = null,
            IPictureService pictureService = null,
            int? pictureSize = null)
        {
            var models = new List<CategoryModel>();

            if (loadPictures)
            {
                if (localizationService == null)
                    localizationService = EngineContext.Current.Resolve<ILocalizationService>();
                if (mediaSettings == null)
                    mediaSettings = EngineContext.Current.Resolve<MediaSettings>();
                if (pictureService == null)
                    pictureService = EngineContext.Current.Resolve<IPictureService>();
            }

            foreach (var entity in entities)
            {
                models.Add(entity.ToModel(loadPictures, localizationService, mediaSettings, pictureService, pictureSize));
            }
            return models;
        }

        public static List<CategoryBaseModel> ToBaseModels(this IList<Category> entities)
        {
            var models = new List<CategoryBaseModel>();

            foreach (var entity in entities)
            {
                models.Add(entity.ToBaseModel());
            }
            return models;
        }
        #endregion

        #region Vendor

        public static VendorModel ToModel(this  Vendor vendor, 
            IWorkContext workContext, 
            IPictureService pictureService, 
            ILocalizationService localizationService,
            MediaSettings mediaSettings,
            IVendorService vendorService)
        {
            var model = new VendorModel
            {
                Id = vendor.Id,
                Name = vendor.GetLocalized(x => x.Name),
                Description = vendor.GetLocalized(x => x.Description),
                MetaKeywords = vendor.GetLocalized(x => x.MetaKeywords),
                MetaDescription = vendor.GetLocalized(x => x.MetaDescription),
                MetaTitle = vendor.GetLocalized(x => x.MetaTitle),
                SeName = vendor.GetSeName(),
                AvgRating = vendor.AvgRating ?? 0,
                EnableCreditCardPayment = vendor.EnableCreditCardPayment ?? false,
                EnableShipping = vendor.EnableShipping ?? false,
                AllowEdit = workContext.CurrentVendor != null && workContext.CurrentVendor.Id == vendor.Id,
                BackgroundPosition = vendor.BackgroundPosition,
                PhoneNumber = vendor.PhoneNumber
            };
            //Cargan las imagenes

            var pictureModel = new PictureModel
            {
                ImageUrl = pictureService.GetPictureUrl(vendor.Picture, mediaSettings.VendorMainThumbPictureSize, crop: true),
                FullSizeImageUrl = pictureService.GetPictureUrl(vendor.Picture),
                Title = string.Format(localizationService.GetResource("Media.Product.ImageLinkTitleFormat"), model.Name),
                AlternateText = string.Format(localizationService.GetResource("Media.Product.ImageAlternateTextFormat"), model.Name)
            };
            model.Picture = pictureModel;

            var backgroundPictureModel = new PictureModel
            {
                ImageUrl = pictureService.GetPictureUrl(vendor.BackgroundPicture, mediaSettings.VendorBackgroundThumbPictureSize),
                FullSizeImageUrl = pictureService.GetPictureUrl(vendor.BackgroundPicture),
                Title = string.Format(localizationService.GetResource("Media.Product.ImageLinkTitleFormat"), model.Name),
                AlternateText = string.Format(localizationService.GetResource("Media.Product.ImageAlternateTextFormat"), model.Name)
            };
            model.BackgroundPicture = backgroundPictureModel;

            //Carga las categorias especiales
            model.SpecialCategories = vendorService.GetSpecialCategoriesByVendorId(vendor.Id).ToModels();

            model.MetaDescription = model.MetaDescription ?? model.Description;

            return model;
        }


        public static IList<VendorModel> ToModels(this  IList<Vendor> vendors,
            IWorkContext workContext,
            IPictureService pictureService,
            ILocalizationService localizationService,
            MediaSettings mediaSettings,
            IVendorService vendorService)
        {
            var models = new List<VendorModel>();
            foreach (var vendor in vendors)
            {
                models.Add(vendor.ToModel(workContext, pictureService, localizationService, mediaSettings, vendorService));
            }
            return models;
        }

        #endregion

        //public static List<CategoryBaseModel> ToBaseModels(this IList<Category> entities)
        //{
        //    var models = new List<CategoryBaseModel>();

        //    foreach (var entity in entities)
        //    {
        //        models.Add(entity.ToModel());
        //    }
        //    return models;
        //}

        #region manufacturer
        //manufacturer
        public static ManufacturerModel ToModel(this Manufacturer entity,
            bool loadPictures = false,
            ILocalizationService localizationService = null,
            MediaSettings mediaSettings = null,
            IPictureService pictureService = null,
            int? pictureSize = null)
        {
            if (entity == null)
                return null;

            var model = new ManufacturerModel
            {
                Id = entity.Id,
                Name = entity.GetLocalized(x => x.Name),
                Description = entity.GetLocalized(x => x.Description),
                MetaKeywords = entity.GetLocalized(x => x.MetaKeywords),
                MetaDescription = entity.GetLocalized(x => x.MetaDescription),
                MetaTitle = entity.GetLocalized(x => x.MetaTitle),
                SeName = entity.GetSeName(),
            };

            if (loadPictures)
            {
                if (localizationService == null)
                    localizationService = EngineContext.Current.Resolve<ILocalizationService>();
                if (mediaSettings == null)
                    mediaSettings = EngineContext.Current.Resolve<MediaSettings>();
                if (pictureService == null)
                    pictureService = EngineContext.Current.Resolve<IPictureService>();

                model.PictureModel = entity.GetPicture(localizationService, mediaSettings, pictureService, pictureSize);
            }


            return model;
        }

        public static List<ManufacturerModel> ToModels(this IList<Manufacturer> entities,
            bool loadPictures = false,
            ILocalizationService localizationService = null,
            MediaSettings mediaSettings = null,
            IPictureService pictureService = null,
            int? pictureSize = null)
        {
            var models = new List<ManufacturerModel>();

            if (loadPictures)
            {
                if (localizationService == null)
                    localizationService = EngineContext.Current.Resolve<ILocalizationService>();
                if (mediaSettings == null)
                    mediaSettings = EngineContext.Current.Resolve<MediaSettings>();
                if (pictureService == null)
                    pictureService = EngineContext.Current.Resolve<IPictureService>();
            }

            foreach (var entity in entities)
            {
                models.Add(entity.ToModel(loadPictures, localizationService, mediaSettings, pictureService, pictureSize));
            }
            return models;
        }
        #endregion

        


        //address
        /// <summary>
        /// Prepare address model
        /// </summary>
        /// <param name="model">Model</param>
        /// <param name="address">Address</param>
        /// <param name="excludeProperties">A value indicating whether to exclude properties</param>
        /// <param name="addressSettings">Address settings</param>
        /// <param name="localizationService">Localization service (used to prepare a select list)</param>
        /// <param name="stateProvinceService">State service (used to prepare a select list). null to don't prepare the list.</param>
        /// <param name="addressAttributeService">Address attribute service. null to don't prepare the list.</param>
        /// <param name="addressAttributeParser">Address attribute parser. null to don't prepare the list.</param>
        /// <param name="addressAttributeFormatter">Address attribute formatter. null to don't prepare the formatted custom attributes.</param>
        /// <param name="loadCountries">A function to load countries  (used to prepare a select list). null to don't prepare the list.</param>
        /// <param name="prePopulateWithCustomerFields">A value indicating whether to pre-populate an address with customer fields entered during registration. It's used only when "address" parameter is set to "null"</param>
        /// <param name="customer">Customer record which will be used to pre-populate address. Used only when "prePopulateWithCustomerFields" is "true".</param>
        public static void PrepareModel(this AddressModel model,
            Address address, bool excludeProperties,
            AddressSettings addressSettings,
            ILocalizationService localizationService = null,
            IStateProvinceService stateProvinceService = null,
            IAddressAttributeService addressAttributeService = null,
            IAddressAttributeParser addressAttributeParser = null,
            IAddressAttributeFormatter addressAttributeFormatter = null,
            Func<IList<Country>> loadCountries = null,
            bool prePopulateWithCustomerFields = false,
            Customer customer = null)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            if (addressSettings == null)
                throw new ArgumentNullException("addressSettings");

            if (!excludeProperties && address != null)
            {
                model.Id = address.Id;
                model.FirstName = address.FirstName;
                model.LastName = address.LastName;
                model.Email = address.Email;
                model.Company = address.Company;
                model.CountryId = address.CountryId;
                model.CountryName = address.Country != null
                    ? address.Country.GetLocalized(x => x.Name)
                    : null;
                model.StateProvinceId = address.StateProvinceId;
                model.StateProvinceName = address.StateProvince != null
                    ? address.StateProvince.GetLocalized(x => x.Name)
                    : null;
                model.City = address.City;
                model.Address1 = address.Address1;
                model.Address2 = address.Address2;
                model.ZipPostalCode = address.ZipPostalCode;
                model.PhoneNumber = address.PhoneNumber;
                model.FaxNumber = address.FaxNumber;
            }

            if (address == null && prePopulateWithCustomerFields)
            {
                if (customer == null)
                    throw new Exception("Customer cannot be null when prepopulating an address");
                model.Email = customer.Email;
                model.FirstName = customer.GetAttribute<string>(SystemCustomerAttributeNames.FirstName);
                model.LastName = customer.GetAttribute<string>(SystemCustomerAttributeNames.LastName);
                model.Company = customer.GetAttribute<string>(SystemCustomerAttributeNames.Company);
                model.Address1 = customer.GetAttribute<string>(SystemCustomerAttributeNames.StreetAddress);
                model.Address2 = customer.GetAttribute<string>(SystemCustomerAttributeNames.StreetAddress2);
                model.ZipPostalCode = customer.GetAttribute<string>(SystemCustomerAttributeNames.ZipPostalCode);
                model.City = customer.GetAttribute<string>(SystemCustomerAttributeNames.City);
                //ignore country and state for prepopulation. it can cause some issues when posting pack with errors, etc
                //model.CountryId = customer.GetAttribute<int>(SystemCustomerAttributeNames.CountryId);
                //model.StateProvinceId = customer.GetAttribute<int>(SystemCustomerAttributeNames.StateProvinceId);
                model.PhoneNumber = customer.GetAttribute<string>(SystemCustomerAttributeNames.Phone);
                model.FaxNumber = customer.GetAttribute<string>(SystemCustomerAttributeNames.Fax);
            }

            //countries and states
            if (addressSettings.CountryEnabled && loadCountries != null)
            {
                if (localizationService == null)
                    throw new ArgumentNullException("localizationService");

                model.AvailableCountries.Add(new SelectListItem { Text = localizationService.GetResource("Address.SelectCountry"), Value = "0" });
                foreach (var c in loadCountries())
                {
                    model.AvailableCountries.Add(new SelectListItem
                    {
                        Text = c.GetLocalized(x => x.Name),
                        Value = c.Id.ToString(),
                        Selected = c.Id == model.CountryId
                    });
                }

                if (addressSettings.StateProvinceEnabled)
                {
                    //states
                    if (stateProvinceService == null)
                        throw new ArgumentNullException("stateProvinceService");

                    var states = stateProvinceService
                        .GetStateProvincesByCountryId(model.CountryId.HasValue ? model.CountryId.Value : 0)
                        .ToList();
                    if (states.Count > 0)
                    {
                        model.AvailableStates.Add(new SelectListItem { Text = localizationService.GetResource("Address.SelectState"), Value = "0" });

                        foreach (var s in states)
                        {
                            model.AvailableStates.Add(new SelectListItem
                            {
                                Text = s.GetLocalized(x => x.Name),
                                Value = s.Id.ToString(),
                                Selected = (s.Id == model.StateProvinceId)
                            });
                        }
                    }
                    else
                    {
                        bool anyCountrySelected = model.AvailableCountries.Any(x => x.Selected);
                        model.AvailableStates.Add(new SelectListItem
                        {
                            Text = localizationService.GetResource(anyCountrySelected ? "Address.OtherNonUS" : "Address.SelectState"),
                            Value = "0"
                        });
                    }
                }
            }

            //form fields
            model.CompanyEnabled = addressSettings.CompanyEnabled;
            model.CompanyRequired = addressSettings.CompanyRequired;
            model.StreetAddressEnabled = addressSettings.StreetAddressEnabled;
            model.StreetAddressRequired = addressSettings.StreetAddressRequired;
            model.StreetAddress2Enabled = addressSettings.StreetAddress2Enabled;
            model.StreetAddress2Required = addressSettings.StreetAddress2Required;
            model.ZipPostalCodeEnabled = addressSettings.ZipPostalCodeEnabled;
            model.ZipPostalCodeRequired = addressSettings.ZipPostalCodeRequired;
            model.CityEnabled = addressSettings.CityEnabled;
            model.CityRequired = addressSettings.CityRequired;
            model.CountryEnabled = addressSettings.CountryEnabled;
            model.StateProvinceEnabled = addressSettings.StateProvinceEnabled;
            model.PhoneEnabled = addressSettings.PhoneEnabled;
            model.PhoneRequired = addressSettings.PhoneRequired;
            model.FaxEnabled = addressSettings.FaxEnabled;
            model.FaxRequired = addressSettings.FaxRequired;

            //customer attribute services
            if (addressAttributeService != null && addressAttributeParser != null)
            {
                PrepareCustomAddressAttributes(model, address, addressAttributeService, addressAttributeParser);
            }
            if (addressAttributeFormatter != null && address != null)
            {
                model.FormattedCustomAddressAttributes = addressAttributeFormatter.FormatAttributes(address.CustomAttributes);
            }
        }
        private static void PrepareCustomAddressAttributes(this AddressModel model,
            Address address,
            IAddressAttributeService addressAttributeService,
            IAddressAttributeParser addressAttributeParser)
        {
            if (addressAttributeService == null)
                throw new ArgumentNullException("addressAttributeService");

            if (addressAttributeParser == null)
                throw new ArgumentNullException("addressAttributeParser");

            var attributes = addressAttributeService.GetAllAddressAttributes();
            foreach (var attribute in attributes)
            {
                var attributeModel = new AddressAttributeModel
                {
                    Id = attribute.Id,
                    Name = attribute.GetLocalized(x => x.Name),
                    IsRequired = attribute.IsRequired,
                    AttributeControlType = attribute.AttributeControlType,
                };

                if (attribute.ShouldHaveValues())
                {
                    //values
                    var attributeValues = addressAttributeService.GetAddressAttributeValues(attribute.Id);
                    foreach (var attributeValue in attributeValues)
                    {
                        var attributeValueModel = new AddressAttributeValueModel
                        {
                            Id = attributeValue.Id,
                            Name = attributeValue.GetLocalized(x => x.Name),
                            IsPreSelected = attributeValue.IsPreSelected
                        };
                        attributeModel.Values.Add(attributeValueModel);
                    }
                }

                //set already selected attributes
                var selectedAddressAttributes = address != null ? address.CustomAttributes : null;
                switch (attribute.AttributeControlType)
                {
                    case AttributeControlType.DropdownList:
                    case AttributeControlType.RadioList:
                    case AttributeControlType.Checkboxes:
                        {
                            if (!String.IsNullOrEmpty(selectedAddressAttributes))
                            {
                                //clear default selection
                                foreach (var item in attributeModel.Values)
                                    item.IsPreSelected = false;

                                //select new values
                                var selectedValues = addressAttributeParser.ParseAddressAttributeValues(selectedAddressAttributes);
                                foreach (var attributeValue in selectedValues)
                                    foreach (var item in attributeModel.Values)
                                        if (attributeValue.Id == item.Id)
                                            item.IsPreSelected = true;
                            }
                        }
                        break;
                    case AttributeControlType.ReadonlyCheckboxes:
                        {
                            //do nothing
                            //values are already pre-set
                        }
                        break;
                    case AttributeControlType.TextBox:
                    case AttributeControlType.MultilineTextbox:
                        {
                            if (!String.IsNullOrEmpty(selectedAddressAttributes))
                            {
                                var enteredText = addressAttributeParser.ParseValues(selectedAddressAttributes, attribute.Id);
                                if (enteredText.Count > 0)
                                    attributeModel.DefaultValue = enteredText[0];
                            }
                        }
                        break;
                    case AttributeControlType.ColorSquares:
                    case AttributeControlType.Datepicker:
                    case AttributeControlType.FileUpload:
                    default:
                        //not supported attribute control types
                        break;
                }

                model.CustomAddressAttributes.Add(attributeModel);
            }
        }


        #region SpecialCategoryVendorModel

        public static SpecialCategoryVendorModel ToModel(this SpecialCategoryVendor entity)
        {
            var model = new SpecialCategoryVendorModel() { 
                Id = entity.Id,
                CategoryId = entity.CategoryId,
                VendorId = entity.VendorId,
                SpecialTypeId = entity.SpecialTypeId,
                VendorName = entity.Vendor.Name
            };

            if (entity.Category != null)
            {
                model.CategoryName = entity.Category.Name;
                model.CategorySeName = entity.Category.GetSeName();
            }

            return model;
        }

        public  static List<SpecialCategoryVendorModel> ToModels(this IList<SpecialCategoryVendor> entities)
        {
            var models = new List<SpecialCategoryVendorModel>();
            foreach (var entity in entities)
            {
                models.Add(entity.ToModel());
            }
            return models;
        }

        #endregion



        public static Address ToEntity(this AddressModel model, bool trimFields = true)
        {
            if (model == null)
                return null;

            var entity = new Address();
            return ToEntity(model, entity, trimFields);
        }
        public static Address ToEntity(this AddressModel model, Address destination, bool trimFields = true)
        {
            if (model == null)
                return destination;

            if (trimFields)
            {
                if (model.FirstName != null)
                    model.FirstName = model.FirstName.Trim();
                if (model.LastName != null)
                    model.LastName = model.LastName.Trim();
                if (model.Email != null)
                    model.Email = model.Email.Trim();
                if (model.Company != null)
                    model.Company = model.Company.Trim();
                if (model.City != null)
                    model.City = model.City.Trim();
                if (model.Address1 != null)
                    model.Address1 = model.Address1.Trim();
                if (model.Address2 != null)
                    model.Address2 = model.Address2.Trim();
                if (model.ZipPostalCode != null)
                    model.ZipPostalCode = model.ZipPostalCode.Trim();
                if (model.PhoneNumber != null)
                    model.PhoneNumber = model.PhoneNumber.Trim();
                if (model.FaxNumber != null)
                    model.FaxNumber = model.FaxNumber.Trim();
            }
            destination.Id = model.Id;
            destination.FirstName = model.FirstName;
            destination.LastName = model.LastName;
            destination.Email = model.Email;
            destination.Company = model.Company;
            destination.CountryId = model.CountryId;
            destination.StateProvinceId = model.StateProvinceId;
            destination.City = model.City;
            destination.Address1 = model.Address1;
            destination.Address2 = model.Address2;
            destination.ZipPostalCode = model.ZipPostalCode;
            destination.PhoneNumber = model.PhoneNumber;
            destination.FaxNumber = model.FaxNumber;

            return destination;
        }


        public static SpecialCategoryProductModel ToModel(this SpecialCategoryProduct entity)
        {
            return new SpecialCategoryProductModel()
            {
                CategoryId = entity.CategoryId,
                CategoryName = entity.Category.Name,
                SpecialType = entity.SpecialType
            };
        }

        public static IList<SpecialCategoryProductModel> ToModels(this IList<SpecialCategoryProduct> list)
        {
            var models = new List<SpecialCategoryProductModel>();
            foreach (var entity in list)
            {
                models.Add(entity.ToModel());
            }
            return models;
        }

    }
}