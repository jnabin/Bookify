using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookify.Domain.Abstraction;

namespace Bookify.Domain.Bookings.Events
{
    public sealed record BookingRejectedDomainEvent(Guid id) : IDomainEvent;
}
