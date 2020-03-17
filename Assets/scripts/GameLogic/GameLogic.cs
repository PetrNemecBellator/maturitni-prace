using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class GameLogic : MonoBehaviour {
    //constants
    public readonly static int maximalNumberOfMoves = 6;

    private static int getMaximumNumberOfMovesByGroup(int numberOfunitsInGroup) {
        
        //returns maxial number of moves by unit
        if (numberOfunitsInGroup == 1)
        {
            return maximalNumberOfMoves;
        }
        else if (numberOfunitsInGroup == 2)
        {
            return maximalNumberOfMoves / numberOfunitsInGroup;
        }
        else if (numberOfunitsInGroup >= 3) return (maximalNumberOfMoves / numberOfunitsInGroup) + 2;

        throw new System.Exception("No other value is allowed for maximum number of moves");
    }
    public static bool isPosibleForCurentGroupToMoveThatFar(TileClick oldTile, TileClick destinationTile)
    {
        //function witch determin if its posivle move as far as destination tile is
        int groupNumber = (int)oldTile.transform.GetComponent<Unit>().getGroupNumber();

        //  |číslo aktualni hodnota - cil poličko| = počet tahů


        //distance is dependet on the borthers of the triangle it is not straight line
        // Vector2 distance = new Vector2(destinationTile.getCoordinatesInMatrix().x - oldTile.getCoordinatesInMatrix().x

        //return true false dependitn on getMaximumNumberOfMovesByGroup() and maximal number of moves
        return false;//

        //mozna bude nejlepsi kdyz to udelam podle path fiding algoritmu
       // zmen znazorneni pohybu a dohledu

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
    public void manageAllConseguencesOfmovement(Unit unit, Hrac actualPlayer)
    {
        throw new System.Exception("Function was not implemented yet");
    }
}
