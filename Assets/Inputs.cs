// GENERATED AUTOMATICALLY FROM 'Assets/Inputs.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Inputs : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Inputs()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Inputs"",
    ""maps"": [
        {
            ""name"": ""Uni"",
            ""id"": ""9616501a-d596-4cae-a5fc-e00c45633edb"",
            ""actions"": [
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""e3b81631-2f31-4094-8d5a-ddec16118a01"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""Walk"",
                    ""type"": ""Button"",
                    ""id"": ""409f3ada-cc86-43c5-a5ab-5b14ac9b18ff"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Crouch"",
                    ""type"": ""Button"",
                    ""id"": ""79ee36a7-04d8-41ea-aae2-d09f17fdd078"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Climb_Up"",
                    ""type"": ""Button"",
                    ""id"": ""00132744-9f9f-4a95-aaae-14aa3b448856"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Let_Go"",
                    ""type"": ""Button"",
                    ""id"": ""43f9d004-683b-4719-b99a-b9e08877f2b1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Grapple"",
                    ""type"": ""Button"",
                    ""id"": ""a9beec95-4220-447a-9ae2-248edc70385b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Detach"",
                    ""type"": ""Button"",
                    ""id"": ""ccb05ed0-bbc9-4639-a1fb-82732755e8c7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Grapple_Vert"",
                    ""type"": ""Button"",
                    ""id"": ""679c510f-50ed-477f-96ff-8f6c70efbf9e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Inhale"",
                    ""type"": ""Button"",
                    ""id"": ""f2d47e32-bbff-40fc-b485-c3bac1ecd983"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Exhale"",
                    ""type"": ""Button"",
                    ""id"": ""30a704ed-ffe8-4299-99da-d49bc013be34"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""NextHook"",
                    ""type"": ""Button"",
                    ""id"": ""17dfa0db-dbb0-4d1a-8921-5f7139a420ed"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PrevHook"",
                    ""type"": ""Button"",
                    ""id"": ""e9ae0492-ce3c-4094-b04c-d4082e207a49"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Die"",
                    ""type"": ""Button"",
                    ""id"": ""12bd2d93-e6ef-4209-a8a3-5ae629c9e58d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PressButton"",
                    ""type"": ""Button"",
                    ""id"": ""f9fabe22-2bd3-4166-b147-5814c136d516"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Move_Box"",
                    ""type"": ""Button"",
                    ""id"": ""378d6280-d838-4f65-a38a-d1e3fcf955fb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""GetLetter"",
                    ""type"": ""Button"",
                    ""id"": ""c7d0038a-838b-4c03-980f-fe6dbbeb7d25"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SwitchOffLight"",
                    ""type"": ""Button"",
                    ""id"": ""ca787971-7875-4624-835d-90a05cf9cd69"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""rightup"",
                    ""type"": ""Button"",
                    ""id"": ""e81921db-c7f9-4da4-b7d0-72a9f7df149e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""rightdown"",
                    ""type"": ""Button"",
                    ""id"": ""34c28a99-a906-4499-bfb5-b0e3e95c0200"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""rightright"",
                    ""type"": ""Button"",
                    ""id"": ""0f0769a3-6b8c-488d-8e80-4874b3161aa7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""rightleft"",
                    ""type"": ""Button"",
                    ""id"": ""69cac43c-c80b-4f3e-bd78-0cfce6598c8d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""4b09c6dc-efbc-4014-b1d3-41def1d3a5e8"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""759fb388-b915-41ef-a747-e8a901359ed6"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""7c93bf13-5389-45eb-b991-eb8df053e31e"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Walk"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""09b17cb6-7cef-41c7-be95-9944e3cb7f68"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Walk"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""8219b9c4-a420-4452-ab68-cf786c169ed7"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Walk"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""ff4830fa-0b34-4138-ab59-1751bfbc04b7"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Walk"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""91aea01c-10fb-4850-bb59-2ed33cc6c041"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Walk"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""6b26594c-ea57-436e-b95e-3d4c0fcd7076"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Walk"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""74f857eb-06d5-4c07-a7e1-abbe203ff298"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Crouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ee2c3522-9401-4a70-abed-d502d7381ad4"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Crouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0630ec61-abec-45ac-bd79-bf6646c38e06"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Climb_Up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""63b5c96e-fe23-4215-9a14-72df76e680da"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Climb_Up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3c1a6d71-f226-4738-bbc8-cc0fac1e44a6"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Let_Go"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7c3b3e61-73c8-4e47-9c90-74b7b89b7a27"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Let_Go"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""386e6f93-0ca9-4db6-8d95-92432995e0a3"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Grapple"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3cf980a7-84c8-47db-80ae-4595605cf240"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Grapple"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6d1f3065-eaca-444c-8ddd-d43f2e65f941"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Detach"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0eb92af1-5698-4f9f-a17b-39512fa02bec"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Detach"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""bf98c992-1188-41ec-9878-3d0fa2c2cb8c"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Grapple_Vert"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""2b96c1da-78ff-467b-ac2c-d2e17ea4123c"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Grapple_Vert"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""17f4df04-6a86-4ccf-be71-55982a07f6b0"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Grapple_Vert"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""33a555da-bb7a-44ba-9368-ca9889875ab6"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Grapple_Vert"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""3331f1ae-d204-49f9-a1cb-8662b8a8bbf9"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Grapple_Vert"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""7f202a3e-9da2-49b4-afdc-d311edeef1ee"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Grapple_Vert"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""bc5224aa-e308-452f-9daa-f03073f40805"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Inhale"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c17ca297-53d6-42d7-9069-07c7d7dd9736"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Inhale"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8f9776d4-a4e2-406e-8a54-568f2991fd3a"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Exhale"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d091513a-aa3c-4eb1-8207-ef2e5a382aa1"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Exhale"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fed71884-8e61-4757-88b0-8717d43d4304"",
                    ""path"": ""<Gamepad>/rightStick/left"",
                    ""interactions"": ""Press(pressPoint=1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""NextHook"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""762e0665-6900-4a17-b621-244c51fc5310"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""NextHook"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""216ce447-bb36-4555-9864-cb2668dbc66f"",
                    ""path"": ""<Gamepad>/rightStick/right"",
                    ""interactions"": ""Press(pressPoint=1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PrevHook"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f73f5e55-ad4e-4af5-89bc-5a5088275864"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PrevHook"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""363389dd-5db5-4ed8-a5e2-bf696b0fce94"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Die"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""788452d5-6c38-4454-a386-07358b9c2172"",
                    ""path"": ""<Keyboard>/p"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Die"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""809b5315-b866-48aa-ac43-e57c2e9e6bd9"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Die"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""826735e1-6b1a-4874-a8ab-cc33d4e88f5a"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PressButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ebdc5c42-c2b6-45c2-a32c-61ba997a04e6"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PressButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a535213c-9353-4d37-990e-65e4011aecba"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move_Box"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cba125c7-0852-448b-935b-ab7a99dd78f0"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move_Box"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7c897d84-1a2e-459c-af29-d4bd81af4c35"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GetLetter"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7c0252d4-e15e-43af-b132-061e2a733ba8"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GetLetter"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""07bc999c-4c7c-46b9-a3b5-d37ced03efcb"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchOffLight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f2fcc27a-818e-4df7-9d6e-893bf2be0d9b"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchOffLight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""02ed5283-f413-4ef2-8fe5-733e74c5db1e"",
                    ""path"": ""<Gamepad>/rightStick/up"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""rightup"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dc3d71a2-93b3-4ca0-9bdb-3f9da66ac494"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""rightup"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e75d9d8e-e96f-44af-8721-814d3940e0e8"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""rightup"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""355db9c2-43a6-4362-add0-cb8bb3229081"",
                    ""path"": ""<Gamepad>/rightStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""rightdown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""17eb80fb-ee1d-408f-819a-d924dff4b5e2"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""rightdown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ee92b381-b46f-48fb-8274-8f56796ed7fa"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""rightdown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ae108822-0732-473d-a7b9-104063e3e0a1"",
                    ""path"": ""<Gamepad>/rightStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""rightright"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""11ee7f00-47b4-416e-89cd-261f57263a87"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""rightright"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4ce38866-cde9-44e7-b2d7-6b4c48596635"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""rightright"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8d9d9edd-d265-4faa-83be-bf80c7e6b000"",
                    ""path"": ""<Gamepad>/rightStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""rightleft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d9810ce7-760c-4765-9905-97e058a838bd"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""rightleft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""da9e3243-e820-4eac-8212-bf7b4cb2dbc4"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""rightleft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Menu"",
            ""id"": ""d34c8792-218f-4c78-831c-1d83160a8105"",
            ""actions"": [
                {
                    ""name"": ""PlayPause"",
                    ""type"": ""Button"",
                    ""id"": ""a3ed67ff-c8bc-4978-86a1-0ada43aa5a55"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""486d4014-dbe9-4b38-8e38-8ec66746e2ec"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayPause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Uni
        m_Uni = asset.FindActionMap("Uni", throwIfNotFound: true);
        m_Uni_Jump = m_Uni.FindAction("Jump", throwIfNotFound: true);
        m_Uni_Walk = m_Uni.FindAction("Walk", throwIfNotFound: true);
        m_Uni_Crouch = m_Uni.FindAction("Crouch", throwIfNotFound: true);
        m_Uni_Climb_Up = m_Uni.FindAction("Climb_Up", throwIfNotFound: true);
        m_Uni_Let_Go = m_Uni.FindAction("Let_Go", throwIfNotFound: true);
        m_Uni_Grapple = m_Uni.FindAction("Grapple", throwIfNotFound: true);
        m_Uni_Detach = m_Uni.FindAction("Detach", throwIfNotFound: true);
        m_Uni_Grapple_Vert = m_Uni.FindAction("Grapple_Vert", throwIfNotFound: true);
        m_Uni_Inhale = m_Uni.FindAction("Inhale", throwIfNotFound: true);
        m_Uni_Exhale = m_Uni.FindAction("Exhale", throwIfNotFound: true);
        m_Uni_NextHook = m_Uni.FindAction("NextHook", throwIfNotFound: true);
        m_Uni_PrevHook = m_Uni.FindAction("PrevHook", throwIfNotFound: true);
        m_Uni_Die = m_Uni.FindAction("Die", throwIfNotFound: true);
        m_Uni_PressButton = m_Uni.FindAction("PressButton", throwIfNotFound: true);
        m_Uni_Move_Box = m_Uni.FindAction("Move_Box", throwIfNotFound: true);
        m_Uni_GetLetter = m_Uni.FindAction("GetLetter", throwIfNotFound: true);
        m_Uni_SwitchOffLight = m_Uni.FindAction("SwitchOffLight", throwIfNotFound: true);
        m_Uni_rightup = m_Uni.FindAction("rightup", throwIfNotFound: true);
        m_Uni_rightdown = m_Uni.FindAction("rightdown", throwIfNotFound: true);
        m_Uni_rightright = m_Uni.FindAction("rightright", throwIfNotFound: true);
        m_Uni_rightleft = m_Uni.FindAction("rightleft", throwIfNotFound: true);
        // Menu
        m_Menu = asset.FindActionMap("Menu", throwIfNotFound: true);
        m_Menu_PlayPause = m_Menu.FindAction("PlayPause", throwIfNotFound: true);
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

    // Uni
    private readonly InputActionMap m_Uni;
    private IUniActions m_UniActionsCallbackInterface;
    private readonly InputAction m_Uni_Jump;
    private readonly InputAction m_Uni_Walk;
    private readonly InputAction m_Uni_Crouch;
    private readonly InputAction m_Uni_Climb_Up;
    private readonly InputAction m_Uni_Let_Go;
    private readonly InputAction m_Uni_Grapple;
    private readonly InputAction m_Uni_Detach;
    private readonly InputAction m_Uni_Grapple_Vert;
    private readonly InputAction m_Uni_Inhale;
    private readonly InputAction m_Uni_Exhale;
    private readonly InputAction m_Uni_NextHook;
    private readonly InputAction m_Uni_PrevHook;
    private readonly InputAction m_Uni_Die;
    private readonly InputAction m_Uni_PressButton;
    private readonly InputAction m_Uni_Move_Box;
    private readonly InputAction m_Uni_GetLetter;
    private readonly InputAction m_Uni_SwitchOffLight;
    private readonly InputAction m_Uni_rightup;
    private readonly InputAction m_Uni_rightdown;
    private readonly InputAction m_Uni_rightright;
    private readonly InputAction m_Uni_rightleft;
    public struct UniActions
    {
        private @Inputs m_Wrapper;
        public UniActions(@Inputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @Jump => m_Wrapper.m_Uni_Jump;
        public InputAction @Walk => m_Wrapper.m_Uni_Walk;
        public InputAction @Crouch => m_Wrapper.m_Uni_Crouch;
        public InputAction @Climb_Up => m_Wrapper.m_Uni_Climb_Up;
        public InputAction @Let_Go => m_Wrapper.m_Uni_Let_Go;
        public InputAction @Grapple => m_Wrapper.m_Uni_Grapple;
        public InputAction @Detach => m_Wrapper.m_Uni_Detach;
        public InputAction @Grapple_Vert => m_Wrapper.m_Uni_Grapple_Vert;
        public InputAction @Inhale => m_Wrapper.m_Uni_Inhale;
        public InputAction @Exhale => m_Wrapper.m_Uni_Exhale;
        public InputAction @NextHook => m_Wrapper.m_Uni_NextHook;
        public InputAction @PrevHook => m_Wrapper.m_Uni_PrevHook;
        public InputAction @Die => m_Wrapper.m_Uni_Die;
        public InputAction @PressButton => m_Wrapper.m_Uni_PressButton;
        public InputAction @Move_Box => m_Wrapper.m_Uni_Move_Box;
        public InputAction @GetLetter => m_Wrapper.m_Uni_GetLetter;
        public InputAction @SwitchOffLight => m_Wrapper.m_Uni_SwitchOffLight;
        public InputAction @rightup => m_Wrapper.m_Uni_rightup;
        public InputAction @rightdown => m_Wrapper.m_Uni_rightdown;
        public InputAction @rightright => m_Wrapper.m_Uni_rightright;
        public InputAction @rightleft => m_Wrapper.m_Uni_rightleft;
        public InputActionMap Get() { return m_Wrapper.m_Uni; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UniActions set) { return set.Get(); }
        public void SetCallbacks(IUniActions instance)
        {
            if (m_Wrapper.m_UniActionsCallbackInterface != null)
            {
                @Jump.started -= m_Wrapper.m_UniActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_UniActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_UniActionsCallbackInterface.OnJump;
                @Walk.started -= m_Wrapper.m_UniActionsCallbackInterface.OnWalk;
                @Walk.performed -= m_Wrapper.m_UniActionsCallbackInterface.OnWalk;
                @Walk.canceled -= m_Wrapper.m_UniActionsCallbackInterface.OnWalk;
                @Crouch.started -= m_Wrapper.m_UniActionsCallbackInterface.OnCrouch;
                @Crouch.performed -= m_Wrapper.m_UniActionsCallbackInterface.OnCrouch;
                @Crouch.canceled -= m_Wrapper.m_UniActionsCallbackInterface.OnCrouch;
                @Climb_Up.started -= m_Wrapper.m_UniActionsCallbackInterface.OnClimb_Up;
                @Climb_Up.performed -= m_Wrapper.m_UniActionsCallbackInterface.OnClimb_Up;
                @Climb_Up.canceled -= m_Wrapper.m_UniActionsCallbackInterface.OnClimb_Up;
                @Let_Go.started -= m_Wrapper.m_UniActionsCallbackInterface.OnLet_Go;
                @Let_Go.performed -= m_Wrapper.m_UniActionsCallbackInterface.OnLet_Go;
                @Let_Go.canceled -= m_Wrapper.m_UniActionsCallbackInterface.OnLet_Go;
                @Grapple.started -= m_Wrapper.m_UniActionsCallbackInterface.OnGrapple;
                @Grapple.performed -= m_Wrapper.m_UniActionsCallbackInterface.OnGrapple;
                @Grapple.canceled -= m_Wrapper.m_UniActionsCallbackInterface.OnGrapple;
                @Detach.started -= m_Wrapper.m_UniActionsCallbackInterface.OnDetach;
                @Detach.performed -= m_Wrapper.m_UniActionsCallbackInterface.OnDetach;
                @Detach.canceled -= m_Wrapper.m_UniActionsCallbackInterface.OnDetach;
                @Grapple_Vert.started -= m_Wrapper.m_UniActionsCallbackInterface.OnGrapple_Vert;
                @Grapple_Vert.performed -= m_Wrapper.m_UniActionsCallbackInterface.OnGrapple_Vert;
                @Grapple_Vert.canceled -= m_Wrapper.m_UniActionsCallbackInterface.OnGrapple_Vert;
                @Inhale.started -= m_Wrapper.m_UniActionsCallbackInterface.OnInhale;
                @Inhale.performed -= m_Wrapper.m_UniActionsCallbackInterface.OnInhale;
                @Inhale.canceled -= m_Wrapper.m_UniActionsCallbackInterface.OnInhale;
                @Exhale.started -= m_Wrapper.m_UniActionsCallbackInterface.OnExhale;
                @Exhale.performed -= m_Wrapper.m_UniActionsCallbackInterface.OnExhale;
                @Exhale.canceled -= m_Wrapper.m_UniActionsCallbackInterface.OnExhale;
                @NextHook.started -= m_Wrapper.m_UniActionsCallbackInterface.OnNextHook;
                @NextHook.performed -= m_Wrapper.m_UniActionsCallbackInterface.OnNextHook;
                @NextHook.canceled -= m_Wrapper.m_UniActionsCallbackInterface.OnNextHook;
                @PrevHook.started -= m_Wrapper.m_UniActionsCallbackInterface.OnPrevHook;
                @PrevHook.performed -= m_Wrapper.m_UniActionsCallbackInterface.OnPrevHook;
                @PrevHook.canceled -= m_Wrapper.m_UniActionsCallbackInterface.OnPrevHook;
                @Die.started -= m_Wrapper.m_UniActionsCallbackInterface.OnDie;
                @Die.performed -= m_Wrapper.m_UniActionsCallbackInterface.OnDie;
                @Die.canceled -= m_Wrapper.m_UniActionsCallbackInterface.OnDie;
                @PressButton.started -= m_Wrapper.m_UniActionsCallbackInterface.OnPressButton;
                @PressButton.performed -= m_Wrapper.m_UniActionsCallbackInterface.OnPressButton;
                @PressButton.canceled -= m_Wrapper.m_UniActionsCallbackInterface.OnPressButton;
                @Move_Box.started -= m_Wrapper.m_UniActionsCallbackInterface.OnMove_Box;
                @Move_Box.performed -= m_Wrapper.m_UniActionsCallbackInterface.OnMove_Box;
                @Move_Box.canceled -= m_Wrapper.m_UniActionsCallbackInterface.OnMove_Box;
                @GetLetter.started -= m_Wrapper.m_UniActionsCallbackInterface.OnGetLetter;
                @GetLetter.performed -= m_Wrapper.m_UniActionsCallbackInterface.OnGetLetter;
                @GetLetter.canceled -= m_Wrapper.m_UniActionsCallbackInterface.OnGetLetter;
                @SwitchOffLight.started -= m_Wrapper.m_UniActionsCallbackInterface.OnSwitchOffLight;
                @SwitchOffLight.performed -= m_Wrapper.m_UniActionsCallbackInterface.OnSwitchOffLight;
                @SwitchOffLight.canceled -= m_Wrapper.m_UniActionsCallbackInterface.OnSwitchOffLight;
                @rightup.started -= m_Wrapper.m_UniActionsCallbackInterface.OnRightup;
                @rightup.performed -= m_Wrapper.m_UniActionsCallbackInterface.OnRightup;
                @rightup.canceled -= m_Wrapper.m_UniActionsCallbackInterface.OnRightup;
                @rightdown.started -= m_Wrapper.m_UniActionsCallbackInterface.OnRightdown;
                @rightdown.performed -= m_Wrapper.m_UniActionsCallbackInterface.OnRightdown;
                @rightdown.canceled -= m_Wrapper.m_UniActionsCallbackInterface.OnRightdown;
                @rightright.started -= m_Wrapper.m_UniActionsCallbackInterface.OnRightright;
                @rightright.performed -= m_Wrapper.m_UniActionsCallbackInterface.OnRightright;
                @rightright.canceled -= m_Wrapper.m_UniActionsCallbackInterface.OnRightright;
                @rightleft.started -= m_Wrapper.m_UniActionsCallbackInterface.OnRightleft;
                @rightleft.performed -= m_Wrapper.m_UniActionsCallbackInterface.OnRightleft;
                @rightleft.canceled -= m_Wrapper.m_UniActionsCallbackInterface.OnRightleft;
            }
            m_Wrapper.m_UniActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Walk.started += instance.OnWalk;
                @Walk.performed += instance.OnWalk;
                @Walk.canceled += instance.OnWalk;
                @Crouch.started += instance.OnCrouch;
                @Crouch.performed += instance.OnCrouch;
                @Crouch.canceled += instance.OnCrouch;
                @Climb_Up.started += instance.OnClimb_Up;
                @Climb_Up.performed += instance.OnClimb_Up;
                @Climb_Up.canceled += instance.OnClimb_Up;
                @Let_Go.started += instance.OnLet_Go;
                @Let_Go.performed += instance.OnLet_Go;
                @Let_Go.canceled += instance.OnLet_Go;
                @Grapple.started += instance.OnGrapple;
                @Grapple.performed += instance.OnGrapple;
                @Grapple.canceled += instance.OnGrapple;
                @Detach.started += instance.OnDetach;
                @Detach.performed += instance.OnDetach;
                @Detach.canceled += instance.OnDetach;
                @Grapple_Vert.started += instance.OnGrapple_Vert;
                @Grapple_Vert.performed += instance.OnGrapple_Vert;
                @Grapple_Vert.canceled += instance.OnGrapple_Vert;
                @Inhale.started += instance.OnInhale;
                @Inhale.performed += instance.OnInhale;
                @Inhale.canceled += instance.OnInhale;
                @Exhale.started += instance.OnExhale;
                @Exhale.performed += instance.OnExhale;
                @Exhale.canceled += instance.OnExhale;
                @NextHook.started += instance.OnNextHook;
                @NextHook.performed += instance.OnNextHook;
                @NextHook.canceled += instance.OnNextHook;
                @PrevHook.started += instance.OnPrevHook;
                @PrevHook.performed += instance.OnPrevHook;
                @PrevHook.canceled += instance.OnPrevHook;
                @Die.started += instance.OnDie;
                @Die.performed += instance.OnDie;
                @Die.canceled += instance.OnDie;
                @PressButton.started += instance.OnPressButton;
                @PressButton.performed += instance.OnPressButton;
                @PressButton.canceled += instance.OnPressButton;
                @Move_Box.started += instance.OnMove_Box;
                @Move_Box.performed += instance.OnMove_Box;
                @Move_Box.canceled += instance.OnMove_Box;
                @GetLetter.started += instance.OnGetLetter;
                @GetLetter.performed += instance.OnGetLetter;
                @GetLetter.canceled += instance.OnGetLetter;
                @SwitchOffLight.started += instance.OnSwitchOffLight;
                @SwitchOffLight.performed += instance.OnSwitchOffLight;
                @SwitchOffLight.canceled += instance.OnSwitchOffLight;
                @rightup.started += instance.OnRightup;
                @rightup.performed += instance.OnRightup;
                @rightup.canceled += instance.OnRightup;
                @rightdown.started += instance.OnRightdown;
                @rightdown.performed += instance.OnRightdown;
                @rightdown.canceled += instance.OnRightdown;
                @rightright.started += instance.OnRightright;
                @rightright.performed += instance.OnRightright;
                @rightright.canceled += instance.OnRightright;
                @rightleft.started += instance.OnRightleft;
                @rightleft.performed += instance.OnRightleft;
                @rightleft.canceled += instance.OnRightleft;
            }
        }
    }
    public UniActions @Uni => new UniActions(this);

    // Menu
    private readonly InputActionMap m_Menu;
    private IMenuActions m_MenuActionsCallbackInterface;
    private readonly InputAction m_Menu_PlayPause;
    public struct MenuActions
    {
        private @Inputs m_Wrapper;
        public MenuActions(@Inputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @PlayPause => m_Wrapper.m_Menu_PlayPause;
        public InputActionMap Get() { return m_Wrapper.m_Menu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MenuActions set) { return set.Get(); }
        public void SetCallbacks(IMenuActions instance)
        {
            if (m_Wrapper.m_MenuActionsCallbackInterface != null)
            {
                @PlayPause.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnPlayPause;
                @PlayPause.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnPlayPause;
                @PlayPause.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnPlayPause;
            }
            m_Wrapper.m_MenuActionsCallbackInterface = instance;
            if (instance != null)
            {
                @PlayPause.started += instance.OnPlayPause;
                @PlayPause.performed += instance.OnPlayPause;
                @PlayPause.canceled += instance.OnPlayPause;
            }
        }
    }
    public MenuActions @Menu => new MenuActions(this);
    public interface IUniActions
    {
        void OnJump(InputAction.CallbackContext context);
        void OnWalk(InputAction.CallbackContext context);
        void OnCrouch(InputAction.CallbackContext context);
        void OnClimb_Up(InputAction.CallbackContext context);
        void OnLet_Go(InputAction.CallbackContext context);
        void OnGrapple(InputAction.CallbackContext context);
        void OnDetach(InputAction.CallbackContext context);
        void OnGrapple_Vert(InputAction.CallbackContext context);
        void OnInhale(InputAction.CallbackContext context);
        void OnExhale(InputAction.CallbackContext context);
        void OnNextHook(InputAction.CallbackContext context);
        void OnPrevHook(InputAction.CallbackContext context);
        void OnDie(InputAction.CallbackContext context);
        void OnPressButton(InputAction.CallbackContext context);
        void OnMove_Box(InputAction.CallbackContext context);
        void OnGetLetter(InputAction.CallbackContext context);
        void OnSwitchOffLight(InputAction.CallbackContext context);
        void OnRightup(InputAction.CallbackContext context);
        void OnRightdown(InputAction.CallbackContext context);
        void OnRightright(InputAction.CallbackContext context);
        void OnRightleft(InputAction.CallbackContext context);
    }
    public interface IMenuActions
    {
        void OnPlayPause(InputAction.CallbackContext context);
    }
}
