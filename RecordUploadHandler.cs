using SecurityCheckStatusLoader.ElmaUseCases;
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


        //async public static string ProceesRecord(
        //    CounterpartyFromFilesFolder record,
        //    Dictionary<Guid, XlsxRecord> xlsxRecordsMap,
        //    SemaphoreSlim apiLock,
        //    HttpClient httpClient,
        //    string token,
        //    string host
        //)
        //{
        //    await apiLock.WaitAsync();
        //    try
        //    {
        //        var contractorExternalId = record.ExternalId;
        //        var contractorResult = await Contractor_getByExternalId_usecase.Contractor_getByExternalId(
        //            contractorExternalId.ToString(),
        //            host,
        //            httpClient
        //        );
        //        Boolean doesCounterpartyExist = contractorResult.Contains("\"success\":true") &&
        //            contractorResult.Contains("__id");
        //        if (doesCounterpartyExist && record.SecurityRecords != null)
        //        {
        //            foreach (var securityRecordFromFile in record.SecurityRecords)
        //            {
        //                Boolean doCreate = false;
        //                string scsRecord = await SCS_getByExternalId_usecase.SCS_getByExternalId(
        //                    $"securityRecordFromFile.Id", host, httpClient);

        //                if (!scsRecord.Contains("\"success\":true") && !(scsRecord.Contains("__id")))
        //                {                                                       
        //                    doCreate = true;
        //                }
        //                var xlsxRecord = xlsxRecordsMap[securityRecordFromFile.Id];

        //                if (doCreate) .. 5 х 550 2750
        //                {

        //                }

        //                fillSecurityRecordDataFromExcel(securityRecord, xlsxRecord);
        //                var fileId = uploadFile(securityRecordFromFile);
        //                securityRecord.FileId = fileId;
        //                if (doCreate)
        //                {
        //                    createRecord(securityRecord);
        //                }
        //                else
        //                {
        //                    updateRecord(securityRecord);
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }
        //    finally
        //    {
        //        apiLock.Release();
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
