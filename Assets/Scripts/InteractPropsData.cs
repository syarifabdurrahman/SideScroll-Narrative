using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Prop Data")]
public class InteractPropsData : ScriptableObject
{
    public string propName;

    [TextArea(1,3)]
    public string description;
}
