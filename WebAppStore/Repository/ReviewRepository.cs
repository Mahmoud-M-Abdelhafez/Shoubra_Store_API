using WebAppStore.Data;
using WebAppStore.Interfaces;
using WebAppStore.Models;
using WebAppStore.ViewModels;

namespace WebAppStore.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly StoreContextDB db;
        public ReviewRepository(StoreContextDB context)
        {
            db = context;
        }
        public List<Review> GetAll()
        {
            return (db.Reviews.ToList());
        }

        public Review GetById(int id)
        {
            return (db.Reviews.SingleOrDefault(r => r.Id == id));
        }
        public void Insert(AddReviewVM item)
        {
            int star = item.stars;

            var review = new Review
            {
                stars = star,
                Description = item.Description,
                UserId = item.UserId
            };

            db.Reviews.Add(review);
            db.SaveChanges();
        }
       
        public void Edit(int id, AddReviewVM item)
        {
            Review review = db.Reviews.SingleOrDefault(r => r.Id == id);
            
            int star = item.stars;
            review.stars = star;
            review.Description = item.Description;
            db.SaveChanges();
        }
        public void Delete(int id)
        {
            Review review = db.Reviews.SingleOrDefault(r => r.Id == id);
          
            db.Reviews.Remove(review);
            db.SaveChanges();
        }





    }
}
