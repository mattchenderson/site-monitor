#r "Newtonsoft.Json"

using System;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;

public static void Run(HttpRequestMessage req, out TodoItem todoItem, TraceWriter log)
{
    log.Verbose($"Webhook was triggered!");

    string jsonContent = req.Content.ReadAsStringAsync().Result;
    dynamic data = JsonConvert.DeserializeObject(jsonContent);
    
    dynamic alert = data.context; 

    string message = "alert!";
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
