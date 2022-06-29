//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/GAMES/HuntWordsGame/Scripts/PlayerInputs/TouchActions.inputactions
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

public partial class @PlayerTouchActions : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerTouchActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""TouchActions"",
    ""maps"": [
        {
            ""name"": ""Touch"",
            ""id"": ""6a662cff-45ba-4677-b90e-35562dd90161"",
            ""actions"": [
                {
                    ""name"": ""TouchUp"",
                    ""type"": ""Button"",
                    ""id"": ""7c243e50-5a1e-4524-a681-87302d0cc175"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""ce9ed681-cb7c-434b-bb22-f64f2a9a4dd6"",
                    ""path"": ""<Touchscreen>/Press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TouchUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3cc60c08-6476-4989-b0e8-1b15e42b7725"",
                    ""path"": ""<Mouse>/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TouchUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Touch
        m_Touch = asset.FindActionMap("Touch", throwIfNotFound: true);
        m_Touch_TouchUp = m_Touch.FindAction("TouchUp", throwIfNotFound: true);
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

    // Touch
    private readonly InputActionMap m_Touch;
    private ITouchActions m_TouchActionsCallbackInterface;
    private readonly InputAction m_Touch_TouchUp;
    public struct TouchActions
    {
        private @PlayerTouchActions m_Wrapper;
        public TouchActions(@PlayerTouchActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @TouchUp => m_Wrapper.m_Touch_TouchUp;
        public InputActionMap Get() { return m_Wrapper.m_Touch; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(TouchActions set) { return set.Get(); }
        public void SetCallbacks(ITouchActions instance)
        {
            if (m_Wrapper.m_TouchActionsCallbackInterface != null)
            {
                @TouchUp.started -= m_Wrapper.m_TouchActionsCallbackInterface.OnTouchUp;
                @TouchUp.performed -= m_Wrapper.m_TouchActionsCallbackInterface.OnTouchUp;
                @TouchUp.canceled -= m_Wrapper.m_TouchActionsCallbackInterface.OnTouchUp;
            }
            m_Wrapper.m_TouchActionsCallbackInterface = instance;
            if (instance != null)
            {
                @TouchUp.started += instance.OnTouchUp;
                @TouchUp.performed += instance.OnTouchUp;
                @TouchUp.canceled += instance.OnTouchUp;
            }
        }
    }
    public TouchActions @Touch => new TouchActions(this);
    public interface ITouchActions
    {
        void OnTouchUp(InputAction.CallbackContext context);
    }
}