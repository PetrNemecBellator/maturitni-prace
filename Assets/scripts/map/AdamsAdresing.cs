using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public static class AdamsAdresing 
{
    public static int  calculatePozition(TileClick startTile, TileClick destiNationTile) {
        Vector3 vStartTile = startTile.getCoordinetsForDistanceCalculation();
        Vector3 vDestinationTile = destiNationTile.getCoordinetsForDistanceCalculation();

        return (int)(Mathf.Abs(vStartTile.x - vDestinationTile.x) + Mathf.Abs(vStartTile.y - vDestinationTile.y)
               + Mathf.Abs(vStartTile.z - vDestinationTile.z));
    }
}
