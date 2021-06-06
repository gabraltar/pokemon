public class BlueTASSquirTauros : RedBlueForce {

    // TODO:
    //  - TAS menu execution
    //  - TAS instant text execution (this is challenging)
    //  - Better NPC support (being able to specify how they should move)
    //  - Automatic fly menus
    //  - Better pathfinding
    //    > Make pathfinding consider turn frames (last moon room/post underground elixer house)

    public BlueTASSquirTauros() : base("roms/pokeblue.gbc", true) {
        // NOTE: Record requires ffmpeg.exe to be in PATH, it will output to movies/video.mp4, movies/audio.mp3, stitch the two together and save to movies/blue-tas.mp4
        
        Show();

        //Note: you may start after the playback of an existing bk2:
        /*
            Parameter #1: file path to the bk2 file
            Parameter #2: frame count to playback up to [Optional, if omitted the entire bk2 will be played back]
        */
        PlayBizhawkMovie("bizhawk/blue_321313.bk2", 47349);

        MoveTo(61,27,6);
        MoveTo(61,27,5);
        ForceEncounter(Action.Right, 6, 0xf6ef);
        ForceYoloball();
        ClearText();
        No();
        PickupItem();
        MoveTo(60,17,11);
        UseItem("TM01", "SQUIRTLE", "TAIL WHIP");

        // MOON ROCKET
        TalkTo("MtMoonB2F", 11, 16);
        ForceTurn(new RbyTurn("MEGA PUNCH", Crit), new RbyTurn("TACKLE", 7));
        ForceTurn(new RbyTurn("MEGA PUNCH", Crit));
        Evolve();
        
        // SUPER NERD
        MoveTo(13, 8);
        ClearText();
        ForceTurn(new RbyTurn("BUBBLE"), new RbyTurn("DISABLE", Miss));
        ForceTurn(new RbyTurn("WATER GUN", Crit));
        ForceTurn(new RbyTurn("MEGA PUNCH", Crit), new RbyTurn("SCREECH", Miss));
        ForceTurn(new RbyTurn("WATER GUN", Crit), new RbyTurn("SMOG", Miss));
        ForceTurn(new RbyTurn("BUBBLE"));

        TalkTo(13, 6);
        Yes();
        ClearText(); // helix fossil picked up

        Record("blue-tas");

        TalkTo("BikeShop", 6, 3);
        No();
        ClearText(); // got instant text

        // MISTY MINION
        MoveTo("CeruleanGym", 5, 3);
        ClearText();
        ForceTurn(new RbyTurn("MEGA PUNCH", Crit), new RbyTurn("PECK"));
        ForceTurn(new RbyTurn("MEGA PUNCH", Crit));

        // MISTY
        TalkTo(4, 2);
        ForceTurn(new RbyTurn("MEGA PUNCH"), new RbyTurn("TACKLE", Miss));
        ForceTurn(new RbyTurn("MEGA PUNCH"), new RbyTurn("TACKLE", Miss));
        ForceTurn(new RbyTurn("MEGA PUNCH", Crit), new RbyTurn("TACKLE", Miss));
        ForceTurn(new RbyTurn("MEGA PUNCH", Crit), new RbyTurn("TACKLE", Miss));
        ForceTurn(new RbyTurn("MEGA PUNCH", Crit), new RbyTurn("TACKLE", Miss));

        TalkTo("CeruleanPokecenter", 3, 2);
        Yes();
        ClearText(); // healed at center
        ClearText(); // healed at center
        UseItem("TM11", "WARTORTLE", "WATER GUN");

        TalkTo("BikeShop", 6, 3);
        No();
        ClearText(); // got instant text

        // RIVAL 2
        MoveTo("CeruleanCity", 21, 6, Action.Up);
        ClearText();
        ClearText(); // sneaky joypad call
        ForceTurn(new RbyTurn("BUBBLE"), new RbyTurn("GUST", Crit));
        ForceTurn(new RbyTurn("BUBBLEBEAM", Crit), new RbyTurn("QUICK ATTACK", Crit));
        ForceTurn(new RbyTurn("MEGA PUNCH"));
        ForceTurn(new RbyTurn("BUBBLEBEAM", Crit));
        ForceTurn(new RbyTurn("HORN ATTACK", Crit));

        // NUGGET BRIDGE #1
        TalkTo("Route24", 11, 31);
        ForceTurn(new RbyTurn("BUBBLEBEAM"));
        ForceTurn(new RbyTurn("BUBBLEBEAM"));

        // NUGGET BRIDGE #2
        TalkTo(10, 28);
        ForceTurn(new RbyTurn("BUBBLEBEAM", Crit));
        ForceTurn(new RbyTurn("BUBBLEBEAM", Crit));

        // NUGGET BRIDGE #3
        TalkTo(11, 25);
        ForceTurn(new RbyTurn("BUBBLEBEAM"));
        ForceTurn(new RbyTurn("BUBBLEBEAM", Crit));
        ForceTurn(new RbyTurn("BUBBLEBEAM", Crit));

        // NUGGET BRIDGE #4
        TalkTo(10, 22);
        ForceTurn(new RbyTurn("BUBBLEBEAM", Crit));
        ForceTurn(new RbyTurn("BUBBLEBEAM", Crit));

        // NUGGET BRIDGE #5
        TalkTo(11, 19);
        ForceTurn(new RbyTurn("BUBBLEBEAM", Crit));

        // NUGGET BRIDGE #5
        MoveTo(10, 15);
        ClearText();
        ForceTurn(new RbyTurn("BUBBLEBEAM", Crit));
        ForceTurn(new RbyTurn("BUBBLEBEAM", Crit));

        // HIKER
        MoveTo("Route25", 14, 7);
        ClearText();
        ForceTurn(new RbyTurn("BUBBLEBEAM"));

        // LASS
        TalkTo(18, 8, Action.Down);
        ForceTurn(new RbyTurn("BUBBLEBEAM", Crit));
        ForceTurn(new RbyTurn("BUBBLEBEAM", Crit));

        // JR TRAINER
        MoveTo(24, 6);
        ClearText();
        ForceTurn(new RbyTurn("BUBBLEBEAM"));
        TeachLevelUpMove("POISON STING");
        ForceTurn(new RbyTurn("BUBBLEBEAM"));

        // ODDISH GIRL
        TalkTo(37, 4);
        MoveSwap("BUBBLEBEAM", "THRASH");
        ForceTurn(new RbyTurn("THRASH"));
        ForceTurn(new RbyTurn("THRASH"));
        ForceTurn(new RbyTurn("THRASH"));

        TalkTo("BillsHouse", 6, 5, Action.Right);
        Yes();
        ClearText();
        TalkTo(1, 4);
        TalkTo(4, 4);
        UseItem("ESCAPE ROPE"); // escape rope out of bill's house

        TalkTo("BikeShop", 6, 3);
        No();
        ClearText(); // got instant text

        // DIG ROCKET
        MoveTo("CeruleanCity", 30, 9);
        ClearText();
        ForceTurn(new RbyTurn("THRASH"));
        ForceTurn(new RbyTurn("THRASH"));

        PickupItemAt("UndergroundPathNorthSouth", 3, 4); // full restore

        // ROUTE 6 #1
        TalkTo("Route6", 11, 30, Action.Down);
        ForceTurn(new RbyTurn("THRASH"));
        ForceTurn(new RbyTurn("THRASH"));
        ForceTurn(new RbyTurn("THRASH"));

        // ROUTE 6 #2
        MoveTo(10, 31);
        ClearText();
        ForceTurn(new RbyTurn("THRASH"));
        ForceTurn(new RbyTurn("THRASH"));

        MoveTo("VermilionCity", 18, 30);
        ClearText();

        // RIVAL 3
        MoveTo("SSAnne2F", 37, 8, Action.Up);
        ClearText();
        ForceTurn(new RbyTurn("THRASH", Crit), new RbyTurn("QUICK ATTACK", Crit | 20));
        ForceTurn(new RbyTurn("THRASH"));
        ForceTurn(new RbyTurn("THRASH"));
        ForceTurn(new RbyTurn("THRASH", Crit));

        TalkTo("SSAnneCaptainsRoom", 4, 2); // hm02 received

        MoveTo("VermilionDock", 14, 2);
        ClearText();
        ClearText(); // watch cutscene

        MoveTo("VermilionCity", 15, 17, Action.Down);
        UseItem("HM01", "SANDSHREW");
        Cut();

        MoveTo("VermilionGym", 4, 9);
        Press(Joypad.Left);
        ForceCan();
        Press(Joypad.Right);
        ForceCan();

        // SURGE
        TalkTo(5, 1);
        ForceTurn(new RbyTurn("THRASH"));
        ForceTurn(new RbyTurn("THRASH"));
        ForceTurn(new RbyTurn("THRASH", Crit));

        CutAt("VermilionCity", 15, 18);
        TalkTo("PokemonFanClub", 3, 1);
        Yes();
        ClearText();
        UseItem("ESCAPE ROPE"); // Escape rope to cerulean

        TalkTo("BikeShop", 6, 3);

        MoveTo("CeruleanCity", 13, 26);
        ItemSwap("POKE BALL", "BICYCLE");
        UseItem("TM24", "NIDOKING", "HORN ATTACK");
        UseItem("BICYCLE");

        /*
            Works like TalkTo, but uses Cut.
        */
        CutAt(19, 28);
        CutAt("Route9", 5, 8);

        // 4 TURN THRASH
        TalkTo(13, 10);
        ForceTurn(new RbyTurn("THRASH"));
        ForceTurn(new RbyTurn("THRASH"));
        ForceTurn(new RbyTurn("THRASH"));
        ForceTurn(new RbyTurn("THRASH"));

        // BUG CATCHER
        TalkTo(40, 8);
        ForceTurn(new RbyTurn("THRASH"));
        ForceTurn(new RbyTurn("THRASH"));
        ForceTurn(new RbyTurn("THRASH"));

        // POKEMANIAC #1
        TalkTo("RockTunnel1F", 23, 8);
        ForceTurn(new RbyTurn("BUBBLEBEAM"));
        ForceTurn(new RbyTurn("THUNDERBOLT"));

        // POKEMANIAC #2
        TalkTo("RockTunnelB1F", 26, 30);
        ForceTurn(new RbyTurn("THUNDERBOLT"));

        // ODDISH GIRL
        TalkTo(14, 28);
        ForceTurn(new RbyTurn("THRASH"));
        ForceTurn(new RbyTurn("THRASH"));

        // HIKER
        TalkTo(6, 10);
        ForceTurn(new RbyTurn("THUNDERBOLT"), new RbyTurn("SELFDESTRUCT", Miss));
        ForceTurn(new RbyTurn("THUNDERBOLT"), new RbyTurn("SELFDESTRUCT", Miss));
        ForceTurn(new RbyTurn("THUNDERBOLT"), new RbyTurn("SELFDESTRUCT", Miss));

        // PIDGEY GIRL
        TalkTo("RockTunnel1F", 22, 24);
        ForceTurn(new RbyTurn("THRASH"));
        ForceTurn(new RbyTurn("THRASH"));
        ForceTurn(new RbyTurn("THRASH"), new RbyTurn("QUICK ATTACK", 38));

        // GAMBLER
        TalkTo("Route8", 46, 13);
        ForceTurn(new RbyTurn("THUNDERBOLT", Crit));
        ForceTurn(new RbyTurn("THUNDERBOLT", Crit));

        MoveTo("UndergroundPathWestEast", 47, 2);

        UseItem("BICYCLE");
        PickupItemAt(21, 5, Action.Down); // elixer

        MoveTo("Route7", 5, 14);
        UseItem("BICYCLE");

        TalkTo("CeladonMart2F", 7, 3);
        Buy("TM07", 2);

        TalkTo("CeladonMart4F", 5, 6);
        Buy("POKE DOLL", 2);

        TalkTo("CeladonMartRoof", 12, 2);
        ChooseMenuItem(0); // fresh water
        ClearText();

        TalkTo("CeladonMart5F", 5, 4);
        Buy("X SPEED", 3);

        TalkTo("CeladonMartElevator", 3, 0);
        ChooseMenuItem(0);

        MoveTo("CeladonCity", 8, 14);
        UseItem("BICYCLE");

        CutAt("Route16", 34, 9);
        MoveTo("Route16", 17, 4);
        UseItem("BICYCLE");
        TalkTo("Route16FlyHouse", 2, 3); // fly received

        MoveTo("Route16", 7, 6);
        ItemSwap("HELIX FOSSIL", "POKE DOLL");
        UseItem("TM07", "NIDOKING", "LEER");
        UseItem("HM02", "PIDGEY");
        Fly(Joypad.Down, 3);

        // RIVAL 4
        MoveTo("PokemonTower2F", 15, 5);
        ClearText();
        ForceTurn(new RbyTurn("HORN DRILL"));
        ForceTurn(new RbyTurn("HORN DRILL"));
        ForceTurn(new RbyTurn("THRASH"));
        ForceTurn(new RbyTurn("THRASH"));
        ForceTurn(new RbyTurn("THRASH", Crit));

        // CHANNELER #1
        TalkTo("PokemonTower4F", 15, 7);
        ForceTurn(new RbyTurn("THUNDERBOLT", Crit));
        ForceTurn(new RbyTurn("THUNDERBOLT", Crit));

        PickupItemAt(12, 10); // elixer

        MoveTo("PokemonTower5F", 11, 9);
        ClearText(); // heal pad

        // CHANNELER #2
        MoveTo("PokemonTower6F", 15, 5);
        ClearText();
        ForceTurn(new RbyTurn("THRASH"), new RbyTurn("NIGHT SHADE"));
        ForceTurn(new RbyTurn("THRASH"), new RbyTurn("NIGHT SHADE"));
        ForceTurn(new RbyTurn("THRASH"), new RbyTurn("NIGHT SHADE"));
        ForceTurn(new RbyTurn("THRASH"), new RbyTurn("NIGHT SHADE"));
        ForceTurn(new RbyTurn("THUNDERBOLT", Crit));

        // CHANNELER #3
        TalkTo("PokemonTower6F", 9, 5);
        ForceTurn(new RbyTurn("THUNDERBOLT", Crit));

        PickupItemAt(6, 8); // rare candy
        MoveTo(10, 16);
        ClearText();
        UseItem("POKE DOLL"); // escape ghost

        // ROCKET #1
        MoveTo("PokemonTower7F", 10, 11);
        ClearText();
        ForceTurn(new RbyTurn("THRASH"));
        ForceTurn(new RbyTurn("THRASH"));
        ForceTurn(new RbyTurn("THRASH", Crit));

        // ROCKET #2
        MoveTo(10, 9);
        ClearText();
        ForceTurn(new RbyTurn("THRASH", Crit));
        ForceTurn(new RbyTurn("THRASH", Crit));

        // ROCKET #3
        MoveTo(10, 7);
        ClearText();
        ForceTurn(new RbyTurn("THRASH"));
        ForceTurn(new RbyTurn("THRASH"));
        ForceTurn(new RbyTurn("THRASH"));
        ForceTurn(new RbyTurn("THRASH"));

        TalkTo(10, 3);
        TalkTo(3, 1);
        ClearText(); // Pokeflute received

        MoveTo("LavenderTown", 7, 10);
        Fly(Joypad.Down, 1);

        UseItem("BICYCLE");

        MoveTo("Route16", 27, 10);
        ItemSwap("POKE DOLL", "ELIXER");
        UseItem("POKE FLUTE");
        RunAway();

        PickupItemAt("Route17", 8, 121); // max elixer

        MoveTo("Route18", 40, 8);
        UseItem("BICYCLE");

        CutAt("FuchsiaCity", 18, 19);
        CutAt(16, 11);
        MoveTo("SafariZoneGate", 3, 2);
        ClearText();
        Yes();
        ClearText();
        ClearText(); // sneaky joypad call

        UseItem("BICYCLE");
        PickupItemAt("SafariZoneWest", 19, 7, Action.Down); // gold teeth

        TalkTo("SafariZoneSecretHouse", 3, 3);
        MoveTo("SafariZoneWest", 3, 4);
        UseItem("ESCAPE ROPE");
        Fly(Joypad.Down, 1);

        UseItem("BICYCLE");

        // JUGGLER #1
        TalkTo("FuchsiaGym", 7, 8);
        ForceTurn(new RbyTurn("THRASH", Crit));
        ForceTurn(new RbyTurn("THRASH", Crit));
        ForceTurn(new RbyTurn("THRASH"));
        ForceTurn(new RbyTurn("THRASH", Crit));

        // JUGGLER #2
        MoveTo(1, 7);
        ClearText();
        ForceTurn(new RbyTurn("HORN DRILL"));
        ForceTurn(new RbyTurn("HORN DRILL"));

        // KOGA
        TalkTo(4, 10);
        ForceTurn(new RbyTurn("HORN DRILL"));
        ForceTurn(new RbyTurn("HORN DRILL"));
        ForceTurn(new RbyTurn("HORN DRILL"));
        ForceTurn(new RbyTurn("ELIXER", "NIDOKING"), new RbyTurn("SELFDESTRUCT", Miss));

        MoveTo("FuchsiaCity", 5, 28);
        UseItem("BICYCLE");

        TalkTo("WardensHouse", 2, 3);

        MoveTo("FuchsiaCity", 27, 28);
        Fly(Joypad.None, 0);

        MoveTo(4, 13, Action.Down);
        ItemSwap("NUGGET", "X SPEED");
        UseItem("HM03", "SQUIRTLE");
        UseItem("RARE CANDY", "NIDOKING");
        Surf();

        MoveTo("CinnabarIsland", 4, 4);

        TalkTo("PokemonMansion3F", 10, 5, Action.Up);
        ActivateMansionSwitch();

        MoveTo(16, 14);
        FallDown(); // TODO: look into not having to do this
        TalkTo("PokemonMansionB1F", 18, 25, Action.Up);
        ActivateMansionSwitch();

        TalkTo(20, 3, Action.Up);
        ActivateMansionSwitch();
        PickupItemAt(5, 13); // secret key
        UseItem("ESCAPE ROPE");
        Fly(Joypad.Down, 3);

        UseItem("BICYCLE");
        MoveTo("Route7Gate", 3, 4);
        ClearText();
        MoveTo("Route7", 18, 10);
        UseItem("BICYCLE");

        PickupItemAt("SilphCo5F", 12, 3);

        // ARBOK TRAINER
        TalkTo(8, 16);
        ForceTurn(new RbyTurn("HORN DRILL"));

        PickupItemAt(21, 16);
        TalkTo(7, 13);
        TalkTo("SilphCo3F", 17, 9);

        // SILPH RIVAL
        MoveTo("SilphCo7F", 3, 2, Action.Left);
        ClearText();
        ForceTurn(new RbyTurn("THUNDERBOLT", Crit), new RbyTurn("QUICK ATTACK", 15));
        ForceTurn(new RbyTurn("THUNDERBOLT", Crit));
        ForceTurn(new RbyTurn("THUNDERBOLT", Crit));
        ForceTurn(new RbyTurn("HORN DRILL"));
        ForceTurn(new RbyTurn("HORN DRILL"));

        // SILPH ROCKET
        TalkTo("SilphCo11F", 3, 16);
        ForceTurn(new RbyTurn("BUBBLEBEAM"));
        ForceTurn(new RbyTurn("HORN DRILL"));
        ForceTurn(new RbyTurn("BUBBLEBEAM"));

        TalkTo(6, 13, Action.Up);

        // SILPH GIOVANNI
        MoveTo(6, 13);
        ClearText();
        ForceTurn(new RbyTurn("THUNDERBOLT", Crit));
        ForceTurn(new RbyTurn("THUNDERBOLT", Crit));
        ForceTurn(new RbyTurn("BUBBLEBEAM"));
        ForceTurn(new RbyTurn("HORN DRILL"));

        UseItem("ELIXER", "NIDOKING");
        UseItem("ESCAPE ROPE");

        Fly(Joypad.Down, 2);

        UseItem("BICYCLE");
        TalkTo("CinnabarGym", 15, 7, Action.Up);
        BlaineQuiz(Joypad.A);
        TalkTo(10, 1, Action.Up);
        BlaineQuiz(Joypad.B);
        TalkTo(9, 7, Action.Up);
        BlaineQuiz(Joypad.B);
        TalkTo(9, 13, Action.Up);
        BlaineQuiz(Joypad.B);
        TalkTo(1, 13, Action.Up);
        BlaineQuiz(Joypad.A);
        TalkTo(1, 7, Action.Up);
        BlaineQuiz(Joypad.B);

        // BLAINE
        TalkTo(3, 3);
        ForceTurn(new RbyTurn("BUBBLEBEAM", Crit));
        ForceTurn(new RbyTurn("BUBBLEBEAM", Crit));
        ForceTurn(new RbyTurn("HORN DRILL"));
        ForceTurn(new RbyTurn("HORN DRILL"));

        UseItem("ESCAPE ROPE");
        Fly(Joypad.Down, 4);
        UseItem("BICYCLE");
        CutAt(35, 32);
        CutAt("CeladonGym", 2, 4);

        // BEAUTY
        MoveTo(3, 4);
        ClearText();
        ForceTurn(new RbyTurn("HORN DRILL"));

        // ERIKA
        TalkTo(4, 3);
        ForceTurn(new RbyTurn("THRASH", Crit));
        ForceTurn(new RbyTurn("THRASH", Crit));
        ForceTurn(new RbyTurn("THRASH", Crit));

        CutAt(5, 7);
        MoveTo("CeladonCity", 12, 28);
        Fly(Joypad.Down, 1);

        UseItem("BICYCLE");

        // SABRINA
        TalkTo("SaffronGym", 9, 8);
        ForceTurn(new RbyTurn("THRASH"));
        ForceTurn(new RbyTurn("THRASH", Crit));
        ForceTurn(new RbyTurn("THRASH", Crit));
        ForceTurn(new RbyTurn("THRASH", Crit), new RbyTurn("PSYWAVE", 26));

        MoveTo(1, 5);
        UseItem("ESCAPE ROPE");
        Fly(Joypad.Up, 1);

        UseItem("BICYCLE");

        // RHYHORN
        MoveTo("ViridianGym", 15, 5);
        ClearText();
        ForceTurn(new RbyTurn("BUBBLEBEAM"));

        // BLACKBELT
        MoveTo(10, 4);
        ClearText();
        ForceTurn(new RbyTurn("THUNDERBOLT", Crit));
        ForceTurn(new RbyTurn("THUNDERBOLT", Crit));
        ForceTurn(new RbyTurn("THUNDERBOLT", Crit));

        // GIOVANNI
        MoveTo("ViridianCity", 32, 8);
        TalkTo("ViridianGym", 2, 1);
        ForceTurn(new RbyTurn("BUBBLEBEAM"));
        ForceTurn(new RbyTurn("BUBBLEBEAM", Crit));
        ForceTurn(new RbyTurn("HORN DRILL"));
        ForceTurn(new RbyTurn("HORN DRILL"));
        ForceTurn(new RbyTurn("BUBBLEBEAM"));

        MoveTo("ViridianCity", 32, 8);
        ItemSwap("S.S.TICKET", "MAX ELIXER");
        UseItem("HM04", "SANDSHREW");
        UseItem("TM27", "NIDOKING", "THRASH");
        UseItem("BICYCLE");

        // VIRIDIAN RIVAL
        MoveTo("Route22", 29, 5);
        ClearText();
        ForceTurn(new RbyTurn("THUNDERBOLT", Crit));
        ForceTurn(new RbyTurn("X SPEED"), new RbyTurn("TAIL WHIP", Miss));
        ForceTurn(new RbyTurn("BUBBLEBEAM"));
        ForceTurn(new RbyTurn("THUNDERBOLT"));
        ForceTurn(new RbyTurn("THUNDERBOLT", Crit));
        ForceTurn(new RbyTurn("FISSURE"));
        ForceTurn(new RbyTurn("FISSURE"));

        MoveTo("Route22Gate", 4, 2, Action.Up);
        ClearText();
        MoveTo("Route23", 7, 139);
        UseItem("BICYCLE");
        MoveTo(7, 136, Action.Up);
        ClearText();
        MoveTo(9, 119, Action.Up);
        ClearText();
        MoveTo(10, 105, Action.Up);
        ClearText();
        MoveTo(10, 104, Action.Up);
        Surf();
        MoveTo(10, 96, Action.Up);
        ClearText();
        MoveTo(7, 85, Action.Up);
        ClearText();
        MoveTo(8, 71, Action.Up);
        UseItem("BICYCLE");
        MoveTo(12, 56, Action.Up);
        ClearText();
        MoveTo(5, 35, Action.Up);
        ClearText();

        MoveTo("VictoryRoad1F", 8, 16);
        Strength();
        MoveTo(5, 14);
        PushBoulder(Joypad.Down);
        Execute("D L D");
        for(int i = 0; i < 4; i++) { PushBoulder(Joypad.Right); Execute("R"); }
        Execute("D R");
        for(int i = 0; i < 2; i++) { PushBoulder(Joypad.Up); Execute("U"); }
        Execute("L U");
        for(int i = 0; i < 7; i++) { PushBoulder(Joypad.Right); Execute("R"); }
        Execute("D R");
        PushBoulder(Joypad.Up); Execute("U");
        PushBoulder(Joypad.Up);
        Execute("L L U U R");
        PushBoulder(Joypad.Right);
        Execute("U R R");
        PushBoulder(Joypad.Down);
        MoveTo("VictoryRoad2F", 0, 9);

        Strength();
        MoveTo(5, 14);
        PushBoulder(Joypad.Left);
        Execute("U L L");
        PushBoulder(Joypad.Down); Execute("D");
        PushBoulder(Joypad.Down);
        Execute("R D D");
        PushBoulder(Joypad.Left); Execute("L");
        PushBoulder(Joypad.Left);

        MoveTo("VictoryRoad3F", 23, 6);
        Strength();
        MoveTo(22, 4);
        for(int i = 0; i < 2; i++) { PushBoulder(Joypad.Up); Execute("U"); }
        Execute("R U");
        for(int i = 0; i < 16; i++) { PushBoulder(Joypad.Left); Execute("L"); }
        Execute("U L");
        PushBoulder(Joypad.Down);
        Execute("R D D");
        for(int i = 0; i < 4; i++) { PushBoulder(Joypad.Left); Execute("L"); }
        Execute("U L");
        for(int i = 0; i < 3; i++) { PushBoulder(Joypad.Down); Execute("D"); }
        Execute("L D");
        PushBoulder(Joypad.Right); Execute("U");

        MoveTo(21, 15);
        PushBoulder(Joypad.Right);
        Execute("R R");
        FallDown();

        Strength();
        UseItem("ELIXER", "NIDOKING");
        UseItem("BICYCLE");
        Execute("D R R U");
        for(int i = 0; i < 14; i++) { PushBoulder(Joypad.Left); Execute("L"); }

        TalkTo("IndigoPlateauLobby", 15, 8, Action.Up);

        // TODO: PC functions
        ChooseMenuItem(0);
        ClearText();
        for(int i = 0; i < 3; i++) {
            ChooseMenuItem(1);
            ChooseMenuItem(1);
            ChooseMenuItem(0);
            ClearText();
        }
        MenuPress(Joypad.B);
        MenuPress(Joypad.B);

        // LORELEI
        MoveTo("IndigoPlateauLobby", 8, 0);
        TalkTo("LoreleisRoom", 5, 2, Action.Right);
        ClearText();
        ForceTurn(new RbyTurn("FISSURE"));
        ForceTurn(new RbyTurn("FISSURE"));
        ForceTurn(new RbyTurn("HORN DRILL"));
        ForceTurn(new RbyTurn("HORN DRILL"));
        ForceTurn(new RbyTurn("HORN DRILL"));

        // BRUNO
        Execute("U U U");
        TalkTo("BrunosRoom", 5, 2, Action.Right);
        ClearText();
        ForceTurn(new RbyTurn("BUBBLEBEAM"));
        ForceTurn(new RbyTurn("THUNDERBOLT", Crit));
        ForceTurn(new RbyTurn("THUNDERBOLT", Crit));
        ForceTurn(new RbyTurn("BUBBLEBEAM"));
        ForceTurn(new RbyTurn("HORN DRILL"));

        // AGATHA
        Execute("U U U");
        TalkTo("AgathasRoom", 5, 2, Action.Right);
        ClearText();
        ForceTurn(new RbyTurn("X SPEED"), new RbyTurn("HYPNOSIS", Miss));
        ForceTurn(new RbyTurn("FISSURE"));
        ForceTurn(new RbyTurn("THUNDERBOLT", Crit));
        ForceTurn(new RbyTurn("FISSURE"));
        ForceTurn(new RbyTurn("HORN DRILL"));
        ForceTurn(new RbyTurn("FISSURE"));

        UseItem("MAX ELIXER", "NIDOKING");

        // LANCE
        Execute("U U U");
        MoveTo("LancesRoom", 6, 2);
        ClearText();
        ForceTurn(new RbyTurn("THUNDERBOLT", Crit));
        ForceTurn(new RbyTurn("FISSURE"));
        ForceTurn(new RbyTurn("FISSURE"));
        ForceTurn(new RbyTurn("THUNDERBOLT", Crit), new RbyTurn("SUPERSONIC", Miss));
        ForceTurn(new RbyTurn("HORN DRILL"));

        // CHAMPION
        Execute("L U U U");
        ClearText();
        ForceTurn(new RbyTurn("X SPEED"), new RbyTurn("MIRROR MOVE", Miss));
        ForceTurn(new RbyTurn("HORN DRILL"));
        ForceTurn(new RbyTurn("FISSURE"));
        ForceTurn(new RbyTurn("BUBBLEBEAM"));
        ForceTurn(new RbyTurn("HORN DRILL"));
        ForceTurn(new RbyTurn("HORN DRILL"));
        ForceTurn(new RbyTurn("HORN DRILL"));

        ClearText();

        Dispose();
    }
}