using System.Collections.Generic;
using Vintagestory.API.Common;
using Vintagestory.API.Config;
using Vintagestory.API.Datastructures;
using Vintagestory.API.MathTools;
using Vintagestory.API.Server;

namespace VSModLauncher.Controllers
{
    public class PrisonController
    {
        private ICoreServerAPI sapi;

        public bool FreePlayer(string uid, ItemSlot shacklegear_slot, bool destroy = true)
        {

            sapi.Server.Logger.Debug("[SHACKLE-GEAR] Free Function Fired");
            foreach (var serverPlayer in sapi.Server.Players)
            {
                if (serverPlayer.PlayerUID == uid)
                {
                    //do the things required to free a player.
                    sapi.Permissions.SetRole(serverPlayer, "suplayer");
                    ITreeAttribute attribs = shacklegear_slot.Itemstack.Attributes;
                    serverPlayer.SpawnPosition.SetPos(GetSpawnFromAttributes(attribs));

                    serverPlayer.SendMessage(GlobalConstants.GeneralChatGroup, "You've been freed!", EnumChatType.Notification);
                    if (destroy) shacklegear_slot.Itemstack.Item.Durability = 0;
                    shacklegear_slot.MarkDirty();

                    return true;
                }
            }
            return false;
        }

        public void SetSpawnInAttributes(ITreeAttribute attribs, IServerPlayer player)
        {
            if (!attribs.HasAttribute("pearled_x"))
            {
                attribs.SetDouble("pearled_x", player.SpawnPosition.X);
                attribs.SetDouble("pearled_y", player.SpawnPosition.Y);
                attribs.SetDouble("pearled_z", player.SpawnPosition.Z);
            }
        }

        public Vec3d GetSpawnFromAttributes(ITreeAttribute attribs)
        {
            return new Vec3d(attribs.GetDouble("pearled_x", 0), attribs.GetDouble("pearled_y", 0), attribs.GetDouble("pearled_z", 0));
        }

        public void ImprisonPlayer(IServerPlayer player, ItemSlot shacklegear_slot)
        {
            //imprison some player
            ITreeAttribute attribs = shacklegear_slot.Itemstack.Attributes;
            sapi.Server.Logger.Debug("[SHACKLE-GEAR] Imprison Function Fired");
            sapi.Permissions.SetRole(player, "suvisitor");
            attribs.SetString("pearled_uid", player.PlayerUID);
            attribs.SetString("pearled_name", player.PlayerName);
            attribs.SetDouble("pearled_timestamp", sapi.World.Calendar.TotalHours);

            SetSpawnInAttributes(attribs, player);
            player.SpawnPosition.SetPos(player.Entity.ServerPos.XYZ);

            player.SendMessage(GlobalConstants.GeneralChatGroup, "You've been pearled!", EnumChatType.Notification);

            shacklegear_slot.MarkDirty();
        }

        public PrisonController(ICoreServerAPI _api)
        {
            sapi = _api;
        }
    }
}