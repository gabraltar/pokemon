using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

public class YellowGlitchlessTASPidgeottoState {

    public string Log;
    public RbyTile Tile;
    public int EdgeSet;
    public int WastedFrames;
    public byte HRandomAdd;
    public byte HRandomSub;
    public byte RDiv;

    public Action BlockedActions;

    public override int GetHashCode() {
        var hash = new HashCode();
        hash.Add(Tile.X);
        hash.Add(Tile.Y);
        hash.Add(EdgeSet);
        hash.Add(WastedFrames);
        hash.Add(HRandomAdd);
        hash.Add(HRandomSub);
        hash.Add(RDiv);
        hash.Add(BlockedActions);
        return hash.ToHashCode();
    }
}

// Code heavily plagiarized from: https://github.com/entrpntr/gb-rta-bruteforce/blob/master/src/dabomstew/rta/entei/GSToto.java
public static class YellowGlitchlessTASPidgeotto {

    const int MaxCost = 10;
    static StreamWriter Writer;
    public static HashSet<int> seenStates = new HashSet<int>();

    public static void OverworldSearch(Rby gb, YellowGlitchlessTASPidgeottoState state) {
        if (!seenStates.Add(state.GetHashCode())) {
            return;
        }
        byte[] oldState = gb.SaveState();

        foreach(Edge<RbyTile> edge in state.Tile.Edges[state.EdgeSet]) {
            gb.LoadState(oldState);
            if (edge.Cost + state.WastedFrames > MaxCost) continue;
            if ((state.BlockedActions & edge.Action) > 0) continue;

            int ret = gb.Execute(edge.Action);
            if (ret == gb.SYM["CollisionCheckOnLand.collision"] || ret == gb.SYM["CollisionCheckOnWater.collision"]) {
                continue;
            }
            
            if ((ret == gb.SYM["CalcStats"]))
            {
                
                //                  if (gb.CpuRead("wEnemyMonSpecies") == gb.Species["RATTATA"].Id) {
                //      Console.WriteLine("Rattata Encounter");
                //  }
                //                                   if (gb.CpuRead("wEnemyMonSpecies") == gb.Species["PIDGEY"].Id) {
                //      Console.WriteLine("Pidgey Encounter");
                //  }
                 if (gb.CpuRead("wEnemyMonSpecies") == gb.Species["CATERPIE"].Id) {
                     Console.WriteLine("Caterpie Encounter");
                 }
                if (gb.CpuRead("wEnemyMonSpecies") == gb.Species["PIDGEOTTO"].Id) {
                    Console.WriteLine("Pidgeotto Encounter");
                    int dvs = gb.CpuRead("wEnemyMonDVs") << 8 | gb.CpuRead(gb.SYM["wEnemyMonDVs"] + 1);

                    int atk = (dvs >> 12) & 0xf;
                    int def = (dvs >> 8) & 0xf;
                    int spd = (dvs >> 4) & 0xf;
                    int spc = dvs & 0xf;

                    if((atk == 15) && (def < 7) && (spd > 6)){
                        lock (Writer) {
                            var foundPidgeotto = $"[{state.WastedFrames} cost] {state.Log}{edge.Action.LogString()} - 0x{dvs:x4}";
                            Writer.WriteLine(foundPidgeotto);
                            Writer.Flush();
                            Console.WriteLine(foundPidgeotto);
                        }
                    }
                    
                }
                continue;
            }
                        Action blockedActions = state.BlockedActions;

            if ((edge.Action & Action.A) > 0)
                blockedActions |= Action.A;
            else
                blockedActions &= ~(Action.A);
            OverworldSearch(gb, new YellowGlitchlessTASPidgeottoState {
                Log = state.Log + edge.Action.LogString() + " ",
                Tile = edge.NextTile,
                EdgeSet = edge.NextEdgeset,
                WastedFrames = state.WastedFrames + edge.Cost,
                HRandomAdd = gb.CpuRead("hRandomAdd"),
                HRandomSub = gb.CpuRead("hRandomSub"),
                RDiv = gb.CpuRead(0xFF04),
                BlockedActions = blockedActions,
            });
            gb.LoadState(oldState);
        }
    }

    public static void StartSearch(int numThreads = 5) {
        Yellow dummyGb = new Yellow();
        RbyMap viridianCityMap = dummyGb.Maps[1];
        RbyMap route2map = dummyGb.Maps[13];
        RbyMap gatehouseMap = dummyGb.Maps[50];
        RbyMap viridianForestMap = dummyGb.Maps[51];
        Pathfinding.GenerateEdges(viridianCityMap, 0, 17, viridianCityMap.Tileset.LandPermissions, Action.Right | Action.Down | Action.Up | Action.Left | Action.Delay | Action.A, viridianForestMap[8, 46]);
        Pathfinding.GenerateEdges(route2map, 0, 17, route2map.Tileset.LandPermissions, Action.Up | Action.Left | Action.A, route2map[3, 44]);
        Pathfinding.GenerateEdges(gatehouseMap, 0, 17, gatehouseMap.Tileset.LandPermissions, Action.Right | Action.Up | Action.A, gatehouseMap[5, 0]);
        Pathfinding.GenerateEdges(viridianForestMap, 0, 17, viridianForestMap.Tileset.LandPermissions, Action.Right | Action.Down | Action.Up | Action.Left | Action.A, viridianForestMap[11, 16]);
        RbyTile startTile = route2map[8, 46];
        viridianCityMap[18, 0].AddEdge(0, new Edge<RbyTile>(){Action = Action.Up, NextTile = route2map[8, 71], NextEdgeset = 0, Cost = 0 });
        viridianCityMap[17, 0].AddEdge(0, new Edge<RbyTile>(){Action = Action.Up, NextTile = route2map[7, 71], NextEdgeset = 0, Cost = 0 });
        route2map[3, 44].AddEdge(0, new Edge<RbyTile>() { Action = Action.Up, NextTile = gatehouseMap[4, 7], NextEdgeset = 0, Cost = 0 });
        gatehouseMap[4, 7].AddEdge(0, new Edge<RbyTile>(){Action = Action.Up, NextTile = gatehouseMap[4, 6], NextEdgeset = 0, Cost = 0 });
        gatehouseMap[5, 1].AddEdge(0, new Edge<RbyTile>() { Action = Action.Up, NextTile = viridianForestMap[16, 47], NextEdgeset = 0, Cost = 0 });
        viridianForestMap[16, 47].AddEdge(0, new Edge<RbyTile>(){Action = Action.Up, NextTile = viridianForestMap[16, 46], NextEdgeset = 0, Cost = 0 });
        viridianForestMap[25, 12].RemoveEdge(0, Action.A);
        viridianForestMap[25, 12].RemoveEdge(0, Action.Up);
        viridianForestMap[26, 12].RemoveEdge(0, Action.Left);
        viridianForestMap[26, 11].RemoveEdge(0, Action.Left);
        viridianForestMap[25,12].GetEdge(0, Action.Right).Cost = 0;
        Writer = new StreamWriter("Pidgeottos" + DateTime.Now.Ticks + ".txt");
        
        for (int threadIndex = 0; threadIndex < numThreads; threadIndex++) {
            new Thread(parameter => {
                int index = (int)parameter;
                Yellow gb = new Yellow();
                Console.WriteLine("starting movie");
                gb.PlayBizhawkInputLogELF3xLinked("movies/TiKevin83YellowGlitchless2021Pidgeotto.txt");
                Console.WriteLine("finished movie");
                for (int i = 0; i < index; i++) {
                    gb.AdvanceFrame();
                    gb.RunUntil("JoypadOverworld");
                }
                Console.WriteLine(gb.Tile.X);
                Console.WriteLine(gb.Tile.Y);
                Console.WriteLine(gb.Map);
                gb.SetSpeedupFlags(SpeedupFlags.NoSound | SpeedupFlags.NoVideo);

                OverworldSearch(gb, new YellowGlitchlessTASPidgeottoState {
                    Log = $"thread {index} ",
                    Tile = startTile,
                    WastedFrames = 0,
                    EdgeSet = 0,
                    HRandomAdd = gb.CpuRead("hRandomAdd"),
                    HRandomSub = gb.CpuRead("hRandomSub"),
                    RDiv = gb.CpuRead(0xFF04),
                });
            }).Start(threadIndex);
        }
    }
}