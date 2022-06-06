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

    public class ResultAndThenExtensionTests
    {
        [Theory]
        [InlineData(1, 10, 0)]
        [InlineData(11, 10, 100)]
        public void AndThenFuncExtTest(int testValue, int compareValue, int expected)
        {
            var result = Result<int, int>.Ok(testValue);
            var r2 =  result.AndThen((ok) => {
                if( ok > compareValue)
                {
                    return Result<int, int>.Ok(expected);
                }
                else
                {
                    return Result<int, int>.Error(default(int));
                }
            });
            Assert.Equal(expected, r2.OkValue);

        }

        [Theory]
        [InlineData(1)]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(int.MaxValue)]
        [InlineData(int.MinValue)]
        public void ImplicitContructorTest(int testValue)
        {
            Result<int, string> result = testValue;
            Assert.True(result.IsOk());
            Assert.False(result.IsError());
            Assert.Null(result.ErrorValue);
            Assert.Equal(testValue, result.OkValue);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(int.MaxValue)]
        [InlineData(int.MinValue)]
        public void ExplicitContructorTest(int testValue)
        {
            var result = Result<int, string>.Ok(testValue);
            Assert.True(result.IsOk());
            Assert.False(result.IsError());
            Assert.Null(result.ErrorValue);
            Assert.Equal(testValue, result.OkValue);
        }

        /// <summary>
        /// Check that AndThen isn't called on TError
        /// </summary>
        [Theory]
        [InlineData("pass")]
        public void InvertedHandling(string passString)
        {
            var result = Result<bool, string>.Error(passString);
            Assert.True(result.IsError());
            Assert.False(result.IsOk());

            var andThenCalled = false;
            result.AndThen(r => andThenCalled = true);
            Assert.False(andThenCalled);
        }

        [Theory]
        [InlineData(1, 2, int.MinValue, 3)]
        [InlineData(11, 12, int.MaxValue, -1)]
        public void Chaining(int firstValue, int secondValue, int checkValue, int expected)
        {
            var result = Result<int, bool>.Ok(firstValue);

            var x = result.AndThen(r => {
                if (r >= checkValue)
                {
                    return secondValue;
                }
                else
                {
                    return false;
                }
            })
            .AndThen(r => {
                if(r >= checkValue)
                {
                    return expected;
                }
                else
                {
                    return false;
                }
            });

            if(expected > 0)
            {
                Assert.True(x.IsOk());
                Assert.False(x.ErrorValue);
                Assert.Equal(expected, x.OkValue);
            }
            else
            {
                Assert.True(x.IsError());
                Assert.False(x.ErrorValue);
                Assert.Equal(default(int), x.OkValue);
            }

        }

        [Fact]
        public void AndThenActionTest()
        {
            var result = Result<bool, string>.Ok(true);

            var boolActionExecuted = false;
            void ActionCallBack(Result<bool, string> result)
            {
                boolActionExecuted = true;
            }

            result.AndThen(r => ActionCallBack(r));

            Assert.True(boolActionExecuted);
        }
    }
}
