using System;
using UnityEngine;

namespace NGS.ExtendableSaveSystem
{
    [Serializable]
    public class ExtendedComponentData : ComponentData
    {
        public virtual void SetStringData(string uniqueName, string dataTable, string dataScore)
        {
            Debug.Log("SetStringData");
            SetString(uniqueName + "Table", dataTable);
            SetString(uniqueName + "Score", dataScore);
        }

        public void GetDataTable(string uniqueName)
        {
            if (GetString(uniqueName + "Table") == "")
            {
                Debug.Log("Null table");
                Table.instance.InitTable();
                return;
            }
            string data = GetString(uniqueName +"Table");
            Debug.Log(data);
            string[] valueList = data.Split(char.Parse(","));  
            int k = 0;
            for (int i = 0; i < Table.instance.Row; ++i)
            {
                for (int j = 0; j < Table.instance.Column; ++j)
                {
                    Table.instance.NumberTable[i][j] = int.Parse(valueList[k]);
                    k++;
                }
            }
            UIManager.instance.UpdateTableUI();
        }
        public void GetDataScore(string uniqueName)
        {
            if (GetString(uniqueName + "Score") == "")
            {
                Debug.Log("Null Score");
                return;
            }
            Debug.Log("GetDataScore");

            string data = GetString(uniqueName + "Score");
            string[] valueList = data.Split(char.Parse(","));

            Table.instance.score = int.Parse(valueList[0]);
            Table.instance.bestScore = int.Parse(valueList[1]);
            UIManager.instance.UpdateScore();
        }
    }
}
