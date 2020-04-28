using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class GameLogic : MonoBehaviour {
    //constants
    public readonly static int maximalNumberOfMoves = 6;
    private static Hrac actualPlayer;
    public static void setHracWhoIsPlaying(Hrac actualPlayer) {
        GameLogic.actualPlayer = actualPlayer;
    }
    public static Hrac getHracWhoIsPlaying() {
        return GameLogic.actualPlayer;
    }
    public static bool isItMyUnit(GameObject actualPlayerUnit)
    {
        Unit curretnUnitGameObject;
        
        curretnUnitGameObject = actualPlayerUnit.GetComponent<Unit>();
        
     

        Hrac actualPlayer = curretnUnitGameObject.getPlayer();

        /* if (actualPlayer as Hrac == null) return false;
         if (curretnUnitGameObject == null) return false;*/
        Debug.Log($"itIt My unit {getHracWhoIsPlaying() == actualPlayer}");
        return getHracWhoIsPlaying() == actualPlayer;
    }
        
    public static  int ?  getMaximumNumberOfMovesByGroup(int ? numberOfunitsInGroup) {
        
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
        return -1;
    }
    public static bool  isDistanceReacheble(TileClick startTile,TileClick destinationTile ,int  numberOfUnitsIngroup)
    {
        return (AdamsAdresing.calculatePozition(startTile, destinationTile) <= GameLogic.getMaximumNumberOfMovesByGroup(numberOfUnitsIngroup));
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
