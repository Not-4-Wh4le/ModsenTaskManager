using Domain.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class User : IEntity
    {
        public Guid Id {  get; init; }
        public string Email { get; private set; } = string.Empty;
        public string PasswordHash { get; private set; } = string.Empty;
        public DateTimeOffset  CreatedAt {  get; init; } 

        public User(Guid id, string email, string passwordHash)
        {
            if(id == Guid.Empty)
                throw new ArgumentNullException("Id is required");

            if(string.IsNullOrEmpty(email))
                throw new ArgumentNullException("Email is required");

            if (string.IsNullOrEmpty(passwordHash))
                throw new ArgumentNullException("Password hash is required");

            Id = id;
            Email = email.Trim().ToLowerInvariant();
            PasswordHash = passwordHash;
            CreatedAt = DateTimeOffset.UtcNow;
        }
    }
}
