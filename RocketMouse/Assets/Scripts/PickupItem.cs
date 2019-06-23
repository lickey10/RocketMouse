﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PickupItem
{
    public GameObject PickupObject;
    //public float MinXProximity = -1f;
    //public float MaxXProximity = -1f;
    public int DensityMax = 2;
    public int DensityMin = 0;
    public Vector2 Buffer = new Vector2();
}
