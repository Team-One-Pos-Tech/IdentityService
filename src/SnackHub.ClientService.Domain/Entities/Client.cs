using System;
using SnackHub.ClientService.Domain.Base;
using SnackHub.ClientService.Domain.ValueObjects;

namespace SnackHub.ClientService.Domain.Entities;

public class Client : IAggregateRoot
{
    public Client(Guid id, string name, Cpf cpf, string email)
    {
        Id = id;
        Name = name;
        Cpf = cpf;
        Email = email;
    }

    public Guid Id { get; }
    public string Name { get; }
    public Cpf Cpf { get; }
    public string Email { get; set; }

    public ClientModel ToDatabaseModel()
    {
        return ClientModel.Create(
            Id,
            Name,
            Cpf.Value,
            Email);
    }
}