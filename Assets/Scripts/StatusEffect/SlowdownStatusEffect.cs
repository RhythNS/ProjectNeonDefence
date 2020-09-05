using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowdownStatusEffect
{
    public float slowdownPercentage { get; private set; }
    
    public float slowdownTime { get; set; }

    public SlowdownStatusEffect(float slowdownPercentage, float slowdownTime)
    {
        this.slowdownPercentage = slowdownPercentage;
        this.slowdownTime = slowdownTime;
    }
}
