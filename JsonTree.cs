namespace JsonTreeProject
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class JsonTree
    {
        /// <summary>
        /// 将关系数据列表 转化为 树状图
        /// </summary>
        /// <param name="parentId">根节点</param>
        /// <param name="relation">关系数据</param>
        /// <returns></returns>
        public Node GetRoot(int parentId, List<Mapping> mapping)
        {
            var root = new Node();
            root.Key = parentId;

            var groups = mapping.Select(x => new Node { Key = x.ChildID, Left = x.ParentID }).GroupBy(x => x.Left);
            var child = groups.FirstOrDefault(x => x.Key == parentId);

            if (child == null)
            {
                return null;
            }
            else
            {
                var children = child.ToList();
                if (children.Count > 0)
                {
                    var dict = groups.ToDictionary(g => g.Key, g => g.ToList());
                    for (int i = 0; i < children.Count; i++)
                    {
                        this.AddChild(children[i], dict);
                    }
                }

                root.Children = children;
            }

            return root;
        }

        public void AddChild(Node child, IDictionary<int, List<Node>> mapping)
        {
            if (mapping != null && mapping.ContainsKey(child.Key))
            {
                child.Children = mapping[child.Key].ToList();
                for (int i = 0; i < child.Children.Count; i++)
                {
                    this.AddChild(child.Children[i], mapping);
                }
            }
        }

        public void PrintTree(Node node, string prefix)
        {
            Console.WriteLine("{0} + {1}", prefix, node.Key);
            if (node.Children == null)
            {
                return;
            }

            foreach (Node child in node.Children)
            {
                PrintTree(child, prefix + "    |");
            }
        }


        public bool IsAContainB(int a, int b, IList<Mapping> mapping)
        {
            var result = IsAContainBTop(a, b, mapping);

            if (result)
            {
                return true;
            }

            result = IsAContainBBottom(a, b, mapping);
            if (result)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// A 是否包含 B
        /// </summary>
        /// <param name="a">Parent</param>
        /// <param name="b">Child</param>
        /// <param name="mapping"></param>
        /// <returns></returns>
        public bool IsAContainBTop(int a, int b, IList<Mapping> mapping)
        {
            // 排除自循环
            if (a == b)
            {
                return true;
            }

            var getBList = mapping.Where(x => x.ChildID == b).ToList();
            if (getBList.Count == 0)
            {
                // go to bottom jquery
                return false;
            }

            var result = false;
            foreach (var getB in getBList)
            {
                if (getB.ParentID == a)
                {
                    return true;
                }
                else
                {
                    // 向上查找
                    result = this.IsAContainBTop(a, getB.ParentID, mapping);
                }

                if (result)
                {
                    return true;
                }
            }

            return result;
        }

        public bool IsAContainBBottom(int a, int b, IList<Mapping> mapping)
        {
            // 排除自循环
            if (a == b)
            {
                return true;
            }

            var getBList = mapping.Where(x => x.ParentID == b).ToList();
            if (getBList.Count == 0)
            {
                return false;
            }

            var result = false;
            foreach (var getB in getBList)
            {
                if (getB.ChildID == a)
                {
                    return true;
                }
                else
                {
                    // 向下查找
                    result = this.IsAContainBBottom(a, getB.ChildID, mapping);
                }

                if (result)
                {
                    return true;
                }
            }

            return result;
        }
    }

    /// <summary>
    /// 关系数据结构
    /// parent --{Node}--children
    /// </summary>
    public class Mapping
    {
        public int ParentID { get; set; }

        public int ChildID { get; set; }
    }

    /// <summary>
    /// 节点数据结构
    /// {p1,p2,p3,...} -- [Node] -- {c1,c2,c3,...}
    /// </summary>
    public class Node
    {
        public int Left { get; set; }

        public int Key { get; set; }

        public List<Node> Children { get; set; }
    }
}
