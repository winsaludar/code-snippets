using EFCoreTransactions.Models;

namespace EFCoreTransactions.Services
{
    public interface IProductService
    {
        public Task<IEnumerable<Product>> GetAll();
        public Task<Product?> Get(int id);
        public Task<(bool isSuccessful, string message)> Add(Product product);
    }
}
