using System.Text.Json;

namespace EveIsSim.NvimDapTestEx;

public class Program 
{
    public static void Main()
    {
        Console.WriteLine("Start");

        var result = Utils.Add(2, 3);
        Console.WriteLine($"Add operation result: {result}");

        var objs = Utils.GetObjArr(result);

        var json = JsonSerializer.Serialize(objs, new JsonSerializerOptions{WriteIndented = true});
        Console.WriteLine($"Rand objs: {json}");

        Console.WriteLine("Stop");
    }
}
