using System;
using System.Collections.Generic;

namespace proje4
{
    public class Node  // Düğüm sınıfı. Kendi içinde alt düğümleri de (left child, right child) tutmaktadır.
    {
        public int data; // Düğümün değeri
        public Node left;
        public Node right;
        public Node(int data) // Düğüm sınıfı yapılandırıcısı. Düğüm nesnesi, taşıyacağı değer girilerek oluşturulur.
        {
            this.data = data;
        }
    }

    public class AVL
    {
        Node root; // AVL sınıfının kök düğümü

        public AVL() { }// AVL sınıfı yapılandırıcısı. AVL nesnesi herhangi bir argüman almadan oluşturulur.

        public void Add(int data)
        {
            Node newItem = new Node(data);
            if (root == null)
            {
                root = newItem;
            }
            else
            {
                root = RecursiveInsert(root, newItem);
            }
        }

        private Node RecursiveInsert(Node current, Node n)
        {
            if (current == null)
            {
                current = n;
                return current;
            }
            else if (n.data < current.data)
            {
                current.left = RecursiveInsert(current.left, n);
                current = balance_tree(current);
            }
            else if (n.data > current.data)
            {
                current.right = RecursiveInsert(current.right, n);
                current = balance_tree(current);
            }
            return current;
        }

        private Node balance_tree(Node current)
        {
            int b_factor = balance_factor(current);
            if (b_factor > 1)
            {
                if (balance_factor(current.left) > 0)
                {
                    current = RotateLL(current);
                }
                else
                {
                    current = RotateLR(current);
                }
            }
            else if (b_factor < -1)
            {
                if (balance_factor(current.right) > 0)
                {
                    current = RotateRL(current);
                }
                else
                {
                    current = RotateRR(current);
                }
            }
            return current;
        }

        private void InOrder(Node localRoot)
        {
            if (localRoot != null)
            {
                InOrder(localRoot.left);
                Console.Write("({0}) ", localRoot.data);
                InOrder(localRoot.right);
            }
        }

        public void DisplayAVL()
        {
            if (root == null)
            {
                Console.WriteLine("Tree is empty");
                return;
            }
            Console.WriteLine("AVL (Dengeli İkili Arama Ağacı) InOrder Düzende Yazdırılması : \n");
            InOrder(root);
            Console.WriteLine("\n\n\n");
        }

        private int max(int l, int r)
        {
            return l > r ? l : r;
        }

        private int getHeight(Node current)
        {
            int height = 0;
            if (current != null)
            {
                int l = getHeight(current.left);
                int r = getHeight(current.right);
                int m = max(l, r);
                height = m + 1;
            }
            return height;
        }

        private int balance_factor(Node current)
        {
            int l = getHeight(current.left);
            int r = getHeight(current.right);
            int b_factor = l - r;
            return b_factor;
        }

        private Node RotateRR(Node parent)
        {
            Node pivot = parent.right;
            parent.right = pivot.left;
            pivot.left = parent;
            return pivot;
        }

        private Node RotateLL(Node parent)
        {
            Node pivot = parent.left;
            parent.left = pivot.right;
            pivot.right = parent;
            return pivot;
        }

        private Node RotateLR(Node parent)
        {
            Node pivot = parent.left;
            parent.left = RotateRR(pivot);
            return RotateLL(parent);
        }
        private Node RotateRL(Node parent)
        {
            Node pivot = parent.right;
            parent.right = RotateLL(pivot);
            return RotateRR(parent);
        }
    }

    public class PrimMinimumSpanningTree
    {
        int koseSayisi; // çizgedeki köşe sayısı

        public PrimMinimumSpanningTree(int k)
        {
            this.koseSayisi = k;  // MST objesi agacın köşe sayısını argüman alarak oluşturuluyor.
        }

        public int minDeger(int[] key, bool[] mstSet) // Henüz ağaca dahil edilmemiş köşelerden minimum değerlikli olanı bulan metot
        {
            int min = int.MaxValue, min_index = -1;

            for (int v = 0; v < koseSayisi; v++)
                if (mstSet[v] == false && key[v] < min)
                {
                    min = key[v];
                    min_index = v;
                }

            return min_index;
        }

        public void yazdir(int[] mstDizisi, int[,] matris) // MST'yi yazdırmaya yarayan metot. MST ögelerini tutan dizi ve 2 boyutlu komşuluk matrisi argüman olarak gönderilir. 
        {
            Console.WriteLine("Kenar \tAğırlık");
            for (int i = 1; i < koseSayisi; i++)
                Console.WriteLine(mstDizisi[i] + " - " + i + "\t" + matris[i, mstDizisi[i]]);
        }

        public void MSTbul(int[,] matris) // çizgeyi iki boyutlu komşuluk matrisi şeklinde argüman olarak alan, MST'yi bulan ve yazdıran metot
        {

            int[] parent = new int[koseSayisi]; //oluşturulacak MST'yi tutacak olan dizi. Boyutu köşe sayısı kadardır, çünkü MST'nin bütün köşeleri içermesi gerekir.

            int[] key = new int[koseSayisi]; // minimum ağırlıklı kenarların değerlerini tutan dizi

            bool[] mstSet = new bool[koseSayisi]; // algoritma işletimi esnasında uğranmış köşelerin bilgilerini tutan boolean dizisi.

            for (int i = 0; i < koseSayisi; i++)
            {
                key[i] = int.MaxValue;
                mstSet[i] = false;
            }

            // ilk düğüm MST'nin kökü olarak kabul edilir ve 0 değeri atanır
            key[0] = 0;
            parent[0] = -1;

            for (int count = 0; count < koseSayisi - 1; count++)
            {
                int u = minDeger(key, mstSet);

                mstSet[u] = true;

                for (int v = 0; v < koseSayisi; v++)

                    if (matris[u, v] != 0 && mstSet[v] == false && matris[u, v] < key[v])
                    {
                        parent[v] = u;
                        key[v] = matris[u, v];
                    }
            }

            yazdir(parent, matris);
        }
    }

    public class DijkstraShortestPath
    {
        public DijkstraShortestPath() { }

        public int Distance(int[] distance, bool[] shortestPathTreeSet, int verticesCount)
        {
            int min = int.MaxValue;
            int minIndex = 0;

            for (int v = 0; v < verticesCount; ++v)
            {
                if (shortestPathTreeSet[v] == false && distance[v] <= min)
                {
                    min = distance[v];
                    minIndex = v;
                }
            }

            return minIndex;
        }

        public void Find(int[,] graph, int source, int verticesCount)
        {
            int[] distance = new int[verticesCount];
            bool[] shortestPathTreeSet = new bool[verticesCount];

            for (int i = 0; i < verticesCount; ++i)
            {
                distance[i] = int.MaxValue;
                shortestPathTreeSet[i] = false;
            }

            distance[source] = 0;

            for (int count = 0; count < verticesCount - 1; ++count)
            {
                int u = Distance(distance, shortestPathTreeSet, verticesCount);
                shortestPathTreeSet[u] = true;

                for (int v = 0; v < verticesCount; ++v)
                    if (!shortestPathTreeSet[v] && Convert.ToBoolean(graph[u, v]) && distance[u] != int.MaxValue && distance[u] + graph[u, v] < distance[v])
                        distance[v] = distance[u] + graph[u, v];
            }

            Console.WriteLine("Köşe    {0} Nolu Köşeye Olan En Kısa Mesafesi", source);
            for (int i = 0; i < verticesCount; ++i)
                Console.WriteLine("{0}\t  {1}", i, distance[i]);
        }
    }

    public class Cizge
    {
        public int koseSayisi;
        public List<int>[] komsulukDizisi;

        public Cizge(int k)
        {
            this.koseSayisi = k;
            komsulukDizisi = new List<int>[k];
            for (int i = 0; i < koseSayisi; ++i)
                komsulukDizisi[i] = new List<int>();
        }

        public void AddEdge(int v, int w)
        {
            komsulukDizisi[v].Add(w);
        }

        void DFSUtil(int v, bool[] visited)
        {
            visited[v] = true;
            Console.Write(v + " ");

            List<int> vList = komsulukDizisi[v];
            foreach (var n in vList)
            {
                if (!visited[n])
                    DFSUtil(n, visited);
            }
        }

        public void DFS(int v)
        {
            bool[] visited = new bool[koseSayisi];

            DFSUtil(v, visited);
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            AVL tree = new AVL();
            tree.Add(10);
            tree.Add(3);
            tree.Add(15);
            tree.Add(2);
            tree.Add(7);
            tree.Add(12);
            tree.Add(1);
            tree.Add(5);

            tree.DisplayAVL();

            ////////// 2 - a) //////////
            Console.Write("\n\n\nAVL'ye eklenmesini istediğiniz değeri giriniz : ");
            tree.Add(int.Parse(Console.ReadLine()));

            tree.DisplayAVL();

            ////////// 2 - a) //////////
            Console.Write("\n\n\nAVL'ye eklenmesini istediğiniz değeri giriniz : ");
            tree.Add(int.Parse(Console.ReadLine()));

            tree.DisplayAVL();

            ///////// 4 - 1) //////////
            Console.Write("\n\n\n4. sorunun 1. adımına geçmek için ENTER'a basınız : ");
            Console.ReadLine();

            int[,] matris =  {
                          { 0, 6, 0, 0, 0, 0, 0, 9 },
                          { 6, 0, 9, 0, 0, 0, 0, 11 },
                          { 0, 9, 0, 5, 0, 6, 0, 0 },
                          { 0, 0, 5, 0, 9, 0, 16, 0 },
                          { 0, 0, 0, 9, 0, 10, 0, 0 },
                          { 0, 0, 6, 0, 10, 0, 2, 0 },
                          { 0, 0, 0, 16, 0, 2, 0, 1 },
                          { 9, 11, 0, 0, 0, 0, 1, 0 }
                            };

            DijkstraShortestPath dijkstraShortestPath = new DijkstraShortestPath();
            dijkstraShortestPath.Find(matris, 2, 8);

            ///////// 4 - 2) //////////
            Console.Write("\n\n\n4. sorunun 2. adımına geçmek için ENTER'a basınız : ");
            Console.ReadLine();

            matris = new int[,] { { 0, 2, 0, 6, 0 },
                                  { 2, 0, 3, 8, 5 },
                                  { 0, 3, 0, 0, 7 },
                                  { 6, 8, 0, 0, 9 },
                                  { 0, 5, 7, 9, 0 } };

            PrimMinimumSpanningTree primMinimumSpanningTree = new PrimMinimumSpanningTree(5);

            primMinimumSpanningTree.MSTbul(matris);

            ///////// 4 - 3) //////////
            Console.Write("\n\n\n4. sorunun 3. adımına geçmek için ENTER'a basınız : ");
            Console.ReadLine();

            Cizge mCizge = new Cizge(4);

            mCizge.AddEdge(0, 1);
            mCizge.AddEdge(0, 2);
            mCizge.AddEdge(1, 2);
            mCizge.AddEdge(2, 0);
            mCizge.AddEdge(2, 3);
            mCizge.AddEdge(3, 3);

            Console.WriteLine("\n2 Nolu Köşeden Başlayarak Derinlik Öncelikli Dolaşma : ");

            mCizge.DFS(2);

            Console.ReadKey();
        }
    }
}
