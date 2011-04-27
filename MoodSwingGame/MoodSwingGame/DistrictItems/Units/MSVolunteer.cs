using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using MoodSwingCoreComponents;
using MoodSwingGUI;

namespace MoodSwingGame
{
    public class MSVolunteer : MSUnit
    {

        private double rand = MSRandom.random.NextDouble();
        protected override Model Model
        {
            get
            {
                if (rand >= 0.5)
                    return Game.Content.Load<Model>("TaoBabae");
                else
                    return Game.Content.Load<Model>("TaoLalaki");
            }
        }
        protected override Texture2D Texture
        {
            get
            {
                if (rand >= 0.5)
                    return Game.Content.Load<Texture2D>("TaoBabaeVolunteer");
                else
                    return Game.Content.Load<Texture2D>("TaoLalakiVolunteer");
            }
        }

        public override float Speed 
        { 
            get { return 0.82f; }
            set { throw new NotImplementedException(); } 
        }

        private MSUnit target;
        private MSTower office;
        private bool isGoingToMobber;
        
        public MSVolunteer(Vector3 position, Node path_to_mobber, MSUnit target, MSTower office, MSMap map, float initRotation)
            : base(position, path_to_mobber, map, false, initRotation)
        {
            this.target = target;
            this.office = office;
            this.isGoingToMobber = true;
        }

        private int minChaseDistance = 10;
        public override void Walk(MS3DTile[,] map_array, List<MSUnit> units )
        {
 	        base.Walk(map_array, units);
            
            if (isGoingToMobber && 
                ( target.TileCoordinate == MSDistrictHall.getInstance().TileCoordinate|| target.DestinationReached) )
            {
                this.path = map.GetPath(this.TileCoordinate, office.TileCoordinate);
                this.destination = Vector2.Zero;
                isGoingToMobber = false;
            }
            else
            {
                if ( isGoingToMobber && this.TileCoordinate == target.TileCoordinate)
                {
                    destination = new Vector2(target.Position.X, target.Position.Y);
                    targetRotation = (float)Math.Atan2(destination.Y - position.Y, destination.X - position.X);
                }
                if ( isGoingToMobber && Vector3.Distance(position, target.Position) <= minChaseDistance)
                {
                    isGoingToMobber = false;
                    this.path = map.GetPath(this.TileCoordinate, office.TileCoordinate);
                    this.destination = Vector2.Zero;
                    for (int i = 0; i < MSUnitHandler.GetInstance().Units.Count; i++)
                    {
                        MSUnit unit =  MSUnitHandler.GetInstance().Units[i];
                        if (unit == target)
                        {
                            MSUnitHandler.GetInstance().Units[i] = new MSCitizen
                            (
                                unit.Position,
                                path,
                                unit.Map,
                                false,
                                unit.Rotation
                            );
                            MSUnitHandler.GetInstance().Units[i].Speed = this.Speed;
                            office.remove(unit);
                            break;
                        }
                    }

                }
                else if (DestinationReached && !isGoingToMobber)
                {
                    office.VolunteerReturned();

                }
            }

        }
    }
}
