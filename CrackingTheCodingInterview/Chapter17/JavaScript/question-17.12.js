var __spreadArray = (this && this.__spreadArray) || function (to, from, pack) {
    if (pack || arguments.length === 2) for (var i = 0, l = from.length, ar; i < l; i++) {
        if (ar || !(i in from)) {
            if (!ar) ar = Array.prototype.slice.call(from, 0, i);
            ar[i] = from[i];
        }
    }
    return to.concat(ar || Array.prototype.slice.call(from));
};
var BinaryNode = /** @class */ (function () {
    function BinaryNode(value, left, right) {
        this.value = value;
        this.left = left;
        this.right = right;
        this.height = 0;
    }
    BinaryNode.prototype.setRight = function (node) {
        this.right = node;
        this.right.height = this.height + 1;
    };
    BinaryNode.prototype.setLeft = function (node) {
        this.left = node;
        this.left.height = this.height + 1;
    };
    BinaryNode.prototype.leftValue = function () {
        if (!this.left)
            return;
        return this.left.value;
    };
    BinaryNode.prototype.rightValue = function () {
        var _a;
        if (!this.right)
            return;
        return (_a = this.right) === null || _a === void 0 ? void 0 : _a.value;
    };
    BinaryNode.prototype.toString = function () {
        var _a, _b;
        return "[BinaryNode value:".concat(this.value, ", left:").concat((_a = this.leftValue()) !== null && _a !== void 0 ? _a : '', ", right:").concat((_b = this.rightValue()) !== null && _b !== void 0 ? _b : '', "]");
    };
    return BinaryNode;
}());
var tree = (new BinaryNode(4, new BinaryNode(2, new BinaryNode(1, new BinaryNode(0)), new BinaryNode(3, new BinaryNode(2.5))), new BinaryNode(5, undefined, new BinaryNode(6, new BinaryNode(8, new BinaryNode(7), new BinaryNode(9))))));
var toLinkedList = function (root) {
    // Iterates a side of the tree returning the end element, if a callback is provided, will execute for each pass.
    var iterateSide = function (node, isRight) {
        var current = node;
        var nodes = [];
        while (current != null) {
            nodes.unshift(current);
            if (isRight)
                current = current.right;
            else
                current = current.left;
        }
        return nodes;
    };
    var topLeftNodes = iterateSide(root, false);
    if (!(topLeftNodes === null || topLeftNodes === void 0 ? void 0 : topLeftNodes.length))
        return [];
    if (topLeftNodes.length === 1)
        return [topLeftNodes[0]];
    var ret = [];
    var buildLinkedListFromLeft = function (node, i) {
        if (!node)
            return;
        var firstHalfArr = ret.slice(0, (i !== null && i !== void 0 ? i : 0) + 1);
        var secondHalfArr = ret.slice((i !== null && i !== void 0 ? i : 0) + 1, ret.length);
        ret = __spreadArray(__spreadArray(__spreadArray([], firstHalfArr, true), [node], false), secondHalfArr, true);
        return buildLinkedListFromLeft(node === null || node === void 0 ? void 0 : node.left, (i !== null && i !== void 0 ? i : 0) - 1);
    };
    var buildLinkedListFromRight = function (node, i) {
        if (!node)
            return;
        ret.push(node);
        if (node.left)
            buildLinkedListFromLeft(node === null || node === void 0 ? void 0 : node.left, i);
        return buildLinkedListFromRight(node === null || node === void 0 ? void 0 : node.right, (i !== null && i !== void 0 ? i : 0) + 1);
    };
    var buildLinkedList = function (i) {
        if (i === topLeftNodes.length)
            return;
        var node = topLeftNodes[i];
        ret.push(node);
        if (node.right)
            buildLinkedListFromRight(node.right, i);
        buildLinkedList(i + 1);
    };
    buildLinkedList(0);
    return ret;
};
var elements = toLinkedList(tree);
elements.forEach(function (el) { return console.log(el.toString()); });
