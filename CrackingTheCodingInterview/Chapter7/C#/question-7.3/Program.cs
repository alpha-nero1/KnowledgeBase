using System;
using jukebox;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

// Task: Create a musical jukebox using OOP.
// If I were to redo this I would just have the song store the song data
// like an array of strings and the jukebox prints the lines of the array.

namespace C_
{
    class Program
    {
        static void Main(string[] args)
        {
            PlayJukeboxSequence();
            // Cheap little trickeroo to get the program to run forever ;)
            while (true) {
                continue;
            }
        }

        private static async Task PlayJukeboxSequence()
        {
            var jukebox = new Jukebox(
                new List<Song>
                {
                    new Song
                    {
                        Name = "Back in black"
                    },
                    new Song
                    {
                        Name = "Thunderstruck"
                    },
                    new Song
                    {
                        Name = "Macarena"
                    },
                    new Song
                    {
                        Name = "Midnight Runner"
                    }
                }
            );
            jukebox.PlaySong("Macarena");
            await Task.Delay(4000);
            jukebox.PlaySong("Thunderstruck");
        }
    }
}
