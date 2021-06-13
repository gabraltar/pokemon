public class YellowTASPidgeFable : YellowForce {

    // TODO:
    //  - TAS menu execution
    //  - TAS instant text execution (this is challenging)
    //  - Better NPC support (being able to specify how they should move)
    //  - Automatic fly menus
    //  - Better pathfinding
    //    > Make pathfinding consider turn frames (last moon room/post underground elixer house)

    public YellowTASPidgeFable() : base(true) {
        //Show();
        ClearCache();
        Record("yellowPidgeottoMainToKoga");

        CacheState("bk2", () => {
            PlayBizhawkMovie("bizhawk/yellowglitchless.bk2", 19323);
        });

        CacheState("forest", () => {
            RunUntil("JoypadOverworld");
            TalkTo(1,5);
            Buy("POKE BALL", 4);
            MoveTo(1,19,9);
            ClearText();
            MoveTo(51,25,27);
            ForceEncounter(Action.Up, 9, 0xF097);
            ClearText();
            ForceYoloball("POKE BALL");
            ClearText();
            MenuPress(Joypad.A);
            Press(Joypad.None, Joypad.A, Joypad.Start);
            RunUntil("JoypadOverworld");
            PartySwap("PIKACHU", "PIDGEOTTO");

            // Duogeotto
            MoveTo(23,9);
            ForceEncounter(Action.Left, 9, 0xE000);
            ClearText();
            ForceTurn(new RbyTurn("GUST", Crit), new RbyTurn("GUST", Crit));
            ForceTurn(new RbyTurn("GUST", Crit), new RbyTurn("GUST"));

            ForceEncounter(Action.Left, 9, 0xE000);
            ClearText();
            ForceTurn(new RbyTurn("GUST"), new RbyTurn("GUST", Crit));
            ForceTurn(new RbyTurn("GUST", Crit), new RbyTurn("SAND-ATTACK", Miss));

            // WEEDLE GUY
            TalkTo(2, 19);
            ForceTurn(new RbyTurn("GUST"), new RbyTurn("STRING SHOT", Miss));
            ForceTurn(new RbyTurn("GUST", Crit), new RbyTurn("STRING SHOT", Miss));
        });

        // BROCK
        CacheState("brock", () => {
            TalkTo("PewterGym", 4, 1);
            ForceTurn(new RbyTurn("GUST", Crit), new RbyTurn("TACKLE", 1));
            ForceTurn(new RbyTurn("GUST", Crit), new RbyTurn("TACKLE", Miss));
            ForceTurn(new RbyTurn("GUST", Crit), new RbyTurn("TACKLE", Miss));
            ForceTurn(new RbyTurn("GUST", Crit), new RbyTurn("TACKLE", Miss));
            ForceTurn(new RbyTurn("GUST", Crit), new RbyTurn("TACKLE", Miss));

            ForceTurn(new RbyTurn("GUST", Crit), new RbyTurn("SCREECH", Miss));
            ForceTurn(new RbyTurn("GUST", Crit), new RbyTurn("SCREECH", Miss));
            ForceTurn(new RbyTurn("GUST", Crit), new RbyTurn("SCREECH", Miss));
            ForceTurn(new RbyTurn("GUST", Crit), new RbyTurn("SCREECH", Miss));
            ForceTurn(new RbyTurn("GUST", Crit), new RbyTurn("SCREECH", Miss));
            ForceTurn(new RbyTurn("GUST", Crit), new RbyTurn("SCREECH", Miss));
            ForceTurn(new RbyTurn("GUST", Crit), new RbyTurn("SCREECH", Miss));
            ForceTurn(new RbyTurn("GUST", Crit), new RbyTurn("SCREECH", Miss));
        });

        CacheState("route3", () => {
            TalkTo(56,1,5);
            Buy("ESCAPE ROPE", 6);
            // ROUTE 3 BC 1
            MoveTo("Route3", 11, 6);
            ClearText();
            ForceTurn(new RbyTurn("GUST", Crit), new RbyTurn("STRING SHOT"));
            ForceTurn(new RbyTurn("GUST", Crit), new RbyTurn("POISON STING"));
            ForceTurn(new RbyTurn("GUST", Crit), new RbyTurn("STRING SHOT"));

            // SHORTS GUY
            TalkTo(14, 4);
            ForceTurn(new RbyTurn("GUST", Crit), new RbyTurn("QUICK ATTACK", 38));
            ForceTurn(new RbyTurn("GUST"), new RbyTurn("LEER", Miss));
            ForceTurn(new RbyTurn("GUST"), new RbyTurn("LEER", Miss));

            // ROUTE 3 BC 2
            TalkTo(19, 5);
            ForceTurn(new RbyTurn("GUST", Crit), new RbyTurn("STRING SHOT"));
            ForceTurn(new RbyTurn("GUST", Crit), new RbyTurn("Harden"));
            ForceTurn(new RbyTurn("GUST", Crit), new RbyTurn("STRING SHOT"));
            ForceTurn(new RbyTurn("GUST", Crit), new RbyTurn("Harden"));
            
            // ROUTE 3 BC 3
            TalkTo(14, 24, 6);
            ForceTurn(new RbyTurn("GUST", Crit), new RbyTurn("STRING SHOT"));
            ForceTurn(new RbyTurn("GUST"), new RbyTurn("Harden"));
            ForceTurn(new RbyTurn("GUST", Crit), new RbyTurn("Harden"));
        });

        //ClearCache();

        CacheState("mtmoon", () => {
            // Clefairy and Mega Punch
            MoveTo(61,28,6);
            ForceEncounter(Action.Right, 9, 0xFCEF);
            ClearText();
            ForceYoloball("POKE BALL");
            ClearText();
            MenuPress(Joypad.A);
            Press(Joypad.None, Joypad.A, Joypad.Start);
            RunUntil("JoypadOverworld");
            PickupItemAt(61,29,5);
            PickupItemAt(59, 2, 2); // moonstone
            PartySwap("PIDGEOTTO", "CLEFAIRY");
            UseItem("TM01", "CLEFAIRY");
            UseItem("MOON STONE", "CLEFAIRY");

            // SUPER NERD
            MoveTo("MtMoonB2F", 13, 8);
            ClearText();
            MoveSwap("MEGA PUNCH", "POUND");
            ForceTurn(new RbyTurn("MEGA PUNCH", Crit));
            ForceTurn(new RbyTurn("MEGA PUNCH", Crit), new RbyTurn("SCREECH", Miss));
            ForceTurn(new RbyTurn("MEGA PUNCH", Crit));
            TalkTo(13, 6);
            Yes();
            ClearText(); // helix fossil picked up

            // Jessie & James
            MoveTo(3, 5);
            ClearText();
            ForceTurn(new RbyTurn("MEGA PUNCH", Crit));
            ForceTurn(new RbyTurn("MEGA PUNCH"), new RbyTurn("GROWL", Miss));
            ForceTurn(new RbyTurn("MEGA PUNCH", 1), new RbyTurn("SMOG", Miss));
            ForceTurn(new RbyTurn("MEGA PUNCH"));
        });

        //ClearCache();
        //Record("yellowTASMisty");

        CacheState("misty", () => {
            MoveTo("CeruleanPokecenter", 13, 4);
            Press(Joypad.Up,Joypad.A);
            ClearText();
            // TODO: PC functions
            ChooseMenuItem(0);
            ClearText();
            ChooseMenuItem(1);
            ChooseMenuItem(1);
            ChooseMenuItem(0);
            ClearText();
            MenuPress(Joypad.B);
            MenuPress(Joypad.B);
            RunUntil("JoypadOverworld");
            TalkTo("CeruleanPokecenter", 3, 2);
            Yes();
            ClearText(); // healed at center

            MoveTo("CeruleanGym", 4, 10);
            // MISTY MINION
            MoveTo(5, 3);
            ClearText();
            ForceTurn(new RbyTurn("MEGA PUNCH"), new RbyTurn("TAIL WHIP", Miss));
            ForceTurn(new RbyTurn("MEGA PUNCH"), new RbyTurn("TAIL WHIP", Miss));

            // MISTY
            TalkTo(4, 2);
            ForceTurn(new RbyTurn("MEGA PUNCH", Crit), new RbyTurn("TACKLE", Miss));
            ForceTurn(new RbyTurn("MEGA PUNCH", Crit), new RbyTurn("WATER GUN", 1));
            ForceTurn(new RbyTurn("MEGA PUNCH", Crit), new RbyTurn("BUBBLEBEAM", Crit | 35));
        });

        //ClearCache();

        CacheState("nuggetbridge", () => {
            //Record("yellowPidgeottoMainNuggetBridge");
            UseItem("TM11", "CLEFABLE", "GROWL");

            // RIVAL 2
            MoveTo("CeruleanCity", 21, 6, Action.Up);
            ClearText();
            ClearText(); // sneaky joypad call
            MoveSwap("MEGA PUNCH", "BUBBLEBEAM");
            ForceTurn(new RbyTurn("BUBBLEBEAM", Crit), new RbyTurn("LEER", Miss));
            ForceTurn(new RbyTurn("BUBBLEBEAM"));
            ForceTurn(new RbyTurn("BUBBLEBEAM"));
            ForceTurn(new RbyTurn("MEGA PUNCH", Crit));

            // NUGGET BRIDGE #1
            TalkTo("Route24", 11, 31);
            ForceTurn(new RbyTurn("BUBBLEBEAM"));
            ForceTurn(new RbyTurn("BUBBLEBEAM"));

            // NUGGET BRIDGE #2
            TalkTo(10, 28);
            ForceTurn(new RbyTurn("BUBBLEBEAM"));
            ForceTurn(new RbyTurn("BUBBLEBEAM", Crit));
            ClearText();

            // NUGGET BRIDGE #3
            TalkTo(11, 25);
            ForceTurn(new RbyTurn("BUBBLEBEAM"), new RbyTurn("QUICK ATTACK", Crit | 20));
            ForceTurn(new RbyTurn("BUBBLEBEAM", Crit));
            ForceTurn(new RbyTurn("POUND", Crit));

            // NUGGET BRIDGE #4
            TalkTo(10, 22);
            ForceTurn(new RbyTurn("BUBBLEBEAM", Crit));
            ForceTurn(new RbyTurn("BUBBLEBEAM", Crit));

            // NUGGET BRIDGE #5
            TalkTo(11, 19);
            ForceTurn(new RbyTurn("POUND", Crit));

            // NUGGET BRIDGE #5
            MoveTo(10, 15);
            ClearText();
            ForceTurn(new RbyTurn("BUBBLEBEAM", Crit));
            ForceTurn(new RbyTurn("BUBBLEBEAM", Crit));

            TalkTo(6,5);
            ClearText();
            MenuPress(Joypad.A);
            ClearText();
            MenuPress(Joypad.A);
            Press(Joypad.None, Joypad.A, Joypad.Start);
            ClearText();

            // HIKER
            MoveTo("Route25", 14, 7);
            ClearText();
            ForceTurn(new RbyTurn("BUBBLEBEAM"));

            // GIRLFRIEND
            TalkTo(18, 8, Action.Down);
            ForceTurn(new RbyTurn("BUBBLEBEAM", Crit));
            ForceTurn(new RbyTurn("BUBBLEBEAM", Crit));

            // BOYFRIEND
            MoveTo(24, 6);
            ClearText();
            ForceTurn(new RbyTurn("POUND"));
            ForceTurn(new RbyTurn("BUBBLEBEAM"));

            // ODDISH GIRL
            TalkTo(37, 4);
            ForceTurn(new RbyTurn("MEGA PUNCH"));
            ForceTurn(new RbyTurn("POUND"));
            ForceTurn(new RbyTurn("MEGA PUNCH"));
            PickupItemAt(38, 3);

            debugPP(PartyMon1);
            MoveTo("BillsHouse", 4, 5);
            UseItem("ETHER", "CLEFABLE", "BUBBLEBEAM");

            TalkTo("BillsHouse", 6, 5, Action.Right);
            Yes();
            ClearText();
            TalkTo(1, 4);
        });

        //ClearCache();
        //Record("yellowSurge");

        CacheState("surge", () => {
            // DIG ROCKET
            MoveTo("CeruleanCity", 30, 9);
            ClearText();
            ForceTurn(new RbyTurn("BUBBLEBEAM", Crit));
            ForceTurn(new RbyTurn("MEGA PUNCH"));

            PickupItemAt("UndergroundPathNorthSouth", 3, 4); // full restore

            // ROUTE 6 #1
            TalkTo("Route6", 11, 30, Action.Down);
            ForceTurn(new RbyTurn("BUBBLEBEAM"));
            ForceTurn(new RbyTurn("BUBBLEBEAM"));
            ForceTurn(new RbyTurn("BUBBLEBEAM"));

            // ROUTE 6 #2
            MoveTo(10, 31);
            ClearText();
            ForceTurn(new RbyTurn("POUND"));
            ForceTurn(new RbyTurn("POUND", Crit), new RbyTurn("QUICK ATTACK", Crit));

            MoveTo("VermilionCity", 18, 30);
            ClearText();

            // RIVAL 3
            MoveTo("SSAnne2F", 37, 8, Action.Up);
            ClearText();
            ForceTurn(new RbyTurn("BUBBLEBEAM", Crit));
            ForceTurn(new RbyTurn("BUBBLEBEAM"));
            ForceTurn(new RbyTurn("BUBBLEBEAM"));
            ForceTurn(new RbyTurn("MEGA PUNCH"));

            TalkTo("SSAnneCaptainsRoom", 4, 2); // hm02 received

            MoveTo("VermilionDock", 14, 2);
            ClearText();
            ClearText(); // watch cutscene

            MoveTo("VermilionCity", 15, 17, Action.Down);
            ItemSwap("TM34", "ESCAPE ROPE");
            UseItem("HM01", "CHARMANDER");
            Cut();

            MoveTo("VermilionGym", 4, 9);
            Press(Joypad.Left);
            ForceCan();
            Press(Joypad.Right);
            ForceCan();

            // SURGE
            TalkTo(5, 1);
            ForceTurn(new RbyTurn("BUBBLEBEAM", SideEffect), new RbyTurn("GROWL", Miss));
            ForceTurn(new RbyTurn("MEGA PUNCH", Crit));
        });

        //ClearCache();
        //Record("yellowRockTunnel");
        
        CacheState("yellowRockTunnel", () => {
            CutAt("VermilionCity", 15, 18);
            TalkTo("PokemonFanClub", 3, 1);
            Yes();
            ClearText();
            MoveTo(5,19,14);
            TalkTo(19,15);
            ClearText();
            Yes();
            ClearText();
            No();
            ClearText();
            MoveTo(85,2,7);
            UseItem("ESCAPE ROPE");
            RunUntil("JoypadOverworld");

            TalkTo("BikeShop", 6, 3);

            MoveTo("CeruleanCity", 13, 26);
            ItemSwap("POKE BALL", "BICYCLE");
            UseItem("TM24", "CLEFABLE", "POUND");
            UseItem("BICYCLE");

            /*
                Works like TalkTo, but uses Cut.
            */
            CutAt(19, 28);
            CutAt("Route9", 5, 8);

            // 4 TURN THRASH
            TalkTo(13, 10);
            ForceTurn(new RbyTurn("MEGA PUNCH"));
            ForceTurn(new RbyTurn("MEGA PUNCH"));
            ForceTurn(new RbyTurn("MEGA PUNCH"));
            ForceTurn(new RbyTurn("MEGA PUNCH"));

            // BUG CATCHER
            TalkTo(40, 8);
            ForceTurn(new RbyTurn("THUNDERBOLT"));
            ForceTurn(new RbyTurn("THUNDERBOLT"));
            ForceTurn(new RbyTurn("THUNDERBOLT"));

            // POKEMANIAC #1
            TalkTo("RockTunnel1F", 23, 8);
            ForceTurn(new RbyTurn("BUBBLEBEAM"));
            ForceTurn(new RbyTurn("THUNDERBOLT"));

            // POKEMANIAC #2
            TalkTo("RockTunnelB1F", 26, 30);
            ForceTurn(new RbyTurn("THUNDERBOLT"));

            // ODDISH GIRL
            TalkTo(14, 28);
            ForceTurn(new RbyTurn("MEGA PUNCH"));
            ForceTurn(new RbyTurn("MEGA PUNCH"));

            // HIKER
            TalkTo(6, 10);
            ForceTurn(new RbyTurn("BUBBLEBEAM"));
            ForceTurn(new RbyTurn("BUBBLEBEAM"));
            ForceTurn(new RbyTurn("BUBBLEBEAM"));

            // PIDGEY GIRL
            TalkTo("RockTunnel1F", 22, 24);
            MoveSwap("BUBBLEBEAM", "THUNDERBOLT");
            ForceTurn(new RbyTurn("THUNDERBOLT"));
            ForceTurn(new RbyTurn("MEGA PUNCH"));
            ForceTurn(new RbyTurn("THUNDERBOLT"));

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
            Buy("TM05", 2);

            TalkTo("CeladonMart4F", 5, 6);
            Buy("POKE DOLL", 2);

            TalkTo("CeladonMartRoof", 12, 2);
            ChooseMenuItem(0); // fresh water
            ClearText();

            MoveTo("CeladonMart4F", 16, 2);

            MoveTo("CeladonCity", 10, 14);
            UseItem("BICYCLE");

            CutAt("Route16", 34, 9);
            MoveTo("Route16", 17, 4);
            UseItem("BICYCLE");
            TalkTo("Route16FlyHouse", 2, 3); // fly received
        });

        //ClearCache();
        //Record("yellowTASPokeFlute");

        CacheState("pokeFlute", () => {

            MoveTo("Route16", 7, 6);
            ItemSwap("HELIX FOSSIL", "POKE DOLL");
            UseItem("TM05", "CLEFABLE", "SING");
            UseItem("HM02", "PIDGEOTTO");
            Fly(Joypad.Down, 3);

            // RIVAL 4
            MoveTo("PokemonTower2F", 15, 5);
            ClearText();
            ForceTurn(new RbyTurn("THUNDERBOLT"), new RbyTurn("LEER", Miss));
            ForceTurn(new RbyTurn("THUNDERBOLT", Crit));
            ForceTurn(new RbyTurn("MEGA PUNCH"));
            ForceTurn(new RbyTurn("BUBBLEBEAM"));
            ForceTurn(new RbyTurn("THUNDERBOLT", Crit));

            // CHANNELER #1
            TalkTo("PokemonTower4F", 15, 7);
            ForceTurn(new RbyTurn("THUNDERBOLT", Crit));
            ForceTurn(new RbyTurn("THUNDERBOLT", Crit));

            PickupItemAt(12, 10); // elixer
            debugPP(PartyMon1);

            MoveTo("PokemonTower5F", 11, 9);
            ClearText(); // heal pad

            // CHANNELER #2
            MoveTo("PokemonTower6F", 15, 5);
            ClearText();
            ForceTurn(new RbyTurn("THUNDERBOLT", Crit));

            // CHANNELER #3
            TalkTo("PokemonTower6F", 9, 5);
            ForceTurn(new RbyTurn("THUNDERBOLT", Crit));

            PickupItemAt(6, 8); // rare candy
            MoveTo(10, 16);
            ClearText();
            UseItem("POKE DOLL"); // escape ghost

            // TOWER J&J
            MoveTo("PokemonTower7F", 10, 12);
            ClearText();
            ForceTurn(new RbyTurn("THUNDERBOLT"), new RbyTurn("GROWL", Miss));
            ForceTurn(new RbyTurn("MEGA KICK"));
            ForceTurn(new RbyTurn("MEGA KICK", Crit));

            TalkTo(10, 3);
            TalkTo(3, 1);
            ClearText(); // Pokeflute received
        });

        //ClearCache();
        //Record("yellowTASKoga");

        CacheState("koga", () => {
            MoveTo("LavenderTown", 7, 10);
            Fly(Joypad.Down, 1);
            TalkTo(133, 3, 2);
            Yes();
            ClearText(); // healed at center
            MoveTo(6,41,10);
            UseItem("BICYCLE");
            MoveTo("Route16", 27, 10);
            ItemSwap("POKE DOLL", "ELIXER");
            UseItem("POKE FLUTE");
            RunAway();
            //ForceYoloball("POKE BALL");
            //ClearText();
            //No();

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
            ForceTurn(new RbyTurn("MEGA PUNCH", Crit));
            ForceTurn(new RbyTurn("MEGA PUNCH", Crit));
            ForceTurn(new RbyTurn("THUNDERBOLT"), new RbyTurn("Psybeam", Crit | 38));
            ForceTurn(new RbyTurn("THUNDERBOLT", Crit), new RbyTurn("Psybeam", Crit));
            ForceTurn(new RbyTurn("MEGA PUNCH", Crit));

            // JUGGLER #2
            MoveTo(1, 7);
            ClearText();
            ForceTurn(new RbyTurn("MEGA PUNCH", Crit));
            ForceTurn(new RbyTurn("MEGA KICK", Crit));
            System.Console.WriteLine(Bag.NumItems);

            // KOGA
            TalkTo(4, 10);
            ForceTurn(new RbyTurn("MEGA KICK", Crit));
            ForceTurn(new RbyTurn("MEGA KICK", Crit));
            ForceTurn(new RbyTurn("THUNDERBOLT"), new RbyTurn("SLEEP POWDER", Miss));
            ForceTurn(new RbyTurn("THUNDERBOLT", Crit));
            ForceTurn(new RbyTurn("THUNDERBOLT", Crit | SideEffect), new RbyTurn("Psychic", Miss));
            ForceTurn(new RbyTurn("MEGA KICK", Crit));
            System.Console.WriteLine(EmulatedSamples/SamplesPerFrame);
        });
        //ClearCache();
        //Record("yellowTASMansion");

        CacheState("erika", () => {
            MoveTo("FuchsiaCity", 5, 28);
            UseItem("BICYCLE");

            TalkTo("WardensHouse", 2, 3);

            MoveTo("FuchsiaCity", 27, 28);
            Fly(Joypad.None, 0);

            MoveTo(4, 13);
            OpenStartMenu();
            MenuPress(Joypad.Down);
            MenuPress(Joypad.A);
            MenuPress(Joypad.Down);
            MenuPress(Joypad.Down);
            MenuPress(Joypad.A);
            MenuPress(Joypad.Down);
            MenuPress(Joypad.A);
            MenuPress(Joypad.A);
            ClearText();
            Yes();
            ClearText();
            UseItem("HM03", "SQUIRTLE");
            UseItem("HM04", "CHARMANDER", "SCRATCH");
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
            UseItem("BICYCLE");
            CutAt(35, 32);
            CutAt("CeladonGym", 2, 4);

            // BEAUTY
            MoveTo(3, 4);
            ClearText();
            ForceTurn(new RbyTurn("MEGA PUNCH", Crit));

            // ERIKA
            TalkTo(4, 3);
            ForceTurn(new RbyTurn("MEGA KICK", Crit));
            ForceTurn(new RbyTurn("MEGA PUNCH", Crit));
            ForceTurn(new RbyTurn("MEGA PUNCH", Crit));
        });

        CacheState("blaine", () => {
            CutAt(5, 7);
            MoveTo("CeladonCity", 12, 28);
            UseItem("MAX ELIXER", "CLEFABLE");
            Fly(Joypad.Down, 1);
            TalkTo(169,7,2);
            MoveTo(8,6,10);

            UseItem("TM35", "CLEFABLE", "MEGA PUNCH");
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
            ForceTurn(new RbyTurn("METRONOME", "AGILITY"), new RbyTurn("TAIL WHIP", Miss));
            ForceTurn(new RbyTurn("METRONOME", "HORN DRILL"));
            ForceTurn(new RbyTurn("METRONOME", "HORN DRILL"));
            ForceTurn(new RbyTurn("METRONOME", "HORN DRILL"));
        });

        //ClearCache();
        //Record("yellowTASSilph");

        CacheState("silphco", () => {
            UseItem("ESCAPE ROPE");

            UseItem("BICYCLE");
            MoveTo("Route7Gate", 3, 3);
            ClearText();
            MoveTo("Route7", 18, 10);
            UseItem("BICYCLE");

            //PickupItemAt("SilphCo5F", 12, 3); //Elixer

            // ARBOK TRAINER
            TalkTo("SilphCo5F", 8, 16);
            ForceTurn(new RbyTurn("THUNDERBOLT", Crit));

            PickupItemAt(21, 16);
            TalkTo(7, 13);
            TalkTo("SilphCo3F", 17, 9);

            // SILPH RIVAL
            MoveTo("SilphCo7F", 3, 2, Action.Left);
            ClearText();
            ForceTurn(new RbyTurn("BUBBLEBEAM", Crit));
            ForceTurn(new RbyTurn("MEGA KICK", Crit));
            ForceTurn(new RbyTurn("MEGA KICK", Crit), new RbyTurn("TAIL WHIP"));
            ForceTurn(new RbyTurn("MEGA KICK"));
            ForceTurn(new RbyTurn("MEGA KICK", Crit));

            // SILPH J&J
            MoveTo("SilphCo11F", 3, 3);
            ClearText();
            ForceTurn(new RbyTurn("THUNDERBOLT", Crit));
            ForceTurn(new RbyTurn("THUNDERBOLT", Crit));
            ForceTurn(new RbyTurn("THUNDERBOLT"));

            TalkTo(6, 13, Action.Up);

            // SILPH GIOVANNI
            MoveTo(6, 13);
            ClearText();
            ForceTurn(new RbyTurn("THUNDERBOLT", Crit));
            ForceTurn(new RbyTurn("THUNDERBOLT", Crit), new RbyTurn("BITE", 1));
            ForceTurn(new RbyTurn("BUBBLEBEAM"));
            ForceTurn(new RbyTurn("MEGA KICK", Crit));

            UseItem("ESCAPE ROPE");
        });

        //ClearCache();

        CacheState("sabrina", () => {
            UseItem("BICYCLE");
            MoveTo("Route7", 18, 10);
            UseItem("BICYCLE");

            // SABRINA
            TalkTo("SaffronGym", 9, 8);
            ForceTurn(new RbyTurn("METRONOME", "AGILITY"), new RbyTurn("FLASH", Miss));
            ForceTurn(new RbyTurn("METRONOME", "THRASH"));
            ForceTurn(new RbyTurn("THRASH"));
            ForceTurn(new RbyTurn("THRASH", Crit));
        });

        //ClearCache();
        //Record("yellowTASGiovanni");

        CacheState("giovannigym", () => {
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
            ForceTurn(new RbyTurn("THUNDERBOLT"));
            ForceTurn(new RbyTurn("THUNDERBOLT", Crit));

            // GIOVANNI
            MoveTo("ViridianCity", 32, 8);
            TalkTo("ViridianGym", 2, 1);
            ForceTurn(new RbyTurn("METRONOME", "AGILITY"), new RbyTurn("FISSURE", Miss));
            ForceTurn(new RbyTurn("BUBBLEBEAM", Crit));
            ForceTurn(new RbyTurn("METRONOME", "HORN DRILL"));
            ForceTurn(new RbyTurn("METRONOME", "HORN DRILL"));
            ForceTurn(new RbyTurn("METRONOME", "HORN DRILL"));
            ForceTurn(new RbyTurn("BUBBLEBEAM"));
        });

        //ClearCache();
        //Record("yellowTASLorelei");

        CacheState("victoryRoad", () => {
            MoveTo("ViridianCity", 32, 8);
            UseItem("ELIXER", "CLEFABLE");
            UseItem("BICYCLE");

            // VIRIDIAN RIVAL
            MoveTo("Route22", 29, 5);
            ClearText();
            ForceTurn(new RbyTurn("METRONOME", "AGILITY"), new RbyTurn("SLASH", 20));
            ForceTurn(new RbyTurn("BUBBLEBEAM", Crit));
            ForceTurn(new RbyTurn("MEGA KICK", Crit));
            ForceTurn(new RbyTurn("MEGA KICK", Crit));
            ForceTurn(new RbyTurn("MEGA KICK", Crit));
            ForceTurn(new RbyTurn("MEGA KICK", Crit));
            ForceTurn(new RbyTurn("METRONOME", "HORN DRILL"));

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
        });

        CacheState("lorelei", () => {
            // LORELEI
            MoveTo("IndigoPlateauLobby", 8, 0);
            TalkTo("LoreleisRoom", 5, 2, Action.Right);
            ClearText();
            ForceTurn(new RbyTurn("METRONOME", "HORN DRILL"));
            ForceTurn(new RbyTurn("THUNDERBOLT", Crit));
            ForceTurn(new RbyTurn("THUNDERBOLT", Crit));
            ForceTurn(new RbyTurn("MEGA KICK"), new RbyTurn("ICE PUNCH", Miss));
            ForceTurn(new RbyTurn("METRONOME", "HORN DRILL"));
        });

        //ClearCache();

        CacheState("bruno", () => {
            // BRUNO
            Execute("U U U");
            TalkTo("BrunosRoom", 5, 2, Action.Right);
            ClearText();
            ForceTurn(new RbyTurn("BUBBLEBEAM"));
            ForceTurn(new RbyTurn("THUNDERBOLT", Crit));
            ForceTurn(new RbyTurn("THUNDERBOLT", Crit), new RbyTurn("HI JUMP KICK", Miss));
            ForceTurn(new RbyTurn("BUBBLEBEAM"));
            ForceTurn(new RbyTurn("METRONOME", "HORN DRILL"));
        });

        CacheState("agatha", () => {
            // AGATHA
            Execute("U U U");
            TalkTo("AgathasRoom", 5, 2, Action.Right);
            ClearText();
            ForceTurn(new RbyTurn("METRONOME", "AGILITY"), new RbyTurn("HYPNOSIS", Miss));
            ForceTurn(new RbyTurn("METRONOME", "FISSURE"));
            ForceTurn(new RbyTurn("THUNDERBOLT", Crit));
            ForceTurn(new RbyTurn("METRONOME", "FISSURE"));
            ForceTurn(new RbyTurn("METRONOME", "HORN DRILL"));
            ForceTurn(new RbyTurn("METRONOME", "FISSURE"));
        });

        //ClearCache();

        CacheState("lance", () => {
            UseItem("ELIXER", "CLEFABLE");
            // LANCE
            Execute("U U U");
            MoveTo("LancesRoom", 6, 2);
            ClearText();
            ForceTurn(new RbyTurn("METRONOME", "AGILITY"), new RbyTurn("HYDRO PUMP", Miss));
            ForceTurn(new RbyTurn("THUNDERBOLT", Crit));
            ForceTurn(new RbyTurn("MEGA KICK", Crit));
            ForceTurn(new RbyTurn("MEGA KICK", Crit));
            ForceTurn(new RbyTurn("THUNDERBOLT", Crit), new RbyTurn("SUPERSONIC", Miss));
            ForceTurn(new RbyTurn("METRONOME", "HORN DRILL"));
        });

        CacheState("champion", () => {
            // CHAMPION
            Execute("L U U U");
            ClearText();
            ForceTurn(new RbyTurn("BUBBLEBEAM", Crit));
            ForceTurn(new RbyTurn("METRONOME", "AGILITY"), new RbyTurn("KINESIS", Miss));
            ForceTurn(new RbyTurn("MEGA KICK"));
            ForceTurn(new RbyTurn("METRONOME", "HORN DRILL"));
            ForceTurn(new RbyTurn("MEGA KICK", Crit));
            ForceTurn(new RbyTurn("METRONOME", "HORN DRILL"));
            ForceTurn(new RbyTurn("METRONOME", "HORN DRILL"));

            ClearText();

            Dispose();
        });
    }

    void debugPP(RbyPokemon pokemon) {
        for (int i = 0; i < 4; i++) {
            System.Console.WriteLine(PartyMon1.Moves[i].Name + " " + PartyMon1.PP[i].ToString());
        }
    }

    void debugBag(RbyBag bag) {
        for (int i = 0; i < bag.NumItems; i++) {
            System.Console.WriteLine(bag.Items[i].Item.Name);
        }
    }
}