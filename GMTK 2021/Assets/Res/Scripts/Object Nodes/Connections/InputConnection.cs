using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nodes
{
    [System.Serializable]
    public class InputConnection : NodeConnection
    {
        public bool Connect(OutputConnection conn)
        {
            if (conn == null)
                return false;

            GameObject connParent = conn.transform.parent.gameObject;
            GameObject thisParent = transform.parent.gameObject;
            

            if (connParent == null)
                return false;

            if (connParent == thisParent)
                return false;

            if (conn.HasConnection)
                return false;

            if (CheckForCircularConnection(conn))
                return false;

            conn.SetConnection(this);
            SetConnection(conn);


            return true;
        }

        // return true if there is an error
        private bool CheckForCircularConnection(OutputConnection conn)
        {
            Queue<GameObject> toVisit = new Queue<GameObject>();

            toVisit.Enqueue(conn.transform.parent.gameObject);
            while (toVisit.Count > 0)
            {
                GameObject current = toVisit.Dequeue();

                if (current == this)
                    return true;

                InputConnection[] conns = current.GetComponents<InputConnection>();
                for (int i = 0; i < conns.Length; i++)
                {
                    toVisit.Enqueue(conns[i].gameObject);
                }
            }

            return false;
        }

    }
}