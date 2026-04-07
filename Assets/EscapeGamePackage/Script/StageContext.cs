public partial class GameData
{
    public StageContext.StageType currentStageType;
}

public static class StageContext
{
    public static StageType currentStageType { get => DataPersistenceManager.I.gameData.currentStageType; set => DataPersistenceManager.I.gameData.currentStageType = value; }

    public static void Initialize()
    {
        //if (currentStageType == StageType.none) currentStageType = StageType.stage_000;
        
        /*
        if (GameDirector.I.debugStageType != StageType.none)
        {
            currentStageType = GameDirector.I.debugStageType;
        }
        */
    }

    public static string GetCurrentPrefix()
    {
        return currentStageType.ToString();
    }

    public static string GetCurrentPrefixWithSeparator()
    {
        return currentStageType.ToString() + "_";
    }

    public enum StageType
    {
        none, stage_000, stage_001, stage_002, stage_003, stage_004, stage_005, stage_006, stage_007,
        stage_008, stage_009, stage_010, stage_011, stage_012, stage_013, stage_014, stage_015, stage_016,
        stage_017, stage_018, stage_019, stage_020, stage_021, stage_022, stage_023, stage_024, stage_025, stage_026,
        stage_027,stage_028,stage_029,stage_030,stage_031,stage_032,stage_033,stage_034,stage_035,stage_036,stage_037,
        stage_038,stage_039,stage_040,stage_041,stage_042,stage_043,stage_044,stage_045,stage_046,stage_047,stage_048,
        stage_049,stage_050
    }
    
}
