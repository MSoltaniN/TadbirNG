using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace SPPC.Framework.Presentation.Tests
{
    [TestFixture]
    [Category("Presentation")]
    public class FilterExpressionBuilderTests
    {
        [Test]
        public void New_GivenSimpleFilter_BuildsSingleExpression()
        {
            // Arrange
            var filter = GetFirstTestExpression();
            var builder = new FilterExpressionBuilder();

            // Act
            var expression = builder.New(filter)
                .Build();

            // Assert
            Assert.AreEqual(_firstExpected, expression.ToString());
        }

        [Test]
        public void And_GivenSimpleFilter_CombinesWithExistingFilter()
        {
            // Arrange
            var existing = GetFirstTestExpression();
            var filter = GetSecondTestExpression();
            var builder = new FilterExpressionBuilder();
            var expected = String.Format("({0}{1}{2})", _firstExpected, FilterExpressionOperator.And, _secondExpected);

            // Act
            var expression = builder.New(existing)
                .And(filter)
                .Build();

            // Assert
            Assert.AreEqual(expected, expression.ToString());
        }

        [Test]
        public void And_GivenSimpleFilters_CombinesAllWithExistingFilter()
        {
            // Arrange
            var existing = GetFirstTestExpression();
            var filter = GetSecondTestExpression();
            var filters = new List<GridFilter>(new GridFilter[] { filter, filter, filter });
            var builder = new FilterExpressionBuilder();
            var expected = String.Format(
                "({0}{1}{2}{1}{2}{1}{2})", _firstExpected,
                FilterExpressionOperator.And, _secondExpected);

            // Act
            var expression = builder.New(existing)
                .And(filters.ToArray())
                .Build();

            // Assert
            Assert.AreEqual(expected, expression.ToString());
        }

        [Test]
        public void Or_GivenSimpleFilter_CombinesWithExistingFilter()
        {
            // Arrange
            var existing = GetFirstTestExpression();
            var filter = GetSecondTestExpression();
            var builder = new FilterExpressionBuilder();
            var expected = String.Format("({0}{1}{2})", _firstExpected, FilterExpressionOperator.Or, _secondExpected);

            // Act
            var expression = builder.New(existing)
                .Or(filter)
                .Build();

            // Assert
            Assert.AreEqual(expected, expression.ToString());
        }

        [Test]
        public void Or_GivenSimpleFilters_CombinesAllWithExistingFilter()
        {
            // Arrange
            var existing = GetFirstTestExpression();
            var filter = GetSecondTestExpression();
            var filters = new List<GridFilter>(new GridFilter[] { filter, filter, filter });
            var builder = new FilterExpressionBuilder();
            var expected = String.Format(
                "({0}{1}{2}{1}{2}{1}{2})", _firstExpected,
                FilterExpressionOperator.Or, _secondExpected);

            // Act
            var expression = builder.New(existing)
                .Or(filters.ToArray())
                .Build();

            // Assert
            Assert.AreEqual(expected, expression.ToString());
        }

        private GridFilter GetFirstTestExpression()
        {
            return new GridFilter()
            {
                FieldName = "Name",
                FieldTypeName = "System.String",
                Operator = GridFilterOperator.Contains,
                Value = "Test"
            };
        }

        private GridFilter GetSecondTestExpression()
        {
            return new GridFilter()
            {
                FieldName = "Number",
                FieldTypeName = "System.Int32",
                Operator = GridFilterOperator.IsGreaterOrEqualTo,
                Value = "123"
            };
        }

        private const string _firstExpected = "Name.Contains(\"Test\")";
        private const string _secondExpected = "Number >= 123";
    }
}
