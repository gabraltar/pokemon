using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

public class GoldRattataTASState
{

    public string Log;
    public GscTile Tile;
    public int EdgeSet;
    public int WastedFrames;
    public Action BlockedActions;
    public byte HRandomAdd;
    public byte HRandomSub;
    public byte RDiv;

    public override int GetHashCode()
    {

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
public static class GoldRattataTAS
{

    const int MaxCost = 1000;
    static StreamWriter Writer;
    public static HashSet<int> seenStates = new HashSet<int>();

    public static void OverworldSearch(Gsc gb, GoldRattataTASState state)
    {
        if (!seenStates.Add(state.GetHashCode()))
        {
            return;
        }
        byte[] oldState = gb.SaveState();
            
        foreach (Edge<GscTile> edge in state.Tile.Edges[state.EdgeSet])
        {
            gb.LoadState(oldState);

            if (edge.Cost + state.WastedFrames > MaxCost) continue;

            if ((state.BlockedActions & edge.Action) > 0) continue;

            int ret = gb.Execute(edge.Action);
            if (ret == gb.SYM["DoPlayerMovement.BumpSound"])
            {
                continue;
            }


     

            if (ret == 681765) //wild encounter
            {
                // Console.WriteLine("encounter");
                
                gb.Hold(Joypad.B, gb.SYM["CalcMonStats"]);
                
                if(gb.CpuRead("wEnemyMonSpecies") == gb.Species["MARILL"].Id){
                    // gb.Hold(Joypad.B, gb.SYM["GetOpponentItem"]);

                    //int hp = (((dvs >> 9) & 8) | ((dvs >> 6) & 4) | ((dvs >> 3) & 2) | (dvs & 1)) & 0xf;

                     var foundSeadra = $"[{state.WastedFrames} cost] {state.Log}{edge.Action.LogString()}";
                       lock (Writer){
                           Writer.WriteLine(foundSeadra);
                           Writer.Flush();
Console.WriteLine(foundSeadra);
                       }
                    
                    continue;

                }
                else{
                    continue;
                }
            }



            if(ret == 12605){
                Console.WriteLine(state.Log);
            }


            Action blockedActions = state.BlockedActions;

            if ((edge.Action & Action.A) > 0)
                blockedActions |= Action.A;
            else
                blockedActions &= ~(Action.A);

            OverworldSearch(gb, new GoldRattataTASState
            {
                Log = state.Log + edge.Action.LogString() + " ",
                Tile = edge.NextTile,
                EdgeSet = edge.NextEdgeset,
                WastedFrames = state.WastedFrames + edge.Cost,
                BlockedActions = blockedActions,
                HRandomAdd = gb.CpuRead("hRandomAdd"),
                HRandomSub = gb.CpuRead("hRandomSub"),
                RDiv = gb.CpuRead(0xFF04)
            });
            gb.LoadState(oldState);
        }
    }

    public static async void StartSearch(int numThreads = 6)
    {
        Silver dummyGb = new Silver();

        GscMap mtMortar = dummyGb.Maps["MountMortar1FInside"];


        GscTile startTile = mtMortar[17, 33];

        for(int i = 0; i < 3; i++){
            for(int j = 0; j < 2; j++){
                 mtMortar[16 + i, 32 + j].AddEdge(0, new Edge<GscTile>(){Action = Action.Right, NextTile = mtMortar[17 + i, 32 + j], NextEdgeset = 0, Cost = 10 });
                 mtMortar[17 + i, 32 + j].AddEdge(0, new Edge<GscTile>(){Action = Action.Left, NextTile = mtMortar[16 + i, 32 + j], NextEdgeset = 0, Cost = 10 }); 
            }
            
        }
       for(int i = 0; i < 4; i++){
           mtMortar[16 + i, 33].AddEdge(0, new Edge<GscTile>(){Action = Action.Up, NextTile = mtMortar[16 + i, 32], NextEdgeset = 0, Cost = 10 });
           mtMortar[16 + i, 32].AddEdge(0, new Edge<GscTile>(){Action = Action.Down, NextTile = mtMortar[16 + i, 33], NextEdgeset = 0, Cost = 10 });
       }


        
Pathfinding.DebugDrawEdges(mtMortar, 0);
       
        dummyGb.Dispose();
        Writer = new StreamWriter("gold_rattata_tas" + DateTime.Now.Ticks + ".txt");

        for (int threadIndex = 0; threadIndex < numThreads; threadIndex++)
        {
            new Thread(parameter => {
                int index = (int)parameter;
                Silver gb = new Silver();
                gb.SetRTCOffset(-69);	
                Console.WriteLine("starting movie");

                gb.LoadStateBiz("basesaves/Core.bin", 1);
                Console.WriteLine("finished movie");
                gb.Hold(Joypad.B, "OWPlayerInput");
                /*for (int i = 0; i < index; i++)
                {
                    gb.AdvanceFrame();
                    gb.Hold(Joypad.B, "OWPlayerInput");
                }*/
                //gb.Record("test");
                /*var startrng = (gb.CpuRead("hRandomAdd") << 8) | gb.CpuRead("hRandomSub");
                var startrdiv = gb.CpuRead(0xFF04);
                var startrngframe = gb.CpuRead("hVBlankCounter");
                Console.WriteLine($"0x{startrng:x4}");
                Console.WriteLine($"0x{startrdiv:x2}");
                Console.WriteLine($"0x{startrngframe:x2}");*/


                OverworldSearch(gb, new GoldRattataTASState
                {
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