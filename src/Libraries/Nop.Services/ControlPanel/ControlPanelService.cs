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
                IconMini = "icon-publica",
                IconBig = "icon-publica",
                SubModules = new List<ControlPanelModule>() 
                { 
                    new ControlPanelModule(){
                        Name = "PublishProductBike",
                        Controller = "Sales",
                        Action = "PublishProductBike",
                        IconMini = "icon-publica",
                        IconBig = "icon-publica"
                    } ,
                     new ControlPanelModule(){
                        Name = "PublishProduct",
                        Controller = "Sales",
                        Action = "PublishProduct",
                        IconMini = "icon-publica",
                        IconBig = "icon-publica"
                    }
                }
            };

            //Solo pueden vender servicios los que tienen tienda
            //if (vendor != null && vendor.VendorType == Core.Domain.Vendors.VendorType.RepairShop)
            //    salesModule.SubModules.Add(new ControlPanelModule()
            //    {
            //        Name = "PublishProductService",
            //        Controller = "Sales",
            //        Action = "PublishProductService",
            //        IconMini = "icon-publica",
            //        IconBig = "icon-publica"
            //    });

            modules.Add(salesModule);

            //Mis datos
            modules.Add(new ControlPanelModule()
            {
                Name = "MyAccount",
                Controller = "ControlPanel",
                Action = "MyAccount",
                IconMini = "icon-cc",
                IconBig = "icon-cc",
                SubModules = new List<ControlPanelModule>() { 
                         new ControlPanelModule()
                        {
                             Name = "MyAccount",
                            Controller = "ControlPanel",
                            Action = "MyAccount",
                            IconMini = "icon-cc",
                            IconBig = "icon-cc"
                        },
                        new ControlPanelModule()
                        {
                            Name = "ChangePassword",
                            Controller = "Customer",
                            Action = "ChangePassword",
                            IconMini = "icon-cc",
                            IconBig = "icon-cc"
                        }
                    }

            });

            if (vendor != null)
            {
                modules.Add(new ControlPanelModule()
                {
                    Name = "MyOrders",
                    Controller = "ControlPanel",
                    Action = "MyOrders",
                    IconMini = "icon-compra",
                    IconBig = "icon-compra"
                });
                
            }

            

            //Tienda
            if (vendor != null && vendor.VendorType != Core.Domain.Vendors.VendorType.User)
            {
                modules.Add(new ControlPanelModule()
                {
                    Name = _workContext.CurrentVendor.VendorType == Core.Domain.Vendors.VendorType.Market ? "Vendor" : "RepairShop",
                    Controller = "Catalog",
                    Action = "Vendor",
                    IconMini = "icon-tienda",
                    IconBig = "icon-tienda",
                    Parameters = new { seName = vendor.GetSeName() },
                    SubModules = new List<ControlPanelModule>() { 
                        new ControlPanelModule()
                        {
                            Name = "Vendor",
                            Controller = "Catalog",
                            Action = "Vendor",
                            IconMini = "icon-tienda",
                            IconBig = "icon-tienda",
                            Parameters = new { seName = vendor.GetSeName() }
                        },
                        new ControlPanelModule()
                        {
                            Name = "Offices",
                            Controller = "ControlPanel",
                            Action = "Offices",
                            IconMini = "icon-tienda",
                            IconBig = "icon-tienda"
                        },
                        new ControlPanelModule()
                        {
                            Name = "VendorServices",
                            Controller = "ControlPanel",
                            Action = "VendorServices",
                            IconMini = "icon-tienda",
                            IconBig = "icon-tienda"
                        }
                    }
                });
            }

            //if (vendor != null)
            //{
            //    //Ventas
            //    modules.Add(new ControlPanelModule()
            //    {
            //        Name = "MySales",
            //        Controller = "ControlPanel",
            //        Action = "MySales",
            //        IconMini = "icon-venta",
            //        IconBig = "icon-venta",
            //        SubModules = new List<ControlPanelModule>() { 
            //        new ControlPanelModule()
            //        {
            //            Name = "AllOrders",
            //            Controller = "ControlPanel",
            //            Action = "MySales",
            //            IconMini = "icon-venta",
            //            IconBig = "icon-venta"
            //        },
            //        new ControlPanelModule()
            //        {
            //            Name = "MySalesNoRating",
            //            Controller = "ControlPanel",
            //            Action = "MySales",
            //            IconMini = "icon-venta",
            //            IconBig = "icon-venta",
            //            Parameters = new { filter = "norating" }
            //        },
            //        new ControlPanelModule()
            //        {
            //            Name = "MySalesRating",
            //            Controller = "ControlPanel",
            //            Action = "MySales",
            //            IconMini = "icon-venta",
            //            IconBig = "icon-venta",
            //            Parameters = new { filter = "rating" }
            //        },
            //        new ControlPanelModule()
            //        {
            //            Name = "MySalesActiveProducts",
            //            Controller = "ControlPanel",
            //            Action = "MySales",
            //            IconMini = "icon-venta",
            //            IconBig = "icon-venta",
            //            Parameters = new { filter = "active" }
            //        }
            //    }
            //    });

            //}

            if (vendor != null)
            {
                //Mis productos
                modules.Add(new ControlPanelModule()
                {
                    Name = "MyProducts",
                    Controller = "ControlPanel",
                    Action = "MyProducts",
                    IconMini = "icon-scooter",
                    IconBig = "icon-scooter",
                    //Parameters = new { p = true },
                    SubModules = new List<ControlPanelModule>() { 
                    new ControlPanelModule()
                    {
                        Name = "MyProductsPublished",
                        Controller = "ControlPanel",
                        Action = "MyProducts",
                        IconMini = "icon-scooter",
                        IconBig = "icon-scooter"
                    },
                    new ControlPanelModule()
                    {
                        Name = "MyProductsUnpublished",
                        Controller = "ControlPanel",
                        Action = "MyProducts",
                        IconMini = "icon-scooter",
                        IconBig = "icon-scooter",
                        Parameters = new { p = false }
                    }
                }

                });
                
            }


            //Compras
            //modules.Add(new ControlPanelModule()
            //{
            //    Name = "MyOrders",
            //    Controller = "ControlPanel",
            //    Action = "MyOrders",
            //    IconMini = "icon-compra",
            //    IconBig = "icon-compra",
            //    SubModules = new List<ControlPanelModule>() { 
            //        new ControlPanelModule()
            //        {
            //            Name = "AllOrders",
            //            Controller = "ControlPanel",
            //            Action = "MyOrders",
            //            IconMini = "icon-compra",
            //            IconBig = "icon-compra"
            //        },
            //       new ControlPanelModule()
            //        {
            //            Name = "MySalesNoRating",
            //            Controller = "ControlPanel",
            //            Action = "MyOrders",
            //            IconMini = "icon-compra",
            //            IconBig = "icon-compra",
            //            Parameters = new { filter = "norating" }
            //        },
            //        new ControlPanelModule()
            //        {
            //            Name = "MySalesRating",
            //            Controller = "ControlPanel",
            //            Action = "MyOrders",
            //            IconMini = "icon-compra",
            //            IconBig = "icon-compra",
            //            Parameters = new { filter = "rating" }
            //        }
            //    }
            //});

            //Mensajes
            //modules.Add(new ControlPanelModule()
            //{
            //    Name = "Messages",
            //    Controller = "ControlPanel",
            //    Action = "Messages",
            //    IconMini = "icon-mail",
            //    IconBig = "icon-mail"
            //});

            ////Favoritos
            //modules.Add(new ControlPanelModule()
            //{
            //    Name = "Favourites",
            //    Controller = "ControlPanel",
            //    Action = "Favourites",
            //    IconMini = "icon-mail",
            //    IconBig = "icon-mail"
            //});

            return modules;
        }
    }
}
