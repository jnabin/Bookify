using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookify.Domain.Apartments;
using Bookify.Domain.Shared;

namespace Bookify.Domain.Bookings
{
    public class PricingService
    {
        public PricingDetails CalculatePrice(Apartment apartment, DateRange dateRange)
        {
            Currency currency = apartment.Price.Currency;

            var priceForPeriod = new Money(
                dateRange.LengthInDays * apartment.Price.amount,
                currency);

            decimal percentageUpCharge = 0;

            foreach (var amenity in apartment.Amenities)
            {
                percentageUpCharge += amenity switch
                {
                    Amenity.GardenView or Amenity.MountainView => 0.05m,
                    Amenity.AirConditioning => 0.01m,
                    Amenity.Parking => 0.01m,
                    _ => 0
                };
            }

            Money amenitiesUpCharge = Money.zero(currency);
            if(percentageUpCharge > 0)
            {
                amenitiesUpCharge = new Money(
                    priceForPeriod.amount*percentageUpCharge, 
                    currency);
            }

            Money totalPrice = Money.zero(currency);
            totalPrice += priceForPeriod;

            if (!apartment.CleaningFee.IsZero())
            {
                totalPrice += apartment.CleaningFee; 
            }

            totalPrice += amenitiesUpCharge;

            return new PricingDetails(priceForPeriod, apartment.CleaningFee, amenitiesUpCharge, totalPrice);

        }

    }
}
