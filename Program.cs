
using SecurityCheckStatusLoader;
using System;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json.Nodes;
using System.Text;
using Newtonsoft.Json;
using SecurityCheckStatusLoader.ElmaUseCases;
using System.Net.Mail;

//LOGIN
//Console.WriteLine("Логин и авторизация в системе...");
bool isProd = false;
string username = isProd ? "elmaadm@corphn.com" : "denis.belkovsky@masterdata.ru";
string userPassword = isProd ? "HjvKphIV_O" : "Asz79!#58";
string host = isProd ? "http://elmadev.neadru.local/" : "https://l42bom5pymlbs.elma365.ru/";
string bearerToken = "bcf83280-631c-4ce6-8bf2-70cd49d79faa";

using HttpClientHandler httpClientHandler = new()
{
    Proxy = null,
    UseProxy = false,
    AutomaticDecompression = System.Net.DecompressionMethods.GZip
};
using HttpClient client = new(httpClientHandler);
client.DefaultRequestHeaders.Accept.Add(
    new MediaTypeWithQualityHeaderValue("application/json")
);
client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
LoginClient loginClient = new(host, client);
var token = loginClient.LoginAndGetToken(username, userPassword);
if (token == null || token.Length < 5)
{
    Console.WriteLine("Не удалось получить токен");
    Console.ReadKey();
    throw new Exception("Did not get token");
}

//AUTH
AuthClient authClient = new(token, host);
var authStatusCode = await authClient.Auth();
if (authStatusCode.Trim().Equals("OK", StringComparison.CurrentCultureIgnoreCase))
{
    Console.WriteLine("Логин и авторизация прошли успешно");
}
else
{
    Console.WriteLine("Произошла ошибка аутентификации");
}
Console.WriteLine();

//PARSE EXCEL
//string CONTENT_DOCUMENT = "ФайлыСGUIDКонтрагентов.xlsx";
//string currentDirectory = Directory.GetCurrentDirectory();
//var excelFilePath = currentDirectory + $"/{CONTENT_DOCUMENT}";
//if (!File.Exists(excelFilePath))
//{
//    Console.WriteLine($"Файл {CONTENT_DOCUMENT} не был найден по пути {excelFilePath}, нажмите любую клавишу для выхода");
//    Console.ReadKey();
//    throw new FileNotFoundException($"Файл {CONTENT_DOCUMENT} не был найден");
//}
//Console.WriteLine($"Парсим {CONTENT_DOCUMENT}...");
//var xlsxRecords = XlsxParser.Parse(excelFilePath);
//Console.WriteLine($"Данные из xlsx-файла собраны, количество записей: {xlsxRecords.Count}");




var result = await Contractor_getByExternalId_usecase.Contractor_getByExternalId(
    "623063ea-71f7-11f0-a359-005056ae7f7f", host, client
);
Console.WriteLine(result);

//var filePath = currentDirectory + $"/Тестовые файлы проверок/{fileName}";
//var fileName = "testFile.txt";
//string baseDirectory = AppContext.BaseDirectory;
//string filePath = Path.Combine(baseDirectory, "Тестовые файлы проверок", "testFile.txt");
//Console.WriteLine($"filePath: {filePath}");
//byte[] fileBytes = File.ReadAllBytes(filePath);
//FileUploadManager fileUploadManager = new(client, token, host);
//string extension = Path.GetExtension(fileName)?.TrimStart('.') ?? "";
//var (fileRecord, fileUploadError) = await fileUploadManager.UploadFile(fileBytes, fileName);
//if (fileRecord != null && (fileUploadError == null || String.IsNullOrEmpty(fileUploadError)))
//{
//    //Console.WriteLine($"fileRecord.__id: {fileRecord");
//    await SCS_create_usecase.Create(
//            client,
//            host,
//            "019d1dd7-76ca-727c-acf2-7fac5cf113b4" /*H&N test*/,
//            "019cbf55-13bc-7238-af3d-9e1d1251deb3" /*низкий уровень риска*/,
//            fileRecord,
//            new DateTime(2026, 3, 8)
//     );
//} 
//else
//{
//    Console.WriteLine(fileUploadError);
//    Console.WriteLine("file not loaded");
//}





/*
 CORRECT:

{
  "context": {
    "__directory": "00000000-0000-0000-0000-000000000000",
    "__externalId": "example",
    "CRMClienId": [
      "019e1d6e-2e71-706d-80e5-fe9ff0b1e85b"
    ],
    "StatusId": [
      "019cbf55-2d97-75fa-8f90-5a2654fd1d13"
    ],
    "CheckAttachment": ["e4d7b3a9-3045-4b26-b5a1-52a6297c09c2"],
    "CheckDate": "2026-05-14T13:01:54.088Z"
  },
  "statusGroupId": "a35cd8de-0c73-4f6a-8218-90193d02e2e0",
  "withEventForceCreate": true
}
 
 
 
 */


/*
//PROCEED FILES IN PARALLEL
var filesParentFolder = isProd ? "" : "Тестовые файлы проверок";
var filesPath = currentDirectory + $"/{filesParentFolder}";
var counterpartyFolders = Directory.GetDirectories(filesPath);
var apiLock = new object();
// Параллельная обработка контрагентов
Parallel.ForEach(counterpartyFolders, counterpartyFolder =>
{
    if (!Guid.TryParse(Path.GetFileName(counterpartyFolder), out Guid externalId))
    {
        Console.WriteLine($"Пропуск папки с некорректным именем: {counterpartyFolder}");
        return;
    }
    var counterparty = new CounterpartyFromFilesFolder{ ExternalId = externalId };
    var securityRecordFolders = Directory.GetDirectories(counterpartyFolder);
    var records = new ConcurrentBag<SecurityRecordFromFilesFolder>();
    Parallel.ForEach(securityRecordFolders, recordFolder =>
    {
        if (!Guid.TryParse(Path.GetFileName(recordFolder), out Guid recordId))
        {
            Console.WriteLine($"Пропуск папки записи с некорректным именем: {recordFolder}");
            return;
        }
        var files = Directory.GetFiles(recordFolder);
        if (files.Length == 0)
        {
            Console.WriteLine($"В папке {recordFolder} нет файлов.");
            return;
        }
        string filePath = files[0];
        byte[] fileBytes = File.ReadAllBytes(filePath);
        var record = new SecurityRecordFromFilesFolder{ Id = recordId, FileItem = fileBytes};
        records.Add(record);

        // Загрузка файла в БД через API (эмулируем)
        //UploadFileToDatabase(record);
    });
});
*/

//var createPayload = new SecurityCheckStatusCreatePayload
//{
//    Payload = new CreatePayload(),
//    TempData = new TempData()
//};
//createPayload.Payload.CheckDate = new DateTime(2026, 8, 19, 0, 0, 0, DateTimeKind.Utc);
//createPayload.Payload.Name = "Test from loader app";
//createPayload.TempData.WithEventForceCreate = false;
//createPayload.TempData.AssignExistingIndex = false;
//createPayload.Payload.CRMClienId = [Guid.Parse("019e1d6e-2e71-706c-9c66-060a956ba4f1")];
//createPayload.Payload.StatusId = [Guid.Parse("019cbf55-6218-7199-a489-629a01644119")];

//string jsonStringResult;

////try
//{
//    jsonStringResult = PayloadSerializer.SerializeToJson(createPayload);
//    Console.WriteLine(jsonStringResult);
//    Console.WriteLine("\n--- Сериализация завершена успешно ---");


//    var createEndpoint = "https://l42bom5pymlbs.elma365.ru/api/apps/_clients/SecurityCheckStatus/items";
//    var method = HttpMethod.Post;
//    var calloutManager = new CalloutManager(client, createEndpoint, jsonStringResult, token, method);
//    var result = calloutManager.SendRequest(out HttpStatusCode statusCode);
//    Console.WriteLine($"creation statusCode: {statusCode}");
//}
//catch (Exception ex)
//{
//    Console.WriteLine($"Произошла ошибка при сериализации: {ex.Message}");
//    return;
//}