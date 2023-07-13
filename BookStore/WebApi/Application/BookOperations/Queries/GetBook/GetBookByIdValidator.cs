using FluentValidation;

namespace WebApi.Application.BookOperations.Queries.GetBook
{
    public class GetBookByIdValidator : AbstractValidator<GetBookById>
    {
        public GetBookByIdValidator()
        {
            RuleFor(query => query.tempId).GreaterThan(0);
        }
    }
}