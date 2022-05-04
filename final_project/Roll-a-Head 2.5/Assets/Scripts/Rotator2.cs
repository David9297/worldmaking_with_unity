using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator2 : MonoBehaviour
{
    public OSC osc;

    float horizontalSpeed = 2.0f;
    float verticalSpeed = 2.0f;

    float joystickX, joystickY;

	// At the start of the game..
	void Start ()
	{
		osc.SetAddressHandler("/publish/func", OnReceiveData);
	}

    // Update is called once per frame
    void Update()
    {
        // Get the mouse delta. This is not in the range -1...1
        float h = horizontalSpeed * Input.GetAxis("Mouse X");
        float v = verticalSpeed * Input.GetAxis("Mouse Y");

        joystickX = h;
        joystickY = v;

        transform.Rotate(joystickX, joystickY, 0);
    }
    
    void OnReceiveData(OscMessage message)
	{
		joystickX = message.GetInt(5);
        joystickY = message.GetInt(6);

		Debug.Log("Joystick Button X Move State: " + joystickX);
        Debug.Log("Joystick Button Y Move State: " + joystickY);
	}
}