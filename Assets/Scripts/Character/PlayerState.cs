using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState {

    PlayerController player;

    public virtual void Enter(PlayerController player)
    {
        this.player = player;
    }
    public virtual void Exit() { }
    public abstract PlayerState Update();
}
