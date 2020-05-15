using Admin.dbBind;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.validator
{
    public class PartValidator : AbstractValidator<Participant>
    {
        public  PartValidator()
            {
            RuleFor(p => p.email)
                .Cascade(CascadeMode.StopOnFirstFailure)                
                .EmailAddress().WithMessage("{PropertyValue}    is not valid email");

            }
    }
}
