// -----------------------------------------------------------------------
// <copyright file="Left.cs" company="Exiled Team">
// Copyright (c) Exiled Team. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Exiled.Events.Patches.Events.Player
{
#pragma warning disable SA1313
    using System;

    using Exiled.API.Features;
    using Exiled.Events.EventArgs;

    using HarmonyLib;

    using Mirror;

    /// <summary>
    /// Patches <see cref="CustomNetworkManager.OnServerDisconnect(Mirror.NetworkConnection)"/>.
    /// Adds the <see cref="Handlers.Player.Left"/> event.
    /// </summary>
    [HarmonyPatch(typeof(CustomNetworkManager), nameof(CustomNetworkManager.OnServerDisconnect), new[] { typeof(NetworkConnection) })]
    internal static class Left
    {
        private static void Prefix(NetworkConnection conn)
        {
            try
            {
                // The game checks for null NetworkIdentity, do the same
                Player player = Player.Get(conn.identity?.gameObject);

                if (player == null || player.IsHost)
                    return;

                var ev = new LeftEventArgs(player);

                Log.SendRaw($"Player {ev.Player.Nickname} ({ev.Player.UserId}) ({player?.Id}) disconnected", ConsoleColor.Green);

                Handlers.Player.OnLeft(ev);

                Player.IdsCache.Remove(player.Id);
                Player.UserIdsCache.Remove(player.UserId);
                Player.Dictionary.Remove(player.GameObject);
            }
            catch (Exception exception)
            {
                Log.Error($"Exiled.Events.Patches.Events.Player.Left: {exception}\n{exception.StackTrace}");
            }
        }
    }
}
