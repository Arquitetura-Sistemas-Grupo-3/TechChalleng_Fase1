using Core.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Input;
using FluentAssertions;

namespace WebAPI.Tests
{
    public class UsuarioValidatorTeste
    {
        private UsuarioInputValidator validation;

        public UsuarioValidatorTeste()
        {
            validation = new UsuarioInputValidator();
        }

        [Fact(DisplayName = "Validação e-mail empty")]
        [Trait("Validação", "Usuário")]
        public void Validator_ShouldReturnEmailErrorMessage()
        {
            var resultado = validation.Validate(new UsuarioInput { Email = "", NivelAcessoId = 1, Nome = "", Senha = "" });

            resultado.Errors.Should().Contain(e => e.ErrorMessage == "O e-mail é obrigatório.");
          
        }

        [Fact(DisplayName = "Validação e-mail inválido")]
        [Trait("Validação", "Usuário")]
        public void Validator_ShouldReturnEmailInvalidErrorMessage()
        {
            var resultado = validation.Validate(new UsuarioInput { Email = "juli.com", NivelAcessoId = 1, Nome = "", Senha = "" });

            resultado.Errors.Should().Contain(e => e.ErrorMessage == "Formato de e-mail inválido.");
        }

        [Fact(DisplayName = "Validação senha inválida caracteres")]
        [Trait("Validação", "Usuário")]
        public void Validator_ShouldReturnPasswodErrorMessage()
        { 
            var resultado = validation.Validate(new UsuarioInput { Email = "", NivelAcessoId = 1, Nome = "", Senha = "" });

            resultado.Errors.Should().Contain(e => e.ErrorMessage == "A senha é obrigatória.");
            resultado.Errors.Should().Contain(e => e.ErrorMessage == "A senha deve ter no mínimo 8 caracteres.");
            resultado.Errors.Should().Contain(e => e.ErrorMessage == "A senha deve conter ao menos uma letra maiúscula.");
            resultado.Errors.Should().Contain(e => e.ErrorMessage == "A senha deve conter ao menos uma letra minúscula.");  
            resultado.Errors.Should().Contain(e => e.ErrorMessage == "A senha deve conter ao menos um caractere especial.");
            resultado.Errors.Should().Contain(e => e.ErrorMessage == "A senha deve conter ao menos um número.");
        }

        [Fact(DisplayName = "Validação nível de acesso inválido")]
        [Trait("Validação", "Usuário")]
        public void Validator_ShouldReturnNivelAcessoErrorMessage()
        { 
            var resultado = validation.Validate(new UsuarioInput { Email = "", NivelAcessoId = 0, Nome = "", Senha = "" });
            resultado.Errors.Should().Contain(e => e.ErrorMessage == "O nível de acesso é obrigatório.");   
        }

        [Fact(DisplayName = "Validação nome vazio")]
        [Trait("Validação", "Usuário")]
        public void Validator_ShouldReturnNomeErrorMessage()
        {
            var resultado = validation.Validate(new UsuarioInput { Email = "", NivelAcessoId = 1, Nome = "", Senha = "" });
            resultado.Errors.Should().Contain(e => e.ErrorMessage == "O nome é obrigatório.");
        }

    }
}
