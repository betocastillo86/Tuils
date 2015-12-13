namespace Nop.Data.Migrations
{
    using Nop.Core.Data;
    using Nop.Core.Domain.Catalog;
    using Nop.Core.Domain.Configuration;
    using Nop.Core.Domain.Customers;
    using Nop.Core.Domain.Directory;
    using Nop.Core.Domain.Localization;
    using Nop.Core.Domain.Messages;
    using Nop.Core.Domain.Security;
    using Nop.Core.Domain.Seo;
    using Nop.Core.Domain.Tasks;
    using Nop.Core.Infrastructure;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Text;

    internal sealed class Configuration : DbMigrationsConfiguration<Nop.Data.NopObjectContext>
    {

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Nop.Data.NopObjectContext context)
        {
            bool runSpecificationAttributes = false;
            bool runStateProvince = false;
            bool runCategories = false;
            bool runCurrency = false;
            bool runLanguage = false;
            bool runResources = true;
            bool runManufacturers = false;
            bool runManufacturersCategories = false;
            bool runSettings = true;
            bool runTemplatesEmails = true;
            bool runUrls = false;
            bool runTasks = true;
            bool runPermissions = false;
            
            /***DEBUG***/
            //if (System.Diagnostics.Debugger.IsAttached == false)
            //    System.Diagnostics.Debugger.Launch();


            #region Specification Attribute


            var specificationAttributeOptions = new SpecificationAttributeOption[]{
                    new SpecificationAttributeOption() { Id  = 1,Name = "Nuevo", SpecificationAttributeId = 1},
                    new SpecificationAttributeOption() { Id  = 2,Name = "Usado", SpecificationAttributeId = 1},
                    new SpecificationAttributeOption() { Id  = 3,Name = "Si", SpecificationAttributeId = 3},
                    new SpecificationAttributeOption() { Id  = 4,Name = "No", SpecificationAttributeId = 3},
                    new SpecificationAttributeOption() { Id  = 5,Name = "Chopper", SpecificationAttributeId = 4},
                    new SpecificationAttributeOption() { Id  = 6,Name = "De Calle", SpecificationAttributeId = 4},
                    new SpecificationAttributeOption() { Id  = 7, Name = "Deportiva", SpecificationAttributeId = 4},
                    new SpecificationAttributeOption() { Id  = 8, Name = "Enduro", SpecificationAttributeId = 4},
                    new SpecificationAttributeOption() { Id  = 9, Name = "Scooter", SpecificationAttributeId = 4},
                    new SpecificationAttributeOption() { Id  = 10, Name = "Turismo", SpecificationAttributeId = 4},
                    new SpecificationAttributeOption() { Id  = 11, Name = "XXXXXX", SpecificationAttributeId = 5},
                    new SpecificationAttributeOption() { Id  = 12, Name = "XXXXXX", SpecificationAttributeId = 5},
                    new SpecificationAttributeOption() { Id  = 13, Name = "XXXXXX", SpecificationAttributeId = 5},
                    new SpecificationAttributeOption() { Id  = 14, Name = "En perfecto estado", SpecificationAttributeId = 6},
                    new SpecificationAttributeOption() { Id  = 15, Name = "Muy buena", SpecificationAttributeId = 6},
                    new SpecificationAttributeOption() { Id  = 16, Name = "Buena", SpecificationAttributeId = 6},
                    new SpecificationAttributeOption() { Id  = 17, Name = "Para revisión", SpecificationAttributeId = 6},
                    new SpecificationAttributeOption() { Id  = 18, Name = "Mala", SpecificationAttributeId = 6},
                    new SpecificationAttributeOption() { Id  = 19, Name = "Doy financiamiento", SpecificationAttributeId = 7},
                    new SpecificationAttributeOption() { Id  = 20, Name = "Motor reparado", SpecificationAttributeId = 7},
                    new SpecificationAttributeOption() { Id  = 21, Name = "Nunca Chocada", SpecificationAttributeId = 7},
                    new SpecificationAttributeOption() { Id  = 22, Name = "Recibo moto", SpecificationAttributeId = 7},
                    new SpecificationAttributeOption() { Id  = 23, Name = "Unico Dueño", SpecificationAttributeId = 7},
                    new SpecificationAttributeOption() { Id  = 24, Name = "Negociable", SpecificationAttributeId = 7},
                    new SpecificationAttributeOption() { Id  = 25, Name = "Alarmas", SpecificationAttributeId = 8},
                    new SpecificationAttributeOption() { Id  = 26, Name = "Rastreo", SpecificationAttributeId = 8},
                    new SpecificationAttributeOption() { Id  = 27, Name = "Candado", SpecificationAttributeId = 8},
                    new SpecificationAttributeOption() { Id  = 28, Name = "Alforjas", SpecificationAttributeId = 8},
                    new SpecificationAttributeOption() { Id  = 29, Name = "Casco", SpecificationAttributeId = 8},
                    new SpecificationAttributeOption() { Id  = 30, Name = "ETC", SpecificationAttributeId = 8},
                    new SpecificationAttributeOption() { Id  = 31, Name = "Está en garantía de concesionario", SpecificationAttributeId = 7},
                    new SpecificationAttributeOption() { Id  = 32, Name = "0", SpecificationAttributeId = 9},
                    new SpecificationAttributeOption() { Id  = 33, Name = "1", SpecificationAttributeId = 10},
                    new SpecificationAttributeOption() { Id  = 34, Name = "0", SpecificationAttributeId = 11},
                    new SpecificationAttributeOption() { Id  = 35, Name = "Aceite", SpecificationAttributeId = 12},
                    new SpecificationAttributeOption() { Id  = 36, Name = "Gasolina", SpecificationAttributeId = 12},
                    new SpecificationAttributeOption() { Id  = 37, Name = "Pintura", SpecificationAttributeId = 12},
                    new SpecificationAttributeOption() { Id  = 38, Name = "Cera", SpecificationAttributeId = 12},
                    new SpecificationAttributeOption() { Id  = 39, Name = "Filtros", SpecificationAttributeId = 12},
                    new SpecificationAttributeOption() { Id  = 40, Name = "Refrigerante", SpecificationAttributeId = 12},
                    new SpecificationAttributeOption() { Id  = 41, Name = "Pastillas", SpecificationAttributeId = 12},
                    new SpecificationAttributeOption() { Id = 42, Name = "AMARILLO    ".Trim(),SpecificationAttributeId = 5},
                    new SpecificationAttributeOption() { Id = 43, Name = "ANARANJADO  ".Trim(),SpecificationAttributeId = 5},
                    new SpecificationAttributeOption() { Id = 44, Name = "BEIGE       ".Trim(),SpecificationAttributeId = 5},
                    new SpecificationAttributeOption() { Id = 45, Name = "BLANCO      ".Trim(),SpecificationAttributeId = 5},
                    new SpecificationAttributeOption() { Id = 46, Name = "CREMA       ".Trim(),SpecificationAttributeId = 5},
                    new SpecificationAttributeOption() { Id = 47, Name = "DORADO      ".Trim(),SpecificationAttributeId = 5},
                    new SpecificationAttributeOption() { Id = 48, Name = "GRIS        ".Trim(),SpecificationAttributeId = 5},
                    new SpecificationAttributeOption() { Id = 49, Name = "MARRON      ".Trim(),SpecificationAttributeId = 5},
                    new SpecificationAttributeOption() { Id = 50, Name = "NEGRO       ".Trim(),SpecificationAttributeId = 5},
                    new SpecificationAttributeOption() { Id = 51, Name = "PLATEADO    ".Trim(),SpecificationAttributeId = 5},
                    new SpecificationAttributeOption() { Id = 52, Name = "ROJO        ".Trim(),SpecificationAttributeId = 5},
                    new SpecificationAttributeOption() { Id = 53, Name = "VINO TINTO  ".Trim(),SpecificationAttributeId = 5}};

            if (runSpecificationAttributes)
            {
                #region Specs
                var specificationAttributeTable = context.Set<SpecificationAttribute>();
                var specificationAttributes = new SpecificationAttribute[]{
                    new SpecificationAttribute() { Id = 1, Name = "Estado", DisplayOrder = 0 },
                    new SpecificationAttribute() { Id = 2, Name = "Año", DisplayOrder = 0 },
                    new SpecificationAttribute() { Id = 3, Name = "Realiza Envios", DisplayOrder = 0 },
                    new SpecificationAttribute() { Id = 4, Name = "Tipo de Moto", DisplayOrder = 0 },
                    new SpecificationAttribute() { Id = 5, Name = "Color", DisplayOrder = 0 },
                    new SpecificationAttribute() { Id = 6, Name = "Condición", DisplayOrder = 0 },
                    new SpecificationAttribute() { Id = 7, Name = "Condiciones de Negociación", DisplayOrder = 0 },
                    new SpecificationAttribute() { Id = 8, Name = "Accesorios", DisplayOrder = 0 },
                    new SpecificationAttribute() { Id = 9, Name = "Recorrido", DisplayOrder = 0 },
                    new SpecificationAttribute() { Id = 10, Name = "Año/Modelo", DisplayOrder = 0 },
                    new SpecificationAttribute() { Id = 11, Name = "Placa", DisplayOrder = 0 },
                    new SpecificationAttribute() { Id = 12, Name = "Insumos", DisplayOrder = 0 },
                    new SpecificationAttribute() { Id = 13, Name = "Duracion Fotos", DisplayOrder = 0 },
                    new SpecificationAttribute() { Id = 14, Name = "Número Fotos", DisplayOrder = 0 },
                    new SpecificationAttribute() { Id = 15, Name = "Exposición", DisplayOrder = 0 },
                    new SpecificationAttribute() { Id = 16, Name = "Bandas rotativas", DisplayOrder = 0 },
                    new SpecificationAttribute() { Id = 17, Name = "Pagina Inicio", DisplayOrder = 0 },
                    new SpecificationAttribute() { Id = 18, Name = "Redes sociales", DisplayOrder = 0 },
                    new SpecificationAttribute() { Id = 19, Name = "Duración del plan", DisplayOrder = 0 },
                    new SpecificationAttribute() { Id = 20, Name = "Tienda virtual propia", DisplayOrder = 0 },
                    new SpecificationAttribute() { Id = 21, Name = "Marcas especializadas", DisplayOrder = 0 },
                    new SpecificationAttribute() { Id = 22, Name = "Productos publicados", DisplayOrder = 0 },
                    new SpecificationAttribute() { Id = 23, Name = "Productos destacados", DisplayOrder = 0 },
                    new SpecificationAttribute() { Id = 24, Name = "Productos en Home Page", DisplayOrder = 0 },
                    new SpecificationAttribute() { Id = 25, Name = "Productos en redes sociales", DisplayOrder = 0 },
                    new SpecificationAttribute() { Id = 26, Name = "Elaboración de la pagina", DisplayOrder = 0 }
                };
                specificationAttributeTable.AddOrUpdate(sa => sa.Id, specificationAttributes);
                #endregion

                #region SpecificationAttributeOption
                
                var specificationAttributeOptionTable = context.Set<SpecificationAttributeOption>();
                
                
                specificationAttributeOptionTable.AddOrUpdate(sao => sao.Id, specificationAttributeOptions);


                //REALIZA LA INSERCION DE LOS AÑOS DE MANERA MANUAL
                var sqlSAO = new StringBuilder();
                sqlSAO.Append("SET IDENTITY_INSERT [dbo].[SpecificationAttributeOption] ON;");
                sqlSAO.AppendLine();
                //Agrega el listado de años existentes 
                for (int i = 1940; i < 2020; i++)
                {
                    var spec = specificationAttributeOptionTable.Where(s => s.Id == i);
                    if (spec == null)
                    {
                        sqlSAO.AppendFormat("INSERT INTO [dbo].[SpecificationAttributeOption]([Id], [SpecificationAttributeId],[Name],[DisplayOrder]) VALUES ({0},2,'{0}',0);", i);
                        sqlSAO.AppendLine();
                    }
                }
                sqlSAO.Append("SET IDENTITY_INSERT [dbo].[SpecificationAttributeOption] OFF");
                context.ExecuteSqlCommand(sqlSAO.ToString());
                #endregion
            }

            #endregion

            #region StateProvince
            if (runStateProvince)
            {
                var stateProvinceTable = context.Set<StateProvince>();

                stateProvinceTable.AddOrUpdate(s => s.Id,
                    new StateProvince[]{
                    new StateProvince(){ Id = 76, CountryId = 22, Name = "LA GUAJIRA", Abbreviation = "LA GUAJIRA".Substring(2), Published = true, DisplayOrder = 1   },
                    new StateProvince(){ Id = 77, CountryId = 22, Name = "BOGOTÁ", Abbreviation = "BOGOTÁ".Substring(2), Published = true, DisplayOrder = 1   },
                    new StateProvince(){ Id = 78, CountryId = 22, Name = "CAUCA", Abbreviation = "CAUCA".Substring(2), Published = true, DisplayOrder = 1   },
                    new StateProvince(){ Id = 79, CountryId = 22, Name = "GUAINÍA", Abbreviation = "GUAINÍA".Substring(2), Published = true, DisplayOrder = 1   },
                    new StateProvince(){ Id = 80, CountryId = 22, Name = "RISARALDA", Abbreviation = "RISARALDA".Substring(2), Published = true, DisplayOrder = 1   },
                    new StateProvince(){ Id = 81, CountryId = 22, Name = "SUCRE", Abbreviation = "SUCRE".Substring(2), Published = true, DisplayOrder = 1   },
                    new StateProvince(){ Id = 82, CountryId = 22, Name = "ANTIOQUIA", Abbreviation = "ANTIOQUIA".Substring(2), Published = true, DisplayOrder = 1   },
                    new StateProvince(){ Id = 83, CountryId = 22, Name = "AMAZONAS", Abbreviation = "AMAZONAS".Substring(2), Published = true, DisplayOrder = 1   },
                    new StateProvince(){ Id = 84, CountryId = 22, Name = "ATLÁNTICO", Abbreviation = "ATLÁNTICO".Substring(2), Published = true, DisplayOrder = 1   },
                    new StateProvince(){ Id = 85, CountryId = 22, Name = "BOLÍVAR", Abbreviation = "BOLÍVAR".Substring(2), Published = true, DisplayOrder = 1   },
                    new StateProvince(){ Id = 86, CountryId = 22, Name = "MAGDALENA", Abbreviation = "MAGDALENA".Substring(2), Published = true, DisplayOrder = 1   },
                    new StateProvince(){ Id = 87, CountryId = 22, Name = "NORTE DE SANTANDER", Abbreviation = "NORTE DE SANTANDER".Substring(2), Published = true, DisplayOrder = 1   },
                    new StateProvince(){ Id = 88, CountryId = 22, Name = "NARIÑO", Abbreviation = "NARIÑO".Substring(2), Published = true, DisplayOrder = 1   },
                    new StateProvince(){ Id = 89, CountryId = 22, Name = "SANTANDER", Abbreviation = "SANTANDER".Substring(2), Published = true, DisplayOrder = 1   },
                    new StateProvince(){ Id = 90, CountryId = 22, Name = "ARAUCA", Abbreviation = "ARAUCA".Substring(2), Published = true, DisplayOrder = 1   },
                    new StateProvince(){ Id = 91, CountryId = 22, Name = "CAQUETÁ", Abbreviation = "CAQUETÁ".Substring(2), Published = true, DisplayOrder = 1   },
                    new StateProvince(){ Id = 92, CountryId = 22, Name = "CUNDINAMARCA", Abbreviation = "CUNDINAMARCA".Substring(2), Published = true, DisplayOrder = 1   },
                    new StateProvince(){ Id = 93, CountryId = 22, Name = "PUTUMAYO", Abbreviation = "PUTUMAYO".Substring(2), Published = true, DisplayOrder = 1   },
                    new StateProvince(){ Id = 94, CountryId = 22, Name = "BOYACÁ", Abbreviation = "BOYACÁ".Substring(2), Published = true, DisplayOrder = 1   },
                    new StateProvince(){ Id = 95, CountryId = 22, Name = "VICHADA", Abbreviation = "VICHADA".Substring(2), Published = true, DisplayOrder = 1   },
                    new StateProvince(){ Id = 96, CountryId = 22, Name = "CASANARE", Abbreviation = "CASANARE".Substring(2), Published = true, DisplayOrder = 1   },
                    new StateProvince(){ Id = 98, CountryId = 22, Name = "VAUPÉS", Abbreviation = "VAUPÉS".Substring(2), Published = true, DisplayOrder = 1   },
                    new StateProvince(){ Id = 99, CountryId = 22, Name = "ARCHIPIÉLAGO DE SAN ANDRÉS, PR", Abbreviation = "ARCHIPIÉLAGO DE SAN ANDRÉS, PR".Substring(2), Published = true, DisplayOrder = 1   },
                    new StateProvince(){ Id = 100, CountryId = 22, Name = "CÓRDOBA", Abbreviation = "CÓRDOBA".Substring(2), Published = true, DisplayOrder = 1   },
                    new StateProvince(){ Id = 101, CountryId = 22, Name = "GUAVIARE", Abbreviation = "GUAVIARE".Substring(2), Published = true, DisplayOrder = 1   },
                    new StateProvince(){ Id = 102, CountryId = 22, Name = "CESAR", Abbreviation = "CESAR".Substring(2), Published = true, DisplayOrder = 1   },
                    new StateProvince(){ Id = 103, CountryId = 22, Name = "CALDAS", Abbreviation = "CALDAS".Substring(2), Published = true, DisplayOrder = 1   },
                    new StateProvince(){ Id = 104, CountryId = 22, Name = "VALLE DEL CAUCA", Abbreviation = "VALLE DEL CAUCA".Substring(2), Published = true, DisplayOrder = 1   },
                    new StateProvince(){ Id = 105, CountryId = 22, Name = "HUILA", Abbreviation = "HUILA".Substring(2), Published = true, DisplayOrder = 1   },
                    new StateProvince(){ Id = 106, CountryId = 22, Name = "QUINDIO", Abbreviation = "QUINDIO".Substring(2), Published = true, DisplayOrder = 1   },
                    new StateProvince(){ Id = 107, CountryId = 22, Name = "META", Abbreviation = "META".Substring(2), Published = true, DisplayOrder = 1   },
                    new StateProvince(){ Id = 108, CountryId = 22, Name = "TOLIMA", Abbreviation = "TOLIMA".Substring(2), Published = true, DisplayOrder = 1   },
                    new StateProvince(){ Id = 109, CountryId = 22, Name = "CHOCÓ", Abbreviation = "CHOCÓ".Substring(2), Published = true, DisplayOrder = 1  }
                });
            }
            
            #endregion

            #region Categories

            var categories = new Category[]{
                    new Category() { Id = 1, Name = "XXXXXX", Description = "XXXXXX", CategoryTemplateId = 1, ParentCategoryId = 0, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = false, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,    
                    new Category() { Id = 2, Name = "XXXXXX", Description = "XXXXXX", CategoryTemplateId = 1, ParentCategoryId = 0, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = false, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,    
                    new Category() { Id = 3, Name = "XXXXXX", Description = "XXXXXX", CategoryTemplateId = 1, ParentCategoryId = 0, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = false, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,    
                    new Category() { Id = 4, Name = "XXXXXX", Description = "XXXXXX", CategoryTemplateId = 1, ParentCategoryId = 0, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = false, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,    
                    new Category() { Id = 5, Name = "XXXXXX", Description = "XXXXXX", CategoryTemplateId = 1, ParentCategoryId = 0, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = false, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,    
                    new Category() { Id = 6, Name = "XXXXXX", Description = "XXXXXX", CategoryTemplateId = 1, ParentCategoryId = 0, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = false, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,    
                    new Category() { Id = 7, Name = "XXXXXX", Description = "XXXXXX", CategoryTemplateId = 1, ParentCategoryId = 0, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = false, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,    
                    new Category() { Id = 8, Name = "XXXXXX", Description = "XXXXXX", CategoryTemplateId = 1, ParentCategoryId = 0, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = false, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,    
                    new Category() { Id = 9, Name = "XXXXXX", Description = "XXXXXX", CategoryTemplateId = 1, ParentCategoryId = 0, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = false, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,    
                    new Category() { Id = 10, Name = "Productos", Description = "Productos", CategoryTemplateId = 1, ParentCategoryId = 0, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = true, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,33,34,38,39,40,41,42,45,46,47,48,52,53,54,55,56,57,58,59,60,65,66,67,68,74,75,76,77,78,79,80,81,85,90,91,92,93,94,97,98,99,100,101,102"  } ,
                    new Category() { Id = 11, Name = "Cascos", Description = "Cascos", CategoryTemplateId = 1, ParentCategoryId = 10, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = true, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "12,13,14,15,16,17,18,19,20,21,22"  } ,
                    new Category() { Id = 12, Name = "Clasicos", Description = "Clasicos", CategoryTemplateId = 1, ParentCategoryId = 11, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,
                    new Category() { Id = 13, Name = "Jet", Description = "Jet", CategoryTemplateId = 1, ParentCategoryId = 11, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,
                    new Category() { Id = 14, Name = "OffRoad", Description = "OffRoad", CategoryTemplateId = 1, ParentCategoryId = 11, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,
                    new Category() { Id = 15, Name = "Abatibles", Description = "Abatibles", CategoryTemplateId = 1, ParentCategoryId = 11, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,
                    new Category() { Id = 16, Name = "Trail", Description = "Trail", CategoryTemplateId = 1, ParentCategoryId = 11, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,
                    new Category() { Id = 17, Name = "Integrales", Description = "Integrales", CategoryTemplateId = 1, ParentCategoryId = 11, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,
                    new Category() { Id = 18, Name = "Replicas", Description = "Replicas", CategoryTemplateId = 1, ParentCategoryId = 11, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,
                    new Category() { Id = 19, Name = "Accesorios para cascos", Description = "Accesorios para cascos", CategoryTemplateId = 1, ParentCategoryId = 11, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "20,21,22"  } ,
                    new Category() { Id = 20, Name = "Visores", Description = "Visores", CategoryTemplateId = 1, ParentCategoryId = 19, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,
                    new Category() { Id = 21, Name = "Gafas", Description = "Gafas", CategoryTemplateId = 1, ParentCategoryId = 19, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,
                    new Category() { Id = 22, Name = "Repuestos de Cascos", Description = "Repuestos de Cascos", CategoryTemplateId = 1, ParentCategoryId = 11, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,
                    new Category() { Id = 23, Name = "Ropa", Description = "Ropa", CategoryTemplateId = 1, ParentCategoryId = 10, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "24,25,33"  } ,
                    new Category() { Id = 24, Name = "Chaquetas", Description = "Chaquetas", CategoryTemplateId = 1, ParentCategoryId = 23, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,
                    new Category() { Id = 25, Name = "Pantalones", Description = "Pantalones", CategoryTemplateId = 1, ParentCategoryId = 23, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,
                    new Category() { Id = 26, Name = "XXXXXX", Description = "XXXXXX", CategoryTemplateId = 1, ParentCategoryId = 0, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = false, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,    
                    new Category() { Id = 27, Name = "XXXXXX", Description = "XXXXXX", CategoryTemplateId = 1, ParentCategoryId = 0, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = false, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,    
                    new Category() { Id = 28, Name = "XXXXXX", Description = "XXXXXX", CategoryTemplateId = 1, ParentCategoryId = 0, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = false, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,    
                    new Category() { Id = 29, Name = "Gorras", Description = "Gorras", CategoryTemplateId = 1, ParentCategoryId = 28, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,
                    new Category() { Id = 30, Name = "Buzos", Description = "Buzos", CategoryTemplateId = 1, ParentCategoryId = 28, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,
                    new Category() { Id = 31, Name = "Camisetas", Description = "Camisetas", CategoryTemplateId = 1, ParentCategoryId = 28, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,
                    new Category() { Id = 32, Name = "XXXXXX", Description = "XXXXXX", CategoryTemplateId = 1, ParentCategoryId = 0, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = true, HasDiscountsApplied = false, Published = false, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,
                    new Category() { Id = 33, Name = "Rodilleras", Description = "Rodilleras", CategoryTemplateId = 1, ParentCategoryId = 23, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,
                    new Category() { Id = 34, Name = "Guantes", Description = "Guantes", CategoryTemplateId = 1, ParentCategoryId = 10, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = true, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "38,39"  } ,
                    new Category() { Id = 35, Name = "XXXXXX", Description = "XXXXXX", CategoryTemplateId = 1, ParentCategoryId = 0, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = false, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,    
                    new Category() { Id = 36, Name = "XXXXXX", Description = "XXXXXX", CategoryTemplateId = 1, ParentCategoryId = 0, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = false, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,    
                    new Category() { Id = 37, Name = "XXXXXX", Description = "XXXXXX", CategoryTemplateId = 1, ParentCategoryId = 0, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = false, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,    
                    new Category() { Id = 38, Name = "Mujeres", Description = "Mujeres", CategoryTemplateId = 1, ParentCategoryId = 34, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,
                    new Category() { Id = 39, Name = "Electricos", Description = "Electricos", CategoryTemplateId = 1, ParentCategoryId = 34, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,
                    new Category() { Id = 40, Name = "Accesorios", Description = "Accesorios", CategoryTemplateId = 1, ParentCategoryId = 10, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "41,42,45,46,47,48,52,53,54,55,56,57,58,59,60,65"  } ,
                    new Category() { Id = 41, Name = "Seguridad", Description = "Seguridad", CategoryTemplateId = 1, ParentCategoryId = 40, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "42,45"  } ,
                    new Category() { Id = 42, Name = "Bloqueadores de disco", Description = "Bloqueadores de disco", CategoryTemplateId = 1, ParentCategoryId = 41, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,
                    new Category() { Id = 43, Name = "XXXXXX", Description = "XXXXXX", CategoryTemplateId = 1, ParentCategoryId = 0, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = false, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,    
                    new Category() { Id = 44, Name = "XXXXXX", Description = "XXXXXX", CategoryTemplateId = 1, ParentCategoryId = 0, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = false, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,    
                    new Category() { Id = 45, Name = "Anclas de tierra", Description = "Anclas de tierra", CategoryTemplateId = 1, ParentCategoryId = 41, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,
                    new Category() { Id = 46, Name = "Electrónicos", Description = "Electrónicos", CategoryTemplateId = 1, ParentCategoryId = 40, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "47,48,52"  } ,
                    new Category() { Id = 47, Name = "Intercomunicadores", Description = "Intercomunicadores", CategoryTemplateId = 1, ParentCategoryId = 46, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,
                    new Category() { Id = 48, Name = "GPS", Description = "GPS", CategoryTemplateId = 1, ParentCategoryId = 46, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,
                    new Category() { Id = 49, Name = "XXXXXX", Description = "XXXXXX", CategoryTemplateId = 1, ParentCategoryId = 0, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = false, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,    
                    new Category() { Id = 50, Name = "XXXXXX", Description = "XXXXXX", CategoryTemplateId = 1, ParentCategoryId = 0, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = false, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,    
                    new Category() { Id = 51, Name = "XXXXXX", Description = "XXXXXX", CategoryTemplateId = 1, ParentCategoryId = 0, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = false, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,    
                    new Category() { Id = 52, Name = "Alarmas", Description = "Alarmas", CategoryTemplateId = 1, ParentCategoryId = 46, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,
                    new Category() { Id = 53, Name = "Maleteria", Description = "Maleteria", CategoryTemplateId = 1, ParentCategoryId = 40, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "54,55,56,57,58,59,60"  } ,
                    new Category() { Id = 54, Name = "Alforjas", Description = "Alforjas", CategoryTemplateId = 1, ParentCategoryId = 53, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,
                    new Category() { Id = 55, Name = "Pulpos", Description = "Pulpos", CategoryTemplateId = 1, ParentCategoryId = 53, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,
                    new Category() { Id = 56, Name = "Tank Bags", Description = "Tank Bags", CategoryTemplateId = 1, ParentCategoryId = 53, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,
                    new Category() { Id = 57, Name = "Tail Bags", Description = "Tail Bags", CategoryTemplateId = 1, ParentCategoryId = 53, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,
                    new Category() { Id = 58, Name = "Baules y Maleteras", Description = "Baules y Maleteras", CategoryTemplateId = 1, ParentCategoryId = 53, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,
                    new Category() { Id = 59, Name = "Maletas", Description = "Maletas", CategoryTemplateId = 1, ParentCategoryId = 53, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,
                    new Category() { Id = 60, Name = "Otros maleteros", Description = "Otros maleteros", CategoryTemplateId = 1, ParentCategoryId = 53, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,
                    new Category() { Id = 61, Name = "XXXXXX", Description = "XXXXXX", CategoryTemplateId = 1, ParentCategoryId = 0, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = false, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,    
                    new Category() { Id = 62, Name = "XXXXXX", Description = "XXXXXX", CategoryTemplateId = 1, ParentCategoryId = 0, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = false, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,    
                    new Category() { Id = 63, Name = "XXXXXX", Description = "XXXXXX", CategoryTemplateId = 1, ParentCategoryId = 0, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = false, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,    
                    new Category() { Id = 64, Name = "XXXXXX", Description = "XXXXXX", CategoryTemplateId = 1, ParentCategoryId = 0, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = false, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,    
                    new Category() { Id = 65, Name = "Otros Accesorios", Description = "Otros Accesorios", CategoryTemplateId = 1, ParentCategoryId = 40, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,
                    new Category() { Id = 66, Name = "Herramientas", Description = "Herramientas", CategoryTemplateId = 1, ParentCategoryId = 10, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "67,68"  } ,
                    new Category() { Id = 67, Name = "Gatos", Description = "Gatos", CategoryTemplateId = 1, ParentCategoryId = 66, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,
                    new Category() { Id = 68, Name = "Cargadores de Bateria", Description = "Cargadores de Bateria", CategoryTemplateId = 1, ParentCategoryId = 66, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,
                    new Category() { Id = 69, Name = "XXXXXX", Description = "XXXXXX", CategoryTemplateId = 1, ParentCategoryId = 0, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = false, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,    
                    new Category() { Id = 70, Name = "XXXXXX", Description = "XXXXXX", CategoryTemplateId = 1, ParentCategoryId = 0, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = false, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,    
                    new Category() { Id = 71, Name = "XXXXXX", Description = "XXXXXX", CategoryTemplateId = 1, ParentCategoryId = 0, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = false, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,    
                    new Category() { Id = 72, Name = "XXXXXX", Description = "XXXXXX", CategoryTemplateId = 1, ParentCategoryId = 0, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = false, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,    
                    new Category() { Id = 73, Name = "XXXXXX", Description = "XXXXXX", CategoryTemplateId = 1, ParentCategoryId = 0, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = false, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,    
                    new Category() { Id = 74, Name = "Repuestos", Description = "Repuestos", CategoryTemplateId = 1, ParentCategoryId = 10, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "75,76,77,78,79,80,81,85,90,91,92"  } ,
                    new Category() { Id = 75, Name = "Frenos", Description = "Frenos", CategoryTemplateId = 1, ParentCategoryId = 74, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "76,77"  } ,
                    new Category() { Id = 76, Name = "Discos", Description = "Discos", CategoryTemplateId = 1, ParentCategoryId = 75, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,
                    new Category() { Id = 77, Name = "Pastillas", Description = "Pastillas", CategoryTemplateId = 1, ParentCategoryId = 75, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,
                    new Category() { Id = 78, Name = "Filtros", Description = "Filtros", CategoryTemplateId = 1, ParentCategoryId = 74, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "79,80"  } ,
                    new Category() { Id = 79, Name = "Filtros de Aceite", Description = "Filtros de Aceite", CategoryTemplateId = 1, ParentCategoryId = 78, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,
                    new Category() { Id = 80, Name = "Aire", Description = "Aire", CategoryTemplateId = 1, ParentCategoryId = 78, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,
                    new Category() { Id = 81, Name = "Kit de arrastre", Description = "Kit de arrastre", CategoryTemplateId = 1, ParentCategoryId = 74, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,
                    new Category() { Id = 82, Name = "Kits", Description = "Kits", CategoryTemplateId = 1, ParentCategoryId = 82, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,
                    new Category() { Id = 83, Name = "Piñones", Description = "Piñones", CategoryTemplateId = 1, ParentCategoryId = 82, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,
                    new Category() { Id = 84, Name = "Cadenas", Description = "Cadenas", CategoryTemplateId = 1, ParentCategoryId = 82, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,
                    new Category() { Id = 85, Name = "Baterias", Description = "Baterias", CategoryTemplateId = 1, ParentCategoryId = 74, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,
                    new Category() { Id = 86, Name = "XXXXXX", Description = "XXXXXX", CategoryTemplateId = 1, ParentCategoryId = 0, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = false, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,    
                    new Category() { Id = 87, Name = "XXXXXX", Description = "XXXXXX", CategoryTemplateId = 1, ParentCategoryId = 0, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = false, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,    
                    new Category() { Id = 88, Name = "XXXXXX", Description = "XXXXXX", CategoryTemplateId = 1, ParentCategoryId = 0, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = false, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,    
                    new Category() { Id = 89, Name = "XXXXXX", Description = "XXXXXX", CategoryTemplateId = 1, ParentCategoryId = 0, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = false, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,    
                    new Category() { Id = 90, Name = "Portaplacas", Description = "Portaplacas", CategoryTemplateId = 1, ParentCategoryId = 74, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,
                    new Category() { Id = 91, Name = "Llantas", Description = "Llantas", CategoryTemplateId = 1, ParentCategoryId = 74, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,
                    new Category() { Id = 92, Name = "Direccionales", Description = "Direccionales", CategoryTemplateId = 1, ParentCategoryId = 74, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,
                    new Category() { Id = 93, Name = "Lubricantes", Description = "Lubricantes", CategoryTemplateId = 1, ParentCategoryId = 10, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "94,97"  } ,
                    new Category() { Id = 94, Name = "Aceite", Description = "Aceite", CategoryTemplateId = 1, ParentCategoryId = 93, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,
                    new Category() { Id = 95, Name = "XXXXXX", Description = "XXXXXX", CategoryTemplateId = 1, ParentCategoryId = 0, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = false, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,    
                    new Category() { Id = 96, Name = "XXXXXX", Description = "XXXXXX", CategoryTemplateId = 1, ParentCategoryId = 0, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = false, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,    
                    new Category() { Id = 97, Name = "Liquido de frenos", Description = "Liquido de frenos", CategoryTemplateId = 1, ParentCategoryId = 93, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,
                    new Category() { Id = 98, Name = "Regalos", Description = "Regalos", CategoryTemplateId = 1, ParentCategoryId = 10, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "99,100,101,102"  } ,
                    new Category() { Id = 99, Name = "Libros", Description = "Libros", CategoryTemplateId = 1, ParentCategoryId = 98, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,
                    new Category() { Id = 100, Name = "Coleccionables", Description = "Coleccionables", CategoryTemplateId = 1, ParentCategoryId = 98, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,
                    new Category() { Id = 101, Name = "DVDs", Description = "DVDs", CategoryTemplateId = 1, ParentCategoryId = 98, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,
                    new Category() { Id = 102, Name = "Juguetes y Replicas", Description = "Juguetes y Replicas", CategoryTemplateId = 1, ParentCategoryId = 98, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,
                    new Category() { Id = 103, Name = "Servicios", Description = "Servicios", CategoryTemplateId = 1, ParentCategoryId = 0, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = true, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "104,105,106,107,108,112,113,114,115,116,117,120,121,122,123,124,125,126,129,133,134"  } ,
                    new Category() { Id = 104, Name = "Mecanica", Description = "Mecanica", CategoryTemplateId = 1, ParentCategoryId = 103, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "105,106,107,108,112,113,114,115"  } ,
                    new Category() { Id = 105, Name = "Revision preventiva", Description = "Revision preventiva", CategoryTemplateId = 1, ParentCategoryId = 104, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,
                    new Category() { Id = 106, Name = "Suspension", Description = "Suspension", CategoryTemplateId = 1, ParentCategoryId = 104, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,
                    new Category() { Id = 107, Name = "Mecánica para Frenos", Description = "Frenos", CategoryTemplateId = 1, ParentCategoryId = 104, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,
                    new Category() { Id = 108, Name = "Motor", Description = "Motor", CategoryTemplateId = 1, ParentCategoryId = 104, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "112,113,114"  } ,
                    new Category() { Id = 109, Name = "XXXXXX", Description = "XXXXXX", CategoryTemplateId = 1, ParentCategoryId = 0, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = false, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,    
                    new Category() { Id = 110, Name = "XXXXXX", Description = "XXXXXX", CategoryTemplateId = 1, ParentCategoryId = 0, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = false, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,    
                    new Category() { Id = 111, Name = "XXXXXX", Description = "XXXXXX", CategoryTemplateId = 1, ParentCategoryId = 0, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = false, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,    
                    new Category() { Id = 112, Name = "Cambios de filtro", Description = "Cambios de filtro", CategoryTemplateId = 1, ParentCategoryId = 108, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,
                    new Category() { Id = 113, Name = "Cambio de cadena de repartición", Description = "Cambio de cadena de repartición", CategoryTemplateId = 1, ParentCategoryId = 108, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,
                    new Category() { Id = 114, Name = "Reparación caja de velocidad", Description = "Reparación caja de velocidad", CategoryTemplateId = 1, ParentCategoryId = 108, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,
                    new Category() { Id = 115, Name = "Cambio de guayas", Description = "Cambio de guayas", CategoryTemplateId = 1, ParentCategoryId = 104, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,
                    new Category() { Id = 116, Name = "Pintura", Description = "Pintura", CategoryTemplateId = 1, ParentCategoryId = 103, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "117,120"  } ,
                    new Category() { Id = 117, Name = "Pintura general", Description = "Pintura general", CategoryTemplateId = 1, ParentCategoryId = 116, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,
                    new Category() { Id = 118, Name = "XXXXXX", Description = "XXXXXX", CategoryTemplateId = 1, ParentCategoryId = 0, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = false, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,    
                    new Category() { Id = 119, Name = "XXXXXX", Description = "XXXXXX", CategoryTemplateId = 1, ParentCategoryId = 0, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = false, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,    
                    new Category() { Id = 120, Name = "Calcomanias", Description = "Calcomanias", CategoryTemplateId = 1, ParentCategoryId = 116, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,
                    new Category() { Id = 121, Name = "Latonería", Description = "Latonería", CategoryTemplateId = 1, ParentCategoryId = 103, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "122,123"  } ,
                    new Category() { Id = 122, Name = "Reparación de pastas", Description = "Reparación de pastas", CategoryTemplateId = 1, ParentCategoryId = 121, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,
                    new Category() { Id = 123, Name = "Cambios de pastas", Description = "Cambios de pastas", CategoryTemplateId = 1, ParentCategoryId = 121, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,
                    new Category() { Id = 124, Name = "Electronica y Electricidad", Description = "Electronica y Electricidad", CategoryTemplateId = 1, ParentCategoryId = 103, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "125,126"  } ,
                    new Category() { Id = 125, Name = "Instalación de alarmas", Description = "Instalación de alarmas", CategoryTemplateId = 1, ParentCategoryId = 124, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,
                    new Category() { Id = 126, Name = "Instalar GPS", Description = "Instalar GPS", CategoryTemplateId = 1, ParentCategoryId = 124, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,
                    new Category() { Id = 127, Name = "XXXXXX", Description = "XXXXXX", CategoryTemplateId = 1, ParentCategoryId = 0, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = false, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,    
                    new Category() { Id = 128, Name = "XXXXXX", Description = "XXXXXX", CategoryTemplateId = 1, ParentCategoryId = 0, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = false, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,    
                    new Category() { Id = 129, Name = "Instalación de accesorios", Description = "Instalación de accesorios", CategoryTemplateId = 1, ParentCategoryId = 103, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "133,134"  } ,
                    new Category() { Id = 130, Name = "XXXXXX", Description = "XXXXXX", CategoryTemplateId = 1, ParentCategoryId = 0, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = false, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,    
                    new Category() { Id = 131, Name = "XXXXXX", Description = "XXXXXX", CategoryTemplateId = 1, ParentCategoryId = 0, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = false, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,    
                    new Category() { Id = 132, Name = "XXXXXX", Description = "XXXXXX", CategoryTemplateId = 1, ParentCategoryId = 0, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = false, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,    
                    new Category() { Id = 133, Name = "Instalacion de Maleteros", Description = "Maleteros", CategoryTemplateId = 1, ParentCategoryId = 129, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,
                    new Category() { Id = 134, Name = "Instalacion de Alforjas", Description = "Alforjas", CategoryTemplateId = 1, ParentCategoryId = 129, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,
                    new Category() { Id = 135, Name = "Motos", Description = "Motos", CategoryTemplateId = 1, ParentCategoryId = 0, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "136,138,143,144,145,146,147,148,149,150,151,152,153,154,155,156,157,158,159,160,161,162,163,164,165,166,167,168,169,170,171,172,173,174,175,176,177,178,179,180,181,182,183,184,185,186,187,188,189,190,191,192,193,194,195,196,197,198,199,200,201,202,203,204,205,206,207,208,209,210,211,212,213,214,215,216,217,218,219,220,221,222,223,224,225,226,227,228,229,230,231,232,233,234,235,236,237,238,239,240,241,242,243,244,245,246,247,248,249,250,251,252,253,254,255,256,257,258,259,260,261,262,263,264,265,266,267,268,269,270,271,272,273,274,275,276,277,278,279,280,281,282,283,284,285,286,287,288,289,290,291,292,293,294,295,296,297,298,299,300,301,302,303,304,305,306,307,308,309,310,311,312,313,314,315,316,317,318,319,320,321,322,323,324,325,326,327,328,329,330,331,332,333,334,335,336,337,338,339,340,341,342,343,344,345,346,347,348,349,350,351,352,353,354,355,356,357,358,359,360,361,362,363,364,365,366,367,368,369,370,371,372,373,374,375,376,377,378,379,380,381,382,383,384,385,386,387,388,389,390,391,392,393,394,395,396,397,398,399,400,401,402,403,404,405,406,407,408,409,410,411,412,413,414,415,416,417,418,419,420,421,422,423,424,425,426,427,428,429,430,431,432,433,434,435,436,437,438,439,440,441,442,443,444,457,458,459,460,461,462,463,464,465,466,467,468,469,470,471,472,473,474,475,476,477,478,479,480,481,482,483,484,485,486,487,488,489,490,491,492,493,494,495,496,497,498,499,500,501,502,503,504,505,506,507,508,509,510,511,512,513,514,515,516,517,518,519,520,521,522,523,524,525,526,527,528,529,530,531,532,533,534,535,536,537,538,539,540,541,542,543,544,545,546,547,548,549,550,551,552,553,554,555,556,557,558,559,560,561,562,563,564,565,566,567,568,569,570,571,572,573,574,575,576,577,578,579,580,581,582,583,584,585,586,587,588,589,590,591,592,593,594,595,596,597,598,599,600,601,602,603,604,605,606,607,608,609,610,611,612,613,614,615,616,617,618,619,620,621,622,623,624,625,626,627,628,629,630,631,632,633,634,635,636,637,638,639,640,641,642,643,644,645,646,647,648,649,650,651,652,653,654,655,656,657,658,659,660,661,662,663,664,665,666,667,668,669,670,671,672,673,674,675,676,677,678,679,680,681,682,683,684,685,686,687,688,689,690,691,692,693,694,695,696,697,698,699,700,701,702,703,704,705,706,707,708,709,710,711,712,713,714,715,716,717,718,719,720,721,722,723,724,725,726,727,728,729,730,731,732,733,734,735,736,737,738,739,740,741,742,743,744,745,746,747,748,749,750,751,752,753,754,755,756,757,758,759,760,761,762,763,764,765,766,139,767,768,769,770,771,772,773,774,775,776,777,778,779,780,781,782,783,784,785,786,787,788,789,790,791,792,793,794,795,796,797,798,799,800,801,802,803,804,805,806,807,808,809,810,811,812,813,814,815,816,817,818,819,820,821,822,823,824,825,826,827,828,829,830,831,832,833,834,835,836,837,838,839,840,841,842,843,844,845,846,847,848,849,850,851,852,853,854,855,856,857,858,859,860,861,862,863,864,865,866,867,868,869,870,871,872,873,874,875,876,877,878,879,880,881,882,883,884,885,886,887,888,889,890,891,892,893,894,895,896,897,898,899,900,901,902,903,904,905,906,907,908,909,910,911,912,913,914,915,916,917,918,919,920,921,922,923,924,925,926,927,928,929,930,931,932,933,934,935,936,937,938,939,940,941,942,943,944,945,946,947,948,949,950,951,952,953,954,955,956,957,958,959,960,961,962,963,964,965,966,967,968,969,970,971,972,973,974,975,976,977,978,979,980,981,982,983,984,985,986,987,988,989,990,991,992,993,994,995,996,997,998,999,1000,1001,1002,1003,1004,1005,1006,1007,1008,1009,1010,1011,1012,1013,1014,1015,1016,1017,1018,1019,1020,1021,1022,1023,1024,1025,1026,1027,1028,1029,1030,1031,1032,1033,1034,1035,1036,1037,1038,1039,1040,1041,1042,1043,1044,1045,1046,1047,1048,1049,1050,1051,1052,1053,1054,1055,1056,1057,1058,1059,1060,1061,1062,1063,1064,1065,1066,1067,1068,1069,1070,1071,1072,1073,1074,1075,1076,1077,1078,1079,1080,1081,1082,1083,1084,1085,1086,1087,1088,1089,1090,1091,1092,1093,1094,1095,1096,1097,1098,1099,1100,1101,1102,1103,1104,1105,1106,1107,1108,1109,1110,1111,1112,1113,1114,1115,1116,1117,1118,1119,1120,1121,1122,1123,1124,1125,1126,1127,1128,1129,1130,1131,1132,1133,1134,1135,1136,1137,1138,1139,1140,1141,1142,1143,1144,1145,1146,1147,1148,1149,1150,1151,1152,1153,1154,1155,1156,1157,1158,1159,1160,1161,1162,1163,1164,1165,1166,1167,1168,1169,1170,1171,1172,1173,1174,1175,1176,1177,1178,1179,1180,1181,1182,1183,1184,1185,1186,1187,1188,1189,1190,1191,1192,1193,1194,1195,1196,1197,1198,1199,1200,1201,1202,1203,1204,1205,1206,1207,1208,1209,1210,1211,1212,1213,1214,1215,1216,1217,1218,1219,1220,1221,1222,1223,1224,1225,1226,1227,1228,1229,1230,1231,1232,1233,1234,1235,1236,1237,1238,1239,1240,1241,1242,1243,1244,1245,1246,1247,1248,1249,1250,1251,1252,1253,1254,1255,1256,1257,1258,1259,1260,1261,1262,1263,1264,1265,1266,1267,1268,1269,1270,1271,1272,1273,1274,1275,1276,1277,1278,1279,1280,1281,1282,1283,1284,1285,1286,1287,1288,1289,1290,1291,1292,1293,1294,1295,1296,1297,1298,1299,1300,1301,1302,1303,1304,1305,1306,1307,1308,1309,1310,1311,1312,1313,1314,1315,1316,1317,1318,1319,1320,1321,1322,1323,1324,1325,1326,1327,1328,1329,1330,1331,1332,1333,1334,1335,1336,1337,1338,1339,1340,1341,1342,1343,1344,1345,1346,1347,1348,1349,1350,1351,1352,1353,1354,1355,1356,1357,1358,1359,1360,1361,1362,1363,1364,1365,1366,1367,1368,1369,1370,1371,1372,1373,1374,1375,1376,1377,1378,1379,1380,1381,1382,1383,1384,1590,137,445,446,447,448,449,450,451,452,453,454,455,456,1591,1592,1593,1594,1595,1596,1597,1598,1599,1600,1601,1602,1603,1604,1605,1606,1607,1608,1609,1610,1611,1612,1613,1614,1615,1616,1617,1618,1619,1620,1621,1622,1623,1624,1625,1626,1627,1628,1629,1630,1631,1632,1633,1634,1635,1636,1637,1638,1639,1640,1641,1642,1643,1644,1645,1646,1647,1648,1649,1650,1651,1652,1653,1654,1655,1656,1657,1658,1659,1660,1661,1662,1663,1664,1665,1666,1667,1668,1669,1670,1671,1672,1673,1674,1675,1676,1677,1678,1679,1680,1681,1682,1683,1684,1685,1686,1687,1688,1689,1690,1691,1692,1693,1694,1695,1696,1697,1698,1699,1700,1701,1702,1713,1703,1704,1705,1706,142,1707,1708,1709,1710,1711,140,141,1712"  } ,
                    new Category() { Id = 136, Name = "Otras Marcas", Description = "Otras Marcas", CategoryTemplateId = 1, ParentCategoryId = 135, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,
                    new Category() { Id = 137, Name = "Yamaha", Description = "Yamaha", CategoryTemplateId = 1, ParentCategoryId = 135, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "140,141"  } ,
                    new Category() { Id = 138, Name = "Yakima", Description = "Yakima", CategoryTemplateId = 1, ParentCategoryId = 135, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,
                    new Category() { Id = 139, Name = "Vento", Description = "Vento", CategoryTemplateId = 1, ParentCategoryId = 135, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,
                    new Category() { Id = 140, Name = "United Motors", Description = "United Motors", CategoryTemplateId = 1, ParentCategoryId = 135, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,
                    new Category() { Id = 141, Name = "Triumph", Description = "Triumph", CategoryTemplateId = 1, ParentCategoryId = 135, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,
                    new Category() { Id = 142, Name = "Suzuki", Description = "Suzuki", CategoryTemplateId = 1, ParentCategoryId =135, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "142"  } ,
                    new Category() { Id = 143, Name = "Sachs", Description = "Sachs", CategoryTemplateId = 1, ParentCategoryId = 135, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,
                    new Category() { Id = 144, Name = "Royal Enfield", Description = "Royal Enfield", CategoryTemplateId = 1, ParentCategoryId = 135, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,
                    new Category() { Id = 145, Name = "Piaggio", Description = "Piaggio", CategoryTemplateId = 1, ParentCategoryId = 135, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,
                    new Category() { Id = 146, Name = "Kymco", Description = "Kymco", CategoryTemplateId = 1, ParentCategoryId = 135, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "1713"  } ,
                    new Category() { Id = 147, Name = "KTM", Description = "KTM", CategoryTemplateId = 1, ParentCategoryId = 135, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,
                    new Category() { Id = 148, Name = "Kawasaki", Description = "<p>Kawasaki</p>", CategoryTemplateId = 1, ParentCategoryId = 135, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "137,445,446,447,448,449,450,451,452,453,454,455,456,1591,1592,1593,1594,1595,1596,1597,1598,1599,1600,1601,1602,1603,1604,1605,1606,1607,1608,1609,1610,1611,1612,1613,1614,1615,1616,1617,1618,1619,1620,1621,1622,1623,1624,1625,1626,1627,1628,1629,1630,1631,1632,1633,1634,1635,1636,1637,1638,1639,1640,1641,1642,1643,1644,1645,1646,1647,1648,1649,1650,1651,1652,1653,1654,1655,1656,1657,1658,1659,1660,1661,1662,1663,1664,1665,1666,1667,1668,1669,1670,1671,1672,1673,1674,1675,1676,1677,1678,1679,1680,1681,1682,1683,1684,1685,1686,1687,1688,1689,1690,1691,1692,1693,1694,1695,1696,1697,1698,1699,1700"  } ,
                    new Category() { Id = 149, Name = "Honda", Description = "Honda", CategoryTemplateId = 1, ParentCategoryId = 135, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = ""  } ,
                    new Category() { Id = 150, Name = "Harley Davidson", Description = "Harley Davidson", CategoryTemplateId = 1, ParentCategoryId = 135, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "1282,1283,1284,1285,1286,1287,1288,1289,1290,1291,1292,1293,1294,1295,1296,1297,1298,1299,1300,1301,1302,1303,1304,1305,1306,1307,1308,1309,1310,1311,1312,1313,1314,1315,1316,1317,1318,1319,1320,1321,1322,1323,1324,1325,1326,1327,1328,1329,1330,1331,1332,1333,1334,1335,1336,1337,1338,1339,1340,1341,1342,1343,1344,1345,1346,1347,1348,1349,1350,1351,1352,1353,1354,1355,1356,1357,1358,1359,1360,1361,1362,1363,1364,1365,1366,1367,1368,1369,1370,1371,1372,1373,1374,1375,1376,1377,1378,1379,1380,1381,1382,1383"  } ,
                    new Category() { Id = 151, Name = "Gasgas", Description = "Gasgas", CategoryTemplateId = 1, ParentCategoryId = 135, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "1179,1180,1181,1182,1183,1184,1185,1186,1187,1188,1189,1190,1191,1192,1193,1194,1195,1196,1197,1198,1199,1200,1201,1202,1203,1204,1205,1206,1207,1208,1209,1210,1211,1212,1213,1214,1215,1216,1217,1218,1219,1220,1221,1222,1223,1224,1225,1226,1227,1228,1229,1230,1231,1232,1233,1234,1235,1236,1237,1238,1239,1240,1241,1242,1243,1244,1245,1246,1247,1248,1249,1250,1251,1252,1253,1254,1255,1256,1257,1258,1259,1260,1261,1262,1263,1264,1265,1266,1267,1268,1269,1270,1271,1272,1273,1274,1275,1276,1277,1278,1279,1280"  } ,
                    new Category() { Id = 152, Name = "Ducati", Description = "<p>Ducati</p>", CategoryTemplateId = 1, ParentCategoryId = 135, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "1076,1077,1078,1079,1080,1081,1082,1083,1084,1085,1086,1087,1088,1089,1090,1091,1092,1093,1094,1095,1096,1097,1098,1099,1100,1101,1102,1103,1104,1105,1106,1107,1108,1109,1110,1111,1112,1113,1114,1115,1116,1117,1118,1119,1120,1121,1122,1123,1124,1125,1126,1127,1128,1129,1130,1131,1132,1133,1134,1135,1136,1137,1138,1139,1140,1141,1142,1143,1144,1145,1146,1147,1148,1149,1150,1151,1152,1153,1154,1155,1156,1157,1158,1159,1160,1161,1162,1163,1164,1165,1166,1167,1168,1169,1170,1171,1172,1173,1174,1175,1176,1177"  } ,
                    new Category() { Id = 153, Name = "Cagiva", Description = "Cagiva", CategoryTemplateId = 1, ParentCategoryId = 135, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "973,974,975,976,977,978,979,980,981,982,983,984,985,986,987,988,989,990,991,992,993,994,995,996,997,998,999,1000,1001,1002,1003,1004,1005,1006,1007,1008,1009,1010,1011,1012,1013,1014,1015,1016,1017,1018,1019,1020,1021,1022,1023,1024,1025,1026,1027,1028,1029,1030,1031,1032,1033,1034,1035,1036,1037,1038,1039,1040,1041,1042,1043,1044,1045,1046,1047,1048,1049,1050,1051,1052,1053,1054,1055,1056,1057,1058,1059,1060,1061,1062,1063,1064,1065,1066,1067,1068,1069,1070,1071,1072,1073,1074"  } ,
                    new Category() { Id = 154, Name = "Buell", Description = "Buell", CategoryTemplateId = 1, ParentCategoryId = 135, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "870,871,872,873,874,875,876,877,878,879,880,881,882,883,884,885,886,887,888,889,890,891,892,893,894,895,896,897,898,899,900,901,902,903,904,905,906,907,908,909,910,911,912,913,914,915,916,917,918,919,920,921,922,923,924,925,926,927,928,929,930,931,932,933,934,935,936,937,938,939,940,941,942,943,944,945,946,947,948,949,950,951,952,953,954,955,956,957,958,959,960,961,962,963,964,965,966,967,968,969,970,971"  } ,
                    new Category() { Id = 155, Name = "Bajaj", Description = "Bajaj", CategoryTemplateId = 1, ParentCategoryId = 135, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "139,767,768,769,770,771,772,773,774,775,776,777,778,779,780,781,782,783,784,785,786,787,788,789,790,791,792,793,794,795,796,797,798,799,800,801,802,803,804,805,806,807,808,809,810,811,812,813,814,815,816,817,818,819,820,821,822,823,824,825,826,827,828,829,830,831,832,833,834,835,836,837,838,839,840,841,842,843,844,845,846,847,848,849,850,851,852,853,854,855,856,857,858,859,860,861,862,863,864,865,866,867,868"  } ,
                    new Category() { Id = 156, Name = "BMW", Description = "BMW", CategoryTemplateId = 1, ParentCategoryId = 135, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "664,665,666,667,668,669,670,671,672,673,674,675,676,677,678,679,680,681,682,683,684,685,686,687,688,689,690,691,692,693,694,695,696,697,698,699,700,701,702,703,704,705,706,707,708,709,710,711,712,713,714,715,716,717,718,719,720,721,722,723,724,725,726,727,728,729,730,731,732,733,734,735,736,737,738,739,740,741,742,743,744,745,746,747,748,749,750,751,752,753,754,755,756,757,758,759,760,761,762,763,764,765"  } ,
                    new Category() { Id = 157, Name = "Ayco", Description = "Ayco", CategoryTemplateId = 1, ParentCategoryId = 135, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "561,562,563,564,565,566,567,568,569,570,571,572,573,574,575,576,577,578,579,580,581,582,583,584,585,586,587,588,589,590,591,592,593,594,595,596,597,598,599,600,601,602,603,604,605,606,607,608,609,610,611,612,613,614,615,616,617,618,619,620,621,622,623,624,625,626,627,628,629,630,631,632,633,634,635,636,637,638,639,640,641,642,643,644,645,646,647,648,649,650,651,652,653,654,655,656,657,658,659,660,661,662"  } ,
                    new Category() { Id = 158, Name = "Aprilia", Description = "Aprilia", CategoryTemplateId = 1, ParentCategoryId = 135, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "458,459,460,461,462,463,464,465,466,467,468,469,470,471,472,473,474,475,476,477,478,479,480,481,482,483,484,485,486,487,488,489,490,491,492,493,494,495,496,497,498,499,500,501,502,503,504,505,506,507,508,509,510,511,512,513,514,515,516,517,518,519,520,521,522,523,524,525,526,527,528,529,530,531,532,533,534,535,536,537,538,539,540,541,542,543,544,545,546,547,548,549,550,551,552,553,554,555,556,557,558,559"  } ,
                    new Category() { Id = 159, Name = "AKT", Description = "AKT", CategoryTemplateId = 1, ParentCategoryId = 135, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "240,241,242,243,244,245,246,247,248,249,250,251,252,253,254,255,256,257,258,259,260,261,262,263,264,265,266,267,268,269,270,271,272,273,274,275,276,277,278,279,280,281,282,283,284,285,286,287,288,289,290,291,292,293,294,295,296,297,298,299,300,301,302,303,304,305,306,307,308,309,310,311,312,313,314,315,316,317,318,319,320,321,322,323,324,325,326,327,328,329,330,331,332,333,334,335,336,337,338,339,340,341,342,343,344,345,346,347,348,349,350,351,352,353,354,355,356,357,358,359,360,361,362,363,364,365,366,367,368,369,370,371,372,373,374,375,376,377,378,379,380,381,382,383,384,385,386,387,388,389,390,391,392,393,394,395,396,397,398,399,400,401,402,403,404,405,406,407,408,409,410,411,412,413,414,415,416,417,418,419,420,421,422,423,424,425,426,427,428,429,430,431,432,433,434,435,436,437,438,439,440,441,442,443,444"  } ,
                    new Category() { Id = 160, Name = "Agusta 3", Description = "Agusta 3", CategoryTemplateId = 1, ParentCategoryId = 135, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "138,143,144,145,146,147,148,149,150,151,152,153,154,155,156,157,158,159,160,161,162,163,164,165,166,167,168,169,170,171,172,173,174,175,176,177,178,179,180,181,182,183,184,185,186,187,188,189,190,191,192,193,194,195,196,197,198,199,200,201,202,203,204,205,206,207,208,209,210,211,212,213,214,215,216,217,218,219,220,221,222,223,224,225,226,227,228,229,230,231,232,233,234,235,236,237,238"  } ,
                    ////CATEGORIAS DE YAMAHA
                    new Category() { Id = 161, Name = "Yamaha Bolt", Description = "Bolt", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 5  } ,
                    new Category() { Id = 162, Name = "Yamaha Bws", Description = "Bws", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 9  } ,
                    new Category() { Id = 163, Name = "Yamaha Calimatic", Description = "Calimatic", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 8  } ,
                    new Category() { Id = 164, Name = "Yamaha Dragstar", Description = "Dragstar", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 5  } ,
                    new Category() { Id = 165, Name = "Yamaha Dt", Description = "Dt", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 8  } ,
                    new Category() { Id = 166, Name = "Yamaha Dt-125", Description = "Dt-125", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 8  } ,
                    new Category() { Id = 167, Name = "Yamaha FZ8", Description = "FZ8", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 7  } ,
                    new Category() { Id = 168, Name = "Yamaha FZ8 n", Description = "FZ8 n", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 7  } ,
                    new Category() { Id = 169, Name = "Yamaha FZ8 s", Description = "FZ8 s", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 7  } ,
                    new Category() { Id = 170, Name = "Yamaha Fazer 16", Description = "Fazer 16", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 6  } ,
                    new Category() { Id = 171, Name = "Yamaha Fazer 600", Description = "Fazer 600", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 7  } ,
                    new Category() { Id = 172, Name = "Yamaha Fino", Description = "Fino", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 9  } ,
                    new Category() { Id = 173, Name = "Yamaha Fz 16", Description = "Fz 16", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 6  } ,
                    new Category() { Id = 174, Name = "Yamaha Fz 1", Description = "Fz 1", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 7  } ,
                    new Category() { Id = 175, Name = "Yamaha Fz 1 n", Description = "Fz 1 n", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 7  } ,
                    new Category() { Id = 176, Name = "Yamaha Fz 1 s", Description = "Fz 1 s", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 7  } ,
                    new Category() { Id = 177, Name = "Yamaha Fz 6", Description = "Fz 6", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 7  } ,
                    new Category() { Id = 178, Name = "Yamaha Fz 6 S", Description = "Fz 6 S", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 7  } ,
                    new Category() { Id = 179, Name = "Yamaha Grizzly", Description = "Grizzly", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  } ,
                    new Category() { Id = 180, Name = "Yamaha Monoshock", Description = "Monoshock", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 8  } ,
                    new Category() { Id = 181, Name = "Yamaha Mt 07", Description = "Mt 07", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 7  } ,
                    new Category() { Id = 182, Name = "Yamaha Mt 09", Description = "Mt 09", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 7  } ,
                    new Category() { Id = 183, Name = "Yamaha R 1", Description = "R 1", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 7  } ,
                    new Category() { Id = 184, Name = "Yamaha R 15", Description = "R 15", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 7  } ,
                    new Category() { Id = 185, Name = "Yamaha R6", Description = "R6", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 7  } ,
                    new Category() { Id = 186, Name = "Yamaha R6 r", Description = "R6 r", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 7  } ,
                    new Category() { Id = 187, Name = "Yamaha R6 s", Description = "R6 s", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 7  } ,
                    new Category() { Id = 188, Name = "Yamaha R3", Description = "R3", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 7  } ,
                    new Category() { Id = 189, Name = "Yamaha Raider", Description = "Raider", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 5  } ,
                    new Category() { Id = 190, Name = "Yamaha Raptor", Description = "Raptor", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  } ,
                    new Category() { Id = 191, Name = "Yamaha Raptor 250", Description = "Raptor 250", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  } ,
                    new Category() { Id = 192, Name = "Yamaha Raptor 700 r", Description = "Raptor 700 r", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  } ,
                    new Category() { Id = 193, Name = "Yamaha Raptor 80", Description = "Raptor 80", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  } ,
                    new Category() { Id = 194, Name = "Yamaha Rx", Description = "Rx", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 6  } ,
                    new Category() { Id = 195, Name = "Yamaha Super Tenere", Description = "Super Tenere", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 10  } ,
                    new Category() { Id = 196, Name = "Yamaha Sz", Description = "Sz", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 6  } ,
                    new Category() { Id = 197, Name = "Yamaha Tdm 850", Description = "Tdm 850", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 10  } ,
                    new Category() { Id = 198, Name = "Yamaha Tdm 900", Description = "Tdm 900", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 10  } ,
                    new Category() { Id = 199, Name = "Yamaha V", Description = "V", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 5  } ,
                    new Category() { Id = 200, Name = "Yamaha Virago", Description = "Virago", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 6  } ,
                    new Category() { Id = 201, Name = "Yamaha V star", Description = "V star", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 5  } ,
                    new Category() { Id = 202, Name = "Yamaha Vulcan", Description = "Vulcan", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 5  } ,
                    new Category() { Id = 203, Name = "Yamaha Wolverine", Description = "Wolverine", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  } ,
                    new Category() { Id = 204, Name = "Yamaha Wr", Description = "Wr", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 8  } ,
                    new Category() { Id = 205, Name = "Yamaha Xj 6", Description = "Xj 6", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 10  } ,
                    new Category() { Id = 206, Name = "Yamaha Xj 6 f", Description = "Xj 6 f", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 10  } ,
                    new Category() { Id = 207, Name = "Yamaha Xj 6 n", Description = "Xj 6 n", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 10  } ,
                    new Category() { Id = 208, Name = "Yamaha Xt", Description = "Xt", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 8  } ,
                    new Category() { Id = 209, Name = "Yamaha Xt 1200", Description = "Xt 1200", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 8  } ,
                    new Category() { Id = 210, Name = "Yamaha Xt 225", Description = "Xt 225", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 8  } ,
                    new Category() { Id = 211, Name = "Yamaha Xt 500", Description = "Xt 500", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 8  } ,
                    new Category() { Id = 212, Name = "Yamaha Xt 600", Description = "Xt 600", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 8  } ,
                    new Category() { Id = 213, Name = "Yamaha Xt 660", Description = "Xt 660", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 8  } ,
                    new Category() { Id = 214, Name = "Yamaha Xtz", Description = "Xtz", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 8  } ,
                    new Category() { Id = 215, Name = "Yamaha Xv", Description = "Xv", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 5  } ,
                    new Category() { Id = 216, Name = "Yamaha Ybr", Description = "Ybr", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 6  } ,
                    new Category() { Id = 217, Name = "Yamaha Ybr 125", Description = "Ybr 125", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 6  } ,
                    new Category() { Id = 218, Name = "Yamaha Ybr 250", Description = "Ybr 250", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 6  } ,
                    new Category() { Id = 219, Name = "Yamaha Yfm", Description = "Yfm", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  } ,
                    new Category() { Id = 220, Name = "Yamaha Yfz", Description = "Yfz", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  } ,
                    new Category() { Id = 221, Name = "Yamaha Yz", Description = "Yz", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  } ,
                    new Category() { Id = 222, Name = "Exosto", Description = "Exosto", CategoryTemplateId = 1, ParentCategoryId = 74, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    ///SEGUNDA MIGRACIÓN
                    new Category() { Id =223, Name = "Akt Rtx 150", Description = "Akt Rtx 150", CategoryTemplateId = 1, ParentCategoryId = 159, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =224, Name = "Akt TT", Description = "Akt TT", CategoryTemplateId = 1, ParentCategoryId = 159, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =225, Name = "Akt Apache", Description = "Akt Apache", CategoryTemplateId = 1, ParentCategoryId = 159, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =226, Name = "Akt Carguero", Description = "Akt Carguero", CategoryTemplateId = 1, ParentCategoryId = 159, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =227, Name = "Akt Dynamic", Description = "Akt Dynamic", CategoryTemplateId = 1, ParentCategoryId = 159, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =228, Name = "Akt Evo R3", Description = "Akt Evo R3", CategoryTemplateId = 1, ParentCategoryId = 159, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =229, Name = "Akt Jet", Description = "Akt Jet", CategoryTemplateId = 1, ParentCategoryId = 159, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =230, Name = "Akt SL 125", Description = "Akt SL 125", CategoryTemplateId = 1, ParentCategoryId = 159, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =231, Name = "Akt Special", Description = "Akt Special", CategoryTemplateId = 1, ParentCategoryId = 159, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =232, Name = "Akt XM 180", Description = "Akt XM 180", CategoryTemplateId = 1, ParentCategoryId = 159, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =233, Name = "Aprilia Caponord Travel Pack 1200cc", Description = "Aprilia Caponord Travel Pack 1200cc", CategoryTemplateId = 1, ParentCategoryId = 158, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =234, Name = "Aprilia Dorsoduro", Description = "Aprilia Dorsoduro", CategoryTemplateId = 1, ParentCategoryId = 158, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =235, Name = "Aprilia Etx", Description = "Aprilia Etx", CategoryTemplateId = 1, ParentCategoryId = 158, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =236, Name = "Aprilia Shiver", Description = "Aprilia Shiver", CategoryTemplateId = 1, ParentCategoryId = 158, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =237, Name = "Bajaj Avenger", Description = "Bajaj Avenger", CategoryTemplateId = 1, ParentCategoryId = 155, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =238, Name = "Bajaj Boxer", Description = "Bajaj Boxer", CategoryTemplateId = 1, ParentCategoryId = 155, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =239, Name = "Bajaj Discover", Description = "Bajaj Discover", CategoryTemplateId = 1, ParentCategoryId = 155, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =240, Name = "Bajaj Platina", Description = "Bajaj Platina", CategoryTemplateId = 1, ParentCategoryId = 155, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =241, Name = "Bajaj Pulsar 135", Description = "Bajaj Pulsar 135", CategoryTemplateId = 1, ParentCategoryId = 155, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =242, Name = "Bajaj Pulsar 180", Description = "Bajaj Pulsar 180", CategoryTemplateId = 1, ParentCategoryId = 155, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =243, Name = "Bajaj Pulsar 200", Description = "Bajaj Pulsar 200", CategoryTemplateId = 1, ParentCategoryId = 155, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =244, Name = "Bajaj Pulsar 200 Ns", Description = "Bajaj Pulsar 200 Ns", CategoryTemplateId = 1, ParentCategoryId = 155, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =245, Name = "Bajaj Pulsar 220", Description = "Bajaj Pulsar 220", CategoryTemplateId = 1, ParentCategoryId = 155, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =246, Name = "Bmw Adventure", Description = "Bmw Adventure", CategoryTemplateId = 1, ParentCategoryId = 156, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =247, Name = "Bmw C", Description = "Bmw C", CategoryTemplateId = 1, ParentCategoryId = 156, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =248, Name = "Bmw F", Description = "Bmw F", CategoryTemplateId = 1, ParentCategoryId = 156, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =249, Name = "Bmw Gs", Description = "Bmw Gs", CategoryTemplateId = 1, ParentCategoryId = 156, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =250, Name = "Bmw Hp4", Description = "Bmw Hp4", CategoryTemplateId = 1, ParentCategoryId = 156, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =251, Name = "Bmw K1200 R", Description = "Bmw K1200 R", CategoryTemplateId = 1, ParentCategoryId = 156, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =252, Name = "Bmw R", Description = "Bmw R", CategoryTemplateId = 1, ParentCategoryId = 156, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =253, Name = "Bmw Sertao 650", Description = "Bmw Sertao 650", CategoryTemplateId = 1, ParentCategoryId = 156, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =254, Name = "Bmw Touring", Description = "Bmw Touring", CategoryTemplateId = 1, ParentCategoryId = 156, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =255, Name = "Ducati 848", Description = "Ducati 848", CategoryTemplateId = 1, ParentCategoryId = 152, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =256, Name = "Ducati 848 Corse", Description = "Ducati 848 Corse", CategoryTemplateId = 1, ParentCategoryId = 152, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =257, Name = "Ducati 889", Description = "Ducati 889", CategoryTemplateId = 1, ParentCategoryId = 152, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =258, Name = "Ducati Diavel", Description = "Ducati Diavel", CategoryTemplateId = 1, ParentCategoryId = 152, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =259, Name = "Ducati Diavel Carbon", Description = "Ducati Diavel Carbon", CategoryTemplateId = 1, ParentCategoryId = 152, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =260, Name = "Ducati Hipermotard", Description = "Ducati Hipermotard", CategoryTemplateId = 1, ParentCategoryId = 152, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =261, Name = "Ducati Monster", Description = "Ducati Monster", CategoryTemplateId = 1, ParentCategoryId = 152, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =262, Name = "Ducati Multistrada", Description = "Ducati Multistrada", CategoryTemplateId = 1, ParentCategoryId = 152, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =263, Name = "Ducati Panigale", Description = "Ducati Panigale", CategoryTemplateId = 1, ParentCategoryId = 152, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =264, Name = "Ducati Scrambler", Description = "Ducati Scrambler", CategoryTemplateId = 1, ParentCategoryId = 152, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =265, Name = "Ducati Sport Clasic", Description = "Ducati Sport Clasic", CategoryTemplateId = 1, ParentCategoryId = 152, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =266, Name = "Ducati Street Fighter", Description = "Ducati Street Fighter", CategoryTemplateId = 1, ParentCategoryId = 152, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =267, Name = "Ducati Testastretta", Description = "Ducati Testastretta", CategoryTemplateId = 1, ParentCategoryId = 152, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =268, Name = "Harley Davidson Breakuot", Description = "Harley Davidson Breakuot", CategoryTemplateId = 1, ParentCategoryId = 150, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =269, Name = "Harley Davidson Clasica Fatboy", Description = "Harley Davidson Clasica Fatboy", CategoryTemplateId = 1, ParentCategoryId = 150, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =270, Name = "Harley Davidson Dyna", Description = "Harley Davidson Dyna", CategoryTemplateId = 1, ParentCategoryId = 150, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =271, Name = "Harley Davidson Electraglide Ultraclassic", Description = "Harley Davidson Electraglide Ultraclassic", CategoryTemplateId = 1, ParentCategoryId = 150, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =272, Name = "Harley Davidson Fat Boy", Description = "Harley Davidson Fat Boy", CategoryTemplateId = 1, ParentCategoryId = 150, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =273, Name = "Harley Davidson Forty Eight", Description = "Harley Davidson Forty Eight", CategoryTemplateId = 1, ParentCategoryId = 150, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =274, Name = "Harley Davidson Fxr", Description = "Harley Davidson Fxr", CategoryTemplateId = 1, ParentCategoryId = 150, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =275, Name = "Harley Davidson Harley Hadidson Nigth Rod", Description = "Harley Davidson Harley Hadidson Nigth Rod", CategoryTemplateId = 1, ParentCategoryId = 150, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =276, Name = "Harley Davidson Heritage 1690", Description = "Harley Davidson Heritage 1690", CategoryTemplateId = 1, ParentCategoryId = 150, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =277, Name = "Harley Davidson Iron 883", Description = "Harley Davidson Iron 883", CategoryTemplateId = 1, ParentCategoryId = 150, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =278, Name = "Harley Davidson Low Rider 1690", Description = "Harley Davidson Low Rider 1690", CategoryTemplateId = 1, ParentCategoryId = 150, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =279, Name = "Harley Davidson Muscle", Description = "Harley Davidson Muscle", CategoryTemplateId = 1, ParentCategoryId = 150, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =280, Name = "Harley Davidson Night Rod-vrod", Description = "Harley Davidson Night Rod-vrod", CategoryTemplateId = 1, ParentCategoryId = 150, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =281, Name = "Harley Davidson Saftail", Description = "Harley Davidson Saftail", CategoryTemplateId = 1, ParentCategoryId = 150, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =282, Name = "Harley Davidson Seventy - Two 1200", Description = "Harley Davidson Seventy - Two 1200", CategoryTemplateId = 1, ParentCategoryId = 150, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =283, Name = "Harley Davidson Soft Tail Deluxe 1690", Description = "Harley Davidson Soft Tail Deluxe 1690", CategoryTemplateId = 1, ParentCategoryId = 150, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =284, Name = "Harley Davidson Softail Slim", Description = "Harley Davidson Softail Slim", CategoryTemplateId = 1, ParentCategoryId = 150, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =285, Name = "Harley Davidson Sportster", Description = "Harley Davidson Sportster", CategoryTemplateId = 1, ParentCategoryId = 150, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =286, Name = "Harley Davidson Stree Glide Special 1690", Description = "Harley Davidson Stree Glide Special 1690", CategoryTemplateId = 1, ParentCategoryId = 150, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =287, Name = "Harley Davidson Suftail Custom", Description = "Harley Davidson Suftail Custom", CategoryTemplateId = 1, ParentCategoryId = 150, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =288, Name = "Harley Davidson Ultra", Description = "Harley Davidson Ultra", CategoryTemplateId = 1, ParentCategoryId = 150, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =289, Name = "Harley Davidson V Rod", Description = "Harley Davidson V Rod", CategoryTemplateId = 1, ParentCategoryId = 150, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =290, Name = "Harley Davidson XL", Description = "Harley Davidson XL", CategoryTemplateId = 1, ParentCategoryId = 150, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =291, Name = "Honda Biz C 100", Description = "Honda Biz C 100", CategoryTemplateId = 1, ParentCategoryId = 149, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =292, Name = "Honda Cbf", Description = "Honda Cbf", CategoryTemplateId = 1, ParentCategoryId = 149, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =293, Name = "Honda Cbf 150", Description = "Honda Cbf 150", CategoryTemplateId = 1, ParentCategoryId = 149, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =294, Name = "Honda Cbf 125", Description = "Honda Cbf 125", CategoryTemplateId = 1, ParentCategoryId = 149, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =295, Name = "Honda Cbf 750", Description = "Honda Cbf 750", CategoryTemplateId = 1, ParentCategoryId = 149, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =296, Name = "Honda Cbr 1000", Description = "Honda Cbr 1000", CategoryTemplateId = 1, ParentCategoryId = 149, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =297, Name = "Honda Cbr 250", Description = "Honda Cbr 250", CategoryTemplateId = 1, ParentCategoryId = 149, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =298, Name = "Honda Cbr 600 Fi", Description = "Honda Cbr 600 Fi", CategoryTemplateId = 1, ParentCategoryId = 149, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =299, Name = "Honda Cbr 900", Description = "Honda Cbr 900", CategoryTemplateId = 1, ParentCategoryId = 149, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =300, Name = "Honda Cbx 1047", Description = "Honda Cbx 1047", CategoryTemplateId = 1, ParentCategoryId = 149, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =301, Name = "Honda Cbx 250", Description = "Honda Cbx 250", CategoryTemplateId = 1, ParentCategoryId = 149, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =302, Name = "Honda Cbx 500", Description = "Honda Cbx 500", CategoryTemplateId = 1, ParentCategoryId = 149, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =303, Name = "Honda Elite", Description = "Honda Elite", CategoryTemplateId = 1, ParentCategoryId = 149, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =304, Name = "Honda Falcon", Description = "Honda Falcon", CategoryTemplateId = 1, ParentCategoryId = 149, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =305, Name = "Honda Fl", Description = "Honda Fl", CategoryTemplateId = 1, ParentCategoryId = 149, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =306, Name = "Honda Goldwing", Description = "Honda Goldwing", CategoryTemplateId = 1, ParentCategoryId = 149, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =307, Name = "Honda Honda Splendor Nxg", Description = "Honda Honda Splendor Nxg", CategoryTemplateId = 1, ParentCategoryId = 149, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =308, Name = "Honda Invicta", Description = "Honda Invicta", CategoryTemplateId = 1, ParentCategoryId = 149, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =309, Name = "Honda Nighthawk", Description = "Honda Nighthawk", CategoryTemplateId = 1, ParentCategoryId = 149, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =310, Name = "Honda Shadow", Description = "Honda Shadow", CategoryTemplateId = 1, ParentCategoryId = 149, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =311, Name = "Honda Tornado 250cc", Description = "Honda Tornado 250cc", CategoryTemplateId = 1, ParentCategoryId = 149, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =312, Name = "Honda Translap", Description = "Honda Translap", CategoryTemplateId = 1, ParentCategoryId = 149, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =313, Name = "Honda Trx 250", Description = "Honda Trx 250", CategoryTemplateId = 1, ParentCategoryId = 149, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =314, Name = "Honda Twister 250", Description = "Honda Twister 250", CategoryTemplateId = 1, ParentCategoryId = 149, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =315, Name = "Honda Varadero", Description = "Honda Varadero", CategoryTemplateId = 1, ParentCategoryId = 149, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =316, Name = "Honda Vtx 1300", Description = "Honda Vtx 1300", CategoryTemplateId = 1, ParentCategoryId = 149, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =317, Name = "Honda Xc 500", Description = "Honda Xc 500", CategoryTemplateId = 1, ParentCategoryId = 149, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =318, Name = "Honda Xl-350", Description = "Honda Xl-350", CategoryTemplateId = 1, ParentCategoryId = 149, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =319, Name = "Honda Xr 125l", Description = "Honda Xr 125l", CategoryTemplateId = 1, ParentCategoryId = 149, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =320, Name = "Kawasaki Er 6n", Description = "Kawasaki Er 6n", CategoryTemplateId = 1, ParentCategoryId = 148, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =321, Name = "Kawasaki Er250c", Description = "Kawasaki Er250c", CategoryTemplateId = 1, ParentCategoryId = 148, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =322, Name = "Kawasaki Er6n ", Description = "Kawasaki Er6n ", CategoryTemplateId = 1, ParentCategoryId = 148, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =323, Name = "Kawasaki Ex-250", Description = "Kawasaki Ex-250", CategoryTemplateId = 1, ParentCategoryId = 148, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =324, Name = "Kawasaki Klx", Description = "Kawasaki Klx", CategoryTemplateId = 1, ParentCategoryId = 148, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =325, Name = "Kawasaki Kmx125", Description = "Kawasaki Kmx125", CategoryTemplateId = 1, ParentCategoryId = 148, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =326, Name = "Kawasaki Krl", Description = "Kawasaki Krl", CategoryTemplateId = 1, ParentCategoryId = 148, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =327, Name = "Kawasaki Ninja 250", Description = "Kawasaki Ninja 250", CategoryTemplateId = 1, ParentCategoryId = 148, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =328, Name = "Kawasaki Ninja 300", Description = "Kawasaki Ninja 300", CategoryTemplateId = 1, ParentCategoryId = 148, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =329, Name = "Kawasaki Ninja 650", Description = "Kawasaki Ninja 650", CategoryTemplateId = 1, ParentCategoryId = 148, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =330, Name = "Kawasaki Versys", Description = "Kawasaki Versys", CategoryTemplateId = 1, ParentCategoryId = 148, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =331, Name = "Kawasaki Vulcan", Description = "Kawasaki Vulcan", CategoryTemplateId = 1, ParentCategoryId = 148, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =332, Name = "Kawasaki Z 1000", Description = "Kawasaki Z 1000", CategoryTemplateId = 1, ParentCategoryId = 148, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =333, Name = "Kawasaki Z 250", Description = "Kawasaki Z 250", CategoryTemplateId = 1, ParentCategoryId = 148, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =334, Name = "Kawasaki Z 800", Description = "Kawasaki Z 800", CategoryTemplateId = 1, ParentCategoryId = 148, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =335, Name = "Ktm Exc", Description = "Ktm Exc", CategoryTemplateId = 1, ParentCategoryId = 147, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =336, Name = "Ktm Supermoto", Description = "Ktm Supermoto", CategoryTemplateId = 1, ParentCategoryId = 147, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =337, Name = "Ktm Super Enduro", Description = "Ktm Super Enduro", CategoryTemplateId = 1, ParentCategoryId = 147, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =338, Name = "Ktm Adventure", Description = "Ktm Adventure", CategoryTemplateId = 1, ParentCategoryId = 147, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =339, Name = "Ktm Duke 200", Description = "Ktm Duke 200", CategoryTemplateId = 1, ParentCategoryId = 147, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =340, Name = "Ktm Duke 390", Description = "Ktm Duke 390", CategoryTemplateId = 1, ParentCategoryId = 147, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =341, Name = "Ktm Duke 690", Description = "Ktm Duke 690", CategoryTemplateId = 1, ParentCategoryId = 147, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =342, Name = "Ktm Exc", Description = "Ktm Exc", CategoryTemplateId = 1, ParentCategoryId = 147, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =343, Name = "Ktm Freeride", Description = "Ktm Freeride", CategoryTemplateId = 1, ParentCategoryId = 147, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =344, Name = "Ktm Sm", Description = "Ktm Sm", CategoryTemplateId = 1, ParentCategoryId = 147, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =345, Name = "Ktm Sx", Description = "Ktm Sx", CategoryTemplateId = 1, ParentCategoryId = 147, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =346, Name = "Ktm Xc", Description = "Ktm Xc", CategoryTemplateId = 1, ParentCategoryId = 147, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =347, Name = "Kymco Agility", Description = "Kymco Agility", CategoryTemplateId = 1, ParentCategoryId = 146, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =348, Name = "Kymco Agility City", Description = "Kymco Agility City", CategoryTemplateId = 1, ParentCategoryId = 146, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =349, Name = "Kymco Agility Xtreme", Description = "Kymco Agility Xtreme", CategoryTemplateId = 1, ParentCategoryId = 146, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =350, Name = "Kymco Downtown", Description = "Kymco Downtown", CategoryTemplateId = 1, ParentCategoryId = 146, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =351, Name = "Kymco Fly", Description = "Kymco Fly", CategoryTemplateId = 1, ParentCategoryId = 146, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =352, Name = "Kymco Like", Description = "Kymco Like", CategoryTemplateId = 1, ParentCategoryId = 146, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =353, Name = "Kymco Rocket 125", Description = "Kymco Rocket 125", CategoryTemplateId = 1, ParentCategoryId = 146, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =354, Name = "Kymco Scooters Y Ciclomotores", Description = "Kymco Scooters Y Ciclomotores", CategoryTemplateId = 1, ParentCategoryId = 146, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =355, Name = "Kymco Track", Description = "Kymco Track", CategoryTemplateId = 1, ParentCategoryId = 146, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =356, Name = "Kymco Xciting", Description = "Kymco Xciting", CategoryTemplateId = 1, ParentCategoryId = 146, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =357, Name = "Piaggio Vespa", Description = "Piaggio Vespa", CategoryTemplateId = 1, ParentCategoryId = 145, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =358, Name = "Piaggio Vespa Gs", Description = "Piaggio Vespa Gs", CategoryTemplateId = 1, ParentCategoryId = 145, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =359, Name = "Piaggio Vespa Plus", Description = "Piaggio Vespa Plus", CategoryTemplateId = 1, ParentCategoryId = 145, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =360, Name = "Piaggio Vespa Primavera", Description = "Piaggio Vespa Primavera", CategoryTemplateId = 1, ParentCategoryId = 145, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =361, Name = "Piaggio Vespa Star Automatic", Description = "Piaggio Vespa Star Automatic", CategoryTemplateId = 1, ParentCategoryId = 145, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =362, Name = "Royal Enfield Bullet", Description = "Royal Enfield Bullet", CategoryTemplateId = 1, ParentCategoryId = 144, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =363, Name = "Royal Enfield Clasicc", Description = "Royal Enfield Clasicc", CategoryTemplateId = 1, ParentCategoryId = 144, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =364, Name = "Royal Enfield Continental Gt", Description = "Royal Enfield Continental Gt", CategoryTemplateId = 1, ParentCategoryId = 144, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =365, Name = "Royal Enfield Rumbler", Description = "Royal Enfield Rumbler", CategoryTemplateId = 1, ParentCategoryId = 144, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =366, Name = "Sachs Madass", Description = "Sachs Madass", CategoryTemplateId = 1, ParentCategoryId = 143, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =367, Name = "Suzuki Bandit", Description = "Suzuki Bandit", CategoryTemplateId = 1, ParentCategoryId = 142, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =368, Name = "Suzuki B-king", Description = "Suzuki B-king", CategoryTemplateId = 1, ParentCategoryId = 142, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =369, Name = "Suzuki Dr", Description = "Suzuki Dr", CategoryTemplateId = 1, ParentCategoryId = 142, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =370, Name = "Suzuki Freewind 650cc", Description = "Suzuki Freewind 650cc", CategoryTemplateId = 1, ParentCategoryId = 142, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =371, Name = "Suzuki Gs 125cc", Description = "Suzuki Gs 125cc", CategoryTemplateId = 1, ParentCategoryId = 142, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =372, Name = "Suzuki Gs 500", Description = "Suzuki Gs 500", CategoryTemplateId = 1, ParentCategoryId = 142, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =373, Name = "Suzuki Gsr", Description = "Suzuki Gsr", CategoryTemplateId = 1, ParentCategoryId = 142, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =374, Name = "Suzuki Gsxr", Description = "Suzuki Gsxr", CategoryTemplateId = 1, ParentCategoryId = 142, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =375, Name = "Suzuki Inazuma", Description = "Suzuki Inazuma", CategoryTemplateId = 1, ParentCategoryId = 142, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =376, Name = "Suzuki V- Strom", Description = "Suzuki V- Strom", CategoryTemplateId = 1, ParentCategoryId = 142, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =377, Name = "Triumph Bonneville Special", Description = "Triumph Bonneville Special", CategoryTemplateId = 1, ParentCategoryId = 141, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =378, Name = "Triumph Daytona", Description = "Triumph Daytona", CategoryTemplateId = 1, ParentCategoryId = 141, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =379, Name = "Triumph Explorer", Description = "Triumph Explorer", CategoryTemplateId = 1, ParentCategoryId = 141, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =380, Name = "Triumph Speed Triple", Description = "Triumph Speed Triple", CategoryTemplateId = 1, ParentCategoryId = 141, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =381, Name = "Triumph Tiger", Description = "Triumph Tiger", CategoryTemplateId = 1, ParentCategoryId = 141, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =382, Name = "Triumph Touring", Description = "Triumph Touring", CategoryTemplateId = 1, ParentCategoryId = 141, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    new Category() { Id =383, Name = "Triumph Truxton", Description = "Triumph Truxton", CategoryTemplateId = 1, ParentCategoryId = 141, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    //Tercera migración
                    new Category() { Id =384, Name = "Impermeables", Description = "Impermeables", CategoryTemplateId = 1, ParentCategoryId = 23, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12"	,  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  },
                    
                };


            if (runCategories)
            {
                var categoriesTable = context.Set<Category>();
                
                categoriesTable.AddOrUpdate(c => c.Id, categories);
                
            }
            


            #endregion

            #region Currency

            if (runCurrency)
            {
                var currenciesTable = context.Set<Currency>();
                var currencies = new Currency[]{
                    new Currency(){ Id=12, Name = "Peso Colombiano", CurrencyCode="COP", Rate=1, DisplayLocale= "es-CO", CustomFormatting = "C0", Published = true, DisplayOrder = 0, CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now  },
                };
                currenciesTable.AddOrUpdate(c => c.Id, currencies);
            }
           
            #endregion

            #region Language
            if (runLanguage)
            {
                var languagesTable = context.Set<Language>();
                var languages = new Language[]{
                    new Language(){ Id=1, Name = "English", LanguageCulture= "en-US", UniqueSeoCode="en", FlagImageFileName="us.png", Rtl= false, LimitedToStores = false, DefaultCurrencyId = 0, DisplayOrder = 1, Published = false },
                    new Language(){ Id=2, Name = "Español", LanguageCulture= "es-CO", UniqueSeoCode="es", FlagImageFileName="co.png", Rtl= false, LimitedToStores = false, DefaultCurrencyId = 12, DisplayOrder = 0, Published = true }
                };
                languagesTable.AddOrUpdate(c => c.Id, languages);
            }
            
            #endregion

            #region LocaleStringResource

            if (runResources)
            {
                #region Resources
                var newLocaleStringResources = new Dictionary<string, string>();

                //AddColumn_SocialNetwork_Table_Manufacturer
                newLocaleStringResources.Add("PublishProduct.Product.ForBrandBike", "Escoje las referencias de moto para las que va el producto");
                newLocaleStringResources.Add("PublishProduct.Product.SelectBrand", "- Selecciona una marca -");
                newLocaleStringResources.Add("publishproduct.stepmenu1", "Seleccionar Categoría");
                newLocaleStringResources.Add("publishproduct.producttitle", "Titulo");
                newLocaleStringResources.Add("publishproduct.manufacturer", "Marca");
                newLocaleStringResources.Add("publishproduct.hasshipping", "Realiza envíos");
                newLocaleStringResources.Add("publishproduct.shippingvalue", "Valor del envío");
                newLocaleStringResources.Add("publishproduct.price", "Precio");
                newLocaleStringResources.Add("publishproduct.product.html", "Descripción");
                newLocaleStringResources.Add("publishproduct.stepmenu2", "Detalle del producto");
                newLocaleStringResources.Add("PublishProduct.NextStep", "Siguiente");
                newLocaleStringResources.Add("PublishProduct.BackStep", "Atras");
                newLocaleStringResources.Add("PublishProduct.StepMenu3", "Imagenes");
                newLocaleStringResources.Add("PublishProduct.StepMenu4", "Resumen");
                newLocaleStringResources.Add("publishproduct.finishstep", "Publicar");
                newLocaleStringResources.Add("publishproduct.selectcategoryfilter", "Buscar...");
                newLocaleStringResources.Add("PublishProduct.PriceExample", "No utilices puntos ni comas Eg: 20000");
                newLocaleStringResources.Add("PublishProduct.TitleExample", "Ej. Casco Hjc ref C10-20 Completamente nuevo");
                newLocaleStringResources.Add("PublishProduct.SelectImagesDescription", "Ingresa las imagenes acordes con tu producto");
                newLocaleStringResources.Add("PublishProduct.SelectImagesExtensions", "Los formatos aceptados son .jpg, .gif y .png. El tamaño máximo permitido para los archivos es 5 MB.");
                newLocaleStringResources.Add("publishproduct.carriageplate", "Placa");
                newLocaleStringResources.Add("PublishProduct.CarriagePlateExample", "EJ. Sin espacios HJC20C");
                newLocaleStringResources.Add("PublishProduct.Kms", "Recorrido");
                newLocaleStringResources.Add("publishproduct.codition", "Condición de la moto");
                newLocaleStringResources.Add("publishproduct.stepmenu2_bike", "Detalle de la moto");
                newLocaleStringResources.Add("PublishProduct.Year", "Año");
                newLocaleStringResources.Add("PublishProduct.Color", "Color");
                newLocaleStringResources.Add("PublishProduct.IsInGaranty", "¿Está en garantia actualmente?");
                newLocaleStringResources.Add("publishproduct.negotiationoptions", "Condiciones de Negociación");
                newLocaleStringResources.Add("PublishProduct.AccesoriesOptions", "Accesorios");
                newLocaleStringResources.Add("PublishProduct.IsNew", "Estado");
                newLocaleStringResources.Add("PublishProduct.StateProvince", "Ciudad");
                newLocaleStringResources.Add("PublishProduct.PublishSimilar", "Publicar similar");
                newLocaleStringResources.Add("PublishProduct.GotoPublishing", "Ir a publicaciones");
                newLocaleStringResources.Add("PublishProduct.PublishFinished", "¡Listo ya publicaste!");
                newLocaleStringResources.Add("PublishProduct.TimeOutPostUser", "Recuerda que tu anuncio será revisado y aprobado dentro de los próximos minutos y estará publicado durante {0} días.");
                newLocaleStringResources.Add("PublishProduct.TimeOutPostMarket", "Recuerde que su anuncio será revisado y aprobado dentro de los próximos minutos y estará publicado hasta la vigencia de su plan.");
                newLocaleStringResources.Add("PublishProduct.StepMenu2_Service", "Detalle del servicio");
                newLocaleStringResources.Add("PublishProduct.TitleDoorToDoor", "Domicilios");
                newLocaleStringResources.Add("PublishProduct.IsDoorToDoor", "Se hacen domicilios");
                newLocaleStringResources.Add("PublishProduct.IncludeSupplies", "Incluye insumos");
                newLocaleStringResources.Add("PublishProduct.DetailDoorToDoor", "Especifica la cobertura");
                newLocaleStringResources.Add("PublishProduct.ShippingValueDoorToDoor", "Valor del domicilio");
                newLocaleStringResources.Add("PublishProduct.TitleSupplies", "Insumos");
                newLocaleStringResources.Add("PublishProduct.Supplies", "Que insumos son necesarios");
                newLocaleStringResources.Add("PublishProduct.SuppliesValue", "Valor promedio de los Insumos");
                newLocaleStringResources.Add("CreateUser.Name", "Nombres");
                newLocaleStringResources.Add("CreateUser.LastName", "Apellidos");
                newLocaleStringResources.Add("CreateUser.Email", "Correo Elctrónico");
                newLocaleStringResources.Add("CreateUser.Password", "Clave");
                newLocaleStringResources.Add("CreateUser.CompanyName", "Nombre de la empresa");
                newLocaleStringResources.Add("CreateUser.Save", "Registrarse");
                newLocaleStringResources.Add("createuser.back", "Ya estoy registrado");
                newLocaleStringResources.Add("Login.Email", "Correo electrónico");
                newLocaleStringResources.Add("Login.Password", "Clave");
                newLocaleStringResources.Add("Login.ButtonLogin", "Entrar");
                newLocaleStringResources.Add("Login.RegisterButton", "¿No estas registrado?");
                newLocaleStringResources.Add("account.login.wrongcredentials.WrongPassword", "The credentials provided are incorrect");
                newLocaleStringResources.Add("Account.MyBike", "Mi Moto");
                newLocaleStringResources.Add("account.fields.bikebrand", "Marca moto");
                newLocaleStringResources.Add("Account.Fields.BikeReference", "Referencia moto");
                newLocaleStringResources.Add("Account.Fields.BikeYear", "Año");
                newLocaleStringResources.Add("Account.Fields.BikeCarriagePlate", "Placa");
                newLocaleStringResources.Add("Account.Fields.BrandNewsletter", "Recibir noticias de la marca");
                newLocaleStringResources.Add("Account.Fields.ReferenceNewsletter", "Recibir noticias de la referencia");
                newLocaleStringResources.Add("Account.ChangePassword.ContinueEditing", "Volver a mis datos personales");
                newLocaleStringResources.Add("Offices.Name", "Nombre de sucursal");
                newLocaleStringResources.Add("Offices.Email", "Correo electrónico");
                newLocaleStringResources.Add("Offices.PhoneNumber", "Teléfono");
                newLocaleStringResources.Add("Offices.FaxNumber", "Fax");
                newLocaleStringResources.Add("Offices.StateProvinceId", "Ciudad");
                newLocaleStringResources.Add("Offices.Address1", "Dirección");
                newLocaleStringResources.Add("Offices.List", "Listado");
                newLocaleStringResources.Add("Offices.Detail", "Detalle");
                newLocaleStringResources.Add("Offices.Schedule", "Horario");
                newLocaleStringResources.Add("vendor.edit", "Editar");
                newLocaleStringResources.Add("vendor.contactus", "Contactar");
                newLocaleStringResources.Add("vendor.rateUs", "Reseñar");
                newLocaleStringResources.Add("vendor.backgroudpicture", "Subir foto de portada");
                newLocaleStringResources.Add("vendor.profilepicture", "Subir logo");
                newLocaleStringResources.Add("vendor.save", "Guardar");
                newLocaleStringResources.Add("vendor.enableShipping", "Realiza envios");
                newLocaleStringResources.Add("vendor.enableCreditCard", "Recibe tarjeta");
                newLocaleStringResources.Add("vendor.movecover", "Mover fondo");
                newLocaleStringResources.Add("Vendor.SpecialCategoriesServices", "Servicios Especiales");
                newLocaleStringResources.Add("Vendor.SpecialCategoriesBikes", "Especializado en las siguientes marcas");
                newLocaleStringResources.Add("vendor.DefaultReviewsPageSize", "3");
                newLocaleStringResources.Add("vendor.moreReviews", "Más");
                newLocaleStringResources.Add("Account.DescriptionSection", "Ingresa los datos de tu cuenta, entre más información nos des mejores serán las ofertas que te llevarémos");
                newLocaleStringResources.Add("controlpanel.sale", "Vender");
                newLocaleStringResources.Add("controlpanel.publishproductservice", "Servicio");
                newLocaleStringResources.Add("controlpanel.publishproductbike", "Moto");
                newLocaleStringResources.Add("controlpanel.publishproduct", "Producto");
                newLocaleStringResources.Add("controlpanel.myaccount", "Mis datos");
                newLocaleStringResources.Add("controlpanel.menutitle", "Panel de Control");
                newLocaleStringResources.Add("controlpanel.Vendor", "Mi tienda");
                newLocaleStringResources.Add("controlpanel.mysales", "Mis ventas");
                newLocaleStringResources.Add("controlpanel.MyProducts", "Mis productos");
                newLocaleStringResources.Add("controlpanel.MyOrders", "Mis compras");
                newLocaleStringResources.Add("controlpanel.Messages", "Mensajes");
                newLocaleStringResources.Add("controlpanel.Favourites", "Mis favoritos");
                newLocaleStringResources.Add("controlpanel.Offices", "Sedes");
                newLocaleStringResources.Add("controlpanel.VendorServices", "Servicios prestados");
                newLocaleStringResources.Add("VendorServices.Specialized", "Servicios especializados");
                newLocaleStringResources.Add("VendorServices.References", "Referencias Especializadas");
                newLocaleStringResources.Add("MyOrdersModel.Title", "Mis Compras");
                newLocaleStringResources.Add("PageTitle.MyOrders", "Mira lo que has comprado en Tuils");
                newLocaleStringResources.Add("MyOrders.DescriptionUser", "Abajo encuentras todas las veces en las que has pagado por destacar uno de tus productos");
                newLocaleStringResources.Add("MyOrders.DescriptionMarket", "A continuación encuentra los planes que ha adquirido a lo largo de la historia. El plan que se encuentre en estado Activo es el que posee actualmente.");
                newLocaleStringResources.Add("MyOrders.Product", "Producto");
                newLocaleStringResources.Add("MyOrders.Vendor", "Vendedor");
                newLocaleStringResources.Add("MyOrders.Rating", "Calificación");
                newLocaleStringResources.Add("MyOrders.Date", "Fecha");
                newLocaleStringResources.Add("MyOrders.Price", "Precio");
                newLocaleStringResources.Add("MyOrders.TitleImgProduct", "Ver el detalle de {0}");
                newLocaleStringResources.Add("myorders.title", "Mis compras");
                newLocaleStringResources.Add("PageTitle.MySales", "Mis Ventas");
                newLocaleStringResources.Add("MySales.Title", "Mis Ventas");
                newLocaleStringResources.Add("MySales.Description", "Acá encuentras todas las ventas que has hecho");
                newLocaleStringResources.Add("MySales.Customer", "Comprador");
                newLocaleStringResources.Add("MySales.NoRating", "Sin calificar");
                newLocaleStringResources.Add("controlpanel.MySalesActiveProducts", "Productos Activos");
                newLocaleStringResources.Add("controlpanel.MySalesNoRating", "Sin calificar");
                newLocaleStringResources.Add("controlpanel.MySalesRating", "Calificadas");
                newLocaleStringResources.Add("controlpanel.AllOrders", "Todas");
                newLocaleStringResources.Add("PageTitle.ControlPanel", "Panel de control");
                newLocaleStringResources.Add("ControlPanelIndex.PublishedProducts", "Productos publicados");
                newLocaleStringResources.Add("ControlPanelIndex.SoldProducts", "Productos vendidos");
                newLocaleStringResources.Add("controlpanelindex.numratings", "Calificaciones recibidas");
                newLocaleStringResources.Add("controlpanelindex.AvgRating", "Satisfacción");
                newLocaleStringResources.Add("controlpanelindex.newmessages", "Preguntas sin responder");
                newLocaleStringResources.Add("controlpanelindex.PendingMessages", "Mensajes sin responder");
                newLocaleStringResources.Add("controlpanelindex.description", "En el panel de control podrás encontrar todas las funcionalidades relacionadas con tu cuenta");
                newLocaleStringResources.Add("controlpanelindex.greeting", "Hola {0} bienvenido(a), al panel de control");
                newLocaleStringResources.Add("PageTitle.MyProducts", "Mis productos");
                newLocaleStringResources.Add("myproducts.title", "Mis Productos");
                newLocaleStringResources.Add("myproducts.visits", "Visitas");
                newLocaleStringResources.Add("myproducts.totalsales", "Ventas");
                newLocaleStringResources.Add("myproducts.ApprovedTotalReviews", "Reseñas");
                newLocaleStringResources.Add("myproducts.UnansweredQuestions", "Preguntas sin responder");
                newLocaleStringResources.Add("myproducts.AvailableStartDate", "Fecha inicio");
                newLocaleStringResources.Add("myproducts.publised", "Publicado");
                newLocaleStringResources.Add("myproducts.nopublised", "Inactivo");
                newLocaleStringResources.Add("myproducts.viewOnline", "Ver producto");
                newLocaleStringResources.Add("myproducts.availableenddate", "Fecha cierre");
                newLocaleStringResources.Add("myproducts.noPendingQuestions", "No tienes preguntas pendientes");
                newLocaleStringResources.Add("controlpanel.MyProductsPublished", "Activos");
                newLocaleStringResources.Add("controlpanel.MyProductsUnPublished", "Inactivos");
                newLocaleStringResources.Add("PageTitle.Questions", "Preguntas por responder");
                newLocaleStringResources.Add("Questions.Title", "Preguntas por responder");
                newLocaleStringResources.Add("Questions.ButtonAnswer", "Responder");
                newLocaleStringResources.Add("questions.backtoproduct", "Volver a mis productos");
                newLocaleStringResources.Add("offices.title", "Adminsitrar las sedes");
                newLocaleStringResources.Add("common.options", "Opciones");
                newLocaleStringResources.Add("Offices.Edit", "Editar");
                newLocaleStringResources.Add("Offices.NewImageHelp", "Agregar imagen de la sede");
                newLocaleStringResources.Add("Products.SeeVendor", "Quiero este producto");
                newLocaleStringResources.Add("products.confirmBuy", "Después de esto el vendedor te podrá contactar ya que tendrá tus datos. ¿Quieres continuar?");
                newLocaleStringResources.Add("Filtering.StateProvinceFilter", "Ciudad");
                newLocaleStringResources.Add("Filtering.StateProvinceFilter.CurrentlyFilteredBy", "Ciudades Filtradas");
                newLocaleStringResources.Add("Filtering.StateProvinceFilter.Remove", "Quitar filtro");
                newLocaleStringResources.Add("Filtering.CategoryFilter", "Categoría");
                newLocaleStringResources.Add("Filtering.CategoryFilter.CurrentlyFilteredBy", "Ya filtró por");
                newLocaleStringResources.Add("Filtering.ManufacturerFilter", "Marca");
                newLocaleStringResources.Add("Filtering.ManufacturerFilter.CurrentlyFilteredBy", "Estas filtrando por marca");
                newLocaleStringResources.Add("Filtering.ManufacturerFilter.Remove", "Quitar filtro");
                newLocaleStringResources.Add("controlpanel.changePassword", "Cambiar clave");
                newLocaleStringResources.Add("common.publishProduct", "Publica tu anuncio gratis");
                newLocaleStringResources.Add("common.filterByService", "Filtrar por servicios");
                newLocaleStringResources.Add("common.filterByProduct", "Filtrar por producto");
                newLocaleStringResources.Add("common.filterByBike", "FIltrar por motocicletas");
                newLocaleStringResources.Add("header.publishproduct", "Publica tu anuncio");
                newLocaleStringResources.Add("Admin.Catalog.Categories.Fields.SpecificationAttributeOptionId", "Atributo relacionado");
                newLocaleStringResources.Add("Filtering.BikeReferenceFilter", "Moto");
                newLocaleStringResources.Add("Filtering.BikeReference.CurrentlyFilteredBy", "Filtrado por moto");
                newLocaleStringResources.Add("Filtering.BikeReference.Remove", "Quitar filtro de moto");
                newLocaleStringResources.Add("Admin.Catalog.Manufacturers.Fields.ShowOnHomePage", "Mostrar en el home");
                newLocaleStringResources.Add("Admin.Common.SocialNetwork.Fields.FacebookPage", "Url Página de Facebook");
                newLocaleStringResources.Add("Admin.Common.SocialNetwork.Fields.TwitterAccount", "Usuario de Twitter");
                newLocaleStringResources.Add("Admin.Common.SocialNetwork.Fields.InstagramAccount", "Usuario de Instagram");
                newLocaleStringResources.Add("Admin.Common.SocialNetwork.Fields.PinterestAccount", "Usuario de Pinterest");
                newLocaleStringResources.Add("header.publishproduct.widescreen", "Publica tu anuncio");
                newLocaleStringResources.Add("header.publishproduct.mobile", "Publicar");
                newLocaleStringResources.Add("FeaturedLeftMenu", "Destacados");
                newLocaleStringResources.Add("Admin.Catalog.Products.Fields.LeftFeatured", "Mostrar como destacado izquierdo");
                newLocaleStringResources.Add("Admin.Catalog.Products.Fields.LeftFeatured.Hint", "Muestra el producto en la parte izquierda de las paginas");
                newLocaleStringResources.Add("ProductBox.AddToWishlist", "Añador deseo");
                newLocaleStringResources.Add("ProductBox.Compare", "Comparar");
                newLocaleStringResources.Add("Products.StateProvinceName", "Ubicación");
                newLocaleStringResources.Add("Products.VendorShippingEnabled", "Envío a acordar con el vendedor");
                newLocaleStringResources.Add("Products.VendorCreditCardEnabled", "Recibimos tarjetas");
                newLocaleStringResources.Add("Product.SpecialCategories", "Producto especial para");
                newLocaleStringResources.Add("vendor.enableShipping.description", "Se realizan envíos. El valor depende de la ubicación geográfica");
                newLocaleStringResources.Add("vendor.enableCreditCard.description", "Se recibes tarjetas. Para más información contactarnos en los teléfonos de las sedes");
                newLocaleStringResources.Add("Products.Questions", "Preguntas");
                newLocaleStringResources.Add("Questions.Write", "Deja una pregunta para el vendedor");
                newLocaleStringResources.Add("Questions.Date", "Fecha de Pregunta");
                newLocaleStringResources.Add("Questions.AnsweredDate", "Fecha de respuesta");
                newLocaleStringResources.Add("Questions.SubmitButton", "Preguntar al vendedor");
                newLocaleStringResources.Add("Questions.InvalidCatpcha", "Valida que el número de la imagen sea el correcto");
                newLocaleStringResources.Add("Admin.Configuration.Settings.GeneralCommon.CaptchaShowOnProductQuestionsPage", "Mostrar el captcha en la zona de preguntas del producto");
                newLocaleStringResources.Add("Questions.QuestionPublished", "Tu pregunta fue guardada correctamente");
                newLocaleStringResources.Add("Questions.LinkWriteQuestion", "Puedes dejarle una pregunta al vendedor");
                newLocaleStringResources.Add("Questions.LinkWriteQuestion.Alt", "Deja una pregunta al vendedor de este producto para que la conteste lo más rapido posible");
                newLocaleStringResources.Add("Questions.NoQuestions", "No han dejado preguntas todavia");
                newLocaleStringResources.Add("Reviews.NoReviews", "Nadie ha calificado este producto todavía");
                newLocaleStringResources.Add("custom.reviews.productreviews.Alt", "Revisa las calificaciones que otros usuarios le han dejado a este vendedor");
                newLocaleStringResources.Add("vendor.backgroudPicture.alt", "Cambia la imagen de fondo de tu tienda");
                newLocaleStringResources.Add("vendor.movecover.alt", "Acomoda la imagen como mejor te parezca");
                newLocaleStringResources.Add("vendor.edit.alt", "Cambia la información de tu almacen o taller");
                newLocaleStringResources.Add("Vendor.vendorServices.services.title", "Servicios en los que {0} se especializa");
                newLocaleStringResources.Add("Vendor.vendorServices.services.edit", "Actualiza los servicios en los que te especializas");
                newLocaleStringResources.Add("Vendor.vendorServices.bikes.title", "Referencias de motocicletas en los que {0} se especializa");
                newLocaleStringResources.Add("Vendor.vendorServices.bikes.edit", "Actualiza los servicios en los que te especializas");
                newLocaleStringResources.Add("similarSearches", "Busquedas similares:");
                newLocaleStringResources.Add("similarSearches.title", "Busquedas similares para {0}");
                newLocaleStringResources.Add("similarSearches.suggest.title", "Busca resultados para {0}");
                newLocaleStringResources.Add("similarSearches.most", "Busquedas más comunes:");
                newLocaleStringResources.Add("similarSearches.most.title", "Estas son las busquedas más comunes en tuils");
                newLocaleStringResources.Add("Vendor.offices.noResults", "{0} no tiene sedes configuradas");
                newLocaleStringResources.Add("Vendor.offices.editar", "Actualiza tus sedes");
                newLocaleStringResources.Add("Vendor.offices.editar.alt", "Actualiza las sedes para que te conozcan mejor");
                newLocaleStringResources.Add("Vendor.products.noResults", "<b>{0}</b> no tiene productos registrados");
                newLocaleStringResources.Add("Vendor.products.noResults.publish", "Publica tu primer producto");
                newLocaleStringResources.Add("Offices.GoVendor", "Volver a mi tienda");
                newLocaleStringResources.Add("vendor.reviews.noRows", "Nadie a calificado a {0}");
                newLocaleStringResources.Add("Products.ForMyBike", "Productos para mi moto");
                newLocaleStringResources.Add("account.login", "Entrar");
                newLocaleStringResources.Add("account.register", "Unete a Tuils");
                newLocaleStringResources.Add("search.searchbox.tooltip", "¿Que estas buscando?");
                newLocaleStringResources.Add("custom.wishlist", "Mi lista de deseos");
                newLocaleStringResources.Add("custom.phonenumber", "3183413183");
                newLocaleStringResources.Add("custom.emailfooter", "contacto@tuils.com");
                newLocaleStringResources.Add("CUSTOM.JOINOURCOMMUNITY", "Nosotros");
                newLocaleStringResources.Add("ACCOUNT.MYACCOUNT", "Mi cuenta");
                newLocaleStringResources.Add("CUSTOM.CUSTOMERSERVICES", "Tuils web");
                newLocaleStringResources.Add("controlpanel.RepairShop", "Mi taller");
                newLocaleStringResources.Add("publishProduct.error.publishInvalidCategoryService", "Para poder publicar un servicio debes estar registrado como un taller");
                newLocaleStringResources.Add("enums.nop.core.CodeNopException.HasSessionActive", "Ya hay una sesión activa. Cierra primero sesión.");
                newLocaleStringResources.Add("PublishProduct.HasSpecialBikes", "¿Te gustaría destacar tu producto para algunas referencias de moto?");
                newLocaleStringResources.Add("PublishProduct.HasSpecialBikes.Alt", "Puedes agregar referencias de motocicletas en las cuales tu producto funcionará mejor. Esto permitira a los moteros con estas motocicletas encontrarte más fácil");
                newLocaleStringResources.Add("PublishProduct.Product.TitleForBrandBike", "Referencias de motos");
                newLocaleStringResources.Add("PublishProduct.PhoneNumber", "Teléfono de contacto");
                newLocaleStringResources.Add("Products.Vendor.PhoneNumber", "Teléfono de contacto");
                newLocaleStringResources.Add("LoginMessage.PublishProduct", "Para poder publicar debes registrarte, si ya tienes usuario ingresa tu usuario y clave");
                newLocaleStringResources.Add("LoginMessage.ShowVendor", "Para poder ver los datos del vendedor debes registrarte, si ya tienes usuario ingresa tu usuario y clave");
                newLocaleStringResources.Add("LoginMessage.AskQuestion", "Para poder preguntar debes registrarte, si ya tienes usuario ingresa tu usuario y clave");
                newLocaleStringResources.Add("CreateUser.Bike", "Referencia de tu moto");
                newLocaleStringResources.Add("MyAccount.Confirm", "Tus datos han sido actualizados correctamente");
                newLocaleStringResources.Add("Common.CloseButtonDialog", "Cerrar");
                newLocaleStringResources.Add("MyAccount.ModelInvalid", "Los datos están incompletos o mal diligenciados");
                newLocaleStringResources.Add("Offices.New", "Nueva sede");
                newLocaleStringResources.Add("VendorServices.Confirm", "Servicios actualizados correctamente");
                newLocaleStringResources.Add("Products.DetailShipping", "Detalles del domicilio");
                newLocaleStringResources.Add("Products.IncludeSupplies", "Incluye insumos");
                newLocaleStringResources.Add("Products.SuppliesValue", "Valor de los insumos");
                newLocaleStringResources.Add("MyOrders.NoRows.Rating", "No tienes compras calificadas todavía");
                newLocaleStringResources.Add("MyOrders.NoRows.Active", "No tienes compras, animate a buscar el producto que necesitas para tu moto. Acá van unas sugerencias");
                newLocaleStringResources.Add("MyOrders.NoRows.General", "No tienes compras, animate a buscar el producto que necesitas para tu moto. Acá van unas sugerencias");
                newLocaleStringResources.Add("MyOrders.NoRows.NoRating", "No tienes compras sin calificar");

                newLocaleStringResources.Add("MySales.NoRows.Rating", "No tienes ventas calificadas todavía");
                newLocaleStringResources.Add("MySales.NoRows.Active", "No tienes ventas activas, animate a publicar un producto");
                newLocaleStringResources.Add("MySales.NoRows.General", "No tienes ventas, animate a publicar un producto");
                newLocaleStringResources.Add("MySales.NoRows.NoRating", "No tienes ventas pendientes de calificación");
                newLocaleStringResources.Add("MyProducts.NoRows.Active", "No productos activos, animate a publicar uno");
                newLocaleStringResources.Add("MyProducts.NoRows.Inactive", "No tienes productos pendientes de activar, si quieres vender algo animate a hacerlo en Tuils");

                newLocaleStringResources.Add("MyOffices.NoRows", "No tienes sedes cargadas, puedes crear una ahora mismo");
                newLocaleStringResources.Add("Category.NoRows", "No hay productos activos en esta categoría");
                newLocaleStringResources.Add("Vendor.Offices.NoRows", "No hay sedes configuradas");
                newLocaleStringResources.Add("Vendor.Services.NoRows.Own", "¿Te especializas en algúna referencia de moto?");
                newLocaleStringResources.Add("Vendor.Services.NoRows.Other", "{0} no tiene servicios especiales");
                newLocaleStringResources.Add("Vendor.vendorServices.services.button", "Actualiza servicios");
                newLocaleStringResources.Add("Account.UnansweredQuestions.Alt", "Tienes {0} preguntas sin responder");
                newLocaleStringResources.Add("Catalog.FilterBy", "Filtrar");
                newLocaleStringResources.Add("PageTitle.Home", "Compra y vende motos, cascos, exostos y en Colombia");
                newLocaleStringResources.Add("Account.Register.Alt", "Registrate y vende tu moto usada");
                newLocaleStringResources.Add("Account.Login.Alt", "Entra y vende tu moto usada");
                newLocaleStringResources.Add("Account.Logout.Alt", "Salir");
                newLocaleStringResources.Add("Account.MyAccount.Alt", "Mi cuenta");
                newLocaleStringResources.Add("PublishProduct.SelectCategory", "Selecciona");
                newLocaleStringResources.Add("Products.SameVendor", "Más productos de este vendedor");
                newLocaleStringResources.Add("Custom.Twitter.Alt", "Siguenos en twitter @mototuils");
                newLocaleStringResources.Add("Custom.Facebook.Alt", "Siguenos en Facebook @mototuils");
                newLocaleStringResources.Add("PageTitle.Publish", "Vende tu moto usada, accesorios, repuestos y mucho más");
                newLocaleStringResources.Add("Publish.MetaDescription", "En Tuils puedes poner a la venta tu moto usada, accesorios para moto, repuestos para moto y mucho más en Colombia");
                newLocaleStringResources.Add("PageTitle.Publish.Service", "Vende servicios de mecánico para motos, pintura, calcomanias, cursos, lavado de motos");
                newLocaleStringResources.Add("Publish.Service.MetaDescription", "Si tienes un taller, academia de conducción puedes ofrecer y vender servicios relacionados con las motos. Lavado de motos, calcomanias, pintura para motos, mecanico para motos");
                newLocaleStringResources.Add("PageTitle.Publish.Bike", "Vende tu moto usada, accesorios, repuestos y mucho más");
                newLocaleStringResources.Add("Publish.Bike.MetaDescription", "Si tienes una moto usada vendela en tuils gratis, somos una comunidad en la que todos los motociclistas pueden encontrar lo que están buscando");
                newLocaleStringResources.Add("PageTitle.Publish.Product", "Vende accesorios, respuestos o cualquier cosa relacionada con las motos");
                newLocaleStringResources.Add("Publish.Product.MetaDescription", "Si tienes alguna parte de tu motocicleta que no usas, o simplemente quieres vender tu casco nuevo o usado solo tienes que publicarlo en Tuils y seguro conseguiras un buen comprador");
                newLocaleStringResources.Add("Publish.Bike.Img.Alt", "Vender una moto");
                newLocaleStringResources.Add("Publish.Product.Img.Alt", "Vender un producto, accesorio o repuesto para moto");
                newLocaleStringResources.Add("Publish.Service.Img.Alt", "Ofrecer y vender servicios");

                newLocaleStringResources.Add("PublishProduct.IsNew.Alt", "El producto es nuevo o usado");
                newLocaleStringResources.Add("PublishProduct.CarriagePlate.Alt", "La placa no se mostrará a nadie");
                newLocaleStringResources.Add("PublishProduct.Kms.Alt", "Recorrido en kilometros de tu moto");
                newLocaleStringResources.Add("PublishProduct.Year.Alt", "Año en que salió la moto");
                newLocaleStringResources.Add("PublishProduct.Color.Alt", "Color de tu moto");
                newLocaleStringResources.Add("PublishProduct.StateProvince.Alt", "Especifica la ciudad");
                newLocaleStringResources.Add("PublishProduct.Price.Alt", "Especifica el precio del producto sin comas ni puntos");
                newLocaleStringResources.Add("PublishProduct.ProductTitle.Alt", "Titulo con el que se mostrará tu producto o servicio");
                newLocaleStringResources.Add("PublishProduct.Condition.Alt", "Estado en que se encuentra tu moto");
                newLocaleStringResources.Add("PublishProduct.FullDescriptionBike.Alt", "Describe las características que quieras resaltar de tu moto");
                newLocaleStringResources.Add("PublishProduct.SuppliesValue.Alt", "Valor en pesos de los insumos requeridos");
                newLocaleStringResources.Add("PublishProduct.Supplies.Alt", "Insumos requeridos para prestar el servicio");
                newLocaleStringResources.Add("PublishProduct.IncludeSupplies.Alt", "Si tu asumes el costo de los insumos para realizar el trabajo deja esta opción seleccionada");
                newLocaleStringResources.Add("PublishProduct.DetailDoorToDoor.Alt", "Especifica cual es la cobertura máxima en caso de realizar el domicílio. Ej: Bogotá sector Suba");
                newLocaleStringResources.Add("PublishProduct.ShippingValueDoorToDoor.Alt", "Valor de realizar el domicilio");
                newLocaleStringResources.Add("PublishProduct.TitleDoorToDoor.Alt", "Si realizas domicilios deja seleccionada esta opción");
                newLocaleStringResources.Add("PublishProduct.ForBrandBikeService.Alt", "Especifica las referencias de motocicletas para las que tu servicio es más importante");
                newLocaleStringResources.Add("PublishProduct.ForBrandBike.Alt", "Especifica las referencias de motocicletas para las que tu producto es más importante");                
                newLocaleStringResources.Add("PublishProduct.FullDescriptionProduct.Alt", "Realiza una pequeña descripción del producto que deseas vender");
                newLocaleStringResources.Add("PublishProduct.HasShipping.Alt", "Si realizas envíos selecciona esta opción");
                newLocaleStringResources.Add("PublishProduct.NegotiationOptions.Alt", "Opciones de negocación y de estado de tu moto");
                newLocaleStringResources.Add("PublishProduct.AccesoriesOptions.Alt", "Accesorios con los que cuenta tu moto");
                newLocaleStringResources.Add("PublishProduct.Manufacturer.Alt", "Escoge la marca de el producto que deseas vender. Si no está en la lista selecciona \"Otro\"");
                newLocaleStringResources.Add("createuser.ConfirmMessage", "Has sido registrado correctamente. Tu clave ha sido enviada a tu correo");
                newLocaleStringResources.Add("Plugin.Misc.MailChimp.GeneralSuscriptionListId", "Id de lista general de newsletter");
                newLocaleStringResources.Add("Plugin.Misc.MailChimp.UserSuscriptionListId", "Id de lista usuarios registrados de tipo usuario");
                newLocaleStringResources.Add("Plugin.Misc.MailChimp.ShopSuscriptionListId", "Id de lista usuarios registrados de tipo tienda");
                newLocaleStringResources.Add("Plugin.Misc.MailChimp.RepairShopSuscriptionListId", "Id de lista usuarios registrados de tipo taller");
                newLocaleStringResources.Add("Plugin.Misc.MailChimp.DoubleOptin", "Habilitar doble opt-in");
                newLocaleStringResources.Add("Admin.Common.CategoriesHome", "Categorias del menu del home");
                newLocaleStringResources.Add("Homepage.Categories", "Categorías");
                newLocaleStringResources.Add("Admin.Catalog.Categories.Fields.NotAllowedToPublishProduct", "No permitir publicar productos con esta categoría");
                newLocaleStringResources.Add("Plugins.ExternalAuth.Facebook.Login", "Inicia sesión con Facebook");
                newLocaleStringResources.Add("CreateUser.Back.Before", "Si ya tienes cuenta en Tuils");
                newLocaleStringResources.Add("Search.Metadescription", "Te gustan las motos y no has encontrado cosas como: {0} {1}. Solo tienes que venir a Tuils y encontrarás los mejores accesorios, cascos, respuestos y motos.");
                newLocaleStringResources.Add("Account.PasswordRecovery.Back", "Volver al inicio");
                newLocaleStringResources.Add("Admin.Configuration.Settings.GeneralCommon.DisableRobotsForTestingSite", "Este sitio es de pruebas y es necesario deshabilitarle el robots.txt");
                newLocaleStringResources.Add("MySales.Date", "Fecha compra");
                newLocaleStringResources.Add("Admin.Configuration.Settings.Catalog.ProductLimit", "Limite de productos publicados por usuario");
                newLocaleStringResources.Add("PublishProduct.HasReachedLimitOfProductsUser", "Has alcanzado el limite de publicaciones gratis ({0}), te recomendamos destacar tu producto con alguno de nuestros planes.");
                newLocaleStringResources.Add("PublishProduct.HasReachedLimitOfProductsMarket", "Ha alcanzado el limite de productos a publicar en el plan que posee({0} productos). Adquiera un plan más alto para poder destacar mejor sus productos");

                newLocaleStringResources.Add("controlpanelindex.greeting.complement", "Para nosotros es un gusto recibirte en esta gran plataforma. Recuerda que aquí puedes publicar productos de forma GRATUITA para que logres hacer negocios como compras, ventas, adquisición de servicios, y mucho más.");
                newLocaleStringResources.Add("controlpanelindex.greeting.complement.shops", "Para nosotros es un gusto recibirte en esta gran plataforma. Recuerda que aquí puedes publicar productos para que logres hacer negocios como compras, ventas, adquisición de servicios, y mucho más.");
                newLocaleStringResources.Add("vendor.noEditedDescription", "Agregue una breve descripción de su tienda");
                newLocaleStringResources.Add("pageTitle.categoryDefault", "¿Donde comprar {0} {1}?");
                newLocaleStringResources.Add("Category.DefaultMetadescription", "Compra y vende {0} facilmente en tuils. Tenemos para ti los mejores productos relacionados con el mundo de las motos.");
                newLocaleStringResources.Add("Admin.Configuration.Settings.Catalog.ExpirationBikeReferencesKey", "Llave que valida en el cliente si se deben recargar las marcas de motos");
                newLocaleStringResources.Add("Admin.Catalog.Categories.Fields.ShowWithManufacturers", "Mostrar con las marcas en el home");
                newLocaleStringResources.Add("Admin.Catalog.Products.List.ShowUnpublised", "Mostrar solo los que no están publicados");
                newLocaleStringResources.Add("Admin.Customers.Customers.Fields.BikeReferenceName", "Referencia de moto seleccionada");
                newLocaleStringResources.Add("myproducts.numclicksformoreinfo", "Interesados en tu producto");
                newLocaleStringResources.Add("Admin.ContentManagement.Topics.Fields.TemplateName", "Nombre plantilla");
                newLocaleStringResources.Add("Admin.ContentManagement.Topics.Fields.TemplateName.hint", "Nombre del template que se usara para mostrar la noticia");
                newLocaleStringResources.Add("Admin.ContentManagement.Topics.Fields.FullWidth", "Activar ancho completo");
                newLocaleStringResources.Add("Admin.ContentManagement.Topics.Fields.FullWidth.hint", "Permite que el diseño tome un ancho completo aplicandole estilos diferentes");
                newLocaleStringResources.Add("PageTitle.EditProduct", "Editar producto");
                newLocaleStringResources.Add("EditProduct.Title", "Editar producto");
                newLocaleStringResources.Add("myproducts.editProduct", "Actualizar");
                newLocaleStringResources.Add("myproducts.editProduct.back", "Atrás");
                newLocaleStringResources.Add("myproducts.disable", "Eliminar");
                newLocaleStringResources.Add("myproducts.disable.alt", "Deshabilita el producto para que nadie lo pueda ver más");
                newLocaleStringResources.Add("Admin.ContentManagement.Topics.Fields.HideTitle", "Ocultar titulo");
                newLocaleStringResources.Add("Admin.ContentManagement.Topics.Fields.HideTitle.Hint", "Oculta el titulo por defecto");
                newLocaleStringResources.Add("product.additionalInfo", "Información adicional");
                newLocaleStringResources.Add("Filtering.MoreOptions", "Más opciones");
                newLocaleStringResources.Add("Admin.Catalog.Products.Fields.Sold", "Marcar como vendido");
                newLocaleStringResources.Add("Admin.Catalog.Products.Fields.Sold.Hint", "Marca como vendido un producto en el Home y no permite obtener el numero");
                newLocaleStringResources.Add("Products.Sold", "Vendido");
                newLocaleStringResources.Add("Products.ForYourBike", "PARA TU MOTO");
                newLocaleStringResources.Add("Admin.Catalog.Products.SpecialCategories", "Categorías especiales");
                newLocaleStringResources.Add("Admin.Catalog.Products.SpecialCategories.SaveBeforeEdit", "Primero guarde el producto");
                newLocaleStringResources.Add("Admin.Catalog.Products.SpecialCategories.NoCategoriesAvailable", "No hay categorías disponibles");
                newLocaleStringResources.Add("Admin.Catalog.Products.SpecialCategories.Fields.Category", "Categoría");
                newLocaleStringResources.Add("Admin.Catalog.Products.SpecialCategories.Fields.Category.Hint", "Categoría");
                newLocaleStringResources.Add("HeaderLinks.Home", "Home");
                newLocaleStringResources.Add("Account.SocialNetworks", "Nuestras redes:");
                newLocaleStringResources.Add("Admin.Configuration.Settings.Plans", "Planes Settings");
                newLocaleStringResources.Add("Admin.Configuration.Settings.Plans.CategoryProductPlans", "Categoría Planes Productos");
                newLocaleStringResources.Add("Admin.Configuration.Settings.Plans.CategoryStorePlans", "Categoría Planes Tiendas");
                newLocaleStringResources.Add("Admin.Configuration.Settings.Plans.SpecificationAttributeIdLimitDays", "Especificación limite de días");
                newLocaleStringResources.Add("Admin.Configuration.Settings.Plans.SpecificationAttributeIdPictures", "Especificación Limite de fotos");
                newLocaleStringResources.Add("Admin.Configuration.Settings.Plans.SpecificationAttributeIdDisplayOrder", "Especificación Orden de Exposición");
                newLocaleStringResources.Add("Admin.Configuration.Settings.Plans.SpecificationAttributeIdSliders", "Especificación Bandas rotativas");
                newLocaleStringResources.Add("Admin.Configuration.Settings.Plans.SpecificationAttributeIdHomePage", "Especificación HomePage");
                newLocaleStringResources.Add("Admin.Configuration.Settings.Plans.SpecificationAttributeIdSocialNetworks", "Especificación redes sociales");
                newLocaleStringResources.Add("Admin.Configuration.Settings.Plans.PlanProductsFree", "Plan gratis para productos");
                newLocaleStringResources.Add("Admin.Configuration.Settings.Plans.PlanStoresFree", "Plan gratis para tiendas");
                newLocaleStringResources.Add("Admin.Configuration.Settings.Order.MinutesBeforeCanAddPlanToCart", "Minutos para liberar carrito");
                newLocaleStringResources.Add("Admin.Configuration.Settings.Order.MinutesBeforeCanAddPlanToCart.Hint", "Número de minutos después de los que se puede reiniciar el carrito de compras cuando se desea adquirir un plan");
                newLocaleStringResources.Add("PageTitle.ResponsePayment", "Respuesta del pago realizado");
                newLocaleStringResources.Add("PageTitle.SelectPlan", "Selecciona un plan para destacar tus productos");


                newLocaleStringResources.Add("Plugins.PayUExternal.PaymentResponse.SelectedPlanName", "Plan seleccionado");
                newLocaleStringResources.Add("Plugins.PayUExternal.PaymentResponse.ReferenceCode", "Referencia 1");
                newLocaleStringResources.Add("Plugins.PayUExternal.PaymentResponse.ReferencePayUCode", "Referencia 2");
                newLocaleStringResources.Add("Plugins.PayUExternal.PaymentResponse.State", "Estado de la transacción");
                newLocaleStringResources.Add("Plugins.PayUExternal.PaymentResponse.TransactionValue", "Valor del pago");
                newLocaleStringResources.Add("Plugins.PayUExternal.PaymentResponse.Currency", "Moneda");
                newLocaleStringResources.Add("Plugins.PayUExternal.PaymentResponse.TransactionDate", "Fecha");
                newLocaleStringResources.Add("Plugins.PayUExternal.PaymentResponse.ProductName", "Producto destacado");
                newLocaleStringResources.Add("Plugins.PayUExternal.TransactionState.Approved", "Aprobado");
                newLocaleStringResources.Add("Plugins.PayUExternal.TransactionState.Declined", "Rechazado");
                newLocaleStringResources.Add("Plugins.PayUExternal.TransactionState.Expired", "Expirado");
                newLocaleStringResources.Add("Plugins.PayUExternal.TransactionState.Pending", "Pendiente");
                newLocaleStringResources.Add("Plugins.PayUExternal.TransactionState.Error", "Error");
                newLocaleStringResources.Add("Plugins.PayUExternal.PaymentMethodType.CreditCard", "Tarjeta de credito");
                newLocaleStringResources.Add("Plugins.PayUExternal.PaymentMethodType.PSE", "PSE");
                newLocaleStringResources.Add("Plugins.PayUExternal.PaymentMethodType.ACH", "ACH");
                newLocaleStringResources.Add("Plugins.PayUExternal.PaymentMethodType.DebitCard", "Debito");
                newLocaleStringResources.Add("Plugins.PayUExternal.PaymentMethodType.Cash", "Efectivo");
                newLocaleStringResources.Add("Plugins.PayUExternal.PaymentMethodType.Referenced", "Pago Referenciado");
                newLocaleStringResources.Add("Plugins.PayUExternal.PaymentMethodType.BankReferenced", "Pago en bancos");
                newLocaleStringResources.Add("Plugins.PayUExternal.ErrorResponse.External", "No fue posible procesar tu orden. Comunicate con servicio al cliente con la referencia {0} y codigo de error {1}");

                newLocaleStringResources.Add("Admin.Configuration.Settings.Plans.OptionAttributeFeaturedCategories", "Destacado en Categorias");
                newLocaleStringResources.Add("Admin.Configuration.Settings.Plans.OptionAttributeFeaturedManufacturers", "Destacado en marcas");
                newLocaleStringResources.Add("Admin.Configuration.Settings.Plans.OptionAttributeFeaturedRelated", "Destacado en productos relacionados");
                newLocaleStringResources.Add("Admin.Configuration.Settings.Plans.OptionAttributeFeaturedLeft", "Destacado a la izquierda");
                newLocaleStringResources.Add("Admin.Catalog.Products.Fields.SocialNetworkFeatured", "Destacado en redes sociales");
                newLocaleStringResources.Add("Admin.Catalog.Products.Fields.SocialNetworkFeatured.Hint", "Destacado en redes sociales");

                newLocaleStringResources.Add("Admin.Configuration.Settings.Plans.SpecificationAttributePlanDays", "Atributo Duración del plan");
                newLocaleStringResources.Add("Admin.Configuration.Settings.Plans.SpecificationAttributeIdOwnStore", "Atributo propia tienda");
                newLocaleStringResources.Add("Admin.Configuration.Settings.Plans.SpecificationAttributeIdFeaturedManufacturers", "Atributo marcas especializadas");
                newLocaleStringResources.Add("Admin.Configuration.Settings.Plans.SpecificationAttributeIdHelpWithStore", "Atributo ayuda con tienda");
                newLocaleStringResources.Add("Admin.Configuration.Settings.Plans.SpecificationAttributeIdProductsFeaturedOnSliders", "Atributo productos destacados");
                newLocaleStringResources.Add("Admin.Configuration.Settings.Plans.SpecificationAttributeIdProductsOnHomePage", "Atributo productos en homepage");
                newLocaleStringResources.Add("Admin.Configuration.Settings.Plans.SpecificationAttributeIdProductsOnSocialNetworks", "Atributo productos en redes sociales");

                newLocaleStringResources.Add("SelectFeaturedAttributesByPlan.SelectOnHome", "Página principal");
                newLocaleStringResources.Add("SelectFeaturedAttributesByPlan.SelectOnSliders", "Página principal de categorías y marcas");
                newLocaleStringResources.Add("SelectFeaturedAttributesByPlan.SelectOnSocialNetworks", "En redes sociales (Facebook, Instagram)");
                newLocaleStringResources.Add("SelectFeaturedAttributesByPlan.Save", "Destacar");

                newLocaleStringResources.Add("myproducts.featureByPlan", "Destacar");
                newLocaleStringResources.Add("SelectFeaturedAttributesByPlan.gotoMyProducts", "Volver a mis productos");
                newLocaleStringResources.Add("Admin.Configuration.Settings.Plans.SpecificationAttributeIdLimitProducts", "Limite de productos publicados");
                newLocaleStringResources.Add("SelectFeaturedAttributesByPlan.HasReachedLimitOfProducts", "Su plan ha llegado al limite de productos a publicar ({0} productos). Adquiera un plan más alto para poder destacar mejor sus productos");
                newLocaleStringResources.Add("SelectFeaturedAttributesByPlan.HasReachedLimitOfFeature", "Ha alcanzado el limite de productos destacados para el plan que tiene, por esta razón este producto no podrá ser destacado.");
                newLocaleStringResources.Add("SelectFeaturedAttributesByPlan.SelectPlan", "Adquirir Plan");
                newLocaleStringResources.Add("Plugins.PayUExternal.PaymentResponse.PublishProduct", "Publicar producto");
                newLocaleStringResources.Add("myproducts.featureByProduct", "Destacar");
                newLocaleStringResources.Add("MyOrders.Status", "Estado");
                newLocaleStringResources.Add("Myorders.Expired", "Vencido");
                newLocaleStringResources.Add("Myorders.Active", "Activo");
                newLocaleStringResources.Add("MyOrders.RenovatePlan", "Renovar");
                newLocaleStringResources.Add("MyOrders.UpgradePlan", "Subir el plan");
                newLocaleStringResources.Add("MyOrders.RenovatePlanNow", "Renovar Ahora");
                newLocaleStringResources.Add("MyOrders.SoonPlanWillExpire", "Quedan {0} días para que el plan que tiene actualmente expire. Lo puede renovar.");
                newLocaleStringResources.Add("MyOrders.BuyAPlan", "No pierdas la oporunidad de vender más. Adquiere uno de nuestros planes a muy bajo costo.");
                newLocaleStringResources.Add("MyOrders.BuyAPlanButton", "Comprar plan");

                newLocaleStringResources.Add("myproducts.enable", "Reactivar");
                newLocaleStringResources.Add("myproducts.enable.hint", "Permite que se puede visualizar nuevamente el producto");
                newLocaleStringResources.Add("Admin.Configuration.Settings.Catalog.NumberOfVendorsOnHome", "Número de vendors en el home");
                newLocaleStringResources.Add("Admin.Configuration.Settings.Catalog.NumberOfVendorsOnHome.Hint", "Número de vendors en el home");
                newLocaleStringResources.Add("Homepage.Vendors", "Vendedores destacados");
                newLocaleStringResources.Add("Admin.Catalog.Products.List.ShowHidden", "Mostrar productos ocultos");
                newLocaleStringResources.Add("Admin.Catalog.Products.List.ShowHidden.Hint", "Mostrar productos ocultos");
                newLocaleStringResources.Add("Admin.Configuration.Settings.Plans.RunLikeTest", "Sitio de Pruebas");


                newLocaleStringResources.Add("Admin.Vendors.List.ShowOnHome", "Vendedores en el Home");
                newLocaleStringResources.Add("Admin.Vendors.List.WithPlan", "Vendedores con plan");
                newLocaleStringResources.Add("Admin.Vendors.Fields.PlanExpiredOnUtc", "Plan expira en");


                newLocaleStringResources.Add("showplans.users", "Personas");
                newLocaleStringResources.Add("showplans.market", "Empresas");
                
                
                newLocaleStringResources.Add("showplans.selectPlanMarket", "Comprar plan");
                newLocaleStringResources.Add("showplans.selectPlanUser", "Publicar producto destacado");


                newLocaleStringResources.Add("showplans.contactForBetterPlanQuestion", "¿Necesita un plan más especifico?");
                newLocaleStringResources.Add("showplans.contactForBetterPlan", "Ya sea una tienda , taller, una empresa de producción o un concesionario, tenemos la solución perfecta para sus necesidades.");
                newLocaleStringResources.Add("showplans.contactForBetterPlanButton", "Ver soluciones para empresas");
                newLocaleStringResources.Add("OurPlans", "Nuestros planes");
                newLocaleStringResources.Add("OurPlans.Market", "Planes para empresas");
                newLocaleStringResources.Add("OurPlans.Users", "Planes para personas");


                newLocaleStringResources.Add("selectplan.buttonpay", "Pagar");
                newLocaleStringResources.Add("featuredProduct.ProductInformationTitle", "Detalles del producto");
                newLocaleStringResources.Add("Plugins.PayUExternal.PaymentResponse.Success", "El pago fue efectuado de manera correcta");
                newLocaleStringResources.Add("publishproduct.timeToActivate", "Recuerda que en pocos minutos podrás ver tu producto activado");
                newLocaleStringResources.Add("selectplan.continuewithoutplan", "Continuar sin destacar");
                newLocaleStringResources.Add("SelectFeaturedAttributesByPlan.SelectOnHome.Alt", "Número de productos destacados en la pagina de inicio");
                newLocaleStringResources.Add("SelectFeaturedAttributesByPlan.SelectOnSliders.Alt", "Número de productos destacados en la pagina en las bandas rotativas");
                newLocaleStringResources.Add("SelectFeaturedAttributesByPlan.SelectOnSocialNetworks.Alt", "Número de productos destacados en Facebook");


                newLocaleStringResources.Add("Plugins.PayUExternal.PaymentResponse.Error", "Lo sentimos, el pago no pudo realizarse");
                newLocaleStringResources.Add("Plugins.PayUExternal.ContinueWithoutPlan", "Publicar sin destacar");
                newLocaleStringResources.Add("Plugins.PayUExternal.TryAgain", "Volver a intentar");
                newLocaleStringResources.Add("Plugins.PayUExternal.PaymentResponse.SuccessPlan", "Gracias, el pago de su plan se ha hecho correctamente");
                newLocaleStringResources.Add("selectPlan.fields.stateprovince", "Departamento");
                newLocaleStringResources.Add("selectplan.subtitle.market", "Seleccione el tipo de plan ideal que desea para su empresa");
                newLocaleStringResources.Add("selectplan.subtitle.user", "Selecciona el tipo de publicación que deseas");
                newLocaleStringResources.Add("selectplan.additionalMessage.market", "A mayor visibilidad, tendrá más oportunidades de vender");
                newLocaleStringResources.Add("selectplan.additionalMessage.user", "A mayor visibilidad, tendrás más oportunidades de vendero");
                newLocaleStringResources.Add("Admin.Vendors.List.VendorType", "Tipo de vendedor");
                newLocaleStringResources.Add("Admin.Vendors.List.VendorType.Hint", "Tipo de vendedor");
                newLocaleStringResources.Add("Admin.Vendors.Fields.VendorType", "Tipo de vendedor");
                newLocaleStringResources.Add("Admin.Vendors.Fields.VendorType.Hint", "Tipo de vendedor");
                newLocaleStringResources.Add("Admin.Vendors.Fields.HasPlan", "Tiene plan");
                newLocaleStringResources.Add("Admin.Vendors.Fields.HasPlan.Hint", "Tiene plan");
                newLocaleStringResources.Add("Admin.Vendors.Fields.PlanName", "Plan seleccionado");
                newLocaleStringResources.Add("Admin.Vendors.Fields.PlanName.Hint", "Plan seleccionado");
                newLocaleStringResources.Add("PageTitle.Plans", "Escoge el plan que más se adecue para tus productos y vende muy fácil");
                newLocaleStringResources.Add("LoginMessage.GetPlanMarketLikeUserError", "No puedes seleccionar un plan de empresas ya que te encuentras registrado como persona. Si deseas cambiar el tipo de registro envianos un correo a info@tuils.com y te ayudaremos con todo gusto.");
                newLocaleStringResources.Add("myproducts.hasReachedLimitFeaturedAlert", "Ha alcanzado el limite de productos a destacar. Si desea puede  <a href='/mis-productos/seleccionar-plan'>adquirir un plan con mejores privilegios</a>");
                newLocaleStringResources.Add("Admin.Catalog.Products.Pictures.Fields.Active", "Activo");
                newLocaleStringResources.Add("Admin.Catalog.Products.Pictures.Fields.Active.Hint", "Activo");
                newLocaleStringResources.Add("myproducts.noPublished", "Pendiente aprobación");
                newLocaleStringResources.Add("myproducts.outOfDate", "Vencido");
                newLocaleStringResources.Add("Plugins.PayUExternal.Doubt", "Si tienes alguna sugerencia o pregunta no dudes en contactarnos a nuestro correo electrónico info@tuils.com");
                newLocaleStringResources.Add("Admin.Configuration.Settings.Plans.SpecificationAttributeIdMostExpensivePlan", "Caracteristica del plan más caro");
                newLocaleStringResources.Add("Admin.Configuration.Settings.Plans.SpecificationAttributeIdMostExpensivePlan.Hint", "Caracteristica del plan más caro");
                newLocaleStringResources.Add("Admin.Vendors.Fields.ShowOnHomePage", "Mostrar en el home");
                newLocaleStringResources.Add("Admin.Vendors.Fields.ShowOnHomePage.Hint", "Mostrar en el home");
                newLocaleStringResources.Add("myordersmarket.title", "Mi plan");
                newLocaleStringResources.Add("myordersmarketmodel.title", "Mi plan");
                newLocaleStringResources.Add("controlpanel.myordersmarket", "Mi plan");
                newLocaleStringResources.Add("Admin.Catalog.Products.List.ShowOnHomePage", "En el home");
                newLocaleStringResources.Add("Admin.Catalog.Products.List.ShowOnSliders", "En sliders");
                newLocaleStringResources.Add("Admin.Catalog.Products.List.ShowOnSN", "En redes sociales");
                newLocaleStringResources.Add("Admin.Configuration.Settings.Catalog.DefaultServicePicture", "Imagen por defecto categoría");
                newLocaleStringResources.Add("Admin.Configuration.Settings.Catalog.DefaultServicePicture.Hint", "Imagen por defecto categoría");
                newLocaleStringResources.Add("SelectFeaturedAttributesByPlan.ChooseOne", "Escoja la manera en que desea destacar su producto");
                newLocaleStringResources.Add("PageTitle.ConfirmationWithoutPlanUser", "Felicitaciones. Tu anuncio ha sido envíado de forma exitosa!");
                newLocaleStringResources.Add("PageTitle.ConfirmationWithoutPlanMarket", "Felicitaciones. Su anuncio ha sido envíado de forma exitosa!");
                newLocaleStringResources.Add("PublishProduct.PublishFinishedUser", "!Tu anuncio ha sido envíado de forma exitosa!");
                newLocaleStringResources.Add("PublishProduct.PublishFinishedMarket", "!Su anuncio ha sido publicado de forma exitosa!");
                newLocaleStringResources.Add("common.publishProductNoFree", "Publica tu anuncio");

                /***SOLO CORRER UNA VEZ***/
                newLocaleStringResources.Add("showplans.specificationAttributeDescription.Market.26", "Darémos asistencia completa para la configuración de su tienda virtual en Tuils. Tomamos fotografías profesionales de la sede principal * y las configuramos en su tienda virtual.  <br><br><div style='font-size:10px'>La toma de fotografías solo aplica en Bogotá y no incluye toma de fotografías de productos</div>");
                newLocaleStringResources.Add("showplans.specificationAttributeDescription.Market.25", "Número de productos que aparecerán en nuestras redes sociales de Facebook e Instagram. Si escogé un plan que permita publicar 2 productos en redes sociales, estos dos saldrán publicados en el transcurso del mes.* <br><br><div style='font-size:10px'>Solo se garantiza la publicación por redes sociales una vez por mes por producto</div>");
                newLocaleStringResources.Add("showplans.specificationAttributeDescription.Market.24", "Número de productos que aparecerán en la pagina principal de Tuils. Si escogé un plan que permita publicar 2 productos en la página principal, estos dos saldrán publicados en el transcurso del mes.* <br><br><div style='font-size:10px'>Debido a la cantidad de productos publicados en la página princial es posible que no todas las veces que recargue su navegador el producto salga en la pagina principal. Tenga en cuenta que debido a la gran cantidad de visitas que tenemos en Tuils mucha gente verá su producto destacado</div>");
                newLocaleStringResources.Add("showplans.specificationAttributeDescription.Market.23", "Número de productos que aparecerán en las paginas principales de categorías y marcas. Cuando seleccione un plan con esta característica los productos se verán en la parte superior de la categoría correspondiente al producto, así como a su correspondiente marca. Adicionalmente saldrá destacado como relacionado de productos que se encuentren en la misma categoría. * <br><br><div style='font-size:10px'>Debido a la cantidad de productos publicados como destacados es posible que no todas las veces que recargue su navegador el producto salga en la pagina principal. Tenga en cuenta que debido a la gran cantidad de visitas que tenemos en Tuils mucha gente verá su producto destacado.</div>");
                newLocaleStringResources.Add("showplans.specificationAttributeDescription.Market.22", "Número de productos que podrá publicar durante el tiempo que posea el plan.");
                newLocaleStringResources.Add("showplans.specificationAttributeDescription.Market.20", "Es como tener una pagina web propia. Podrá configurar nombre, logo, imagenes, productos, sedes y servicios que presta entre otros.");
                newLocaleStringResources.Add("showplans.specificationAttributeDescription.Market.19", "Número de días que estará vigente el plan. Después de pasado este tiempo los productos se despublicarán automáticamente y las ventajas de tener plan pago se perderán. En el caso de no poseer plan, los productos durarán publicados el tiempo especulado en la tabla de planes.");
                newLocaleStringResources.Add("showplans.specificationAttributeDescription.User.19", "Número de días tu producto estará publicado y activo");
                newLocaleStringResources.Add("showplans.specificationAttributeDescription.Market.18", "Su empresa será publicada en nuestras redes sociales Facebook e Instagram en el transcurso del mes.*<br><br> <div style='font-size:10px'>Solo se garantiza la publicación por redes sociales una vez por mes.</div>");
                newLocaleStringResources.Add("showplans.specificationAttributeDescription.User.18", "Tu producto será pulicado en nuestras redes sociales en el transcurso de los 3 primeros días después de la publicación.* <br><br><div style='font-size:10px'>Solo se garantiza la publicación por redes sociales una vez por publicación.</div>");
                newLocaleStringResources.Add("showplans.specificationAttributeDescription.Market.17", "Su empresa aparecerá en la pagina principal de Tuils durante el transcurso del plan.* <br><br><div style='font-size:10px'>Debido a la cantidad de productos publicados en la página princial es posible que no todas las veces que recargue su navegador el producto salga en la pagina principal. Tenga en cuenta que debido a la gran cantidad de visitas que tenemos en Tuils mucha gente verá su producto destacado</div>");
                newLocaleStringResources.Add("showplans.specificationAttributeDescription.User.17", "Tu producto aparecerá en la pagina principal de Tuils.* <br><br><div style='font-size:10px'>Debido a la cantidad de productos publicados en la página princial es posible que no todas las veces que recargues el navegador veas tu producto en la pagina principal.  Pero ten en cuenta que debido a la gran cantidad de visitas que tenemos en Tuils mucha gente seguramente verá tu producto ;).</div>");
                newLocaleStringResources.Add("showplans.specificationAttributeDescription.User.16", "Tu producto aparecerá en la pagina principales de la categoría y la marca a la que pertenece tu producto. Adicionalmente saldrá como producto relacionado de los productos que esten en la misma categoría de tu producto. * <br><br> <div style='font-size:10px'>Debido a la cantidad de productos publicados como destacados es posible que no todas las veces que recargues tu navegador veas tu producto. Pero ten en cuenta que ya que tenemos muchas visitas en Tuils mucha gente seguramente verá tu producto ;).</div>");
                newLocaleStringResources.Add("showplans.specificationAttributeDescription.Market.15", "La exposición es que tan arriba y resaltados se verán sus productos cuando un usuario realice una busqueda. Hay cuatro tipos de exposición: <ul><li><b>La mejor:</b> Su producto saldrá en la parte más alta de los listados y tendrá una etiqueta que lo resalte.</li><li><b>Muy alta:</b> Su producto saldrá en la parte alta de los listados debajo de 'la mejor'.</li><li><b>Alta:</b> Su producto saldrá en la parte media alta de los listados debajo de los que se encuentran en 'muy alta'.</li><li><b>Media:</b> Su producto saldrá debajo de todos los demás resultados.</li></ul>");
                newLocaleStringResources.Add("showplans.specificationAttributeDescription.User.15", "La exposición es que tan arriba y resaltados se verá tu producto cuando un usuario realice una busqueda. Hay cuatro tipos de exposición: <ul><li><b>La mejor:</b> Tu producto saldrá en la parte más alta de los listados y tendrá una etiqueta que lo resalte.</li><li><b>Muy alta:</b> Tu producto saldrá en la parte alta de los listados debajo de 'la mejor'.</li><li><b>Alta:</b> Tu producto saldrá en la parte media alta de los listados debajo de los que se encuentran en 'muy alta'.</li><li><b>Media:</b> Tu producto saldrá debajo de todos los demás resultados.</li></ul>");
                newLocaleStringResources.Add("showplans.specificationAttributeDescription.User.14", "Número de fotografías que podrás mostrar en tu producto");
                newLocaleStringResources.Add("showplans.specificationAttributeDescription.User.28", "Vamos con una camara profesional a tomar las fotografías de tu motocicleta.*<br><br><div style='font-size:10px'>Solo aplica para la ciudad de Bogotá</div>");
                newLocaleStringResources.Add("account.login.fields.email.required", "Ingresa el correo electrónico");
                newLocaleStringResources.Add("PageTitle.SelectFeaturedAttributesByPlan", "Destacar productos de acuerdo al plan");
                newLocaleStringResources.Add("admin.catalog.products.specificationattributes.fields.attributetype", "Tipo de atributo");
                newLocaleStringResources.Add("admin.catalog.products.specificationattributes.fields.value", "Valor");
                newLocaleStringResources.Add("showplans.specificationattributedisplayorder.0", "<span class='icon-star'></span><span class='icon-star'></span><span class='icon-star'></span><span class='icon-star'></span><span class='icon-star'></span>");
                newLocaleStringResources.Add("showplans.specificationattributedisplayorder.1", "<span class='icon-star'></span><span class='icon-star'></span><span class='icon-star'></span><span class='icon-star'></span>");
                newLocaleStringResources.Add("showplans.specificationattributedisplayorder.2", "<span class='icon-star'></span><span class='icon-star'></span><span class='icon-star'></span>");
                newLocaleStringResources.Add("showplans.specificationattributedisplayorder.3", "<span class='icon-star'></span><span class='icon-star'></span>");
                newLocaleStringResources.Add("Home.Vendors.Title", "Almacenes y talleres de confianza");
                
                

                //Recorre todas las llaves que desea adicional
                foreach (var resource in newLocaleStringResources)
                {
                    var resourceDB = context.Set<LocaleStringResource>().FirstOrDefault(s => s.ResourceName.Equals(resource.Key) && s.LanguageId == 2);
                    //Valida que estas llaves no existan
                    if (resourceDB == null)
                    {
                        //Agrega en los dos primeros idiomas
                        try
                        {
                            context.Set<LocaleStringResource>().Add(new LocaleStringResource()
                            {
                                ResourceName = resource.Key,
                                ResourceValue = resource.Value,
                                LanguageId = 1
                            });
                        }
                        catch
                        {
                        }

                        context.Set<LocaleStringResource>().Add(new LocaleStringResource()
                        {
                            ResourceName = resource.Key,
                            ResourceValue = resource.Value,
                            LanguageId = 2
                        });
                    }
                }
                #endregion
            }

            

            #endregion

            #region Manufacturers


            var manufacturers = new Manufacturer[]{
                    					new Manufacturer(){ Id=1, Name = "HJC", Description="HJC", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true, ShowOnHomePage = true },
                    new Manufacturer(){ Id=2, Name = "Shoei", Description="Shoei", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=3, Name = "AFX", Description="AFX", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=4, Name = "Arai", Description="Arai", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true, ShowOnHomePage = true },
                    new Manufacturer(){ Id=5, Name = "Nolan", Description="Nolan", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=6, Name = "Gmax", Description="Gmax", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true, ShowOnHomePage = true },
                    new Manufacturer(){ Id=7, Name = "AGV", Description="AGV", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true, ShowOnHomePage = true },
                    new Manufacturer(){ Id=8, Name = "Scorpion", Description="Scorpion", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=9, Name = "Shark", Description="Shark", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=10, Name = "Bell", Description="Bell", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=11, Name = "TORC", Description="TORC", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=12, Name = "Fly Racing", Description="Fly Racing", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=13, Name = "Speed and Strength", Description="Speed and Strength", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=14, Name = "Airoh", Description="Airoh", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=15, Name = "THH Helmets", Description="THH Helmets", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=16, Name = "Cyber Helmets", Description="Cyber Helmets", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=17, Name = "Suomy", Description="Suomy", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=18, Name = "Fox", Description="Fox", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=19, Name = "LS2 Helmets", Description="LS2 Helmets", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=20, Name = "Troy Lee Designs", Description="Troy Lee Designs", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=21, Name = "Skid Lid", Description="Skid Lid", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=22, Name = "Sparx", Description="Sparx", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=23, Name = "Answer", Description="Answer", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=24, Name = "Origine", Description="Origine", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=25, Name = "Joe Rocket", Description="Joe Rocket", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=26, Name = "Jafrum", Description="Jafrum", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=27, Name = "Alpinestars", Description="Alpinestars ", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=28, Name = "GAO", Description="GAO ", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=29, Name = "Rstaichi", Description="Rstaichi ", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=30, Name = "Billys Biker Gear", Description="Billys Biker Gear ", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=31, Name = "Tourmaster", Description="Tourmaster ", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=32, Name = "Spidi", Description="Spidi ", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=33, Name = "BMW", Description="BMW ", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=34, Name = "Cortech", Description="Cortech ", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=35, Name = "HMK", Description="HMK ", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=36, Name = "Xelement", Description="Xelement ", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=37, Name = "Fieldsheer", Description="Fieldsheer ", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=38, Name = "Klim", Description="Klim ", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=39, Name = "XXXXXX", Description="Speed and Strength ", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = false },
                    new Manufacturer(){ Id=40, Name = "Dainese", Description="Dainese ", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=41, Name = "Pokerun", Description="Pokerun ", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=42, Name = "Ducati", Description="Ducati ", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=43, Name = "Firstgear", Description="Firstgear ", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=44, Name = "Jackets 4 Bikes", Description="Jackets 4 Bikes ", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=45, Name = "A-Pro", Description="A-Pro ", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=46, Name = "xxxxxx", Description="xxxxxx", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = false },
                    new Manufacturer(){ Id=47, Name = "Indian", Description="Indian ", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=48, Name = "Meizhoushi", Description="Meizhoushi", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=49, Name = "Fantastic-Rider World", Description="Fantastic-Rider World", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=50, Name = "Motorstar", Description="Motorstar", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=51, Name = "EVS", Description="EVS", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=52, Name = "XXXXXX", Description="Troy Lee Designs", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = false },
                    new Manufacturer(){ Id=53, Name = "ABKK", Description="ABKK", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=54, Name = "XXXXXX", Description="xxxxxx", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = false },
                    new Manufacturer(){ Id=55, Name = "Saddlemen", Description="Saddlemen", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=56, Name = "Comple Buy", Description="Comple Buy", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=57, Name = "Shift", Description="Shift", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=58, Name = "Otro", Description="Otro", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },

                    //Nuevos de segunda migración
                    new Manufacturer(){ Id=59, Name = "MT", Description="MT", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=60, Name = "Battery Tender", Description="Battery Tender", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=61, Name = "NOCO", Description="NOCO", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=62, Name = "Schumacher", Description="Schumacher", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=63, Name = "Extreme Max", Description="Extreme Max", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=64, Name = "Yuasa", Description="Yuasa", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=65, Name = "Black & Decker", Description="Black & Decker", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=66, Name = "PowerStar", Description="PowerStar", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=67, Name = "Chrome Battery", Description="Chrome Battery", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=68, Name = "Buybits", Description="Buybits", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=69, Name = "Shotgun", Description="Shotgun", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=70, Name = "CTEK", Description="CTEK", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=71, Name = "RioRand", Description="RioRand", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=72, Name = "BuyBits Addons", Description="BuyBits Addons", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=73, Name = "Image", Description="Image", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=74, Name = "Rage Powersports", Description="Rage Powersports", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=75, Name = "Dragway Tools", Description="Dragway Tools", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=76, Name = "Pro Lift", Description="Pro Lift", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=77, Name = "Strongway", Description="Strongway", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=78, Name = "Milestone Tools", Description="Milestone Tools", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=79, Name = "TMS", Description="TMS", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=80, Name = "Pit Posse", Description="Pit Posse", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=81, Name = "Honda", Description="Honda", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=82, Name = "Maxima", Description="Maxima", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=83, Name = "Yamaha", Description="Yamaha", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=84, Name = "Motul", Description="Motul", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=85, Name = "Bicxoil", Description="Bicxoil", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=86, Name = "Coexito", Description="Coexito", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=87, Name = "Castrol", Description="Castrol", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=88, Name = "Acerbis", Description="Acerbis", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=89, Name = "Acfly", Description="Acfly", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=90, Name = "Athena Manufacturing", Description="Athena Manufacturing", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=91, Name = "BikeMaster", Description="BikeMaster", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=92, Name = "Delkevic", Description="Delkevic", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=93, Name = "Dr. ColorChip", Description="Dr. ColorChip", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=94, Name = "Dynojet", Description="Dynojet", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=95, Name = "EBC Brakes", Description="EBC Brakes", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=96, Name = "Emgo", Description="Emgo", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=97, Name = "EPI", Description="EPI", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=98, Name = "E-TOP", Description="E-TOP", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=99, Name = "FullSix", Description="FullSix", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=100, Name = "GAO", Description="GAO", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=101, Name = "HFP", Description="HFP", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=102, Name = "Huang", Description="Huang", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=103, Name = "JT Sprockets", Description="JT Sprockets", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=104, Name = "K&L Supply", Description="K&L Supply", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=105, Name = "K&N", Description="K&N", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=106, Name = "Kawasaki", Description="Kawasaki", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=107, Name = "KMG", Description="KMG", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=108, Name = "Lin", Description="Lin", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=109, Name = "Luo Luo", Description="Luo Luo", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=110, Name = "Maier", Description="Maier", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=111, Name = "MAO", Description="MAO", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=112, Name = "Memphis Shades", Description="Memphis Shades", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=113, Name = "Moto Onfire", Description="Moto Onfire", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=114, Name = "MOTOR-RACING", Description="MOTOR-RACING", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=115, Name = "Namura", Description="Namura", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=116, Name = "Niree", Description="Niree", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=117, Name = "Polaris", Description="Polaris", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=118, Name = "Polisport", Description="Polisport", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=119, Name = "Prox Racing Parts", Description="Prox Racing Parts", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=120, Name = "Quadboss", Description="Quadboss", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=121, Name = "Race-Driven", Description="Race-Driven", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=122, Name = "Ski-Doo", Description="Ski-Doo", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=123, Name = "Sky", Description="Sky", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=124, Name = "surmount", Description="surmount", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=125, Name = "Suzuki", Description="Suzuki", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=126, Name = "UFO Plastic", Description="UFO Plastic", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=127, Name = "Vesrah Racing", Description="Vesrah Racing", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=128, Name = "Volar Motorsport, Inc", Description="Volar Motorsport, Inc", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=129, Name = "V-Twin", Description="V-Twin", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=130, Name = "WSM", Description="WSM", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=131, Name = "XING", Description="XING", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=132, Name = "Yana Shiki Parts & Accessories", Description="Yana Shiki Parts & Accessories", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=133, Name = "Akrapovic ", Description="Akrapovic ", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=134, Name = "Yoshimura", Description="Yoshimura", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true },
                    new Manufacturer(){ Id=135, Name = "Icon", Description="Icon", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true }

                };


            if (runManufacturers)
            {
                var manufacturersTable = context.Set<Manufacturer>();
                
                manufacturersTable.AddOrUpdate(c => c.Id, manufacturers);
            }

            
            


            #endregion

            #region Manufacturer_Categories

            if (runManufacturersCategories)
            {
                var manufacturersCatTable = context.Set<Nop.Core.Domain.Catalog.ManufacturerCategory>();
                var manufacturersCat = new Nop.Core.Domain.Catalog.ManufacturerCategory[] { 
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 1, CategoryId =12, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 1, CategoryId =13, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 1, CategoryId =14, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 1, CategoryId =15, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 1, CategoryId =16, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 1, CategoryId =17, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 1, CategoryId =18, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 1, CategoryId =20, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 1, CategoryId =21, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 1, CategoryId =22, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 1, CategoryId =38, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 1, CategoryId =39, DisplayOrder = 1, IsFeaturedManufacturer = false  },

	
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 2, CategoryId =12, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 2, CategoryId =13, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 2, CategoryId =14, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 2, CategoryId =15, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 2, CategoryId =16, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 2, CategoryId =17, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 2, CategoryId =18, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 2, CategoryId =20, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 2, CategoryId =21, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 2, CategoryId =22, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 2, CategoryId =38, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 2, CategoryId =39, DisplayOrder = 1, IsFeaturedManufacturer = false  },

	
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 3, CategoryId =12, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 3, CategoryId =13, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 3, CategoryId =14, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 3, CategoryId =15, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 3, CategoryId =16, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 3, CategoryId =17, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 3, CategoryId =18, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 3, CategoryId =20, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 3, CategoryId =21, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 3, CategoryId =22, DisplayOrder = 1, IsFeaturedManufacturer = false  },

	
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 4, CategoryId =12, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 4, CategoryId =13, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 4, CategoryId =14, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 4, CategoryId =15, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 4, CategoryId =16, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 4, CategoryId =17, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 4, CategoryId =18, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 4, CategoryId =20, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 4, CategoryId =21, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 4, CategoryId =22, DisplayOrder = 1, IsFeaturedManufacturer = false  },


                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 5, CategoryId =12, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 5, CategoryId =13, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 5, CategoryId =14, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 5, CategoryId =15, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 5, CategoryId =16, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 5, CategoryId =17, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 5, CategoryId =18, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 5, CategoryId =20, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 5, CategoryId =21, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 5, CategoryId =22, DisplayOrder = 1, IsFeaturedManufacturer = false  },


                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 6, CategoryId =12, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 6, CategoryId =13, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 6, CategoryId =14, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 6, CategoryId =15, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 6, CategoryId =16, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 6, CategoryId =17, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 6, CategoryId =18, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 6, CategoryId =20, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 6, CategoryId =21, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 6, CategoryId =22, DisplayOrder = 1, IsFeaturedManufacturer = false  },


                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 7, CategoryId =12, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 7, CategoryId =13, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 7, CategoryId =14, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 7, CategoryId =15, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 7, CategoryId =16, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 7, CategoryId =17, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 7, CategoryId =18, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 7, CategoryId =20, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 7, CategoryId =21, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 7, CategoryId =22, DisplayOrder = 1, IsFeaturedManufacturer = false  },


                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 8, CategoryId =12, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 8, CategoryId =13, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 8, CategoryId =14, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 8, CategoryId =15, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 8, CategoryId =16, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 8, CategoryId =17, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 8, CategoryId =18, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 8, CategoryId =20, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 8, CategoryId =21, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 8, CategoryId =22, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 8, CategoryId =24, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 8, CategoryId =25, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 8, CategoryId =29, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 8, CategoryId =30, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 8, CategoryId =31, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 8, CategoryId =38, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 8, CategoryId =39, DisplayOrder = 1, IsFeaturedManufacturer = false  },


                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 9, CategoryId =12, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 9, CategoryId =13, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 9, CategoryId =14, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 9, CategoryId =15, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 9, CategoryId =16, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 9, CategoryId =17, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 9, CategoryId =18, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 9, CategoryId =20, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 9, CategoryId =21, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 9, CategoryId =22, DisplayOrder = 1, IsFeaturedManufacturer = false  },


                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 10, CategoryId =12, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 10, CategoryId =13, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 10, CategoryId =14, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 10, CategoryId =15, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 10, CategoryId =16, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 10, CategoryId =17, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 10, CategoryId =18, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 10, CategoryId =20, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 10, CategoryId =21, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 10, CategoryId =22, DisplayOrder = 1, IsFeaturedManufacturer = false  },


                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 11, CategoryId =12, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 11, CategoryId =13, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 11, CategoryId =14, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 11, CategoryId =15, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 11, CategoryId =16, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 11, CategoryId =17, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 11, CategoryId =18, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 11, CategoryId =20, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 11, CategoryId =21, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 11, CategoryId =22, DisplayOrder = 1, IsFeaturedManufacturer = false  },


                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 12, CategoryId =12, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 12, CategoryId =13, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 12, CategoryId =14, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 12, CategoryId =15, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 12, CategoryId =16, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 12, CategoryId =17, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 12, CategoryId =18, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 12, CategoryId =20, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 12, CategoryId =21, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 12, CategoryId =22, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 12, CategoryId =24, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 12, CategoryId =25, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 12, CategoryId =29, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 12, CategoryId =30, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 12, CategoryId =31, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 12, CategoryId =38, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 12, CategoryId =39, DisplayOrder = 1, IsFeaturedManufacturer = false  },


                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 13, CategoryId =12, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 13, CategoryId =13, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 13, CategoryId =14, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 13, CategoryId =15, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 13, CategoryId =16, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 13, CategoryId =17, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 13, CategoryId =18, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 13, CategoryId =20, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 13, CategoryId =21, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 13, CategoryId =22, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 13, CategoryId =24, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 13, CategoryId =25, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 13, CategoryId =29, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 13, CategoryId =30, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 13, CategoryId =31, DisplayOrder = 1, IsFeaturedManufacturer = false  },


                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 14, CategoryId =12, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 14, CategoryId =13, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 14, CategoryId =14, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 14, CategoryId =15, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 14, CategoryId =16, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 14, CategoryId =17, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 14, CategoryId =18, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 14, CategoryId =20, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 14, CategoryId =21, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 14, CategoryId =22, DisplayOrder = 1, IsFeaturedManufacturer = false  },


                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 15, CategoryId =12, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 15, CategoryId =13, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 15, CategoryId =14, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 15, CategoryId =15, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 15, CategoryId =16, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 15, CategoryId =17, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 15, CategoryId =18, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 15, CategoryId =20, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 15, CategoryId =21, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 15, CategoryId =22, DisplayOrder = 1, IsFeaturedManufacturer = false  },


                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 16, CategoryId =12, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 16, CategoryId =13, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 16, CategoryId =14, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 16, CategoryId =15, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 16, CategoryId =16, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 16, CategoryId =17, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 16, CategoryId =18, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 16, CategoryId =20, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 16, CategoryId =21, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 16, CategoryId =22, DisplayOrder = 1, IsFeaturedManufacturer = false  },


                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 17, CategoryId =12, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 17, CategoryId =13, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 17, CategoryId =14, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 17, CategoryId =15, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 17, CategoryId =16, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 17, CategoryId =17, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 17, CategoryId =18, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 17, CategoryId =20, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 17, CategoryId =21, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 17, CategoryId =22, DisplayOrder = 1, IsFeaturedManufacturer = false  },


                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 18, CategoryId =12, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 18, CategoryId =13, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 18, CategoryId =14, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 18, CategoryId =15, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 18, CategoryId =16, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 18, CategoryId =17, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 18, CategoryId =18, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 18, CategoryId =20, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 18, CategoryId =21, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 18, CategoryId =22, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 18, CategoryId =24, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 18, CategoryId =25, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 18, CategoryId =29, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 18, CategoryId =30, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 18, CategoryId =31, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 18, CategoryId =23, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 18, CategoryId =38, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 18, CategoryId =39, DisplayOrder = 1, IsFeaturedManufacturer = false  },


                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 19, CategoryId =12, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 19, CategoryId =13, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 19, CategoryId =14, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 19, CategoryId =15, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 19, CategoryId =16, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 19, CategoryId =17, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 19, CategoryId =18, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 19, CategoryId =20, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 19, CategoryId =21, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 19, CategoryId =22, DisplayOrder = 1, IsFeaturedManufacturer = false  },


                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 20, CategoryId =12, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 20, CategoryId =13, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 20, CategoryId =14, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 20, CategoryId =15, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 20, CategoryId =16, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 20, CategoryId =17, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 20, CategoryId =18, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 20, CategoryId =20, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 20, CategoryId =21, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 20, CategoryId =22, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 20, CategoryId =24, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 20, CategoryId =25, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 20, CategoryId =29, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 20, CategoryId =30, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 20, CategoryId =31, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 20, CategoryId =23, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 20, CategoryId =38, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 20, CategoryId =39, DisplayOrder = 1, IsFeaturedManufacturer = false  },


                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 21, CategoryId =12, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 21, CategoryId =13, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 21, CategoryId =14, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 21, CategoryId =15, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 21, CategoryId =16, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 21, CategoryId =17, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 21, CategoryId =18, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 21, CategoryId =20, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 21, CategoryId =21, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 21, CategoryId =22, DisplayOrder = 1, IsFeaturedManufacturer = false  },


                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 22, CategoryId =12, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 22, CategoryId =13, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 22, CategoryId =14, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 22, CategoryId =15, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 22, CategoryId =16, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 22, CategoryId =17, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 22, CategoryId =18, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 22, CategoryId =20, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 22, CategoryId =21, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 22, CategoryId =22, DisplayOrder = 1, IsFeaturedManufacturer = false  },


                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 23, CategoryId =12, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 23, CategoryId =13, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 23, CategoryId =14, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 23, CategoryId =15, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 23, CategoryId =16, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 23, CategoryId =17, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 23, CategoryId =18, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 23, CategoryId =20, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 23, CategoryId =21, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 23, CategoryId =22, DisplayOrder = 1, IsFeaturedManufacturer = false  },


                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 24, CategoryId =12, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 24, CategoryId =13, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 24, CategoryId =14, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 24, CategoryId =15, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 24, CategoryId =16, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 24, CategoryId =17, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 24, CategoryId =18, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 24, CategoryId =20, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 24, CategoryId =21, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 24, CategoryId =22, DisplayOrder = 1, IsFeaturedManufacturer = false  },


                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 25, CategoryId =12, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 25, CategoryId =13, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 25, CategoryId =14, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 25, CategoryId =15, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 25, CategoryId =16, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 25, CategoryId =17, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 25, CategoryId =18, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 25, CategoryId =20, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 25, CategoryId =21, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 25, CategoryId =22, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 25, CategoryId =24, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 25, CategoryId =25, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 25, CategoryId =29, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 25, CategoryId =30, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 25, CategoryId =31, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 25, CategoryId =23, DisplayOrder = 1, IsFeaturedManufacturer = false  },


                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 26, CategoryId =12, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 26, CategoryId =13, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 26, CategoryId =14, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 26, CategoryId =15, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 26, CategoryId =16, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 26, CategoryId =17, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 26, CategoryId =18, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 26, CategoryId =20, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 26, CategoryId =21, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 26, CategoryId =22, DisplayOrder = 1, IsFeaturedManufacturer = false  },


                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 27, CategoryId =24, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 27, CategoryId =25, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 27, CategoryId =29, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 27, CategoryId =30, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 27, CategoryId =31, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 27, CategoryId =23, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 27, CategoryId =38, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 27, CategoryId =39, DisplayOrder = 1, IsFeaturedManufacturer = false  },


                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 28, CategoryId =24, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 28, CategoryId =25, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 28, CategoryId =29, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 28, CategoryId =30, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 28, CategoryId =31, DisplayOrder = 1, IsFeaturedManufacturer = false  },


                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 29, CategoryId =24, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 29, CategoryId =25, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 29, CategoryId =29, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 29, CategoryId =30, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 29, CategoryId =31, DisplayOrder = 1, IsFeaturedManufacturer = false  },


                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 30, CategoryId =24, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 30, CategoryId =25, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 30, CategoryId =29, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 30, CategoryId =30, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 30, CategoryId =31, DisplayOrder = 1, IsFeaturedManufacturer = false  },


                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 31, CategoryId =24, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 31, CategoryId =25, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 31, CategoryId =29, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 31, CategoryId =30, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 31, CategoryId =31, DisplayOrder = 1, IsFeaturedManufacturer = false  },


                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 32, CategoryId =24, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 32, CategoryId =25, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 32, CategoryId =29, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 32, CategoryId =30, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 32, CategoryId =31, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 32, CategoryId =38, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 32, CategoryId =39, DisplayOrder = 1, IsFeaturedManufacturer = false  },


                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 33, CategoryId =24, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 33, CategoryId =25, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 33, CategoryId =29, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 33, CategoryId =30, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 33, CategoryId =31, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 33, CategoryId =38, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 33, CategoryId =39, DisplayOrder = 1, IsFeaturedManufacturer = false  },


                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 34, CategoryId =24, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 34, CategoryId =25, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 34, CategoryId =29, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 34, CategoryId =30, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 34, CategoryId =31, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 34, CategoryId =38, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 34, CategoryId =39, DisplayOrder = 1, IsFeaturedManufacturer = false  },


                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 35, CategoryId =24, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 35, CategoryId =25, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 35, CategoryId =29, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 35, CategoryId =30, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 35, CategoryId =31, DisplayOrder = 1, IsFeaturedManufacturer = false  },


                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 36, CategoryId =24, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 36, CategoryId =25, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 36, CategoryId =29, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 36, CategoryId =30, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 36, CategoryId =31, DisplayOrder = 1, IsFeaturedManufacturer = false  },


                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 37, CategoryId =24, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 37, CategoryId =25, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 37, CategoryId =29, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 37, CategoryId =30, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 37, CategoryId =31, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 37, CategoryId =38, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 37, CategoryId =39, DisplayOrder = 1, IsFeaturedManufacturer = false  },


                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 38, CategoryId =24, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 38, CategoryId =25, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 38, CategoryId =29, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 38, CategoryId =30, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 38, CategoryId =31, DisplayOrder = 1, IsFeaturedManufacturer = false  },


                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 39, CategoryId =24, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 39, CategoryId =25, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 39, CategoryId =29, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 39, CategoryId =30, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 39, CategoryId =31, DisplayOrder = 1, IsFeaturedManufacturer = false  },


                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 40, CategoryId =24, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 40, CategoryId =25, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 40, CategoryId =29, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 40, CategoryId =30, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 40, CategoryId =31, DisplayOrder = 1, IsFeaturedManufacturer = false  },



                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 41, CategoryId =24, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 41, CategoryId =25, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 41, CategoryId =29, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 41, CategoryId =30, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 41, CategoryId =31, DisplayOrder = 1, IsFeaturedManufacturer = false  },



                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 42, CategoryId =24, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 42, CategoryId =25, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 42, CategoryId =29, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 42, CategoryId =30, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 42, CategoryId =31, DisplayOrder = 1, IsFeaturedManufacturer = false  },

                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 43, CategoryId =24, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 43, CategoryId =25, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 43, CategoryId =29, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 43, CategoryId =30, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 43, CategoryId =31, DisplayOrder = 1, IsFeaturedManufacturer = false  },


                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 44, CategoryId =24, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 44, CategoryId =25, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 44, CategoryId =29, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 44, CategoryId =30, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 44, CategoryId =31, DisplayOrder = 1, IsFeaturedManufacturer = false  },


                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 45, CategoryId =24, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 45, CategoryId =25, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 45, CategoryId =29, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 45, CategoryId =30, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 45, CategoryId =31, DisplayOrder = 1, IsFeaturedManufacturer = false  },


                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 46, CategoryId =24, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 46, CategoryId =25, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 46, CategoryId =29, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 46, CategoryId =30, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 46, CategoryId =31, DisplayOrder = 1, IsFeaturedManufacturer = false  },


                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 47, CategoryId =24, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 47, CategoryId =25, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 47, CategoryId =29, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 47, CategoryId =30, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 47, CategoryId =31, DisplayOrder = 1, IsFeaturedManufacturer = false  },

                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 48, CategoryId =23, DisplayOrder = 1, IsFeaturedManufacturer = false  },


                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 49, CategoryId =23, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 49, CategoryId =38, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 49, CategoryId =39, DisplayOrder = 1, IsFeaturedManufacturer = false  },

                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 50, CategoryId =23, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 50, CategoryId =38, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 50, CategoryId =39, DisplayOrder = 1, IsFeaturedManufacturer = false  },

                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 51, CategoryId =23, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 51, CategoryId =38, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 51, CategoryId =39, DisplayOrder = 1, IsFeaturedManufacturer = false  },

                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 52, CategoryId =23, DisplayOrder = 1, IsFeaturedManufacturer = false  },

                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 53, CategoryId =38, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 53, CategoryId =39, DisplayOrder = 1, IsFeaturedManufacturer = false  },

                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 54, CategoryId =38, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 54, CategoryId =39, DisplayOrder = 1, IsFeaturedManufacturer = false  },

                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 55, CategoryId =38, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 55, CategoryId =39, DisplayOrder = 1, IsFeaturedManufacturer = false  },

                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 56, CategoryId =38, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 56, CategoryId =39, DisplayOrder = 1, IsFeaturedManufacturer = false  },

                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 57, CategoryId =12, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 57, CategoryId =13, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 57, CategoryId =14, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 57, CategoryId =15, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 57, CategoryId =16, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 57, CategoryId =17, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 57, CategoryId =18, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 57, CategoryId =20, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 57, CategoryId =21, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 57, CategoryId =22, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 57, CategoryId =38, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 57, CategoryId =39, DisplayOrder = 1, IsFeaturedManufacturer = false  },



                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 58, CategoryId =12, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 58, CategoryId =13, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 58, CategoryId =14, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 58, CategoryId =15, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 58, CategoryId =16, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 58, CategoryId =17, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 58, CategoryId =18, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 58, CategoryId =20, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 58, CategoryId =21, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 58, CategoryId =22, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 58, CategoryId =38, DisplayOrder = 1, IsFeaturedManufacturer = false  },
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 58, CategoryId =39, DisplayOrder = 1, IsFeaturedManufacturer = false  },


                //Nuevas de segunda migración

                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 60, ManufacturerId =12, DisplayOrder = 1, IsFeaturedManufacturer = false  } , 																		
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 62, ManufacturerId =68, DisplayOrder = 1, IsFeaturedManufacturer = false  } , 										
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 63, ManufacturerId =68, DisplayOrder = 1, IsFeaturedManufacturer = false  } , 										
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 64, ManufacturerId =68, DisplayOrder = 1, IsFeaturedManufacturer = false  } , 										
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 65, ManufacturerId =68, DisplayOrder = 1, IsFeaturedManufacturer = false  } , 										
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 66, ManufacturerId =68, DisplayOrder = 1, IsFeaturedManufacturer = false  } , 										
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 67, ManufacturerId =68, DisplayOrder = 1, IsFeaturedManufacturer = false  } , 										
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 68, ManufacturerId =68, DisplayOrder = 1, IsFeaturedManufacturer = false  } , 										
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 69, ManufacturerId =68, DisplayOrder = 1, IsFeaturedManufacturer = false  } , 										
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 70, ManufacturerId =68, DisplayOrder = 1, IsFeaturedManufacturer = false  } , 										
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 71, ManufacturerId =68, DisplayOrder = 1, IsFeaturedManufacturer = false  } , 										
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 72, ManufacturerId =68, DisplayOrder = 1, IsFeaturedManufacturer = false  } , 										
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 73, ManufacturerId =68, DisplayOrder = 1, IsFeaturedManufacturer = false  } , 										
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 74, ManufacturerId =68, DisplayOrder = 1, IsFeaturedManufacturer = false  } , 										
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 75, ManufacturerId =68, DisplayOrder = 1, IsFeaturedManufacturer = false  } , 										
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 76, ManufacturerId =67, DisplayOrder = 1, IsFeaturedManufacturer = false  } , 										
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 77, ManufacturerId =67, DisplayOrder = 1, IsFeaturedManufacturer = false  } , 										
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 78, ManufacturerId =67, DisplayOrder = 1, IsFeaturedManufacturer = false  } , 										
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 79, ManufacturerId =67, DisplayOrder = 1, IsFeaturedManufacturer = false  } , 										
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 80, ManufacturerId =67, DisplayOrder = 1, IsFeaturedManufacturer = false  } , 										
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 81, ManufacturerId =67, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 82, ManufacturerId =67, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 83, ManufacturerId =94, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 83, ManufacturerId =77, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 83, ManufacturerId =79, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 83, ManufacturerId =80, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 83, ManufacturerId =81, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 83, ManufacturerId =85, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 83, ManufacturerId =90, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 83, ManufacturerId =91, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 83, ManufacturerId =92, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 83, ManufacturerId =76, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 84, ManufacturerId =94, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 85, ManufacturerId =94, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 85, ManufacturerId =76, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 85, ManufacturerId =77, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 85, ManufacturerId =79, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 85, ManufacturerId =80, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 85, ManufacturerId =81, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 85, ManufacturerId =85, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 85, ManufacturerId =90, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 85, ManufacturerId =91, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 85, ManufacturerId =92, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 86, ManufacturerId =94, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 86, ManufacturerId =97, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 87, ManufacturerId =94, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 88, ManufacturerId =94, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 89, ManufacturerId =94, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 89, ManufacturerId =97, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 90, ManufacturerId =76, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 90, ManufacturerId =77, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 90, ManufacturerId =79, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 90, ManufacturerId =80, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 90, ManufacturerId =81, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 90, ManufacturerId =85, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 90, ManufacturerId =90, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 90, ManufacturerId =91, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 90, ManufacturerId =92, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 91, ManufacturerId =76, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 91, ManufacturerId =77, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 91, ManufacturerId =79, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 91, ManufacturerId =80, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 91, ManufacturerId =81, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 91, ManufacturerId =85, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 91, ManufacturerId =90, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 91, ManufacturerId =91, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 91, ManufacturerId =92, DisplayOrder = 1, IsFeaturedManufacturer = false  } , 
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 92, ManufacturerId =76, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 92, ManufacturerId =77, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 92, ManufacturerId =79, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 92, ManufacturerId =80, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 92, ManufacturerId =81, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 92, ManufacturerId =85, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 92, ManufacturerId =90, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 92, ManufacturerId =91, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 92, ManufacturerId =92, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 93, ManufacturerId =76, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 93, ManufacturerId =77, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 93, ManufacturerId =79, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 93, ManufacturerId =80, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 93, ManufacturerId =81, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 93, ManufacturerId =85, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 93, ManufacturerId =90, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 93, ManufacturerId =91, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 93, ManufacturerId =92, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 94, ManufacturerId =76, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 94, ManufacturerId =77, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 94, ManufacturerId =79, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 94, ManufacturerId =80, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 94, ManufacturerId =81, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 94, ManufacturerId =85, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 94, ManufacturerId =90, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 94, ManufacturerId =91, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 94, ManufacturerId =92, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 95, ManufacturerId =76, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 95, ManufacturerId =77, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 95, ManufacturerId =79, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 95, ManufacturerId =80, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 95, ManufacturerId =81, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 95, ManufacturerId =85, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 95, ManufacturerId =90, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 95, ManufacturerId =91, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 95, ManufacturerId =92, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 96, ManufacturerId =76, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 96, ManufacturerId =77, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 96, ManufacturerId =79, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 96, ManufacturerId =80, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 96, ManufacturerId =81, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 96, ManufacturerId =85, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 96, ManufacturerId =90, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 96, ManufacturerId =91, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 96, ManufacturerId =92, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 97, ManufacturerId =76, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 97, ManufacturerId =77, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 97, ManufacturerId =79, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 97, ManufacturerId =80, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 97, ManufacturerId =81, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 97, ManufacturerId =85, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 97, ManufacturerId =90, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 97, ManufacturerId =91, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 97, ManufacturerId =92, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 98, ManufacturerId =76, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 98, ManufacturerId =77, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 98, ManufacturerId =79, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 98, ManufacturerId =80, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 98, ManufacturerId =81, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 98, ManufacturerId =85, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 98, ManufacturerId =90, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 98, ManufacturerId =91, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 98, ManufacturerId =92, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 99, ManufacturerId =76, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 99, ManufacturerId =77, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 99, ManufacturerId =79, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 99, ManufacturerId =80, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 99, ManufacturerId =81, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 99, ManufacturerId =85, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 99, ManufacturerId =90, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 99, ManufacturerId =91, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 99, ManufacturerId =92, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 100, ManufacturerId =76, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 100, ManufacturerId =77, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 100, ManufacturerId =79, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 100, ManufacturerId =80, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 100, ManufacturerId =81, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 100, ManufacturerId =85, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 100, ManufacturerId =90, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 100, ManufacturerId =91, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 100, ManufacturerId =92, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 101, ManufacturerId =76, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 101, ManufacturerId =77, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 101, ManufacturerId =79, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 101, ManufacturerId =80, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 101, ManufacturerId =81, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 101, ManufacturerId =85, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 101, ManufacturerId =90, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 101, ManufacturerId =91, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 101, ManufacturerId =92, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 102, ManufacturerId =76, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 102, ManufacturerId =77, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 102, ManufacturerId =79, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 102, ManufacturerId =80, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 102, ManufacturerId =81, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 102, ManufacturerId =85, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 102, ManufacturerId =90, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 102, ManufacturerId =91, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 102, ManufacturerId =92, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 103, ManufacturerId =76, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 103, ManufacturerId =77, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 103, ManufacturerId =79, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 103, ManufacturerId =80, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 103, ManufacturerId =81, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 103, ManufacturerId =85, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 103, ManufacturerId =90, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 103, ManufacturerId =91, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 103, ManufacturerId =92, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 104, ManufacturerId =76, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 104, ManufacturerId =77, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 104, ManufacturerId =79, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 104, ManufacturerId =80, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 104, ManufacturerId =81, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 104, ManufacturerId =85, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 104, ManufacturerId =90, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 104, ManufacturerId =91, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 104, ManufacturerId =92, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 105, ManufacturerId =76, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 105, ManufacturerId =77, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 105, ManufacturerId =79, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 105, ManufacturerId =80, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 105, ManufacturerId =81, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 105, ManufacturerId =85, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 105, ManufacturerId =90, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 105, ManufacturerId =91, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 105, ManufacturerId =92, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 106, ManufacturerId =76, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 106, ManufacturerId =77, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 106, ManufacturerId =79, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 106, ManufacturerId =80, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 106, ManufacturerId =81, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 106, ManufacturerId =85, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 106, ManufacturerId =90, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 106, ManufacturerId =91, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 106, ManufacturerId =92, DisplayOrder = 1, IsFeaturedManufacturer = false  } , 
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 107, ManufacturerId =76, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 107, ManufacturerId =77, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 107, ManufacturerId =79, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 107, ManufacturerId =80, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 107, ManufacturerId =81, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 107, ManufacturerId =85, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 107, ManufacturerId =90, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 107, ManufacturerId =91, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 107, ManufacturerId =92, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 108, ManufacturerId =76, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 108, ManufacturerId =77, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 108, ManufacturerId =79, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 108, ManufacturerId =80, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 108, ManufacturerId =81, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 108, ManufacturerId =85, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 108, ManufacturerId =90, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 108, ManufacturerId =91, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 108, ManufacturerId =92, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 109, ManufacturerId =76, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 109, ManufacturerId =77, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 109, ManufacturerId =79, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 109, ManufacturerId =80, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 109, ManufacturerId =81, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 109, ManufacturerId =85, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 109, ManufacturerId =90, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 109, ManufacturerId =91, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 109, ManufacturerId =92, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 110, ManufacturerId =76, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 110, ManufacturerId =77, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 110, ManufacturerId =79, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 110, ManufacturerId =80, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 110, ManufacturerId =81, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 110, ManufacturerId =85, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 110, ManufacturerId =90, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 110, ManufacturerId =91, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 110, ManufacturerId =92, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 111, ManufacturerId =76, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 111, ManufacturerId =77, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 111, ManufacturerId =79, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 111, ManufacturerId =80, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 111, ManufacturerId =81, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 111, ManufacturerId =85, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 111, ManufacturerId =90, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 111, ManufacturerId =91, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 111, ManufacturerId =92, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 112, ManufacturerId =76, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 112, ManufacturerId =77, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 112, ManufacturerId =79, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 112, ManufacturerId =80, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 112, ManufacturerId =81, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 112, ManufacturerId =85, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 112, ManufacturerId =90, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 112, ManufacturerId =91, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 112, ManufacturerId =92, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 113, ManufacturerId =76, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 113, ManufacturerId =77, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 113, ManufacturerId =79, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 113, ManufacturerId =80, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 113, ManufacturerId =81, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 113, ManufacturerId =85, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 113, ManufacturerId =90, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 113, ManufacturerId =91, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 113, ManufacturerId =92, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 114, ManufacturerId =76, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 114, ManufacturerId =77, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 114, ManufacturerId =79, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 114, ManufacturerId =80, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 114, ManufacturerId =81, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 114, ManufacturerId =85, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 114, ManufacturerId =90, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 114, ManufacturerId =91, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 114, ManufacturerId =92, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 115, ManufacturerId =76, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 115, ManufacturerId =77, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 115, ManufacturerId =79, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 115, ManufacturerId =80, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 115, ManufacturerId =81, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 115, ManufacturerId =85, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 115, ManufacturerId =90, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 115, ManufacturerId =91, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 115, ManufacturerId =92, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 116, ManufacturerId =76, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 116, ManufacturerId =77, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 116, ManufacturerId =79, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 116, ManufacturerId =80, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 116, ManufacturerId =81, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 116, ManufacturerId =85, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 116, ManufacturerId =90, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 116, ManufacturerId =91, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 116, ManufacturerId =92, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 117, ManufacturerId =76, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 117, ManufacturerId =77, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 117, ManufacturerId =79, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 117, ManufacturerId =80, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 117, ManufacturerId =81, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 117, ManufacturerId =85, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 117, ManufacturerId =90, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 117, ManufacturerId =91, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 117, ManufacturerId =92, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 118, ManufacturerId =76, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 118, ManufacturerId =77, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 118, ManufacturerId =79, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 118, ManufacturerId =80, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 118, ManufacturerId =81, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 118, ManufacturerId =85, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 118, ManufacturerId =90, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 118, ManufacturerId =91, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 118, ManufacturerId =92, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 119, ManufacturerId =76, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 119, ManufacturerId =77, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 119, ManufacturerId =79, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 119, ManufacturerId =80, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 119, ManufacturerId =81, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 119, ManufacturerId =85, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 119, ManufacturerId =90, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 119, ManufacturerId =91, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 119, ManufacturerId =92, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 120, ManufacturerId =76, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 120, ManufacturerId =77, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 120, ManufacturerId =79, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 120, ManufacturerId =80, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 120, ManufacturerId =81, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 120, ManufacturerId =85, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 120, ManufacturerId =90, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 120, ManufacturerId =91, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 120, ManufacturerId =92, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 121, ManufacturerId =76, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 121, ManufacturerId =77, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 121, ManufacturerId =79, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 121, ManufacturerId =80, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 121, ManufacturerId =81, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 121, ManufacturerId =85, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 121, ManufacturerId =90, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 121, ManufacturerId =91, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 121, ManufacturerId =92, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 122, ManufacturerId =76, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 122, ManufacturerId =77, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 122, ManufacturerId =79, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 122, ManufacturerId =80, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 122, ManufacturerId =81, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 122, ManufacturerId =85, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 122, ManufacturerId =90, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 122, ManufacturerId =91, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 122, ManufacturerId =92, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 123, ManufacturerId =76, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 123, ManufacturerId =77, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 123, ManufacturerId =79, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 123, ManufacturerId =80, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 123, ManufacturerId =81, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 123, ManufacturerId =85, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 123, ManufacturerId =90, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 123, ManufacturerId =91, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 123, ManufacturerId =92, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 124, ManufacturerId =76, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 124, ManufacturerId =77, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 124, ManufacturerId =79, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 124, ManufacturerId =80, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 124, ManufacturerId =81, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 124, ManufacturerId =85, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 124, ManufacturerId =90, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 124, ManufacturerId =91, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 124, ManufacturerId =92, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 125, ManufacturerId =76, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 125, ManufacturerId =77, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 125, ManufacturerId =79, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 125, ManufacturerId =80, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 125, ManufacturerId =81, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 125, ManufacturerId =85, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 125, ManufacturerId =90, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 125, ManufacturerId =91, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 125, ManufacturerId =92, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 126, ManufacturerId =76, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 126, ManufacturerId =77, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 126, ManufacturerId =79, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 126, ManufacturerId =80, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 126, ManufacturerId =81, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 126, ManufacturerId =85, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 126, ManufacturerId =90, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 126, ManufacturerId =91, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 126, ManufacturerId =92, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 127, ManufacturerId =76, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 127, ManufacturerId =77, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 127, ManufacturerId =79, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 127, ManufacturerId =80, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 127, ManufacturerId =81, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 127, ManufacturerId =85, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 127, ManufacturerId =90, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 127, ManufacturerId =91, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 127, ManufacturerId =92, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 128, ManufacturerId =76, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 128, ManufacturerId =77, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 128, ManufacturerId =79, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 128, ManufacturerId =80, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 128, ManufacturerId =81, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 128, ManufacturerId =85, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 128, ManufacturerId =90, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 128, ManufacturerId =91, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 128, ManufacturerId =92, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 129, ManufacturerId =76, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 129, ManufacturerId =77, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 129, ManufacturerId =79, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 129, ManufacturerId =80, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 129, ManufacturerId =81, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 129, ManufacturerId =85, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 129, ManufacturerId =90, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 129, ManufacturerId =91, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 129, ManufacturerId =92, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 130, ManufacturerId =76, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 130, ManufacturerId =77, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 130, ManufacturerId =79, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 130, ManufacturerId =80, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 130, ManufacturerId =81, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 130, ManufacturerId =85, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 130, ManufacturerId =90, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 130, ManufacturerId =91, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 130, ManufacturerId =92, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 131, ManufacturerId =76, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 131, ManufacturerId =77, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 131, ManufacturerId =79, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 131, ManufacturerId =80, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 131, ManufacturerId =81, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 131, ManufacturerId =85, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 131, ManufacturerId =90, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 131, ManufacturerId =91, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 131, ManufacturerId =92, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 132, ManufacturerId =76, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 132, ManufacturerId =77, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 132, ManufacturerId =79, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 132, ManufacturerId =80, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 132, ManufacturerId =81, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 132, ManufacturerId =85, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 132, ManufacturerId =90, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 132, ManufacturerId =91, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 132, ManufacturerId =92, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 48, ManufacturerId =76, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 48, ManufacturerId =77, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 48, ManufacturerId =79, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 48, ManufacturerId =80, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 48, ManufacturerId =81, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 48, ManufacturerId =85, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 48, ManufacturerId =90, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 48, ManufacturerId =91, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 48, ManufacturerId =92, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 53, ManufacturerId =76, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 53, ManufacturerId =77, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 53, ManufacturerId =79, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 53, ManufacturerId =80, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 53, ManufacturerId =81, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 53, ManufacturerId =85, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 53, ManufacturerId =90, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 53, ManufacturerId =91, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 53, ManufacturerId =92, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 48, ManufacturerId =12, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 48, ManufacturerId =12, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 48, ManufacturerId =12, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 48, ManufacturerId =12, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 48, ManufacturerId =12, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 48, ManufacturerId =12, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 48, ManufacturerId =12, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 48, ManufacturerId =12, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 48, ManufacturerId =12, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 133, ManufacturerId =76, DisplayOrder = 1, IsFeaturedManufacturer = false  } ,
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ CategoryId = 134, ManufacturerId =76, DisplayOrder = 1, IsFeaturedManufacturer = false  } 
    

            };

                manufacturersCatTable.AddOrUpdate(c => new { c.CategoryId, c.ManufacturerId }, manufacturersCat);
            }

            #endregion

            #region Settings
            if (runSettings)
            {
                var settings = new Dictionary<string, string>();

                settings.Add("tuilssettings.productbasetypes_bike", "135");
                settings.Add("tuilssettings.productbasetypes_service", "103");
                settings.Add("tuilssettings.productbasetypes_product", "10");
                settings.Add("tuilssettings.tempuploadfiles", "~/TempFiles");
                settings.Add("tuilssettings.maxfileuploadsize", "5120000");
                settings.Add("tuilssettings.specificationattributecolor", "5");
                settings.Add("tuilssettings.specificationattributecondition", "6");
                settings.Add("publishproduct.isingaranty", "¿Está en garantía?");
                settings.Add("tuilssettings.specificationattributeaccesories", "8");
                settings.Add("tuilssettings.specificationattributenegotiation", "7");
                settings.Add("tuilssettings.specificationattributeoptionkms", "33");
                settings.Add("tuilssettings.specificationattributeoptioncarriageplate", "34");
                settings.Add("tuilssettings.defaultcountry", "1");
                settings.Add("tuilssettings.specificationattributesupplies", "12");
                settings.Add("vendorsettings.defaultpagesize", "6");
                settings.Add("mediasettings.vendormainthumbpicturesize", "500");
                settings.Add("mediasettings.vendorbackgroundthumbpicturesize", "1200");
                settings.Add("vendorsettings.defaultreviewspagesize", "3");
                settings.Add("controlpanelsettings.defaultpagesize", "10");
                settings.Add("mediasettings.officethumbpicturesizeoncontrolpanel", "120");
                settings.Add("tuilssettings.defaultstockquantity", "1000");
                settings.Add("header.publishproduct", "Publicar anuncio");
                settings.Add("tuilssettings.specificationattributebiketype", "4");
                settings.Add("catalogsettings.numbermanufacturersonhome", "5");
                settings.Add("catalogsettings.showmanufacturershomepage", "True");
                settings.Add("CatalogSettings.ProductSearchAutoCompleteWithSearchTerms", "True");
                settings.Add("captchasettings.showonproductquestions", "True");
                settings.Add("CatalogSettings.ShowSimilarSearches", "True");
                settings.Add("CatalogSettings.NumSuggestionSimilarSearches", "5");
                settings.Add("NumSuggestionSimilarSearchesHome", "10");
                settings.Add("CatalogSettings.NumSuggestionSimilarSearchesHome", "10");
                settings.Add("datetimesettings.jqueryformat", "mm/dd/yy");
                settings.Add("mediasettings.productImageMaxSizeResize", "2400");
                settings.Add("mediasettings.CoverImageMaxSizeResize", "2400");
                settings.Add("mediasettings.LogoImageMaxSizeResize", "1700");
                settings.Add("CatalogSettings.ShowMyBikeProductsOnHomepage", "True");
                settings.Add("CatalogSettings.NumberOfProductsMyBikeOnHomepage", "8");
                settings.Add("CatalogSettings.LimitOfSpecialCategories", "5");
                settings.Add("CatalogSettings.DefaultSpecificationAttributeTopMenu", "6");

                settings.Add("tuilssettings.specificationattributeIsNew", "1");
                settings.Add("tuilssettings.specificationattributeOptionIsNewYes", "1");
                settings.Add("tuilssettings.specificationattributeOptionIsNewNo", "2");
                settings.Add("catalogSettings.NumberOfProductsVendorProductsProductPage", "6");
                settings.Add("catalogSettings.CategoryOrganizationHomeMenu", "");
                settings.Add("catalogSettings.MaxColumnsCategoriesHome", "4");
                settings.Add("seoSettings.DisableRobotsForTestingSite", "False");
                settings.Add("catalogSettings.ProductLimitPublished", "3");
                settings.Add("tuilsSettings.SendMessageExpirationProductDaysBefore", "5");
                settings.Add("tuilssettings.Url_OG_ImageHome", "http://tuils.com/Content/images/ogHome.jpg");
                settings.Add("tuilssettings.Url_OG_ImageSellBike", "http://tuils.com/Content/images/ogBike.jpg");
                settings.Add("tuilssettings.Url_OG_ImageSellProduct", "http://tuils.com/Content/images/ogProduct.jpg");
                settings.Add("tuilssettings.Url_OG_ImageSellService", "http://tuils.com/Content/images/ogService.jpg");
                settings.Add("catalogSettings.NumberOfFeaturedProductsOnHomepage", "16");
                settings.Add("catalogSettings.LimitNumPictures", "7");
                settings.Add("tuilsSettings.MaxNumberOptionsToShowOnFilters", "4");
                settings.Add("vendorSettings.MinWidthCover", "1200");
                settings.Add("vendorSettings.MinHeightCover", "500");
                settings.Add("ShoppingCartSettings.allowgueststoaddcart", "False");
                settings.Add("CatalogSettings.ShowRelatedProductsAsFeatured", "False");
                settings.Add("CatalogSettings.NumberOfVendorsOnHome", "6");
                settings.Add("ordersettings.minutesbeforecanaddplantocart", "2");
                settings.Add("vendorsettings.DaysUpdateShopFirstEmail", "2");
                settings.Add("vendorsettings.DaysUpdateShopSecondEmail", "30");
                settings.Add("commonSettings.ActiveFacebookPixels", "701079780023326");

                
                


                //Recorre todas las llaves que desea adicional
                foreach (var setting in settings)
                {
                    var settingDB = context.Set<Setting>().FirstOrDefault(s => s.Name.Equals(setting.Key));
                    //Valida que estas llaves no existan
                    if (settingDB == null)
                    {
                        //Agrega en los dos primeros idiomas
                        context.Set<Setting>().Add(new Setting()
                        {
                            Name = setting.Key,
                            Value = setting.Value
                        });
                    }
                }
            }
            

            #endregion

            #region Template

            if (runTemplatesEmails)
            {
                var templatesTable = context.Set<MessageTemplate>();

                var templates = new List<MessageTemplate>(){
                    new MessageTemplate()
                    {
                        Id = 33,
                        Name = "Product.QuestionAnswered",
                        Subject = "%Store.Name%. Te han respondido la pregunta de %Product.Name%",
                        Body = "<p>Respondieron la pregunta:</p><p>&nbsp;</p><p>%Question.Answer%</p>",
                        IsActive = true,
                        EmailAccountId = 1
                    },
                    new MessageTemplate()
                    {
                        Id = 34,
                        Name = "Product.NewQuestion",
                        Subject = "%Store.Name%. Tienes una nueva pregunta del producto %Product.Name%",
                        Body = "<p>Te han hecho una nueva pregunta con el siguiente contenido:</p><p>&nbsp;</p><p>%Question.QuestionText%</p>",
                        IsActive = true,
                        EmailAccountId = 1
                    },
                    new MessageTemplate()
                    {
                        Id = 35,
                        Name = "Product.Published",
                        Subject = "%Store.Name%. Acabas de publicar un nuevo producto %Product.Name%",
                        Body = "<p>Acabas de publicar el producto <b>%Product.Name%</b> muy pronto recibiras un correo con la aprobación del producto </p>",
                        IsActive = true,
                        EmailAccountId = 1
                    },
                    new MessageTemplate()
                    {
                        Id = 36,
                        Name = "Product.PublishApproved",
                        Subject = "%Store.Name%. La publicación de %Product.Name% ha sido aprobada",
                        Body = "<p>Acabamos de aprobar la publicación del producto <b>%Product.Name%</b>. Miles de personas lo estan viendo desede este momento </p>",
                        IsActive = true,
                        EmailAccountId = 1
                    },
                    new MessageTemplate()
                    {
                        Id = 37,
                        Name = "Product.ExpirationProduct",
                        Subject = "%Store.Name%. La publicación de %Product.Name% está a punto de expirar",
                        Body = "<p>Tu producto  <b>%Product.Name%</b> expirará muy pronto en %Product.AvailableEndDateTimeUtc%. Si no lo has vendido y tu plan lo permite, activalo de nuevo </p>",
                        IsActive = true,
                        EmailAccountId = 1
                    },
                    new MessageTemplate()
                    {
                        Id = 38,
                        Name = "Product.ExpirationProductFinished",
                        Subject = "%Store.Name%. La publicación de %Product.Name% finalizó",
                        Body = "<p>Tu producto  <b>%Product.Name%</b> finalizó la publicación. Si no lo has vendido y tu plan lo permite, activalo de nuevo </p>",
                        IsActive = true,
                        EmailAccountId = 1
                    },
                    new MessageTemplate()
                    {
                        Id = 39,
                        Name = "Customer.WelcomeMessageMarket",
                        Subject = "Nos alegra mucho que su tienda se haya unido a Tuils",
                        Body = "Queremos comunicarle que el perfil ya está creado en nuestra plataforma y hemos generado una clave de forma automática XvhAMk. Si desea cambiarla ingrese y modifíquela en el perfil",
                        IsActive = true,
                        EmailAccountId = 1
                    },
                    new MessageTemplate()
                    {
                        Id = 40,
                        Name = "Customer.WelcomeMessageRepairShop",
                        Subject = "Nos alegra mucho que su taller se haya unido a Tuils",
                        Body = "Queremos comunicarle que el perfil ya está creado en nuestra plataforma y hemos generado una clave de forma automática XvhAMk. Si desea cambiarla ingrese y modifíquela en el perfil",
                        IsActive = true,
                        EmailAccountId = 1
                    },
                    new MessageTemplate()
                    {
                        Id = 41,
                        Name = "Product.PublishedMarket",
                        Subject = "Su tienda ha creado un nuevo anuncio en Tuils",
                        Body = "La información de Casco HJC Original está en proceso de validación y en las próximas horas le confirmaremos su aprobación.",
                        IsActive = true,
                        EmailAccountId = 1
                    },
                    new MessageTemplate()
                    {
                        Id = 42,
                        Name = "Product.PublishedRepairShop",
                        Subject = "Su taller ha creado un nuevo anuncio en Tuils",
                        Body = "La información de Casco HJC Original está en proceso de validación y en las próximas horas le confirmaremos su aprobación.",
                        IsActive = true,
                        EmailAccountId = 1
                    },
                    new MessageTemplate()
                    {
                        Id = 43,
                        Name = "Product.PublishApprovedMarket",
                        Subject = "Su anuncio ha sido aprobado",
                        Body = "Ahora puede comenzar a compartir esta información a través de sus redes sociales para que tenga más ofertas. ",
                        IsActive = true,
                        EmailAccountId = 1
                    },
                    new MessageTemplate()
                    {
                        Id = 44,
                        Name = "Product.PublishApprovedRepairShop",
                        Subject = "Su anuncio ha sido aprobado",
                        Body = "Ahora puede comenzar a compartir esta información a través de sus redes sociales para que tenga más ofertas. ",
                        IsActive = true,
                        EmailAccountId = 1
                    },
                    new MessageTemplate()
                    {
                        Id = 45,
                        Name = "Product.NewQuestionMarket",
                        Subject = "Han dejado una pregunta en su anuncio",
                        Body = "Responda la preguntas de los usuarios interesados en sus publicaciones",
                        IsActive = true,
                        EmailAccountId = 1
                    },
                    new MessageTemplate()
                    {
                        Id = 46,
                        Name = "Product.NewQuestionRepairShop",
                        Subject = "Han dejado una pregunta en su anuncio",
                        Body = "Responda la preguntas de los usuarios interesados en sus publicaciones",
                        IsActive = true,
                        EmailAccountId = 1
                    },
                    new MessageTemplate()
                    {
                        Id = 47,
                        Name = "Product.ExpirationProductMarket",
                        Subject = "Su anuncio se vencerá próximamente.",
                        Body = "Su anuncio se vencerá próximamente.",
                        IsActive = true,
                        EmailAccountId = 1
                    },
                    new MessageTemplate()
                    {
                        Id = 48,
                        Name = "Product.ExpirationProductRepairShop",
                        Subject = "Su anuncio se vencerá próximamente.",
                        Body = "Su anuncio se vencerá próximamente.",
                        IsActive = true,
                        EmailAccountId = 1
                    },
                    new MessageTemplate()
                    {
                        Id = 49,
                        Name = "Product.ExpirationProductFinishedMarket",
                        Subject = "%Store.Name%. La publicación de %Product.Name% finalizó",
                        Body = "<p>Tu producto  <b>%Product.Name%</b> finalizó la publicación. Si no lo has vendido y tu plan lo permite, activalo de nuevo </p>",
                        IsActive = true,
                        EmailAccountId = 1
                    },
                    new MessageTemplate()
                    {
                        Id = 50,
                        Name = "Product.ExpirationProductFinishedRepairShop",
                        Subject = "%Store.Name%. La publicación de %Product.Name% finalizó",
                        Body = "<p>Tu producto  <b>%Product.Name%</b> finalizó la publicación. Si no lo has vendido y tu plan lo permite, activalo de nuevo </p>",
                        IsActive = true,
                        EmailAccountId = 1
                    },
                    new MessageTemplate()
                    {
                        Id = 51,
                        Name = "Vendor.ExpirationPlan",
                        Subject = "%Store.Name%. El plan seleccionado para publicar está a punto de expirar",
                        Body = "<p>Tu plan expirará en %Vendor.PlanExpiredOnUtc%. Te invitamos a renovarlo </p>",
                        IsActive = true,
                        EmailAccountId = 1
                    },
                    new MessageTemplate()
                    {
                        Id = 52,
                        Name = "Vendor.PlanFinished",
                        Subject = "%Store.Name%. El plan seleccionado ha expirado",
                        Body = "<p> Los productos se desactivarán atumáticamente para vvolverlos a activar compre el plan de nuevo </p>",
                        IsActive = true,
                        EmailAccountId = 1
                    },
                    new MessageTemplate()
                    {
                        Id = 53,
                        Name = "Vendor.UpdateVirtualShop",
                        Subject = "%Store.Name%. Actualice su tienda virtual",
                        Body = "<p> Actualice su tienda virtual, le mostramos como. </p>",
                        IsActive = true,
                        EmailAccountId = 1
                    },
                    
                };

                foreach (var template in templates)
                {
                    var exists = templatesTable.FirstOrDefault(t => t.Name == template.Name);
                    if (exists == null)
                    {
                        context.Set<MessageTemplate>().Add(template);
                    }
                }
            }

            #endregion

            #region PermissionRecords

            if (runPermissions)
            {
                //var permissionTable = context.Set<PermissionRecord>();

                ////Consulta los permisos y empieza a agregarle todos los roles si es necesario
                //var pmEnableShoppingCart = permissionTable.FirstOrDefault(p => p.SystemName.Equals("EnableShoppingCart"));

                //var roles = context.Set<CustomerRole>();
                //foreach (var role in roles)
                //{
                //   if(pmEnableShoppingCart.CustomerRoles.FirstOrDefault(r => r.Id == role.Id) == null)
                //        pmEnableShoppingCart.CustomerRoles.Add(role);
                //}

            }

            #endregion

            #region Url

            if (runUrls)
            {
                var urlsTable = context.Set<UrlRecord>();


                var urls = new List<UrlRecord>();
                foreach (var item in specificationAttributeOptions.Where(s => s.SpecificationAttributeId == 4))
                {
                    urls.Add(new UrlRecord() { EntityName = "SpecificationAttributeOption", EntityId = item.Id, Slug = item.Name.ToLower().TrimEnd().Replace(" ", "-").Replace("xxxxxx", Guid.NewGuid().ToString()), LanguageId = 0, IsActive = true });
                }

                foreach (var item in categories)
                {
                    urls.Add(new UrlRecord() { EntityName = "Category", EntityId = item.Id, Slug = item.Name.ToLower().TrimEnd().Replace(" ", "-").Replace("xxxxxx", Guid.NewGuid().ToString()), LanguageId = 0, IsActive = true });
                }

                foreach (var item in manufacturers)
                {
                    urls.Add(new UrlRecord() { EntityName = "Manufacturer", EntityId = item.Id, Slug = item.Name.ToLower().TrimEnd().Replace(" ", "-").Replace("xxxxxx", Guid.NewGuid().ToString()), LanguageId = 0, IsActive = true });
                }

                //urlsTable.AddOrUpdate(sa => sa.Slug, urls.ToArray());
                //Recorre todas las llaves que desea adicional
                foreach (var url in urls)
                {
                    var settingDB = context.Set<UrlRecord>().FirstOrDefault(s => s.Slug.Equals(url.Slug));
                    //Valida que estas llaves no existan
                    if (settingDB == null)
                    {
                        //Agrega en los dos primeros idiomas
                        context.Set<UrlRecord>().Add(url);
                    }
                }
            }
		    
	        #endregion

            #region Task
            if (runTasks)
            {
                var tasksTable = context.Set<ScheduleTask>();
                var tasks = new List<ScheduleTask>(){
                    new ScheduleTask()
                    {
                        Id = 7,
                        Name = "Load Bikes Reference Alive",
                        Seconds = 120,
                        Enabled = true,
                        StopOnError = false,
                        Type = "Nop.Services.Common.LoadBikesCacheTask, Nop.Services"
                    },
                    new ScheduleTask()
                    {
                        Id = 8,
                        Name = "Vencimiento de publicaciones",
                        Seconds = 14400,
                        Enabled = true,
                        StopOnError = false,
                        Type = "Nop.Services.Common.PublishingAlmostFinishedTask, Nop.Services"
                    },
                    new ScheduleTask()
                    {
                        Id = 11,
                        Name = "Vencimiento de planes",
                        Seconds = 43200,
                        Enabled = true,
                        StopOnError = false,
                        Type = "Nop.Services.Vendors.ValidateVendorExpiredPlansTask, Nop.Services"
                    }

                };

                foreach (var task in tasks)
                {
                    var exists = tasksTable.FirstOrDefault(t => t.Id == task.Id);
                    if (exists == null)
                        context.Set<ScheduleTask>().Add(task);
                }
            }
            #endregion
        }
    }
}
