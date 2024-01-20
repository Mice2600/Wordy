using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class GPTAI : MonoBehaviour
{
    public static string apiKey => "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";
    [Button]
    public static void GenerateSentences(string Word, System.Action<List<string>> Resolt, System.Action OnFailed = null)
    {
        AICommand.OpenAIUtil.InvokeChat(WrapPrompt(), (a) =>
        {
            for (int i = 1; i < 20; i++) a = a.Replace(i + ". ", "");
            a = a.Replace("\n", "|");
            a.Split("|").ToList().ForEach(d => Debug.Log(d));
            Resolt?.Invoke(a.Split("|").ToList());
        }, () => { OnFailed?.Invoke(); Debug.Log("unseccsese"); });
        string WrapPrompt()
          => $"make several sentences with words {Word}.\n" +
            " - Avoid using numbers.\n" +
            " - I only need the sentences. Don't add any explanation.\n" +
            " - you should generate at least 10 sentences";
    }
    public static void GenerateDefenition(string Word, System.Action<List<string>> Resolt, System.Action OnFailed = null)
    {
    
    }
    public static void GenerateSinonim(string Word, System.Action<List<string>> Resolt, System.Action OnFailed = null) 
    {
        AICommand.OpenAIUtil.InvokeChat(WrapPrompt(), (a) =>
        {
            if (a.Contains("Couldn't find", System.StringComparison.OrdinalIgnoreCase)) 
            {
                OnFailed?.Invoke(); Debug.Log("unseccsese");
                return;
            }
            for (int i = 1; i < 20; i++) a = a.Replace(i + ". ", "");
            a = a.Replace("- ", "|");
            a = a.Replace(", ", "|");
            a = a.Replace("\n", "|");
            a.Split("|").ToList().ForEach(d => Debug.Log(d));
            Resolt?.Invoke(a.Split("|").ToList());
        }, () => { OnFailed?.Invoke(); Debug.Log("unseccsese"); });
        string WrapPrompt()
          => $"give the words synonyms for the word {Word}" + "\n" +
            " - the synonyms should be popular, easy to use and should not contain weak matches, " + "\n" +
            " - if there are no suitable matches or the word grammarly cannot have synonyms, just say \"Couldn't find\"" + "\n" +
            " - I only need the synonyms. Don't add any explanation.\n";
    }
    public static void GenerateAntonium(string Word, System.Action<List<string>> Resolt) { }

}

[System.Serializable]
public class TTSPayload
{
    public string model;
    public string input;
    public string voice;
    public string response_format;
    public float speed;
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

        public static async void InvokeChat(string prompt, System.Action<string> Resolt, System.Action OnField = null)
        {

            try
            {
                using var post = UnityWebRequest.Post
              (OpenAI.Api.Url, CreateChatRequestBody(prompt), "application/json");

                // Request timeout setting
                post.timeout = 20;

                // API key authorization
                post.SetRequestHeader("Authorization", "Bearer " + GPTAI.apiKey);

                // Request start
                var req = post.SendWebRequest();

                for (var progress = 0.0f; !req.isDone; progress += 0.01f)
                {
                    await Task.Delay(10);
                    if (progress > 30)
                    {
                        OnField?.Invoke();
                        return;
                    }
                }

                var json = post.downloadHandler.text;
                Debug.Log(post.downloadHandler.text);
                var data = JsonUtility.FromJson<OpenAI.Response>(json);
                Resolt?.Invoke(data.choices[0].message.content);
            }
            catch (System.Exception)
            {
                OnField?.Invoke();
            }

            // POST
            
            /*
            req.completed += (a) => Debug.Log("ss");
#if UNITY_EDITOR
            // Progress bar (Totally fake! Don't try this at home.)
            for (var progress = 0.0f; !req.isDone; progress += 0.01f)
            {
                UnityEditor.EditorUtility.DisplayProgressBar
                  ("AI Command", "Generating...", progress);
                System.Threading.Thread.Sleep(100);
                progress += 0.01f;
            }
            UnityEditor.EditorUtility.ClearProgressBar();

            var json = post.downloadHandler.text;
            Debug.Log(post.downloadHandler.text);
            var data = JsonUtility.FromJson<OpenAI.Response>(json);
            Resolt?.Invoke(data.choices[0].message.content);
#endif

            */
        }
    }

} // namespace AICommand
