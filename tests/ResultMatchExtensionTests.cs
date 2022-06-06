using csharp_utils;
using Xunit;

namespace ResultTests
{
    public class ResultMatchExtensionTests
    {

        [Fact]
        public void MatchTest()
        {
            Result<int, string> ok = 99;

            var result = ok.Match(
                Ok: o => 1,
                Error: e => -1
            );

            Assert.Equal(1, result);

            Result<int, string> fail = "error";

            var result2 = fail.Match(
                Ok: o => 1,
                Error: e => -1
            );

            Assert.Equal(-1, result2);
        }

        [Fact]
        public void IndividualMatchTest()
        {
            Result<int, string> ok = 99;

            var result = ok.Match(
                Ok: o => true
            );

            Assert.True(result);

            Result<int, string> fail = "error message";

            var result2 = fail.Match(
                Error: e => e == "error message"
            );

            Assert.True(result2);
        }

        [Fact]
        public void SameTypeMatchTest()
        {
            var ok = Result<string, string>.Ok("Ok result");

            var result = ok.Match(
                Ok: o => 1,
                Error: e => -1
            );

            Assert.Equal(1, result);

            var fail = Result<string, string>.Error("error");

            var result2 = fail.Match(
                Ok: o => 1,
                Error: e => -1
            );

            Assert.Equal(-1, result2);
        }

    }
}
