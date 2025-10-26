using System;

// A binary search tree has all elements on the left <= the root whilst all
// on the right must be greater than the root value.

namespace C_
{
    class Program
    {
        static void Main(string[] args)
        {
            BinaryNode root = new BinaryNode(6);
            root.left = new BinaryNode(4);
            root.right = new BinaryNode(8);
            root.right.right = new BinaryNode(1);
            bool valid = Program.ValidateBST(root, null, null);
            Console.WriteLine($"valid: {valid}");
        }

        // To validate the BST we should recursively check level by level
        // if the left and right values of the node are in the allowable range.
        // if not return false;
        static bool ValidateBST(BinaryNode node, int? min, int? max) {
            if (node == null) {
                // If the node passed was null, its considered valid.
                return true;
            }
            if ((min != null && node.value <= min) || (max != null && node.value > max)) {
                // If min was passed in and the node value does not succeed or
                // vice versa with max, return false!
                return false;
            }
            // We have not been killed yet, continued checking down the levels
            // and propogating the max on left side and min on right side.
            if (
                !Program.ValidateBST(node.left, min, node.value) || 
                !Program.ValidateBST(node.right, node.value, max)
            ) {
                return false;
            }
            // Ret true if all g!
            return true;
        }
    }
}
