using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

namespace Atavism
{

    //public class AtavismSkillConvert
    //{
    //    public OID oid;
    //    public string name;
    //    public int rank;
    //    public int level;
    //    public string zone;
    //    public string note;
    //    public int status;
    //}

    public class AtavismSkillConvert : MonoBehaviour
    {
        //static AtavismGuild instance;
        string StatName;
        readonly int StatIDENT;
        string instance;
        public string License;
          
        // Start is called before the first frame update
        void Start()
        {
            License = license().ToString();
            UnityWebRequest myWww = UnityWebRequest.Get("http://lic.nxthostingsolutions.com/atavism/getlic.php?type=skillconvert&lic="+License);
            if (License != null)
            {

                string web_Response = myWww.ToString();
                if(web_Response == "ok!")
                {
                    NetworkAPI.RegisterExtensionMessageHandler("IncreaseStat", HandleStatIncrease);
                }
                return;
            }
            //instance = this;

            // Add message handlers
            

            SceneManager.sceneLoaded += sceneLoaded;
        }


        private void sceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            if (arg0.name.Equals("Login") || arg0.name.Equals(ClientAPI.Instance.characterSceneName))
            {
                //todo scene loaded code
            }
        }
        // personal license generated at package compile time.
        private string license()
        {
            return "yQr5IW3gx0";
        }

        public void StatIncreaseResponse(object obj, bool accepted)
        {
            string statName = (string)obj;

            StatIncreaseReponser(ClientAPI.GetPlayerOid(), statName, accepted);
        }
        // Update is called once per frame
        void Update()
        {

        }
        #region Message Senders
        public void StatIncreaseReponser(long id, string StateName, bool accepted)
        {

            Dictionary<string, object> props = new Dictionary<string, object>();
            props.Add("license", License);
            props.Add("statName", StatName);
            NetworkAPI.SendExtensionMessage(id, false, "SkillConvert.increaseStat", props);
        }

        #endregion Message Senders
        #region Message Handlers
        public void HandleStatIncrease(Dictionary<string, object> props)
        {

            string StatUpMessage = " You Increased Stat: " + StatName + " +1";
            UGUIConfirmationPanel.Instance.ShowConfirmationBox(StatUpMessage, StatName, StatIncreaseResponse);
        }



        #endregion Message Handlers
    }
}
