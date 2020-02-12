using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarvedRock.Api.Data;
using CarvedRock.Api.Data.Entities;

namespace CarvedRock.Api.Repositories
{
    public class ProductReviewRepository
    {
        private readonly CarvedRockDbContext _dbContext;

        public ProductReviewRepository(CarvedRockDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<ProductReview> GetForProduct(int productId)
        {
            var reviews = _dbContext.ProductReviews.Where(r => r.ProductId == productId).ToList();

            return reviews;
        }

        public Task<ILookup<int, ProductReview>> GetLazyLookupForProducts(IEnumerable<int> productIds)
        {
            var reviews = _dbContext.ProductReviews.Where(r => productIds.Contains(r.ProductId)).ToList();

            return Task.FromResult(reviews.ToLookup(r => r.ProductId));
        }
    }
}
