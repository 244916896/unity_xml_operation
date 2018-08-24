using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Demo
{
    public class Demo : MonoBehaviour
    {
        public Text info1;
        public Text info2;
        // Use this for initialization
        void Start()
        {
            mm.XmlPersistence.UpdateXml("xml", "demo");
            info1.text = mm.XmlPersistence.RetrieveXml("xml");
            mm.XmlPersistence.UpdateXml("xml", "unity3d");
            info2.text = mm.XmlPersistence.RetrieveXml("xml");
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
