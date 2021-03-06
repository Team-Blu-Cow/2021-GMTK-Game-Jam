// GENERATED AUTOMATICALLY FROM 'Assets/JUtils/Example Scenes/Grid Example Scene/InputMaster.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputMaster : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputMaster()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputMaster"",
    ""maps"": [
        {
            ""name"": ""PlayerMovement"",
            ""id"": ""77164e1f-6adb-484a-9512-1643b08347b3"",
            ""actions"": [
                {
                    ""name"": ""MousePosition"",
                    ""type"": ""Value"",
                    ""id"": ""fb38c63a-3383-4071-bf0f-f0804b69f415"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MouseLClick"",
                    ""type"": ""Button"",
                    ""id"": ""697ff8b4-c471-4803-915b-212288ee8a59"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MouseRClick"",
                    ""type"": ""Button"",
                    ""id"": ""89138300-a624-4c86-aab2-292f10352964"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""b4983e7d-9c0d-4934-9ec5-3227fd3b8b7d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""WASD"",
                    ""type"": ""Value"",
                    ""id"": ""6f47a0a2-5c87-4ef3-b25c-38247b939720"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pickup"",
                    ""type"": ""Button"",
                    ""id"": ""5ccd583e-1050-43ea-b0fd-f8774afde9f9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""21184cac-84a8-48a6-857d-5f44bf4c7d08"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""reset"",
                    ""type"": ""Button"",
                    ""id"": ""c90e2d53-9a14-4b41-9f5d-54a4a711aee7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""8c9d5abe-7a70-4db5-b7ff-3d948cda47f4"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e47080a0-f10d-4151-8a45-a43bec8cc479"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseLClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9f7dc9b6-907e-4d01-9ded-2f4addfb9e13"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseRClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""996bd2a9-f7d1-4cad-8a74-ec3a63647ce1"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3808f8c6-78e0-4c12-b1ac-cf91d46724a7"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""66a61257-5b89-4b79-8401-c932785bfc17"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WASD"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""down"",
                    ""id"": ""a8bc5188-1684-4906-b6f8-2aa27bb898b9"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WASD"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""542dd738-ba93-4eaf-9b03-2b3b6d8293cd"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WASD"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""7fa3ed6c-2a68-417b-b5ae-2c37b8a1ff21"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WASD"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""ec739523-dcce-4bdc-bbda-991ab7a29b6f"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pickup"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c1ba2d8b-9a18-4985-87ba-db728f985a37"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""27e12f2e-934d-4098-afeb-d296877250da"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""reset"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PlayerMovement
        m_PlayerMovement = asset.FindActionMap("PlayerMovement", throwIfNotFound: true);
        m_PlayerMovement_MousePosition = m_PlayerMovement.FindAction("MousePosition", throwIfNotFound: true);
        m_PlayerMovement_MouseLClick = m_PlayerMovement.FindAction("MouseLClick", throwIfNotFound: true);
        m_PlayerMovement_MouseRClick = m_PlayerMovement.FindAction("MouseRClick", throwIfNotFound: true);
        m_PlayerMovement_Jump = m_PlayerMovement.FindAction("Jump", throwIfNotFound: true);
        m_PlayerMovement_WASD = m_PlayerMovement.FindAction("WASD", throwIfNotFound: true);
        m_PlayerMovement_Pickup = m_PlayerMovement.FindAction("Pickup", throwIfNotFound: true);
        m_PlayerMovement_Pause = m_PlayerMovement.FindAction("Pause", throwIfNotFound: true);
        m_PlayerMovement_reset = m_PlayerMovement.FindAction("reset", throwIfNotFound: true);
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

    // PlayerMovement
    private readonly InputActionMap m_PlayerMovement;
    private IPlayerMovementActions m_PlayerMovementActionsCallbackInterface;
    private readonly InputAction m_PlayerMovement_MousePosition;
    private readonly InputAction m_PlayerMovement_MouseLClick;
    private readonly InputAction m_PlayerMovement_MouseRClick;
    private readonly InputAction m_PlayerMovement_Jump;
    private readonly InputAction m_PlayerMovement_WASD;
    private readonly InputAction m_PlayerMovement_Pickup;
    private readonly InputAction m_PlayerMovement_Pause;
    private readonly InputAction m_PlayerMovement_reset;
    public struct PlayerMovementActions
    {
        private @InputMaster m_Wrapper;
        public PlayerMovementActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @MousePosition => m_Wrapper.m_PlayerMovement_MousePosition;
        public InputAction @MouseLClick => m_Wrapper.m_PlayerMovement_MouseLClick;
        public InputAction @MouseRClick => m_Wrapper.m_PlayerMovement_MouseRClick;
        public InputAction @Jump => m_Wrapper.m_PlayerMovement_Jump;
        public InputAction @WASD => m_Wrapper.m_PlayerMovement_WASD;
        public InputAction @Pickup => m_Wrapper.m_PlayerMovement_Pickup;
        public InputAction @Pause => m_Wrapper.m_PlayerMovement_Pause;
        public InputAction @reset => m_Wrapper.m_PlayerMovement_reset;
        public InputActionMap Get() { return m_Wrapper.m_PlayerMovement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerMovementActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerMovementActions instance)
        {
            if (m_Wrapper.m_PlayerMovementActionsCallbackInterface != null)
            {
                @MousePosition.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMousePosition;
                @MousePosition.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMousePosition;
                @MousePosition.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMousePosition;
                @MouseLClick.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMouseLClick;
                @MouseLClick.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMouseLClick;
                @MouseLClick.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMouseLClick;
                @MouseRClick.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMouseRClick;
                @MouseRClick.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMouseRClick;
                @MouseRClick.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMouseRClick;
                @Jump.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnJump;
                @WASD.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnWASD;
                @WASD.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnWASD;
                @WASD.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnWASD;
                @Pickup.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnPickup;
                @Pickup.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnPickup;
                @Pickup.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnPickup;
                @Pause.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnPause;
                @reset.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnReset;
                @reset.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnReset;
                @reset.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnReset;
            }
            m_Wrapper.m_PlayerMovementActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MousePosition.started += instance.OnMousePosition;
                @MousePosition.performed += instance.OnMousePosition;
                @MousePosition.canceled += instance.OnMousePosition;
                @MouseLClick.started += instance.OnMouseLClick;
                @MouseLClick.performed += instance.OnMouseLClick;
                @MouseLClick.canceled += instance.OnMouseLClick;
                @MouseRClick.started += instance.OnMouseRClick;
                @MouseRClick.performed += instance.OnMouseRClick;
                @MouseRClick.canceled += instance.OnMouseRClick;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @WASD.started += instance.OnWASD;
                @WASD.performed += instance.OnWASD;
                @WASD.canceled += instance.OnWASD;
                @Pickup.started += instance.OnPickup;
                @Pickup.performed += instance.OnPickup;
                @Pickup.canceled += instance.OnPickup;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
                @reset.started += instance.OnReset;
                @reset.performed += instance.OnReset;
                @reset.canceled += instance.OnReset;
            }
        }
    }
    public PlayerMovementActions @PlayerMovement => new PlayerMovementActions(this);
    public interface IPlayerMovementActions
    {
        void OnMousePosition(InputAction.CallbackContext context);
        void OnMouseLClick(InputAction.CallbackContext context);
        void OnMouseRClick(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnWASD(InputAction.CallbackContext context);
        void OnPickup(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
        void OnReset(InputAction.CallbackContext context);
    }
}
