using System.Collections;
using Robotic.Domain.Entity;

namespace Robotic.Infra.Utils;

public class Converter
{
    public static Certifications CertificationsConverter(Dictionary<string, object> certificates)
    {
        var done = new List<Guid>();
        var inProgress = new List<Dictionary<Guid, int>>();
        var notStarted = new List<Guid>();
        
        foreach (var certificateType in certificates)
        {
            var item = certificates[certificateType.Key];

            switch (certificateType.Key)
            {
                case "done":
                    foreach (var data in certificateType.Value as IEnumerable<object>)
                    {
                        done.Add(Guid.Parse(data.ToString()));
                    }           
                    break;
                case "inProgress":
                    if (certificateType.Value is IEnumerable<object> dataCollection)
                    {
                        foreach (var data in dataCollection)
                        {
                            if (data is Dictionary<string, object> dictionaryData)
                            {
                                var result = new Dictionary<Guid, int>();

                                foreach (var keyValue in dictionaryData)
                                {
                                    string key = keyValue.Key;
                                    object value = keyValue.Value;

                                    result.Add(Guid.Parse(key), (int)(long)value);
                                }

                                inProgress.Add(result);
                            }
                        }
                    }
                    break;
                case "notStarted":
                    foreach (var data in certificateType.Value as IEnumerable<object>)
                    {
                        notStarted.Add(Guid.Parse(data.ToString()));
                    }           
                    break;
            }
        }

        var certifications = new Certifications(done, inProgress, notStarted);
        
        return certifications;
    }
}
