#r "Newtonsoft.Json"

using System;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;

public static void Run(HttpRequestMessage req, out TodoItem todoItem, TraceWriter log)//, out Notification pushNotification)
{
    log.Verbose($"Webhook was triggered!");

    string jsonContent = req.Content.ReadAsStringAsync().Result;
    dynamic data = JsonConvert.DeserializeObject(jsonContent);
    
    dynamic alert = data.context; 

    string message = string.Format( "{0} - Resource {1} experienced {2} errors over a {3} minute period", alert.timestamp, alert.resourceName, alert.condition.metricValue, alert.condition.windowSize);
    log.Verbose(message);
    
    todoItem = new TodoItem()
    {
        Id = Guid.NewGuid().ToString(),
        Text = message
    };
    
    return;
}

public class TodoItem {
    public string Id {get; set;}
    public string Text { get; set; }
}

public class Notification {
    public string Message { get; set; }
}