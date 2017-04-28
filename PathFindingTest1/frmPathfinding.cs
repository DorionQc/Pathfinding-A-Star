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
using System.Threading;
using System.Windows.Forms;

using PathFindingTest1.Pathfinding;
using PathFindingTest1.Map;

namespace PathFindingTest1
{
    public partial class frmPathfinding : Form
    {

        Grille m_Grille;
        bool m_MiddlePressed;
        bool m_Drawing;
        List<Path> m_lPaths;
        bool m_Animer;


        public frmPathfinding()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            m_Grille = new Grille(new Size(30, 30), this.ClientSize);
            m_MiddlePressed = false;
            m_Drawing = false;
            m_lPaths = new List<Path>();
            m_Animer = true;
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
                if (m_Drawing)
                    return;
                Thread t = new Thread(new ThreadStart(Trace));
                t.Start();
                m_Drawing = true;
            }
            else if (e.KeyChar == 'r')
            {
                m_lPaths.ForEach((p) => p.Stop());
                m_Grille.Reset();
                this.Invalidate();
            }
            else if (e.KeyChar == 'a')
            {
                m_Animer = !m_Animer;
                MessageBox.Show("Animer = " + m_Animer.ToString());
                m_lPaths.ForEach((p) => p.ChangeAnimer(m_Animer));
            }
        }

        private void Trace()
        {
           
            Path p = new Path(m_Grille.getBeginPoint(), m_Grille.getEndPoint(), m_Grille, m_Animer);
            m_lPaths.Add(p);
            p.Trace(this);
            m_Drawing = false;
        }

        private void frmPathfinding_Shown(object sender, EventArgs e)
        {
            MessageBox.Show("Sur la souris :\nBouton gauche = Point de départ\nBouton du milieu (click de roulette) = Point de fin\nBouton droit (enfoncé) = Murs\n\n" +
                            "Sur le clavier :\nTouche # (à côté du 1) = Exécuter\nTouche 'R' = Réinitialiser.\nTouche 'A' = Enlever l'animation");
        }
    }
}
