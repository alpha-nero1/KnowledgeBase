using System.Collections.Generic;
using System.Collections;
using System;
using System.Threading;

namespace jukebox
{
    class Jukebox
    {
        private readonly Dictionary<string, Song> _songs = new Dictionary<string, Song>();

        private Song _currentSong;

        public Jukebox(List<Song> songs)
        {
            foreach (var song in songs)
            {
                // Build the songs hashtable.
                _songs.Add(song.Name, song);
            }
        }

        public void PlaySong(string songName)
        {
            try
            {
                if (_currentSong != null) _currentSong.Stop();
                var song = _songs[songName];
                _currentSong = song;
                song.Play();
            }
            catch (Exception e)
            {
                Console.WriteLine($"You tried to play a song not in the Jukebox \"{songName}\"");
            }
        }

        public void StopSong(string songName)
        {
            try
            {
                var song = _songs[songName];
                song.Stop();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to stop song \"{songName}\"");
            }
        }
    }
}
