using Robotic.Domain.Entity;

namespace Robotic.Infra.Utils;

public static class DataUtils
{
    public static Dictionary<string, object> FormatDataToDb<T>(T data, string[] propToIgnore = null)
    {
        try
        {
            propToIgnore = propToIgnore ?? Array.Empty<string>();
            propToIgnore.Select(prop => prop.ToLower());
            
            Dictionary<string, object> obj = new Dictionary<string, object>();

            foreach (var prop in data.GetType().GetProperties())
            {
                var propName = FormatKey(prop.Name);

                if (propToIgnore.Contains(propName.ToLower()))
                    continue;

                var propValue = prop.GetValue(data, null);

                if (propValue is Guid)
                {
                    propValue = propValue.ToString();
                }
                else if (propName == "modifiedOn" && propValue is DateTime)
                {
                    propValue = DataUtils.UpdateTime();
                }
                else if (propValue is Array)
                {
                    var propValueArray = propValue as IEnumerable<Guid>;
                    propValue = propValueArray.Select(prop => prop.ToString());
                }
                else if (propValue is Certifications)
                {
                    propValue = new Dictionary<string, object>
                    {
                        ["done"] = new List<Guid>(),
                        ["inProgress"] = new List<KeyValuePair<Guid, int>>(),
                        ["notStarted"] = new List<Guid>(),
                    };
                }
                
                obj.Add(propName, propValue);
            }

            return obj;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }        
    }

    public static DateTime UpdateTime()
    {
        var year = DateTime.Today.Year;
        var month = DateTime.Today.Month;
        var day = DateTime.Today.Day;
        var hour = DateTime.Now.Hour;
        var minutes = DateTime.Now.Minute;
        var seconds = DateTime.Now.Second;
        
        return new DateTime(year, month, day, hour, minutes, seconds).ToUniversalTime(); 
    }
    
    private static string FormatKey(string key)
    {
        return char.ToLower(key[0]) + key.Substring(1);
    }
}