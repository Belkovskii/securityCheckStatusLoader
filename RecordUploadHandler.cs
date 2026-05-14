using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace SecurityCheckStatusLoader
{
    public static class RecordUploadHandler
    {

        
        //public static string ProceesRecord(
        //    CounterpartyFromFilesFolder record,
        //    Dictionary<Guid, XlsxRecord> xlsxRecordsMap,
        //    object apiLock,
        //    HttpClient httpClient, 
        //    string token
        //)
        //{
        //    lock (apiLock)
        //    {
        //        try
        //        {
        //            var contractorExternalId = record.ExternalId;
        //            Boolean doesCounterpartyExist = RequestCounterpartyByExternalId(contractorExternalId, httpClient, token);
        //            if (doesCounterpartyExist && record.SecurityRecords != null)
        //            {

        //                foreach (var securityRecordFromFile in record.SecurityRecords)
        //                {
        //                    Boolean doCreate = false;
        //                    SecurityRecord_Elma? securityRecord = requestSecurityRecordById(securityRecordFromFile.Id);
        //                    if (securityRecord == null)
        //                    {
        //                        securityRecord = new SecurityRecord_Elma();
        //                        doCreate = true;
        //                    }
        //                    var xlsxRecord = xlsxRecordsMap[securityRecordFromFile.Id];
        //                    fillSecurityRecordDataFromExcel(securityRecord, xlsxRecord);
        //                    var fileId = uploadFile(securityRecordFromFile);
        //                    securityRecord.FileId = fileId;
        //                    if (doCreate)
        //                    {
        //                        createRecord(securityRecord);
        //                    }
        //                    else
        //                    {
        //                        updateRecord(securityRecord);
        //                    }
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            return ex.Message;
        //        }
        //    }
        //}

        public static Boolean RequestCounterpartyByExternalId(Guid contractorExternalId, HttpClient httpClient, string token)
        {
            var contractorSearchEndpoint = "https://l42bom5pymlbs.elma365.ru/api/worker/query/_clients/_companies/search";            
            var method = HttpMethod.Put;
            var payload = $"{{\"filter\":{{\"and\":[{{\"and\":[{{\"eq\":[{{\"field\":\"ClientExternalGUID\"}},{{\"const\":\"{contractorExternalId}\"}}]}},{{\"eq\":[{{\"field\":\"__deletedAt\"}},null]}}]}}]}},\"offset\":0,\"limit\":1,\"order\":[]}}";
            var calloutManager = new CalloutManager(httpClient, contractorSearchEndpoint, payload, token, method);
            var result = calloutManager.SendRequest(out HttpStatusCode statusCode);
            Console.WriteLine(statusCode.ToString());
            Console.WriteLine(result.ToString());
            return statusCode == HttpStatusCode.OK;
        }
        
     }    
}
