using System;
using System.Linq;
using System.Collections.Generic;
using Sandbox;

using TTTReborn.Items;
using TTTReborn.Roles;

namespace TTTReborn.Player
{
    partial class TTTPlayer
    {
        [ServerCmd(Name = "respawn", Help = "Respawns the current player")]
        public static void RespawnPlayer()
        {
            (ConsoleSystem.Caller.Pawn as TTTPlayer).Respawn();

            Log.Info($"You respawned yourself.");

            return;
        }

        [ServerCmd(Name = "respawnid", Help = "Respawns the player with the associated ID")]
        public static void RespawnPlayer(int id)
        {
            List<Client> playerList = Client.All.ToList();

            for (int i = 0; i < playerList.ToList().Count; i++)
            {
                if (i == id)
                {
                    if (playerList[i].Pawn is TTTPlayer player)
                    {
                        player.Respawn();

                        Log.Info($"You've respawned the client '{playerList[i].Name}'.");
                    }

                    return;
                }
            }
        }

        [ServerCmd(Name = "requestitem")]
        public static void RequestItem(string itemName)
        {
            IBuyableItem item = null;

            Library.GetAll<IBuyableItem>().ToList().ForEach(t =>
            {
                if (!t.IsAbstract && !t.ContainsGenericParameters)
                {
                    if (Library.GetAttribute(t).Name == itemName)
                    {
                        item = Library.Create<IBuyableItem>(t);
                    }
                }
            });

            if (item == null)
            {
                return;
            }

            (ConsoleSystem.Caller.Pawn as TTTPlayer).RequestPurchase(item);
        }

        [ServerCmd(Name = "setrole")]
        public static void SetRole(string roleName)
        {
            Type type = RoleFunctions.GetRoleTypeByName(roleName);

            if (type == null)
            {
                Log.Info($"{ConsoleSystem.Caller.Name} entered a wrong role name: '{roleName}'.");

                return;
            }

            TTTRole role = RoleFunctions.GetRoleByType(type);

            if (role == null)
            {
                return;
            }

            TTTPlayer player = ConsoleSystem.Caller.Pawn as TTTPlayer;

            player.SetRole(role);
            player.ClientSetRole(To.Single(player), role.Name);
        }

        [ClientCmd(Name = "playerids", Help = "Returns a list of all players (clients) and their associated IDs")]
        public static void PlayerID()
        {
            List<Client> playerList = Client.All.ToList();

            for (int i = 0; i < playerList.ToList().Count; i++)
            {
                Log.Info($"Player (ID: '{i}'): {playerList[i].Name}");
            }
        }
    }
}
