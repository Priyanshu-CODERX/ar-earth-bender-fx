using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


public class ARPlacementManager : MonoBehaviour
{
    // Detect User's Touch
    // Project/Shoot a Raycast
    // Instantiate the object where the raycast was shot

    public ARSessionOrigin sessionOrigin;
    public GameObject _Object;

    public List<ARRaycastHit> raycastHits = new List<ARRaycastHit>();
    private GameObject instantiatedObject;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            bool collision = sessionOrigin.GetComponent<ARRaycastManager>().Raycast(Input.mousePosition, raycastHits, TrackableType.PlaneWithinPolygon);

            if (collision)
            {
                instantiatedObject = Instantiate(_Object);

                // Disable all planes
                foreach (var plane in sessionOrigin.GetComponent<ARPlaneManager>().trackables)
                {
                    plane.gameObject.SetActive(false);
                }

                // Disable plane manager
                sessionOrigin.GetComponent<ARPlaneManager>().enabled = false;

                float randomScale = Random.Range(0.2f, 1f);
                instantiatedObject.transform.localScale = new Vector3(randomScale, randomScale, randomScale);

                instantiatedObject.transform.position = raycastHits[0].pose.position;
            }
        }
    }
}
