using csharp_utils;
using csharp_utils.Result;
using Xunit;

namespace ResultTests
{

    public class ErrorResultTests
    {
        [Fact]
        public void ConstructorTest()
        {
            var ok = ErrorResult<int>.Ok();
            Assert.True(ok.IsOk());
            Assert.False(ok.IsError());

            var error = ErrorResult<string>.Error("Forced error");
            Assert.False(error.IsOk());
            Assert.True(error.IsError());

        }

        [Theory]
        [InlineData(1)]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(int.MaxValue)]
        [InlineData(int.MinValue)]
        public void ImplicitContructorTest(int testValue)
        {
            //ErrorResult< string> result = testValue;
            //Assert.True(result.IsOk());
            //Assert.False(result.IsError());
            //Assert.Null(result.ErrorValue);
            //Assert.Equal(testValue, result.OkValue);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(int.MaxValue)]
        [InlineData(int.MinValue)]
        public void ExplicitContructorTest(int testValue)
        {
            //var result = ErrorResult<int, string>.Ok(testValue);
            //Assert.True(result.IsOk());
            //Assert.False(result.IsError());
            //Assert.Null(result.ErrorValue);
            //Assert.Equal(testValue, result.OkValue);
        }

        /// <summary>
        /// Check that AndThen isn't called on TError
        /// </summary>
        [Theory]
        [InlineData("pass")]
        public void InvertedHandling(string passString)
        {
            //var result = ErrorResult<bool, string>.Error(passString);
            //Assert.True(result.IsError());
            //Assert.False(result.IsOk());

            //var andThenCalled = false;
            //result.AndThen(r => andThenCalled = true);
            //Assert.False(andThenCalled);
        }

        [Theory]
        [InlineData(1, 2, int.MinValue, 3)]
        [InlineData(11, 12, int.MaxValue, -1)]
        public void Chaining(int firstValue, int secondValue, int checkValue, int expected)
        {
            //var result = ErrorResult<int, bool>.Ok(firstValue);

            //var x = result.AndThen(r => {
            //    if (r >= checkValue)
            //    {
            //        return secondValue;
            //    }
            //    else
            //    {
            //        return false;
            //    }
            //})
            //.AndThen(r => {
            //    if(r >= checkValue)
            //    {
            //        return expected;
            //    }
            //    else
            //    {
            //        return false;
            //    }
            //});

            //if(expected > 0)
            //{
            //    Assert.True(x.IsOk());
            //    Assert.False(x.ErrorValue);
            //    Assert.Equal(expected, x.OkValue);
            //}
            //else
            //{
            //    Assert.True(x.IsError());
            //    Assert.False(x.ErrorValue);
            //    Assert.Equal(default(int), x.OkValue);
            //}

        }

        [Fact]
        public void AndThenActionTest()
        {
            //var result = ErrorResult<null, string>.Ok(true);

            //var boolActionExecuted = false;
            //void ActionCallBack(ErrorResult<bool, string> result)
            //{
            //    boolActionExecuted = true;
            //}

            //result.AndThen(r => ActionCallBack(r));

            //Assert.True(boolActionExecuted);
        }
    }
}
