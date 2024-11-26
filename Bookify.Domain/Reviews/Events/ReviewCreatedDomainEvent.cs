using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookify.Domain.Abstraction;

namespace Bookify.Domain.Reviews.Events
{
    public sealed record ReviewCreatedDomainEvent(Guid value) : IDomainEvent;
}
