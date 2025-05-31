using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.SignalR;
using MPRSG6Z; // For Amp, ConfigParameters, KeyPad, Command, Amp.State
using AmpBlazorServer.Hubs;
using AmpBlazorServer.Models;
using System;
using System.Threading.Tasks;
using System.Linq; // For Keypads.Count

namespace AmpBlazorServer.Services
{
    public class AmpControlService : IAmpControlService
    {
        private readonly IConfiguration _configuration;
        private readonly IHubContext<AmpControlHub> _hubContext;
        private Amp? _amp;

        public bool IsAmpInitialized { get; private set; } = false;

        public AmpControlService(IConfiguration configuration, IHubContext<AmpControlHub> hubContext)
        {
            _configuration = configuration;
            _hubContext = hubContext;
        }

        public void InitializeAmp(string comPort, int units, string[] sources, bool polledWait, int pollMs, bool queueDupeElimination)
        {
            if (IsAmpInitialized) return; // Prevent re-initialization for now

            var configParameters = new ConfigParameters
            {
                ComPort = comPort,
                Units = units,
                Sources = sources,
                PolledWait = polledWait,
                PollMS = pollMs,
                RemoveDupes = queueDupeElimination
            };

            try
            {
                _amp = new Amp(configParameters);
                _amp.OnValueChanged += Amp_OnValueChanged;
                _amp.Start();
                IsAmpInitialized = true;
                Console.WriteLine("AmpControlService: Amp initialized and started successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"AmpControlService: Error initializing Amp: {ex.Message}");
                IsAmpInitialized = false;
                // Optionally, rethrow or handle more gracefully depending on application requirements
            }
        }

        private async void Amp_OnValueChanged(object sender, Amp.State e)
        {
            if (!IsAmpInitialized) return;
            Console.WriteLine($"Amp_OnValueChanged: KeypadID {e.Keypad.ID}, Property {e.Property}, NewValue {e.NewValue}, OldValue {e.OldValue}");
            await _hubContext.Clients.All.SendAsync("valueChanged", e);
        }

        public Task<KeyPad?> GetKeyPadAsync(int chan) // chan is 0-indexed
        {
            if (!IsAmpInitialized || _amp == null || chan < 0 || _amp.Keypads == null || chan >= _amp.Keypads.Count)
            {
                Console.WriteLine($"GetKeyPadAsync: Amp not initialized or invalid channel {chan}.");
                return Task.FromResult<KeyPad?>(null);
            }
            return Task.FromResult<KeyPad?>(_amp.Keypads[chan]);
        }

        public Task<AmpStateDto?> GetAmpStateAsync()
        {
            if (!IsAmpInitialized || _amp == null)
            {
                Console.WriteLine("GetAmpStateAsync: Amp not initialized.");
                // Return a DTO indicating not initialized, or null
                return Task.FromResult<AmpStateDto?>(new AmpStateDto { AmpIsRunning = false, AmpIsResponding = false });
            }

            var dto = new AmpStateDto
            {
                Units = _amp.Units,
                KeypadCount = _amp.Keypads?.Count ?? 0,
                AmpIsRunning = _amp.AmpIsRunning,
                AmpIsResponding = _amp.AmpIsResponding,
                ResetCount = _amp.ResetCount,
                Sources = _amp.Sources?.ToArray()
            };
            return Task.FromResult<AmpStateDto?>(dto);
        }

        public Task SendUnitCommandAsync(int unit, int channel, Command command, int value)
        {
            if (!IsAmpInitialized || _amp == null)
            {
                Console.WriteLine($"SendUnitCommandAsync({unit},{channel},{command},{value}): Amp not initialized.");
                return Task.CompletedTask; // Or throw
            }
            _amp.SendCommand(unit, channel, command, value);
            return Task.CompletedTask;
        }

        public Task SelectSourceAsync(int unit, int source)
        {
            if (!IsAmpInitialized || _amp == null) return Task.CompletedTask;
            _amp.SendCommand(unit, 0, Command.Source, source);
            return Task.CompletedTask;
        }

        public Task SetAllSourcesAsync(int source)
        {
            if (!IsAmpInitialized || _amp == null) return Task.CompletedTask;
            _amp.SendCommand(0, 0, Command.Source, source);
            return Task.CompletedTask;
        }

        public Task SetVolumeAsync(int unit, int volume)
        {
            if (!IsAmpInitialized || _amp == null) return Task.CompletedTask;
            _amp.SendCommand(unit, 0, Command.Volume, volume);
            return Task.CompletedTask;
        }

        public Task SetTrebleAsync(int unit, int treble)
        {
            if (!IsAmpInitialized || _amp == null) return Task.CompletedTask;
            _amp.SendCommand(unit, 0, Command.Treble, treble);
            return Task.CompletedTask;
        }

        public Task SetBassAsync(int unit, int bass)
        {
            if (!IsAmpInitialized || _amp == null) return Task.CompletedTask;
            _amp.SendCommand(unit, 0, Command.Bass, bass);
            return Task.CompletedTask;
        }

        public Task SetBalanceAsync(int unit, int balance)
        {
            if (!IsAmpInitialized || _amp == null) return Task.CompletedTask;
            _amp.SendCommand(unit, 0, Command.Balance, balance);
            return Task.CompletedTask;
        }

        public Task SetMuteAsync(int unit, bool mute)
        {
            if (!IsAmpInitialized || _amp == null) return Task.CompletedTask;
            _amp.SendCommand(unit, 0, Command.Mute, mute ? 1 : 0);
            return Task.CompletedTask;
        }

        public Task SetLoudnessAsync(int unit, bool loudness)
        {
            if (!IsAmpInitialized) {
                 Console.WriteLine($"SetLoudnessAsync called for unit {unit} with {loudness}, but Amp not initialized.");
                return Task.CompletedTask;
            }
            Console.WriteLine($"SetLoudnessAsync called for unit {unit} with {loudness}, but no direct 'Loudness' command in MPRSG6Z.Command enum.");
            return Task.CompletedTask;
        }

        public Task SetPowerAsync(int unit, bool power)
        {
            if (!IsAmpInitialized || _amp == null) return Task.CompletedTask;
            _amp.SendCommand(unit, 0, Command.Power, power ? 1 : 0);
            return Task.CompletedTask;
        }

        public Task PropertyUpAsync(int channel_0_indexed, string property) {
            if (!IsAmpInitialized || _amp == null || _amp.Keypads == null || channel_0_indexed >= _amp.Keypads.Count) return Task.CompletedTask;
            _amp.Keypads[channel_0_indexed].Set_ValueUp(property);
            return Task.CompletedTask;
        }

        public Task PropertyDnAsync(int channel_0_indexed, string property) {
            if (!IsAmpInitialized || _amp == null || _amp.Keypads == null || channel_0_indexed >= _amp.Keypads.Count) return Task.CompletedTask;
            _amp.Keypads[channel_0_indexed].Set_ValueDn(property);
            return Task.CompletedTask;
        }

        public Task SetPropertyAsync(int channel_0_indexed, string property, int value) {
            if (!IsAmpInitialized || _amp == null || _amp.Keypads == null || channel_0_indexed >= _amp.Keypads.Count) return Task.CompletedTask;
            _amp.Keypads[channel_0_indexed].Set_Value(value, property);
            return Task.CompletedTask;
        }

        public Task SetKeyPadLockAsync(int unit, bool keypadLock)
        {
            if (!IsAmpInitialized) return Task.CompletedTask;
            Console.WriteLine($"SetKeyPadLockAsync called for unit {unit} with {keypadLock}. No direct command available.");
            return Task.CompletedTask;
        }

        public Task SetSourceLockAsync(int unit, bool sourceLock)
        {
            if (!IsAmpInitialized) return Task.CompletedTask;
            Console.WriteLine($"SetSourceLockAsync called for unit {unit} with {sourceLock}. No direct command available.");
            return Task.CompletedTask;
        }

        public Task SetAllKeypadsLockAsync(bool allKeypadsLock)
        {
            if (!IsAmpInitialized) return Task.CompletedTask;
            Console.WriteLine($"SetAllKeypadsLockAsync called with {allKeypadsLock}. No direct command available.");
            return Task.CompletedTask;
        }

        public Task SetAllUnitsOffAsync()
        {
            if (!IsAmpInitialized || _amp == null) return Task.CompletedTask;
            _amp.SendCommand(0,0,Command.Power,0);
            return Task.CompletedTask;
        }

        public Task SetAllUnitsOnAsync()
        {
            if (!IsAmpInitialized || _amp == null) return Task.CompletedTask;
            _amp.SendCommand(0,0,Command.Power,1);
            return Task.CompletedTask;
        }

        public Task StartAmpAsync()
        {
            if (!IsAmpInitialized || _amp == null) { // Or if _amp is not null but not running, try to start
                 Console.WriteLine($"StartAmpAsync: Amp not initialized or already running.");
                return Task.CompletedTask;
            }
            _amp.Start();
            return Task.CompletedTask;
        }

        public Task StopAmpAsync()
        {
            if (!IsAmpInitialized || _amp == null) return Task.CompletedTask;
            _amp.Stop();
            // IsAmpInitialized might need to be set to false here, or handled by Amp.Stop logic.
            // For now, keep IsAmpInitialized as true, assuming the service can re-start it.
            return Task.CompletedTask;
        }
    }
}
