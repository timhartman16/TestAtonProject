using Infrastructure.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Commands
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private IServiceProvider _serviceProvider;

        public CommandDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Handle<TCommand>(TCommand command)
        {
            _serviceProvider.GetService<IHandle<TCommand>>()?.Handle(command);
        }

        public TResult Handle<TCommand, TResult>(TCommand command)
        {
            var handler = _serviceProvider.GetService<IHandle<TCommand, TResult>>();
            var result = handler.Handle(command);

            return result;
        }
    }
}
