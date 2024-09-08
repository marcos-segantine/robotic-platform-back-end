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
                var propName = prop.Name;

                if (propToIgnore.Contains(propName.ToLower()))
                {
                    continue;
                }
                
                var propValue = prop.GetValue(data, null);

                if (propValue is Guid)
                {
                    propValue = propValue.ToString();
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
}