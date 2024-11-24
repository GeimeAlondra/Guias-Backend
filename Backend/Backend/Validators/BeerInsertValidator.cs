using Backend.DTOs;
using FluentValidation;

namespace Backend.Validators
{
    public class BeerInsertValidator : AbstractValidator<BeerInsertDto>
    {
        public BeerInsertValidator() 
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("El nombre es obligatorio");

            RuleFor(x => x.Name).Length(2,20).WithMessage("Debe tener entre 2 y 20 caracteres");

            RuleFor(x => x.BrandID).NotNull().WithMessage("Debe ingresar una marca de cerveza valida");

            RuleFor(x => x.BrandID).GreaterThan(0).WithMessage("La marca debe estar registrada");

            RuleFor(x => x.Al).GreaterThan(0).WithMessage("El nivel de alcohol {PropertyName} debe ser legal");
        }
    }
}
