using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XTT
{
    public class EventTest : Event 
    { 
        public EventTest() { }
    }

    public class EventTestWithParam : Event
    {
        public bool boolVal;
        public EventTestWithParam( bool boolVal)
        {
            this.boolVal = boolVal;
        }
    }

    public class TestInvokeEvent : MonoBehaviour
    {
        // ==================================================================

        #region Monobehaviour

        private void Update()
        {
            if ( Input.GetKeyDown(KeyCode.Z) )
            {
                // Test Invoke EventTest
                EventManager.AddEvent(new EventTest());
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                // Test Invoke EventTestWithParam
                EventManager.AddEvent(new EventTestWithParam(false));
            }
        }

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