using WebAppStore.Models;
using WebAppStore.ViewModels;

namespace WebAppStore.Interfaces
{
    public interface ICategoryRepository
    {
        List<Category> GetAll();
        CategoryDetailsVM GetById(int id);
        void Insert(AddCategoryVM item);
        void Edit(int id, Category item);
        void Delete(int id);
    }
}
