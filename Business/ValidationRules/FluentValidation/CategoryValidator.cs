using Core.Utilities.Results;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation;

public class CategoryValidator:AbstractValidator<Category>
{
    
    public CategoryValidator()
    {
        RuleFor(c => c.CategoryName).NotEmpty();
        RuleFor(c=>c.CategoryName).MinimumLength(2);
        RuleFor( c=>c.CategoryName).MaximumLength(50);
        RuleFor( c=>c.CategoryName).Must(ContainNoDigits).WithMessage("Category name must not contain digits.");
    }

    private bool ContainNoDigits(string categoryName)
    {
        return !categoryName.Any(char.IsDigit);
    }
}