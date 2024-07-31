﻿using Domain.Response.Product;

namespace Application.Features.Products.Queries.GetById
{
    public class GetByIdProductQuery : IRequest<ProcessResult<ProductResponse>>
    {
        public long Id { get; set; }
    }
}
