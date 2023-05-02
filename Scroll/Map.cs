using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Media;
using System.Threading;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;
using System.Collections;

namespace Scroll
{
    public class Map
    {
        int divs = 2;
        public int nTileWidth = 20;
        public int nTileHeight = 20;
        int nLevelWidth, nLevelHeight;
        private string map;
        public Bitmap bmp;
        public Graphics g;
        Sprite coin;
        SoundPlayer soundPlayer;
        int score;
        bool isP1 = true;
        Thread thread, threadStop;
        private Bitmap img, cat, wood, wood02, black, pinkbush, tree, enemy, rocks, water,diamond;
        private Bitmap sprite, spriteTrim, gifImage;
        Bitmap layer0, layer1, layer2, layer3;

        public int l1_x1, l1_x2, l2_x1, l2_x2, l3_x1, l3_x2;
        public int l1_y1, l1_y2, l2_y1, l2_y2, l3_y1, l3_y2;
        public int width = 400;
        public int height = 400;

       public int motion1 = 2;
        public int motion2 = 4;
        public int motion3 = 8;
        public int motion4 = 16;


        public Map(Size size)

        {
            cat = new Bitmap(Resource1.tile43); //piso
            wood = new Bitmap(Resource1.tile42);
            wood02 = new Bitmap(Resource1.tile52);
            black = new Bitmap(Resource1.tile11);
            pinkbush = new Bitmap(Resource1.pink_bush2);
            tree = new Bitmap(Resource1.trees2_1);
            enemy = new Bitmap(Resource1.pngwing_com);
            rocks = new Bitmap(Resource1.rocks1_5);
            diamond = new Bitmap(Resource1._33Ho);
            water = new Bitmap(Resource1.ola3);
            
            coin        = new Sprite(new Size(35, 33), new Size(nTileWidth, nTileHeight), new Point(), Resource1.coin, Resource1.coin);
            soundPlayer = new SoundPlayer(Resource1._344521__jeremysykes__coin04);            
            //Play();
            score = 0;

            map = "";
            map += "................................########.................................).....%..............................................................................................................................................................................";
            map += "$........................................$......................................#####.....&..........................................................%.........../.....)................................/......................................................$";
            map += "$..............................................................................................................................................................#########...................................................###################################$";
            map += "$....../..................%...........................................................................................................................$$$$$$$$$$$$$$$$$.......................$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$.....)......$$$$$$$$$$";
            map += "$......###........................................................................../...................................................................$$$$$$$$$$$$$$..............................)............................$$$$$$............$$$$$$$$$$$$$";
            map += "$.......................$.......................................................############......................................................................................$$........################........$$$.............&...............$$$$$$$$$$$$";
            map += "$......................................$.$......................................................................%.............................................&....................$.............$$$$$$$..............$..............................$$$$$$$$$$$";
            map += "$..................#$$$.............................................................................################...........................................................$..............................................................$$$$$$$$$$$$$$";
            map += "$........$$............................................./................................................$..............#####..(...................................................$.........########.........................../.........(..............(...$$$";
            map += "$............................%.......................######........................................(..$$$................$$$$...##...........................).....................$.................................#########..................###########.....";
            map += "$........*..&...........############..............#...).....####.(................................########.............../....).....................&..........####..(................(........................................................................$";
            map += "$..........+............$.(...)................###..............#####...%....................................................................................$$$$$####.............######...............###########..111......................................$";
            map += "$.......................$.###########$......##$......................######......................................................$.....................).......$$$$$$$$......................########......$....$..............................................$";
            map += "$......%........................................#.....................................(..........%................................$..../......######....................$$..................................................(............)......%........../....$";
            map += "#####################################################.............###############################################################$##########$$$$$$$$$$$$$$$$$$$$$$$$$$$$$######################################################################################$";
            map += ",,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,";
            map += ",,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,";
            map += ",,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,";
            map += ",,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,";
            map += ",,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,";
            map += ",,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,";
            map += ",,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,";
            map += ",,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,";
            map += ",,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,";
            map += "#####....................................................................................................................................................................................................................................................#######";



            nLevelWidth = 256;
            nLevelHeight = 25;

            layer1 = Resource1.fondo;
            layer2 = Resource1._6;
            layer3 = Resource1._7;


            l1_x1 = l2_x1 = l3_x1 = 0;
            l1_x2 = l2_x2 = l3_x2 = width;

            bmp = new Bitmap(size.Width / divs, size.Height / divs);

            g = Graphics.FromImage(bmp);
            g.PixelOffsetMode = PixelOffsetMode.HighSpeed;
            g.SmoothingMode = SmoothingMode.HighSpeed;

            
        }

        public void Draw(PointF cameraPos, string message, Player player)
        {
            // Draw Level based on the visible tiles on our picturebox (canvas)
            int nVisibleTilesX = bmp.Width / nTileWidth;
            int nVisibleTilesY = bmp.Height / nTileHeight;

            // Calculate Top-Leftmost visible tile
            float fOffsetX = cameraPos.X - (float)nVisibleTilesX / 2.0f;
            float fOffsetY = cameraPos.Y - (float)nVisibleTilesY / 2.0f;

            // Clamp camera to game boundaries
            if (fOffsetX < 0) fOffsetX = 0;
            if (fOffsetY < 0) fOffsetY = 0;
            if (fOffsetX > nLevelWidth - nVisibleTilesX) fOffsetX = nLevelWidth - nVisibleTilesX;
            if (fOffsetY > nLevelHeight - nVisibleTilesY) fOffsetY = nLevelHeight - nVisibleTilesY;

            float fTileOffsetX = (fOffsetX - (int)fOffsetX) * nTileWidth;
            float fTileOffsetY = (fOffsetY - (int)fOffsetY) * nTileHeight;
            
            GC.Collect();
            //Draw visible tile map//*--------------------DRAW------------------------------
            char c;
            float stepX, stepY;
            

            

            g.DrawImage(layer1, l1_x1, 0, width, height);
            g.DrawImage(layer1, l1_x2, 0, width, height);
            
            g.DrawImage(layer2, l2_x1, 0, width, height);
            g.DrawImage(layer2, l2_x2, 0, width, height);

            g.DrawImage(layer3, l3_x1, 0, width, height);
            g.DrawImage(layer3, l3_x2, 0, width, height);


            for (int x = -1; x < nVisibleTilesX + 2; x++)
            {
                for (int y = -1; y < nVisibleTilesY + 2; y++)
                {
                    c = GetTile(x + (int)fOffsetX, y + (int)fOffsetY);
                    stepX = x * nTileWidth - fTileOffsetX;
                    stepY = y * nTileHeight - fTileOffsetY;
                   

                    if (c == '1')
                        g.DrawImage(wood02, stepX, stepY, nTileWidth, nTileHeight);
                    if (c == '#')
                        g.DrawImage(cat, stepX, stepY, nTileWidth, nTileHeight);
                        
                    if (c == '$')
                        g.DrawImage(wood02, stepX, stepY, nTileWidth, nTileHeight);
                    if (c == '%')
                        g.DrawImage(pinkbush, stepX, stepY, nTileWidth, nTileHeight);
                    if (c == '&')
                        g.DrawImage(tree, stepX, stepY, nTileWidth, nTileHeight);
                    if (c == '/')
                        g.DrawImage(enemy, stepX, stepY , nTileWidth, nTileHeight);
                    if (c == '(')
                        g.DrawImage(rocks, stepX, stepY , nTileWidth, nTileHeight);
                    if (c == ')')
                        g.DrawImage(diamond, stepX, stepY, nTileWidth, nTileHeight);
                    if (c == '=')
                        g.DrawImage(water, stepX, stepY, nTileWidth, nTileHeight);
                    if (c == '-')
                        g.DrawImage(wood, stepX, stepY, nTileWidth, nTileHeight);
                    if (c == ',')
                        g.DrawImage(black, stepX, stepY, nTileWidth, nTileHeight);
                }
            }

            g.DrawString("SCORE: " + score, new Font("Consolas", 10, FontStyle.Bold), Brushes.Black, 5, 5);

            

            player.MainSprite.posX = (player.fPlayerPosX - fOffsetX) * nTileWidth;
            player.MainSprite.posY = (player.fPlayerPosY - fOffsetY) * nTileHeight;

          
        }

        public void SetTile(float x, float y, char c)//changes the tile
        {
            if (x >= 0 && x < nLevelWidth && y >= 0 && y < nLevelHeight)
            {
                int index = (int)y * nLevelWidth + (int)x;
                map = map.Remove(index, 1).Insert(index, c.ToString());
                Play();
                score += 100;

                if (score == 1000) 
                    {
                        
                    DialogResult result = MessageBox.Show("You won awesome!", "Victory", MessageBoxButtons.RetryCancel);
                    if (result == DialogResult.Retry)
                    {
                        Application.Restart();
                    }
                    else if (result == DialogResult.Cancel)
                    {
                        Application.Exit();
                    }
                    score = 0;
                } 
              
            }
        }

        public char GetTile(float x, float y)
        {
            if (x >= 0 && x < nLevelWidth && y >= 0 && y < nLevelHeight)
                return map[(int)y * nLevelWidth + (int)x];
            else
                return ' ';
        }

        public void Play()
        {
            if (isP1)
            {
                thread = new Thread(PlayThread);
                thread.Start();
            }
            threadStop = new Thread(PlayStop);
            threadStop.Start();
        }
        private void PlayThread()
        {
            isP1 = false;
            soundPlayer.PlaySync();
            isP1 = true;            
        }

        private void PlayStop()
        {
            soundPlayer.Stop();
        }

    }
}
