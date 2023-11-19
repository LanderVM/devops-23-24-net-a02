﻿namespace Shared.Customers;

public abstract class CustomerRequest
{
    public class Index
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 25;
    }
}


