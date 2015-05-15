using Nop.Core;
using Nop.Core.Domain.ControlPanel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Services.Seo;

namespace Nop.Services.ControlPanel
{
    public partial class ControlPanelService : IControlPanelService
    {
        #region Props
        private readonly IWorkContext _workContext;
        #endregion

        #region Ctor
        public ControlPanelService(IWorkContext workContext)
        {
            this._workContext = workContext;
        }
        #endregion




        public List<ControlPanelModule> GetModulesActiveUser()
        {
            var vendor = this._workContext.CurrentVendor;
            var customer = this._workContext.CurrentVendor;

            var modules = new List<ControlPanelModule>();


            //Vender
            var salesModule = new ControlPanelModule()
            {
                Name = "Sale",
                Controller = "Sales",
                Action = "Index",
                IconMini = "icon-mail",
                IconBig = "icon-mail",
                SubModules = new List<ControlPanelModule>() 
                { 
                    new ControlPanelModule(){
                        Name = "PublishProductBike",
                        Controller = "Sales",
                        Action = "PublishProductBike",
                        IconMini = "icon-mail",
                        IconBig = "icon-mail"
                    } ,
                     new ControlPanelModule(){
                        Name = "PublishProduct",
                        Controller = "Sales",
                        Action = "PublishProduct",
                        IconMini = "icon-mail",
                        IconBig = "icon-mail"
                    }
                }
            };

            //Solo pueden vender servicios los que tienen tienda
            if (vendor != null && vendor.VendorType == Core.Domain.Vendors.VendorType.RepairShop)
                salesModule.SubModules.Add(new ControlPanelModule()
                {
                    Name = "PublishProductService",
                    Controller = "Sales",
                    Action = "PublishProductService",
                    IconMini = "icon-mail",
                    IconBig = "icon-mail"
                });

            modules.Add(salesModule);

            //Mis datos
            modules.Add(new ControlPanelModule()
            {
                Name = "MyAccount",
                Controller = "ControlPanel",
                Action = "MyAccount",
                IconMini = "icon-mail",
                IconBig = "icon-mail"
            });

            //Tienda
            if (vendor != null && vendor.VendorType != Core.Domain.Vendors.VendorType.User)
            {
                modules.Add(new ControlPanelModule()
                {
                    Name = "Vendor",
                    Controller = "Catalog",
                    Action = "Vendor",
                    IconMini = "icon-mail",
                    IconBig = "icon-mail",
                    Parameters = new { seName = vendor.GetSeName() },
                    SubModules = new List<ControlPanelModule>() { 
                        new ControlPanelModule()
                        {
                            Name = "Vendor",
                            Controller = "Catalog",
                            Action = "Vendor",
                            IconMini = "icon-mail",
                            IconBig = "icon-mail",
                            Parameters = new { seName = vendor.GetSeName() }
                        },
                        new ControlPanelModule()
                        {
                            Name = "Offices",
                            Controller = "ControlPanel",
                            Action = "Offices",
                            IconMini = "icon-mail",
                            IconBig = "icon-mail"
                        },
                        new ControlPanelModule()
                        {
                            Name = "VendorServices",
                            Controller = "ControlPanel",
                            Action = "VendorServices",
                            IconMini = "icon-mail",
                            IconBig = "icon-mail"
                        }
                    }
                });
            }


            //Ventas
            modules.Add(new ControlPanelModule()
            {
                Name = "MySales",
                Controller = "ControlPanel",
                Action = "MySales",
                IconMini = "icon-mail",
                IconBig = "icon-mail",
                SubModules = new List<ControlPanelModule>() { 
                    new ControlPanelModule()
                    {
                        Name = "AllOrders",
                        Controller = "ControlPanel",
                        Action = "MySales",
                        IconMini = "icon-mail",
                        IconBig = "icon-mail"
                    },
                    new ControlPanelModule()
                    {
                        Name = "MySalesNoRating",
                        Controller = "ControlPanel",
                        Action = "MySales",
                        IconMini = "icon-mail",
                        IconBig = "icon-mail",
                        Parameters = new { filter = "norating" }
                    },
                    new ControlPanelModule()
                    {
                        Name = "MySalesRating",
                        Controller = "ControlPanel",
                        Action = "MySales",
                        IconMini = "icon-mail",
                        IconBig = "icon-mail",
                        Parameters = new { filter = "rating" }
                    },
                    new ControlPanelModule()
                    {
                        Name = "MySalesActiveProducts",
                        Controller = "ControlPanel",
                        Action = "MySales",
                        IconMini = "icon-mail",
                        IconBig = "icon-mail",
                        Parameters = new { filter = "active" }
                    }
                }
            });

            

            //Mis productos
            modules.Add(new ControlPanelModule()
            {
                Name = "MyProducts",
                Controller = "ControlPanel",
                Action = "MyProducts",
                IconMini = "icon-mail",
                IconBig = "icon-mail",
                Parameters = new { p = true },
                SubModules = new List<ControlPanelModule>() { 
                    new ControlPanelModule()
                    {
                        Name = "MyProductsPublished",
                        Controller = "ControlPanel",
                        Action = "MyProducts",
                        IconMini = "icon-mail",
                        IconBig = "icon-mail",
                        Parameters = new { p = true }
                    },
                    new ControlPanelModule()
                    {
                        Name = "MyProductsUnpublished",
                        Controller = "ControlPanel",
                        Action = "MyProducts",
                        IconMini = "icon-mail",
                        IconBig = "icon-mail",
                        Parameters = new { p = false }
                    }
                }
                
            });

            //Compras
            modules.Add(new ControlPanelModule()
            {
                Name = "MyOrders",
                Controller = "ControlPanel",
                Action = "MyOrders",
                IconMini = "icon-mail",
                IconBig = "icon-mail",
                SubModules = new List<ControlPanelModule>() { 
                    new ControlPanelModule()
                    {
                        Name = "AllOrders",
                        Controller = "ControlPanel",
                        Action = "MyOrders",
                        IconMini = "icon-mail",
                        IconBig = "icon-mail"
                    },
                   new ControlPanelModule()
                    {
                        Name = "MySalesNoRating",
                        Controller = "ControlPanel",
                        Action = "MyOrders",
                        IconMini = "icon-mail",
                        IconBig = "icon-mail",
                        Parameters = new { filter = "norating" }
                    },
                    new ControlPanelModule()
                    {
                        Name = "MySalesRating",
                        Controller = "ControlPanel",
                        Action = "MyOrders",
                        IconMini = "icon-mail",
                        IconBig = "icon-mail",
                        Parameters = new { filter = "rating" }
                    }
                }
            });

            //Mensajes
            modules.Add(new ControlPanelModule()
            {
                Name = "Messages",
                Controller = "ControlPanel",
                Action = "Messages",
                IconMini = "icon-mail",
                IconBig = "icon-mail"
            });

            //Favoritos
            modules.Add(new ControlPanelModule()
            {
                Name = "Favourites",
                Controller = "ControlPanel",
                Action = "Favourites",
                IconMini = "icon-mail",
                IconBig = "icon-mail"
            });

            return modules;
        }
    }
}
