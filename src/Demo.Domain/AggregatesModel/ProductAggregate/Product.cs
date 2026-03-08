using Demo.SharedKernel.Exceptions;
using Demo.SharedKernel.SeedWork;

namespace Demo.Domain.AggregatesModel.ProductAggregate;

public class Product : AuditableEntity, IAggregateRoot
{
    public string Name { get; private set; } = null!;
    public string? Description { get; private set; }
    public Money Price { get; private set; } = null!;
    public ProductStatus Status { get; private set; } = ProductStatus.Draft;

    private Product()
    {
    }

    public Product(string name, Money price, string? description = null)
    {
        Name = name;
        Description = description;
        Price = price;
    }

    public void Activate()
    {
        if (Status == ProductStatus.Discontinued)
        {
            throw new DomainException(ProductErrors.ActivationNotAllowedForDiscontinued(Id));
        }

        if (Status == ProductStatus.Active)
        {
            return;
        }

        Status = ProductStatus.Active;
    }
}
