using System;
using Xunit;
using CodeGenerator;

namespace CodeGeneratorTest
{
    public class PocTest
    {
        [Fact]
        public void Generator_Stub_ReturnSomeCode()
        {
            var code = Generator.ReturnStubCode();


            Assert.True(code.Length > 100, "Stub Didn't return long enough test");
        }
    }
}
