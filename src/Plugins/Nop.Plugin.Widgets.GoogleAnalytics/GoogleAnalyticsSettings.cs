
using Nop.Core.Configuration;

namespace Nop.Plugin.Widgets.GoogleAnalytics
{
    public class GoogleAnalyticsSettings : ISettings
    {
        public string GoogleId { get; set; }
        public string TrackingScript { get; set; }
        public string EcommerceScript { get; set; }
        public string EcommerceDetailScript { get; set; }

        /////// <summary>
        /////// Llave de la categoría para el registro de usuarios
        /////// </summary>
        ////public string GAQ_Category_Register { get; set; }
        /////// <summary>
        /////// Llave de la accion para los registros que se dan cuando una persona compra un producto
        /////// </summary>
        ////public string GAQ_Action_RegisterFromBuyProduct { get; set; }
        /////// <summary>
        /////// Llave de la accion para los registros que se dan cuando una persona realiza una pregunta
        /////// </summary>
        ////public string GAQ_Action_RegisterFromAskQuestion { get; set; }
        /////// <summary>
        /////// Llave de la accion para los registros que se dan cuando una persona publica un producto
        /////// </summary>
        ////public string GAQ_Action_RegisterFromPublish { get; set; }
        /////// <summary>
        /////// Llave de la accion para los registros que se dan cuando una persona presiona el menu principal
        /////// </summary>
        ////public string GAQ_Action_RegisterFromMainMenu { get; set; }
    }
}