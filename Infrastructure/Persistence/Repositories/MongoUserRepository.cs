using Application.Common.Interfaces.Repositories;
using Domain.Entites;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Repositories
{
    public class MongoUserRepository 
        : MongoRepositoryBase<User>, IUserRepository
    {
        public MongoUserRepository(MongoDbContext context)
            : base(context, MongoCollectionNames.Users)
        {
        }

        public async Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken)
        {
            var isExist = await Collection.Find(u => u.Email == email).AnyAsync(cancellationToken);
            return isExist;

        }

        public async Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return await Collection.Find(u => u.Email == email).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
