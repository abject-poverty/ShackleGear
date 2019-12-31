using Vintagestory.API.Common;
using Vintagestory.API.Config;
using Vintagestory.API.Server;

namespace VSModLauncher.Commands {
    public class ppDebug {
        public void Handler(IServerPlayer player, int groupid, CmdArgs args) {
            if (player.InventoryManager.ActiveHotbarSlot.Itemstack.GetName() == "Shackle-Gear")
            {
                player.SendMessage(
                    GlobalConstants.GeneralChatGroup,
                    "Current pearled uid = " + 
                    player.InventoryManager.ActiveHotbarSlot.Itemstack.Attributes.GetString("pearled_uid") + 
                    "\nCurrently pearled playername = " +
                    player.InventoryManager.ActiveHotbarSlot.Itemstack.Attributes.GetString("pearled_name"),
                    EnumChatType.Notification);
            }
        }
    }
}