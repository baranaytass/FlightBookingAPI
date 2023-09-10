using FlightBookingAPI.Models;
using FluentValidation;

public class RequestValidator : AbstractValidator<FlightSearchRequest>
{
    public RequestValidator()
    {
        RuleFor(request => request.FromAirportCode).NotEmpty().WithMessage("FromAirportCode alanı gereklidir.");
        RuleFor(request => request.ToAirportCode).NotEmpty().WithMessage("ToAirportCode alanı gereklidir.");
        RuleFor(request => request.FromAirportCode).NotEqual(request => request.ToAirportCode).WithMessage("FromAirportCode ve ToAirportCode aynı olamaz.");
        RuleFor(request => request.PassengerCount).InclusiveBetween(1, 5).WithMessage("PassengerCount 1 ile 5 arasında olmalıdır.");
        RuleFor(request => request.DepartureDate).NotEmpty().WithMessage("DepartureDate alanı gereklidir.").Must(BeAValidDate).WithMessage("DepartureDate tarih alanı olmalıdır.");
        RuleFor(request => request.ReturnDate).Must((request, returnDate) => BeValidReturnDate(request.DepartureDate, returnDate)).When(request => request.FlightType == FlightType.RoundTrip).WithMessage("ReturnDate tarih alanı olmalıdır ve DepartureDate'den sonra olmalıdır.");
    }

    private bool BeAValidDate(DateTime date)
    {
        return date > DateTime.Now;
    }

    private bool BeValidReturnDate(DateTime departureDate, DateTime? returnDate)
    {
        return returnDate.HasValue && returnDate.Value > departureDate;
    }
}
