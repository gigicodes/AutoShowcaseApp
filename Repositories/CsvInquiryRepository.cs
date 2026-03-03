using AutoShowcaseApp.Models;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace AutoShowcaseApp.Repositories
{
    public class CsvInquiryRepository : IInquiryRepository
    {
        private readonly string _filePath;
        private static readonly object _lock = new();

        public CsvInquiryRepository(IWebHostEnvironment env)
        {
            _filePath = Path.Combine(env.ContentRootPath, "Data", "inquiries.csv");
        }

        public List<Inquiry> GetAll()
        {
            lock (_lock)
            {
                if (!File.Exists(_filePath)) return new();
                using var reader = new StreamReader(_filePath);
                using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true,
                    MissingFieldFound = null
                });
                return csv.GetRecords<Inquiry>().ToList();
            }
        }

        public Inquiry? GetById(int id) => GetAll().FirstOrDefault(i => i.Id == id);

        public void Add(Inquiry inquiry)
        {
            lock (_lock)
            {
                var inquiries = GetAll();
                inquiry.Id = inquiries.Any() ? inquiries.Max(i => i.Id) + 1 : 1;
                inquiry.DateSubmitted = DateTime.Now;
                inquiries.Add(inquiry);
                using var writer = new StreamWriter(_filePath, false);
                using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
                csv.WriteRecords(inquiries);
            }
        }
    }
}
