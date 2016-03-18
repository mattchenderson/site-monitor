#r "Newtonsoft.Json"

using System;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;

public static async Task<object> Run(HttpRequestMessage req, TraceWriter log)//, out T eventStore)//, out Notification pushNotification)
{
    log.Verbose($"Webhook was triggered!");

    string jsonContent = await req.Content.ReadAsStringAsync();
    dynamic data = JsonConvert.DeserializeObject(jsonContent);
    
    dynamic alert = data.context; 

    string message = string.Format( "{0} - Resource {1} experienced {2} errors over a {3} minute period", alert.timestamp, alert.resourceName, alert.condition.metricValue, alert.condition.windowSize);
    log.Verbose(message);

    return Task.FromResult<object>(null);
}
