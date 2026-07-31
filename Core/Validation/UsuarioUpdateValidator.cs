using Core.Input;
using FluentValidation;

namespace Core.Validation {
    public class UsuarioUpdateValidator : AbstractValidator<UsuarioUpdate> {
        public UsuarioUpdateValidator() {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("O Id do usuário é obrigatório.");

            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O nome é obrigatório.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("O e-mail é obrigatório.")
                .EmailAddress().WithMessage("Formato de e-mail inválido.");

            When(x => !string.IsNullOrWhiteSpace(x.Senha), () => {
                RuleFor(x => x.Senha)
                    .MinimumLength(8).WithMessage("A senha deve ter no mínimo 8 caracteres.")
                    .Matches(@"[A-Z]").WithMessage("A senha deve conter ao menos uma letra maiúscula.")
                    .Matches(@"[a-z]").WithMessage("A senha deve conter ao menos uma letra minúscula.")
                    .Matches(@"[0-9]").WithMessage("A senha deve conter ao menos um número.")
                    .Matches(@"[\W_]").WithMessage("A senha deve conter ao menos um caractere especial.");
            });

            RuleFor(x => x.NivelAcessoId)
                .GreaterThan(0).WithMessage("O nível de acesso é obrigatório.");
        }
    }
}