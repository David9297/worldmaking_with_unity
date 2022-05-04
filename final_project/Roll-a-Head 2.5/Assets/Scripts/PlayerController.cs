using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Include the namespace required to use Unity UI and Input System
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour 
{
	public OSC osc;
	
	// Create public variables for player speed, and for the Text UI game objects
	public float speed;
	public TextMeshProUGUI countText;
	public GameObject winTextObject;

    private float movementX;
    private float movementY;

	private Rigidbody rb;
	private int count;

	int OSCButton, buttonOne, buttonTwo, buttonThree, buttonFour, joystickPress;

	// At the start of the game..
	void Start ()
	{
		// Assign the Rigidbody component to our private rb variable
		rb = GetComponent<Rigidbody>();

		// Set the count to zero 
		count = 0;

		SetCountText ();

        // Set the text property of the Win Text UI to an empty string, making the 'You Win' (game over message) blank
         winTextObject.SetActive(false);

		 osc.SetAddressHandler("/publish/func", OnReceiveData);
	}

	void FixedUpdate ()
	{
		// Create a Vector3 variable, and assign X and Z to feature the horizontal and vertical float variables above
		Vector3 movement = new Vector3 (movementX, 0.0f, movementY);

		rb.AddForce (movement * speed);
	}

	void OnTriggerEnter(Collider other) 
	{
		// ..and if the GameObject you intersect has the tag 'Pick Up' assigned to it..
		if (other.gameObject.CompareTag ("PickUp"))
		{
			other.gameObject.SetActive (false);

			// Add one to the score variable 'count'
			count = count + 1;

			// Run the 'SetCountText()' function (see below)
		    SetCountText ();
		}
	}

    void OnMove(InputValue value)
    {
        Vector2 v = value.Get<Vector2>();

        movementX = v.x;
        movementY = v.y;
    }

    void SetCountText()
	{
		countText.text = "Eggs Collected: " + count.ToString();

		if (count >= 52) 
		{
            // Set the text value of your 'winText'
            winTextObject.SetActive(true);
		}
    }
	void Update() 
	{
		if(OSCButton == 1) 
		{

		}

		if(buttonOne == 1) 
		{
			movementY += 0.005f;
		}

		if (buttonTwo == 1)
		{
			movementX -= 0.005f;
		}

		if(buttonThree == 1)
		{
			movementY -= 0.005f;
		}

		if(buttonFour == 1)
		{
			movementX += 0.005f;
		}

		if(joystickPress == 1)
		{
        	transform.Translate(Vector3.up * 3, Space.World);
		}
	}

	void OnReceiveData(OscMessage message)
	{
		OSCButton = message.GetInt(0);
		buttonOne = message.GetInt(1);
		buttonTwo = message.GetInt(2);
		buttonThree = message.GetInt(3);
		buttonFour = message.GetInt(4);
		joystickPress = message.GetInt(7);

		Debug.Log("OSC Button State: " + OSCButton);
		Debug.Log("Up Button State: " + buttonOne);
		Debug.Log("Left Button State: " + buttonTwo);
		Debug.Log("Down Button State: " + buttonThree);
		Debug.Log("Right Button State: " + buttonFour);
		Debug.Log("Joystick Button State: " + joystickPress);
	}
}