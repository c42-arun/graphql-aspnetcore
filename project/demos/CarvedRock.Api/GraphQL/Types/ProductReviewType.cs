﻿using GraphQL.Types;
using CarvedRock.Api.Data.Entities;

namespace CarvedRock.Api.GraphQL.Types
{
    public class ProductReviewType : ObjectGraphType<ProductReview>
    {
        public ProductReviewType()
        {
            Field(t => t.Id);
            Field(t => t.Title);
            Field(t => t.Review);
            Field(t => t.ProductId).Description("ID of the associated product");
        }
    }
}
