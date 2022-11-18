using System.Net;
using System.Text.Json;
using System.Text;
using System.Net.Http.Json;
using System.Net.Http;
using System.Collections.Specialized;
using System.Reflection.Metadata.Ecma335;
using System.IO;
using System;
using Microsoft.VisualBasic;
using System.Text.Json.Nodes;

Console.Title = "Webhook Spammer";

int count = 0; // Makes A Variable For Counting, Can Be Used In Any Function

main(); // Calls Our Main Function

void main()
{
    Console.Clear();
    Console.WriteLine("1: Webhook Spam\n2: Webhook Check\n3: Get Webhook Information\n4: Delete Webhook");
    var c1 = Console.ReadLine();
    Console.Clear();
    Console.Write("Url: ");
    var url = Console.ReadLine();
    Console.Clear();

    switch (c1) // Gets The Choice Ready To Be Checked For Certain Values
    {
        case "1": // If It Is Equal To "1", The Code Below Will Run
            Console.Write("Name: ");
            var name = Console.ReadLine();
            Console.Write("Avatar Url (Optional): ");
            var avatarUrl = Console.ReadLine();
            Console.Clear();
            Console.WriteLine("1: Spam Fake IP's\n2: Spam Random 2,000 Character Messages\n3: Custom Spam");
            var c2 = Console.ReadLine();
            switch (c2) // Same Thing As Above To Prepare
            {
                case "1": // Same Checking Just Different Expression
                    Console.Clear();
                    sendRequest(url, name, avatarUrl, "RIP_929s8999298283992", ""); // Calls Our Post Request Function While Passing Through The Values Needed
                    Console.Clear();
                    break;
                case "2":
                    Console.Clear();
                    Console.WriteLine("1: 2k Random Characters\n2: 2k Empty Characters");
                    var c3 = Console.ReadLine();
                    switch (c3)
                    {
                        case "1":
                            sendRequest(url, name, avatarUrl, "2kS_929s8999298283992", ""); // Calls Our Post Request Function While Passing Through The Values Needed
                            break;
                        case "2":
                            sendRequest(url, name, avatarUrl, "2kN_929s8999298283992", ""); // Calls Our Post Request Function While Passing Through The Values Needed
                            break;
                        default:
                            break;
                    }
                    Console.Clear();
                    break;
                case "3":
                    Console.Clear();
                    Console.Write("Embeded (Y\\n): ");
                    var c4 = Console.ReadLine();
                    switch (c4)
                    {
                        case "Y":
                            Console.WriteLine("Format Required: {\"username\": \"webhook username\",\"content\": null,\"embeds\": [{\"title\": \"This is the embed title\",\"description\": \"This is the embed description\",\"color\": 4062976}],\"attachments\": []}");
                            Console.Write("\nPlease Enter Your Embed: ");
                            var payload = Console.ReadLine();
                            sendRequest(url, "booo boo", "sooo", "booo", payload); // Calls Our Post Request Function While Passing Through The Values Needed
                            Console.Clear();
                            break;
                        case "n":
                            Console.Write("Message: ");
                            var content = Console.ReadLine();
                            sendRequest(url, name, avatarUrl, content, ""); // Calls Our Post Request Function While Passing Through The Values Needed
                            Console.Clear();
                            break;
                        default:
                            Console.WriteLine("Invalid Choice, Press Enter To Return To Main Menu...");
                            Console.ReadLine();
                            Console.Clear();
                            main();
                            break;
                    }
                    break;
            }
            break;
        case "2": // If It Is Equal To "2", The Code Below Will Run
            checkWebhook(url.ToString()); // Calls Our Check Request Function While Passing Through The Values Needed
            break;
        case "3": // If It Is Equal To "3", The Code Below Will Run
            getWebhookInfo(url.ToString()); // Calls Our Webhook Information Function While Passing Through The Values Needed
            break;
        case "4": // If It Is Equal To "4", The Code Below Will Run
            deleteWebhook(url); // Calls Our Delete Webhook Function While Passing Through The Values Needed
            break;
        default: // If It Is Not Equal To The Values We Check, The Code Will Run Below
            Console.WriteLine("Invalid Choice, Press Enter To Return To Main Menu...");
            Console.ReadLine();
            Console.Clear();
            main();
            break;
    }
}

string generateRandomString(string choice) // Function Allows Us To Create A Random String (The String Depends On Your Choice Passed Through)
{
    StringBuilder str_build = new StringBuilder();
    Random random = new Random();

    switch (choice) // Same Thing, Preparing The Expression To Be Read For Certain Values
    {
        case "N":
            for (int i = 0; i < 2000; i++)
            {
                str_build.Append("⠀"); // Appends blank character to a string.
            }
            return str_build.ToString(); // Returns the blank characters string.
            break;
        default:
            const string characters = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz0123456789!@$?_-."; // Includes A List Of All Characters We Will Use
            char[] chars = new char[2000]; // Our Unicode List (2000 Length)

            for (int i = 0; i < 2000; i++) // Starts A Loop Until i = 2000
            {
                chars[i] = characters[random.Next(0, characters.Length)]; // Sets The List's Index To A Random Character Starting From The First Character Of Our Custom List To The Last One
            }
            return new string(chars); // Returns String
            break;
    }
}

string generateRandomIP()
{
    Random random = new Random();
    int num2 = random.Next(30, 200); // Generate Random Number
    Random random2 = new Random();
    int num3 = random.Next(30, 200);
    Random random3 = new Random();
    int num4 = random.Next(30, 200);
    Random random4 = new Random();
    int num5 = random.Next(30, 200);
    string ip = string.Concat(new string[] // Appends Numbers Together
    {
                    num2.ToString(),
                    ".",
                    num3.ToString(),
                    ".",
                    num4.ToString(),
                    ".",
                    num5.ToString()
    });
    return ip; // Returns IP
}

void sendRequest(string urlS, string nameS, string avatarUrlS, string contentS, string customPS)
{
    if (string.IsNullOrEmpty(nameS))
    {
        Console.WriteLine("Null Names Are Not Allowed...\nPress Enter To Return To The Main Menu...");
        Console.ReadLine();
        main();
        return;
    }
    bool continueSpam = true;
    try
    {
        while (continueSpam == true)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(urlS.ToString()); // Creates A Web Request
            httpWebRequest.ContentType = "application/json"; // Sets The Content Type To Json
            httpWebRequest.Method = "POST"; // Sets Method To POST Method

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string payload = "{\"username\":\"" + nameS.ToString() + "\"," +
                                  "\"content\":\"" + contentS.ToString() + "\"," + "\"avatar_url\":\"" + avatarUrlS.ToString() + "\"}"; // Our Json Payload We Will Send Through Our WebRequest
                if (string.IsNullOrEmpty(avatarUrlS)) { payload = payload.Replace(",\"avatar_url\":\"\"", null); } // Checks For Null Avatar String And Replaces The Avatar URL If It Is Null
                if (contentS.ToString() == "2kS_929s8999298283992") { payload = payload.Replace(contentS.ToString(), generateRandomString("")); } // Checks For Random String And Replaces It With A Random String
                if (contentS.ToString() == "2kN_929s8999298283992") { payload = payload.Replace(contentS.ToString(), generateRandomString("N")); } // Checks For Blank Character String And Replaces It With A Long Blank Character String
                if (contentS.ToString() == "RIP_929s8999298283992") { payload = payload.Replace(contentS.ToString(), generateRandomIP()); } // Checks For IP String And Replaces It With A Random IP
                if (!string.IsNullOrEmpty(customPS))
                {
                    payload = customPS.ToString(); // If Custom Payload Isn't Null It Will Set Our Paylod To The One Passed Through The Function
                }

                streamWriter.Write(payload.ToString()); // Writes Json To The Stream
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            
            Console.WriteLine((int)httpResponse.StatusCode);

            if ((int)httpResponse.StatusCode == 204)
            {
                count += 1;
                Console.WriteLine("Spammed Webhook (" + count.ToString() + ") Times");
            }
            Thread.Sleep(1000);
        }
    }
    catch (Exception e)
    {
        switch (e.Message)
        {
            case "The remote server returned an error: (404) Not Found.":
                continueSpam = false;
                Console.WriteLine("They Deleted Their Webhook...");
                Console.ReadLine();
                break;
            case "The remote server returned an error: (400) Bad Request.":
                Thread.Sleep(1000);
                break;
            case "The remote server returned an error: (429) Too Many Requests.":
                Console.WriteLine("To Many Requests, Waiting 15 Seconds...");
                Thread.Sleep(15000);
                sendRequest(urlS, nameS, avatarUrlS, contentS, customPS);
                break;
            default:
                Console.WriteLine(e.Message);
                Console.Read();
                break;
        }
    }
}

async void checkWebhook(string urlS)
{
    try
    {
        // Practically Same Process As Spaming The Webhook But With Nothing In The Payload
        var httpWebRequest = (HttpWebRequest)WebRequest.Create(urlS.ToString());
        httpWebRequest.ContentType = "application/json";
        httpWebRequest.Method = "POST";

        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        {
            string json = "{}";

            streamWriter.Write(json);
        }
        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        Console.WriteLine((int)httpResponse.StatusCode);
        if ((int)httpResponse.StatusCode == 204)
        {
            Console.WriteLine("Valid Webhook, Press Enter To Return...");
            Console.ReadLine();
            main();
        }
        else
        {
            Console.WriteLine("Invalid Webhook, Press Enter To Return...");
            Console.ReadLine();
            main();
        }
    }
    catch (Exception e)
    {
        if (e.Message == "The remote server returned an error: (404) Not Found.")
        {
            Console.WriteLine("Invalid Webhook, Press Enter To Return...");
            Console.ReadLine();
            main();
        }
        else if(e.Message == "The remote server returned an error: (400) Bad Request.")
        {
            Console.WriteLine("Valid Webhook, Press Enter To Return...");
            Console.ReadLine();
            main();
        }
        else
        {
            Console.WriteLine("Error: " + e.Message);
            Console.Read();
        }
    }
}

void getWebhookInfo(string urlS)
{
    try
    {
        // Essentially The Same As Spamming The Webhook, Just Using GET Method Instead And Reading The Response
        string resp = "";
        var httpWebRequest = (HttpWebRequest)WebRequest.Create(urlS.ToString());
        httpWebRequest.ContentType = "application/json";
        httpWebRequest.Method = "GET";

        using (HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse())
        {
            using (var streamResponse = httpResponse.GetResponseStream())
            {
                {
                    using (var streamReader = new StreamReader(streamResponse))
                    {
                        resp = streamReader.ReadToEnd();
                    }
                }
            }
        }

        JsonNode dresp = JsonNode.Parse(resp); // Parsing The JSON Response

        Console.WriteLine("[[ Webhook Information ]]\n\n");
        Console.WriteLine("Name: " + dresp["name"].ToString() + "\n");
        if (dresp["avatar"] == null) { Console.WriteLine("Avatar: None\n"); } else { Console.WriteLine("Avatar: " + dresp["avatar"].ToString() + "\n"); }
        Console.WriteLine("Id: " + dresp["id"].ToString() + "\n");
        Console.WriteLine("Guild Id: " + dresp["guild_id"].ToString() + "\n\n");
        Console.WriteLine("[[ End Of Webhook Information ]]\n\n");

        Console.WriteLine("Press Enter To Go Back To The Main Menu...");
        Console.ReadLine();
        main();
    }
    catch (Exception e)
    {
        if (e.Message == "The remote server returned an error: (404) Not Found.")
        {
            Console.WriteLine("Invalid Webhook, Press Enter To Return...");
            Console.ReadLine();
            main();
        }
        else if (e.Message == "The remote server returned an error: (400) Bad Request.")
        {
            Console.WriteLine("Valid Webhook, Press Enter To Return...");
            Console.ReadLine();
            main();
        }
        else
        {
            Console.WriteLine("Error: " + e.Message);
            Console.Read();
        }
    }
}

void deleteWebhook(string urlS)
{
    try
    {
        // Same Method As Spamming Just Using DELETE Method To Delete The Webhook
        var httpWebRequest = (HttpWebRequest)WebRequest.Create(urlS.ToString());
        httpWebRequest.Method = "DELETE";
        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

        switch ((int)httpResponse.StatusCode)
        {
            case 204:
                Console.WriteLine("Deleted The Webhook, Press Enter To Return...");
                Console.ReadLine();
                main();
                break;
            default:
                Console.WriteLine("Something Went Wrong, Press Enter To Return...");
                Console.ReadLine();
                main();
                break;
        }
    }
    catch (Exception e)
    {
        if (e.Message == "The remote server returned an error: (404) Not Found.")
        {
            Console.WriteLine("Invalid Webhook, Press Enter To Return...");
            Console.ReadLine();
            main();
        }
        else if (e.Message == "The remote server returned an error: (400) Bad Request.")
        {
            Console.WriteLine("Valid Webhook, Press Enter To Return...");
            Console.ReadLine();
            main();
        }
        else
        {
            Console.WriteLine("Error: " + e.Message);
            Console.Read();
        }
    }
}
