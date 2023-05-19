using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IHandle<in TCommand>
    {
        void Handle(TCommand command);
    }

    public interface IHandle<in TCommand, out TResult>
    {
        TResult Handle(TCommand command);
    }
}
