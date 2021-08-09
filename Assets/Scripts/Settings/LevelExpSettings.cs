using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Settings
{
    [Serializable]
    [CreateAssetMenu(fileName = "LevelExpSettings", menuName = "Settings/LevelExpSettings", order = 1)]
    public class LevelExpSettings : ScriptableObject, ISettings
    {
        [FormerlySerializedAs("_LevelExpDatas")] [SerializeField] 
        public List<LevelExpData> LevelExpDatas;

        public Dictionary<int, int> LevelExpMap;
        
        public void Init()
        {
            LevelExpMap = new Dictionary<int, int>();
            foreach (var levelExpData in LevelExpDatas)
            {
                LevelExpMap.Add(levelExpData.Level, levelExpData.MaxExp);
            }
        }
    }

    [Serializable]
    public class LevelExpData
    {
        public int Level;
        public int MaxExp;
    }
}