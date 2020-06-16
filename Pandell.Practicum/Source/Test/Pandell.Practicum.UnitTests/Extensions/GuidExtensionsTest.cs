using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Pandell.Practicum.App.Extensions;
using Xunit;

namespace Pandell.Practicum.UnitTests.Extensions
{
    [ExcludeFromCodeCoverage]
    public class GuidExtensionsTest
    {
        private const int MaxByteSize = 16;
        
        [Fact]
        public void FromGuid_TransformsAGuid_IntoAByteArray()
        {
            var actualResult = GenerateByteArrayFromGuid();

            actualResult.Should()
                .NotBeEmpty()
                .And.BeOfType<byte[]>()
                .And.HaveCount(MaxByteSize);
        }

        [Fact]
        public void ToGuid_TransformsAByteArray_IntoAGuid()
        {
            var testByteArray = GenerateByteArrayFromGuid();
            var actualResult = testByteArray.ToGuid();

            actualResult.Should().NotBeEmpty();
        }

        [Theory]
        [MemberData(nameof(IsEmptyGuidTestCases))]
        public void IsEmptyGuid_WithDifferingGuidParameters_ReturnsExpectedResults(Guid guidToTest, bool expectedResult)
        {
            guidToTest.IsEmptyGuid().Should().Be(expectedResult);
        }
        
        #region Test Cases

        public static IEnumerable<object[]> IsEmptyGuidTestCases => new List<object[]>
        {
            new object[] {Guid.Empty, true}, 
            new object[] {null, true}, 
            new object[] {Guid.NewGuid(), false}
        };
        
        #endregion
        
        private byte[] GenerateByteArrayFromGuid()
        {
            var testGuid = Guid.NewGuid();
            return testGuid.FromGuid();
        }
    }
}