using System;
using System.IO;
using System.Runtime.CompilerServices;
using Models;
using Storage;
using UnityEngine;

namespace SaveAndLoad {
    public static class SaveAndLoad {
        public static void Save(IModel data, string dirPath, string fileName) {
            dirPath = Application.persistentDataPath + dirPath;
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

        public static IModel Load(string dirPath, string fileName, ModelTypesEnums modelTypeEnums) {
            //Do load method in every model
            dirPath = Application.persistentDataPath + dirPath;
            string path = Path.Combine(dirPath, fileName);
            IModel model = null;
            if (File.Exists(path)) {
                try {
                    string dataToLoad = "";
                    using (FileStream stream = new FileStream(path,FileMode.Open)) {
                        using (StreamReader reader = new StreamReader(stream)) {
                            dataToLoad = reader.ReadToEnd();
                        }
                    }
                    switch (modelTypeEnums) {
                        case ModelTypesEnums.StorageModel:
                            model = new StorageModel().Load(dataToLoad);
                            break;
                        case ModelTypesEnums.ShopModel:
                            model = new ShopModel().Load(dataToLoad);
                            break; 
                    }
                }
                catch (Exception e) {
                    Debug.Log("Data load failed" + e);
                }
            }

            return model;
        }
        
    }
}