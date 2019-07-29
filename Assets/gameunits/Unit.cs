using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    protected  Vector3 offset = new Vector3(0,0); //¨musim doplnit offsety jednotek
    private GameObject Player;

    private GameObject getPlayer()
    {
        return this.Player;
    }
    private void setPlayer( GameObject newPlayer)
    {
        this.Player = newPlayer;
    }

    public virtual void setOffset(Vector3 offsett)
    {
       this.offset = offsett;
    }
    public  Vector3 getoffset()
    {
        return this.offset;
    }
    public static Unit unitFight(Unit attackingUnit, Unit passiveUnit)
    {
        //funkce co vraci viteze souboje
        int allPossibilities = 0;
        do {
            allPossibilities++;
            if (attackingUnit is Vez && passiveUnit is Stit)
            {

                passiveUnit.gameObject.transform.parent.gameObject.GetComponent<TileClick>().changeToUnMark();
                passiveUnit.gameObject.transform.parent.gameObject.GetComponent<TileClick>().unSetUnit();
                return attackingUnit;

            } else if (attackingUnit is Sip && passiveUnit is Vez)
            {

                passiveUnit.gameObject.transform.parent.gameObject.GetComponent<TileClick>().changeToUnMark();
                passiveUnit.gameObject.transform.parent.gameObject.GetComponent<TileClick>().unSetUnit();
                return attackingUnit;
            } else if (attackingUnit is Stit && passiveUnit is Sip)
            {
                passiveUnit.gameObject.transform.parent.gameObject.GetComponent<TileClick>().changeToUnMark();
                passiveUnit.gameObject.transform.parent.gameObject.GetComponent<TileClick>().unSetUnit();

                return attackingUnit;
            }
            if (attackingUnit.GetType() == passiveUnit.GetType())
            {//pokud utoci stejna jednotka na stejnou neni vitez

                return null;
            }

            Unit helpunit = attackingUnit;
            attackingUnit = passiveUnit;
            passiveUnit = helpunit;

        } while (allPossibilities< 2);

        throw new System.Exception("Incorect fight option line 36", new System.Exception());
        return null;
    }
}
