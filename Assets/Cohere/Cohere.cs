using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using UnityEngine;
using System.Linq;
using Study.Crossword;
using UnityEngine.SocialPlatforms.Impl;

public static class Cohere 
{



    static async void Chat()
    {
        var client = new HttpClient();

        string Message = "Hi, what is day today";
        string Model = "command";/*command command-light command-nightly command-light-nightly  https://docs.cohere.com/docs/models */
        string Json = "{" + $"\"message\":\"{Message}\",\"model\":\"{Model}\",\"stream\":true,\"preamble_override\":\"string\",\"conversation_id\":\"string\",\"prompt_truncation\":\"OFF\",\"search_queries_only\":true,\"documents\":[" + "{\"id\":\"string\",\"additionalProp\":\"string\"}],\"citation_quality\":\"fast\",\"temperature\":0.3}";

        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("https://api.cohere.ai/v1/chat"),
            Headers = { { "accept", "application/json" }, { "authorization", "Bearer Efu1dI7SD2mvEdz3bgcdjt7TjQC3yyhUT6fU89Yr" }, },
            Content = new StringContent(Json)
            {
                Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
            }
        };
        using (var response = await client.SendAsync(request))
        {
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            Debug.Log(body);
        }
    }
    public static async void Generate(string Word, System.Action<List<string>> Resolt)
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Resolt?.Invoke(new List<string>());
            Debug.Log("Error. Check internet connection!");
            return;
        }
        var sentences = new List<string>();
        try
        {
            string Message = $"make sentences with words {Word}";
            //string Message = "make sentences with words anal";
            string Model = "command-nightly";/*command command-light command-nightly command-light-nightly  https://docs.cohere.com/docs/models */
            string Json = "{\"max_tokens\":20,\"truncate\":\"END\",\"return_likelihoods\":\"NONE\",\"prompt\":\"" + Message + "\",\"model\":\"" + Model + "\",\"num_generations\":5,\"stream\":false,\"temperature\":1.75}";

            var client = new HttpClient();
            var request = new HttpRequestMessage
            {

                Method = HttpMethod.Post,
                RequestUri = new Uri("https://api.cohere.ai/v1/generate"),
                Headers = { { "accept", "application/json" }, { "authorization", "Bearer Efu1dI7SD2mvEdz3bgcdjt7TjQC3yyhUT6fU89Yr" }, },
                Content = new StringContent(Json)
                { Headers = { ContentType = new MediaTypeHeaderValue("application/json") } }
            };
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            string json = await response.Content.ReadAsStringAsync();
            
            var data = (JObject)JsonConvert.DeserializeObject(json);
            for (int i = 0; i < data["generations"].Count(); i++)
            {

                data["generations"][i]["text"].Value<string>().Split("\n").ToList().ForEach(x => {

                    if (!x.Contains($"\"{Word}\"", StringComparison.OrdinalIgnoreCase) &&
                    !x.Contains($"\'{Word}\'", StringComparison.OrdinalIgnoreCase) && !x.Contains($"the word {Word}", StringComparison.OrdinalIgnoreCase))
                    {
                        if (x.Length < 5) return;
                        x = x.Replace("-1. ", "").Replace("-2. ", "").Replace("-3. ", "").Replace("-4. ", "").Replace("-5. ", "").Replace("-6. ", "").Replace("-7. ", "");
                        x = x.Replace("1. ", "").Replace("2. ", "").Replace("3. ", "").Replace("4. ", "").Replace("5. ", "").Replace("6. ", "").Replace("7. ", "");
                        x = x.Replace("1 ", "").Replace("2 ", "").Replace("3 ", "").Replace("4 ", "").Replace("5 ", "").Replace("6 ", "").Replace("7 ", "");
                        x = x.Replace("- ", "");
                        x = x.Replace(" \"", "").Replace("\"", "");
                        if (x[0] == '-') { x = x.Remove(0, 1); }
                        sentences.Add(x);
                        Debug.Log(x);
                    }
                    //else Debug.LogError(x);
                });

                //Debug.Log(data["generations"][i]["text"]);
            }
        }
        catch (Exception X)
        {
            ConsoleLog.print(X.Message);
            Debug.LogException(X);
            
            //throw;
        }
        Resolt?.Invoke(sentences);

        //Debug.Log(body);

        //return new List<string> { Message };

    }
}

