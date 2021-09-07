using System;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Object = System.Object;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class CameraControl : MonoBehaviour
{

	public int speed = 10;
	public int edgeScreen = 20;
	public float marginLeftRight = 5f;
	public float marginUpDown = 2f;
	public float resolutionUpdateTime = 1f;
	private Dictionary<string, float> borderCord= new Dictionary<string, float>();
	private Vector2 prevScreenSize;
	private Camera mainCamera;
    

    void Awake()
    {
	    mainCamera = Camera.main;
	    SetCameraBorders();
	    InvokeRepeating(nameof(CheckResolutionChange), resolutionUpdateTime,resolutionUpdateTime);
	}
	
	void Update()
	{
		MoveCamera();
	}

	private void MoveCamera()
	{
		Vector3 mousePos = Input.mousePosition;

		bool moveConditionXmouse = !((mousePos.x > edgeScreen) && (mousePos.x < Screen.width - edgeScreen));
		bool moveConditionYmouse = !((mousePos.y > edgeScreen) && (mousePos.y < Screen.height - edgeScreen));

		if (moveConditionXmouse || moveConditionYmouse || (Input.GetAxis("Horizontal") != 0) ||
		    (Input.GetAxis("Vertical") != 0))
		{
			Vector3 moveByAxis = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
			Vector3 moveByMouse = new Vector3(0, 0, 0);
			if (moveConditionXmouse || moveConditionYmouse)
				moveByMouse = new Vector3(mousePos.x - Screen.width / 2, mousePos.y - Screen.height / 2, 0);
			Vector3 moveSum = moveByAxis + moveByMouse.normalized;
			
			if (moveSum.x > 0)
			{
				if (transform.position.x > borderCord["Right"])
					moveSum.x = 0;
			}
				else
			{
				if (transform.position.x < borderCord["Left"])
					moveSum.x = 0;
			}
			
			if (moveSum.y > 0)
			{
				if (transform.position.y > borderCord["Up"])
					moveSum.y = 0;
			}
			else
			{
				if (transform.position.y < borderCord["Down"])
					moveSum.y = 0;
			}
			
			transform.position += moveSum.normalized * Time.deltaTime * speed;
		}
	}
	private void SetCameraBorders()
	{
		borderCord.Clear();
		Rect cameraZone = mainCamera.pixelRect;
        Vector3 cameraTopRight = new Vector3((cameraZone.width), (cameraZone.height), 0);
        Vector3 cameraBottomLeft = new Vector3(cameraZone.x, cameraZone.y, 0);
        Vector3 cameraBorders = mainCamera.ScreenToWorldPoint(cameraTopRight) -
                                mainCamera.ScreenToWorldPoint(cameraBottomLeft);
        
        borderCord.Add("Left", ConstantValues.MoveBorderLeft+cameraBorders.x/2-marginLeftRight);
        borderCord.Add("Right", ConstantValues.MoveBorderRight-cameraBorders.x/2+marginLeftRight);
        borderCord.Add("Up", ConstantValues.MoveBorderUp-cameraBorders.y/2+marginUpDown);
        borderCord.Add("Down", ConstantValues.MoveBorderDown+cameraBorders.y/2-marginUpDown);
        prevScreenSize = new Vector2(cameraZone.width, cameraZone.height);
	}

	private void CheckResolutionChange()
	{
		if (!prevScreenSize.Equals(new Vector2(mainCamera.pixelRect.width, mainCamera.pixelRect.height)))
			SetCameraBorders();
	}
}
