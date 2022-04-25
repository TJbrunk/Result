using csharp_utils;
using Xunit;

namespace ResultTests
{
    public class TestObject
    {
        public int IntProp { get; set; } = -1;
        public string StringProp { get; set; } = "Test obj string";
        public bool BoolProp { get; set; } = true;

        public TestObject(int i)
        {
            this.IntProp = i;
        }
    }

    public class ResultExtensionTests
    {
        [Fact]
        public void AndThenExtTest()
        {
            Result<int, string> fail = 1;
            Assert.False(fail.AndThen((ok) => ok > 10));

            Result<int, string> pass = 100;
            Assert.True(pass.AndThen((ok) => ok > 10));
        }

        [Fact]
        public void OrElseAndThenTest()
        {
            Result<int, string> fail = "err";
            Assert.True(fail.OrElse((err) => err == "err"));
            Assert.False(fail.AndThen((ok) => ok == 1));
        }

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

        [Fact]
        public void InvertedHandling()
        {
            var pass = SimulateErrorCall()
                .AndThen(r => new TestObject(0) { BoolProp = r.BoolProp });
            Assert.Null(pass);

            var fail = SimulateOkCall()
                .OrElse(r => r == "error");
            Assert.False(fail);
        }

        [Fact]
        public void Chaining()
        {
            var result = SimulateOkCall(0)
                .AndThen(r =>
                {
                    if (r.IntProp >= 1)
                    {
                        return Result<bool, string>.Ok(true);
                    }
                    else
                    {
                        return Result<bool, string>.Error("Int Prop is < 1");
                    }
                });
            Assert.True(result.IsError());
            Assert.Equal("Int Prop is < 1", result.ErrorValue);

            result = SimulateOkCall(10)
                .AndThen(r =>
                {
                    if (r.IntProp >= 1)
                    {
                        return Result<bool, string>.Ok(true);
                    }
                    else
                    {
                        return Result<bool, string>.Error("Int Prop is < 1");
                    }
                });
            Assert.True(result.OkValue);
            Assert.True(result.IsOk());

            result = SimulateOkCall(10)
                .AndThen(r => AndThenMethod(r));
            Assert.Null(result);
        }

        private Result<TestObject, string> SimulateOkCall(int intPropValue = -1)
        {
            return Result<TestObject, string>.Ok(new TestObject(intPropValue));
        }
        private Result<TestObject, string> SimulateErrorCall()
        {
            Result<TestObject, string> r = "An error occured";
            return r;
        }
        private Result<bool, string> AndThenMethod(TestObject testObject)
        {
            return null;
        }
    }
}
