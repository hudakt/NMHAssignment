using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMHAssignment.Application.Common.Interfaces
{
    public interface IMessageHub
    {
        void Publish<T>(T data, string queueName);
    }
}
