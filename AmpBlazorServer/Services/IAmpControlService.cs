using MPRSG6Z; // For KeyPad, Command, Amp.State
using System.Threading.Tasks;

namespace AmpBlazorServer.Services
{
    public interface IAmpControlService
    {
        bool IsAmpInitialized { get; }
        void InitializeAmp(string comPort, int units, string[] sources, bool polledWait, int pollMs, bool queueDupeElimination);

        // Placeholder methods from MyHub's public interface
        Task<KeyPad?> GetKeyPadAsync(int chan); // Return type made nullable
        Task<Models.AmpStateDto> GetAmpStateAsync(); // Assuming AmpStateDto will be in Models namespace
        Task SendUnitCommandAsync(int unit, int channel, Command command, int value);
        Task SelectSourceAsync(int unit, int source);
        Task SetAllSourcesAsync(int source);
        Task SetVolumeAsync(int unit, int volume);
        Task SetTrebleAsync(int unit, int treble);
        Task SetBassAsync(int unit, int bass);
        Task SetBalanceAsync(int unit, int balance);
        Task SetMuteAsync(int unit, bool mute);
        Task SetLoudnessAsync(int unit, bool loudness);
        Task SetPowerAsync(int unit, bool power);
        Task SetKeyPadLockAsync(int unit, bool keypadLock);
        Task SetSourceLockAsync(int unit, bool sourceLock);
        Task SetAllKeypadsLockAsync(bool allKeypadsLock);
        Task SetAllUnitsOffAsync();
        Task SetAllUnitsOnAsync();
        Task StartAmpAsync();
        Task StopAmpAsync();
    }
}
