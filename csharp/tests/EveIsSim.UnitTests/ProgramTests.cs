using EveIsSim.NvimDapTestEx;

namespace EveIsSim.UnitTests;

public class ProgramTests
{
    [Fact]
    public void TestAdd_Success()
    {
        Assert.Equal(5, Utils.Add(2, 3));
    }

    [Fact]
    public void TestAdd_Fail()
    {
        Assert.Equal(50, Utils.Add(2, 3));
    }
}

