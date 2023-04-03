using System;
using System.IO;
using System.Runtime.CompilerServices;
using Models;
using Storage;
using UnityEditor.Build.Content;
using UnityEngine;

namespace SaveAndLoad {
    public static class SaveAndLoad {
        public static void Save(IModel data, string dirPath, string fileName) {
            string path = Path.Combine(dirPath, fileName);
            try {
                Directory.CreateDirectory(Path.GetDirectoryName(path) ?? string.Empty);

                string dataToStore = JsonUtility.ToJson(data, true);

                using (FileStream stream = new FileStream(path,FileMode.Create)) {
                    using (StreamWriter writer = new StreamWriter(stream)) {
                        writer.Write(dataToStore);
                    }
                }
            }
            catch (Exception e) {
                Debug.Log("Data save failed" + e);
            }
        }

        public static StorageModel Load(string dirPath, string fileName) {
            string path = Path.Combine(dirPath, fileName);
            StorageModel data = null;
            if (File.Exists(path)) {
                try {
                    string dataToLoad = "";
                    using (FileStream stream = new FileStream(path,FileMode.Open)) {
                        using (StreamReader reader = new StreamReader(stream)) {
                            dataToLoad = reader.ReadToEnd();
                        }
                    }

                    data = JsonUtility.FromJson<StorageModel>(dataToLoad);

                }
                catch (Exception e) {
                    Debug.Log("Data load failed" + e);
                }
            }

            return data;
        }
    }
}