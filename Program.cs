
using SecurityCheckStatusLoader;
using SecurityCheckStatusLoader.ElmaUseCases;
using System.Collections.Concurrent;
using System.Net.Http.Headers;

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
string CONTENT_DOCUMENT = "ФайлыСGUIDКонтрагентов.xlsx";
string currentDirectory = Directory.GetCurrentDirectory();
var excelFilePath = currentDirectory + $"/{CONTENT_DOCUMENT}";
if (!File.Exists(excelFilePath))
{
    Console.WriteLine($"Файл {CONTENT_DOCUMENT} не был найден по пути {excelFilePath}, нажмите любую клавишу для выхода");
    Console.ReadKey();
    throw new FileNotFoundException($"Файл {CONTENT_DOCUMENT} не был найден");
}
Console.WriteLine($"Парсим {CONTENT_DOCUMENT}...");
var xlsxRecords = XlsxParser.Parse(excelFilePath);
Console.WriteLine($"Данные из xlsx-файла собраны, количество записей: {xlsxRecords.Count}");

//ScsUpdateCreateData data = new();
//data.RecordId = "019e2813-0f04-7cef-8bcd-26eccd1d3d5c";
//data.SecurotyUserId = "a9f37bb1-8f2a-4ecf-97b1-8679c8e0c8b8";
//data.CheckDate = new DateTime(2027, 1, 1);
//var response = await SCS_update_usecase.Update(client, host, data);
//Console.WriteLine(response.ToString());



//var result = await Contractor_getByExternalId_usecase.Contractor_getByExternalId(
//    "623063ea-71f7-11f0-a359-005056ae7f7f", host, client
//);
//Console.WriteLine(result.Contains("\"success\":true") && result.Contains("__id"));

//var user = User_getByName_usecase.User_getByName("GARAEVA", "Asiya", host, client, token);
//Console.WriteLine(user);



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



//PROCEED FILES IN PARALLEL
var filesParentFolder = "Files";
var filesPath = currentDirectory + $"/{filesParentFolder}";
var contractorFolders = Directory.GetDirectories(filesPath);
// Параллельная обработка контрагентов
Parallel.ForEach(contractorFolders, async contractorFolder =>
{
    if (!Guid.TryParse(Path.GetFileName(contractorFolder), out Guid externalId))
    {
        Console.WriteLine($"Пропуск папки с некорректным именем: {contractorFolder}");
        return;
    }
    string contractorId = externalId.ToString();

    //Проверяем, есть ли контрагент в системе
    bool isContractorInSystem = false;
    SemaphoreSlim semaphoreSlim = new(1, 1);
    await semaphoreSlim.WaitAsync();
    try
    {
        string response = await Contractor_getByExternalId_usecase.Contractor_getByExternalId(contractorId, host, client);
        isContractorInSystem = response.Contains("\"success\":true") && response.Contains("__id");
    }
    catch
    {
        Console.WriteLine("Error fetching contractor");
    }
    finally
    {
        semaphoreSlim.Release();
    }
    if (!isContractorInSystem)
    {
        return;
    }
    string[] subfoldersNames = Directory.GetDirectories(contractorFolder);


    Parallel.ForEach(subfoldersNames, async subfolderName =>
    {
        if (!Guid.TryParse(Path.GetFileName(subfolderName), out Guid recordId))
        {
            Console.WriteLine($"Пропуск папки записи с некорректным именем: {subfolderName}");
            return;
        }
        var files = Directory.GetFiles(subfolderName);
        if (files.Length == 0)
        {
            Console.WriteLine($"В папке {subfolderName} нет файлов.");
            return;
        }
        string filePath = files[0];
        byte[] fileBytes = File.ReadAllBytes(filePath);
        //RecordUploadHandler.
    });
});
    

        
