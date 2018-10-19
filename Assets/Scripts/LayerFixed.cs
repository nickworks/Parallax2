using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerFixed : MonoBehaviour {

    public static float separation = 10;

    public int z = 0;

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

    }
    void Start() {
        
    }
    public void GoBack()
    {
        z++;
    }
    public void ComeForward()
    {
        z--;
    }
    void Snap()
    {
        transform.position = targetPosition;
    }
	void LateUpdate () {

        int v = (int)Input.GetAxisRaw("Vertical");
        if (phaseTimer > 0)
        {
            phaseTimer -= Time.deltaTime;
            print("countdown...");
        }
        else if (v != 0)
        {
            z += v;
            Snap();
            phaseTimer = phaseTime;
        }

        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 10);
	}
}
