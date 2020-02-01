using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewShipParameter", menuName = "Scriptable Object/ShipParameter")]
public class ShipParameters : BaseVariable
{
    [Space]
    public float moveSpeed = 12;

    [Range(.1f, 1f)]public float clampMovement = .95f;
    public float maxLookInclination = .25f;
    public float lookSpeed = 6000;
    public float forwardSpeed = 12;
}
