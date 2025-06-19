using System.Threading;
using System.Threading.Tasks;
using System;

namespace jukebox
{
    public interface ISong
    {
        string Name { get; set; }

        void Play();
        void Stop();
    }

    public class Song : ISong
    {
        public string Name { get;set; }
        private CancellationToken _cancellationToken { get; set; }
        private CancellationTokenSource _cancellationSource { get; set; }

        public Song() {}
        public Song(string name)
        {
            Name = name;
        }

        private void KeepPlaying()
        {
            _cancellationToken.ThrowIfCancellationRequested();
            int delay = 1000;
            Task.Delay(delay).ContinueWith((task) => {
              _cancellationToken.ThrowIfCancellationRequested();
              Console.WriteLine($"Do do do do do do");
              KeepPlaying();
            });
        }

        public virtual void Play()
        {
            _cancellationSource = new CancellationTokenSource();
            _cancellationToken = _cancellationSource.Token;
            Console.WriteLine($"Started playing \"{Name}\"");
            try 
            {
                KeepPlaying();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Stopped playing song \"{Name}\"");
            }
        }

        public virtual void Stop()
        {
            if (_cancellationSource != null) _cancellationSource.Cancel();
            Console.WriteLine($"Stopped playing song \"{Name}\"");
        }
    }
}
