using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class GPTAI : MonoBehaviour
{


    public static string DoShrift(string Text)
    {
        char[] CharArrey = Text.ToCharArray();
        string CharNumbered = "";
        for (int i = 0; i < CharArrey.Length; i++)
        {
            int NC = (int)CharArrey[i];
            string NIW = "";
            if (NC < 10)
                NIW = NC + "**";
            else if (NC < 100)
                NIW = NC + "*";
            else NIW = NC.ToString();
            CharNumbered += NIW;   
        }
        return CharNumbered;

    }

    public static string apiKey { 
        get 
        {
            /*
            char[] CharArrey = apiKey.ToCharArray();
            string CharNumbered = "";
            for (int i = 0; i < CharArrey.Length; i++) 
            {
                int NC = (int)CharArrey[i];
                string NIW = "";
                if (NC < 10)
                    NIW = NC + "**";
                else if (NC < 100)
                    NIW = NC + "*";
                else NIW = NC.ToString();
                CharNumbered += NIW;
            }*/
            //115*107*45*118*55*111*52*113*111*48*72*101*55*72*71*76*119*100*105*113*89*111*73*84*51*66*108*98*107*70*74*52*113*112*50*111*108*70*118*86*121*112*119*117*122*78*121*100*90*66*117
            //11510745*11855*11152*11311148*72*10155*72*71*76*11910010511389*11173*84*51*66*10898*10770*74*52*11311250*11110870*11886*12111211911712278*12110090*66*117

            //string SikretKey = "11510745*11855*11152*11311148*72*10155*72*71*76*11910010511389*11173*84*51*66*10898*10770*74*52*11311250*11110870*11886*12111211911712278*12110090*66*117"; my Old key
            string SikretKey = "11510745*11482*89*69*57*53*11281*80*11974*78*10051*66*72*97*75*77*10484*51*66*10898*10770*74*77*50*50*10356*57*87*10712157*10512057*82*11272*11380*11598*"; // bro's key
            string AAKKK = "";
            while (SikretKey.Length > 0) 
            {
                string NCH= (SikretKey[0].ToString() + SikretKey[1] + SikretKey[2]);
                NCH = NCH.Replace("*", "");
                AAKKK += (char)int.Parse(NCH);
                SikretKey = SikretKey.Remove(0, 3);
            }


            
            return AAKKK;
        } 
    } 
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
        AICommand.OpenAIUtil.InvokeChat(WrapPrompt(), (a) =>
        {
            for (int i = 1; i < 20; i++) a = a.Replace(i + ". ", "");
            a = a.Replace("- ", "|").Replace(", ", "|").Replace("\n", "|");
            a = a.Replace("||||", "|").Replace("|||", "|").Replace("||", "|");
            if (a[0] == '|') a = a.Remove(0, 1);
            a.Split("|").ToList().ForEach(d => Debug.Log(d));
            Resolt?.Invoke(a.Split("|").ToList());
        }, () => { OnFailed?.Invoke(); Debug.Log("unseccsese"); });
        string WrapPrompt()
          => $"give definition of {Word}.\n" +
            " - I only need the sentences. Don't add any explanation.\n";
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
            a = a.Replace("- ", "|").Replace(", ", "|").Replace("\n", "|");
            a = a.Replace("||||", "|").Replace("|||", "|").Replace("||", "|");
            if(a[0] == '|') a = a.Remove(0, 1);
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
            Debug.Log(prompt);
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
