using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1
{
    internal interface ISending<in T>
    {
        bool Send(T sender, long sum);
    }
}
