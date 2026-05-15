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


        async public static void ProceesRecord(
            Guid contractorExternalId,
            Guid scsExternalId,
            byte[] fileBody,
            string fileName,
            Dictionary<Guid, XlsxRecord> xlsxRecordsMap,
            SemaphoreSlim apiLock,
            HttpClient httpClient,
            string token,
            string host
        )
        {
            await apiLock.WaitAsync();
            try
            {
                XlsxRecord xlsxRecord = xlsxRecordsMap[scsExternalId];
                string scsRecord = await SCS_getByExternalId_usecase.SCS_getByExternalId(
                    scsExternalId.ToString(), host, httpClient
                );                
                bool doNeedToCreate = scsRecord.Contains("\"success\":true") && scsRecord.Contains("__id");
                FileUploadManager fileUploadManager = new(httpClient, token, host);                
                var (fileRecord, fileUploadError) = await fileUploadManager.UploadFile(fileBody, fileName);
                if (fileRecord != null)
                {
                    FileAttachment fileAttachment = new(fileRecord);
                    ScsUpdateCreateData data = new()
                    {
                        CheckAttachment = fileRecord,
                        SecurityCheckGUID = xlsxRecord.Guid.ToString()                                              
                    };
                    if (xlsxRecord.CreatedAt != null) data.CheckDate = xlsxRecord.CreatedAt.Value;

                    if (doNeedToCreate)
                    {

                    }
                    else
                    {
                       
                    }
                }
                else
                {
                    //File upload error;
                }
                                
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                apiLock.Release();
            }
        }

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
