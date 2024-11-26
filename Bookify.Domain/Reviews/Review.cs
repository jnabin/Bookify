using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookify.Domain.Abstraction;
using Bookify.Domain.Apartments;
using Bookify.Domain.Bookings;
using Bookify.Domain.Reviews.Events;
using Bookify.Domain.Users;

namespace Bookify.Domain.Reviews
{
    public sealed class Review : Entity
    {
        private Review(
            Guid id,
            Comment comment,
            Guid apartmentId,
            Guid bookingId,
            Guid userId,
            Rating rating,
            DateTime createdOnUtc) 
            : base(id)
        {
            Comment = comment;
            ApartmentId = apartmentId;
            BookingId = bookingId;
            UserId = userId;
            Rating = rating;
            CreatedOnUtc = createdOnUtc;
        }

        public Guid ApartmentId { get; private set; }
        public Guid BookingId { get; private set; }
        public Guid UserId { get; private set; }
        public Comment Comment { get; private set; }
        public Rating Rating { get; private set; }
        public DateTime CreatedOnUtc { get; private set; }

        public static Result<Review> Create(
            Guid id,
            Comment comment,
            Booking booking,
            User user,
            Rating rating,
            DateTime createdOnUtc)
        {
            if(booking.Status != BookingStatus.Completed)
            {
                return Result.Failure<Review>(ReviewErrors.NotEligible);
            }

            Review review = new Review(
                Guid.NewGuid(),
                comment,
                booking.ApartmentId,
                booking.Id,
                user.Id,
                rating,
                createdOnUtc);

            review.RaiseDomainEvents(new ReviewCreatedDomainEvent(review.Id));

            return review;
        }
    }
}
