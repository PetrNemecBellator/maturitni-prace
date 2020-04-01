using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Unit : MonoBehaviour
{
    protected Vector3 offset = new Vector3(0, 0); //musim doplnit offsety jednotek

    protected Hrac player;
    protected int? groupNumber = null;


    private static int maximumGroupNumber = -1;//last last given value to group

    public void initUnit(Hrac player, int groupNumber, bool isInSameGroupAsPreviousGroup)
    {
        Debug.Log($"in unit groupNumber {groupNumber}");
        if (isInSameGroupAsPreviousGroup)
        {
            this.player = player;
            this.groupNumber = (maximumGroupNumber);

        }
        else
        {
            // can be add only higer or same number as maximum
            Unit.incrementGroupNumber();
            this.player = player;
            this.groupNumber = (maximumGroupNumber);
        }

    }
    public void setCoordinatesOfunit(Vector2 coordinatesOfunit)
    {
        this.gameObject.transform.position = coordinatesOfunit;
    }
    public Vector2 getCoordinatesOfUnit()
    {
        return this.transform.position;
    }

    public int? getGroupNumber()
    {
        return this.groupNumber;
    }
    public void setGroupNumber(int? groupNumber)
    {
        this.groupNumber = groupNumber;
    }

    public Hrac getPlayer()
    {
        return this.player;
    }
    public void setPlayer(Hrac player)
    {
        this.player = player;
    }
    public virtual void setOffset(Vector3 offsett)
    {
        this.offset = offsett;
    }
    public Vector3 getoffset()
    {
        return this.offset;
    }

    public Unit getUnit()
    {
        return this;
    }

    private static void incrementGroupNumber()
    {
        //increments group number
        maximumGroupNumber++;
    }

}
