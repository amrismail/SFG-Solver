using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SFGSOLVER;

namespace SFGSOLVER
{
    public partial class Form1 : Form
    {
        List<Node> nodes;
        Node Start;
        Node End;
        private Stack<Node> testStack;
        List<Line> lines;
        List<List<Node>> Loops;
        List<List<Node>> Paths;
        Line tempLine;
        public enum operations
        {
            Node, Path, DeleteNode, Evaluate, Disconnect, StartNode, EndNode, noOperation

        };

        operations nowOperation;
        public Form1()
        {
            InitializeComponent();
            nodes = new List<Node>();
            lines = new List<Line>();
            Paths = new List<List<Node>>();
            Loops = new List<List<Node>>();
            tempLine = new Line(null, null);
            textBox1.Visible = false;
            testStack = new Stack<Node>();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            nowOperation = operations.Node;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (nowOperation == operations.Node)
            {
                nodes.Add(new Node(new Point(e.X, e.Y)));
                textBox1.Visible = true;
                //textBox1.foc
                Refresh();
            }
            else if (nowOperation == operations.Path)
            {
                foreach (Node x in nodes)
                {
                    if (x.contain(new Point(e.X, e.Y)))
                    {
                        if (tempLine.Start == null)
                        {
                            tempLine.Start = x;
                        }
                        else
                        {
                            tempLine.End = x;
                            tempLine.Start.neighbours.Add(tempLine.End);
                            lines.Add(tempLine);
                            tempLine = new Line(null, null);
                            textBox1.Visible = true;
                            Refresh();
                        }
                    }
                }

            }
            else if (nowOperation == operations.StartNode)
            {
                foreach (Node x in nodes)
                {
                    if (x.contain(new Point(e.X, e.Y)))
                    {
                        if (Start == null)
                        {
                            Start = x;
                            x.isStart = true;
                        }
                        else
                        {
                            Start.isStart = false;
                            x.isStart = true;
                            Start = x;

                            Refresh();
                        }
                    }
                }

            }
            else if(nowOperation == operations.DeleteNode)
            {
                List<int> temp1 = new List<int>();
                foreach (Node x in nodes)
                {
                    if (x.contain(new Point(e.X, e.Y)))
                    {
                        NotifyAll(x);
                        List<int> temp = new List<int>();
                        foreach (Line y in lines)
                        {
                            if (y.Start == x || y.End == x)
                            {
                                temp.Add(lines.IndexOf(y));

                            }
                        }
                        foreach(int z in temp)
                        {
                            lines.Remove(lines[z]);
                        }
                        temp1.Add(nodes.IndexOf(x));
                    }
                }
                foreach (int z in temp1)
                {
                    nodes.Remove(nodes[z]);
                }
            }
            else if (nowOperation == operations.EndNode)
            {
                foreach (Node x in nodes)
                {
                    if (x.contain(new Point(e.X, e.Y)))
                    {
                        if (End == null)
                        {
                            End = x;
                            x.isEnd = true;
                        }
                        else
                        {
                            End.isEnd = false;
                            x.isEnd = true;
                            End = x;

                            Refresh();
                        }
                    }
                }

            }Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            foreach (Node x in nodes)
                x.Draw(e.Graphics);
            foreach (Line x in lines)
                x.Draw(e.Graphics);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            nowOperation = operations.Path;
        }
        private void AddPath(Node root, int type)
        {
            List<Node> temp = new List<Node>();
            temp.Add(root);
            for (int i = testStack.Count-1; i>=0; i--)
            {
                temp.Add(testStack.ElementAt(i));
            }
            if (type == 0)
                 Paths.Add(temp);
            else Loops.Add(temp);
        }

        public void DFS(Node nowNode, Node end, Node root, int type)
        {
            if (nowNode == end && testStack.Count != 0)
            {
                AddPath(root,type);
                testStack.Pop().isVisited = false;
                return;
            }
            else if (nowNode.neighbours.Count == 0)
            {
                if (testStack.Count != 0)
                    testStack.Pop().isVisited = false;
                return;
            }
            else {
                foreach (Node x in nowNode.neighbours)
                {
                    if (!x.isVisited)
                    {
                        x.isVisited = true;
                        testStack.Push(x);
                        DFS(x, end, root,type);
                    }
                }
                if (testStack.Count != 0)
                    testStack.Pop().isVisited = false;
            }
        }
        public List<Line> CalcLines(List<Node> x)
        {
            List<Line> returnValue = new List<Line>();
            for (int i = 0; i < x.Count - 1; i++)
            {
                foreach (Line y in lines)
                    if (y.Start == x[i] && y.End == x[i + 1])
                    {
                        returnValue.Add(y);
                        break;
                    }
            }
            return returnValue;
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (nowOperation == operations.Path)
                {
                    lines[lines.Count - 1].gain = textBox1.Text;
                    textBox1.Visible = false;
                    textBox1.Text = "";
                }
                else if(nowOperation == operations.Node)
                {
                    nodes[nodes.Count - 1].Name = textBox1.Text;
                    textBox1.Visible = false;
                    textBox1.Text = "";
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            nowOperation = operations.StartNode;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            nowOperation = operations.EndNode;

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Answer.Visible = true;
            DFS(Start, End, Start, 0);
            foreach(Node x in nodes)
            {
                DFS(x, x, x, 1);
                x.isVisited = true;
            }
            Answer.Text = "Paths : \n";
            foreach(List<Node> x in Paths)
            {
                foreach(Node y in x)
                {
                    Answer.AppendText(y.Name);
                }
                Answer.AppendText("\n\nLoops :\n");
            }
            foreach (List<Node> x in Loops)
            {
                foreach (Node y in x)
                {
                    Answer.AppendText(y.Name);
                }
            }
            foreach (Node x in nodes)
            {
                x.isVisited = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            nowOperation = operations.DeleteNode;
        }
         
        private void NotifyAll(Node x)
        {
            foreach(Node y in nodes)
            {
                x.Notify(x);
            }
        }
    }
}
