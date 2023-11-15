﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace _06_05_LinqTree {
    public class TreeNode<T> {
        public T Data;
        public List<TreeNode<T>> Nodes { get; } = new List<TreeNode<T>>();
        public TreeNode<T> Parent { get; }

        public TreeNode(T data, TreeNode<T> parent = null) {
            Data = data;
            Parent = parent;
        }

        public TreeNode<T> Add(T data) {
            var node = new TreeNode<T>(data, this);
            Nodes.Add(node);
            return node;
        }

        public IEnumerable<T> Treverse_DepthFirst() {
            var stack = new Stack<TreeNode<T>>();
            stack.Push(this);
            while (stack.Count != 0) {
                var node = stack.Pop();
                yield return node.Data;
                foreach (var child in node.Nodes) {
                    stack.Push(child);
                }
            }
        }

        public IEnumerable<T> Treverse_BreadthFirst() {
            var q = new Queue<TreeNode<T>>();
            q.Enqueue(this);
            while (q.Count != 0) {
                var node = q.Dequeue();
                yield return node.Data;
                foreach (var child in node.Nodes) {
                    q.Enqueue(child);
                }
            }
        }
    }

    public class Tree<T> : IEnumerable<T> {
        public TreeNode<T> Root { get; }

        public Tree() : this(default) { }
        public Tree(T rootData) => Root = new TreeNode<T>(rootData);

        // IEnumerable<T> implementation
        public IEnumerator<T> GetEnumerator() => Treverse_DepthFirst().GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerable<T> Treverse_DepthFirst() => Root.Treverse_DepthFirst();
        public IEnumerable<T> Treverse_BreadthFirst() => Root.Treverse_BreadthFirst();
    }

    internal class Program {        
        static void Main(string[] args) {
            var t0 = Stopwatch.GetTimestamp();

            Tree<string> tree = GetDriveTree("d:\\");

            var t1 = Stopwatch.GetTimestamp();

            var sb = new StringBuilder();

            Console.WriteLine("== Treverse_DepthFirst ==");
            foreach (var data in tree.Treverse_DepthFirst()) {
                sb.AppendLine($"{data}");
            }
            Console.WriteLine();

            //Console.WriteLine("== Treverse_BreadthFirst ==");
            //foreach (var data in tree.Treverse_BreadthFirst()) {
            //    sb.AppendLine($"{data}");
            //}
            //Console.WriteLine();

            var t2 = Stopwatch.GetTimestamp();

            Console.WriteLine(sb.ToString()); 

            Console.WriteLine($"Generation: {(t1 - t0) / (double)Stopwatch.Frequency * 1000:F2} ms");
            Console.WriteLine($"Enumration: {(t2 - t1) / (double)Stopwatch.Frequency * 1000:F2} ms");
        }

        private static Tree<string> GetDriveTree(string drive) {
            var tree = new Tree<string>(drive);
            var root = tree.Root;
            var stack = new Stack<TreeNode<string>>();
            stack.Push(root);

            while (stack.Count != 0) {
                var node = stack.Pop();
                try {
                    foreach (var dir in System.IO.Directory.GetDirectories(node.Data)) {
                        var child = node.Add(dir);
                        stack.Push(child);
                    }
                } catch (Exception) { }
            }

            return tree;
        }
    }
}
