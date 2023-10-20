using UnityEngine;
public class ConsoleLog : MonoBehaviour
{

    public string Massage;
    private static ConsoleLog consoleLog 
    {
        get 
        {
            if (_consoleLog == null) _consoleLog = new GameObject("Log").AddComponent<ConsoleLog>();
            return _consoleLog;
        }
    }
    private static ConsoleLog _consoleLog;
    public static void Log(string Massage, float TimeToCount = 3f)
    {
        consoleLog.Massage = Massage;
        consoleLog.TimeToCount = TimeToCount;
    }

    float TimeToCount;
    private void OnGUI()
    {
        TimeToCount -= Time.deltaTime;
        if (TimeToCount < 0) return;
        if (GUI.Button(new Rect((Screen.width / 2) - 100, Screen.height / 10, 200, 30), Massage)) 
        {
            TimeToCount = -1; 
        }
    }




}
