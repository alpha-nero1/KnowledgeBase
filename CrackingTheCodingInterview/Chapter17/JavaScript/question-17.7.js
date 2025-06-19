/*
    17.7. True name frequencies.
*/


// Start From Core.
class GraphNode {
    // Graph node must be conbstructed with a key and a value.
    constructor(key, value) {
        this.key = key;
        this.value = value;
        this._edges = new Map();
        this._visited = false;
    }

    getEdges() {
        return new Map(this._edges);
    }

    removeEdge(key) {
        return this._edges.delete(key);
    }

    // Add an edge to the GraphNode.
    addEdge(node) {
        this._edges.set(node.key, node);
    }

    getVisited() {
        return this._visited;
    }

    setVisited(visited) {
        this._visited = visited;
    }

    toString() {
        return `{ ${this.key} = (value: ${this.value}) }`
    }
}

// Graph implementation.
class Graph {
    constructor() {
        // Initialise nodes map.
        this.nodes = new Map();
    }

    // Add a node the the graph using the key and value.
    createNode(key, value) {
        this.nodes.set(key, new GraphNode(key, value));
    }

    forEach(callback) {
        return this.nodes.forEach((value) => callback(value));
    }

    // Add an edge in the graph using the node keys of two nodes.
    addEdge(keyOne, keyTwo) {
        const nodeOne = this.nodes.get(keyOne);
        const nodeTwo = this.nodes.get(keyTwo);
        if (!nodeOne || !nodeTwo) return;
        nodeOne.addEdge(nodeTwo);
        nodeTwo.addEdge(nodeOne);
    }

    // Reset the visited state of all the nodes.
    resetState() {
        this.nodes.forEach((node => {
            node.setVisited(false);
        }));
    }

    toString() {
        let str = '[\n';
        this.nodes.forEach((node, i) => {
            str += `  ${node.toString()},\n`
        })
        return `${str}]`
    }
}

// End from Core.

const findTrueNameFreqs = (nameFreqsArr, synonymsArr) => {
    const constructGraph = (names) => {
        const graph = new Graph();
        names.forEach((value) => {
            graph.createNode(value[0], value[1]);
        });
        return graph;
    }
    
    const connectNodeEdges = (graph, synonymsArr) => {
        synonymsArr.forEach((synPair) => {
            graph.addEdge(synPair[0], synPair[1]);
        });
    }
    
    const countFrequencies = (node) => {
        if (node.getVisited()) return 0; // Already counted!
        node.setVisited(true);
        let sum = node.value;
        node.getEdges().forEach((edgeNode) => {
            sum += countFrequencies(edgeNode);
        });
        return sum;
    };
    
    const trueCounts = new Map();
    const graph = constructGraph(nameFreqsArr);
    connectNodeEdges(graph, synonymsArr);
    graph.forEach((node) => {
        if (node.getVisited()) return;
        trueCounts.set(node.key, countFrequencies(node));
    });
    return trueCounts;
}

console.log(findTrueNameFreqs(
    [
        ['Jonny', 10],
        ['Jon', 3],
        ['Davis', 2],
        ['Kari', 3],
        ['Johnny', 11],
        ['John', 1],
        ['Carlton', 8],
        ['Carleton', 2],
        ['Jonathan', 9],
        ['Cartrie', 5],
        ['Carrrie', 1],
    ],
    [
        ['Jonathan', 'John'],
        ['Jon', 'Jonny'],
        ['Jonny', 'Johnny'],
        ['John', 'Johnny'],
        ['Johnny', 'Jonny'],
        ['Kari', 'Carrie'],
        ['Carleton', 'Carlton'],
    ]
));