using Microsoft.AspNetCore.Components.Forms;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Globalization;
using System.IO;
using System.Text;
using TestTask2.Server.Models;
using TestTask2.Server.Services.Interfaces;

namespace TestTask2.Server.Services.Implementations
{
    public class ParseService : IParseService
    {
        //парсинг данных о тиблице
        private void ParseReportHeader(ISheet sheet, ReportFile report)
        {
            var row = sheet.GetRow(0);
            report.BankName = row.GetCell(0).ToString() ?? string.Empty;
            string description = string.Empty;
            for (int i = 1; i < 4; i++)
            {
                description += sheet.GetRow(i).GetCell(0).ToString() + "\n";
            }
            report.ReportDescription = description.Trim();
            sheet.GetRow(4);
            row = sheet.GetRow(5);
            report.CreationDate = row.GetCell(0).ToString() ?? string.Empty;
            report.Currency = row.GetCell(6).ToString() ?? string.Empty;
        }

        //парсинг данных для счёта
        private Account ParseAccount(ISheet sheet, int number, IRow row, bool isClassResult = false, bool isGroupResult = false, bool isRecordResult = false)
        {
            var account = new Account()
            {
                IsClassResult = isClassResult,
                IsGroupResult = isGroupResult,
                IsRecordResult = isRecordResult,
                ActiveInBalance = Convert.ToDecimal(row.GetCell(1)?.NumericCellValue),
                PassiveInBalance = Convert.ToDecimal(row.GetCell(2)?.NumericCellValue),
                Debit = Convert.ToDecimal(row.GetCell(3)?.NumericCellValue),
                Credit = Convert.ToDecimal(row.GetCell(4)?.NumericCellValue),
                ActiveOutBalance   = Convert.ToDecimal(row.GetCell(5)?.NumericCellValue),
                PassiveOutBalance = Convert.ToDecimal(row.GetCell(6)?.NumericCellValue),
            };

            if (!isClassResult && !isRecordResult)
            {
                account.Number = number;
            }
            return account;
        }

        //парсинг названия и номера класса
        private Class ParseClass(string str)
        {
            var elements = str.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (elements.Length < 3 || !int.TryParse(elements[1], out var number))
            {
                throw new ArgumentException();
            }
            var name = new StringBuilder();
            for (int i = 2; i < elements.Length; i++)
            {
                name.Append(elements[i]);
                name.Append(' ');
            }
            return new Class
            {
                Number = number,
                Name = name.ToString().Trim(),
            };
        }

        public ReportFile ParseFile(IFormFile reportFile)
        {
            var accounts = new List<Account>();
            var classes = new List<Class>();
            var report = new ReportFile();
            using (var file = reportFile.OpenReadStream())
            {
                IWorkbook workbook = WorkbookFactory.Create(file);
                var sheet = workbook.GetSheetAt(0);
                ParseReportHeader(sheet, report);
                Class? currentClass = null;
                for (int row = 8; row <= sheet.LastRowNum; row++)
                {
                    var currentRow = sheet.GetRow(row);

                    if (currentRow != null)
                    {
                        var cell = currentRow.GetCell(0).ToString() ?? string.Empty;
                        //если начало класса
                        if (cell.StartsWith("КЛАСС"))
                        {
                            currentClass = ParseClass(cell);
                        }
                        //если итоговый баланс по классу
                        else if (cell.StartsWith("ПО КЛАССУ"))
                        {
                            var account = ParseAccount(sheet, 0, currentRow, isClassResult: true);
                            accounts.Add(account);
                            if (currentClass == null)
                            {
                                throw new ArgumentException();
                                
                            }
                            currentClass.Accounts.AddRange(accounts);
                            classes.Add(currentClass);
                            currentClass = null;
                            accounts.Clear();
                        }
                        //если итоговый баланс по всему файлу
                        else if (cell.StartsWith("БАЛАНС"))
                        {
                            report.ReportAccount = ParseAccount(sheet, 0, currentRow, isRecordResult: true); 
                        }
                        //если номер счета
                        else if (int.TryParse(cell, out var number))
                        {
                            Account? account = null;
                            if (currentRow.GetCell(0).CellStyle.GetFont(workbook).IsBold)
                            {
                                account = ParseAccount(sheet, number, currentRow, isGroupResult: true);
                            }
                            else
                            {
                                account = ParseAccount(sheet, number, currentRow);
                            }
                            accounts.Add(account);
                            
                        }
                        
                    }
                }
            }
            report.Classes.AddRange(classes);
            report.FileName = reportFile.FileName;
            return report;
        }
    }
}
