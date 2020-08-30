using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

namespace MagicSave
{
    public interface ISaveLoad
    {
        object GetSaveData();
        void Load(object o);

    }

    public partial class SaveHandler : MonoBehaviour
    {
        static List<SaveHandler> allSaveHandler = new List<SaveHandler>();

      

        public string prefabPath;  


        [System.Serializable]
        public class SaveData
        {
            public object[] staticObjSaveDatas; 
            public string[] prefabPathes;       
            public object[] objSaveDatas;       
        }


        public static void SaveScene(string filePathAndName, ISaveLoad[] staticObjs= null)
        {
            
            if (string.IsNullOrEmpty(filePathAndName)) return;

            SaveData sd = CreateSaveData(staticObjs == null? new ISaveLoad[0]: staticObjs);

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(filePathAndName); 
            bf.Serialize(file, sd);
            file.Close();
        }

        public static void LoadScene(string filePathAndName, ISaveLoad[] staticObjs = null)
        {
            
            if (string.IsNullOrEmpty(filePathAndName)) return;
            allSaveHandler.Clear(); 

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.OpenRead(filePathAndName);
            if (file == null) return;

            SaveData sd = bf.Deserialize(file) as SaveData;
            file.Close();
            if (sd == null) return;

           
            for (int i=0; i< sd.prefabPathes.Length; i++)
            {
                GameObject go = CreateFromPrefab(sd.prefabPathes[i]);
                if (go == null) continue;

                ISaveLoad isl = go.GetComponent<ISaveLoad>();
                if (isl != null)
                {
                    isl.Load(sd.objSaveDatas[i]);
                }
            }

          
            if (staticObjs != null)
            {
                int length = Math.Min(staticObjs.Length, sd.staticObjSaveDatas.Length);
                for (int i=0; i< length; i++)
                {
                	if (staticObjs[i] != null)
                    {
                        staticObjs[i].Load(sd.staticObjSaveDatas[i]);
                    }
                }
            }
        }

        static SaveData CreateSaveData(ISaveLoad[] staticObjs)
        {
          
            SaveData sd = new SaveData();

            sd.staticObjSaveDatas = new object[staticObjs.Length];
            sd.prefabPathes = new string[allSaveHandler.Count];
            sd.objSaveDatas = new object[allSaveHandler.Count];

           
            for (int i = 0; i < allSaveHandler.Count; i++)
            {
                sd.prefabPathes[i] = "";
                sd.objSaveDatas[i] = null;

                SaveHandler sh = allSaveHandler[i];
                if (sh != null)
                {
                    sd.prefabPathes[i] = sh.prefabPath;

                    ISaveLoad isl = sh.GetComponent<ISaveLoad>();
                    if (isl != null)
                    {
                        sd.objSaveDatas[i] = isl.GetSaveData();
                    }
                }
            }

          
            for (int i=0; i< staticObjs.Length; i++)
            {
                if (staticObjs[i] != null)
                {
                    sd.staticObjSaveDatas[i] = staticObjs[i].GetSaveData();
                }
            }

            return sd;
        }

        static GameObject CreateFromPrefab(string path)
        {
            
            GameObject prefab= Resources.Load<GameObject>(path);
            if (prefab == null)
            {
                Debug.Log("Cannot find: '" + path + "' in Resources");
                return null;
            }

            GameObject go=  GameObject.Instantiate(prefab); 
            go.name = prefab.name;
            return go;
        }

        void OnEnable()
        {
            allSaveHandler.Add(this);
        }

        void OnDisable()
        {
            allSaveHandler.Remove(this);
        }


    }
}
