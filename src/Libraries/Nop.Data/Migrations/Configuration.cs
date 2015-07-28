namespace Nop.Data.Migrations
{
    using Nop.Core.Data;
    using Nop.Core.Domain.Catalog;
    using Nop.Core.Domain.Configuration;
    using Nop.Core.Domain.Directory;
    using Nop.Core.Domain.Localization;
    using Nop.Core.Domain.Messages;
    using Nop.Core.Domain.Seo;
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
            bool runSettings = true;
            bool runTemplatesEmails = false;
            bool runUrls = false;



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
                    new SpecificationAttribute() { Id = 12, Name = "Insumos", DisplayOrder = 0 }
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
                    new Category() { Id = 161, Name = "Bolt", Description = "Bolt", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 5  } ,
                    new Category() { Id = 162, Name = "Bws", Description = "Bws", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 9  } ,
                    new Category() { Id = 163, Name = "Calimatic", Description = "Calimatic", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 8  } ,
                    new Category() { Id = 164, Name = "Dragstar", Description = "Dragstar", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 5  } ,
                    new Category() { Id = 165, Name = "Dt", Description = "Dt", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 8  } ,
                    new Category() { Id = 166, Name = "Dt-125", Description = "Dt-125", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 8  } ,
                    new Category() { Id = 167, Name = "FZ8", Description = "FZ8", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 7  } ,
                    new Category() { Id = 168, Name = "FZ8 n", Description = "FZ8 n", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 7  } ,
                    new Category() { Id = 169, Name = "FZ8 s", Description = "FZ8 s", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 7  } ,
                    new Category() { Id = 170, Name = "Fazer 16", Description = "Fazer 16", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 6  } ,
                    new Category() { Id = 171, Name = "Fazer 600", Description = "Fazer 600", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 7  } ,
                    new Category() { Id = 172, Name = "Fino", Description = "Fino", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 9  } ,
                    new Category() { Id = 173, Name = "Fz 16", Description = "Fz 16", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 6  } ,
                    new Category() { Id = 174, Name = "Fz 1", Description = "Fz 1", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 7  } ,
                    new Category() { Id = 175, Name = "Fz 1 n", Description = "Fz 1 n", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 7  } ,
                    new Category() { Id = 176, Name = "Fz 1 s", Description = "Fz 1 s", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 7  } ,
                    new Category() { Id = 177, Name = "Fz 6", Description = "Fz 6", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 7  } ,
                    new Category() { Id = 178, Name = "Fz 6 S", Description = "Fz 6 S", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 7  } ,
                    new Category() { Id = 179, Name = "Grizzly", Description = "Grizzly", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  } ,
                    new Category() { Id = 180, Name = "Monoshock", Description = "Monoshock", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 8  } ,
                    new Category() { Id = 181, Name = "Mt 07", Description = "Mt 07", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 7  } ,
                    new Category() { Id = 182, Name = "Mt 09", Description = "Mt 09", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 7  } ,
                    new Category() { Id = 183, Name = "R 1", Description = "R 1", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 7  } ,
                    new Category() { Id = 184, Name = "R 15", Description = "R 15", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 7  } ,
                    new Category() { Id = 185, Name = "R6", Description = "R6", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 7  } ,
                    new Category() { Id = 186, Name = "R6 r", Description = "R6 r", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 7  } ,
                    new Category() { Id = 187, Name = "R6 s", Description = "R6 s", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 7  } ,
                    new Category() { Id = 188, Name = "R3", Description = "R3", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 7  } ,
                    new Category() { Id = 189, Name = "Raider", Description = "Raider", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 5  } ,
                    new Category() { Id = 190, Name = "Raptor", Description = "Raptor", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  } ,
                    new Category() { Id = 191, Name = "Raptor 250", Description = "Raptor 250", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  } ,
                    new Category() { Id = 192, Name = "Raptor 700 r", Description = "Raptor 700 r", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  } ,
                    new Category() { Id = 193, Name = "Raptor 80", Description = "Raptor 80", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  } ,
                    new Category() { Id = 194, Name = "Rx", Description = "Rx", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 6  } ,
                    new Category() { Id = 195, Name = "Super Tenere", Description = "Super Tenere", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 10  } ,
                    new Category() { Id = 196, Name = "Sz", Description = "Sz", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 6  } ,
                    new Category() { Id = 197, Name = "Tdm 850", Description = "Tdm 850", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 10  } ,
                    new Category() { Id = 198, Name = "Tdm 900", Description = "Tdm 900", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 10  } ,
                    new Category() { Id = 199, Name = "V", Description = "V", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 5  } ,
                    new Category() { Id = 200, Name = "Virago", Description = "Virago", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 6  } ,
                    new Category() { Id = 201, Name = "V star", Description = "V star", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 5  } ,
                    new Category() { Id = 202, Name = "Vulcan", Description = "Vulcan", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 5  } ,
                    new Category() { Id = 203, Name = "Wolverine", Description = "Wolverine", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  } ,
                    new Category() { Id = 204, Name = "Wr", Description = "Wr", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 8  } ,
                    new Category() { Id = 205, Name = "Xj 6", Description = "Xj 6", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 10  } ,
                    new Category() { Id = 206, Name = "Xj 6 f", Description = "Xj 6 f", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 10  } ,
                    new Category() { Id = 207, Name = "Xj 6 n", Description = "Xj 6 n", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 10  } ,
                    new Category() { Id = 208, Name = "Xt", Description = "Xt", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 8  } ,
                    new Category() { Id = 209, Name = "Xt 1200", Description = "Xt 1200", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 8  } ,
                    new Category() { Id = 210, Name = "Xt 225", Description = "Xt 225", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 8  } ,
                    new Category() { Id = 211, Name = "Xt 500", Description = "Xt 500", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 8  } ,
                    new Category() { Id = 212, Name = "Xt 600", Description = "Xt 600", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 8  } ,
                    new Category() { Id = 213, Name = "Xt 660", Description = "Xt 660", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 8  } ,
                    new Category() { Id = 214, Name = "Xtz", Description = "Xtz", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 8  } ,
                    new Category() { Id = 215, Name = "Xv", Description = "Xv", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 5  } ,
                    new Category() { Id = 216, Name = "Ybr", Description = "Ybr", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 6  } ,
                    new Category() { Id = 217, Name = "Ybr 125", Description = "Ybr 125", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 6  } ,
                    new Category() { Id = 218, Name = "Ybr 250", Description = "Ybr 250", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = 6  } ,
                    new Category() { Id = 219, Name = "Yfm", Description = "Yfm", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  } ,
                    new Category() { Id = 220, Name = "Yfz", Description = "Yfz", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  } ,
                    new Category() { Id = 221, Name = "Yz", Description = "Yz", CategoryTemplateId = 1, ParentCategoryId = 137, PictureId = 0, PageSize = 4, AllowCustomersToSelectPageSize = true,  PageSizeOptions = "8, 4, 12",  ShowOnHomePage = false, IncludeInTopMenu = false, HasDiscountsApplied = false, Published = true, Deleted = false, CreatedOnUtc = DateTime.Now, UpdatedOnUtc= DateTime.Now, ChildrenCategoriesStr = "",  SpecificationAttributeOptionId = null  } 
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
                newLocaleStringResources.Add("PublishProduct.TimeOutPost", "¡Tu publicación es Gratuita y durará {0} días!");
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
                newLocaleStringResources.Add("MyOrders.Description", "Lorem Ipsum es simplemente el texto de relleno de las imprentas y archivos de texto. Lorem Ipsum ha sido el texto de relleno estándar de las industrias desde el año 1500");
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
                newLocaleStringResources.Add("common.publishProduct", "Publica tu anuncio");
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
                    new Manufacturer(){ Id=58, Name = "Otro", Description="Otro", CreatedOnUtc = DateTime.Now, UpdatedOnUtc = DateTime.Now, PageSize = 12, PageSizeOptions = "4,8,12", Published = true }
                };


            if (runManufacturers)
            {
                var manufacturersTable = context.Set<Manufacturer>();
                
                manufacturersTable.AddOrUpdate(c => c.Id, manufacturers);


                #region Manufacturer_Categories
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
                new Nop.Core.Domain.Catalog.ManufacturerCategory(){ ManufacturerId = 58, CategoryId =39, DisplayOrder = 1, IsFeaturedManufacturer = false  }

            };

                manufacturersCatTable.AddOrUpdate(c => c.Id, manufacturersCat);
                #endregion
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
                settings.Add("tuilssettings.specificationattributeoptionkms", "9");
                settings.Add("tuilssettings.specificationattributeoptioncarriageplate", "11");
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
                settings.Add("CatalogSettings.LimitDaysOfProductPublished", "18");
                settings.Add("CatalogSettings.DefaultSpecificationAttributeTopMenu", "6");


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
                }
            };

                templatesTable.AddOrUpdate(t => t.Id, templates.ToArray());
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

                urlsTable.AddOrUpdate(sa => sa.Slug, urls.ToArray());
            }
		    
	        #endregion
        }
    }
}
