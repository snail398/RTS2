﻿using System;
using System.Collections.Generic;
 using Battle.Buildings;
 using GameObjectView;
 using UnityEngine;
 using UnityEngine.Serialization;

 namespace Settings {
    [Serializable]
    [CreateAssetMenu(fileName = "BuildingsConfig", menuName = "Configs/BuildingsConfig", order = 1)]
    public class BuildingsConfig : ScriptableObject, ISettings {
        public List<BuildingConfig> Buildings;
        
        public void Init() {
            
        }
    }

    [Serializable]
    public class BuildingConfig {
        public string Name;
        public BuildingType BuildingType;
        public BuildingView Prefab;
        public Sprite BuildingSprite;
        public int MineralCost = 50;
        public int GasCost = 50;
        public int Width = 1;
        public int Height = 1;
        public bool CanBeUsed;
        // public BuildingType BuildingType;
    }
}
