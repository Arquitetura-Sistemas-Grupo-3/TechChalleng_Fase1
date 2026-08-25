using Core.Validation;
using Core.Input;
using FluentAssertions;

namespace WebAPI.Tests
{
    public class UsuarioValidatorTeste
    {
        private UsuarioAdicionarRequisicaoValidator validation;

        public UsuarioValidatorTeste()
        {
            validation = new UsuarioAdicionarRequisicaoValidator();
        }

        [Fact(DisplayName = "Validação - e-mail empty")]
        [Trait("Validação", "Usuário")]
        public void Validator_ShouldReturnEmailErrorMessage()
        {
            var resultado = validation.Validate(new UsuarioAdicionarRequisicao { Email = "", Nome = "", Senha = "" });

            resultado.Errors.Should().Contain(e => e.ErrorMessage == "O e-mail é obrigatório.");
          
        }

        [Fact(DisplayName = "Validação - e-mail inválido")]
        [Trait("Validação", "Usuário")]
        public void Validator_ShouldReturnEmailInvalidErrorMessage()
        {
            var resultado = validation.Validate(new UsuarioAdicionarRequisicao { Email = "juli.com", Nome = "", Senha = "" });

            resultado.Errors.Should().Contain(e => e.ErrorMessage == "Formato de e-mail inválido.");
        }

        [Fact(DisplayName = "Validação - senha inválida")]
        [Trait("Validação", "Usuário")]
        public void Validator_ShouldReturnPasswodErrorMessage()
        { 
            var resultado = validation.Validate(new UsuarioAdicionarRequisicao { Email = "", Nome = "", Senha = "" });

            resultado.Errors.Should().Contain(e => e.ErrorMessage == "A senha é obrigatória.");
            resultado.Errors.Should().Contain(e => e.ErrorMessage == "A senha deve ter no mínimo 8 caracteres.");
            resultado.Errors.Should().Contain(e => e.ErrorMessage == "A senha deve conter ao menos uma letra maiúscula.");
            resultado.Errors.Should().Contain(e => e.ErrorMessage == "A senha deve conter ao menos uma letra minúscula.");  
            resultado.Errors.Should().Contain(e => e.ErrorMessage == "A senha deve conter ao menos um caractere especial.");
            resultado.Errors.Should().Contain(e => e.ErrorMessage == "A senha deve conter ao menos um número.");
        }

        [Fact(DisplayName = "Validação - nome vazio")]
        [Trait("Validação", "Usuário")]
        public void Validator_ShouldReturnNomeErrorMessage()
        {
            var resultado = validation.Validate(new UsuarioAdicionarRequisicao { Email = "", Nome = "", Senha = "" });
            resultado.Errors.Should().Contain(e => e.ErrorMessage == "O nome é obrigatório.");
        }

        [Fact(DisplayName = "Validação - dados válidos não geram erros")]
        [Trait("Validação", "Usuário")]
        public void Validator_QuandoDadosValidos_NaoDeveRetornarErros()
        {
            var resultado = validation.Validate(new UsuarioAdicionarRequisicao
            {
                Nome = "Juliana",
                Email = "juliana@gmail.com",
                Senha = "SenhaForte123!"
            });

            resultado.IsValid.Should().BeTrue();
            resultado.Errors.Should().BeEmpty();
        }

    }
}
