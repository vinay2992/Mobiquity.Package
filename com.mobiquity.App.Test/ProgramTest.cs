using Xunit;

namespace com.mobiquity.App.Test
{
    public class ProgramTest
    {
        const string SUCCESS_SCENARIO = @"TestData\SuccessScenario.txt";

        [Fact]
        public void SucessSceanrioDatata()
        {
            Program.Main(new string[] { SUCCESS_SCENARIO });
            Assert.True(true);
        }
    }
}
