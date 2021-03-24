// GENERATED AUTOMATICALLY FROM 'Assets/Input/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""TileControls"",
            ""id"": ""ab98b647-74c5-4365-95a7-668dbeeef835"",
            ""actions"": [
                {
                    ""name"": ""MoveLeft"",
                    ""type"": ""PassThrough"",
                    ""id"": ""ed75a858-2dae-479b-acfa-bdf60c639ffb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveRight"",
                    ""type"": ""PassThrough"",
                    ""id"": ""a7841827-12b4-4101-b7f3-6c52040de4ec"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Rush"",
                    ""type"": ""PassThrough"",
                    ""id"": ""e00392ee-f859-4b7c-a88f-147242410198"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TouchContact"",
                    ""type"": ""PassThrough"",
                    ""id"": ""718e5508-d184-41f3-8405-acf0393a0626"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TouchPosition"",
                    ""type"": ""PassThrough"",
                    ""id"": ""34e43beb-e99d-494b-86b7-ecb5cc1245ce"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""9b255412-21ff-4754-abf2-78fe70147ae8"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b5dca13c-cd07-40b2-9631-345c8ace06b0"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""56965606-f197-4481-bda0-f8ae2bbd0d9d"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""26c0dad7-8a5c-4399-8285-2dbc27f80b52"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""08c6e796-0a37-4194-b748-f27c0677bb3d"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rush"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3360b4c8-4e50-4b9b-bcd7-e2d174c83667"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rush"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2eea2a44-e08a-4013-910a-e3acec8edcce"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rush"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""af23986a-bcee-46a6-b2ef-2a2dd28aa8e4"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rush"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""822e3ea4-e0ec-41de-a501-98a4ccacb46f"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rush"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""da8f78ab-4582-4e3d-9ef2-fedc90c84971"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rush"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6bb4b700-2767-4226-b2d4-da1b63b28b8d"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""18a13d37-d4c4-4ecb-8da5-5a24c77489d5"",
                    ""path"": ""<Gamepad>/rightStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""96b89943-09fe-4b4a-9b48-9376b1f258f7"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9e8af221-f8bd-4663-aa3e-8f44052e8aa4"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2bc23e80-9760-4f05-99d5-ec7e05921519"",
                    ""path"": ""<Touchscreen>/primaryTouch/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TouchContact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f31e41a9-0973-42f3-8196-a774ddc2ac6c"",
                    ""path"": ""<Touchscreen>/primaryTouch/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TouchPosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // TileControls
        m_TileControls = asset.FindActionMap("TileControls", throwIfNotFound: true);
        m_TileControls_MoveLeft = m_TileControls.FindAction("MoveLeft", throwIfNotFound: true);
        m_TileControls_MoveRight = m_TileControls.FindAction("MoveRight", throwIfNotFound: true);
        m_TileControls_Rush = m_TileControls.FindAction("Rush", throwIfNotFound: true);
        m_TileControls_TouchContact = m_TileControls.FindAction("TouchContact", throwIfNotFound: true);
        m_TileControls_TouchPosition = m_TileControls.FindAction("TouchPosition", throwIfNotFound: true);
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

    // TileControls
    private readonly InputActionMap m_TileControls;
    private ITileControlsActions m_TileControlsActionsCallbackInterface;
    private readonly InputAction m_TileControls_MoveLeft;
    private readonly InputAction m_TileControls_MoveRight;
    private readonly InputAction m_TileControls_Rush;
    private readonly InputAction m_TileControls_TouchContact;
    private readonly InputAction m_TileControls_TouchPosition;
    public struct TileControlsActions
    {
        private @PlayerControls m_Wrapper;
        public TileControlsActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @MoveLeft => m_Wrapper.m_TileControls_MoveLeft;
        public InputAction @MoveRight => m_Wrapper.m_TileControls_MoveRight;
        public InputAction @Rush => m_Wrapper.m_TileControls_Rush;
        public InputAction @TouchContact => m_Wrapper.m_TileControls_TouchContact;
        public InputAction @TouchPosition => m_Wrapper.m_TileControls_TouchPosition;
        public InputActionMap Get() { return m_Wrapper.m_TileControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(TileControlsActions set) { return set.Get(); }
        public void SetCallbacks(ITileControlsActions instance)
        {
            if (m_Wrapper.m_TileControlsActionsCallbackInterface != null)
            {
                @MoveLeft.started -= m_Wrapper.m_TileControlsActionsCallbackInterface.OnMoveLeft;
                @MoveLeft.performed -= m_Wrapper.m_TileControlsActionsCallbackInterface.OnMoveLeft;
                @MoveLeft.canceled -= m_Wrapper.m_TileControlsActionsCallbackInterface.OnMoveLeft;
                @MoveRight.started -= m_Wrapper.m_TileControlsActionsCallbackInterface.OnMoveRight;
                @MoveRight.performed -= m_Wrapper.m_TileControlsActionsCallbackInterface.OnMoveRight;
                @MoveRight.canceled -= m_Wrapper.m_TileControlsActionsCallbackInterface.OnMoveRight;
                @Rush.started -= m_Wrapper.m_TileControlsActionsCallbackInterface.OnRush;
                @Rush.performed -= m_Wrapper.m_TileControlsActionsCallbackInterface.OnRush;
                @Rush.canceled -= m_Wrapper.m_TileControlsActionsCallbackInterface.OnRush;
                @TouchContact.started -= m_Wrapper.m_TileControlsActionsCallbackInterface.OnTouchContact;
                @TouchContact.performed -= m_Wrapper.m_TileControlsActionsCallbackInterface.OnTouchContact;
                @TouchContact.canceled -= m_Wrapper.m_TileControlsActionsCallbackInterface.OnTouchContact;
                @TouchPosition.started -= m_Wrapper.m_TileControlsActionsCallbackInterface.OnTouchPosition;
                @TouchPosition.performed -= m_Wrapper.m_TileControlsActionsCallbackInterface.OnTouchPosition;
                @TouchPosition.canceled -= m_Wrapper.m_TileControlsActionsCallbackInterface.OnTouchPosition;
            }
            m_Wrapper.m_TileControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MoveLeft.started += instance.OnMoveLeft;
                @MoveLeft.performed += instance.OnMoveLeft;
                @MoveLeft.canceled += instance.OnMoveLeft;
                @MoveRight.started += instance.OnMoveRight;
                @MoveRight.performed += instance.OnMoveRight;
                @MoveRight.canceled += instance.OnMoveRight;
                @Rush.started += instance.OnRush;
                @Rush.performed += instance.OnRush;
                @Rush.canceled += instance.OnRush;
                @TouchContact.started += instance.OnTouchContact;
                @TouchContact.performed += instance.OnTouchContact;
                @TouchContact.canceled += instance.OnTouchContact;
                @TouchPosition.started += instance.OnTouchPosition;
                @TouchPosition.performed += instance.OnTouchPosition;
                @TouchPosition.canceled += instance.OnTouchPosition;
            }
        }
    }
    public TileControlsActions @TileControls => new TileControlsActions(this);
    public interface ITileControlsActions
    {
        void OnMoveLeft(InputAction.CallbackContext context);
        void OnMoveRight(InputAction.CallbackContext context);
        void OnRush(InputAction.CallbackContext context);
        void OnTouchContact(InputAction.CallbackContext context);
        void OnTouchPosition(InputAction.CallbackContext context);
    }
}
