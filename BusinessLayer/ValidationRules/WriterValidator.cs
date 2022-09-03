using EntityLayer.Concreate;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class WriterValidator :AbstractValidator<Writer>
    {
        public WriterValidator()
        {
            RuleFor(x => x.WriterName).NotEmpty().WithMessage("Yazar adı boş geçilemez");
            RuleFor(x => x.WriterMail).NotEmpty().WithMessage("Mail adresi boş geçilemez");
            RuleFor(x => x.WriterPassword).NotEmpty().WithMessage("Şifre boş geçilemez");
            RuleFor(p => p.WriterPassword).Matches(@"[A-Z]+").WithMessage("Sifre en az bir büyük harfden içermelidir");
            RuleFor(p => p.WriterPassword).Matches(@"[a-z]+").WithMessage("Sifre en az bir küçük harfden içermelidir");
            RuleFor(p => p.WriterPassword).Matches(@"[0-9]+").WithMessage("Sifre en az bir rakamdan içermelidir");
            RuleFor(x => x.WriterName).NotEmpty().WithMessage("Lütfen en az 2 karakter girişi yapın");
            RuleFor(x => x.WriterName).NotEmpty().WithMessage("Lütfen en fazla 50 karakter veri girişi yapın");

        }
    }
}
