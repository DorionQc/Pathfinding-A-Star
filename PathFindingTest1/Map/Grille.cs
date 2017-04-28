using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PathFindingTest1.Map
{
    public class Grille
    {
        Case BeginPoint;
        Case EndPoint;
        Size m_NumCase;
        Size m_Size;
        Case[,] m_tCase;

        float m_MarginH, m_MarginW;

        Case m_CursorLocation;

        public Size CaseAmount
        {
            get { return m_NumCase; }
        }

        public Grille(Size numCase, Size size)
        {
            m_NumCase = numCase;
            m_Size = size;
            m_tCase = new Case[m_NumCase.Width, m_NumCase.Height];
            m_CursorLocation = m_tCase[0, 0];
            m_MarginH = (float)m_Size.Height / m_NumCase.Height;
            m_MarginW = (float)m_Size.Width / m_NumCase.Width;
        }

        public Grille(Size numCase, int Width, int Height)
        {
            m_NumCase = numCase;
            m_Size = new Size(Width, Height);
            m_tCase = new Case[m_NumCase.Width, m_NumCase.Height];
            m_CursorLocation = m_tCase[0, 0];
            m_MarginH = (float)m_Size.Height / m_NumCase.Height;
            m_MarginW = (float)m_Size.Width / m_NumCase.Width;
        }

        public void Resize(Size s)
        {
            m_Size = s;
            m_MarginH = (float)m_Size.Height / m_NumCase.Height;
            m_MarginW = (float)m_Size.Width / m_NumCase.Width;
            foreach (Case c in m_tCase)
            {
                c.setSize(m_MarginW, m_MarginH);
            }
        }

        public void Initialise()
        {
            for (int I = 0; I < m_NumCase.Height; I++)
                for (int J = 0; J < m_NumCase.Width; J++)
                {
                    m_tCase[J, I] = new Case(0, J, I, this);
                }

            m_tCase[0, 0].setType(2);
            m_tCase[1, 0].setType(3);
            BeginPoint = m_tCase[0, 0];
            EndPoint = m_tCase[1, 0];
            Resize(m_Size);
        }

        public void HandleClick(MouseButtons m, Point p)
        {
            Case c = GetCaseAtLocation(p);
            if (m == MouseButtons.Left)
            {
                /*List<Case> ln = c.getNeighbors();
                StringBuilder sb = new StringBuilder();
                foreach (Case cn in ln)
                {
                    sb.Append(cn.getPos().X.ToString() + ", " + cn.getPos().Y.ToString() + "\n");
                    cn.setType(4);
                }
                MessageBox.Show(sb.ToString());*/
                if (c.getType() == 2)
                {
                    c.setType(0);
                    BeginPoint = null;
                }
                else
                {
                    BeginPoint.setType(0);
                    c.setType(2);
                    BeginPoint = c;
                }
            }
            else
                if (m == MouseButtons.Middle)
                {
                    if (c.getType() == 3)
                    {
                        c.setType(0);
                        EndPoint = null;
                    }
                    else
                    {
                        EndPoint.setType(0);
                        c.setType(3);
                        EndPoint = c;
                    }
                }
                else
                    if (m == MouseButtons.Right)
                    {
                        if (c.getType() == 1)
                            c.setType(0);
                        else if (c.getType() == 0)
                            c.setType(1);
                    }
        }

        public void HandleMouseMove(Point p)
        {
            Case c = GetCaseAtLocation(p);
            if (c != m_CursorLocation)
            {
                HandleMouseMoveCaseChange(c);
                m_CursorLocation = c;
            }
        }

        public void HandleMouseMoveCaseChange(Case c)
        {
            if (c.getType() == 1)
            {
                c.setType(0);
            }
            else
                if (c.getType() == 0)
                {
                    c.setType(1);
                }
        }

        public Case GetCaseAtLocation(Point p)
        {
            if (p.X < m_Size.Width && p.X > 0 && p.Y < m_Size.Height && p.Y > 0)
            {
                int x = (int)(p.X / ((float)m_Size.Width / m_NumCase.Width));
                int y = (int)(p.Y / ((float)m_Size.Height / m_NumCase.Height));
                return m_tCase[x, y];
            }
            else
                return m_tCase[0, 0];
        }

        public Case GetCaseAtIndex(Point p)
        {
            return m_tCase[p.X, p.Y];
        }

        public Case getBeginPoint()
        {
            return BeginPoint;
        }

        public Case getEndPoint()
        {
            return EndPoint;
        }

        public void Draw(Graphics g)
        {
            
            float Offset;
            Pen p = new Pen(Color.Black, 2);
            for (int I = 1; I < m_NumCase.Height; I++)
            {
                Offset = I * m_MarginH;
                g.DrawLine(p, 0, Offset, m_Size.Width, Offset);
            }
            
            for (int I = 1; I < m_NumCase.Width; I++)
            {
                Offset = I * m_MarginW;
                g.DrawLine(p, Offset, 0, Offset, m_Size.Height);
            }

            for (int I = 0; I < m_NumCase.Height; I++)
                for (int J = 0; J < m_NumCase.Width; J++)
                {
                    m_tCase[J, I].Draw(g);
                }
        }

        public void Reset()
        {
            Initialise();
        }
    }
}
