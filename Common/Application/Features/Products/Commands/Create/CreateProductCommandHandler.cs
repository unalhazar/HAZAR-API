﻿using Application.Abstraction;
using Application.Base;
using Application.Contracts.Persistence;
using Application.EventHandlers;
using Application.Features.Products.Commands.Create;
using Application.Features.Products.Responses;
using Application.Features.Products.Rules;
using System.Net;

public class CreateProductCommandHandler(
    IProductRepository productRepository,
    IMapper mapper,
    ProductRules productRules,
    IMediator mediator,
    ICacheService cacheService)
    : IRequestHandler<CreateProductCommand, ProcessResult<ProductResponse>>
{
    private readonly IProductRepository _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public async Task<ProcessResult<ProductResponse>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var checkResult = await productRules.CheckIfProductNameIsUniqueAsync(request.Name);
        if (!checkResult.Durum)
        {
            return new ProcessResult<ProductResponse>
            {
                Durum = checkResult.Durum,
                Mesaj = checkResult.Mesaj,
                HttpStatusCode = checkResult.HttpStatusCode
            };
        }

        try
        {
            var product = _mapper.Map<Product>(request);
            var addedProduct = await _productRepository.AddAsync(product);

            await cacheService.ClearCacheByPrefixAsync(CacheConstants.ProductCachePrefix);
            await mediator.Publish(new ProductCreatedEvent(product.Id, product.Name), cancellationToken);

            return new ProcessResult<ProductResponse>
            {
                Result = _mapper.Map<ProductResponse>(addedProduct),
                Durum = true,
                Mesaj = MessageConstants.SuccessMessage,
                HttpStatusCode = HttpStatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new ProcessResult<ProductResponse>
            {
                Durum = false,
                Mesaj = $"{MessageConstants.ErrorMessage} - {ex.Message}",
                HttpStatusCode = HttpStatusCode.InternalServerError
            };
        }
    }
}

public static class CacheConstants
{
    public const string ProductCachePrefix = "Hazarproducts__1";
}

public static class MessageConstants
{
    public const string SuccessMessage = "Product created successfully";
    public const string ErrorMessage = "An error occurred while creating the product";
}