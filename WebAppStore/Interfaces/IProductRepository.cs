using WebAppStore.Models;
using WebAppStore.ViewModels;

namespace WebAppStore.Interfaces
{
    public interface IProductRepository
    {
        ProductsWithImagesVM GetAll();
        ProductDetailsVM GetById(int id);
        ProductsWithImagesVM search(string txt);
        void Insert(AddProductVM item);
        void Edit(int id, AddProductVM item);
        void Delete(int id);

    }
}
