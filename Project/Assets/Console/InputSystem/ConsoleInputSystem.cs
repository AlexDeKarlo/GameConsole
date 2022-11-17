//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/Console/InputSystem/ConsoleInputSystem.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @ConsoleInputSystem : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @ConsoleInputSystem()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""ConsoleInputSystem"",
    ""maps"": [
        {
            ""name"": ""Console"",
            ""id"": ""e46b8e98-0722-4334-a1ed-cc2b64ee13f0"",
            ""actions"": [
                {
                    ""name"": ""AppendText"",
                    ""type"": ""Button"",
                    ""id"": ""d3af81b4-ffc0-42ca-b67c-06e6fdab5886"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""InputText"",
                    ""type"": ""Button"",
                    ""id"": ""eb19d486-e5d9-419d-ac6e-3e4a67487aa7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""b5b59808-ff43-41ee-9a66-0f3ed1a71f1c"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AppendText"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""eb439a82-f304-48cf-976d-e1955f4242ec"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""InputText"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Console
        m_Console = asset.FindActionMap("Console", throwIfNotFound: true);
        m_Console_AppendText = m_Console.FindAction("AppendText", throwIfNotFound: true);
        m_Console_InputText = m_Console.FindAction("InputText", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Console
    private readonly InputActionMap m_Console;
    private IConsoleActions m_ConsoleActionsCallbackInterface;
    private readonly InputAction m_Console_AppendText;
    private readonly InputAction m_Console_InputText;
    public struct ConsoleActions
    {
        private @ConsoleInputSystem m_Wrapper;
        public ConsoleActions(@ConsoleInputSystem wrapper) { m_Wrapper = wrapper; }
        public InputAction @AppendText => m_Wrapper.m_Console_AppendText;
        public InputAction @InputText => m_Wrapper.m_Console_InputText;
        public InputActionMap Get() { return m_Wrapper.m_Console; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ConsoleActions set) { return set.Get(); }
        public void SetCallbacks(IConsoleActions instance)
        {
            if (m_Wrapper.m_ConsoleActionsCallbackInterface != null)
            {
                @AppendText.started -= m_Wrapper.m_ConsoleActionsCallbackInterface.OnAppendText;
                @AppendText.performed -= m_Wrapper.m_ConsoleActionsCallbackInterface.OnAppendText;
                @AppendText.canceled -= m_Wrapper.m_ConsoleActionsCallbackInterface.OnAppendText;
                @InputText.started -= m_Wrapper.m_ConsoleActionsCallbackInterface.OnInputText;
                @InputText.performed -= m_Wrapper.m_ConsoleActionsCallbackInterface.OnInputText;
                @InputText.canceled -= m_Wrapper.m_ConsoleActionsCallbackInterface.OnInputText;
            }
            m_Wrapper.m_ConsoleActionsCallbackInterface = instance;
            if (instance != null)
            {
                @AppendText.started += instance.OnAppendText;
                @AppendText.performed += instance.OnAppendText;
                @AppendText.canceled += instance.OnAppendText;
                @InputText.started += instance.OnInputText;
                @InputText.performed += instance.OnInputText;
                @InputText.canceled += instance.OnInputText;
            }
        }
    }
    public ConsoleActions @Console => new ConsoleActions(this);
    public interface IConsoleActions
    {
        void OnAppendText(InputAction.CallbackContext context);
        void OnInputText(InputAction.CallbackContext context);
    }
}
