using Base.Word;
using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using SystemBox.Engine;
using UnityEngine;
using UnityEngine.Networking;
namespace Traonsletor
{
    public static class Translator
    {
        public static IEnumerator Process(string targetLang, string sourceText)
        {
            string sourceLang = "auto";
            string url = "https://translate.googleapis.com/translate_a/single?client=gtx&sl="
                + sourceLang + "&tl=" + targetLang + "&dt=t&q=" + UnityWebRequest.EscapeURL(sourceText);

            using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
            {
                yield return webRequest.SendWebRequest();
                if (string.IsNullOrEmpty(webRequest.error))
                {
                    var N = JSONNode.Parse(webRequest.downloadHandler.text);
                    Debug.Log(N[0][0][0]);
                }
            }
        }

        // Exactly the same as above but allow the user to change from Auto, for when google get's all Jerk Butt-y
        public static IEnumerator Process(string sourceLang, string targetLang, string sourceText, System.Action<string> FineshedOne = null)
        {
            string url = "https://translate.googleapis.com/translate_a/single?client=gtx&sl="
                + sourceLang + "&tl=" + targetLang + "&dt=t&q=" + UnityWebRequest.EscapeURL(sourceText);
            
            using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
            {
                yield return webRequest.SendWebRequest();
                if (string.IsNullOrEmpty(webRequest.error))
                {
                    var N = JSONNode.Parse(webRequest.downloadHandler.text);
                    try
                    {
                        //Debug.Log(N[0][0][0]);
                        FineshedOne.Invoke(N[0][0][0]);
                    }
                    catch (System.Exception)
                    {
                    }
                }
            }
        }

        public static void GetRandomWord(System.Action<List<Word>> OnFinsh)
        {
            Engine.Get_Engine("Game").StartCoroutine(Work());
            IEnumerator Work()
            {
                yield return new WaitForEndOfFrame();


                WebClient client = new WebClient();
                string downloadedString = client.DownloadString("http://www.wordgenerator.net/application/p.php?id=dictionary_words&type=2&spaceflag=false");
                string[] randomWords = downloadedString.Split(',');

                List<Word> Firsts = new List<Word>();
                for (int i = 0; i < randomWords.Length - 1; i++)
                {
                    string All = randomWords[i];
                    int LastInn = All.LastIndexOf("</");
                    int LastInn22 = All.LastIndexOf("Definition:</b> ");
                    string Content = All.Substring(LastInn22 + ("Definition:</b> ").Length);
                    Content = Content.Remove(Content.Length - ("</p>").Length, ("</p>").Length);
                    string ContentTronsleted = "";
                    bool Stoop = true;
                    string English = randomWords[i].Split('<')[0];
                    string Tronsleted = "";
                    Engine.Get_Engine("Game").StartCoroutine(Process("en", "ru", English, (a) =>
                    {
                        Stoop = false;
                        Tronsleted = a;
                    }));
                    while (Stoop) { yield return null; }
                    Stoop = true;
                    Engine.Get_Engine("Game").StartCoroutine(Process("en", "ru", Content, (g) =>
                    {
                        ContentTronsleted = g;
                        Stoop = false;
                    }));
                    while (Stoop) { yield return null; }
                    Firsts.Add(new Word(English, Tronsleted, 0, true, Content, ContentTronsleted));
                    Debug.Log(English + "  Done");
                    ConsoleLog.Log(English);
                }
                OnFinsh.Invoke(Firsts);
            }
        }
        /*
        public void GenereteLevl(System.Action<List<(string En, string Ru)>> Resultat) 
        {

            StartCoroutine(Worddk());
            IEnumerator Worddk() 
            {
                string[] AllEn = GetRandomWord();
                List<(string En, string Ru)> Levle = new List<(string En, string Ru)>();
                for (int i = 0; i < AllEn.Length; i++)
                {
                    string d = AllEn[i];
                    bool wait = true;
                    StartCoroutine(Process("en", "ru", AllEn[i], (a) => { Levle.Add((d, a)); wait = false; }));
                    yield return new WaitUntil(() => wait);
                }
                Resultat.Invoke(Levle);
            }
        }*/


        /*
        IEnumerator StartDialogue(DialogueScript script)
        {
            TTS.PauseAll();
            Queue<DialogueEntry> dialogueQueue = script.GetEntryQueue();
            while (dialogueQueue.Count > 0)
            {
                DialogueEntry entry = dialogueQueue.Dequeue();
                string dialogueText = entry.text;
                TTSSpeaker speaker = gameObject.GetComponent<TTSSpeaker>();
                TTS.SayAsync(dialogueText, speaker);
                yield return new WaitUntil(() => speaker.audioSource.isPlaying);
                yield return new WaitUntil(() => !speaker.audioSource.isPlaying);
                yield return new WaitForSeconds(script.timeBetweenEntries);
            }
            TTS.ResumeAll();
        }
        */
    }

}