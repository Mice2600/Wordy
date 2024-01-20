#if UNITY_EDITOR
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
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using static OpenAIWrapper;
using SystemBox;


//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
//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
public class TestOpenAI : MonoBehaviour
{



    public static string apiKey => "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";
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
             " - It doesn�t provide any editor window. It immediately does the task when the menu item is invoked.\n" +
             " - Don�t use GameObject.FindGameObjectsWithTag.\n" +
             " - There is no selected object. Find game objects manually.\n" +
             " - I only need the script body. Don�t add any explanation.\n" +
             "The task is described as follows:\n" + input;
        */
    }

    static async Task Main()
    {
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






                float[] samples = new float[audioData.Length / 4]; //size of a float is 4 bytes

                Buffer.BlockCopy(audioData, 0, samples, 0, audioData.Length);


                int frequency = 44100;  // Sample rate
                int channels = 1;      // Number of channels (1 for mono, 2 for stereo)
                bool stream = false;   // Set to true if you want to stream the audio

                AudioClip clip = AudioClip.Create("ClipName", samples.Length, channels, frequency, false);
                clip.SetData(samples, 0);





                // Use the audioClip as needed (e.g., play it)
                if (clip != null)
                {
                    AudioSource audioSource = new GameObject("DD").AddComponent<AudioSource>();
                    audioSource.clip = clip;
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


    public static void RequestTTS(string text)
    {
        FindObjectOfType<MonoBehaviour>().StartCoroutine(PostRequest(text));
    }

    static IEnumerator PostRequest(string text)
    {



        string jsonPayload = "{\"text\":\"" + text + "\"}";

        // UnityWebRequest request = UnityWebRequest.Post("https://api.openai.com/v1/tts", jsonPayload);
        string ttsEndpoint = "https://api.openai.com/v1/audio/speech";

        UnityWebRequest request = UnityWebRequest.PostWwwForm(ttsEndpoint, jsonPayload);
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", "Bearer " + apiKey);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            byte[] audioData = request.downloadHandler.data;

            // Play the audio
            AudioSource audioSource = new GameObject("DD").AddComponent<AudioSource>();
            audioSource.clip = WavUtility.ToAudioClip(audioData, 0, audioData.Length, 0, 44100, false);
            audioSource.Play();
        }
        else
        {
            Debug.LogError("TTS request failed: " + request.error);
        }
    }
}
public static class WavUtility
{
    public static AudioClip ToAudioClip(byte[] audioData, int offset, int length, int offsetSamples, int frequency, bool stereo)
    {
        if (audioData == null || audioData.Length == 0)
        {
            Debug.LogError("Audio data is null or empty.");
            return null;
        }

        // Convert 16-bit PCM to float
        float[] floatData = new float[length / 2];
        for (int i = 0; i < length / 2; i++)
        {
            floatData[i] = BitConverter.ToInt16(audioData, offset + i * 2) / 32768.0f;
        }

        // Create AudioClip
        AudioClip audioClip = AudioClip.Create("GeneratedAudio", length / 2, stereo ? 2 : 1, frequency, false);
        audioClip.SetData(floatData, 0);

        return audioClip;
    }

}



#endif