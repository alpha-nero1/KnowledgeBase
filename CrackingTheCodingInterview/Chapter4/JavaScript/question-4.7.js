// Build order. https://cin.ufpe.br/~fbma/Crack/Cracking%20the%20Coding%20Interview%20189%20Programming%20Questions%20and%20Solutions.pdf
const { TreeNode } = require('../../Core/JavaScript/tree-node');
const { StackList } = require('../../Core/JavaScript/stack-list');

const NodeStates = {
  None: 0,
  Processing: 1,
  Complete: 2
}

const buildNodes = (items, dependencies) => {
  const nodeHash = {};
  items.forEach((item, i) => {
    nodeHash[item] = new TreeNode(null, item, item);
    nodeHash[item].state = NodeStates.None;
    dependencies.forEach(depPair => {
      if (depPair[0] === item) {
        if (!nodeHash[depPair[1]]) {
          // Make the node for dep.
          nodeHash[depPair[1]] = new TreeNode(null, depPair[1], depPair[1]);
          nodeHash[depPair[1]].state = NodeStates.None;
        }
        // Add to this deps.
        nodeHash[depPair[0]].addChild(nodeHash[depPair[1]]);
      }
    })
  });
  return Object.values(nodeHash);
}

const dfs = (node, stack) => {
  // If there is no node or we caught a loop, return false up the chain.
  if (!node || node.state === NodeStates.Processing) return false;
  if (node.state === NodeStates.None) {
    node.state = NodeStates.Processing;
    node.getChildren().forEach(child => {
      // Back out if children err.
      if (!dfs(child, stack)) {
        return false;
      }
    })
    node.state = NodeStates.Complete;
    // Push to stack once CHILDREN processed.
    stack.push(node);
  }
  return true;
}

const buildOrder = (items, dependencies) => {
  const orderStack = new StackList();
  if (items && items.forEach && dependencies && dependencies.forEach) {
    nodes = buildNodes(items, dependencies);
    // dfs.
    nodes.forEach(n => {
      if (n.state === NodeStates.None) {
        // Backout up chain if err.
        if (!dfs(n, orderStack)) {
          return null;
        }
      }
    })
  }
  return orderStack;
}

const stack = buildOrder(['a', 'b', 'c', 'd', 'e', 'f'], [['a', 'b'], ['b', 'c'], ['b', 'd'], ['d', 'f'], ['b', 'e']])
if (stack) {
  stack.forEach(item => {
    console.log(item.value.getValue());
  })
}