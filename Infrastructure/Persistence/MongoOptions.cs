using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence
{
    public class MongoOptions
    {
        public const string SectionName = "MongoSettings";

        public string ConnectionString { get; init; } = string.Empty;
        public string DatabaseName {  get; init; } = string.Empty;
    }
}
