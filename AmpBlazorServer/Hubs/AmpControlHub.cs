using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using MPRSG6Z; // For KeyPad, Command
using AmpBlazorServer.Services; // For IAmpControlService
using AmpBlazorServer.Models; // For AmpStateDto
using System; // For String, Int32, bool

namespace AmpBlazorServer.Hubs
{
    public class AmpControlHub : Hub
    {
        private readonly IAmpControlService _ampControlService;

        public AmpControlHub(IAmpControlService ampControlService)
        {
            _ampControlService = ampControlService ?? throw new ArgumentNullException(nameof(ampControlService));
        }

        // Methods delegated to IAmpControlService
        public Task<KeyPad> GetKeyPadAsync(int chan)
        {
            return _ampControlService.GetKeyPadAsync(chan);
        }

        public Task<AmpStateDto> GetAmpStateAsync()
        {
            return _ampControlService.GetAmpStateAsync();
        }

        public Task SendUnitCommandAsync(int unit, int channel, Command command, int value)
        {
            return _ampControlService.SendUnitCommandAsync(unit, channel, command, value);
        }

        public Task SelectSourceAsync(int unit, int source)
        {
            return _ampControlService.SelectSourceAsync(unit, source);
        }

        public Task SetAllSourcesAsync(int source)
        {
            return _ampControlService.SetAllSourcesAsync(source);
        }

        public Task SetVolumeAsync(int unit, int volume)
        {
            return _ampControlService.SetVolumeAsync(unit, volume);
        }

        public Task SetTrebleAsync(int unit, int treble)
        {
            return _ampControlService.SetTrebleAsync(unit, treble);
        }

        public Task SetBassAsync(int unit, int bass)
        {
            return _ampControlService.SetBassAsync(unit, bass);
        }

        public Task SetBalanceAsync(int unit, int balance)
        {
            return _ampControlService.SetBalanceAsync(unit, balance);
        }

        public Task SetMuteAsync(int unit, bool mute)
        {
            return _ampControlService.SetMuteAsync(unit, mute);
        }

        public Task SetLoudnessAsync(int unit, bool loudness)
        {
            return _ampControlService.SetLoudnessAsync(unit, loudness);
        }

        public Task SetPowerAsync(int unit, bool power)
        {
            return _ampControlService.SetPowerAsync(unit, power);
        }

        public Task SetKeyPadLockAsync(int unit, bool keypadLock)
        {
            return _ampControlService.SetKeyPadLockAsync(unit, keypadLock);
        }

        public Task SetSourceLockAsync(int unit, bool sourceLock)
        {
            return _ampControlService.SetSourceLockAsync(unit, sourceLock);
        }

        public Task SetAllKeypadsLockAsync(bool allKeypadsLock)
        {
            return _ampControlService.SetAllKeypadsLockAsync(allKeypadsLock);
        }

        public Task SetAllUnitsOffAsync()
        {
            return _ampControlService.SetAllUnitsOffAsync();
        }

        public Task SetAllUnitsOnAsync()
        {
            return _ampControlService.SetAllUnitsOnAsync();
        }

        public Task StartAmpAsync()
        {
            return _ampControlService.StartAmpAsync();
        }

        public Task StopAmpAsync()
        {
            return _ampControlService.StopAmpAsync();
        }
    }
}
