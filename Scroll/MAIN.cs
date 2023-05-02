using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Media;

namespace Scroll
{
    public partial class MAIN : Form
    {
        Map map;
        Player player;
        
        
        float fElapsedTime;

        SoundPlayer sPlayer;
        Thread thread, thread2;
        bool isP1 = true;

       

        // Camera properties
        float fCameraPosX = 0.0f;
        float fCameraPosY = 0.0f;
        bool left, right, up, down;

        public MAIN()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            map                 = new Map(PCT_CANVAS.Size);
            player              = new Player();
            PCT_CANVAS.Image    = map.bmp;
            fElapsedTime        = 0.05f;
            left                = false;
            right               = false;
            sPlayer             = new SoundPlayer(Resource1._412843__inspectorj__boiling_water_large_a);

            Play();
        }

        public void Play()
        {
            thread = new Thread(PlayThread);
            thread.Start();
        }

        private void PlayThread()
        {
            sPlayer.PlaySync();
        }

        private void MAIN_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:                    
                    left = true;
                    //nDirModY = 1;
                    break;
                case Keys.Right:
                    right = true;
                    //nDirModY = 0;
                    break;
                case Keys.Up:
                    player.FPlayerVelY = -6.0f;
                    player.bPlayerOnGround = false;
                    up = true;
                    break;
                case Keys.Down:
                    player.FPlayerVelY = 6.0f;
                    down = true;
                    break;
            }

            UpdateEnv();
        }

        private void MAIN_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case (char)Keys.Space:
                    if (player.FPlayerVelY == 0)// sin brincar o cayendo
                    {
                        player.FPlayerVelY = -5;
                        player.Frame(2);
                        player.bPlayerOnGround = false;
                    }
                    break;
            }
        }



        private void MAIN_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
                return;

            left = false;
            right = false;

            up = false;
            down= false;

            player.Stop();
        }

        private void PCT_CANVAS_Click(object sender, EventArgs e)
        {

        }
       

        private void BackgroundMove()
        {
            if (map.l2_x1 < -map.width) { map.l2_x1 = map.width - map.motion2; }
            map.l2_x1 -= map.motion2; map.l2_x2 -= map.motion2;
            if (map.l2_x2 < -map.width) { map.l2_x2 = map.width - map.motion2; }

            if (map.l1_x1 < -map.width) { map.l1_x1 = map.width - map.motion1; }
            map.l1_x1 -= map.motion1; map.l1_x2 -= map.motion1;
            if (map.l1_x2 < -map.width) { map.l1_x2 = map.width - map.motion1; }

            if (map.l3_x1 < -map.width) { map.l3_x1 = map.width - map.motion3; }
            map.l3_x1 -= map.motion3; map.l3_x2 -= map.motion3;
            if (map.l3_x2 < -map.width) { map.l3_x2 = map.width - map.motion3; }
        }

        private void BackgroundMoveUp()
        {
            if (map.l1_y1 > map.height) { map.l1_y1 = -map.height + map.motion1; map.l1_y2 = map.motion1; }
            map.l1_y1 += map.motion1; map.l1_y2 += map.motion1;
        }

        private void BackgroundMoveDown()
        {
            if (map.l1_y1 < -map.height) { map.l1_y1 = map.height - map.motion1; map.l1_y2 = map.height + map.motion1; }
            map.l1_y1 -= map.motion1; map.l1_y2 -= map.motion1;
        }

        private void BackgroundMoveLeft()
        {
            if (map.l2_x1 > map.width) { map.l2_x1 = -map.width + map.motion2; }
            map.l2_x1 += map.motion2; map.l2_x2 += map.motion2;
            if (map.l2_x2 > map.width) { map.l2_x2 = -map.width + map.motion2; }

            if (map.l1_x1 > map.width) { map.l1_x1 = -map.width + map.motion1; }
            map.l1_x1 += map.motion1; map.l1_x2 += map.motion1;
            if (map.l1_x2 > map.width) { map.l1_x2 = -map.width + map.motion1; }

            if (map.l3_x1 > map.width) { map.l3_x1 = -map.width + map.motion3; }
            map.l3_x1 += map.motion3; map.l3_x2 += map.motion3;
            if (map.l3_x2 > map.width) { map.l3_x2 = -map.width + map.motion3; }
        }


        private void TIMER_Tick(object sender, EventArgs e)
        {
            
            if (right)
            {
                BackgroundMove();
            }
            if (left)
            {
                BackgroundMoveLeft();
            }
            if (up)
            {
                BackgroundMoveUp();
            }

            if (down)
            {
                BackgroundMoveDown();
            }
            UpdateEnv();
            
            if(player.over==true)
            {
                TIMER.Stop();
                
                DialogResult result = MessageBox.Show("Sorry, You lost. Try again!", "Game Over", MessageBoxButtons.RetryCancel);
                if (result == DialogResult.Retry)
                {
                    Application.Restart();
                }
                else if (result == DialogResult.Cancel)
                {
                    Application.Exit();

                }
                
            }
            
        }

        private void UpdateEnv()
        {
            if (left)
                player.Left(fElapsedTime);
            if (right)
                player.Right(fElapsedTime);
            
            fCameraPosX = player.fPlayerPosX;
            fCameraPosY = player.fPlayerPosY;

            map.Draw(new PointF(fCameraPosX,fCameraPosY),player.fPlayerPosX.ToString() , player);
            player.Update(fElapsedTime, map);
            PCT_CANVAS.Invalidate();

            
            

        }
    }
}
