using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Vez :Unit
{
    public static Vector3 offsett = new Vector3(-0.02F,-0.16F);//constatnt offset of unit

    public void Awake()
    {
        base.setOffset(offset);
    }
    public override void setOffset(Vector3 offsett)
    {
        base.setOffset(offsett);
    }





}


