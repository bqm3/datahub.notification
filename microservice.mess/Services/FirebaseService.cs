using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;

namespace microservice.mess.Services
{
    public class FirebaseNotificationService
{
    public FirebaseNotificationService()
    {
        if (FirebaseApp.DefaultInstance == null)
        {
            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile("firebase-settings.json")
            });
        }
    }

    public async Task SendNotificationAsync(string deviceToken, string title, string body)
    {
        var message = new Message()
        {
            Token = deviceToken,
            Notification = new Notification
            {
                Title = title,
                Body = body
            }
        };

        string response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
        Console.WriteLine("Sent notification: " + response);
    }

    public async Task SendDataMessageAsync(string deviceToken, Dictionary<string, string> data)
    {
        var message = new Message()
        {
            Token = deviceToken,
            Data = data
        };

        string response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
        Console.WriteLine("Sent data message: " + response);
    }
}

}