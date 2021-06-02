using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public int saveVersion = 1;
    public Vector3 playerPosition;
    public int collectedCollectables;
    public int collectedElements;
    public string dateTimeOfSave;
}
