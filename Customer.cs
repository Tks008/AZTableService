using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace AZTableService
{
    public class Customer : TableEntity
    {
        public string Name { get; set; }
        public Customer()
        {

        }
        public Customer(string name,string city,string customerID)
        {
            Name = name;
            RowKey = customerID;
            PartitionKey = city;
        }
    }
}
