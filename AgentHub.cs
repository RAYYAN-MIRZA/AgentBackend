using Microsoft.AspNetCore.SignalR;

namespace AgentBackend
{
    public class AgentHub: Hub
    {

        public AgentHub() {
        }
        public async Task SendScanResult(string payloadJson)
        {            
            Console.WriteLine($"✅ Received payload: {payloadJson}");

            await Clients.All.SendAsync("Recievec Scan Update", payloadJson);
        }
    }
}
