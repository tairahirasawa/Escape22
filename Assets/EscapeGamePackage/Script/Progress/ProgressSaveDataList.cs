using System.Collections.Generic;
using UnityEngine;

public interface IProgressSaveData
{
    public StageContext.StageType prefix { get; }
    public List<string> saveKeys { get; }
}

public class AllProgressSaveDatalist
{
    public List<IProgressSaveData> progressSaveDatas = new List<IProgressSaveData>()
    {
        new stage_000_saveData(),
        new stage_001_saveData(),
        new stage_002_saveData(),
        new stage_003_saveData(),
        new stage_004_saveData(),
        new stage_005_saveData(),
        new stage_006_saveData(),
        new stage_007_saveData(),
        new stage_008_saveData(),
        new stage_009_saveData(),
        new stage_010_saveData(),
        new stage_011_saveData(),
        new stage_012_saveData(),
        new stage_013_saveData(),
        new stage_014_saveData(),
        new stage_015_saveData(),
        new stage_016_saveData(),
        new stage_017_saveData(),
        new stage_018_saveData(),
        new stage_019_saveData(),
        new stage_020_saveData(),
        new stage_021_saveData(),
        new stage_022_saveData(),
        new stage_023_saveData(),
        new stage_024_saveData(),
        new stage_025_saveData(),
        new stage_026_saveData(),
        new stage_027_saveData(),
        new stage_028_saveData(),
        new stage_029_saveData(),
        new stage_030_saveData(),
        new stage_031_saveData(),
        new stage_032_saveData(),
        new stage_033_saveData(),
        new stage_034_saveData(),
        new stage_035_saveData(),
        new stage_036_saveData(),
        new stage_037_saveData(),
        new stage_038_saveData(),
        new stage_039_saveData(),
        new stage_040_saveData(),
        new stage_041_saveData(),
        new stage_042_saveData(),
        new stage_043_saveData(),
        new stage_044_saveData(),
        new stage_045_saveData(),
        new stage_046_saveData(),
        new stage_047_saveData(),
        new stage_048_saveData(),
        new stage_049_saveData(),
        new stage_050_saveData(),

    };

    public List<ProgressData> GetProgressSaveData(string prefix)
    {
        var saveKeys = new List<string>();
        var returndata = new List<ProgressData>();

        for (int i = 0; i < progressSaveDatas.Count; i++)
        {
            if (prefix == progressSaveDatas[i].prefix.ToString())
            {
                saveKeys = progressSaveDatas[i].saveKeys;
            }
        }

        for (int i = 0; i < saveKeys.Count; i++)
        {
            returndata.Add(new ProgressData(saveKeys[i]));
        }
        return returndata;
    }
}