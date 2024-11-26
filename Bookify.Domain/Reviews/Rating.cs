using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookify.Domain.Abstraction;

namespace Bookify.Domain.Reviews
{
    public record Rating
    {
        public static readonly Error RatingInvalid = new(
            "Rating.Invalid",
            "The rating is invalid");
        public Rating(int value) => Value = value;
        public int Value { get; init; }

        public static Result<Rating> Create(int value)
        {
            if(value < 1 || value > 5)
            {
                return Result.Failure<Rating>(RatingInvalid);
            }

            return new Rating(value);
        }
    }
}
