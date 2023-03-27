using System;
using System.IO;
using Storage;
using UnityEngine;

namespace SaveAndLoad {
    public class SaveAndLoad {
        public static void Save(StorageDataSaver data, string dirPath, string fileName) {
            string path = Path.Combine(dirPath, fileName);
            try {
                Directory.CreateDirectory(Path.GetDirectoryName(path));

                string dataToStore = JsonUtility.ToJson(data, true);

                using (FileStream stream = new FileStream(path,FileMode.Create)) {
                    using (StreamWriter writer = new StreamWriter(stream)) {
                        writer.Write(dataToStore);
                    }
                }
            }
            catch (Exception e) {
                Debug.Log("Data save failed");
            }
        }

        public static StorageDataSaver Load(string dirPath, string fileName) {
            string path = Path.Combine(dirPath, fileName);
            StorageDataSaver data = null;
            if (File.Exists(path)) {
                try {
                    string dataToLoad = "";
                    using (FileStream stream = new FileStream(path,FileMode.Open)) {
                        using (StreamReader reader = new StreamReader(stream)) {
                            dataToLoad = reader.ReadToEnd();
                        }
                    }

                    data = JsonUtility.FromJson<StorageDataSaver>(dataToLoad);

                }
                catch (Exception e) {
                    Debug.Log("Data load failed");
                }
            }

            return data;
        }
    }
}