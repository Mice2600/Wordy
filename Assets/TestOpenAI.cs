using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
//using Sirenix.OdinInspector;
using System;
using UnityEngine.Networking;
using UnityEditor;
using System.Reflection;
using AICommand;
using System.IO;
using System.Net.Http;


//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
public class TestOpenAI : MonoBehaviour
{
    public static string apiKey = "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";
    public static async void DDA() 
    {
        System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
        stopwatch.Start();

        // Call the function you want to measure
        await Main2();

        // Stop the stopwatch
        stopwatch.Stop();

        // Get the elapsed time and print it
        TimeSpan elapsedTime = stopwatch.Elapsed;
        Debug.Log($"Time spent: {elapsedTime.TotalMilliseconds} milliseconds");
        
        //Debug.Log( OpenAIUtil.InvokeChat("Hi im tryin OpenAi in unity firs time, how can you help me?"));
        /*string WrapPrompt(string input)
          => "Write a Unity Editor script.\n" +
             " - It provides its functionality as a menu item placed \"Edit\" > \"Do Task\".\n" +
             " - It doesn’t provide any editor window. It immediately does the task when the menu item is invoked.\n" +
             " - Don’t use GameObject.FindGameObjectsWithTag.\n" +
             " - There is no selected object. Find game objects manually.\n" +
             " - I only need the script body. Don’t add any explanation.\n" +
             "The task is described as follows:\n" + input;
        */
    }

    static async Task Main()
    {
        string apiKey = "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";
        string model = "tts-1";
        string voice = "alloy";
        string inputText = "Today is a wonderful day to build something people love!";
        string outputPath = "D\\speech.mp3";
        Debug.Log("ddd");
        using (HttpClient client = new HttpClient())
        {
            Debug.Log("22");
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

            string apiUrl = "https://api.openai.com/v1/audio/speech";

            // Construct the request payload
            var requestData = new
            {
                model,
                voice,
                input = inputText
            };

            // Convert the payload to JSON
            var jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(requestData);

            // Send the request
            var response = await client.PostAsync(apiUrl, new StringContent(jsonRequest, System.Text.Encoding.UTF8, 
                "application/json"));
            Debug.Log(response.IsSuccessStatusCode);
            // Check if the request was successful
            if (response.IsSuccessStatusCode)
            {
                
                // Read and save the response content (speech) to a file
                using (Stream stream = await response.Content.ReadAsStreamAsync()) 
                {
                    Debug.Log("2");
                    using (FileStream fs = new FileStream(outputPath, FileMode.Create))
                    {
                        Debug.Log("3");
                        await stream.CopyToAsync(fs);
                        Debug.Log($"Speech saved to {outputPath}");
                    }
                }
                
            }
            else
            {
                Debug.Log($"Error: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
            }
        }
    }

    static async Task Main2()
    {
        string apiKey = "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";
        string model = "tts-1";
        string voice = "alloy";
        string inputText = "Today is a wonderful day to build something people love!";
        string outputPath = @"D:\speech.mp3";  // Save in the D:\ directory

        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

            string apiUrl = "https://api.openai.com/v1/audio/speech";

            // Construct the request payload
            var requestData = new
            {
                model,
                voice,
                input = inputText
            };

            // Convert the payload to JSON
            var jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(requestData);

            // Send the request
            var response = await client.PostAsync(apiUrl, 
                new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json"));

            // Check if the request was successful
            if (response.IsSuccessStatusCode)
            {
                byte[] audioData = await response.Content.ReadAsByteArrayAsync();

                int frequency = 44100;  // Sample rate
                int channels = 1;      // Number of channels (1 for mono, 2 for stereo)
                bool stream = false;   // Set to true if you want to stream the audio

                AudioClip audioClip = AudioClip.Create("YourAudioClipName", audioData.Length / 2, channels, frequency, stream);


                float myFloat = System.BitConverter.ToSingle(mybyteArray, startIndex);



                float[] floatData = new float[audioData.Length / 2];
                for (int i = 0; i < floatData.Length; i++)
                {
                    short sample = BitConverter.ToInt16(audioData, i * 2);
                    floatData[i] = sample / 32768.0f;
                }
                audioClip.SetData(floatData, 0);


                // Use the audioClip as needed (e.g., play it)
                if (audioClip != null)
                {
                    AudioSource audioSource = new GameObject("DD").AddComponent<AudioSource>();
                    audioSource.clip = audioClip;
                    audioSource.Play();
                }

                // Save the response content (speech) as an MP3 file in D:\
                await File.WriteAllBytesAsync(outputPath, await response.Content.ReadAsByteArrayAsync());
                Debug.Log($"Speech saved to {outputPath}");
            }
            else
            {
                Debug.Log($"Error: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
            }
        }
    }


}
public static class WavUtility
{
    public static byte[] FromAudioClip(AudioClip audioClip, out AudioClip audioClipWav, bool downsample = true)
    {
        // Implementation for converting AudioClip to WAV
        // ...

        // Example: Convert audioClip to WAV and return the byte array
        audioClipWav = audioClip; // Replace this line with actual implementation
        return new byte[0]; // Replace this line with actual implementation
    }

    public static AudioClip ToAudioClip(byte[] wavData, int offset, int length, int samples, int frequency, bool isStereo)
    {
        // Implementation for converting WAV to AudioClip
        // ...

        // Example: Convert wavData to AudioClip and return it
        return AudioClip.Create("AudioClip", length, isStereo ? 2 : 1, frequency, false); // Replace this line with actual implementation
    }
}


namespace AICommand.OpenAI
{
    public static class Api
    {
        public const string Url = "https://api.openai.com/v1/chat/completions";
    }

    [System.Serializable]
    public struct ResponseMessage
    {
        public string role;
        public string content;
    }

    [System.Serializable]
    public struct ResponseChoice
    {
        public int index;
        public ResponseMessage message;
    }

    [System.Serializable]
    public struct Response
    {
        public string id;
        public ResponseChoice[] choices;
    }

    [System.Serializable]
    public struct RequestMessage
    {
        public string role;
        public string content;
    }

    [System.Serializable]
    public struct Request
    {
        public string model;
        public RequestMessage[] messages;
    }
}

namespace AICommand
{

    static class OpenAIUtil
    {
        static string CreateChatRequestBody(string prompt)
        {
            var msg = new OpenAI.RequestMessage();
            msg.role = "user";
            msg.content = prompt;

            var req = new OpenAI.Request();
            req.model = "gpt-3.5-turbo-1106";//"gpt-3.5-turbo";
            req.messages = new[] { msg };

            return JsonUtility.ToJson(req);
        }

        public static string InvokeChat(string prompt)
        {
            
            // POST
            using var post = UnityWebRequest.Post
              (OpenAI.Api.Url, CreateChatRequestBody(prompt), "application/json");

            // Request timeout setting
            post.timeout = 20;

            // API key authorization
            post.SetRequestHeader("Authorization", "Bearer " + TestOpenAI.apiKey);

            // Request start
            var req = post.SendWebRequest();

            // Progress bar (Totally fake! Don't try this at home.)
            for (var progress = 0.0f; !req.isDone; progress += 0.01f)
            {
                EditorUtility.DisplayProgressBar
                  ("AI Command", "Generating...", progress);
                System.Threading.Thread.Sleep(100);
                progress += 0.01f;
            }
            EditorUtility.ClearProgressBar();

            // Response extraction
            var json = post.downloadHandler.text;
            var data = JsonUtility.FromJson<OpenAI.Response>(json);
            return data.choices[0].message.content;
        }
    }

} // namespace AICommand