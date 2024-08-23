using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace NGS.ExtendableSaveSystem
{
    public class SaveMaster : MonoBehaviour
    {
        protected ISavableComponent[] GetOrderedSavableComponents()
        {
            return FindObjectsOfTypeAll(typeof(Component))
                .Where(c => c is ISavableComponent)
                .Select(c => (ISavableComponent)c)
                .OrderBy(c => c.executionOrder)
                .ToArray(); 
        }

        public virtual void Save(string folderPath, string fileName, string fileFormat)
        {
            Debug.Log("Save");
            if (!folderPath.EndsWith("/")) folderPath += "/";
            if (!fileFormat.StartsWith(".")) fileFormat = "." + fileFormat;

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            Dictionary<int, ComponentData> componentsData = new Dictionary<int, ComponentData>();

            foreach (var savableComponent in GetOrderedSavableComponents())
                componentsData.Add(savableComponent.uniqueID, savableComponent.Serialize());

            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream stream = new FileStream(folderPath + fileName + fileFormat, FileMode.Create))
                formatter.Serialize(stream, componentsData);
        }

        public virtual void Load(string folderPath, string fileName, string fileFormat)
        {
            Debug.Log("Load");
            if (!folderPath.EndsWith("/")) folderPath += "/";
            if (!fileFormat.StartsWith(".")) fileFormat = "." + fileFormat;
            Debug.Log(folderPath);

            if (!Directory.Exists(folderPath))
            {
                Debug.Log("!Exists");
                Directory.CreateDirectory(folderPath);
            }
            string fullPath = folderPath + fileName + fileFormat;

            // Kiểm tra xem file có tồn tại không
            if (!File.Exists(fullPath))
            {
                Debug.Log("File does not exist, creating new file...");
                // Gọi phương thức Save để tạo file mới
                Save(folderPath, fileName, fileFormat);
                return;
            }
            Dictionary<int, ComponentData> componentsData = null;

            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(folderPath + fileName + fileFormat, FileMode.Open))
                componentsData = (Dictionary<int, ComponentData>) formatter.Deserialize(stream);

            foreach (var savableComponent in GetOrderedSavableComponents())
                if (componentsData.ContainsKey(savableComponent.uniqueID))
                    savableComponent.Deserialize(componentsData[savableComponent.uniqueID]);
        }
    }
}
