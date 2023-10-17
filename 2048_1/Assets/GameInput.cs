// GENERATED AUTOMATICALLY FROM 'Assets/GameInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @GameInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @GameInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""GameInput"",
    ""maps"": [
        {
            ""name"": ""Main"",
            ""id"": ""74fdf8f1-d5f8-4664-bb1f-9e85170287b5"",
            ""actions"": [
                {
                    ""name"": ""Touch"",
                    ""type"": ""PassThrough"",
                    ""id"": ""c031ad02-dc7a-465d-b1a3-a83225d82f45"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Keyboard"",
                    ""type"": ""PassThrough"",
                    ""id"": ""9000458e-c344-4aa7-b51c-b5c74633b9b6"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": ""Tap(duration=0.05,pressPoint=0.5)""
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""5e195f59-d8d6-405d-866a-03c7bb24cf67"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Touch"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""dc00bb06-61a1-43ef-9d5a-73a7c79d2fc9"",
                    ""path"": ""<Touchscreen>/primaryTouch/delta/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Touch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""49221386-69e8-4298-a84a-96c228adbd4a"",
                    ""path"": ""<Touchscreen>/primaryTouch/delta/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Touch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""4d81b7f4-832e-40eb-bcfd-ce226c8c1422"",
                    ""path"": ""<Touchscreen>/primaryTouch/delta/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Touch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""18596401-db86-42b3-a52f-6b35967a9a2a"",
                    ""path"": ""<Touchscreen>/primaryTouch/delta/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Touch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""267de750-75b3-40b5-829e-1185e6e5773f"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Keyboard"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""ca5aaa16-3664-4817-8d59-9599b8352b56"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Keyboard"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""5db368cc-edc6-4f2b-8018-4b5170cfb12e"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Keyboard"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""7349c8a6-c8cd-4c08-8ce3-6cd3dff9ce32"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Keyboard"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""7861f54b-dbbf-49c1-8a6e-cbed732cd2a2"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Keyboard"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Main
        m_Main = asset.FindActionMap("Main", throwIfNotFound: true);
        m_Main_Touch = m_Main.FindAction("Touch", throwIfNotFound: true);
        m_Main_Keyboard = m_Main.FindAction("Keyboard", throwIfNotFound: true);
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

    // Main
    private readonly InputActionMap m_Main;
    private IMainActions m_MainActionsCallbackInterface;
    private readonly InputAction m_Main_Touch;
    private readonly InputAction m_Main_Keyboard;
    public struct MainActions
    {
        private @GameInput m_Wrapper;
        public MainActions(@GameInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Touch => m_Wrapper.m_Main_Touch;
        public InputAction @Keyboard => m_Wrapper.m_Main_Keyboard;
        public InputActionMap Get() { return m_Wrapper.m_Main; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MainActions set) { return set.Get(); }
        public void SetCallbacks(IMainActions instance)
        {
            if (m_Wrapper.m_MainActionsCallbackInterface != null)
            {
                @Touch.started -= m_Wrapper.m_MainActionsCallbackInterface.OnTouch;
                @Touch.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnTouch;
                @Touch.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnTouch;
                @Keyboard.started -= m_Wrapper.m_MainActionsCallbackInterface.OnKeyboard;
                @Keyboard.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnKeyboard;
                @Keyboard.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnKeyboard;
            }
            m_Wrapper.m_MainActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Touch.started += instance.OnTouch;
                @Touch.performed += instance.OnTouch;
                @Touch.canceled += instance.OnTouch;
                @Keyboard.started += instance.OnKeyboard;
                @Keyboard.performed += instance.OnKeyboard;
                @Keyboard.canceled += instance.OnKeyboard;
            }
        }
    }
    public MainActions @Main => new MainActions(this);
    public interface IMainActions
    {
        void OnTouch(InputAction.CallbackContext context);
        void OnKeyboard(InputAction.CallbackContext context);
    }
}
