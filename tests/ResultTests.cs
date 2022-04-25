using csharp_utils;
using System;
using Xunit;

namespace ResultTests {

    public class ResultTests {

        [Fact]
        public void RetrieveOKResult() {
            Result<int, string> ok = 1;

            Assert.True(ok.IsOk());

            Assert.Equal(1, ok.OkValue);
        }

        [Fact]
        public void RetrieveOKResult2() {
            var e = new ArgumentNullException();
            var ok = Result<Exception, string>.Ok(e);

            Assert.True(ok.IsOk());

            Assert.NotNull(ok.OkValue);
            Assert.Null(ok.ErrorValue);
        }


        [Fact]
        public void RetrieveErrorResult() {
            Result<ResultTests, Exception> error = new ArgumentException("error msg");

            Assert.True(error.IsError());

            Assert.Equal("error msg", error?.ErrorValue?.Message);
            Assert.Null(error.OkValue);
            Assert.False(error.IsOk());
            Assert.True(error.IsError());
        }


        [Fact]
        public void ToStringTest() {
            Result<int, bool> ok = 1;
            var s = ok.ToString();
            Assert.Equal("Result.Ok<Int32>: 1", s);

            Result<int, bool> error = false;
            s = error.ToString();
            Assert.Equal("Result.Error<Boolean>: False", s);

        }

        [Fact]
        public void DefaultStringTest() {
            Result<int, bool> ok = 1;
            var s = ok.ToString();
            Assert.Equal("Result.Ok<Int32>: 1", s);

            Result<int, bool> error = true;
            s = error.ToString();
            Assert.Equal("Result.Error<Boolean>: True", s);
        }

        [Fact]
        public void OkConstructorTests() {
            var okObj = new OKObj {IntProp = 1, BoolProp = false};

            // Test implicit contructor
            Result<OKObj, ErrorObj> ok = okObj;
            Assert.NotNull(ok);
            Assert.True(ok.IsOk());
            Assert.False(ok.IsError());
            Assert.NotNull(ok.OkValue);
            Assert.Null(ok.ErrorValue);
            Assert.Equal(okObj.IntProp, ok.OkValue.IntProp);
            Assert.Equal(okObj.BoolProp, ok.OkValue.BoolProp);

            // Test explicit contructor
            var ok2 = Result<OKObj, ErrorObj>.Ok(okObj);
            Assert.NotNull(ok2);
            Assert.True(ok2.IsOk());
            Assert.False(ok2.IsError());
            Assert.NotNull(ok2.OkValue);
            Assert.Null(ok2.ErrorValue);
            Assert.Equal(okObj.IntProp, ok.OkValue.IntProp);
            Assert.Equal(okObj.BoolProp, ok.OkValue.BoolProp);
        }

        [Fact]
        public void ErrorConstructorTests() {
            var errorObj = new ErrorObj {BoolProp = true, Msg = "error details"};

            // Test implicit contructor
            Result<OKObj, ErrorObj> error = errorObj;
            Assert.NotNull(error);
            Assert.False(error.IsOk());
            Assert.True(error.IsError());
            Assert.Null(error.OkValue);
            Assert.NotNull(error.ErrorValue);
            Assert.Equal(errorObj.Msg, error.ErrorValue.Msg);
            Assert.Equal(errorObj.BoolProp, error.ErrorValue.BoolProp);

            // Test explicit contructor
            var error2 = Result<OKObj, ErrorObj>.Error(errorObj);
            Assert.NotNull(error2);
            Assert.False(error2.IsOk());
            Assert.True(error2.IsError());
            Assert.Null(error2.OkValue);
            Assert.NotNull(error2.ErrorValue);
            Assert.Equal(errorObj.Msg, error.ErrorValue.Msg);
            Assert.Equal(errorObj.BoolProp, error.ErrorValue.BoolProp);
        }

        [Fact]
        public void NullTests() {

            OKObj nullObj = null;
            var explicitOk = Result<OKObj, ErrorObj>.Ok(nullObj);
            Assert.NotNull(explicitOk);
            Assert.Null(explicitOk.OkValue);

            Result<OKObj, ErrorObj> implicitOk = nullObj;
            Assert.NotNull(implicitOk);



            ErrorObj nullErrorObj = null;
            var explicitError = Result<OKObj, ErrorObj>.Error(nullErrorObj);
            Assert.NotNull(explicitError);

            Result<OKObj, ErrorObj> implicitError = nullErrorObj;
            Assert.NotNull(implicitError);
        }

    }

    public class ErrorObj {
        public bool BoolProp { get; set; }
        public string Msg { get; set; }
    }

    public class OKObj {
        public int IntProp { get; set; }
        public bool BoolProp { get; set; }
    }
}
