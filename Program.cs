namespace JsonTreeProject
{
    using System;
    using System.Collections.Generic;

    class Program
    {
        static void Main(string[] args)
        {
            /// <summary>
            /// 0|
            ///  |--1 {4,5,6:[10]}
            ///  |--2 {7,8,9}
            ///  |--3 {10:[13:{6},14],11,12}
            /// </summary>
            List<Mapping> list = new List<Mapping>()
            {
                new Mapping() { ParentID = 0, ChildID = 1 },
                new Mapping() { ParentID = 0, ChildID = 2 },
                new Mapping() { ParentID = 0, ChildID = 3 },

                new Mapping() { ParentID = 1, ChildID = 4 },
                new Mapping() { ParentID = 1, ChildID = 5 },
                new Mapping() { ParentID = 1, ChildID = 6 },

                new Mapping() { ParentID = 2, ChildID = 7 },
                new Mapping() { ParentID = 2, ChildID = 8 },
                new Mapping() { ParentID = 2, ChildID = 9 },

                new Mapping() { ParentID = 3, ChildID = 10 },
                new Mapping() { ParentID = 3, ChildID = 11 },
                new Mapping() { ParentID = 3, ChildID = 12 },
                
                //new Mapping() { ParentID = 10, ChildID = 6 },
                new Mapping() { ParentID = 10, ChildID = 13 },
                new Mapping() { ParentID = 10, ChildID = 14 },

                new Mapping() { ParentID = 13, ChildID = 6 },
                //new Mapping() { ParentID = 6, ChildID = 10 },
            };

            JsonTree arborescence = new JsonTree();
            var root = arborescence.GetRoot(0, list);
            arborescence.PrintTree(root, string.Empty);

            var a = 60;
            var b = 10;
            var result = arborescence.IsAContainB(a, b, list);

            Console.WriteLine("\n\r {0} {1} {2}", a, result ? "包含" : "不包含", b);
            Console.Read();
        }
    }
}
