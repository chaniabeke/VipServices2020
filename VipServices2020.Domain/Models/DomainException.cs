using System;
using System.Collections.Generic;
using System.Text;

namespace VipServices2020.Domain.Models
{
    public class DomainException : Exception
    {
        public DomainException(string message) : base(message)
        {
        }
    }
}
