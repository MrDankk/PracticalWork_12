using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1
{
    internal interface IReceiving<in T>
    {
        void Receive(T recipient, long sum);
    }
}
