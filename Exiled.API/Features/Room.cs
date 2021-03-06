// -----------------------------------------------------------------------
// <copyright file="Room.cs" company="Exiled Team">
// Copyright (c) Exiled Team. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Exiled.API.Features
{
    using System.Collections.Generic;
    using System.Linq;

    using Exiled.API.Enums;

    using UnityEngine;

    /// <summary>
    /// The in-game room.
    /// </summary>
    public class Room
    {
        private readonly FlickerableLightController flickerableLightController;

        /// <summary>
        /// Initializes a new instance of the <see cref="Room"/> class.
        /// </summary>
        /// <param name="name">The room name.</param>
        /// <param name="transform">The room transform.</param>
        /// <param name="position">The room position.</param>
        public Room(string name, Transform transform, Vector3 position)
        {
            Name = name;
            Transform = transform;
            Position = position;
            Zone = FindZone();
            Type = FindType(name);
            Doors = FindDoors();
            flickerableLightController = transform.GetComponentInChildren<FlickerableLightController>();
        }

        /// <summary>
        /// Gets the <see cref="Room"/> name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the <see cref="Room"/> <see cref="UnityEngine.Transform"/>.
        /// </summary>
        public Transform Transform { get; }

        /// <summary>
        /// Gets the <see cref="Room"/> position.
        /// </summary>
        public Vector3 Position { get; }

        /// <summary>
        /// Gets the <see cref="ZoneType"/> in which the room is located.
        /// </summary>
        public ZoneType Zone { get; }

        /// <summary>
        /// Gets the <see cref="RoomType"/>.
        /// </summary>
        public RoomType Type { get; }

        /// <summary>
        /// Gets a <see cref="IEnumerable{T}"/> of <see cref="Player"/> in the <see cref="Room"/>.
        /// </summary>
        public IEnumerable<Player> Players => Player.List.Where(player => player.CurrentRoom.Transform == Transform);

        /// <summary>
        /// Gets a <see cref="IEnumerable{T}"/> of <see cref="Door"/> in the <see cref="Room"/>.
        /// </summary>
        public IEnumerable<Door> Doors { get; }

        /// <summary>
        /// Flickers the room's lights off for a duration.
        /// </summary>
        /// <param name="duration">Duration in seconds.</param>
        public void TurnOffLights(float duration) => flickerableLightController?.ServerFlickerLights(duration);

        private ZoneType FindZone()
        {
            if (Transform.parent == null)
                return ZoneType.Unspecified;

            switch (Transform.parent.name)
            {
                case "HeavyRooms":
                    return ZoneType.HeavyContainment;
                case "LightRooms":
                    return ZoneType.LightContainment;
                case "EntranceRooms":
                    return ZoneType.Entrance;
                default:
                    return Position.y > 900 ? ZoneType.Surface : ZoneType.Unspecified;
            }
        }

        private RoomType FindType(string rawName)
        {
            // Try to remove brackets if they exist.
            var bracketStart = rawName.IndexOf('(') - 1;
            if (bracketStart > 0)
                rawName = rawName.Remove(bracketStart, rawName.Length - bracketStart);

            switch (rawName)
            {
                case "LCZ_Armory":
                    return RoomType.LczArmory;
                case "LCZ_Curve":
                    return RoomType.LczCurve;
                case "LCZ_Straight":
                    return RoomType.LczStraight;
                case "LCZ_012":
                    return RoomType.Lcz012;
                case "LCZ_914":
                    return RoomType.Lcz914;
                case "LCZ_Crossing":
                    return RoomType.LczCrossing;
                case "LCZ_TCross":
                    return RoomType.LczTCross;
                case "LCZ_Cafe":
                    return RoomType.LczCafe;
                case "LCZ_Plants":
                    return RoomType.LczPlants;
                case "LCZ_Toilets":
                    return RoomType.LczToilets;
                case "LCZ_Airlock":
                    return RoomType.LczAirlock;
                case "LCZ_173":
                    return RoomType.Lcz173;
                case "LCZ_ClassDSpawn":
                    return RoomType.LczClassDSpawn;
                case "LCZ_ChkpB":
                    return RoomType.LczChkpB;
                case "LCZ_372":
                    return RoomType.LczGlassBox;
                case "LCZ_ChkpA":
                    return RoomType.LczChkpA;
                case "HCZ_079":
                    return RoomType.Hcz079;
                case "HCZ_EZ_Checkpoint":
                    return RoomType.HczEzCheckpoint;
                case "HCZ_Room3ar":
                    return RoomType.HczArmory;
                case "HCZ_Testroom":
                    return RoomType.Hcz939;
                case "HCZ_Hid":
                    return RoomType.HczHid;
                case "HCZ_049":
                    return RoomType.Hcz049;
                case "HCZ_ChkpA":
                    return RoomType.HczChkpA;
                case "HCZ_Crossing":
                    return RoomType.HczCrossing;
                case "HCZ_106":
                    return RoomType.Hcz106;
                case "HCZ_Nuke":
                    return RoomType.HczNuke;
                case "HCZ_Tesla":
                    return RoomType.HczTesla;
                case "HCZ_Servers":
                    return RoomType.HczServers;
                case "HCZ_ChkpB":
                    return RoomType.HczChkpB;
                case "HCZ_Room3":
                    return RoomType.HczTCross;
                case "HCZ_457":
                    return RoomType.Hcz096;
                case "HCZ_Curve":
                    return RoomType.HczCurve;
                case "EZ_Endoof":
                    return RoomType.EzVent;
                case "EZ_Intercom":
                    return RoomType.EzIntercom;
                case "EZ_GateA":
                    return RoomType.EzGateA;
                case "EZ_PCs_small":
                    return RoomType.EzDownstairsPcs;
                case "EZ_Curve":
                    return RoomType.EzCurve;
                case "EZ_PCs":
                    return RoomType.EzPcs;
                case "EZ_Crossing":
                    return RoomType.EzCrossing;
                case "EZ_CollapsedTunnel":
                    return RoomType.EzCollapsedTunnel;
                case "EZ_Smallrooms2":
                    return RoomType.EzConference;
                case "EZ_Straight":
                    return RoomType.EzStraight;
                case "EZ_Cafeteria":
                    return RoomType.EzCafeteria;
                case "EZ_upstairs":
                    return RoomType.EzUpstairsPcs;
                case "EZ_GateB":
                    return RoomType.EzGateB;
                case "EZ_Shelter":
                    return RoomType.EzShelter;
                case "Root_*&*Outside Cams":
                    return RoomType.Surface;
                default:
                    return RoomType.Unknown;
            }
        }

        private List<Door> FindDoors()
        {
            List<Door> list2 = new List<Door>();
            foreach (global::Scp079Interactable scp079Interactable2 in global::Interface079.singleton.allInteractables)
            {
                foreach (global::Scp079Interactable.ZoneAndRoom zoneAndRoom in scp079Interactable2.currentZonesAndRooms)
                {
                    if (zoneAndRoom.currentRoom == Name && zoneAndRoom.currentZone == Transform.parent.name)
                    {
                        if (scp079Interactable2.type == Scp079Interactable.InteractableType.Door)
                        {
                            Door door = scp079Interactable2.GetComponent<Door>();
                            if (!list2.Contains(door))
                            {
                                list2.Add(door);
                            }
                        }
                    }
                }
            }

            return list2;
        }
    }
}
