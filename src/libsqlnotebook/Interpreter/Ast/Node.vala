// SQL Notebook
// Copyright (C) 2018 Brian Luft
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated
// documentation files (the "Software"), to deal in the Software without restriction, including without limitation the
// rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the
// Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS
// OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
// OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using Gee;
using SqlNotebook.Collections;
using SqlNotebook.Interpreter.Tokens;

namespace SqlNotebook.Interpreter.Ast {
    public abstract class Node : Object {
        public Token source_token { get; set; }

        // each node will implement only one of the following three:

        // implemented if the node has no children
        protected virtual bool is_leaf {
            get {
                return false;
            }
        }

        // implemented if the node has exactly one child
        protected virtual Node? get_child() {
            return null;
        }

        // implemented if the node has multiple children, or if the number of children is not known statically
        protected virtual Node?[] get_children() {
            return new Node?[0];
        }

        public LinkedList<Node> traverse() {
            var result = new LinkedList<Node>();
            var stack = new Stack<Node>();
            stack.push(this);

            while (stack.any()) {
                var n = stack.pop();

                if (!n.is_leaf) {
                    var only_child = n.get_child();
                    if (only_child != null) {
                        stack.push(only_child);
                    } else {
                        var children = n.get_children();
                        for (var i = children.length - 1; i >= 0; i--) {
                            var child = children[i];
                            if (child != null) {
                                stack.push(child);
                            }
                        }
                    }
                }

                result.add(n);
            }

            return result;
        }
    }
}
