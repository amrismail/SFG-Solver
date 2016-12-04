using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SFGSOLVER
{
    public class Line
    {
        public string gain;
        public Node Start;
        public Node End;
        public Point centre;
        //Cursor.Position = new Point(x, y);
        public Line (Node Start , Node End)
        {
            this.Start = Start;
            this.End = End;

        }
        public void Draw (Graphics g)
        {
            if (Start == End)
            {
                //g.DrawBezier(Pens.Blue,Start.centre,,,End.centre)
               // g.DrawEllipse(Pens.Black, centre.X-10, centre.Y-10, 30, 20);
                // g.DrawLine(Pens.Blue, Start.centre,Start.centre);
               // g.DrawEllipse(Pens.Black, Cursor.Position.X-70, Cursor.Position.Y-70, 5, 40);
            } 
            else g.DrawLine(Pens.Black, Start.centre, End.centre);

            //g.DrawBezier(Pens.Black, Start.centre,,, End.centre);
        }
         
    }  
}
