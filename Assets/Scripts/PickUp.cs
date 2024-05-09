using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp: MonoBehaviour
{
    [SerializeField] private LayerMask PickupMask;
    [SerializeField] private Camera PC;
    [SerializeField] private Transform BasePickupTarget;
    [SerializeField] private Transform PickupTargetTool;

    private Transform PickupTarget;
    [Space]
    [SerializeField] private float PickupRange;
    public GameObject CurrentObject = null;

    void Update()
    {
        GameObject go;
        if(Input.GetKeyDown(KeyCode.E)) 
        {
            if (CurrentObject) 
            {
                CurrentObject.SendMessage("PutDown", true, SendMessageOptions.DontRequireReceiver);
                go = Instantiate(CurrentObject, BasePickupTarget.transform.position, CurrentObject.transform.rotation);
                ManageObject(go, true);
                Destroy(CurrentObject);
                CurrentObject = null;
                go = null;
                return;
            }

            Ray CR = PC.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            
            if(Physics.Raycast(CR, out RaycastHit HitInfo, PickupRange, PickupMask)) 
            {
                go = HitInfo.transform.gameObject;
                CurrentObject = Instantiate(go, BasePickupTarget.transform.position, transform.root.rotation, BasePickupTarget.transform);
                CurrentObject.transform.rotation = transform.rotation;
                Destroy(go);
                go = null;
                ManageObject(CurrentObject, false);
                CurrentObject.SendMessage("PickUp", true, SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    private void ManageObject(GameObject go, bool opt)
    {
        Rigidbody rb;
        Collider col;
        rb = go.GetComponent<Rigidbody>();
        col = go.GetComponent<Collider>();
        if(rb) {
            rb.isKinematic = !opt;
            rb.detectCollisions = opt;
            rb.useGravity = opt;
        }

        if(col != null) {
            col.enabled = opt;
        }
    }
}