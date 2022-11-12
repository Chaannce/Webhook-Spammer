using System.Net;
using System.Text.Json;
using System.Text;
using System.Net.Http.Json;
using System.Net.Http;
using System.Collections.Specialized;
using System.Reflection.Metadata.Ecma335;

Console.Title = "Webhook Spammer";

int count = 0;

main();

void main()
{
    Console.Clear();
    Console.WriteLine("1: Webhook Spam\n2: Webhook Check");
    var c1 = Console.ReadLine();

    switch (c1)
    {
        case "1":
            Console.Clear();
            Console.WriteLine("1: Spam Fake IP's\n2: Spam Random 2,000 Character Messages\n3: Custom Spam");
            var c2 = Console.ReadLine();
            switch (c2)
            {
                case "1":
                    Console.Clear();
                    Console.Write("Url: ");
                    var url = Console.ReadLine();
                    Console.Write("Name: ");
                    var name = Console.ReadLine();
                    Console.Write("Avatar Url (Leave Blank If You Don't Need To Change It): ");
                    var avatarUrl = Console.ReadLine();
                    spamWebhookI(url, name, avatarUrl);
                    Console.Clear();
                    break;
                case "2":
                    Console.Clear();
                    Console.Write("Url: ");
                    var url1 = Console.ReadLine();
                    spamWebhookS(url1);
                    Console.Clear();
                    break;
                case "3":
                    Console.Clear();
                    Console.Write("Embeded (Y\\n): ");
                    var c3 = Console.ReadLine();
                    switch (c3)
                    {
                        case "Y":
                            Console.Write("Url: ");
                            var url3 = Console.ReadLine();
                            Console.WriteLine("Format Required: {\"username\": \"webhook username\",\"content\": null,\"embeds\": [{\"title\": \"This is the embed title\",\"description\": \"This is the embed description\",\"color\": 4062976}],\"attachments\": []}");
                            Console.Write("\nPlease Enter Your Embed: ");
                            var cEmb = Console.ReadLine();
                            spamWebhookCE(url3, cEmb);
                            Console.Clear();
                            break;
                        case "n":
                            Console.Write("Url: ");
                            var url4 = Console.ReadLine();
                            Console.Write("Name: ");
                            var name2 = Console.ReadLine();
                            Console.Write("Message: ");
                            var msg = Console.ReadLine();
                            spamWebhookC(url4, name2, msg);
                            Console.Clear();
                            break;
                        default:
                            Console.WriteLine("Invalid Choice, Returning In 3 Seconds...");
                            Thread.Sleep(3000);
                            Console.Clear();
                            main();
                            break;
                    }
                    break;
            }
            break;
        case "2":
            Console.Clear();
            Console.Write("Url: ");
            var url2 = Console.ReadLine();
            checkWebhook(url2.ToString());
            break;
        default:
            Console.WriteLine("Invalid Choice, Returning In 3 Seconds...");
            Thread.Sleep(3000);
            Console.Clear();
            main();
            break;
    }
}

string generateRandomString(int length)
{
    StringBuilder str_build = new StringBuilder();
    Random random = new Random();

    char letter;

    for (int i = 0; i < length; i++)
    {
        double flt = random.NextDouble();
        int shift = Convert.ToInt32(Math.Floor(25 * flt));
        letter = Convert.ToChar(shift + 65);
        str_build.Append(letter);
    }
    return str_build.ToString();
}

string generateRandomIP()
{
    Random random = new Random();
    int num2 = random.Next(30, 200);
    Random random2 = new Random();
    int num3 = random.Next(30, 200);
    Random random3 = new Random();
    int num4 = random.Next(30, 200);
    Random random4 = new Random();
    int num5 = random.Next(30, 200);
    string ip = string.Concat(new string[]
    {
                    num2.ToString(),
                    ".",
                    num3.ToString(),
                    ".",
                    num4.ToString(),
                    ".",
                    num5.ToString()
    });
    return ip;
}

async void spamWebhookI(string urlS, string nameS, string avatarUrlS)
{
    bool continueSpam = true;
    try
    {
        while (continueSpam == true)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(urlS.ToString());
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            if (!string.IsNullOrWhiteSpace(avatarUrlS))
            {
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = "{\"username\":\"" + nameS.ToString() + "\"," +
                              "\"content\":\"" + generateRandomIP() + "\"," + "\"avatar_url\":\"" + avatarUrlS.ToString() + "\"}";

                    streamWriter.Write(json);
                }
            }
            else
            {
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = "{\"username\":\"" + nameS.ToString() + "\"," +
                              "\"content\":\"" + generateRandomIP() + "\"}";

                    streamWriter.Write(json);
                }
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
                spamWebhookI(urlS, nameS, avatarUrlS);
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
            Console.WriteLine("Valid Webhook, Returning In 3 Seconds!");
            Thread.Sleep(3000);
            main();
        }
        else
        {
            Console.WriteLine("Invalid Webhook, Returning In 3 Seconds...");
            Thread.Sleep(3000);
            main();
        }
    }
    catch (Exception e)
    {
        if (e.Message == "The remote server returned an error: (404) Not Found.")
        {
            Console.WriteLine("Invalid Webhook, Returning In 3 Seconds...");
            Thread.Sleep(3000);
            main();
        }
        else if(e.Message == "The remote server returned an error: (400) Bad Request.")
        {
            Console.WriteLine("Valid Webhook, Returning In 3 Seconds!");
            Thread.Sleep(3000);
            main();
        }
        else
        {
            Console.WriteLine("Error: " + e.Message);
            Console.Read();
        }
    }
}

async void spamWebhookS(string urlS)
{
    bool continueSpam = true;
    try
    {
        while (continueSpam == true)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(urlS.ToString());
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{\"username\":\"Spamming\"," +
                          "\"content\":\"" + generateRandomString(2000) + "\"}";

                streamWriter.Write(json);
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
                spamWebhookS(urlS);
                break;
            default:
                Console.WriteLine(e.Message);
                Console.Read();
                break;
        }
    }
}

async void spamWebhookCE(string urlS, string payload)
{
    Console.WriteLine("asdasda");
    bool continueSpam = true;
    try
    {
        while (continueSpam == true)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(urlS.ToString());
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(payload);
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
                spamWebhookCE(urlS, payload);
                break;
            default:
                Console.WriteLine(e.Message);
                Console.Read();
                break;
        }
    }
}

async void spamWebhookC(string urlS, string nameS, string messageS)
{
    bool continueSpam = true;
    try
    {
        while (continueSpam == true)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(urlS.ToString());
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{\"username\":\"" + nameS.ToString() + "\"," +
                          "\"content\":\"" + messageS.ToString() + "\"}";

                streamWriter.Write(json);
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
                spamWebhookC(urlS, nameS, messageS);
                break;
            default:
                Console.WriteLine(e.Message);
                Console.Read();
                break;
        }
    }
}