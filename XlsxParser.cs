using ClosedXML.Excel;
using System.Text;

namespace SecurityCheckStatusLoader
{
    public class XlsxParser
    {
        private static DateTime? ParseDateTime(string cellValue)
        {
            if (DateTime.TryParse(cellValue, out DateTime dt))
            {
                return dt;
            }
            else if (double.TryParse(cellValue, out double excelDate))
            {
                return DateTime.FromOADate(excelDate);
            }
            else
            {
                return null;
            }
        } 

        public static Dictionary<Guid, XlsxRecord> Parse(string filePath)
        {
            var records = new Dictionary<Guid, XlsxRecord>();
            using (var workbook = new XLWorkbook(filePath))
            {
                var worksheet = workbook.Worksheet(1);                
                var rows = worksheet.RangeUsed().RowsUsed().Skip(1).Take(10);
                
                foreach (var row in rows)
                {
                    var record = new XlsxRecord
                    {
                        Guid = Guid.TryParse(row.Cell(1).Value.ToString(), out var g1) ? g1 : Guid.Empty,
                        Link = row.Cell(2).Value.ToString(),
                        CreatedAt = ParseDateTime(row.Cell(3).Value.ToString()),
                        Author = ParsePerson(row.Cell(4).Value.ToString()),
                        CreationTemplate = ParseTemplate(row.Cell(5).Value.ToString()),
                        CurrendVersionAuthor = ParsePerson(row.Cell(6).Value.ToString()),
                        CurrentVersionCreatedAt = ParseDateTime(row.Cell(7).Value.ToString()),
                        ContractorsGuid = Guid.TryParse(row.Cell(8).Value.ToString(), out var g2) ? g2 : Guid.Empty,
                        Correspondent = row.Cell(9).Value.ToString()
                    };
                    records[record.Guid] = record;
                }
            }
            return records;
        }

        public static XlsxRecordPerson ParsePerson(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName) || fullName == "(нет значения)")
                return null;

            var parts = fullName.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 1)
                return new XlsxRecordPerson { FirstName = parts[0], LastName = string.Empty };

            // Последнее слово — фамилия (UPPERCASE), остальные — имя
            return new XlsxRecordPerson
            {
                LastName = parts.Last(),
                FirstName = string.Join(" ", parts.Take(parts.Length - 1))
            };
        }

        public static string ParseTemplate(string value)
        {
            string trimmed = value.Trim();
            if (string.IsNullOrEmpty(trimmed) || trimmed == "(нет значения)")
                return null;

            return trimmed;
        }
    }


}
