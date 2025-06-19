/**
 *  17.12. BiNode. 
 */

class BinaryNode<TData> {
    left?: BinaryNode<TData>;
    right?: BinaryNode<TData>;
    value: TData;
    height: number;

    constructor(value: TData, left?: BinaryNode<TData>, right?: BinaryNode<TData>) {
        this.value = value;
        this.left = left;
        this.right = right;
        this.height = 0;
    }

    setRight(node?: BinaryNode<TData>) {
        this.right = node;
        this.right!.height = this.height + 1;
    }

    setLeft(node?: BinaryNode<TData>) {
        this.left = node;
        this.left!.height = this.height + 1;
    }

    leftValue(): TData | undefined {
        if (!this.left) return;
        return this.left.value;
    }

    rightValue(): TData | undefined {
        if (!this.right) return;
        return this.right?.value;
    }

    toString(): string {
        return `[BinaryNode value:${this.value}, left:${this.leftValue() ?? ''}, right:${this.rightValue() ?? ''}]`;
    }
}

// Build tree to traverse.
const tree = (
    new BinaryNode(
        4,
        new BinaryNode(
            2,
            new BinaryNode(
                1,
                new BinaryNode(0)
            ),
            new BinaryNode(
                3,
                new BinaryNode(2.5)
            ),
        ),
        new BinaryNode(
            5,
            undefined,
            new BinaryNode(
                6,
                // Uncomment to see effect of left nodes on right side.
                // new BinaryNode(
                //     8,
                //     new BinaryNode(7),
                //     new BinaryNode(9),
                // )
            )
        ),
    )
);

// Create the linked list.
const toLinkedList = (root: BinaryNode<any>): BinaryNode<any>[] => {
    
    // Iterates a side of the tree returning the end element, if a callback is provided, will execute for each pass.
    const iterateSide = (node: BinaryNode<any>, isRight: boolean) => {
        let current: BinaryNode<any> | undefined = node;
        const nodes: BinaryNode<any>[] = [];

        while (current != null) {
            nodes.unshift(current);
            if (isRight) current = current.right;
            else current = current.left;
        }
        return nodes;
    }

    const topLeftNodes = iterateSide(root, false);

    // Checks before proceeding.
    if (!topLeftNodes?.length) return [];
    if (topLeftNodes.length === 1) return [topLeftNodes[0]];

    let ret: BinaryNode<any>[] = [];

    const buildLinkedListFromLeft = (node?: BinaryNode<any>, i?: number) => {
        if (!node) return;
        const firstHalfArr = ret.slice(0, (i ?? 0) + 1);
        const secondHalfArr = ret.slice((i ?? 0) + 1, ret.length)
        ret = [...firstHalfArr, node, ...secondHalfArr];
        return buildLinkedListFromLeft(node?.left, (i ?? 0) - 1);
    }

    const buildLinkedListFromRight = (node?: BinaryNode<any>, i?: number) => {
        if (!node) return;
        ret.push(node);
        if (node.left) buildLinkedListFromLeft(node?.left, i);
        return buildLinkedListFromRight(node?.right, (i ?? 0) + 1);
    }

    // Buld the linked list considering either index or node's right value.
    const buildLinkedList = (i: number) => {
        if (i === topLeftNodes.length) return;

        const node = topLeftNodes[i];

        ret.push(node);

        if (node.right) buildLinkedListFromRight(node.right, i);
        buildLinkedList(i + 1);
    }

    buildLinkedList(0);
    return ret;
}

const elements = toLinkedList(tree);
elements.forEach(el => console.log(el.toString()));