
/******
	ACTUALIZAR IDIOMA DE LAS TABLAS PARA QUE NO TENGA EN CUENTA LOS ACENTOS
******/

---Actualiza el idioma de las tablas de Producto
ALTER FULLTEXT CATALOG [nopCommerceFullTextCatalog] REBUILD WITH ACCENT_SENSITIVITY = OFF
GO
ALTER FULLTEXT INDEX ON [dbo].[ProductTag] DROP ([Name]) 
GO
ALTER FULLTEXT INDEX ON [dbo].[ProductTag] ADD ([Name] LANGUAGE [Spanish])
GO
ALTER FULLTEXT INDEX ON [dbo].[LocalizedProperty] DROP ([LocaleValue]) 
GO
ALTER FULLTEXT INDEX ON [dbo].[LocalizedProperty] ADD ([LocaleValue] LANGUAGE [Spanish])
GO
---Actualiza el idioma de las tablas de Busqueda
ALTER FULLTEXT CATALOG [nopCommerceSearchingTerms] REBUILD WITH ACCENT_SENSITIVITY = OFF
GO
ALTER FULLTEXT INDEX ON [dbo].[SearchTerm] DROP ([Keyword]) 
GO
ALTER FULLTEXT INDEX ON [dbo].[SearchTerm] ADD ([Keyword] LANGUAGE [Spanish])
GO
/*******
	CREAR UNA LISTA DE STOPWORDS
*******/
GO
CREATE FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product];
/*****
	ASOCIAR LAS PALABRAS QUE DEBEN IR
*****/
go
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD '$' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'Ud.' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'Uds.' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'Vd.' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'Vds.' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'ah' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'ajá' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'ale' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'algo' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'alta' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'alto' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'ante' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'arre' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'así' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'aun' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'ay' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'aúpa' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'bah' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'bajo' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'bien' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'cabe' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'cada' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'chau' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'che' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'chis' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'cien' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'como' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'con' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'cual' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'cuya' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'cuyo' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'cuál' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'cómo' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'daca' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'de' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'deba' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'debe' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'debo' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'debí' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'diez' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'doce' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'dos' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'ea' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'eh' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'ejem' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'el' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'ella' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'ello' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'en' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'epa' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'era' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'eran' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'eras' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'eres' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'ergo' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'es' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'esa' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'esas' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'ese' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'eso' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'esos' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'esta' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'este' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'esto' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'está' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'esté' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'fu' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'fue' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'fui' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'gua' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'ha' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'hala' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'han' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'has' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'hay' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'haya' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'he' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'hola' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'hopo' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'hube' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'hubo' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'huy' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'ja' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'je' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'ji' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'jo' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'la' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'las' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'le' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'les' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'lo' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'los' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'mas' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'me' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'mi' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'mil' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'mis' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'mí' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'mía' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'mías' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'mío' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'míos' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'nada' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'ni' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'no' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'nos' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'nra.' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'nro.' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'ocho' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'oh' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'ole' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'olé' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'once' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'ora' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'os' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'otra' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'otro' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'paf' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'para' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'pche' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'pchs' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'pero' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'pf' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'poca' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'poco' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'por' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'pude' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'pudo' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'pues' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'puf' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'pum' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'que' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'quia' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'qué' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'ro' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'se' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'sea' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'sean' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'seas' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'sed' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'seis' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'ser' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'será' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'seré' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'si' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'sido' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'sin' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'sino' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'so' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'sois' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'son' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'soy' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'su' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'sus' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'suya' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'suyo' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'sé' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'sí' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'ta' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'tal' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'tate' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'te' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'ti' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'toda' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'todo' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'tras' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'tres' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'tu' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'tus' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'tuya' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'tuyo' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'tú' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'uf' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'un' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'una' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'unas' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'uno' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'unos' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'upa' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'uy' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'vale' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'viva' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'vos' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'vra.' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'vro.' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'vía' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'y' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'ya' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'yo' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'zape' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'zas' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'zis' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'zuzo' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'él' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'ésa' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'ésas' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'ése' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'ésos' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'ésta' LANGUAGE 'Spanish';
ALTER FULLTEXT STOPLIST [StopList_Tuils_Search_And_Product] ADD 'éste' LANGUAGE 'Spanish';
go
/*****
	ACTUALIZA LA TABLA CON LA NUEVA STOPWORD LIST
******/
ALTER FULLTEXT INDEX ON [dbo].[Product] SET STOPLIST = [StopList_Tuils_Search_And_Product]
GO
ALTER FULLTEXT INDEX ON [dbo].[SearchTerm] SET STOPLIST = [StopList_Tuils_Search_And_Product]
GO
	