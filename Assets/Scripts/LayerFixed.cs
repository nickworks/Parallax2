using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerFixed : MonoBehaviour {

    public static float separation = 100;

    public int z = 0;
    public bool allowOverlappingColliders = false;
    private float phaseTimer = 0;
    public float phaseTime = .5f;

    Vector3 targetPosition
    {
        get
        {
            return new Vector3(transform.position.x, transform.position.y, z * separation);
        }
    }
    void OnValidate()
    {
        Snap();
    }
    void Start() {
        
    }
    public void GoBack()
    {
        if (CanPhase(true))
        {
            z++;
            Snap();
            phaseTimer = phaseTime;
        }
    }
    public void ComeForward()
    {
        if (CanPhase(false))
        {
            z--;
            Snap();
            phaseTimer = phaseTime;
        }
    }
    public bool CanPhase(bool isGoingBack)
    {
        if (allowOverlappingColliders) return true;
        Collider collider = transform.GetComponent<Collider>();
        if (collider)
        {
            float posZ = transform.position.z;
            Vector3[] origins = new Vector3[] {
                transform.position,
                new Vector3(collider.bounds.min.x, collider.bounds.min.y, posZ),
                new Vector3(collider.bounds.min.x, collider.bounds.max.y, posZ),
                new Vector3(collider.bounds.max.x, collider.bounds.min.y, posZ),
                new Vector3(collider.bounds.max.x, collider.bounds.max.y, posZ)
            };
            if (DoRaycastsHit(origins, isGoingBack)) return false;
        }
        return true;
    }
    private bool DoRaycastsHit(Vector3[] origins, bool isGoingBack)
    {
        foreach(Vector3 pt in origins)
        {
            Ray ray = new Ray(pt, isGoingBack ? Vector3.forward : Vector3.back);
            //Debug.DrawRay(ray.origin, ray.direction * separation, Color.green, 5);
            if (Physics.Raycast(ray, separation)) return true;
        }
        return false;
    }
    void Snap()
    {
        transform.position = targetPosition;
    }
	void LateUpdate () {

        int v = (int)Input.GetAxisRaw("Vertical");
        if (phaseTimer > 0) phaseTimer -= Time.deltaTime;
        else if (v > 0) GoBack();
        else if (v < 0) ComeForward();

        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 10);
	}
    public float GetZPosition(int z)
    {
        return z * separation;
    }
}
