#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using Sirenix.OdinInspector;
using static OpenAIWrapper;

public class OpenAIWrapper : MonoBehaviour
{
    [Button]
    public async void SynthesizeAndPlay(string text)
    {
        TTSModel model = TTSModel.TTS_1;
        TTSVoice voice = TTSVoice.Alloy;
        float speed = 1f;
        Debug.Log("Trying to synthesize " + text);
        byte[] audioData = await RequestTextToSpeech(text, model, voice, speed);
        if (audioData != null)
        {
            Debug.Log("Playing audio.");
            ProcessAudioBytes(audioData);

            void ProcessAudioBytes(byte[] audioData)
            {
                string filePath = Path.Combine(Application.persistentDataPath, "audio.mp3");
                File.WriteAllBytes(filePath, audioData);

                StartCoroutine(LoadAndPlayAudio(filePath));

                IEnumerator LoadAndPlayAudio(string filePath)
                {
                    using UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip("file://" + filePath, AudioType.MPEG);
                    yield return www.SendWebRequest();

                    if (www.result == UnityWebRequest.Result.Success)
                    {
                        AudioClip audioClip = DownloadHandlerAudioClip.GetContent(www);

                        AudioSource audioSource = new GameObject("DD").AddComponent<AudioSource>();
                        audioSource.clip = audioClip;
                        audioSource.Play();
                    }
                    else
                    {
                        Debug.LogError("Audio file loading error: " + www.error);
                    }
                    File.Delete(filePath);
                }

            }

        }
        else
        {
            Debug.LogError("Failed to get audio data from OpenAI.");
        }
    }

    public enum TTSModel
    {
        TTS_1,
        TTS_1_HD,
    }
    public enum TTSVoice
    {
        Alloy,
        Echo,
        Fable,
        Onyx,
        Nova,
        Shimmer
    }
    [SerializeField] private string openAIKey = "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";
    private TTSModel model = TTSModel.TTS_1_HD;
    private TTSVoice voice = TTSVoice.Nova;
    private float speed = 1f;
    private readonly string outputFormat = "mp3";

    public async Task<byte[]> RequestTextToSpeech(string text)
    {
        Debug.Log("Sending new request to OpenAI TTS.");
        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", openAIKey);

        TTSPayload payload = new TTSPayload
        {
            model = this.model.EnumToString(),
            input = text,
            voice = this.voice.ToString().ToLower(),
            response_format = this.outputFormat,
            speed = this.speed
        };

        string jsonPayload = JsonUtility.ToJson(payload);

        var httpResponse = await httpClient.PostAsync(
            "https://api.openai.com/v1/audio/speech",
            new StringContent(jsonPayload, Encoding.UTF8, "application/json"));

        byte[] response = await httpResponse.Content.ReadAsByteArrayAsync();

        if (httpResponse.IsSuccessStatusCode)
        {
            return response;
        }
        Debug.Log("Error: " + httpResponse.StatusCode);
        return null;
    }

    public async Task<byte[]> RequestTextToSpeech(string text, TTSModel model, TTSVoice voice, float speed)
    {
        this.model = model;
        this.voice = voice;
        this.speed = speed;
        return await RequestTextToSpeech(text);
    }
}
public static class TTSModelExtensions
{
    public static string EnumToString(this TTSModel model)
    {
        switch (model)
        {
            case TTSModel.TTS_1:
                return "tts-1";
            case TTSModel.TTS_1_HD:
                return "tts-1-hd";
            default:
                Debug.Log(model + " is not a valid TTSModel.");
                return "tts-1";
        }
    }
}
#endif