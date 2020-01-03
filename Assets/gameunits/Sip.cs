using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sip : Unit{
    // Start is called before the first frame update
    public static Vector3 offsett; //constatnt offset of unit

    public override void setOffset(Vector3 offsett)
    {
        base.setOffset(offsett);


    }
    public void Awake()
    {
        base.setOffset(offset);
    }

}
