using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public   class GameLogic : MonoBehaviour {
    private readonly static int maximalNumberOfMoves = 8;

    
    public static int getMaximumNumberOfMoves(int numberOfunitsInGroup)
    {
        //returns maxial number of moves by unit
        return 1;
    }
    public static Unit unitFight(Unit attackingUnit, Unit passiveUnit)
    {
        //funkce co vraci viteze souboje
        int allPossibilities = 0;
        do
        {
            allPossibilities++;
            if (attackingUnit is Vez && passiveUnit is Stit)
            {

                passiveUnit.gameObject.transform.parent.gameObject.GetComponent<TileClick>().changeToUnMark();
                passiveUnit.gameObject.transform.parent.gameObject.GetComponent<TileClick>().unSetUnit();
                return attackingUnit;

            }
            else if (attackingUnit is Sip && passiveUnit is Vez)
            {

                passiveUnit.gameObject.transform.parent.gameObject.GetComponent<TileClick>().changeToUnMark();
                passiveUnit.gameObject.transform.parent.gameObject.GetComponent<TileClick>().unSetUnit();
                return attackingUnit;
            }
            else if (attackingUnit is Stit && passiveUnit is Sip)
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

        } while (allPossibilities < 2);

        throw new System.Exception("Incorect fight option ", new System.Exception());
        return null;
    }
}
