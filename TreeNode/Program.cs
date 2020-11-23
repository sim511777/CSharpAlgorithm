using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeNode {
    // 배열 사용
    //class TreeNode {
    //    public object Data { get; set; }
    //    public TreeNode[] Links { get; private set; }
    //    public TreeNode(object data, int maxDegree = 3) {
    //        Data = data;
    //        Links = new TreeNode[maxDegree];
    //    }
    //}

    // 리스트 사용
    class TreeNode {
        public object Data { get; set; }
        public List<TreeNode> Links {
            get; private set;
        }

        public TreeNode(object data) {
            Data = data;
            Links = new List<TreeNode>();
        }
    }

    class Program {
        static void Main(string[] args) {
            var A = new TreeNode("A");
            var B = new TreeNode("B");
            var C = new TreeNode("C");
            var D = new TreeNode("D");

            // 배열사용
            //A.Links[0] = B;
            //A.Links[1] = C;
            //A.Links[2] = D;
            //B.Links[0] = new TreeNode("E");
            //B.Links[1] = new TreeNode("F");
            //D.Links[0] = new TreeNode("G");

            // 리스트 사용
            A.Links.Add(B);
            A.Links.Add(C);
            A.Links.Add(D);
            B.Links.Add(new TreeNode("E"));
            B.Links.Add(new TreeNode("F"));
            D.Links.Add(new TreeNode("G"));

            // A의 자식노드들 출력
            foreach (var node in A.Links) {
                Console.WriteLine(node.Data);
            }
        }
    }
}
