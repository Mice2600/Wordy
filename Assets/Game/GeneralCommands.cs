using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SystemBox.Engine;
using System.Linq;
using UnityEngine.SceneManagement;
using Servises.BaseList;
using Servises;
using Base.Dialog;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.CloudSave;
using Base.Word;
using System;
using System.Threading.Tasks;
using EnhancedScrollerDemos.FlickSnap;
using Sirenix.OdinInspector;
using SystemBox;
using Base.Synonym;
using Base.Antonym;

public class GeneralCommands : MonoBehaviour
{
    public string FirsSceneName;
    [Required, SerializeField]
    private TMPro.TMP_InputField UserName, password;
    private async void Awake()
    {

        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.Log("Error. Check internet connection!");
            return;
        }
        await UnityServices.InitializeAsync();
    }
    private async Task ListAllKeys()
    {
        try
        {
            var keys = await CloudSaveService.Instance.Data.RetrieveAllKeysAsync();

            Debug.Log($"Keys count: {keys.Count}\n" +
                $"Keys: {String.Join(", ", keys)}");
        }
        catch (CloudSaveValidationException e)
        {
            Debug.LogError(e);
        }
        catch (CloudSaveRateLimitedException e)
        {
            Debug.LogError(e);
        }
        catch (CloudSaveException e)
        {
            Debug.LogError(e);
        }
    }

    private void Start()
    {


        
        

        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.Log("Error. Check internet connection!");
            SceneManager.LoadScene(FirsSceneName, LoadSceneMode.Single);
            return;
        }

        DontDestroyOnLoad(gameObject);
        if (SystemBox.Data.Bool("UsernamePassword")) 
        {
            SignIn(SystemBox.Data.String("Username"), SystemBox.Data.String("Password"));
            Debug.Log((SystemBox.Data.String("Username"), SystemBox.Data.String("Password")));
        
        }
    }

    float SaveTime;
    private void Update()
    {
        if (!AuthenticationService.Instance.IsSignedIn) return;
        SaveTime += Time.deltaTime;
        if (SaveTime > 10) 
        {
            SaveTime = 0;
            Save();
        }
    }
    public async Task Save() 
    {

        

        List<string> Keys = new List<string>() { WordBase.Wordgs.DataID, DialogBase.Dialogs.DataID, IrregularBase.Irregulars.DataID,
            SynonymBase.Synonyms.DataID, AntonymBase.Antonyms.DataID
        };

        List<string> Values = new List<string>();
        for (int i = 0; i < Keys.Count; i++) Values.Add(PlayerPrefs.GetString(Keys[i]));
        Dictionary<string, object> oneElement = new Dictionary<string, object>();
        for (int i = 0; i < Keys.Count; i++) 
        {
            oneElement.Add(Keys[i], PlayerPrefs.GetString(Keys[i]));
            //Debug.Log(("Save - ", Keys[i], PlayerPrefs.GetString(Keys[i])));
        }
        try
        {
            await CloudSaveService.Instance.Data.ForceSaveAsync(oneElement);
            Debug.Log($"Successfully saved:");
        }
        catch (CloudSaveValidationException e)
        {
            Debug.LogError(e);
        }
        catch (CloudSaveRateLimitedException e)
        {
            Debug.LogError(e);
        }
        catch (CloudSaveException e)
        {
            Debug.LogError(e);
        }
        return;
    }
    public async Task Lode() 
    {
        List<string> Keys = new List<string>() { WordBase.Wordgs.DataID, DialogBase.Dialogs.DataID, IrregularBase.Irregulars.DataID,
            SynonymBase.Synonyms.DataID, AntonymBase.Antonyms.DataID
        };
        for (int i = 0; i < Keys.Count; i++)
        {
            try
            {
                var results = await CloudSaveService.Instance.Data.LoadAsync(new HashSet<string> { Keys[i] });
                if (results.TryGetValue(Keys[i], out string value))
                {
                    PlayerPrefs.SetString(Keys[i], value);
                    //Debug.Log((Keys[i], value));
                }
            }
            catch (CloudSaveValidationException e)
            {
                Debug.LogError(e);
            }
            catch (CloudSaveRateLimitedException e)
            {
                Debug.LogError(e);
            }
            catch (CloudSaveException e)
            {
                Debug.LogError(e);
            }
        }
        WordBase.Wordgs = new WordBase();
        DialogBase.Dialogs = new DialogBase();
        IrregularBase.Irregulars = new IrregularBase();
        SynonymBase.Synonyms = new SynonymBase();
        AntonymBase.Antonyms = new AntonymBase();
    }

    [Button]
    public void SignUp(string username, string password) => SignUpWithUsernamePassword(username, password);
    public void SignUp() => SignUp(UserName.text, password.text);
    async Task SignUpWithUsernamePassword(string username, string password)
    {
        if (AuthenticationService.Instance.IsSignedIn) return;
        try
        {
            await AuthenticationService.Instance.SignUpWithUsernamePasswordAsync(username, password);
            await Save();
            SystemBox.Data.Bool("UsernamePassword", true);
            SystemBox.Data.String("Username", username);
            SystemBox.Data.String("Password", password);


            SceneManager.LoadScene(FirsSceneName, LoadSceneMode.Single);
        }
        catch (AuthenticationException ex)
        {
            ConsoleLog.Log(ex.Message, 10);
            // Compare error code to AuthenticationErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
        }
        catch (RequestFailedException ex)
        {
            ConsoleLog.Log(ex.Message, 10);
            // Compare error code to CommonErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
        }

    }
    [Button]
    public void SignIn(string username, string password) => SignInWithUsernamePasswordAsync(username, password);
    public void SignIn() => SignIn(UserName.text, password.text);
    async Task SignInWithUsernamePasswordAsync(string username, string password)
    {
        if (AuthenticationService.Instance.IsSignedIn) return;
        try
        {
            await AuthenticationService.Instance.SignInWithUsernamePasswordAsync(username, password);
            await Lode();
            SystemBox.Data.Bool("UsernamePassword", true);
            SystemBox.Data.String("Username", username);
            SystemBox.Data.String("Password", password);
            SceneManager.LoadScene(FirsSceneName, LoadSceneMode.Single);
            Debug.Log("SignIn is successful.");
        }
        catch (AuthenticationException ex)
        {
            ConsoleLog.Log(ex.Message, 10);
            // Compare error code to AuthenticationErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
        }
        catch (RequestFailedException ex)
        {
            ConsoleLog.Log(ex.Message, 10);
            // Compare error code to CommonErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
        }
    }

}
public static partial class SceneComands
{
    public static void OpenSceneBaseSearch(string SearchID, int SlideIndex)
    {
        Engine.Get_Engine("Game").StartCoroutine(enumerator());
        IEnumerator enumerator()
        {
            if(SceneManager.GetActiveScene().name != "TotleBaseViwe") SceneManager.LoadScene("TotleBaseViwe", LoadSceneMode.Single);
            yield return null;
            yield return null;
            yield return null;
            yield return null;
            yield return null;
            GameObject.FindObjectOfType<FlickSnap>().JumpToDataIndex(SlideIndex);
            SearchViwe Lis = GameObject.FindObjectOfType<SearchViwe>(true);
            Lis.gameObject.SetActive(true);
            Lis.OnShearchValueChanged(SearchID);
        }
    }
}
