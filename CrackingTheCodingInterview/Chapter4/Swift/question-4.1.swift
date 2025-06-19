// Given a directed graph (one directional), design an algorithm to find out wether there is  aroute between two paths.
// Exec using "cat ../../Core/Swift/queue.swift ../../Core/Swift/graph.swift question-4.1.swift | swift -"

let g = Graph();
let rootNode = GraphNode(value: -1);
g.nodes.append(rootNode);

let node1 = GraphNode(value: 1);
let node2 = GraphNode(value: 2);
rootNode.addChild(node: node1);
rootNode.addChild(node: node2);

let node3: GraphNode = GraphNode(value: 3);
let node4: GraphNode = GraphNode(value: 4);
node1.addChild(node: node3);
node1.addChild(node: node4);

let node5: GraphNode = GraphNode(value: 5);
let node6: GraphNode = GraphNode(value: 6);

node3.addChild(node: node5)
node5.addChild(node: node6)
node6.addChild(node: node1)

func canRouteBetweenTwo(startNode: GraphNode, endNode: GraphNode) -> Bool {
  let q = Queue<GraphNode>();
  q.add(value: startNode);
  startNode.marked = true;
  if (startNode === endNode) {
    return true;
  }
  while (!q.isEmpty()) {
    print(1)
    let node = q.pop();
    node!.marked = true;
    print(node ?? "", endNode)
    if (node === endNode) {
      return true;
    }
    for adjNode in node!.adjacent {
      if (!adjNode.marked) {
        adjNode.marked = true;
        q.add(value: adjNode);
      }
    }
  }
  return false;
}

print(canRouteBetweenTwo(startNode: node1, endNode: node6));
