/*
    15.4. Deadlock-Free Class
    Design a class which provides a lock only if there are no possible deadlocks.
*/

using System;

namespace question_15._4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }

    public class LockFactory
    {
        private static LockFactory _instance;
        private int _numberOfLocks = 5;
        private LockNode[] _locks = new LockNode[] {};
        // Maps from a process or owner to the order that the owner claimed it would call the locks in.
        private Dictionary<int, List<LockNode>> _lockOrder = new Dictionary<int, List<LockNode>>();

        private LockFactory(int count) {
            _numberOfLocks = count;
        }

        public static LockFactory GetInstance()
        {
            return _instance;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static LockFactory Initialise(int count)
        {
            if (_instance == null) _instance = new LockFactory(count);
            return _instance;
        }

        public bool HasCycle(Dictionary<int, bool> touchedNodes, int[] resourcesInOrder)
        {
            // Check for a cycle...
            foreach (int resource in resourcesInOrder)
            {
                if (!touchedNodes.HasKey(resource))
                {
                    LockNode n = _locks[resource];
                    if (n.HasCycle(touchedNodes)) return true;
                }
            }
            return false;
        }

        // To prevent deadlocks... force the process to declare upfront, what order they will need
        // the locks in... Verify that this order does not create a deadlock.
        public bool Declare(int ownerId, int[] resourcesInOrder, Dictionary<int, bool> touchedNodes = new Dictionary<int, bool>())
        {
            var index = 1;
            touchedNodes[resourcesInOrder[0]] = false;
            // Add nodes to graph.
            for (index = 1; index < resourcesInOrder.Length; index++)
            {
                LockNode prev = _locks[resourcesInOrder[index - 1]];
                LockNode curr = _locks[resourcesInOrder[index]];
                prev.JoinTo(curr);
                touchedNodes[resourcesInOrder[index]] = false;
            }

            // If a cycle was created, destroy the resource list and return false.
            if (HasCycle(touchedNodes, resourcesInOrder))
            {
                for (int j = 1; j < resourcesInOrder.Length; j++)
                {
                    LockNode jprev = _locks[resourcesInOrder[j - 1]];
                    LockNode jcurr = _locks[resourcesInOrder[j]];
                    jprev.remove(jcurr);
                }
            }

            // No cycles created...
            // Save the order that was declared, this way we can verify that the process is
            // really calling the locks in the order that it would.
            List<LockNode> list = new List<LockNode>();
            for (int i = 0; i < resourcesInOrder.Length; i++)
            {
                LockNode resource = _locks[resourcesInOrder[i]];
                list.Add(resource);
            }
            _lockOrder[ownerId] = list;
        }

        // Get the lock, verifying first that the process is really clalling the locks in the order
        // it said it would.
        public Lock GetLock(int ownerId, int resourceId)
        {
            if (!_lockOrder.HasKey(ownerId)) return null;
            List<LockNode> list = _lockOrder[ownerId];

            LockNode head = list[0];
            if (head.GetId() == resourceId)
            {
                list.RemoveAt(0);
                return head.GetLock();
            }
            return null;
        }

        // LockNode implementation...
    }
}
