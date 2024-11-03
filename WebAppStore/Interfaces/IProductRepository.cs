using WebAppStore.DTO;
using WebAppStore.Models;
using WebAppStore.ViewModels;

namespace WebAppStore.Interfaces
{
    public interface IProductRepository
    {
        ProductsWithImagesVM GetAll();
        ProductDetailsVM GetById(int id);
        ProductsWithImagesVM search(string txt);
        void Insert(AddProductDTO item);
        void Edit(int id, AddProductDTO item);
        void Delete(int id);

    }
}
