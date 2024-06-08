using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Sirenix.OdinInspector;
public class GeminiClient
{
    private readonly HttpClient _client;

    public GeminiClient(string apiKey)
    {
        _client = new HttpClient();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
    }

    public async void GenerateText(string prompt, System.Action<string> Answer)
    {
        var baseUrl = "https://generativelanguage.googleapis.com/v1beta/models/gemini-1.0-pro:generateContent";

        var requestBody = new StringContent(
            $"{{\"contents\": [{{\"role\":\"\",\"parts\":[{{\"text\":\"{prompt}\"}}]}}],\"generationConfig\":{{\"temperature\":0.9,\"topK\":50,\"topP\":0.95,\"maxOutputTokens\":4096,\"stopSequences\":[]}}}}",
            Encoding.UTF8, "application/json");

        var response = await _client.PostAsync(baseUrl, requestBody);

        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            // Parse the JSON response to extract the generated text
            // This is implementation specific and depends on your chosen parsing library
            Answer.Invoke(responseContent);
        }
        else
        {
            throw new Exception($"Error making request: {response.StatusCode}");
        }
    }
}

public static class Gemini 
{
    [Button]
    public static void Request()
    {
        // Replace with your actual API key
        var apiKey = "AIzaSyCQlnDiwtBWCDLhgpqBhHmMf6Qp8nJcq3o";
        var client = new GeminiRequest();
        client.SendRequest("gemini-1.0-pro", "{\r\n  \"prompt\": \"Write a poem about a cat who loves to play fetch.\",\r\n  \"parameters\": {\r\n    \"temperature\": 0.7,\r\n    \"max_tokens\": 128\r\n  }\r\n}", (a) => Debug.Log(a));
        //var prompt = "Write a poem about a cat";
        //client.GenerateText(prompt, (a) => Debug.Log(a));
        
    }
}


public class GeminiRequest
{
    private string _apiKey => "AIzaSyDvUr9r-bYihh0GDRHJeeejWV-OERm4M_g";
    private string _baseUrl => "https://generativelanguage.googleapis.com/v1beta/models/gemini-pro";

    public async void SendRequest(string endpoint, string requestBody, System.Action<string> Finshed)
    {
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
            var url = $"{_baseUrl}/{endpoint}";
            var content = new StringContent(requestBody, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                string A = await response.Content.ReadAsStringAsync();
                Finshed.Invoke(A);
                
            }
            else
            {
                throw new Exception($"Error making request to Gemini API. Status code: {response.StatusCode}");
            }
        }
    }
}

