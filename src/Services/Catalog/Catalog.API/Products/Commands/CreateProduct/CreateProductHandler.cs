namespace Catalog.API.Products.Commands.CreateProduct
{
    public record CreateProductCommand(
        string Name,
        List<string> Category,
        string Description,
        string ImageFile,
        decimal Price) : ICommand<CreateProductResult>;

    public record CreateProductResult(Guid Id);

    public class CreateProductCommanValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommanValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(c => c.Category).NotEmpty().WithMessage("Category is required");
            RuleFor(c => c.ImageFile).NotEmpty().WithMessage("ImageFile is required");
            RuleFor(c => c.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
        }
    }

    internal class CreateProductCommandHandler(IDocumentSession session, ILogger<CreateProductCommandHandler> logger) 
        : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            //Create Product entity from command object
            logger.LogInformation("CreateProductCommandHandler.Handler called with @{Command}", command);
            var product = new Product
            {
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price
            };

            //save result 
            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);

            // return CreateProductResult result
            return new CreateProductResult(product.Id);

        }
    }
}
