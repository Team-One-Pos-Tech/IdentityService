# About
A catalog of RabbitMq events that makes work the SnackHub business rules workflow through synchronous communication of  all microservices

### Stack
- Rabbit Mq
- Mass Transit 8.3.0
- .NET 8 or Later

## Client Service emmits
```
[MessageUrn("snack-hub-clients")]
[EntityName("client-created")]
public record ClientCreated(Guid Id, string Name, string Cpf, string Email = "");
```

```
[MessageUrn("snack-hub-clients")]
[EntityName("client-deleted")]
public record ClientDeleted(Guid Id);
```

```
[MessageUrn("snack-hub-clients")]
[EntityName("client-updated")]
public record ClientUpdated(Guid Id, string Name, string Cpf, string Email = "");
```

## Product Service emmits
```
[MessageUrn("snack-hub-products")]
[EntityName("product-created")]
public record ProductCreated(Guid Id, string Name, decimal Price, string Description);
```

```
[MessageUrn("snack-hub-products")]
[EntityName("product-deleted")]
public record ProductDeleted(Guid Id);
```

```
[MessageUrn("snack-hub-products")]
[EntityName("product-updated")]
public record ProductUpdated(Guid Id, string Name, decimal Price, string Description);
```

## Order Service emmits
```
[MessageUrn("snack-hub-production")]
[EntityName("production-order-submitted")]
public record ProductionOrderSubmittedRequest(Guid OrderId, IEnumerable<ProductionOrderProductDetails> ProductList);
public record ProductionOrderProductDetails(Guid ProductId, int Quantity);
```
```
[MessageUrn("snack-hub-payments")]
[EntityName("payment-requested")]
public record PaymentRequest(
    Guid OrderId,
    decimal Amount,
    object? Metadata = null
);
```
## Payment Service emmits
```
[MessageUrn("snack-hub-payments")]
[EntityName("payment-status-updated")]
public record PaymentStatusUpdated(Guid OrderId, Guid TransactionId, TransactionState Status);
```

## Production Order Service emmits
```
[MessageUrn("snack-hub-production")]
[EntityName("production-order-status-updated")]
public record ProductionOrderStatusUpdated(Guid OrderId, ProductionOrderStatus Status);
```
```
[MessageUrn("snack-hub-production")]
[EntityName("production-order-accepted")]
public record ProductionOrderAccepted(Guid OrderId);
```