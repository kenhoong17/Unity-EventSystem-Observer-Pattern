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

        #endregion Monobehaviour

        // ==================================================================

    }
}