using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulkEditingFeatures : MonoBehaviour {



    public bool snapAllLayersInEditor = false;

    bool snapAllLayersPrev = false;

    public bool turnBlockingVolumesOff = false;

    bool turnOffBlockingVolumesPrev = false;




    private void OnValidate() {
        if (snapAllLayersInEditor && !snapAllLayersPrev) {
            //set the snapToLayerInEditor variable on all objects with the layer frixed component to true

        } else if (!snapAllLayersInEditor && snapAllLayersPrev) {
            //set the snapToLayerInEditor variable on all objects with the layer frixed component to false
        }

        if (turnBlockingVolumesOff && !turnOffBlockingVolumesPrev) {
            //setactive as false for all objects with the blocking volume tag

        } else if(!turnBlockingVolumesOff && turnOffBlockingVolumesPrev) {
            //setactive as true for all objects with the blocking volume tag
        }

        snapAllLayersPrev = snapAllLayersInEditor;

        turnOffBlockingVolumesPrev = turnBlockingVolumesOff;
    }

    
}
