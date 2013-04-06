using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Capstone_Project
{
    class EnemyPathing:TileMap
    {
        //create a queue of pathing vectors to hardcode the enemy path
        Queue<Vector2> pathing = new Queue<Vector2>();

        //hardcodes the enemy path
        public EnemyPathing()
        {
            pathing.Enqueue(new Vector2(13, 0) * getTileSize);
            pathing.Enqueue(new Vector2(13, 1) * getTileSize);
            pathing.Enqueue(new Vector2(13, 2) * getTileSize);
            pathing.Enqueue(new Vector2(13, 3) * getTileSize);
            pathing.Enqueue(new Vector2(13, 4) * getTileSize);
            pathing.Enqueue(new Vector2(13, 5) * getTileSize);
            pathing.Enqueue(new Vector2(13, 6) * getTileSize);
            pathing.Enqueue(new Vector2(13, 7) * getTileSize);
            pathing.Enqueue(new Vector2(13, 8) * getTileSize);
            pathing.Enqueue(new Vector2(13, 9) * getTileSize);
            pathing.Enqueue(new Vector2(12, 9) * getTileSize);
            pathing.Enqueue(new Vector2(11, 9) * getTileSize);
            pathing.Enqueue(new Vector2(10, 9) * getTileSize);
            pathing.Enqueue(new Vector2(9, 9) * getTileSize);
            pathing.Enqueue(new Vector2(8, 9) * getTileSize);
            pathing.Enqueue(new Vector2(7, 9) * getTileSize);
            pathing.Enqueue(new Vector2(6, 9) * getTileSize);
            pathing.Enqueue(new Vector2(5, 9) * getTileSize);
            pathing.Enqueue(new Vector2(4, 9) * getTileSize);
            pathing.Enqueue(new Vector2(3, 9) * getTileSize);
            pathing.Enqueue(new Vector2(2, 9) * getTileSize);
            pathing.Enqueue(new Vector2(1, 9) * getTileSize);
            pathing.Enqueue(new Vector2(1, 8) * getTileSize);
            pathing.Enqueue(new Vector2(1, 7) * getTileSize);
            pathing.Enqueue(new Vector2(1, 6) * getTileSize);
            pathing.Enqueue(new Vector2(1, 5) * getTileSize);
            pathing.Enqueue(new Vector2(1, 4) * getTileSize);
            pathing.Enqueue(new Vector2(1, 3) * getTileSize);
            pathing.Enqueue(new Vector2(1, 2) * getTileSize);
            pathing.Enqueue(new Vector2(1, 1) * getTileSize);
            pathing.Enqueue(new Vector2(2, 1) * getTileSize);
            pathing.Enqueue(new Vector2(3, 1) * getTileSize);
            pathing.Enqueue(new Vector2(4, 1) * getTileSize);
            pathing.Enqueue(new Vector2(5, 1) * getTileSize);
            pathing.Enqueue(new Vector2(6, 1) * getTileSize);
            pathing.Enqueue(new Vector2(7, 1) * getTileSize);
            pathing.Enqueue(new Vector2(8, 1) * getTileSize);
            pathing.Enqueue(new Vector2(9, 1) * getTileSize);
            pathing.Enqueue(new Vector2(10, 1) * getTileSize);
            pathing.Enqueue(new Vector2(11, 1) * getTileSize);
            pathing.Enqueue(new Vector2(11, 2) * getTileSize);
            pathing.Enqueue(new Vector2(11, 3) * getTileSize);
            pathing.Enqueue(new Vector2(11, 4) * getTileSize);
            pathing.Enqueue(new Vector2(11, 5) * getTileSize);
            pathing.Enqueue(new Vector2(11, 6) * getTileSize);
            pathing.Enqueue(new Vector2(11, 7) * getTileSize);
            pathing.Enqueue(new Vector2(10, 7) * getTileSize);
            pathing.Enqueue(new Vector2(9, 7) * getTileSize);
            pathing.Enqueue(new Vector2(8, 7) * getTileSize);
            pathing.Enqueue(new Vector2(7, 7) * getTileSize);
            pathing.Enqueue(new Vector2(6, 7) * getTileSize);
            pathing.Enqueue(new Vector2(5, 7) * getTileSize);
            pathing.Enqueue(new Vector2(4, 7) * getTileSize);
            pathing.Enqueue(new Vector2(3, 7) * getTileSize);
            pathing.Enqueue(new Vector2(3, 6) * getTileSize);
            pathing.Enqueue(new Vector2(3, 5) * getTileSize);
            pathing.Enqueue(new Vector2(3, 4) * getTileSize);
            pathing.Enqueue(new Vector2(3, 3) * getTileSize);
            pathing.Enqueue(new Vector2(4, 3) * getTileSize);
            pathing.Enqueue(new Vector2(5, 3) * getTileSize);
            pathing.Enqueue(new Vector2(6, 3) * getTileSize);
            pathing.Enqueue(new Vector2(7, 3) * getTileSize);
            pathing.Enqueue(new Vector2(8, 3) * getTileSize);
            pathing.Enqueue(new Vector2(9, 3) * getTileSize);
            pathing.Enqueue(new Vector2(9, 4) * getTileSize);
            pathing.Enqueue(new Vector2(9, 5) * getTileSize);
            pathing.Enqueue(new Vector2(8, 5) * getTileSize);
            pathing.Enqueue(new Vector2(7, 5) * getTileSize);
            pathing.Enqueue(new Vector2(6, 5) * getTileSize);
            pathing.Enqueue(new Vector2(5, 5) * getTileSize);

        }

        //access the pathing
        public Queue<Vector2> Pathing
        {
            get { return pathing; }
        }



    }
}
