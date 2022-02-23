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

    const int MaxCost = 600;
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

            if ((gb.Tile.X == 17) && (gb.Tile.Y == 26)){
                continue;
            }

            Console.WriteLine(ret);
            if (ret == 680393) //wild encounter
            {
                // Console.WriteLine("encounter");
                gb.Hold(Joypad.B, gb.SYM["CalcMonStats"]);
                if(gb.CpuRead("wEnemyMonSpecies") == gb.Species["YANMA"].Id){

                   
                         lock (Writer){
                        Console.WriteLine(state.Log);
                        Writer.WriteLine(state.Log);
                        Writer.Flush();
                         
                         }
                }
                else{
                    continue;
                }
                    

            }

            // if(ret == 12605){
            //     Console.WriteLine(state.Log);
            // }


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

    public static void StartSearch(int numThreads = 1)
    {
        Crystal dummyGb = new Crystal();

        GscMap icepathmap = dummyGb.Maps["IcePathB3F"];


        GscTile startTile = icepathmap[3, 4];


        // for(int i = 0; i <= 3; i++){
        //     icepathmap[2 + i, 4].AddEdge(0, new Edge<GscTile>(){Action = Action.Right, NextTile = icepathmap[3 + i, 4]});
        //     icepathmap[3 + i, 4].AddEdge(0, new Edge<GscTile>(){Action = Action.Left, NextTile = icepathmap[2 + i, 4]});
        // }
         Pathfinding.DebugDrawEdges(icepathmap, 0);
        dummyGb.Dispose();
        Writer = new StreamWriter("gold_rattata_tas" + DateTime.Now.Ticks + ".txt");

        for (int threadIndex = 0; threadIndex < numThreads; threadIndex++)
        {
            new Thread(parameter => {
                int index = (int)parameter;
                Crystal gb = new Crystal();
                gb.SetRTCOffset(-69);	
                Console.WriteLine("starting movie");

                gb.LoadStateBiz("basesaves/Core.bin", 0);
                Console.WriteLine("finished movie");
                gb.Show();
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