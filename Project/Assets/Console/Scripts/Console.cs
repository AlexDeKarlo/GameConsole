using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Console : MonoBehaviour
{
    public static Console instance;

    [SerializeField] private TMPro.TMP_Text ConsoleText;
    [SerializeField] private TMPro.TMP_Text HelpText;
    [SerializeField] private TMPro.TMP_InputField CommandInputField;
    [SerializeField] private Button SendButton;

    [SerializeField] private GameObject HelpPanel;
    [SerializeField] private GameObject ContentText;

    private List<Command> Commands = new();
    private List<GameObject> HelpPanelCommands = new();

    private ConsoleInputSystem consoleInputSystem;

    private void OnDisable()
    {
        CommandInputField.text = string.Empty;
        HelpPanel.SetActive(false);
        ClearConsole();
    }

    private void Awake()
    {
        instance = this;

        consoleInputSystem = new ConsoleInputSystem();
        consoleInputSystem.Console.Enable();

        FindCommands();
        AddListeners();
    }

    private void HelpAppend()
    { 
        CommandInputField.text = HelpText.text;
        CommandInputField.MoveTextEnd(false);
    }
    private void FindCommands()
    {
        var MonoBehaviours = FindObjectsOfType<MonoBehaviour>();

        foreach (var MonoBehaviour in MonoBehaviours)
        {
            var methods = MonoBehaviour.GetType()
                .GetMethods()
                .Where(method => Attribute.IsDefined(method, typeof(CommandAttribute)));
            foreach (var method in methods)
            {
                var attribute = (CommandAttribute)Attribute.GetCustomAttribute(method, typeof(CommandAttribute));
                Commands.Add(new Command(MonoBehaviour, method, attribute.ParamsTypes)); //, attribute.ParamsTypes
            }
        }
    }

    private void AddListeners()
    {
        consoleInputSystem.Console.AppendText.performed += e => HelpAppend();
        consoleInputSystem.Console.InputText.performed += e =>
        {
            SendButton?.onClick.Invoke();
            UnityEngine.EventSystems.
           EventSystem.current.SetSelectedGameObject(null);
        };

        SendButton.onClick.AddListener(Send);
        CommandInputField.onValueChanged.AddListener(InputValueChanged);
        CommandInputField.onSelect.AddListener(e =>
        {
            CommandInputField.text = string.Empty;
            HelpPanel.SetActive(true);
            InputValueChanged(string.Empty);
        });

        CommandInputField.onDeselect.AddListener(e =>
        { 
            HelpPanel.SetActive(false);
            HelpText.text = string.Empty;
        });

    }

    private object[] GetParameters(List<Type> Types, List<string> Strings)
    {
        object[] Parameters = new object[Types.Count];
        if (Types.Count != Strings.Count)
        {
            WriteConsole($"Incorrect number of attributes. Necessary attributes: {Types.Count}!", Color.red);
            return null;
        }
        else
        {
            for (int i = 0; i < Types.Count; i++)
            {
                try
                {
                    if (Types[i] == typeof(string)) Parameters[i] = Strings[i];
                    else if (Strings[i] == "null") Parameters[i] = null;
                    else if (Types[i] == typeof(int)) Parameters[i] = int.Parse(Strings[i]);
                    else if (Types[i] == typeof(float)) Parameters[i] = float.Parse(Strings[i]);
                    else if (Types[i] == typeof(double)) Parameters[i] = double.Parse(Strings[i]);
                    else if (Types[i] == typeof(GameObject)) Parameters[i] = Resources.Load($"Prefabs/{Strings[i]}") as GameObject;
                    else if (Types[i] == typeof(Transform)) Parameters[i] = GameObject.Find(Strings[i]).transform;
                }
                catch
                {
                    WriteConsole("Invalid attribute type!", Color.red);
                    return null;
                }
            }
            return Parameters;
        }

    }

    private void Send() => Send(CommandInputField.text);
    private void Send(string Text)
    {
        List<String> Strings = new();
        Strings.AddRange(Text.Split(' '));
        CommandInputField.text = string.Empty;

        IEnumerable<Command> SendCommads = Commands.OfType<Command>().Where(i => i.Method.Name == Strings[0]);
        if (SendCommads.Count() == 0)
        {
            WriteConsole("Command not found!", Color.red);
            return;
        }
        List<String> tempList = new();
        tempList.AddRange(Strings.Skip(1));
        object[] param = GetParameters(SendCommads.First().ParametersType, tempList);
        if (param == null)
        {
            return;
        }
        SendCommads.First().Method.Invoke(SendCommads.First().Target, param);
    }

    private void InputValueChanged(string value)
    {
        for (int i = 0; i < HelpPanelCommands.Count; i++)
        {
            Destroy(HelpPanelCommands[i]);
        }
        HelpPanelCommands.Clear();

        List<Command> SendCommads = new();
        SendCommads.AddRange(Commands.OfType<Command>().Where(i => i.Method.Name.StartsWith(value)));

        for (int i = 0; i < SendCommads.Count(); i++)
        {
            TMPro.TMP_Text TempText = Instantiate(ContentText, HelpPanel.transform).GetComponent<TMPro.TMP_Text>();
            HelpPanelCommands.Add(TempText.gameObject);
            TempText.text = SendCommads[i].Method.Name;
        }

        if(SendCommads.Count==0 || string.IsNullOrEmpty(CommandInputField.text))
        {
            HelpText.text = string.Empty;
            return;
        }
        HelpText.text = SendCommads.First().Method.Name;
    }

    public void WriteConsole(string Text)
    {
        ConsoleText.text += $"\n{Text}";
    }
    public void WriteConsole(string Text,Color color)
    {
        ConsoleText.text += $"\n<color=#{color.ToHexString()}>{Text}</color>";
    }

    public void ClearConsole()
    {
        ConsoleText.text = string.Empty;
    }

    public List<Command> GetCommands()
    {
        List<Command> temp = new();
        temp.AddRange(Commands.ToArray());
        return temp;    
    }
}
public struct Command
{
    public Command(object _Target, MethodInfo _Method, List<Type> _ParametersType)
    {
        Target = _Target;
        Method = _Method;
        ParametersType = _ParametersType;
    }

    public object Target;
    public MethodInfo Method;
    public List<Type> ParametersType;
}
