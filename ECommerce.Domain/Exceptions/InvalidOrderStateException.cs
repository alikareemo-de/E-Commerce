﻿namespace ECommerce.Domain.Exceptions
{
    public class InvalidOrderStateException : Exception
    {
        public InvalidOrderStateException(string message) : base(message) { }
    }
}
