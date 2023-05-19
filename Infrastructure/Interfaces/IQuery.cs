using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IQuery<in TCriteria, out TResult>
    {
        TResult Execute(TCriteria criteria);
    }

    public interface IQuery<out TResult>
    {
        TResult Execute();
    }
}
