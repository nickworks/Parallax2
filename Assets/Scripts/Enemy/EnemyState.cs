using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState {

    protected EnemyAI controller;

    public virtual void EnterState(EnemyAI controller) {
        this.controller = controller;
    }
    public virtual void ExitState() { }
    public abstract EnemyState UpdateState();
}
