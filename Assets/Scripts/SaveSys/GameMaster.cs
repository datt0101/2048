using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NGS.ExtendableSaveSystem
{
    [RequireComponent(typeof(SaveMaster))]
    public class GameMaster : MonoBehaviour
    {
        static public GameMaster instance;
        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(instance);


        }
        public void SaveGame()
        {
            GetComponent<SaveMaster>().Save(Application.persistentDataPath + "/", "save", ".data");
            Debug.Log("Game saved");
        }

        public void LoadGame()
        {
            GetComponent<SaveMaster>().Load(Application.persistentDataPath + "/", "save", ".data");
            Debug.Log("Game loaded");
        }
    }
}
