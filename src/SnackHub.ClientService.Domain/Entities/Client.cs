using System;
using SnackHub.ClientService.Domain.Base;
using SnackHub.ClientService.Domain.ValueObjects;

namespace SnackHub.ClientService.Domain.Entities
{
    public class Client : IAggregateRoot
    {
        public Client(Guid id, string name, Cpf cpf, string email)
        {
            Id = id;
            Name = name;
            Cpf = cpf;
            Email = email;
        }
        
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public Cpf Cpf { get; private set; }
        public string Email { get; set; }
    }
}