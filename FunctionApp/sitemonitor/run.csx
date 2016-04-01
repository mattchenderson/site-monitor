#r "Newtonsoft.Json"
#r "System.Runtime"
#r "System.Globalization"


using System;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Humanizer;

public static void Run(HttpRequestMessage req, out TodoItem todoItem, TraceWriter log)
{
    log.Verbose($"Webhook was triggered!");

    string jsonContent = req.Content.ReadAsStringAsync().Result;
    dynamic data = JsonConvert.DeserializeObject(jsonContent);
    
    dynamic alert = data.context; 
    DateTime timestamp = DateTime.Parse(alert.timestamp.ToString());
    string humanized = timestamp.Humanize(false);
    

    string message = string.Format( "{0} - Resource {1} experienced {2} errors over a {3} minute period", humanized, alert.resourceName, alert.condition.metricValue, alert.condition.windowSize);
    
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
