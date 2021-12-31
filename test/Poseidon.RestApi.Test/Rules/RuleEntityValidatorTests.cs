using FluentValidation;
using System.Linq;
using System.Text;
using Xunit;

namespace Poseidon.RestApi.Rules
{
    public class RuleEntityValidatorTests
    {
        [Fact]
        public void InheritsAbstractValidator()
        {
            Assert.True(typeof(AbstractValidator<RuleEntity>)
                .IsAssignableFrom(typeof(RuleEntityValidator)));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void NameEmpty(string? value)
        {
            var entity = CreateEntity(name: value!);

            var result = CreateValidator().Validate(entity);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(RuleEntity.Name));

            var error = Assert.Single(errors);
            Assert.Equal("NameEmpty", error.ErrorCode);
        }

        [Fact]
        public void NameMaxLength125()
        {
            var entity = CreateEntity(name: CreateString(126));

            var result = CreateValidator().Validate(entity);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(RuleEntity.Name));

            var error = Assert.Single(errors);
            Assert.Equal("NameMaxLength125", error.ErrorCode);
        }

        [Fact]
        public void DescriptionMaxLength125()
        {
            var entity = CreateEntity(description: CreateString(126));

            var result = CreateValidator().Validate(entity);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(RuleEntity.Description));

            var error = Assert.Single(errors);
            Assert.Equal("DescriptionMaxLength125", error.ErrorCode);
        }

        [Fact]
        public void JsonMaxLength125()
        {
            var entity = CreateEntity(json: CreateString(126));

            var result = CreateValidator().Validate(entity);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(RuleEntity.Json));

            var error = Assert.Single(errors);
            Assert.Equal("JsonMaxLength125", error.ErrorCode);
        }

        [Fact]
        public void TemplateMaxLength512()
        {
            var entity = CreateEntity(template: CreateString(513));

            var result = CreateValidator().Validate(entity);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(RuleEntity.Template));

            var error = Assert.Single(errors);
            Assert.Equal("TemplateMaxLength512", error.ErrorCode);
        }

        [Fact]
        public void SqlStrMaxLength125()
        {
            var entity = CreateEntity(sqlStr: CreateString(126));

            var result = CreateValidator().Validate(entity);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(RuleEntity.SqlStr));

            var error = Assert.Single(errors);
            Assert.Equal("SqlStrMaxLength125", error.ErrorCode);
        }

        [Fact]
        public void SqlPartMaxLength125()
        {
            var entity = CreateEntity(sqlPart: CreateString(126));

            var result = CreateValidator().Validate(entity);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(RuleEntity.SqlPart));

            var error = Assert.Single(errors);
            Assert.Equal("SqlPartMaxLength125", error.ErrorCode);
        }

        private static RuleEntityValidator CreateValidator() =>
            new RuleEntityValidator();
        private static RuleEntity CreateEntity(
            int id = 1,
            string name = "Test Name",
            string description = "Test Description",
            string json = "Test Json",
            string template = "Test Template",
            string sqlStr = "Test SqlStr",
            string sqlPart = "Test SqlPart") =>
            new RuleEntity()
            {
                Id = id,
                Name = name,
                Description = description,
                Json = json,
                Template = template,
                SqlStr = sqlStr,
                SqlPart = sqlPart,
            };
        private static string CreateString(int length)
        {
            var builder = new StringBuilder();

            for (var i = 0; i < length; i++)
                builder.Append('0');

            return builder.ToString();
        }
    }
}