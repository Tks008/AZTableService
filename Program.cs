using System;
using System.Collections.Generic;
using Microsoft.Azure.Cosmos.Table;
namespace AZTableService
{
    class Program
    {
        static string connectionstring = "DefaultEndpointsProtocol=https;AccountName=tksstgaccout;AccountKey=c3/Ye2sz6QlljyGkG3gTjA8cC2uDTKgoHvPHhZTXg21d++WQ+IYyI+beixjZ2zAOG2g3fpcMmLcVI5A1FAq/Ug==;EndpointSuffix=core.windows.net";
        static String tablename = "Customer2";
        static string Rowkey = "C124";
        static string PartitionKey = "Dubai";
        static void Main(string[] args)
        {


            //AzureTableDataRetrieve();
          //  AzureTableUpdate();
            AzureTableDataDelete();
        }

        private static void AzureTableCreation()
        {
            #region New Table Creation
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(connectionstring);
            CloudTableClient cloudTableClient = cloudStorageAccount.CreateCloudTableClient();
            CloudTable cloudTable = cloudTableClient.GetTableReference(tablename);
            cloudTable.CreateIfNotExists();
            Console.WriteLine("Table Created");
            Console.ReadKey();
            #endregion
        }
        private static void AzureTableSingleDataInsert()
        {
            #region New data insertion
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(connectionstring);
            CloudTableClient cloudTableClient = cloudStorageAccount.CreateCloudTableClient();
            CloudTable cloudTable = cloudTableClient.GetTableReference(tablename);
            Customer customer = new Customer("TKS", "Dubai", "C123");
            TableOperation tableOperation = TableOperation.Insert(customer);
            TableResult tableResult = cloudTable.Execute(tableOperation);
            Console.WriteLine("Data Inserted");
            Console.ReadKey();
            #endregion

        }
        private static void AzureTableBatchDataInsert()
        {
            #region Batch Data Insertion
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(connectionstring);
            CloudTableClient cloudTableClient = cloudStorageAccount.CreateCloudTableClient();
            CloudTable cloudTable = cloudTableClient.GetTableReference(tablename);
            List<Customer> custlist = new List<Customer>()
            {
                new Customer("Tushar Kanti Santra","Dubai","C124"),
                new Customer("Payel Patra","Dubai","C125"),
                 new Customer("Megnath","Dubai","C126"),
            };

            TableBatchOperation tablebatchOperation = new TableBatchOperation();
            foreach (var item in custlist)
            {
                tablebatchOperation.Insert(item);
            }
            TableBatchResult tableResult = cloudTable.ExecuteBatch(tablebatchOperation);
            Console.WriteLine("List of data Inserted");
            Console.ReadKey();
            #endregion
        }
        private static void AzureTableDataRetrieve()
        {
            try
            {
                CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(connectionstring);
                CloudTableClient cloudTableClient = cloudStorageAccount.CreateCloudTableClient();
                CloudTable cloudTable = cloudTableClient.GetTableReference(tablename);
                TableOperation tableOperation = TableOperation.Retrieve<Customer>(PartitionKey, Rowkey);
                TableResult tableResult = cloudTable.Execute(tableOperation);
                Customer customer = tableResult.Result as Customer;
                Console.WriteLine($"Name-{customer.Name}, Partitionkey-{PartitionKey}, Rowkey-{Rowkey}");
                Console.ReadKey();
            }
            catch(Exception exp)
            {
                string msg = exp.Message.ToString();
            }
        }
        private static void AzureTableUpdate()
        {
            try
            {
                CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(connectionstring);
                CloudTableClient cloudTableClient = cloudStorageAccount.CreateCloudTableClient();
                CloudTable cloudTable = cloudTableClient.GetTableReference(tablename);
                Customer customer = new Customer("tks007", PartitionKey, Rowkey);
                TableOperation operation = TableOperation.InsertOrMerge(customer);
                TableResult tableResult = cloudTable.Execute(operation);
                Console.WriteLine("Data updated");
                Console.ReadKey();
            }
            catch (Exception exp)
            {
                string msg = exp.Message.ToString();
            }
        }
        private static void AzureTableDataDelete()
        {
            try
            {
                CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(connectionstring);
                CloudTableClient cloudTableClient = cloudStorageAccount.CreateCloudTableClient();
                CloudTable cloudTable = cloudTableClient.GetTableReference(tablename);
                TableOperation tableOperationretrieve = TableOperation.Retrieve<Customer>(PartitionKey, Rowkey);
                TableResult tableResult = cloudTable.Execute(tableOperationretrieve);
                Customer customer = tableResult.Result as Customer;

                TableOperation tableOperationdelete= TableOperation.Delete(customer);
                TableResult tableDeleteResult = cloudTable.Execute(tableOperationdelete);
                Console.WriteLine("Data deleted");
                Console.ReadKey();
            }
            catch (Exception exp)
            {
                string msg = exp.Message.ToString();
            }
        }

    }
}
