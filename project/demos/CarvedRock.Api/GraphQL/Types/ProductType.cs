﻿using System.Collections.Generic;
using CarvedRock.Api.Data.Entities;
using CarvedRock.Api.Repositories;
using GraphQL.DataLoader;
using GraphQL.Types;

namespace CarvedRock.Api.GraphQL.Types
{
    public class ProductType: ObjectGraphType<Product>
    {
        public ProductType(ProductReviewRepository reviewRepository, IDataLoaderContextAccessor dataLoaderContextAccessor)
        {
            Field(t => t.Id);
            Field(t => t.Name).Description("The name of the product");
            Field(t => t.Description);
            Field(t => t.IntroducedAt).Description("When the product was first introduced in the catalog");
            Field(t => t.PhotoFileName).Description("The file name of the photo so the client can render it");
            Field(t => t.Price);
            Field(t => t.Rating).Description("The (max 5) star customer rating");
            Field(t => t.Stock);
            Field<ProductTypeEnumType>("Type", "The type of product");

            Field<ListGraphType<ProductReviewType>>("reviews",
                resolve: (productContext) =>
                {
                    //var reviews = reviewRepository.GetForProduct(productContext.Source.Id);

                    //return reviews;
                    var loader = dataLoaderContextAccessor.Context.GetOrAddCollectionBatchLoader<int, ProductReview>(
                        "GetReviewsByProductId", reviewRepository.GetLazyLookupForProducts);

                    return loader.LoadAsync(productContext.Source.Id);
                });

        }
    }
}
