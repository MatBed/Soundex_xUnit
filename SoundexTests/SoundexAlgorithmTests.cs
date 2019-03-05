using Soundex_CSharp_xUnit;
using Xunit;

namespace SoundexTests
{
    public class SoundexAlgorithmTests
    {
        private IAlgorithmData soundexData;
        private IAlgorithm soundex;

        public SoundexAlgorithmTests()
        {
            soundexData = new SoundexData();
            soundex = new SoundexAlgorithm(soundexData);
        }

        [Fact]
        public void WhenTheWordIsEmptyThenReturn0000()
        {
            string expectedValue = soundex.Encode("");

            Assert.Equal("0000", expectedValue);
        }

        [Fact]
        public void WhenTheWordIsNullThenReturn0000()
        {
            string expectedValue = soundex.Encode(null);

            Assert.Equal("0000", expectedValue);
        }

        [Fact]
        public void WhenTheWordHaveOneCharThenFillTheWordBy0()
        {
            string expectedValue = soundex.Encode("w");

            Assert.Equal("w000", expectedValue);
        }

        [Fact]
        public void WhenTheWordHaveUpperCaseThenChancgeToLowerCase()
        {
            string expectedValue = soundex.Encode("ABCD");

            Assert.Equal("a123", expectedValue);
        }

        [Fact]
        public void WhenTheWordHaveFourCharsWithDifferentNumbersThenReturnEncodedWord()
        {
            string expectedValue = soundex.Encode("bcdm");

            Assert.Equal("b235", expectedValue);
        }

        [Fact]
        public void WhenTheWordHaveMoreThanOneCharAndLessThanFourCharsThenAdd0()
        {
            string expectedValue = soundex.Encode("an");

            Assert.Equal("a500", expectedValue);
        }

        [Fact]
        public void WhenTheWordHaveMoreThanFourCharsThenRemoveRedundantChars()
        {
            string expectedValue = soundex.Encode("anrtzv");

            Assert.Equal("a563", expectedValue);
        }

        [Fact]
        public void WhenTheWordHaveNeighboringCharsWithTheSameNumberThenRemoveAllThisCharsWitchoutFirst()
        {
            string expectedValue = soundex.Encode("accb");

            Assert.Equal("a210", expectedValue);
        }

        [Fact]
        public void WhenTheWordHaveCharsWhichDoNotExistInDictionaryThenRemoveThisChars()
        {
            string expectedValue = soundex.Encode("acob");

            Assert.Equal("a210", expectedValue);
        }

        [Fact]
        public void WhenTheWordHaveSpecialCharsThenReplaceThisCharsTo0()
        {
            string expectedValue = soundex.Encode("!%#&");

            Assert.Equal("0000", expectedValue);
        }

        [Fact]
        public void WhenInTheWordTwoLettersWithTheSameNumberAreSeparatedByWThenEncodeLikeOneNumber()
        {
            string expectedValue = soundex.Encode("bgwjlm");

            Assert.Equal("b245", expectedValue);
        }

        [Fact]
        public void WhenInTheWordTwoLettersWithTheSameNumberAreSeparatedByHThenEncodeLikeOneNumber()
        {
            string expectedValue = soundex.Encode("bghjlm");

            Assert.Equal("b245", expectedValue);
        }

        [Fact]
        public void WhenInTheWordTwoLettersWithTheSameNumberAreSeparatedByVowelThenEncodeTwice()
        {
            string expectedValue = soundex.Encode("bditv");

            Assert.Equal("b331", expectedValue);
        }

        [Theory]
        [InlineData("Robert")]
        [InlineData("Rupert")]
        public void WhentTheWordIsRobertOrRuperThenReturnR163(string word)
        {
            string expectedValue = soundex.Encode(word);

            Assert.Equal("r163", expectedValue);
        }

        [Theory]
        [InlineData("Rubin")]
        public void WhentTheWordIsRubinThenReturnR150(string word)
        {
            string expectedValue = soundex.Encode(word);

            Assert.Equal("r150", expectedValue);
        }

        [Theory]
        [InlineData("Ashcraft")]
        [InlineData("Ashcroft")]
        public void WhentTheWordIsAshcraftOrAshcroftThenReturnA261(string word)
        {
            string expectedValue = soundex.Encode(word);

            Assert.Equal("a261", expectedValue);
        }

        [Theory]
        [InlineData("Tymczak")]
        public void WhentTheWordIsTymczakThenReturnT522(string word)
        {
            string expectedValue = soundex.Encode(word);

            Assert.Equal("t522", expectedValue);
        }

        [Theory]
        [InlineData("Pfister")]
        public void WhentTheWordIsPfisterThenReturnP123(string word)
        {
            string expectedValue = soundex.Encode(word);

            Assert.Equal("p123", expectedValue);
        }

        [Theory]
        [InlineData("Honeyman")]
        public void WhentTheWordIsHoneymanThenReturnH555(string word)
        {
            string expectedValue = soundex.Encode(word);

            Assert.Equal("h555", expectedValue);
        }
    }
}
