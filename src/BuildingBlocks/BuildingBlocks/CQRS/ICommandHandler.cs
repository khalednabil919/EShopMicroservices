using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.CQRS
{
    public interface ICommandHandler<TCommand>:IRequestHandler<TCommand,Unit> 
        where TCommand:ICommand<Unit>
    {

    }
    public interface ICommandHandler<TCommand, TResponse>:IRequestHandler<TCommand, TResponse>
        where TCommand: ICommand<TResponse>
        where TResponse : notnull
    {
    }
}
