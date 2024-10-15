using Microsoft.EntityFrameworkCore;
using System.Linq;
using TestTask2.Server.Data;
using TestTask2.Server.Data.Dto;
using TestTask2.Server.Models;
using TestTask2.Server.Services.Interfaces;

namespace TestTask2.Server.Services.Implementations
{
    public class ReportService : IReportService
    {
        private readonly AppDbContext _dbContext;
        private readonly IParseService _parseService;
        public ReportService(AppDbContext dbContext, IParseService parseService)
        {
            _dbContext = dbContext;
            _parseService = parseService;
        }

        //получение данных отчета
        public async Task<ReportDataResponseDTO?> GetReportDataAsync(int reportId)
        {
            var reportFile =  await _dbContext.ReportFiles
                .Include(r => r.Classes)
                .ThenInclude(c => c.Accounts)
                .FirstOrDefaultAsync(rf => rf.Id == reportId);
            var account = await _dbContext.Accounts
                .FirstOrDefaultAsync(a => a.Id == reportFile.AccountId);
            if (reportFile != null)
            {
                var reportDataDTO = new ReportDataResponseDTO
                {
                    BankName = reportFile.BankName,
                    ReportDescription = reportFile.ReportDescription,
                    Currency = reportFile.Currency,
                    CreationDate = reportFile.CreationDate,
                    Account = account,
                    Classes = reportFile.Classes.Select(cl => new ClassResponseDTO
                    {
                        Name = cl.Name,
                        Number = cl.Number,
                        Accounts = cl.Accounts.Select(acc => new AccountResponseDTO
                        {
                            Number = acc.Number.ToString(),
                            ActiveInBalance = acc.ActiveInBalance,
                            PassiveInBalance = acc.PassiveInBalance,
                            IsRecordResult = acc.IsRecordResult,
                            IsGroupResult = acc.IsGroupResult,
                            IsClassResult = acc.IsClassResult,
                            Debit = acc.Debit,
                            Credit = acc.Credit,
                            ActiveOutBalance = acc.ActiveOutBalance,
                            PassiveOutBalance = acc.PassiveOutBalance
                        }).ToList()
                    }).ToList()
                };
                return reportDataDTO;
            }
            return null;
        }

        //получение названий всех отчетов
        public async Task<List<ReportItemResponseDTO>> GetReportsAsync()
        {
            var reports = await _dbContext.ReportFiles
                .Select(r => new ReportItemResponseDTO
                {
                    Id = r.Id,
                    Name = r.FileName
                }).ToListAsync();
            return reports;
        }

        //загрузка отчета
        public async Task<bool> UploadReport(IFormFile inputfile)
        {
            var report = _parseService.ParseFile(inputfile);
            await _dbContext.ReportFiles.AddAsync(report);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
