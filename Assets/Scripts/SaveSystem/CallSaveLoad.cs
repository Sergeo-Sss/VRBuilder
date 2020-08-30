using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MagicSave
{
    public class CallSaveLoad : MonoBehaviour
    {
        static CallSaveLoad me; 
        string saveFilename = "Saved.bin";


        public void SaveScene()
        {
           
            ISaveLoad[] allIsl = GameObject.Find("ObjData").GetComponentsInChildren<ISaveLoad>(true);
            SaveHandler.SaveScene(Application.persistentDataPath + "/" + saveFilename, allIsl);
        }

        public void LoadScene()
        {
            StartCoroutine(CoLoadScene());
        }


        IEnumerator CoLoadScene()
        {

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Test");

           
            while (!asyncLoad.isDone)
            {
                yield return null;
            }

            ISaveLoad[] allIsl = GameObject.Find("ObjData").GetComponentsInChildren<ISaveLoad>(true);
            SaveHandler.LoadScene(Application.persistentDataPath + "/" + saveFilename, allIsl);
        }


        void Awake()
        {
            if (me == null)
            {
                me = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        void Start()
        {
            //LoadScene();
            transform.Find("SaveButton").GetComponent<Button>().onClick.AddListener(SaveScene);
            transform.Find("LoadButton").GetComponent<Button>().onClick.AddListener(LoadScene);
        }
    }
}