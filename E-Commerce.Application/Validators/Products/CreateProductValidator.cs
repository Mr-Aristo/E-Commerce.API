using E_Commerce.Application.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Validators.Products
{
    /// <summary>
    /// Validator classlari program.cs de tanimlanmalidir.
    /// </summary>
    public class CreateProductValidator : AbstractValidator<VM_Create_Product>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("Product name cannot be empty")
                .MaximumLength(150)
                .MinimumLength(5)
                .WithMessage("Product name cannot conaint less than 5 and more than 150 charachter");

            RuleFor(x => x.Stock)
                .NotEmpty()
                .NotNull()
                .WithMessage("Stock number cannot be empty")
                .Must(s => s >= 0) //Sayisal degerler must ile verilmeli.
                .WithMessage("Stock cannot be negative");

        }
    }

}
