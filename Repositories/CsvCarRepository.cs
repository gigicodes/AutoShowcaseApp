using AutoShowcaseApp.Models;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace AutoShowcaseApp.Repositories
{
    public class CsvCarRepository : ICarRepository
    {
        private readonly string _filePath;
        private static readonly object _lock = new();

        public CsvCarRepository(IWebHostEnvironment env)
        {
            _filePath = Path.Combine(env.ContentRootPath, "Data", "cars.csv");
        }

        public List<Car> GetAll()
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
                return csv.GetRecords<Car>().ToList();
            }
        }

        public List<Car> GetAvailable() => GetAll().Where(c => c.IsAvailable).ToList();

        public Car? GetById(int id) => GetAll().FirstOrDefault(c => c.Id == id);

        public void Add(Car car)
        {
            lock (_lock)
            {
                var cars = GetAll();
                car.Id = cars.Any() ? cars.Max(c => c.Id) + 1 : 1;
                car.DateAdded = DateTime.Now;
                cars.Add(car);
                WriteAll(cars);
            }
        }

        public void Update(Car car)
        {
            lock (_lock)
            {
                var cars = GetAll();
                var index = cars.FindIndex(c => c.Id == car.Id);
                if (index >= 0) cars[index] = car;
                WriteAll(cars);
            }
        }

        public void Delete(int id)
        {
            lock (_lock)
            {
                var cars = GetAll().Where(c => c.Id != id).ToList();
                WriteAll(cars);
            }
        }

        private void WriteAll(List<Car> cars)
        {
            using var writer = new StreamWriter(_filePath, false);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.WriteRecords(cars);
        }
    }
}
