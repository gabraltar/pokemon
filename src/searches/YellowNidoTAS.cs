using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

public class YellowNidoTASState {

    public string Log;
    public RbyTile Tile;
    public int EdgeSet;
    public int WastedFrames;

    public Action BlockedActions;
    public byte HRandomAdd;
    public byte HRandomSub;
    public byte RDiv;

    public override int GetHashCode() {
        var hash = new HashCode();
        hash.Add(Tile.X);
        hash.Add(Tile.Y);
        hash.Add(EdgeSet);
        hash.Add(WastedFrames);
        hash.Add(BlockedActions);
        hash.Add(HRandomAdd);
        hash.Add(HRandomSub);
        hash.Add(RDiv);
        return hash.ToHashCode();
    }
}

// Code heavily plagiarized from: https://github.com/entrpntr/gb-rta-bruteforce/blob/master/src/dabomstew/rta/entei/GSToto.java
public static class YellowNidoTAS {

    const int MaxCost = 400;
    static StreamWriter Writer;
    public static HashSet<int> seenStates = new HashSet<int>();

    public static void OverworldSearch(Rby gb, YellowNidoTASState state) {
        if (!seenStates.Add(state.GetHashCode())) {
            return;
        }
        byte[] oldState = gb.SaveState();

        List<Edge<RbyTile>> edgeList = state.Tile.Edges[state.EdgeSet];
        foreach(Edge<RbyTile> edge in edgeList) {
            gb.LoadState(oldState);
            if (edge.Cost + state.WastedFrames > MaxCost) continue;

            int ret = gb.Execute(edge.Action);

                        Action blockedActions = state.BlockedActions;

            if ((edge.Action & Action.A) > 0)
                blockedActions |= Action.A;
            else
                blockedActions &= ~(Action.A);
            if (ret == gb.SYM["CalcStats"])
            {
                if (gb.CpuRead("wEnemyMonSpecies") == gb.Species["PONYTA"].Id) {
                    
                    if (gb.CpuRead("wEnemyMonLevel") == 32){
                        Console.WriteLine("Ponyta Encounter");
                    int dvs = gb.CpuRead("wEnemyMonDVs") << 8 | gb.CpuRead(gb.SYM["wEnemyMonDVs"] + 1);

                    int atk = (dvs >> 12) & 0xf;
                    int def = (dvs >> 8) & 0xf;
                    int spd = (dvs >> 4) & 0xf;
                    int spc = dvs & 0xf;

                    if (spd >= 9) {
                        lock (Writer) {
                            var foundNido = $"[{state.WastedFrames} cost] {state.Log}{edge.Action.LogString()} - 0x{dvs:x4}";
                            Writer.WriteLine(foundNido);
                            Writer.Flush();
                            Console.WriteLine(foundNido);
                        }
                    }
                    }
                }
                continue;
            }
             if ((state.BlockedActions & edge.Action) > 0) continue;
            OverworldSearch(gb, new YellowNidoTASState {
                Log = state.Log + edge.Action.LogString() + " ",
                Tile = edge.NextTile,
                EdgeSet = edge.NextEdgeset,
                BlockedActions = blockedActions,
                WastedFrames = state.WastedFrames + edge.Cost,
                HRandomAdd = gb.CpuRead("hRandomAdd"),
                HRandomSub = gb.CpuRead("hRandomSub"),
                RDiv = gb.CpuRead(0xFF04)
            });
            gb.LoadState(oldState);
        }
    }

    public static void StartSearch(int numThreads = 4) {
        Yellow dummyGb = new Yellow();
        RbyMap route16Map = dummyGb.Maps[27];
        RbyMap route17map = dummyGb.Maps[28];
        Pathfinding.GenerateEdges(route16Map, 0, 17, route16Map.Tileset.LandPermissions, Action.Left | Action.Down |Action.Delay | Action.A, route16Map[11, 17]);
        Pathfinding.GenerateEdges(route17map, 0, 17, route17map.Tileset.LandPermissions, Action.Right | Action.Down | Action.Left | Action.Delay | Action.A, route17map[17, 19]);
        RbyTile startTile = route16Map[17, 10];
        
        route16Map[17, 10].AddEdge(0, new Edge<RbyTile>(){Action = Action.Left, NextTile = route16Map[16, 10], NextEdgeset = 0, Cost = 0});
        route16Map[11, 17].AddEdge(0, new Edge<RbyTile>(){Action = Action.Down, NextTile = route17map[11, 0], NextEdgeset = 0, Cost = 0});
        for(int i = 0; i <= 4; i++){
            for(int j = 0; j <= 7; j++){
                route17map[13 + i, 8 + j].AddEdge(0, new Edge<RbyTile>(){Action = Action.Left, NextTile = route17map[12 + i, 8 + j], NextEdgeset = 0, Cost = 10});
            }
                        for(int j = 0; j <= 1; j++){
                route17map[13 + i, 17 + j].AddEdge(0, new Edge<RbyTile>(){Action = Action.Left, NextTile = route17map[12 + i, 17 + j], NextEdgeset = 0, Cost = 10});
            }
            
        }
        for(int i = 0; i <= 3; i++){
            route17map[14 + i, 19].AddEdge(0, new Edge<RbyTile>(){Action = Action.Left, NextTile = route17map[13 + i, 19], NextEdgeset = 0, Cost = 10});
        }
        for(int i = 0; i <= 5; i++){
            for(int j = 0; j <= 6; j++){
                 route17map[14 + i, 9 + j].AddEdge(0, new Edge<RbyTile>(){Action = Action.Up, NextTile = route17map[14 + i, 8 + j], NextEdgeset = 0, Cost = 10});
            }
        }
                    for(int j = 0; j <= 5; j++){
                 route17map[12 + j, 18].AddEdge(0, new Edge<RbyTile>(){Action = Action.Up, NextTile = route17map[12 + j, 17], NextEdgeset = 0, Cost = 10});
            }
                                for(int j = 0; j <= 4; j++){
                 route17map[13 + j, 19].AddEdge(0, new Edge<RbyTile>(){Action = Action.Up, NextTile = route17map[13 + j, 18], NextEdgeset = 0, Cost = 10});
            }
        Pathfinding.DebugDrawEdges(route17map, 0);
        Writer = new StreamWriter("yellow_nido_tas" + DateTime.Now.Ticks + ".txt");
        
        for (int threadIndex = 0; threadIndex < numThreads; threadIndex++) {
            new Thread(parameter => {
                int index = (int)parameter;
                Yellow gb = new Yellow();
                Console.WriteLine("starting movie");
                gb.LoadStateBiz("basesaves/Core.bin", 2);
                // gb.SetSpeedupFlags(SpeedupFlags.NoSound | SpeedupFlags.NoVideo);
                Console.WriteLine("finished movie");
                // gb.Show();
                gb.RunUntil("JoypadOverworld");
                for (int i = 0; i < index; i++) {
                    gb.AdvanceFrame();
                    gb.RunUntil("JoypadOverworld");
                }

                OverworldSearch(gb, new YellowNidoTASState {
                    Log = $"thread {index} ",
                    Tile = startTile,
                    WastedFrames = 0,
                    EdgeSet = 0,
                    BlockedActions = Action.A,
                    HRandomAdd = gb.CpuRead("hRandomAdd"),
                    HRandomSub = gb.CpuRead("hRandomSub"),
                    RDiv = gb.CpuRead(0xFF04)
                });
            }).Start(threadIndex);
        }
    }
}