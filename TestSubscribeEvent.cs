using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XTT
{
    public class TestSubscribeEvent : MonoBehaviour
    {
        // ==================================================================

        #region Monobehaviour

        private void OnEnable()
        {
            EventManager.AddListener<EventTest>(this.OnEventTest);
            EventManager.AddListener<EventTestWithParam>(this.OnEventTestWithParam);
        }
        private void OnDisable()
        {
            EventManager.RemoveListener<EventTest>(this.OnEventTest);
            EventManager.RemoveListener<EventTestWithParam>(this.OnEventTestWithParam);
        }

        #endregion Monobehaviour

        // ==================================================================

        #region Event

        private void OnEventTest( EventTest evt )
        {
            Debug.Log("OnEventTest");
        }

        private void OnEventTestWithParam(EventTestWithParam evt)
        {
            Debug.Log(string.Format("EventTestWithParam: {0}", evt.boolVal.ToString()));
        }

        #endregion Event

        // ==================================================================

    }
}