using Microsoft.Azure.NotificationHubs;
using Soccer.Common.Constants;
using System;
using System.Collections.Generic;

namespace Soccer.NotificationTest
{
    public class Program
    {
        private static NotificationHubClient _hub;

        public static void Main(string[] args)
        {
            _hub = NotificationHubClient.CreateClientFromConnectionString(
                AppConstants.ListenConnectionString,
                AppConstants.NotificationHubName);

            do
            {
                Console.WriteLine("Type a new message:");
                string message = Console.ReadLine();
                SendNotificationAsync(message);
                Console.WriteLine("The message was sent...");
            } while (true);
        }

        private static async void SendNotificationAsync(string message)
        {
            Dictionary<string, string> templateParameters = new Dictionary<string, string>();

            foreach (string tag in AppConstants.SubscriptionTags)
            {
                templateParameters["messageParam"] = message;
                try
                {
                    await _hub.SendTemplateNotificationAsync(templateParameters, tag);
                    Console.WriteLine($"Sent message to {tag} subscribers.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to send template notification: {ex.Message}");
                }
            }
        }
    }
}
