using System;
using System.IO;
using System.Threading;
public class VerificationTests {
    public static void testVerifiedMovies() {
        string[] verifiedGBIMovies = Directory.GetFiles("src/testing/verifiedMovies/gbi", "*.txt");
        string[] verifiedBizhawkMovies = Directory.GetFiles("src/testing/verifiedMovies/bizhawk", "*.bk2");

        foreach (string gbiMovie in verifiedGBIMovies) {
            new Thread(() => {
                string gameName = Path.GetFileNameWithoutExtension(gbiMovie);
                if (File.Exists($"roms/{gameName}.gbc")) {
                    GameBoy gb = new GameBoy("roms/gbc_bios.bin", $"roms/{gameName}.gbc", SpeedupFlags.NoVideo | SpeedupFlags.NoSound);
                    if (gameName.Contains("oracleof")) {
                        byte[] state = gb.SaveState();
                        Array.Fill<byte>(state, 0, gb.SaveStateLabels["wram"], 0x8000);
                        gb.LoadState(state);
                    }
                    gb.PlayGBIInputLog(gbiMovie);
                    gb.SetSpeedupFlags(SpeedupFlags.None);
                    for (int i=0; i < 2; i++) {
                        gb.AdvanceFrame();
                    }
                    gb.Screenshot().Save($"src/testing/screenshots/{gameName}.png");
                }
            }).Start();
        }

        foreach (string bizhawkMovie in verifiedBizhawkMovies) {
            new Thread(() => {
                string gameName = Path.GetFileNameWithoutExtension(bizhawkMovie);
                if (File.Exists($"roms/{gameName}.gbc")) {
                    GameBoy gb = new GameBoy("roms/gbc_bios.bin", $"roms/{gameName}.gbc", SpeedupFlags.NoVideo | SpeedupFlags.NoSound);
                    gb.PlayBizhawkMovie(bizhawkMovie);
                    gb.SetSpeedupFlags(SpeedupFlags.None);
                    for (int i=0; i < 2; i++) {
                        gb.AdvanceFrame();
                    }
                    gb.Screenshot().Save($"src/testing/screenshots/{gameName}.png");
                }
            }).Start();
        }
    }
}