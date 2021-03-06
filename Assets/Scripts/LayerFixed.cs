using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This allow objects to snap themselves to layers.
/// A player or AI controller can use this script to phase jump between layers.
/// The behavior affects the object's world position, and does NOT change
/// the scene graph hierarchy.
/// </summary>
public class LayerFixed : MonoBehaviour {
    /// <summary>
    /// The distance between layers (in meters)
    /// </summary>
    public static float separation = 100;
    /// <summary>
    /// The layer the player is on.
    /// </summary>
    public int z = 0;
    /// <summary>
    /// Whether or not to snap to z-position (with full layer separation applied) in editor.
    /// </summary>
    public bool snapToLayerInEditor = false;
    /// <summary>
    /// This controls whether or not this object checks for collisions when phase jumping.
    /// </summary>
    public bool ignoreCollidersWhenPhasing = false;
    /// <summary>
    /// How much to inset this object's bounds for phase-jump collision checks. Measured in meters.
    /// </summary>
    public float phaseCollisionTolerance = .1f;
    /// <summary>
    /// How much time (in seconds) before the player can phase jump again.
    /// </summary>
    private float phaseTimer = 0;
    /// <summary>
    /// How much time (in seconds) the player must wait between phase jumps.
    /// </summary>
    public float phaseCooldown = .5f;
    /// <summary>
    /// the z position this was previously located at 
    /// </summary>
    float zPrev;
    /// <summary>
    /// this tracks what snapToLayerInEditor was set to the last time OnValidate was called
    /// </summary>
    bool snapPrev = false;

    /// <summary>
    /// Gets this object's projected position.
    /// </summary>
    Vector3 targetPosition {
        get {
            return new Vector3(transform.position.x, transform.position.y, z * separation);
        }
    }


    /// <summary>
    /// Called when a variable on this component is changed in editor. This currently changes the z position when snapToLayerInEditor is changed
    /// </summary>
    void OnValidate() {

        if (transform.position != targetPosition) {
            zPrev = transform.position.z;
        }

        if (snapToLayerInEditor) {
            Snap();
        } else if (!snapToLayerInEditor && snapPrev) {
            SnapBack();
        }

        snapPrev = snapToLayerInEditor;
    }
    /// <summary>
    /// Phase jump away from the camera (z+)
    /// </summary>
    public void GoBack() {
        if (CanPhase(true)) {
            z++;
            Snap();
            phaseTimer = phaseCooldown;
        }
    }
    /// <summary>
    /// Phase jump towards the camera (z-)
    /// </summary>
    public void ComeForward() {
        if (CanPhase(false)) {
            z--;
            Snap();
            phaseTimer = phaseCooldown;
        }
    }
    /// <summary>
    /// Checks to see if this object can phase jump. This casts rays to check for colliders on the layer above or below the current layer.
    /// </summary>
    /// <param name="isGoingBack">If true cast rays Forward(z+), otherwise cast Back(z-).</param>
    /// <returns>Whether or not this object can jump without phasing into other colliders.</returns>
    public bool CanPhase(bool isGoingBack) {
        if (phaseTimer > 0) return false;
        if (ignoreCollidersWhenPhasing) return true;

        Collider collider = GetComponent<CharacterController>();
        if (!collider) collider = GetComponent<Collider>();

        if (collider)
        {
            float posZ = transform.position.z;
            Bounds bounds = collider.bounds;
            bounds.Expand(-phaseCollisionTolerance);

            Vector3[] origins = new Vector3[] {
                transform.position,
                new Vector3(bounds.min.x, bounds.min.y, posZ),
                new Vector3(bounds.min.x, bounds.max.y, posZ),
                new Vector3(bounds.max.x, bounds.min.y, posZ),
                new Vector3(bounds.max.x, bounds.max.y, posZ)
            };
            foreach (Vector3 pt in origins) {
                Ray ray = new Ray(pt, isGoingBack ? Vector3.forward : Vector3.back);
                Debug.DrawRay(ray.origin, ray.direction * separation, Color.green, 5);
                // raycast hits another collider, so we can't phase jump safely
                RaycastHit[] hits = Physics.RaycastAll(ray, separation);
                foreach(RaycastHit hit in hits)
                {
                    if (hit.collider.isTrigger) continue; // triggers are okay to phase into
                    return false;
                } // end foreach for hits
                
            } // end foreach for pts
        } // end if(collider)
        return true;
    }

    /// <summary>
    /// Snaps position to the current z layer.
    /// </summary>
    void Snap() {
        transform.position = targetPosition;
    }

    /// <summary>
    /// When snapToLayerInEditor is set to false this function is called to reset the z location. It sets the z to the previous non index z location.  
    /// If that value was not stored or set, ie. is 0, we multiply 10 by the index number and   
    /// </summary>
    void SnapBack() {
        if (zPrev == 0) {
            zPrev = z * 10;
        }
        transform.position = new Vector3(transform.position.x,transform.position.y,zPrev);
    }

    /// <summary>
    /// Handles input and eases the object to its target layer.
    /// </summary>
    void LateUpdate() {

        if (phaseTimer > 0) phaseTimer -= Time.deltaTime;
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.unscaledDeltaTime * 10);
    }
    /// <summary>
    /// Cancels the phase cooldown timer.
    /// </summary>
    public void CancelTimer() {
        phaseTimer = 0;
    }
}
