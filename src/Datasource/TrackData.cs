using System;
using Vintagestory.API.Common;
using Vintagestory.API.Server;

namespace VSModLauncher.Datasource
{
    public class TrackData
    {
        public ItemStack Item;
        public IServerPlayer Prisoner;
        public IServerPlayer LastHolder;
        public int Last_x;
        public int Last_y;
        public int Last_z;
        
        public void SetLocation(int x, int y, int z)
        {
            this.Last_x = x;
            this.Last_y = y;
            this.Last_z = z;
        }

        public TrackData(ItemStack _item, IServerPlayer _prisoner,IServerPlayer _lastHeldBy)
        {
            this.Item = _item;
            this.Prisoner = _prisoner;
            this.LastHolder = _lastHeldBy;
        }
    }
}