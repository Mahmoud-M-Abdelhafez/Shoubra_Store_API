using WebAppStore.DTO;
using WebAppStore.Models;
using WebAppStore.ViewModels;

namespace WebAppStore.Interfaces
{
    public interface ICartRepository
    {
        CartProductsVM GetAll(string id);
        CartDetailsVM GetById(int id);
        void Insert(AddCartDTO item);
        void Edit(int id, AddCartVM item);
        string Delete(int id);
    }
}
