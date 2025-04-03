using System;
using EmailSenderProgram;
using System.Collections.Generic;

namespace DataAccessLayer
{
    public static class CustomerDataAccessLayer
    {
        public static List<Customer> ListCustomers()
        {
            return DataLayer.ListCustomers();
        }

        public static List<Order> ListOrders()
        {
            return DataLayer.ListOrders();
        }
    }
}
