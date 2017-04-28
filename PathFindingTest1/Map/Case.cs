using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PathFindingTest1.Map
{
    public class Case
    {
        [Flags]
        private enum Direction
        {
            Down,
            Up,
            Left,
            Right,
        }

        public int PosX { get; set; }
        public int PosY { get; set; }
        public byte Type { get; set; }

        public float Width { get; set; }
        public float Height { get; set; }

        public int GCost { get; set; }
        public int HCost { get; set; }
        public int FCost { get; set; }
        public Case ParentCase { get; set; }

        Grille m_Grille;
        public bool Checked { get; set; }

        public Case(byte type, int X, int Y, Grille Grille)
        {
            PosX = X;
            PosY = Y;
            Type = type;
            m_Grille = Grille;
            GCost = 0;
            HCost = 0;
            FCost = 0;
        }

        public void Draw(Graphics g)
        {
            Color c =
                Type == 0 ? Color.White :
                Type == 1 ? Color.Gray :
                Type == 2 ? Color.Red :
                Type == 3 ? Color.Blue :
                Type == 4 ? Color.Orange :
                Type == 5 ? Color.Purple :
                Color.Green;
            SolidBrush b = new SolidBrush(c);
            g.FillRectangle(b, PosX * Width, PosY * Height, Width - 1, Height - 1);
            if (this.ParentCase != null)
            {
                if (this.ParentCase.PosY > this.PosY)
                {
                    DrawArrow(g, c, Direction.Down);
                }
                else if (this.ParentCase.PosY < this.PosY)
                {
                    DrawArrow(g, c, Direction.Up);
                }
                if (this.ParentCase.PosX > this.PosX)
                {
                    DrawArrow(g, c, Direction.Right);
                }
                else if (this.ParentCase.PosX < this.PosX)
                {
                    DrawArrow(g, c, Direction.Left);
                }
            }

            if (this.FCost != 0)
            {
                g.DrawString(FCost.ToString(), new Font("Arial", 5, FontStyle.Bold), new SolidBrush(Color.Black), this.PosX * Width + 0.1f * Width, this.PosY * Height + 0.1f * Height);
            }
        }

        private void DrawArrow(Graphics g, Color c, Direction d)
        {
            PointF topLeft = new PointF(this.PosX * Width, this.PosY * Height);
            SolidBrush b = new SolidBrush(Color.FromArgb(unchecked(~(c.ToArgb()) | (int)0xff000000)));
            GraphicsPath gp;
            
            switch (d)
            {
                default:
                case (Direction.Up):
                    gp = new GraphicsPath(new PointF[]
                        {
                            new PointF(topLeft.X + 0.4f * this.Width, topLeft.Y + 0.6f * this.Height),
                            new PointF(topLeft.X + 0.6f * this.Width, topLeft.Y + 0.6f * this.Height),
                            new PointF(topLeft.X + 0.6f * this.Width, topLeft.Y + 0.2f * this.Height),
                            new PointF(topLeft.X + 0.8f * this.Width, topLeft.Y + 0.2f * this.Height),
                            new PointF(topLeft.X + 0.5f * this.Width, topLeft.Y),
                            new PointF(topLeft.X + 0.2f * this.Width, topLeft.Y + 0.2f * this.Height),
                            new PointF(topLeft.X + 0.4f * this.Width, topLeft.Y + 0.2f * this.Height)
                        },
                        new byte[]
                        {
                            (byte)PathPointType.Start,
                            (byte)PathPointType.Line,
                            (byte)PathPointType.Line,
                            (byte)PathPointType.Line,
                            (byte)PathPointType.Line,
                            (byte)PathPointType.Line,
                            (byte)PathPointType.Line
                        });
                    break;
                case (Direction.Down):
                    gp = new GraphicsPath(new PointF[]
                        {
                            new PointF(topLeft.X + 0.4f * this.Width, topLeft.Y + 0.4f * this.Height),
                            new PointF(topLeft.X + 0.6f * this.Width, topLeft.Y + 0.4f * this.Height),
                            new PointF(topLeft.X + 0.6f * this.Width, topLeft.Y + 0.8f * this.Height),
                            new PointF(topLeft.X + 0.8f * this.Width, topLeft.Y + 0.8f * this.Height),
                            new PointF(topLeft.X + 0.5f * this.Width, topLeft.Y + this.Height),
                            new PointF(topLeft.X + 0.2f * this.Width, topLeft.Y + 0.8f * this.Height),
                            new PointF(topLeft.X + 0.4f * this.Width, topLeft.Y + 0.8f * this.Height)
                        },
                        new byte[]
                        {
                            (byte)PathPointType.Start,
                            (byte)PathPointType.Line,
                            (byte)PathPointType.Line,
                            (byte)PathPointType.Line,
                            (byte)PathPointType.Line,
                            (byte)PathPointType.Line,
                            (byte)PathPointType.Line
                        });
                    break;
                case (Direction.Left):
                    gp = new GraphicsPath(new PointF[]
                        {
                            new PointF(topLeft.X + 0.6f * this.Width, topLeft.Y + 0.6f * this.Height),
                            new PointF(topLeft.X + 0.6f * this.Width, topLeft.Y + 0.4f * this.Height),
                            new PointF(topLeft.X + 0.2f * this.Width, topLeft.Y + 0.4f * this.Height),
                            new PointF(topLeft.X + 0.2f * this.Width, topLeft.Y + 0.2f * this.Height),
                            new PointF(topLeft.X, topLeft.Y + 0.5f * this.Height),
                            new PointF(topLeft.X + 0.2f * this.Width, topLeft.Y + 0.8f * this.Height),
                            new PointF(topLeft.X + 0.2f * this.Width, topLeft.Y + 0.6f * this.Height)
                        },
                        new byte[]
                        {
                            (byte)PathPointType.Start,
                            (byte)PathPointType.Line,
                            (byte)PathPointType.Line,
                            (byte)PathPointType.Line,
                            (byte)PathPointType.Line,
                            (byte)PathPointType.Line,
                            (byte)PathPointType.Line
                        });
                    break;
                case (Direction.Right):
                    gp = new GraphicsPath(new PointF[]
                        {
                            new PointF(topLeft.X + 0.4f * this.Width, topLeft.Y + 0.6f * this.Height),
                            new PointF(topLeft.X + 0.4f * this.Width, topLeft.Y + 0.4f * this.Height),
                            new PointF(topLeft.X + 0.8f * this.Width, topLeft.Y + 0.4f * this.Height),
                            new PointF(topLeft.X + 0.8f * this.Width, topLeft.Y + 0.2f * this.Height),
                            new PointF(topLeft.X + this.Width, topLeft.Y + 0.5f * this.Height),
                            new PointF(topLeft.X + 0.8f * this.Width, topLeft.Y + 0.8f * this.Height),
                            new PointF(topLeft.X + 0.8f * this.Width, topLeft.Y + 0.6f * this.Height)
                        },
                        new byte[]
                        {
                            (byte)PathPointType.Start,
                            (byte)PathPointType.Line,
                            (byte)PathPointType.Line,
                            (byte)PathPointType.Line,
                            (byte)PathPointType.Line,
                            (byte)PathPointType.Line,
                            (byte)PathPointType.Line
                        });
                    break;


            }
            g.FillPath(b, gp);

        }


        public void Draw(Graphics g, Color c)
        {
            SolidBrush b = new SolidBrush(c);
            g.FillRectangle(b, PosX * Width, PosY * Height, Width - 1, Height - 1);
            
        }

        

        public byte getType()
        {
            return Type;
        }

        public void setType(byte type)
        {
            Type = type;
        }

        public void setSize(float W, float H)
        {
            Width = W;
            Height = H;
        }

        public Point getPos()
        {
            return new Point(PosX, PosY);
        }

        public List<Case> getNeighbors()
        {
            List<Case> Neighbors = new List<Case>();
            bool bLeft, bRight, bUp, bDown;

            bLeft = PosX > 0;
            bRight = PosX < m_Grille.CaseAmount.Width - 1;
            bUp = PosY > 0;
            bDown = PosY < m_Grille.CaseAmount.Height - 1;

            Case cLeft, cRight, cUp, cDown, c;

            if (bLeft)
            {
                cLeft = m_Grille.GetCaseAtIndex(new Point(PosX - 1, PosY));
                if (!(cLeft.Checked || cLeft.getType() == 1))
                    Neighbors.Add(cLeft);
            }

            if (bRight)
            {
                cRight = m_Grille.GetCaseAtIndex(new Point(PosX + 1, PosY));
                if (!(cRight.Checked || cRight.getType() == 1))
                    Neighbors.Add(cRight);
            }

            if (bDown)
            {
                cDown = m_Grille.GetCaseAtIndex(new Point(PosX, PosY + 1));

                if (!(cDown.Checked || cDown.getType() == 1))
                    Neighbors.Add(cDown);

                if (bRight)
                {
                    cRight = m_Grille.GetCaseAtIndex(new Point(PosX + 1, PosY));
                    c = m_Grille.GetCaseAtIndex(new Point(PosX + 1, PosY + 1));
                    if (!(c.Checked || c.getType() == 1) && cRight.getType() != 1 && cDown.getType() != 1)
                        Neighbors.Add(c);
                }

                if (bLeft)
                {
                    cLeft = m_Grille.GetCaseAtIndex(new Point(PosX - 1, PosY));
                    c = m_Grille.GetCaseAtIndex(new Point(PosX - 1, PosY + 1));
                    if (!(c.Checked || c.getType() == 1) && cLeft.getType() != 1 && cDown.getType() != 1)
                        Neighbors.Add(c);
                }
                
            }

            if (bUp)
            {
                cUp = m_Grille.GetCaseAtIndex(new Point(PosX, PosY - 1));
                if (!(cUp.Checked || cUp.getType() == 1))
                    Neighbors.Add(cUp);

                if (bLeft)
                {
                    cLeft = m_Grille.GetCaseAtIndex(new Point(PosX - 1, PosY));
                    c = m_Grille.GetCaseAtIndex(new Point(PosX - 1, PosY - 1));
                    if (!(c.Checked || c.getType() == 1) && cLeft.getType() != 1 && cUp.getType() != 1)
                        Neighbors.Add(c);
                }

                if (bRight)
                {
                    cRight = m_Grille.GetCaseAtIndex(new Point(PosX + 1, PosY));
                    c = m_Grille.GetCaseAtIndex(new Point(PosX + 1, PosY - 1));
                    if (!(c.Checked || c.getType() == 1) && cRight.getType() != 1 && cUp.getType() != 1)
                        Neighbors.Add(c);
                }
            }

            return Neighbors;
        }

        public void CalculateCost(Case Parent, Case End)
        {
            int G, H, F;
            G = Parent.GCost + 10;
            if (Math.Abs(Parent.PosY - this.PosY) + Math.Abs(Parent.PosX - this.PosX) > 1)
                G += 4;
            H = (Math.Abs(this.PosY - End.PosY) + Math.Abs(this.PosX - End.PosX)) * 10;
            F = G + H;

            if (ParentCase == null)
            {
                GCost = G;
                HCost = H;
                FCost = F;
                ParentCase = Parent;
            }
            else
            {
                if (F < FCost)
                {
                    ParentCase = Parent;
                    GCost = G;
                    HCost = H;
                    FCost = F;
                }

            }
        }

        public void CalculateCost(int G, Case End)
        {
            GCost = G;

            HCost = (Math.Abs(this.PosY - End.PosY) + Math.Abs(this.PosX - End.PosX)) * 10;

            FCost = GCost + HCost;
        }
    }
}
