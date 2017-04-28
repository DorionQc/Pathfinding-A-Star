using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PathFindingTest1.Map;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace PathFindingTest1.Pathfinding
{
    public class Path
    {
        Case m_Begin;
        Case m_End;

        List<Case> m_ListPath;

        List<Case> m_ListToCheck;

        Grille m_Grille;

        public Path(Case Begin, Case End, Grille Grille)
        {
            m_Begin = Begin;
            m_End = End;
            m_ListPath = new List<Case>();
            m_ListToCheck = new List<Case>();
            m_Grille = Grille;
        }

        // Based on A* Algorithm
        public void Trace(Form f)
        {
            int Iteration = 0;
            m_ListToCheck.Add(m_Begin);
            List<Case> Neighbors = new List<Case>();
            Case c = m_Begin;

            c.CalculateCost(0, m_End);
            

            while (c != m_End && Iteration < 16384 && m_ListToCheck.Any() && c != null)
            {
                // Find lowest F-Value in ListToCheck
                // Check all cases around and calculate their G-Value
                //   If their G-Value is lower when using this new route, change it and change the parent
                // Add them all in ListToCheck
                // Add current case to ListChecked
                // Repeat

                c = FindLowestFCost();
                if (c != null)
                {
                    c.setType(5);
                    Neighbors = c.getNeighbors();
                    foreach (Case cn in Neighbors)
                    {
                        cn.CalculateCost(c, m_End);
                        m_ListToCheck.Add(cn);
                        cn.setType(5);
                        f.Refresh();
                        Thread.Sleep(5);
                        cn.setType(4);
                    }
                    m_ListToCheck.Remove(c);
                    c.Checked = true;
                    Iteration++;
                    f.Refresh();
                    c.setType(6);
                    if (c == m_Begin)
                        c.setType(2);
                    if (c == m_End)
                        c.setType(3);
                    Thread.Sleep(10);
                }

            }
            if (c != null && m_End.ParentCase != null)
            {
                c = m_End.ParentCase;
                while (c != m_Begin)
                {
                    m_ListPath.Add(c);
                    c.setType(5);
                    c = c.ParentCase;
                    f.Refresh();
                    Thread.Sleep(50);
                }
            }
            f.Refresh();
        }

        private Case FindLowestFCost()
        {
            m_ListToCheck.RemoveAll(cs => cs.Checked);
            if (m_ListToCheck.Count < 1)
            {
                return null;
            }
            else if (m_ListToCheck.Count == 1)
            {
                return m_ListToCheck[0];
            }
            int FCost = Int32.MaxValue;
            int I = 0;
            Case LowestCase = null;
            while (I < m_ListToCheck.Count)
            {
                if (m_ListToCheck[I].FCost < FCost)
                {
                    LowestCase = m_ListToCheck[I];
                    FCost = LowestCase.FCost;
                }
                I++;
            }
            return LowestCase;

        }

        public void Draw(Graphics g)
        {
            foreach (Case c in m_ListPath)
                c.setType(4);
            m_Grille.Draw(g);
        }

        public void Clear(Graphics g)
        {
            foreach (Case c in m_ListPath)
                c.setType(0);
            m_ListPath.Clear();
            m_Grille.Draw(g);
        }

    }
}
