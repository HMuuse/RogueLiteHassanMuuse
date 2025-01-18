using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryPig : NPCController
{
    public override void Start()
    {
        base.Start();

        entityType = EntityType.AngryPig;
    }
}
