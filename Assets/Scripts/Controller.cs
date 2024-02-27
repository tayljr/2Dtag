using UnityEngine;
using UnityEngine.InputSystem;


// This is ONLY for the "Generate C#" option in the ActionMap file
public class ControllerActionMapTest : MonoBehaviour
{
   MainControlls mainControls;
  
   // Spawn new instance of the Action map definitions
   // Note: It's actually ok to spawn multiple instances of this. Slightly dodgy but is fine as it's just information
   // You'll eventually want to put this setup 'new' stuff in something like an InputManager
   private void Awake()
   {
      // This is ONLY for the "Generate C#" option in the ActionMap
      mainControls = new MainControlls();
      mainControls.Enable();
   }


   // Start is called before the first frame update
   void OnEnable()
   {
       // Simple action
       // Note: I'm subscribing to cancelled also, so we get letting go values of zero
       //mainControls.Main.Fire.performed += FireOnperformed;
       //mainControls.Main.Fire.canceled += FireOnperformed;
      
       // One axis float value
       // Note: I'm subscribing to cancelled also, so we get letting go values of zero
       mainControls.Main.Horizontal.performed += HorizontalOnperformed;
       mainControls.Main.Horizontal.canceled += HorizontalOnperformed;
       mainControls.Main.Jump.performed += VerticalOnperformed;
       mainControls.Main.Jump.canceled += VerticalOnperformed;
      
       // Combined double axis using Vector2
       // Note: I'm subscribing to cancelled also, so we get letting go values of zero
       //mainControls.Main.Movement.canceled += MovementOnperformed;
       //mainControls.Main.Movement.performed += MovementOnperformed;
   }
  
   // Don't forget to unsubscribe
   void OnDisable()
   {
       // Note: I'm subscribing to cancelled also, so we get letting go values of zero
       //mainControls.Main.Fire.performed -= FireOnperformed;
       //mainControls.Main.Fire.canceled -= FireOnperformed;
      
       // Note: I'm subscribing to cancelled also, so we get letting go values of zero
       mainControls.Main.Horizontal.performed -= HorizontalOnperformed;
       mainControls.Main.Horizontal.canceled -= HorizontalOnperformed;
       mainControls.Main.Jump.performed -= VerticalOnperformed;
       mainControls.Main.Jump.canceled -= VerticalOnperformed;
      
       // Note: I'm subscribing to cancelled also, so we get letting go values of zero
       //mainControls.Main.Movement.performed -= MovementOnperformed;
       //mainControls.Main.Movement.canceled -= MovementOnperformed;
   }


   private void HorizontalOnperformed(InputAction.CallbackContext obj)
   {
	   // Note: How did I know this is a float? Because in the ActionMap editor the 'Action Type' is set to 'Value' and the 'Control Type' is set to 'Axis'
	   Debug.Log(obj.ReadValue<float>());
   }
   
   private void VerticalOnperformed(InputAction.CallbackContext obj)
   {
	   // Note: How did I know this is a float? Because in the ActionMap editor the 'Action Type' is set to 'Value' and the 'Control Type' is set to 'Axis'
       Debug.Log(obj.performed);
   }
  
   private void MovementOnperformed(InputAction.CallbackContext obj)
   {
       // Note: How did I know this is a Vector2? Because in the ActionMap editor the 'Action Type' is set to 'Value' and the 'Control Type' is set to 'Vector2'
       Debug.Log(obj.ReadValue<Vector2>());
   }


   private void FireOnperformed(InputAction.CallbackContext obj)
   {
       // I'm checking whether it was pressed or released
       if (obj.phase == InputActionPhase.Performed)
       {
          Debug.Log("JUMP!");
       }
       if (obj.phase == InputActionPhase.Canceled)
       {
          Debug.Log("Cancelled JUMP!");
       }
   }
}