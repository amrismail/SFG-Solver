using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFGSOLVER
{
    public class Node
    {
        public bool isStart = false;
        public bool isEnd = false;
        public bool isVisited;
        private static int radius = 5;
       public List<Node> neighbours;
        public Point centre;
        public String Name;
        public Node(Point x)
        {
            neighbours = new List<Node>();
            centre = x;

        } 
        public void Draw(Graphics g)
        {
            if (isStart)
            {
                g.FillEllipse(Brushes.Green, centre.X - radius, centre.Y - radius, 10, 10);
            }
            else if (isEnd)
            {
                g.FillEllipse(Brushes.Red, centre.X - radius, centre.Y - radius, 10, 10);
            }
            g.DrawEllipse(Pens.Black, centre.X - radius, centre.Y - radius, 10, 10);

        }

        public bool contain(Point x)
        {
            double distance = Math.Sqrt(Math.Pow(x.X - centre.X, 2) + Math.Pow(x.Y - centre.Y, 2));
            if (distance < radius) return true;
            return false;

        }
        public void Notify(Node x)
        {
            if (neighbours.Contains(x))
                neighbours.Remove(x);
        }

    } 
}
