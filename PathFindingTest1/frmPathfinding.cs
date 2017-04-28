/****************************
 * Samuel Goulet
 * Aout 2016
 * Figuring out Pathfinding algorithm (A*)
 * **************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using PathFindingTest1.Pathfinding;
using PathFindingTest1.Map;

namespace PathFindingTest1
{
    public partial class frmPathfinding : Form
    {

        Grille m_Grille;
        bool m_MiddlePressed;


        public frmPathfinding()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            m_Grille = new Grille(new Size(30, 30), this.ClientSize);
            m_MiddlePressed = false;
            m_Grille.Initialise();
        }

        private void frmPathfinding_MouseClick(object sender, MouseEventArgs e)
        {
            m_Grille.HandleClick(e.Button, e.Location);
            this.Refresh();
        }

        private void frmPathfinding_Paint(object sender, PaintEventArgs e)
        {
            m_Grille.Draw(e.Graphics);
        }

        private void frmPathfinding_Resize(object sender, EventArgs e)
        {
            this.Refresh();
            m_Grille.Resize(this.ClientSize);
        }

        private void frmPathfinding_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                m_MiddlePressed = true;
            }
        }

        private void frmPathfinding_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                m_MiddlePressed = false;
            }
        }

        private void frmPathfinding_MouseMove(object sender, MouseEventArgs e)
        {
            if (m_MiddlePressed)
            {
                m_Grille.HandleMouseMove(e.Location);
                this.Refresh();
            }
        }

        private void frmPathfinding_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '#')
            {
                Path p = new Path(m_Grille.getBeginPoint(), m_Grille.getEndPoint(), m_Grille);
                p.Trace(this);
            }
        }


    }
}
