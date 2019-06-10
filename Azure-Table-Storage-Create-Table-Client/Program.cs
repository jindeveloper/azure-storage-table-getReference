using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Azure.Cosmos.Table; 

namespace Azure_Table_Storage_Create_Table_Client
{
    class Program
    {
        static string ConnectionString; 

        private static void SetupConnectionStrings()
        {
            var builder = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();
            ConnectionString = configuration.GetConnectionString("Storage");

        }

        static void Main(string[] args)
        {
            SetupConnectionStrings();

            var storageAccount = CloudStorageAccount.Parse(ConnectionString);

            var tableClient = storageAccount.CreateCloudTableClient();

            var customersTable = tableClient.GetTableReference("customersTable");

            customersTable.CreateIfNotExists();

            Console.WriteLine(customersTable.Name);
            Console.WriteLine(customersTable.ServiceClient);
            Console.WriteLine(customersTable.StorageUri);
            Console.WriteLine(customersTable.Uri);

        }
    }
}
