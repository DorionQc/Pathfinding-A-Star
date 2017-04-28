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

        bool m_Stop;
        bool m_Animer;

        object Lock;

        public Path(Case Begin, Case End, Grille Grille, bool Animer)
        {
            m_Begin = Begin;
            m_End = End;
            m_ListPath = new List<Case>();
            m_ListToCheck = new List<Case>();
            m_Stop = false;
            m_Animer = Animer;
            Lock = new object();
            m_Grille = Grille;
        }

        public void Stop()
        {
            lock (Lock)
            {
                m_Stop = true;
            }
        }

        public void ChangeAnimer(bool v)
        {
            lock (Lock)
            {
                m_Animer = v;
            }
        }

        // Based on A* Algorithm
        public void Trace(Form f)
        {
            int Iteration = 0;
            m_ListToCheck.Add(m_Begin);
            List<Case> Neighbors = new List<Case>();
            Case c = m_Begin;

            c.CalculateCost(0, m_End);
            

            while (c != m_End && Iteration < 16384 && m_ListToCheck.Any() && c != null && !m_Stop)
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
                        
                        if (m_Animer)
                        {
                            cn.setType(5);
                            Refresh(f);
                            Thread.Sleep(5);
                        }
                        
                        cn.setType(4);
                    }
                    m_ListToCheck.Remove(c);
                    c.Checked = true;
                    Iteration++;
                    if (m_Animer)
                    {
                        Refresh(f);
                    }
                    c.setType(6);
                    if (c == m_Begin)
                        c.setType(2);
                    if (c == m_End)
                        c.setType(3);
                    if (m_Animer)
                    {
                        Thread.Sleep(10);
                    }
                    
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
                    if (m_Animer)
                    {
                        Refresh(f);
                        Thread.Sleep(50);
                    }
                    
                }
            }
            Refresh(f);
        }

        private void Refresh(Form f)
        {
            try
            {
                f.Invoke(new Action(() => f.Refresh()));
            }
            catch (Exception)
            {
                // Le formulaire n'est plus accessible, sans doute a-t-il été fermé.
            }
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
