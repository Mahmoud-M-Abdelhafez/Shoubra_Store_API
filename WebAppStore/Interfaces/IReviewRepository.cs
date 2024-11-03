using WebAppStore.Models;
using WebAppStore.ViewModels;

namespace WebAppStore.Interfaces
{
    public interface IReviewRepository
    {
        List<Review> GetAll();
        Review GetById(int id);
        void Insert(AddReviewVM item);
        void Edit(int id, AddReviewVM item);
        void Delete(int id);
    }
}
