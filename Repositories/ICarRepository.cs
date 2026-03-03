using AutoShowcaseApp.Models;

namespace AutoShowcaseApp.Repositories
{
    public interface ICarRepository
    {
        List<Car> GetAll();
        List<Car> GetAvailable();
        Car? GetById(int id);
        void Add(Car car);
        void Update(Car car);
        void Delete(int id);
    }
}
