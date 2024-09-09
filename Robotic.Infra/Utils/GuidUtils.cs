namespace Robotic.Infra.Utils;

public class GuidUtils
{
    public static string[] GuidToStringArray(Guid[] guidArray)
    {
        var arrayString = new List<string>();

        foreach (var guid in guidArray)
        {
            arrayString.Add(guid.ToString());
        }
        
        return arrayString.ToArray();
    }

    public static Guid[] StringToGuidArray(string[] stringArray)
    {
        var guidArray = new List<Guid>();

        foreach (var item in stringArray)
        {
            guidArray.Add(Guid.Parse(item));
        }
        
        return guidArray.ToArray();
    }
}

