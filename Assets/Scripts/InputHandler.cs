using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour {

    public Camera inputCamera;

    private GameObject touchObject;
    private float turnSpeed = 10f;
	
	// Update is called once per frame
	void Update () {
		
        if (Input.touchCount == 1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Ray raycast = inputCamera.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit raycastHit;
                if (Physics.Raycast(raycast, out raycastHit))
                {
                    if (raycastHit.collider != null)
                    {
                        touchObject = raycastHit.transform.gameObject;
                    }
                }
            }

            if (Input.GetTouch(0).phase == TouchPhase.Moved && touchObject != null)
            {
                //Find figer movement direction
                Vector2 prevPos = Input.GetTouch(0).position - Input.GetTouch(0).deltaPosition;
                Vector2 moveDir = Input.GetTouch(0).position - prevPos;
                
                //Rotate the object in accordance to the variance of the x component
                if (moveDir.x > 0) {
                    touchObject.transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
                }
                if (moveDir.x < 0)
                {
                    touchObject.transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
                }
            }

            if (Input.GetTouch(0).phase == TouchPhase.Ended && touchObject != null)
            {
                touchObject = null;
            }
        }

        if (Input.touchCount == 2)
        {
            // Store both touches.
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 rayPos = touchZero.position + (touchOne.position - touchZero.position) * 0.5f;

            Ray raycast = inputCamera.ScreenPointToRay(rayPos);
            RaycastHit raycastHit;

            if (Physics.Raycast(raycast, out raycastHit))
            {
                if (raycastHit.collider != null)
                {
                    touchObject = raycastHit.transform.gameObject;

                    // Find the position in the previous frame of each touch.
                    Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                    Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                    // Find the magnitude of the vector (the distance) between the touches in each frame.
                    float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                    float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

                    // Find the difference in the distances between each frame.
                    float deltaMagDiff = touchDeltaMag - prevTouchDeltaMag;

                    touchObject.transform.localScale += new Vector3(deltaMagDiff*0.5f, deltaMagDiff*0.5f, deltaMagDiff*0.5f);
                }

                touchObject = null;
            }
        }
    }
}
