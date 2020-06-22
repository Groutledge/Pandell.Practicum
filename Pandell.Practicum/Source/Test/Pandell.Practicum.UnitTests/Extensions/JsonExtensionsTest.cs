using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using FluentAssertions;
using Pandell.Practicum.App.Extensions;
using Pandell.Practicum.UnitTests.Enumerations;
using Xunit;

namespace Pandell.Practicum.UnitTests.Extensions
{
    [ExcludeFromCodeCoverage]
    public class JsonExtensionsTest
    {
        [Fact]
        public void ToJsonObject_ReturnsAJsonObject_FromASetOfIntegers()
        {
            var actualResult = TestSetOfIntegers.ToJsonObject();
            actualResult.Should().NotBeNull();
            actualResult.Object.Length.Should().Be(TestSetOfIntegers.Count());
        }

        [Fact]
        public void FromJsonObject_ReturnsAListOfIntegers_FromAJsonObject()
        {
            var actualResult = TestJsonObject.FromJsonObject();
            actualResult.Should()
                .NotBeEmpty()
                .And.BeEquivalentTo(TestSetOfIntegers);
        }
        
        #region Class Members
        
        private List<int> TestSetOfIntegers => new List<int> {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
        private JsonObject<string[]> TestJsonObject => new JsonObject<string[]>(TestParameterCodes.IntegerJsonString.ToDescription());
        
        #endregion
    }
}