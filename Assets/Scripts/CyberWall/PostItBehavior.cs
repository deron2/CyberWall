using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostItBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit raycastHit;
            if (Physics.Raycast(raycast, out raycastHit))
            {
                if (raycastHit.collider.CompareTag("postit"))
                {
                    GameObject postIt = raycastHit.collider.gameObject;
                    postIt.transform.parent.Rotate(0,180,0);
                    postIt.transform.parent.localPosition = new Vector3(postIt.transform.parent.localPosition.x, postIt.transform.parent.localPosition.y, -1*postIt.transform.parent.localPosition.z);
 
                }
            }
        }
    }
}
