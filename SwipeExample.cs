using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SwipeExample : MonoBehaviour 
{
	public Text titleText;

	public float minSwipeDistX;
		
	private Vector2 startPos;

	void Update()
	{	
		if(Input.GetMouseButtonDown(0)) {
			startPos = Input.mousePosition;
		} else if(Input.GetMouseButtonUp(0)) {
			float swipeDistHorizontal = (new Vector3(Input.mousePosition.x,0, 0) - new Vector3(startPos.x, 0, 0)).magnitude;
			
			if (swipeDistHorizontal > minSwipeDistX) 
			{
				float swipeValue = Mathf.Sign(Input.mousePosition.x - startPos.x);
				
				if (swipeValue > 0)
					titleText.text = "right swipe";
				else if (swipeValue < 0)
					titleText.text = "left swipe";
				
			}
		}
	}
}