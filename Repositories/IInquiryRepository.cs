using AutoShowcaseApp.Models;

namespace AutoShowcaseApp.Repositories
{
    public interface IInquiryRepository
    {
        List<Inquiry> GetAll();
        Inquiry? GetById(int id);
        void Add(Inquiry inquiry);
    }
}
