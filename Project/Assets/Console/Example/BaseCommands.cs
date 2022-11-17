using System.Collections;
using System.Collections.Generic;
using System.Windows.Input;
using UnityEngine;

public class BaseCommands : MonoBehaviour
{
    [Command]
    public void Help()
    {
        Console.instance.WriteConsole("<size=60>Commands List</size>", Color.cyan);
        Console.instance.WriteConsole("Command Example: \"ConsoleWriteText Text\"\n", Color.cyan);
        List<Command> commands = Console.instance.GetCommands();
        foreach(Command command in commands)
        {
            string temp = "<color=white>" + command.Method.Name + "(</color>";

            for (int i = 0; i < command.ParametersType.Count; i++)
            {
                temp += $"<color=yellow>{command.ParametersType[i].Name}</color>";
                if(i!=command.ParametersType.Count-1) temp += "<color=white>, </color>";
            }
            temp += "<color=white>)</color>";
            Console.instance.WriteConsole(temp, Color.black);
        }
    }

    [Command]
    public void Clear()
    {
        Console.instance.ClearConsole();
    }

    [Command(typeof(string))]
    public void ConsoleWriteText(string Text)
    {
        Debug.Log(Text);
    }

    [Command(typeof(int))]
    public void SetTime(float X)
    {
        Console.instance.WriteConsole("TimeScale = "+X,Color.yellow);
        Time.timeScale = X;
    }

    [Command(typeof(GameObject))]
    public void SpawnObject(GameObject prefab)
    {
        if (prefab == null) return;
        Console.instance.WriteConsole("Spawn:" + prefab.name, Color.gray);
        Instantiate(prefab);
    }

    [Command(typeof(Transform), typeof(float), typeof(float), typeof(float))]
    public void Teleport(Transform Obj, float x, float y, float z)
    {
        Obj.position = new Vector3(x,y,z);
    }
    
    [Command(typeof(Transform))]
    public void DestroyObject(Transform Obj)
    {
        Destroy(Obj.gameObject);
    }
}
