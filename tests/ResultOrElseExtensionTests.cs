using csharp_utils;
using Xunit;

namespace ResultTests
{
    public class ResultOrElseExtensionTests
    {
        [Fact]
        public void InvertedHandling()
        {
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
