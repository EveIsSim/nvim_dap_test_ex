using EveIsSim.NvimDapTestEx.Models;

namespace EveIsSim.NvimDapTestEx;

public static class Utils {

    public static int Add(int a, int b) => a + b;

    public static Obj[] GetObjArr(int n)
    => Enumerable.Range(0, n)
        .Select(x => new Obj($"Name_{x}", x + 1))
        .ToArray();
}

