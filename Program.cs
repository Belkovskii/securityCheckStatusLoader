
using SecurityCheckStatusLoader;
using System;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json.Nodes;
using System.Text;
using Newtonsoft.Json;

//LOGIN
//Console.WriteLine("Логин и авторизация в системе...");
bool isProd = false;
//string username = isProd ? "elmaadm@corphn.com" : "denis.belkovsky@masterdata.ru";
//string userPassword = isProd ? "HjvKphIV_O" : "Asz79!#58";
//string host = isProd ? "http://elmadev.neadru.local/" : "https://l42bom5pymlbs.elma365.ru/";
string host = "https://l42bom5pymlbs.elma365.ru/pub/v1";
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


string responseContent = await SCS_getById_usecase.SCS_getByIdAsync(new Guid("019da41d-62a7-780d-8dc1-4e0f4c3e0ae7"), client);
Console.WriteLine(responseContent);


//LoginClient loginClient = new(host, client);
//var token = loginClient.LoginAndGetToken(username, userPassword);
//if (token == null || token.Length < 5)
//{
//    Console.WriteLine("Не удалось получить токен");
//    Console.ReadKey();
//    throw new Exception("Did not get token");
//}

//AUTH
//AuthClient authClient = new(token, host);
//var authStatusCode = await authClient.Auth();
//if (authStatusCode.Trim().Equals("OK", StringComparison.CurrentCultureIgnoreCase))
//{
//    Console.WriteLine("Логин и авторизация прошли успешно");
//}
//else
//{
//    Console.WriteLine("Произошла ошибка аутентификации");
//}
//Console.WriteLine();

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