using UnityEngine;

public partial class GameData
{
    public int S_CoinCount;
}

public static class Coin
{
    public static int CoinCount
    {
        get => DataPersistenceManager.I.gameData.S_CoinCount;
        private set => DataPersistenceManager.I.gameData.S_CoinCount = value;
    }

    public static void AddCoin(int amount)
    {
        CoinCount += amount;
    }

    public static bool UseCoin(int amount)
    {
        if (CoinCount < amount) return false;
        CoinCount -= amount;
        return true;
    }

}
