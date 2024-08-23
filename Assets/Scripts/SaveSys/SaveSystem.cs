using UnityEngine;
using System.IO;
using NGS.ExtendableSaveSystem;

public class SaveSystem : MonoBehaviour, ISavableComponent
{
    [SerializeField] private int m_uniqueID;


    [SerializeField] private int m_executionOrder;
    public int uniqueID { get => m_uniqueID; }
    public int executionOrder { get => m_executionOrder; }

    private void Reset()
    {
        m_uniqueID = GetHashCode();
    }
    public string TableToString()
    {
        
       
        string data = "";
        if (Table.instance.NumberTable == null)
        {
            for (int i = 0; i < Table.instance.Row; ++i)
            {
                for (int j = 0; j < Table.instance.Column; ++j)
                {
                    data += 0 + ",";
                }
            }
            return data;
        }

        for (int i = 0; i < Table.instance.Row; ++i)
        {
            for (int j = 0; j < Table.instance.Column; ++j)
            {
                data += Table.instance.NumberTable[i][j].ToString() + ",";
            }
        }
        Debug.Log("After: " + data);
        return data;
    }

    public string ScoreToString()
    {
        string data = "";

        data = Table.instance.score.ToString() + "," + Table.instance.bestScore.ToString();

        return data;
    }
    public ComponentData Serialize()
    {
        Debug.Log("Serialize");
        ExtendedComponentData data = new ExtendedComponentData();
        data.SetStringData("data", TableToString(), ScoreToString() );
        return data;
    }


    public void Deserialize(ComponentData data)
    {
        Debug.Log("Deserialize");
        ExtendedComponentData unpacked = (ExtendedComponentData) data;
        unpacked.GetDataTable("data");
        unpacked.GetDataScore("data");

    }

}