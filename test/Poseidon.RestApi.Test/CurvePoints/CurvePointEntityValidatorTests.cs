using FluentValidation;
using System;
using System.Linq;
using System.Text;
using Xunit;

namespace Poseidon.RestApi.CurvePoints
{
    public class CurvePointEntityValidatorTests
    {
        [Fact]
        public void InheritsAbstractValidator()
        {
            Assert.True(typeof(AbstractValidator<CurvePointEntity>)
                .IsAssignableFrom(typeof(CurvePointEntityValidator)));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void CurveIdLessThanOne(int value)
        {
            var entity = CreateEntity(curveId: value);

            var result = CreateValidator().Validate(entity);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(CurvePointEntity.CurveId));

            var error = Assert.Single(errors);
            Assert.Equal("CurveIdLessThanOne", error.ErrorCode);
        }

        [Theory]
        [InlineData(-0.1)]
        [InlineData(-1.0)]
        public void TermLessThanZero(double value)
        {
            var entity = CreateEntity(term: value);

            var result = CreateValidator().Validate(entity);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(CurvePointEntity.Term));

            var error = Assert.Single(errors);
            Assert.Equal("TermLessThanZero", error.ErrorCode);
        }

        [Theory]
        [InlineData(-0.1)]
        [InlineData(-1.0)]
        public void ValueLessThanZero(double value)
        {
            var entity = CreateEntity(value: value);

            var result = CreateValidator().Validate(entity);

            var errors = result.Errors
                .Where(e => e.PropertyName == nameof(CurvePointEntity.Value));

            var error = Assert.Single(errors);
            Assert.Equal("ValueLessThanZero", error.ErrorCode);
        }

        private static CurvePointEntityValidator CreateValidator() =>
            new CurvePointEntityValidator();
        private static CurvePointEntity CreateEntity(
            int id = 1,
            int curveId = 1,
            DateTimeOffset? asOfDate = null,
            double term = 1,
            double value = 1,
            DateTimeOffset? creationDate = null) =>
            new CurvePointEntity()
            {
                Id = id,
                CurveId = curveId,
                AsOfDate = asOfDate ?? DateTimeOffset.UtcNow,
                Term = term,
                Value = value,
                CreationDate = creationDate ?? DateTimeOffset.UtcNow,
            };
    }
}