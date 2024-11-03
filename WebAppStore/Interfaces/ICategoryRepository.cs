using WebAppStore.DTO;
using WebAppStore.Models;
using WebAppStore.ViewModels;

namespace WebAppStore.Interfaces
{
    public interface ICategoryRepository
    {
        List<Category> GetAll();
        CategoryDetailsVM GetById(int id);
        void Insert(CategoryDTO item);
        void Edit(int id, CategoryDTO item);
        void Delete(int id);
    }
}
