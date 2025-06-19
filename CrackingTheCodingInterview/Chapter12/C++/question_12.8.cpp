/*
    12.8. Copy node.
    Write a method that takes a pointer to a Node structure as a parameter and returns a
    complete copy of the passed in data structure. The Node data structure contains two pointers to
    other Nodes.

*/

// Type definition / alias to the type map<Node*, Node*>
typedef map<Node*, Node*> NodeMap;

Node * copy_recursive(Node * current_node, NodeMap nodeMap) {
    // If the current node is null, short circuit.
    if (current_node == NULL) return NULL;

    // map.find() finds the element in the object, if found returns an iterator to the object.
    // else returns an iterator to end.
    NodeMap::iterator i = nodeMap.find(current_node);

    if (i != nodeMap.end()) {
        // We have been here before, return the copy!
        // i is the first element found and `second` is the value. whereas `first` would be the key of the mapping.
        return i->second;
    }

    // Map current before traversing links!
    Node * node = new Node;
    nodeMap[current_node] = node;

    node->ptr1 = copy_recursive(current_node->ptr1, nodeMap);
    node->ptr2 = copy_recursive(current_node->ptr2, nodeMap);

    return node;
}


Node * copy_structure(Node * root) {
    NodeMap nodeMap;
    return copy_recursive(root, nodeMap);
}

int main() {
    return 0;
}