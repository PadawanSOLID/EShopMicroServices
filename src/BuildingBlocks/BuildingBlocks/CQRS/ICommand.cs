﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.CQRS
{
    public interface ICommand : IRequest<Unit>
    {
    }
    public interface ICommand<out TResponse>:IRequest<TResponse>
    {
    }
}
