using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Nop.Plugin.Misc.MailChimp.Data;
using Nop.Services.Logging;
using PerceptiveMCAPI;
using PerceptiveMCAPI.Methods;
using PerceptiveMCAPI.Types;
using MailChimp;
using MailChimp.Helper;
using MailChimp.Lists;

namespace Nop.Plugin.Misc.MailChimp.Services
{
    public class MailChimpApiService : IMailChimpApiService
    {
        private readonly MailChimpSettings _mailChimpSettings;
        private readonly ISubscriptionEventQueueingService _subscriptionEventQueueingService;
        private readonly ILogger _log;

        public MailChimpApiService(MailChimpSettings mailChimpSettings, ISubscriptionEventQueueingService subscriptionEventQueueingService, ILogger log)
        {
            _mailChimpSettings = mailChimpSettings;
            _subscriptionEventQueueingService = subscriptionEventQueueingService;
            _log = log;
        }

        /// <summary>
        /// Retrieves the lists.
        /// </summary>
        /// <returns></returns>
        public virtual NameValueCollection RetrieveLists()
        {
            var output = new NameValueCollection();
            try
            {

                // input parameters
                //var listInput = new listsInput(_mailChimpSettings.ApiKey);
                var mc = new MailChimpManager(_mailChimpSettings.ApiKey);
                var listOutput = mc.GetLists();

                if (listOutput != null && listOutput.Total  > 0)
                {
                    foreach (var item in listOutput.Data)
                    {
                        output.Add(item.Name, item.Id);
                    }
                }
            }
            catch (Exception e)
            {
                _log.Debug(e.Message, e);
            }
            return output;
        }

        /// <summary>
        /// Batches the unsubscribe.
        /// </summary>
        /// <param name="recordList">The records</param>
        public virtual listBatchUnsubscribeOutput BatchUnsubscribe(IEnumerable<MailChimpEventQueueRecord> recordList)
        {
            if (String.IsNullOrEmpty(_mailChimpSettings.DefaultListId)) 
                throw new ArgumentException("MailChimp list is not specified");

            var input = new listBatchUnsubscribeInput();
            //any directive overrides
            input.api_Validate = true;
            input.api_AccessType = EnumValues.AccessType.Serial;
            input.api_OutputType = EnumValues.OutputType.JSON;
            //method parameters
            input.parms.apikey = _mailChimpSettings.ApiKey;
            input.parms.id = _mailChimpSettings.DefaultListId;

            //batch the emails
            var batch = recordList.Select(sub => sub.Email).ToList();

            input.parms.emails = batch;

            //execution
            var cmd = new listBatchUnsubscribe(input);

            listBatchUnsubscribeOutput listSubscribeOutput = cmd.Execute();
            return listSubscribeOutput;
        }

        /// <summary>
        /// Batches the subscribe.
        /// </summary>
        /// <param name="recordList">The records</param>
        public virtual BatchSubscribeResult BatchSubscribe(IEnumerable<MailChimpEventQueueRecord> recordList)
        {
            if (String.IsNullOrEmpty(_mailChimpSettings.DefaultListId)) 
                throw new ArgumentException("MailChimp list is not specified");
            
            //var input = new listBatchSubscribeInput();
            ////any directive overrides
            //input.api_Validate = true;
            //input.api_AccessType = EnumValues.AccessType.Serial;
            //input.api_OutputType = EnumValues.OutputType.JSON;
            ////method parameters
            //input.parms.apikey = _mailChimpSettings.ApiKey;
            //input.parms.id = _mailChimpSettings.DefaultListId;
            //input.parms.double_optin = false;
            //input.parms.replace_interests = true;
            //input.parms.update_existing = true;
            //var batch = new List<Dictionary<string, object>>();


            var resultTotal = new BatchSubscribeResult() { Errors = new List<ListError>() };
            

           // input.parms.batch = batch;

            //execution
            //var cmd = new listBatchSubscribe(input);

            var mc = new MailChimpManager(_mailChimpSettings.ApiKey);

            foreach (Nop.Core.Domain.Messages.NewsLetterSuscriptionType type in Enum.GetValues(typeof(Nop.Core.Domain.Messages.NewsLetterSuscriptionType)))
            {
                string listId = _mailChimpSettings.DefaultListId;
                ///Dependiendo del tipo toma una de las listas
                switch (type)
                {
                    case Nop.Core.Domain.Messages.NewsLetterSuscriptionType.General:
                        listId = _mailChimpSettings.GeneralSuscriptionListId;
                        break;
                    case Nop.Core.Domain.Messages.NewsLetterSuscriptionType.User:
                        listId = _mailChimpSettings.UserSuscriptionListId;
                        break;
                    case Nop.Core.Domain.Messages.NewsLetterSuscriptionType.Shop:
                        listId = _mailChimpSettings.ShopSuscriptionListId;
                        break;
                    case Nop.Core.Domain.Messages.NewsLetterSuscriptionType.RepairShop:
                        listId = _mailChimpSettings.RepairShopSuscriptionListId;
                        break;
                    default:
                        break;
                }

                //Filtra los registros pendientes por tipo de suscripción
                var filteredRecordList = GetAddedEmailsToList(recordList.Where(r => r.SuscriptionTypeId == Convert.ToInt32(type)));

                if (filteredRecordList.Count > 0)
                {
                    //Realiza la carga por batch por tipo
                    var result = mc.BatchSubscribe(listId, filteredRecordList, doubleOptIn: _mailChimpSettings.DoubleOptin);
                    resultTotal.ErrorCount += result.ErrorCount;
                    resultTotal.UpdateCount += result.UpdateCount;
                    resultTotal.AddCount += result.AddCount;
                    resultTotal.Errors.AddRange(result.Errors);
                }

            }
            
            return resultTotal;
        }

        /// <summary>
        /// Agrega una lista de emails para agregar a mailchimp en el formato para el llamado en batch
        /// </summary>
        /// <param name="recordList">lista</param>
        /// <returns></returns>
        private List<BatchEmailParameter> GetAddedEmailsToList(IEnumerable<MailChimpEventQueueRecord> recordList)
        {
            var listEmails = new List<BatchEmailParameter>();

            foreach (var sub in recordList)
            {

                //var entry = new Dictionary<string, object>();
                //entry.Add("EMAIL", sub.Email);
                //batch.Add(entry);
                var mergeVars = new MergeVar();
                if (sub.AdditionalInfo != null)
                {
                    //Separa la información adicional
                    var additionalInfoValues = sub.AdditionalInfo.Split(new char[] { '|' });
                    //La primera posición es la del nombre
                    mergeVars.Add("FNAME", additionalInfoValues[0]);
                }

                listEmails.Add(new BatchEmailParameter()
                {
                    Email = new EmailParameter() { Email = sub.Email },
                    MergeVars = mergeVars
                });
            }

            return listEmails;
        }
        
        public virtual SyncResult Synchronize()
        {
            var result = new SyncResult();

            // Get all the queued records for subscription/unsubscription
            var allRecords = _subscriptionEventQueueingService.GetAll();
            //get unique and latest records
            var allRecordsUnique = new List<MailChimpEventQueueRecord>();
            foreach (var item in allRecords
                .OrderByDescending(x => x.CreatedOnUtc))
            {
                var exists = allRecordsUnique
                    .Where(x => x.Email.Equals(item.Email, StringComparison.InvariantCultureIgnoreCase)
                    && x.SuscriptionTypeId == item.SuscriptionTypeId)
                    .FirstOrDefault() != null;
                if (!exists)
                    allRecordsUnique.Add(item);
            }
            var subscribeRecords = allRecordsUnique.Where(x => x.IsSubscribe).ToList();
            var unsubscribeRecords = allRecordsUnique.Where(x => !x.IsSubscribe).ToList();
            
            //subscribe
            if (subscribeRecords.Count > 0)
            {
                var subscribeResult = BatchSubscribe(subscribeRecords);
                //result
                if (subscribeResult.ErrorCount > 0)
                {
                    foreach (var error in subscribeResult.Errors)
                        result.SubscribeErrors.Add(error.ErrorMessage);
                }
                else
                {
                    result.SubscribeResult = subscribeResult.ToString();
                }
            }
            else
            {
                result.SubscribeResult = "No records to add";
            }
            //unsubscribe
            if (unsubscribeRecords.Count > 0)
            {
                var unsubscribeResult = BatchUnsubscribe(unsubscribeRecords);
                //result
                if (unsubscribeResult.api_ErrorMessages.Count > 0)
                {
                    foreach (var error in unsubscribeResult.api_ErrorMessages)
                        result.UnsubscribeErrors.Add(error.error);
                }
                else
                {
                    result.UnsubscribeResult = unsubscribeResult.ToString();
                }
            }
            else
            {
                result.UnsubscribeResult = "No records to unsubscribe";
            }

            //delete the queued records
            foreach (var sub in allRecords)
                _subscriptionEventQueueingService.Delete(sub);

            //other useful properties of listBatchSubscribeOutput and listBatchUnsubscribeOutput
            //output.result.add_count
            //output.result.error_count
            //output.result.update_count
            //output.result.errors
            //output.api_Request, output.api_Response, // raw data
            //output.api_ErrorMessages, output.api_ValidatorMessages); // & errors
            return result;
        }
    }
}