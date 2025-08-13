using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Media;
using Bonsai.Harp;
using Harp.Behavior.Design.Adapters;
using Harp.Behavior.Design.Views;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Harp.Behavior.Design.ViewModels;


public class BehaviorViewModel : ViewModelBase
{
    public string AppVersion { get; set; }
    public ReactiveCommand<Unit, Unit> LoadDeviceInformation { get; }

    #region Connection Information

    [Reactive] public ObservableCollection<string> Ports { get; set; }
    [Reactive] public string SelectedPort { get; set; }
    [Reactive] public bool Connected { get; set; }
    [Reactive] public string ConnectButtonText { get; set; } = "Connect";
    public ReactiveCommand<Unit, Unit> ConnectAndGetBaseInfoCommand { get; }

    #endregion

    #region Operations

    public ReactiveCommand<bool, Unit> SaveConfigurationCommand { get; }
    public ReactiveCommand<Unit, Unit> ResetConfigurationCommand { get; }
    //public ReactiveCommand<int, Unit> Port0ApplyConfigurationCommand { get; }
    //public ReactiveCommand<int, Unit> Port1ApplyConfigurationCommand { get; }
    //public ReactiveCommand<int, Unit> Port2ApplyConfigurationCommand { get; }
    //public ReactiveCommand<Unit, Unit> DOPort0ConfigurationCommand { get; }
    //public ReactiveCommand<Unit, Unit> DOPort1ConfigurationCommand { get; }
    //public ReactiveCommand<Unit, Unit> DOPort2ConfigurationCommand { get; }
    //public ReactiveCommand<Unit, Unit> SupplyPort0ConfigurationCommand { get; }
    //public ReactiveCommand<Unit, Unit> SupplyPort1ConfigurationCommand { get; }
    //public ReactiveCommand<Unit, Unit> SupplyPort2ConfigurationCommand { get; }
    public ReactiveCommand<int, Unit> LedApplyConfigurationCommand { get; }  // TESTING
    public ReactiveCommand<int, Unit> DOApplyConfigurationCommand { get; }
    public ReactiveCommand<Unit, Unit> Rgb0ApplyConfigurationCommand { get; }
    public ReactiveCommand<Unit, Unit> Rgb1ApplyConfigurationCommand { get; }

    public ReactiveCommand<int, Unit> CameraApplyConfigurationCommand { get; }
    public ReactiveCommand<int, Unit> ServoApplyConfigurationCommand { get; }
    public ReactiveCommand<Unit, Unit> EncoderApplyConfigurationCommand { get; }

    public ReactiveCommand<Unit, Unit> SerialTimestampApplyConfigurationCommand { get; }
    public ReactiveCommand<Unit, Unit> DO0SetCommand { get; }
    public ReactiveCommand<Unit, Unit> DO0ClearCommand { get; }
    public ReactiveCommand<Unit, Unit> DO1SetCommand { get; }
    public ReactiveCommand<Unit, Unit> DO1ClearCommand { get; }
    public ReactiveCommand<Unit, Unit> DO2SetCommand { get; }
    public ReactiveCommand<Unit, Unit> DO2ClearCommand { get; }
    public ReactiveCommand<Unit, Unit> DO3SetCommand { get; }
    public ReactiveCommand<Unit, Unit> DO3ClearCommand { get; }
    public ReactiveCommand<Unit, Unit> DOPort0SetCommand { get; }
    public ReactiveCommand<Unit, Unit> DOPort0ClearCommand { get; }
    public ReactiveCommand<Unit, Unit> DIOPort0SetCommand { get; }
    public ReactiveCommand<Unit, Unit> DIOPort0ClearCommand { get; }
    public ReactiveCommand<Unit, Unit> SupplyPort0SetCommand { get; }
    public ReactiveCommand<Unit, Unit> SupplyPort0ClearCommand { get; }
    public ReactiveCommand<Unit, Unit> DOPort1SetCommand { get; }
    public ReactiveCommand<Unit, Unit> DOPort1ClearCommand { get; }
    public ReactiveCommand<Unit, Unit> DIOPort1SetCommand { get; }
    public ReactiveCommand<Unit, Unit> DIOPort1ClearCommand { get; }
    public ReactiveCommand<Unit, Unit> SupplyPort1SetCommand { get; }
    public ReactiveCommand<Unit, Unit> SupplyPort1ClearCommand { get; }
    public ReactiveCommand<Unit, Unit> DOPort2SetCommand { get; }
    public ReactiveCommand<Unit, Unit> DOPort2ClearCommand { get; }
    public ReactiveCommand<Unit, Unit> DIOPort2SetCommand { get; }
    public ReactiveCommand<Unit, Unit> DIOPort2ClearCommand { get; }
    public ReactiveCommand<Unit, Unit> SupplyPort2SetCommand { get; }
    public ReactiveCommand<Unit, Unit> SupplyPort2ClearCommand { get; }
    public ReactiveCommand<Unit, Unit> Led0SetCommand { get; }
    public ReactiveCommand<Unit, Unit> Led0ClearCommand { get; }
    public ReactiveCommand<Unit, Unit> Led1SetCommand { get; }
    public ReactiveCommand<Unit, Unit> Led1ClearCommand { get; }
    public ReactiveCommand<Unit, Unit> Rgb0SetCommand { get; }
    public ReactiveCommand<Unit, Unit> Rgb0ClearCommand { get; }
    public ReactiveCommand<Unit, Unit> Rgb1SetCommand { get; }
    public ReactiveCommand<Unit, Unit> Rgb1ClearCommand { get; }
    public ReactiveCommand<Unit, Unit> PwmDO0StartCommand { get; }
    public ReactiveCommand<Unit, Unit> PwmDO0StopCommand { get; }
    public ReactiveCommand<Unit, Unit> PwmDO1StartCommand { get; }
    public ReactiveCommand<Unit, Unit> PwmDO1StopCommand { get; }
    public ReactiveCommand<Unit, Unit> PwmDO2StartCommand { get; }
    public ReactiveCommand<Unit, Unit> PwmDO2StopCommand { get; }
    public ReactiveCommand<Unit, Unit> PwmDO3StartCommand { get; }
    public ReactiveCommand<Unit, Unit> PwmDO3StopCommand { get; }
    public ReactiveCommand<Unit, Unit> SavePulseConfigDO0Command { get; }
    public ReactiveCommand<Unit, Unit> SavePulseConfigDO1Command { get; }
    public ReactiveCommand<Unit, Unit> SavePulseConfigDO2Command { get; }
    public ReactiveCommand<Unit, Unit> SavePulseConfigDO3Command { get; }
    public ReactiveCommand<Unit, Unit> SavePulseConfigDOPort0Command { get; }
    public ReactiveCommand<Unit, Unit> SavePulseConfigDOPort1Command { get; }
    public ReactiveCommand<Unit, Unit> SavePulseConfigDOPort2Command { get; }
    public ReactiveCommand<Unit, Unit> SavePulseConfigSupplyPort0Command { get; }
    public ReactiveCommand<Unit, Unit> SavePulseConfigSupplyPort1Command { get; }
    public ReactiveCommand<Unit, Unit> SavePulseConfigSupplyPort2Command { get; }
    public ReactiveCommand<Unit, Unit> SavePulseConfigRgb0Command { get; }
    public ReactiveCommand<Unit, Unit> SavePulseConfigRgb1Command { get; }
    public ReactiveCommand<Unit, Unit> SavePulseConfigLed0Command { get; }
    public ReactiveCommand<Unit, Unit> SavePulseConfigLed1Command { get; }
    public ReactiveCommand<Unit, Unit> SaveDirectionConfigCommand { get; }
    public ReactiveCommand<Unit, Unit> SaveMimicConfigPort0IRCommand { get; }
    public ReactiveCommand<Unit, Unit> SaveMimicConfigPort1IRCommand { get; }
    public ReactiveCommand<Unit, Unit> SaveMimicConfigPort2IRCommand { get; }
    public ReactiveCommand<Unit, Unit> SaveMimicConfigPort0ValveCommand { get; }
    public ReactiveCommand<Unit, Unit> SaveMimicConfigPort1ValveCommand { get; }
    public ReactiveCommand<Unit, Unit> SaveMimicConfigPort2ValveCommand { get; }

    // LED Configuration Commands
    public ReactiveCommand<Unit, Unit> SaveLedConfigCurrentLed0Command { get; }
    public ReactiveCommand<Unit, Unit> SaveLedConfigCurrentLed1Command { get; }

    // RGB Configuration Commands  
    public ReactiveCommand<Unit, Unit> SaveRgbConfigColorRgb0Command { get; }
    public ReactiveCommand<Unit, Unit> SaveRgbConfigColorRgb1Command { get; }





    public ReactiveCommand<Unit, Unit> ServoOutput2StartCommand { get; }
    public ReactiveCommand<Unit, Unit> ServoOutput2StopCommand { get; }
    public ReactiveCommand<Unit, Unit> ServoOutput3StartCommand { get; }
    public ReactiveCommand<Unit, Unit> ServoOutput3StopCommand { get; }

    public ReactiveCommand<Unit, Unit> Camera0StartCommand { get; }
    public ReactiveCommand<Unit, Unit> Camera0StopCommand { get; }
    public ReactiveCommand<Unit, Unit> Camera1StartCommand { get; }
    public ReactiveCommand<Unit, Unit> Camera1StopCommand { get; }

    #endregion

    #region Device basic information

    [Reactive] public int DeviceID { get; set; }
    [Reactive] public string DeviceName { get; set; }
    [Reactive] public HarpVersion HardwareVersion { get; set; }
    [Reactive] public HarpVersion FirmwareVersion { get; set; }
    [Reactive] public int SerialNumber { get; set; }

    #endregion

    #region Registers

    [Reactive] public DigitalInputs DigitalInputState { get; set; }
    [Reactive] public DigitalOutputs OutputSet { get; set; }
    [Reactive] public DigitalOutputs OutputClear { get; set; }
    [Reactive] public DigitalOutputs OutputToggle { get; set; }
    [Reactive] public DigitalOutputs OutputState { get; set; }
    [Reactive] public PortDigitalIOS PortDIOSet { get; set; }
    [Reactive] public PortDigitalIOS PortDIOClear { get; set; }
    [Reactive] public PortDigitalIOS PortDIOToggle { get; set; }
    [Reactive] public PortDigitalIOS PortDIOState { get; set; }
    [Reactive] public PortDigitalIOS PortDIODirection { get; set; }
    [Reactive] public PortDigitalIOS PortDIOStateEvent { get; set; }
    [Reactive] public AnalogDataPayload AnalogData { get; set; }
    [Reactive] public DigitalOutputs OutputPulseEnable { get; set; }
    [Reactive] public ushort PulseDOPort0 { get; set; }
    [Reactive] public ushort PulseDOPort1 { get; set; }
    [Reactive] public ushort PulseDOPort2 { get; set; }
    [Reactive] public ushort PulseSupplyPort0 { get; set; }
    [Reactive] public ushort PulseSupplyPort1 { get; set; }
    [Reactive] public ushort PulseSupplyPort2 { get; set; }
    [Reactive] public ushort PulseLed0 { get; set; }
    [Reactive] public ushort PulseLed1 { get; set; }
    [Reactive] public ushort PulseRgb0 { get; set; }
    [Reactive] public ushort PulseRgb1 { get; set; }
    [Reactive] public ushort PulseDO0 { get; set; }
    [Reactive] public ushort PulseDO1 { get; set; }
    [Reactive] public ushort PulseDO2 { get; set; }
    [Reactive] public ushort PulseDO3 { get; set; }
    [Reactive] public ushort PwmFrequencyDO0 { get; set; }
    [Reactive] public ushort PwmFrequencyDO1 { get; set; }
    [Reactive] public ushort PwmFrequencyDO2 { get; set; }
    [Reactive] public ushort PwmFrequencyDO3 { get; set; }
    [Reactive] public byte PwmDutyCycleDO0 { get; set; }
    [Reactive] public byte PwmDutyCycleDO1 { get; set; }
    [Reactive] public byte PwmDutyCycleDO2 { get; set; }
    [Reactive] public byte PwmDutyCycleDO3 { get; set; }
    [Reactive] public PwmOutputs PwmStart { get; set; }
    [Reactive] public PwmOutputs PwmStop { get; set; }
    [Reactive] public RgbAllPayload RgbAll { get; set; }
    [Reactive] public RgbPayload Rgb0 { get; set; }
    [Reactive] public RgbPayload Rgb1 { get; set; }
    [Reactive] public byte Led0Current { get; set; }
    [Reactive] public byte Led1Current { get; set; }
    [Reactive] public byte Led0MaxCurrent { get; set; }
    [Reactive] public byte Led1MaxCurrent { get; set; }
    [Reactive] public Events EventEnable { get; set; }
    [Reactive] public CameraOutputs StartCameras { get; set; }
    [Reactive] public CameraOutputs StopCameras { get; set; }
    [Reactive] public ServoOutputs EnableServos { get; set; }
    [Reactive] public ServoOutputs DisableServos { get; set; }
    [Reactive] public EncoderInputs EnableEncoders { get; set; }
    [Reactive] public EncoderModeConfig EncoderMode { get; set; }
    [Reactive] public FrameAcquired Camera0Frame { get; set; }
    [Reactive] public ushort Camera0Frequency { get; set; }
    [Reactive] public FrameAcquired Camera1Frame { get; set; }
    [Reactive] public ushort Camera1Frequency { get; set; }
    [Reactive] public ushort ServoMotor2Period { get; set; }
    [Reactive] public ushort ServoMotor2Pulse { get; set; }
    [Reactive] public ushort ServoMotor3Period { get; set; }
    [Reactive] public ushort ServoMotor3Pulse { get; set; }
    [Reactive] public EncoderInputs EncoderReset { get; set; }
    [Reactive] public byte EnableSerialTimestamp { get; set; }
    [Reactive] public MimicOutput MimicPort0IR { get; set; }
    [Reactive] public MimicOutput MimicPort1IR { get; set; }
    [Reactive] public MimicOutput MimicPort2IR { get; set; }
    [Reactive] public MimicOutput MimicPort0Valve { get; set; }
    [Reactive] public MimicOutput MimicPort1Valve { get; set; }
    [Reactive] public MimicOutput MimicPort2Valve { get; set; }
    [Reactive] public byte PokeInputFilter { get; set; }

    #endregion

    #region Array collections

    [Reactive] public RgbRegisterAdapter RgbAllAdapter { get; set; }
    [Reactive] public RgbRegisterAdapter Rgb0Adapter { get; set; }
    [Reactive] public RgbRegisterAdapter Rgb1Adapter { get; set; }
    #endregion

    #region Events Flags

    public bool IsPortDIEnabled
    {
        get
        {
            return EventEnable.HasFlag(Events.PortDI);
        }
        set
        {
            if (value)
            {
                EventEnable |= Events.PortDI;
            }
            else
            {
                EventEnable &= ~Events.PortDI;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsPortDIEnabled));
            this.RaisePropertyChanged(nameof(EventEnable));
        }
    }

    public bool IsPortDIOEnabled
    {
        get
        {
            return EventEnable.HasFlag(Events.PortDIO);
        }
        set
        {
            if (value)
            {
                EventEnable |= Events.PortDIO;
            }
            else
            {
                EventEnable &= ~Events.PortDIO;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsPortDIOEnabled));
            this.RaisePropertyChanged(nameof(EventEnable));
        }
    }

    public bool IsAnalogDataEnabled
    {
        get
        {
            return EventEnable.HasFlag(Events.AnalogData);
        }
        set
        {
            if (value)
            {
                EventEnable |= Events.AnalogData;
            }
            else
            {
                EventEnable &= ~Events.AnalogData;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsAnalogDataEnabled));
            this.RaisePropertyChanged(nameof(EventEnable));
        }
    }

    public bool IsCamera0Enabled
    {
        get
        {
            return EventEnable.HasFlag(Events.Camera0);
        }
        set
        {
            if (value)
            {
                EventEnable |= Events.Camera0;
            }
            else
            {
                EventEnable &= ~Events.Camera0;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsCamera0Enabled));
            this.RaisePropertyChanged(nameof(EventEnable));
        }
    }

    public bool IsCamera1Enabled
    {
        get
        {
            return EventEnable.HasFlag(Events.Camera1);
        }
        set
        {
            if (value)
            {
                EventEnable |= Events.Camera1;
            }
            else
            {
                EventEnable &= ~Events.Camera1;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsCamera1Enabled));
            this.RaisePropertyChanged(nameof(EventEnable));
        }
    }

    #endregion

    #region DigitalInputs_DigitalInputState Flags

    public bool IsDIPort0Enabled_DigitalInputState
    {
        get
        {
            return DigitalInputState.HasFlag(DigitalInputs.DIPort0);
        }
        set
        {
            if (value)
            {
                DigitalInputState |= DigitalInputs.DIPort0;
            }
            else
            {
                DigitalInputState &= ~DigitalInputs.DIPort0;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDIPort0Enabled_DigitalInputState));
            this.RaisePropertyChanged(nameof(DigitalInputState));
        }
    }

    public bool IsDIPort1Enabled_DigitalInputState
    {
        get
        {
            return DigitalInputState.HasFlag(DigitalInputs.DIPort1);
        }
        set
        {
            if (value)
            {
                DigitalInputState |= DigitalInputs.DIPort1;
            }
            else
            {
                DigitalInputState &= ~DigitalInputs.DIPort1;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDIPort1Enabled_DigitalInputState));
            this.RaisePropertyChanged(nameof(DigitalInputState));
        }
    }

    public bool IsDIPort2Enabled_DigitalInputState
    {
        get
        {
            return DigitalInputState.HasFlag(DigitalInputs.DIPort2);
        }
        set
        {
            if (value)
            {
                DigitalInputState |= DigitalInputs.DIPort2;
            }
            else
            {
                DigitalInputState &= ~DigitalInputs.DIPort2;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDIPort2Enabled_DigitalInputState));
            this.RaisePropertyChanged(nameof(DigitalInputState));
        }
    }

    public bool IsDI3Enabled_DigitalInputState
    {
        get
        {
            return DigitalInputState.HasFlag(DigitalInputs.DI3);
        }
        set
        {
            if (value)
            {
                DigitalInputState |= DigitalInputs.DI3;
            }
            else
            {
                DigitalInputState &= ~DigitalInputs.DI3;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDI3Enabled_DigitalInputState));
            this.RaisePropertyChanged(nameof(DigitalInputState));
        }
    }

    #endregion

    #region DigitalOutputs_OutputSet Flags

    public bool IsDOPort0Enabled_OutputSet
    {
        get
        {
            return OutputSet.HasFlag(DigitalOutputs.DOPort0);
        }
        set
        {
            if (value)
            {
                OutputSet |= DigitalOutputs.DOPort0;
            }
            else
            {
                OutputSet &= ~DigitalOutputs.DOPort0;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDOPort0Enabled_OutputSet));
            this.RaisePropertyChanged(nameof(OutputSet));
        }
    }

    public bool IsDOPort1Enabled_OutputSet
    {
        get
        {
            return OutputSet.HasFlag(DigitalOutputs.DOPort1);
        }
        set
        {
            if (value)
            {
                OutputSet |= DigitalOutputs.DOPort1;
            }
            else
            {
                OutputSet &= ~DigitalOutputs.DOPort1;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDOPort1Enabled_OutputSet));
            this.RaisePropertyChanged(nameof(OutputSet));
        }
    }

    public bool IsDOPort2Enabled_OutputSet
    {
        get
        {
            return OutputSet.HasFlag(DigitalOutputs.DOPort2);
        }
        set
        {
            if (value)
            {
                OutputSet |= DigitalOutputs.DOPort2;
            }
            else
            {
                OutputSet &= ~DigitalOutputs.DOPort2;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDOPort2Enabled_OutputSet));
            this.RaisePropertyChanged(nameof(OutputSet));
        }
    }

    public bool IsSupplyPort0Enabled_OutputSet
    {
        get
        {
            return OutputSet.HasFlag(DigitalOutputs.SupplyPort0);
        }
        set
        {
            if (value)
            {
                OutputSet |= DigitalOutputs.SupplyPort0;
            }
            else
            {
                OutputSet &= ~DigitalOutputs.SupplyPort0;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsSupplyPort0Enabled_OutputSet));
            this.RaisePropertyChanged(nameof(OutputSet));
        }
    }

    public bool IsSupplyPort1Enabled_OutputSet
    {
        get
        {
            return OutputSet.HasFlag(DigitalOutputs.SupplyPort1);
        }
        set
        {
            if (value)
            {
                OutputSet |= DigitalOutputs.SupplyPort1;
            }
            else
            {
                OutputSet &= ~DigitalOutputs.SupplyPort1;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsSupplyPort1Enabled_OutputSet));
            this.RaisePropertyChanged(nameof(OutputSet));
        }
    }

    public bool IsSupplyPort2Enabled_OutputSet
    {
        get
        {
            return OutputSet.HasFlag(DigitalOutputs.SupplyPort2);
        }
        set
        {
            if (value)
            {
                OutputSet |= DigitalOutputs.SupplyPort2;
            }
            else
            {
                OutputSet &= ~DigitalOutputs.SupplyPort2;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsSupplyPort2Enabled_OutputSet));
            this.RaisePropertyChanged(nameof(OutputSet));
        }
    }

    public bool IsLed0Enabled_OutputSet
    {
        get
        {
            return OutputSet.HasFlag(DigitalOutputs.Led0);
        }
        set
        {
            if (value)
            {
                OutputSet |= DigitalOutputs.Led0;
            }
            else
            {
                OutputSet &= ~DigitalOutputs.Led0;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsLed0Enabled_OutputSet));
            this.RaisePropertyChanged(nameof(OutputSet));
        }
    }

    public bool IsLed1Enabled_OutputSet
    {
        get
        {
            return OutputSet.HasFlag(DigitalOutputs.Led1);
        }
        set
        {
            if (value)
            {
                OutputSet |= DigitalOutputs.Led1;
            }
            else
            {
                OutputSet &= ~DigitalOutputs.Led1;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsLed1Enabled_OutputSet));
            this.RaisePropertyChanged(nameof(OutputSet));
        }
    }

    public bool IsRgb0Enabled_OutputSet
    {
        get
        {
            return OutputSet.HasFlag(DigitalOutputs.Rgb0);
        }
        set
        {
            if (value)
            {
                OutputSet |= DigitalOutputs.Rgb0;
            }
            else
            {
                OutputSet &= ~DigitalOutputs.Rgb0;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsRgb0Enabled_OutputSet));
            this.RaisePropertyChanged(nameof(OutputSet));
        }
    }

    public bool IsRgb1Enabled_OutputSet
    {
        get
        {
            return OutputSet.HasFlag(DigitalOutputs.Rgb1);
        }
        set
        {
            if (value)
            {
                OutputSet |= DigitalOutputs.Rgb1;
            }
            else
            {
                OutputSet &= ~DigitalOutputs.Rgb1;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsRgb1Enabled_OutputSet));
            this.RaisePropertyChanged(nameof(OutputSet));
        }
    }

    public bool IsDO0Enabled_OutputSet
    {
        get
        {
            return OutputSet.HasFlag(DigitalOutputs.DO0);
        }
        set
        {
            if (value)
            {
                OutputSet |= DigitalOutputs.DO0;
            }
            else
            {
                OutputSet &= ~DigitalOutputs.DO0;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDO0Enabled_OutputSet));
            this.RaisePropertyChanged(nameof(OutputSet));
        }
    }

    public bool IsDO1Enabled_OutputSet
    {
        get
        {
            return OutputSet.HasFlag(DigitalOutputs.DO1);
        }
        set
        {
            if (value)
            {
                OutputSet |= DigitalOutputs.DO1;
            }
            else
            {
                OutputSet &= ~DigitalOutputs.DO1;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDO1Enabled_OutputSet));
            this.RaisePropertyChanged(nameof(OutputSet));
        }
    }

    public bool IsDO2Enabled_OutputSet
    {
        get
        {
            return OutputSet.HasFlag(DigitalOutputs.DO2);
        }
        set
        {
            if (value)
            {
                OutputSet |= DigitalOutputs.DO2;
            }
            else
            {
                OutputSet &= ~DigitalOutputs.DO2;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDO2Enabled_OutputSet));
            this.RaisePropertyChanged(nameof(OutputSet));
        }
    }

    public bool IsDO3Enabled_OutputSet
    {
        get
        {
            return OutputSet.HasFlag(DigitalOutputs.DO3);
        }
        set
        {
            if (value)
            {
                OutputSet |= DigitalOutputs.DO3;
            }
            else
            {
                OutputSet &= ~DigitalOutputs.DO3;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDO3Enabled_OutputSet));
            this.RaisePropertyChanged(nameof(OutputSet));
        }
    }

    #endregion

    #region DigitalOutputs_OutputClear Flags

    public bool IsDOPort0Enabled_OutputClear
    {
        get
        {
            return OutputClear.HasFlag(DigitalOutputs.DOPort0);
        }
        set
        {
            if (value)
            {
                OutputClear |= DigitalOutputs.DOPort0;
            }
            else
            {
                OutputClear &= ~DigitalOutputs.DOPort0;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDOPort0Enabled_OutputClear));
            this.RaisePropertyChanged(nameof(OutputClear));
        }
    }

    public bool IsDOPort1Enabled_OutputClear
    {
        get
        {
            return OutputClear.HasFlag(DigitalOutputs.DOPort1);
        }
        set
        {
            if (value)
            {
                OutputClear |= DigitalOutputs.DOPort1;
            }
            else
            {
                OutputClear &= ~DigitalOutputs.DOPort1;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDOPort1Enabled_OutputClear));
            this.RaisePropertyChanged(nameof(OutputClear));
        }
    }

    public bool IsDOPort2Enabled_OutputClear
    {
        get
        {
            return OutputClear.HasFlag(DigitalOutputs.DOPort2);
        }
        set
        {
            if (value)
            {
                OutputClear |= DigitalOutputs.DOPort2;
            }
            else
            {
                OutputClear &= ~DigitalOutputs.DOPort2;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDOPort2Enabled_OutputClear));
            this.RaisePropertyChanged(nameof(OutputClear));
        }
    }

    public bool IsSupplyPort0Enabled_OutputClear
    {
        get
        {
            return OutputClear.HasFlag(DigitalOutputs.SupplyPort0);
        }
        set
        {
            if (value)
            {
                OutputClear |= DigitalOutputs.SupplyPort0;
            }
            else
            {
                OutputClear &= ~DigitalOutputs.SupplyPort0;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsSupplyPort0Enabled_OutputClear));
            this.RaisePropertyChanged(nameof(OutputClear));
        }
    }

    public bool IsSupplyPort1Enabled_OutputClear
    {
        get
        {
            return OutputClear.HasFlag(DigitalOutputs.SupplyPort1);
        }
        set
        {
            if (value)
            {
                OutputClear |= DigitalOutputs.SupplyPort1;
            }
            else
            {
                OutputClear &= ~DigitalOutputs.SupplyPort1;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsSupplyPort1Enabled_OutputClear));
            this.RaisePropertyChanged(nameof(OutputClear));
        }
    }

    public bool IsSupplyPort2Enabled_OutputClear
    {
        get
        {
            return OutputClear.HasFlag(DigitalOutputs.SupplyPort2);
        }
        set
        {
            if (value)
            {
                OutputClear |= DigitalOutputs.SupplyPort2;
            }
            else
            {
                OutputClear &= ~DigitalOutputs.SupplyPort2;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsSupplyPort2Enabled_OutputClear));
            this.RaisePropertyChanged(nameof(OutputClear));
        }
    }

    public bool IsLed0Enabled_OutputClear
    {
        get
        {
            return OutputClear.HasFlag(DigitalOutputs.Led0);
        }
        set
        {
            if (value)
            {
                OutputClear |= DigitalOutputs.Led0;
            }
            else
            {
                OutputClear &= ~DigitalOutputs.Led0;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsLed0Enabled_OutputClear));
            this.RaisePropertyChanged(nameof(OutputClear));
        }
    }

    public bool IsLed1Enabled_OutputClear
    {
        get
        {
            return OutputClear.HasFlag(DigitalOutputs.Led1);
        }
        set
        {
            if (value)
            {
                OutputClear |= DigitalOutputs.Led1;
            }
            else
            {
                OutputClear &= ~DigitalOutputs.Led1;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsLed1Enabled_OutputClear));
            this.RaisePropertyChanged(nameof(OutputClear));
        }
    }

    public bool IsRgb0Enabled_OutputClear
    {
        get
        {
            return OutputClear.HasFlag(DigitalOutputs.Rgb0);
        }
        set
        {
            if (value)
            {
                OutputClear |= DigitalOutputs.Rgb0;
            }
            else
            {
                OutputClear &= ~DigitalOutputs.Rgb0;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsRgb0Enabled_OutputClear));
            this.RaisePropertyChanged(nameof(OutputClear));
        }
    }

    public bool IsRgb1Enabled_OutputClear
    {
        get
        {
            return OutputClear.HasFlag(DigitalOutputs.Rgb1);
        }
        set
        {
            if (value)
            {
                OutputClear |= DigitalOutputs.Rgb1;
            }
            else
            {
                OutputClear &= ~DigitalOutputs.Rgb1;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsRgb1Enabled_OutputClear));
            this.RaisePropertyChanged(nameof(OutputClear));
        }
    }

    public bool IsDO0Enabled_OutputClear
    {
        get
        {
            return OutputClear.HasFlag(DigitalOutputs.DO0);
        }
        set
        {
            if (value)
            {
                OutputClear |= DigitalOutputs.DO0;
            }
            else
            {
                OutputClear &= ~DigitalOutputs.DO0;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDO0Enabled_OutputClear));
            this.RaisePropertyChanged(nameof(OutputClear));
        }
    }

    public bool IsDO1Enabled_OutputClear
    {
        get
        {
            return OutputClear.HasFlag(DigitalOutputs.DO1);
        }
        set
        {
            if (value)
            {
                OutputClear |= DigitalOutputs.DO1;
            }
            else
            {
                OutputClear &= ~DigitalOutputs.DO1;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDO1Enabled_OutputClear));
            this.RaisePropertyChanged(nameof(OutputClear));
        }
    }

    public bool IsDO2Enabled_OutputClear
    {
        get
        {
            return OutputClear.HasFlag(DigitalOutputs.DO2);
        }
        set
        {
            if (value)
            {
                OutputClear |= DigitalOutputs.DO2;
            }
            else
            {
                OutputClear &= ~DigitalOutputs.DO2;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDO2Enabled_OutputClear));
            this.RaisePropertyChanged(nameof(OutputClear));
        }
    }

    public bool IsDO3Enabled_OutputClear
    {
        get
        {
            return OutputClear.HasFlag(DigitalOutputs.DO3);
        }
        set
        {
            if (value)
            {
                OutputClear |= DigitalOutputs.DO3;
            }
            else
            {
                OutputClear &= ~DigitalOutputs.DO3;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDO3Enabled_OutputClear));
            this.RaisePropertyChanged(nameof(OutputClear));
        }
    }

    #endregion

    #region DigitalOutputs_OutputToggle Flags

    public bool IsDOPort0Enabled_OutputToggle
    {
        get
        {
            return OutputToggle.HasFlag(DigitalOutputs.DOPort0);
        }
        set
        {
            if (value)
            {
                OutputToggle |= DigitalOutputs.DOPort0;
            }
            else
            {
                OutputToggle &= ~DigitalOutputs.DOPort0;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDOPort0Enabled_OutputToggle));
            this.RaisePropertyChanged(nameof(OutputToggle));
        }
    }

    public bool IsDOPort1Enabled_OutputToggle
    {
        get
        {
            return OutputToggle.HasFlag(DigitalOutputs.DOPort1);
        }
        set
        {
            if (value)
            {
                OutputToggle |= DigitalOutputs.DOPort1;
            }
            else
            {
                OutputToggle &= ~DigitalOutputs.DOPort1;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDOPort1Enabled_OutputToggle));
            this.RaisePropertyChanged(nameof(OutputToggle));
        }
    }

    public bool IsDOPort2Enabled_OutputToggle
    {
        get
        {
            return OutputToggle.HasFlag(DigitalOutputs.DOPort2);
        }
        set
        {
            if (value)
            {
                OutputToggle |= DigitalOutputs.DOPort2;
            }
            else
            {
                OutputToggle &= ~DigitalOutputs.DOPort2;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDOPort2Enabled_OutputToggle));
            this.RaisePropertyChanged(nameof(OutputToggle));
        }
    }

    public bool IsSupplyPort0Enabled_OutputToggle
    {
        get
        {
            return OutputToggle.HasFlag(DigitalOutputs.SupplyPort0);
        }
        set
        {
            if (value)
            {
                OutputToggle |= DigitalOutputs.SupplyPort0;
            }
            else
            {
                OutputToggle &= ~DigitalOutputs.SupplyPort0;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsSupplyPort0Enabled_OutputToggle));
            this.RaisePropertyChanged(nameof(OutputToggle));
        }
    }

    public bool IsSupplyPort1Enabled_OutputToggle
    {
        get
        {
            return OutputToggle.HasFlag(DigitalOutputs.SupplyPort1);
        }
        set
        {
            if (value)
            {
                OutputToggle |= DigitalOutputs.SupplyPort1;
            }
            else
            {
                OutputToggle &= ~DigitalOutputs.SupplyPort1;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsSupplyPort1Enabled_OutputToggle));
            this.RaisePropertyChanged(nameof(OutputToggle));
        }
    }

    public bool IsSupplyPort2Enabled_OutputToggle
    {
        get
        {
            return OutputToggle.HasFlag(DigitalOutputs.SupplyPort2);
        }
        set
        {
            if (value)
            {
                OutputToggle |= DigitalOutputs.SupplyPort2;
            }
            else
            {
                OutputToggle &= ~DigitalOutputs.SupplyPort2;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsSupplyPort2Enabled_OutputToggle));
            this.RaisePropertyChanged(nameof(OutputToggle));
        }
    }

    public bool IsLed0Enabled_OutputToggle
    {
        get
        {
            return OutputToggle.HasFlag(DigitalOutputs.Led0);
        }
        set
        {
            if (value)
            {
                OutputToggle |= DigitalOutputs.Led0;
            }
            else
            {
                OutputToggle &= ~DigitalOutputs.Led0;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsLed0Enabled_OutputToggle));
            this.RaisePropertyChanged(nameof(OutputToggle));
        }
    }

    public bool IsLed1Enabled_OutputToggle
    {
        get
        {
            return OutputToggle.HasFlag(DigitalOutputs.Led1);
        }
        set
        {
            if (value)
            {
                OutputToggle |= DigitalOutputs.Led1;
            }
            else
            {
                OutputToggle &= ~DigitalOutputs.Led1;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsLed1Enabled_OutputToggle));
            this.RaisePropertyChanged(nameof(OutputToggle));
        }
    }

    public bool IsRgb0Enabled_OutputToggle
    {
        get
        {
            return OutputToggle.HasFlag(DigitalOutputs.Rgb0);
        }
        set
        {
            if (value)
            {
                OutputToggle |= DigitalOutputs.Rgb0;
            }
            else
            {
                OutputToggle &= ~DigitalOutputs.Rgb0;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsRgb0Enabled_OutputToggle));
            this.RaisePropertyChanged(nameof(OutputToggle));
        }
    }

    public bool IsRgb1Enabled_OutputToggle
    {
        get
        {
            return OutputToggle.HasFlag(DigitalOutputs.Rgb1);
        }
        set
        {
            if (value)
            {
                OutputToggle |= DigitalOutputs.Rgb1;
            }
            else
            {
                OutputToggle &= ~DigitalOutputs.Rgb1;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsRgb1Enabled_OutputToggle));
            this.RaisePropertyChanged(nameof(OutputToggle));
        }
    }

    public bool IsDO0Enabled_OutputToggle
    {
        get
        {
            return OutputToggle.HasFlag(DigitalOutputs.DO0);
        }
        set
        {
            if (value)
            {
                OutputToggle |= DigitalOutputs.DO0;
            }
            else
            {
                OutputToggle &= ~DigitalOutputs.DO0;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDO0Enabled_OutputToggle));
            this.RaisePropertyChanged(nameof(OutputToggle));
        }
    }

    public bool IsDO1Enabled_OutputToggle
    {
        get
        {
            return OutputToggle.HasFlag(DigitalOutputs.DO1);
        }
        set
        {
            if (value)
            {
                OutputToggle |= DigitalOutputs.DO1;
            }
            else
            {
                OutputToggle &= ~DigitalOutputs.DO1;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDO1Enabled_OutputToggle));
            this.RaisePropertyChanged(nameof(OutputToggle));
        }
    }

    public bool IsDO2Enabled_OutputToggle
    {
        get
        {
            return OutputToggle.HasFlag(DigitalOutputs.DO2);
        }
        set
        {
            if (value)
            {
                OutputToggle |= DigitalOutputs.DO2;
            }
            else
            {
                OutputToggle &= ~DigitalOutputs.DO2;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDO2Enabled_OutputToggle));
            this.RaisePropertyChanged(nameof(OutputToggle));
        }
    }

    public bool IsDO3Enabled_OutputToggle
    {
        get
        {
            return OutputToggle.HasFlag(DigitalOutputs.DO3);
        }
        set
        {
            if (value)
            {
                OutputToggle |= DigitalOutputs.DO3;
            }
            else
            {
                OutputToggle &= ~DigitalOutputs.DO3;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDO3Enabled_OutputToggle));
            this.RaisePropertyChanged(nameof(OutputToggle));
        }
    }

    #endregion

    #region DigitalOutputs_OutputState Flags

    public bool IsDOPort0Enabled_OutputState
    {
        get
        {
            return OutputState.HasFlag(DigitalOutputs.DOPort0);
        }
        set
        {
            if (value)
            {
                OutputState |= DigitalOutputs.DOPort0;
            }
            else
            {
                OutputState &= ~DigitalOutputs.DOPort0;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDOPort0Enabled_OutputState));
            this.RaisePropertyChanged(nameof(OutputState));
        }
    }

    public bool IsDOPort1Enabled_OutputState
    {
        get
        {
            return OutputState.HasFlag(DigitalOutputs.DOPort1);
        }
        set
        {
            if (value)
            {
                OutputState |= DigitalOutputs.DOPort1;
            }
            else
            {
                OutputState &= ~DigitalOutputs.DOPort1;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDOPort1Enabled_OutputState));
            this.RaisePropertyChanged(nameof(OutputState));
        }
    }

    public bool IsDOPort2Enabled_OutputState
    {
        get
        {
            return OutputState.HasFlag(DigitalOutputs.DOPort2);
        }
        set
        {
            if (value)
            {
                OutputState |= DigitalOutputs.DOPort2;
            }
            else
            {
                OutputState &= ~DigitalOutputs.DOPort2;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDOPort2Enabled_OutputState));
            this.RaisePropertyChanged(nameof(OutputState));
        }
    }

    public bool IsSupplyPort0Enabled_OutputState
    {
        get
        {
            return OutputState.HasFlag(DigitalOutputs.SupplyPort0);
        }
        set
        {
            if (value)
            {
                OutputState |= DigitalOutputs.SupplyPort0;
            }
            else
            {
                OutputState &= ~DigitalOutputs.SupplyPort0;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsSupplyPort0Enabled_OutputState));
            this.RaisePropertyChanged(nameof(OutputState));
        }
    }

    public bool IsSupplyPort1Enabled_OutputState
    {
        get
        {
            return OutputState.HasFlag(DigitalOutputs.SupplyPort1);
        }
        set
        {
            if (value)
            {
                OutputState |= DigitalOutputs.SupplyPort1;
            }
            else
            {
                OutputState &= ~DigitalOutputs.SupplyPort1;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsSupplyPort1Enabled_OutputState));
            this.RaisePropertyChanged(nameof(OutputState));
        }
    }

    public bool IsSupplyPort2Enabled_OutputState
    {
        get
        {
            return OutputState.HasFlag(DigitalOutputs.SupplyPort2);
        }
        set
        {
            if (value)
            {
                OutputState |= DigitalOutputs.SupplyPort2;
            }
            else
            {
                OutputState &= ~DigitalOutputs.SupplyPort2;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsSupplyPort2Enabled_OutputState));
            this.RaisePropertyChanged(nameof(OutputState));
        }
    }

    public bool IsLed0Enabled_OutputState
    {
        get
        {
            return OutputState.HasFlag(DigitalOutputs.Led0);
        }
        set
        {
            if (value)
            {
                OutputState |= DigitalOutputs.Led0;
            }
            else
            {
                OutputState &= ~DigitalOutputs.Led0;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsLed0Enabled_OutputState));
            this.RaisePropertyChanged(nameof(OutputState));
        }
    }

    public bool IsLed1Enabled_OutputState
    {
        get
        {
            return OutputState.HasFlag(DigitalOutputs.Led1);
        }
        set
        {
            if (value)
            {
                OutputState |= DigitalOutputs.Led1;
            }
            else
            {
                OutputState &= ~DigitalOutputs.Led1;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsLed1Enabled_OutputState));
            this.RaisePropertyChanged(nameof(OutputState));
        }
    }

    public bool IsRgb0Enabled_OutputState
    {
        get
        {
            return OutputState.HasFlag(DigitalOutputs.Rgb0);
        }
        set
        {
            if (value)
            {
                OutputState |= DigitalOutputs.Rgb0;
            }
            else
            {
                OutputState &= ~DigitalOutputs.Rgb0;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsRgb0Enabled_OutputState));
            this.RaisePropertyChanged(nameof(OutputState));
        }
    }

    public bool IsRgb1Enabled_OutputState
    {
        get
        {
            return OutputState.HasFlag(DigitalOutputs.Rgb1);
        }
        set
        {
            if (value)
            {
                OutputState |= DigitalOutputs.Rgb1;
            }
            else
            {
                OutputState &= ~DigitalOutputs.Rgb1;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsRgb1Enabled_OutputState));
            this.RaisePropertyChanged(nameof(OutputState));
        }
    }

    public bool IsDO0Enabled_OutputState
    {
        get
        {
            return OutputState.HasFlag(DigitalOutputs.DO0);
        }
        set
        {
            if (value)
            {
                OutputState |= DigitalOutputs.DO0;
            }
            else
            {
                OutputState &= ~DigitalOutputs.DO0;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDO0Enabled_OutputState));
            this.RaisePropertyChanged(nameof(OutputState));
        }
    }

    public bool IsDO1Enabled_OutputState
    {
        get
        {
            return OutputState.HasFlag(DigitalOutputs.DO1);
        }
        set
        {
            if (value)
            {
                OutputState |= DigitalOutputs.DO1;
            }
            else
            {
                OutputState &= ~DigitalOutputs.DO1;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDO1Enabled_OutputState));
            this.RaisePropertyChanged(nameof(OutputState));
        }
    }

    public bool IsDO2Enabled_OutputState
    {
        get
        {
            return OutputState.HasFlag(DigitalOutputs.DO2);
        }
        set
        {
            if (value)
            {
                OutputState |= DigitalOutputs.DO2;
            }
            else
            {
                OutputState &= ~DigitalOutputs.DO2;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDO2Enabled_OutputState));
            this.RaisePropertyChanged(nameof(OutputState));
        }
    }

    public bool IsDO3Enabled_OutputState
    {
        get
        {
            return OutputState.HasFlag(DigitalOutputs.DO3);
        }
        set
        {
            if (value)
            {
                OutputState |= DigitalOutputs.DO3;
            }
            else
            {
                OutputState &= ~DigitalOutputs.DO3;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDO3Enabled_OutputState));
            this.RaisePropertyChanged(nameof(OutputState));
        }
    }

    #endregion

    #region PortDigitalIOS_PortDIOSet Flags

    public bool IsDIO0Enabled_PortDIOSet
    {
        get
        {
            return PortDIOSet.HasFlag(PortDigitalIOS.DIO0);
        }
        set
        {
            if (value)
            {
                PortDIOSet |= PortDigitalIOS.DIO0;
            }
            else
            {
                PortDIOSet &= ~PortDigitalIOS.DIO0;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDIO0Enabled_PortDIOSet));
            this.RaisePropertyChanged(nameof(PortDIOSet));
        }
    }

    public bool IsDIO1Enabled_PortDIOSet
    {
        get
        {
            return PortDIOSet.HasFlag(PortDigitalIOS.DIO1);
        }
        set
        {
            if (value)
            {
                PortDIOSet |= PortDigitalIOS.DIO1;
            }
            else
            {
                PortDIOSet &= ~PortDigitalIOS.DIO1;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDIO1Enabled_PortDIOSet));
            this.RaisePropertyChanged(nameof(PortDIOSet));
        }
    }

    public bool IsDIO2Enabled_PortDIOSet
    {
        get
        {
            return PortDIOSet.HasFlag(PortDigitalIOS.DIO2);
        }
        set
        {
            if (value)
            {
                PortDIOSet |= PortDigitalIOS.DIO2;
            }
            else
            {
                PortDIOSet &= ~PortDigitalIOS.DIO2;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDIO2Enabled_PortDIOSet));
            this.RaisePropertyChanged(nameof(PortDIOSet));
        }
    }

    #endregion

    #region PortDigitalIOS_PortDIOClear Flags

    public bool IsDIO0Enabled_PortDIOClear
    {
        get
        {
            return PortDIOClear.HasFlag(PortDigitalIOS.DIO0);
        }
        set
        {
            if (value)
            {
                PortDIOClear |= PortDigitalIOS.DIO0;
            }
            else
            {
                PortDIOClear &= ~PortDigitalIOS.DIO0;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDIO0Enabled_PortDIOClear));
            this.RaisePropertyChanged(nameof(PortDIOClear));
        }
    }

    public bool IsDIO1Enabled_PortDIOClear
    {
        get
        {
            return PortDIOClear.HasFlag(PortDigitalIOS.DIO1);
        }
        set
        {
            if (value)
            {
                PortDIOClear |= PortDigitalIOS.DIO1;
            }
            else
            {
                PortDIOClear &= ~PortDigitalIOS.DIO1;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDIO1Enabled_PortDIOClear));
            this.RaisePropertyChanged(nameof(PortDIOClear));
        }
    }

    public bool IsDIO2Enabled_PortDIOClear
    {
        get
        {
            return PortDIOClear.HasFlag(PortDigitalIOS.DIO2);
        }
        set
        {
            if (value)
            {
                PortDIOClear |= PortDigitalIOS.DIO2;
            }
            else
            {
                PortDIOClear &= ~PortDigitalIOS.DIO2;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDIO2Enabled_PortDIOClear));
            this.RaisePropertyChanged(nameof(PortDIOClear));
        }
    }

    #endregion

    #region PortDigitalIOS_PortDIOToggle Flags

    public bool IsDIO0Enabled_PortDIOToggle
    {
        get
        {
            return PortDIOToggle.HasFlag(PortDigitalIOS.DIO0);
        }
        set
        {
            if (value)
            {
                PortDIOToggle |= PortDigitalIOS.DIO0;
            }
            else
            {
                PortDIOToggle &= ~PortDigitalIOS.DIO0;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDIO0Enabled_PortDIOToggle));
            this.RaisePropertyChanged(nameof(PortDIOToggle));
        }
    }

    public bool IsDIO1Enabled_PortDIOToggle
    {
        get
        {
            return PortDIOToggle.HasFlag(PortDigitalIOS.DIO1);
        }
        set
        {
            if (value)
            {
                PortDIOToggle |= PortDigitalIOS.DIO1;
            }
            else
            {
                PortDIOToggle &= ~PortDigitalIOS.DIO1;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDIO1Enabled_PortDIOToggle));
            this.RaisePropertyChanged(nameof(PortDIOToggle));
        }
    }

    public bool IsDIO2Enabled_PortDIOToggle
    {
        get
        {
            return PortDIOToggle.HasFlag(PortDigitalIOS.DIO2);
        }
        set
        {
            if (value)
            {
                PortDIOToggle |= PortDigitalIOS.DIO2;
            }
            else
            {
                PortDIOToggle &= ~PortDigitalIOS.DIO2;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDIO2Enabled_PortDIOToggle));
            this.RaisePropertyChanged(nameof(PortDIOToggle));
        }
    }

    #endregion

    #region PortDigitalIOS_PortDIOState Flags

    public bool IsDIO0Enabled_PortDIOState
    {
        get
        {
            return PortDIOState.HasFlag(PortDigitalIOS.DIO0);
        }
        set
        {
            if (value)
            {
                PortDIOState |= PortDigitalIOS.DIO0;
            }
            else
            {
                PortDIOState &= ~PortDigitalIOS.DIO0;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDIO0Enabled_PortDIOState));
            this.RaisePropertyChanged(nameof(PortDIOState));
        }
    }

    public bool IsDIO1Enabled_PortDIOState
    {
        get
        {
            return PortDIOState.HasFlag(PortDigitalIOS.DIO1);
        }
        set
        {
            if (value)
            {
                PortDIOState |= PortDigitalIOS.DIO1;
            }
            else
            {
                PortDIOState &= ~PortDigitalIOS.DIO1;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDIO1Enabled_PortDIOState));
            this.RaisePropertyChanged(nameof(PortDIOState));
        }
    }

    public bool IsDIO2Enabled_PortDIOState
    {
        get
        {
            return PortDIOState.HasFlag(PortDigitalIOS.DIO2);
        }
        set
        {
            if (value)
            {
                PortDIOState |= PortDigitalIOS.DIO2;
            }
            else
            {
                PortDIOState &= ~PortDigitalIOS.DIO2;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDIO2Enabled_PortDIOState));
            this.RaisePropertyChanged(nameof(PortDIOState));
        }
    }

    #endregion

    #region PortDigitalIOS_PortDIODirection Flags

    public bool IsDIO0Enabled_PortDIODirection
    {
        get
        {
            return PortDIODirection.HasFlag(PortDigitalIOS.DIO0);
        }
        set
        {
            if (value)
            {
                PortDIODirection |= PortDigitalIOS.DIO0;
            }
            else
            {
                PortDIODirection &= ~PortDigitalIOS.DIO0;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDIO0Enabled_PortDIODirection));
            this.RaisePropertyChanged(nameof(PortDIODirection));
        }
    }

    public bool IsDIO1Enabled_PortDIODirection
    {
        get
        {
            return PortDIODirection.HasFlag(PortDigitalIOS.DIO1);
        }
        set
        {
            if (value)
            {
                PortDIODirection |= PortDigitalIOS.DIO1;
            }
            else
            {
                PortDIODirection &= ~PortDigitalIOS.DIO1;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDIO1Enabled_PortDIODirection));
            this.RaisePropertyChanged(nameof(PortDIODirection));
        }
    }

    public bool IsDIO2Enabled_PortDIODirection
    {
        get
        {
            return PortDIODirection.HasFlag(PortDigitalIOS.DIO2);
        }
        set
        {
            if (value)
            {
                PortDIODirection |= PortDigitalIOS.DIO2;
            }
            else
            {
                PortDIODirection &= ~PortDigitalIOS.DIO2;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDIO2Enabled_PortDIODirection));
            this.RaisePropertyChanged(nameof(PortDIODirection));
        }
    }

    #endregion

    #region PortDigitalIOS_PortDIOStateEvent Flags

    public bool IsDIO0Enabled_PortDIOStateEvent
    {
        get
        {
            return PortDIOStateEvent.HasFlag(PortDigitalIOS.DIO0);
        }
        set
        {
            if (value)
            {
                PortDIOStateEvent |= PortDigitalIOS.DIO0;
            }
            else
            {
                PortDIOStateEvent &= ~PortDigitalIOS.DIO0;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDIO0Enabled_PortDIOStateEvent));
            this.RaisePropertyChanged(nameof(PortDIOStateEvent));
        }
    }

    public bool IsDIO1Enabled_PortDIOStateEvent
    {
        get
        {
            return PortDIOStateEvent.HasFlag(PortDigitalIOS.DIO1);
        }
        set
        {
            if (value)
            {
                PortDIOStateEvent |= PortDigitalIOS.DIO1;
            }
            else
            {
                PortDIOStateEvent &= ~PortDigitalIOS.DIO1;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDIO1Enabled_PortDIOStateEvent));
            this.RaisePropertyChanged(nameof(PortDIOStateEvent));
        }
    }

    public bool IsDIO2Enabled_PortDIOStateEvent
    {
        get
        {
            return PortDIOStateEvent.HasFlag(PortDigitalIOS.DIO2);
        }
        set
        {
            if (value)
            {
                PortDIOStateEvent |= PortDigitalIOS.DIO2;
            }
            else
            {
                PortDIOStateEvent &= ~PortDigitalIOS.DIO2;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDIO2Enabled_PortDIOStateEvent));
            this.RaisePropertyChanged(nameof(PortDIOStateEvent));
        }
    }

    #endregion

    #region DigitalOutputs_OutputPulseEnable Flags

    public bool IsDOPort0Enabled_OutputPulseEnable
    {
        get
        {
            return OutputPulseEnable.HasFlag(DigitalOutputs.DOPort0);
        }
        set
        {
            if (value)
            {
                OutputPulseEnable |= DigitalOutputs.DOPort0;
            }
            else
            {
                OutputPulseEnable &= ~DigitalOutputs.DOPort0;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDOPort0Enabled_OutputPulseEnable));
            this.RaisePropertyChanged(nameof(OutputPulseEnable));
        }
    }

    public bool IsDOPort1Enabled_OutputPulseEnable
    {
        get
        {
            return OutputPulseEnable.HasFlag(DigitalOutputs.DOPort1);
        }
        set
        {
            if (value)
            {
                OutputPulseEnable |= DigitalOutputs.DOPort1;
            }
            else
            {
                OutputPulseEnable &= ~DigitalOutputs.DOPort1;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDOPort1Enabled_OutputPulseEnable));
            this.RaisePropertyChanged(nameof(OutputPulseEnable));
        }
    }

    public bool IsDOPort2Enabled_OutputPulseEnable
    {
        get
        {
            return OutputPulseEnable.HasFlag(DigitalOutputs.DOPort2);
        }
        set
        {
            if (value)
            {
                OutputPulseEnable |= DigitalOutputs.DOPort2;
            }
            else
            {
                OutputPulseEnable &= ~DigitalOutputs.DOPort2;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDOPort2Enabled_OutputPulseEnable));
            this.RaisePropertyChanged(nameof(OutputPulseEnable));
        }
    }

    public bool IsSupplyPort0Enabled_OutputPulseEnable
    {
        get
        {
            return OutputPulseEnable.HasFlag(DigitalOutputs.SupplyPort0);
        }
        set
        {
            if (value)
            {
                OutputPulseEnable |= DigitalOutputs.SupplyPort0;
            }
            else
            {
                OutputPulseEnable &= ~DigitalOutputs.SupplyPort0;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsSupplyPort0Enabled_OutputPulseEnable));
            this.RaisePropertyChanged(nameof(OutputPulseEnable));
        }
    }

    public bool IsSupplyPort1Enabled_OutputPulseEnable
    {
        get
        {
            return OutputPulseEnable.HasFlag(DigitalOutputs.SupplyPort1);
        }
        set
        {
            if (value)
            {
                OutputPulseEnable |= DigitalOutputs.SupplyPort1;
            }
            else
            {
                OutputPulseEnable &= ~DigitalOutputs.SupplyPort1;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsSupplyPort1Enabled_OutputPulseEnable));
            this.RaisePropertyChanged(nameof(OutputPulseEnable));
        }
    }

    public bool IsSupplyPort2Enabled_OutputPulseEnable
    {
        get
        {
            return OutputPulseEnable.HasFlag(DigitalOutputs.SupplyPort2);
        }
        set
        {
            if (value)
            {
                OutputPulseEnable |= DigitalOutputs.SupplyPort2;
            }
            else
            {
                OutputPulseEnable &= ~DigitalOutputs.SupplyPort2;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsSupplyPort2Enabled_OutputPulseEnable));
            this.RaisePropertyChanged(nameof(OutputPulseEnable));
        }
    }

    public bool IsLed0Enabled_OutputPulseEnable
    {
        get
        {
            return OutputPulseEnable.HasFlag(DigitalOutputs.Led0);
        }
        set
        {
            if (value)
            {
                OutputPulseEnable |= DigitalOutputs.Led0;
            }
            else
            {
                OutputPulseEnable &= ~DigitalOutputs.Led0;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsLed0Enabled_OutputPulseEnable));
            this.RaisePropertyChanged(nameof(OutputPulseEnable));
        }
    }

    public bool IsLed1Enabled_OutputPulseEnable
    {
        get
        {
            return OutputPulseEnable.HasFlag(DigitalOutputs.Led1);
        }
        set
        {
            if (value)
            {
                OutputPulseEnable |= DigitalOutputs.Led1;
            }
            else
            {
                OutputPulseEnable &= ~DigitalOutputs.Led1;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsLed1Enabled_OutputPulseEnable));
            this.RaisePropertyChanged(nameof(OutputPulseEnable));
        }
    }

    public bool IsRgb0Enabled_OutputPulseEnable
    {
        get
        {
            return OutputPulseEnable.HasFlag(DigitalOutputs.Rgb0);
        }
        set
        {
            if (value)
            {
                OutputPulseEnable |= DigitalOutputs.Rgb0;
            }
            else
            {
                OutputPulseEnable &= ~DigitalOutputs.Rgb0;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsRgb0Enabled_OutputPulseEnable));
            this.RaisePropertyChanged(nameof(OutputPulseEnable));
        }
    }

    public bool IsRgb1Enabled_OutputPulseEnable
    {
        get
        {
            return OutputPulseEnable.HasFlag(DigitalOutputs.Rgb1);
        }
        set
        {
            if (value)
            {
                OutputPulseEnable |= DigitalOutputs.Rgb1;
            }
            else
            {
                OutputPulseEnable &= ~DigitalOutputs.Rgb1;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsRgb1Enabled_OutputPulseEnable));
            this.RaisePropertyChanged(nameof(OutputPulseEnable));
        }
    }

    public bool IsDO0Enabled_OutputPulseEnable
    {
        get
        {
            return OutputPulseEnable.HasFlag(DigitalOutputs.DO0);
        }
        set
        {
            if (value)
            {
                OutputPulseEnable |= DigitalOutputs.DO0;
            }
            else
            {
                OutputPulseEnable &= ~DigitalOutputs.DO0;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDO0Enabled_OutputPulseEnable));
            this.RaisePropertyChanged(nameof(OutputPulseEnable));
        }
    }

    public bool IsDO1Enabled_OutputPulseEnable
    {
        get
        {
            return OutputPulseEnable.HasFlag(DigitalOutputs.DO1);
        }
        set
        {
            if (value)
            {
                OutputPulseEnable |= DigitalOutputs.DO1;
            }
            else
            {
                OutputPulseEnable &= ~DigitalOutputs.DO1;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDO1Enabled_OutputPulseEnable));
            this.RaisePropertyChanged(nameof(OutputPulseEnable));
        }
    }

    public bool IsDO2Enabled_OutputPulseEnable
    {
        get
        {
            return OutputPulseEnable.HasFlag(DigitalOutputs.DO2);
        }
        set
        {
            if (value)
            {
                OutputPulseEnable |= DigitalOutputs.DO2;
            }
            else
            {
                OutputPulseEnable &= ~DigitalOutputs.DO2;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDO2Enabled_OutputPulseEnable));
            this.RaisePropertyChanged(nameof(OutputPulseEnable));
        }
    }

    public bool IsDO3Enabled_OutputPulseEnable
    {
        get
        {
            return OutputPulseEnable.HasFlag(DigitalOutputs.DO3);
        }
        set
        {
            if (value)
            {
                OutputPulseEnable |= DigitalOutputs.DO3;
            }
            else
            {
                OutputPulseEnable &= ~DigitalOutputs.DO3;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDO3Enabled_OutputPulseEnable));
            this.RaisePropertyChanged(nameof(OutputPulseEnable));
        }
    }

    #endregion

    #region PwmOutputs_PwmStart Flags

    public bool IsPwmDO0Enabled_PwmStart
    {
        get
        {
            return PwmStart.HasFlag(PwmOutputs.PwmDO0);
        }
        set
        {
            if (value)
            {
                PwmStart |= PwmOutputs.PwmDO0;
            }
            else
            {
                PwmStart &= ~PwmOutputs.PwmDO0;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsPwmDO0Enabled_PwmStart));
            this.RaisePropertyChanged(nameof(PwmStart));
        }
    }

    public bool IsPwmDO1Enabled_PwmStart
    {
        get
        {
            return PwmStart.HasFlag(PwmOutputs.PwmDO1);
        }
        set
        {
            if (value)
            {
                PwmStart |= PwmOutputs.PwmDO1;
            }
            else
            {
                PwmStart &= ~PwmOutputs.PwmDO1;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsPwmDO1Enabled_PwmStart));
            this.RaisePropertyChanged(nameof(PwmStart));
        }
    }

    public bool IsPwmDO2Enabled_PwmStart
    {
        get
        {
            return PwmStart.HasFlag(PwmOutputs.PwmDO2);
        }
        set
        {
            if (value)
            {
                PwmStart |= PwmOutputs.PwmDO2;
            }
            else
            {
                PwmStart &= ~PwmOutputs.PwmDO2;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsPwmDO2Enabled_PwmStart));
            this.RaisePropertyChanged(nameof(PwmStart));
        }
    }

    public bool IsPwmDO3Enabled_PwmStart
    {
        get
        {
            return PwmStart.HasFlag(PwmOutputs.PwmDO3);
        }
        set
        {
            if (value)
            {
                PwmStart |= PwmOutputs.PwmDO3;
            }
            else
            {
                PwmStart &= ~PwmOutputs.PwmDO3;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsPwmDO3Enabled_PwmStart));
            this.RaisePropertyChanged(nameof(PwmStart));
        }
    }

    #endregion

    #region PwmOutputs_PwmStop Flags

    public bool IsPwmDO0Enabled_PwmStop
    {
        get
        {
            return PwmStop.HasFlag(PwmOutputs.PwmDO0);
        }
        set
        {
            if (value)
            {
                PwmStop |= PwmOutputs.PwmDO0;
            }
            else
            {
                PwmStop &= ~PwmOutputs.PwmDO0;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsPwmDO0Enabled_PwmStop));
            this.RaisePropertyChanged(nameof(PwmStop));
        }
    }

    public bool IsPwmDO1Enabled_PwmStop
    {
        get
        {
            return PwmStop.HasFlag(PwmOutputs.PwmDO1);
        }
        set
        {
            if (value)
            {
                PwmStop |= PwmOutputs.PwmDO1;
            }
            else
            {
                PwmStop &= ~PwmOutputs.PwmDO1;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsPwmDO1Enabled_PwmStop));
            this.RaisePropertyChanged(nameof(PwmStop));
        }
    }

    public bool IsPwmDO2Enabled_PwmStop
    {
        get
        {
            return PwmStop.HasFlag(PwmOutputs.PwmDO2);
        }
        set
        {
            if (value)
            {
                PwmStop |= PwmOutputs.PwmDO2;
            }
            else
            {
                PwmStop &= ~PwmOutputs.PwmDO2;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsPwmDO2Enabled_PwmStop));
            this.RaisePropertyChanged(nameof(PwmStop));
        }
    }

    public bool IsPwmDO3Enabled_PwmStop
    {
        get
        {
            return PwmStop.HasFlag(PwmOutputs.PwmDO3);
        }
        set
        {
            if (value)
            {
                PwmStop |= PwmOutputs.PwmDO3;
            }
            else
            {
                PwmStop &= ~PwmOutputs.PwmDO3;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsPwmDO3Enabled_PwmStop));
            this.RaisePropertyChanged(nameof(PwmStop));
        }
    }

    #endregion

    #region CameraOutputs_StartCameras Flags

    public bool IsCameraOutput0Enabled_StartCameras
    {
        get
        {
            return StartCameras.HasFlag(CameraOutputs.CameraOutput0);
        }
        set
        {
            if (value)
            {
                StartCameras |= CameraOutputs.CameraOutput0;
            }
            else
            {
                StartCameras &= ~CameraOutputs.CameraOutput0;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsCameraOutput0Enabled_StartCameras));
            this.RaisePropertyChanged(nameof(StartCameras));
        }
    }

    public bool IsCameraOutput1Enabled_StartCameras
    {
        get
        {
            return StartCameras.HasFlag(CameraOutputs.CameraOutput1);
        }
        set
        {
            if (value)
            {
                StartCameras |= CameraOutputs.CameraOutput1;
            }
            else
            {
                StartCameras &= ~CameraOutputs.CameraOutput1;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsCameraOutput1Enabled_StartCameras));
            this.RaisePropertyChanged(nameof(StartCameras));
        }
    }

    #endregion

    #region CameraOutputs_StopCameras Flags

    public bool IsCameraOutput0Enabled_StopCameras
    {
        get
        {
            return StopCameras.HasFlag(CameraOutputs.CameraOutput0);
        }
        set
        {
            if (value)
            {
                StopCameras |= CameraOutputs.CameraOutput0;
            }
            else
            {
                StopCameras &= ~CameraOutputs.CameraOutput0;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsCameraOutput0Enabled_StopCameras));
            this.RaisePropertyChanged(nameof(StopCameras));
        }
    }

    public bool IsCameraOutput1Enabled_StopCameras
    {
        get
        {
            return StopCameras.HasFlag(CameraOutputs.CameraOutput1);
        }
        set
        {
            if (value)
            {
                StopCameras |= CameraOutputs.CameraOutput1;
            }
            else
            {
                StopCameras &= ~CameraOutputs.CameraOutput1;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsCameraOutput1Enabled_StopCameras));
            this.RaisePropertyChanged(nameof(StopCameras));
        }
    }

    #endregion

    #region ServoOutputs_EnableServos Flags

    public bool IsServoOutput2Enabled_EnableServos
    {
        get
        {
            return EnableServos.HasFlag(ServoOutputs.ServoOutput2);
        }
        set
        {
            if (value)
            {
                EnableServos |= ServoOutputs.ServoOutput2;
            }
            else
            {
                EnableServos &= ~ServoOutputs.ServoOutput2;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsServoOutput2Enabled_EnableServos));
            this.RaisePropertyChanged(nameof(EnableServos));
        }
    }

    public bool IsServoOutput3Enabled_EnableServos
    {
        get
        {
            return EnableServos.HasFlag(ServoOutputs.ServoOutput3);
        }
        set
        {
            if (value)
            {
                EnableServos |= ServoOutputs.ServoOutput3;
            }
            else
            {
                EnableServos &= ~ServoOutputs.ServoOutput3;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsServoOutput3Enabled_EnableServos));
            this.RaisePropertyChanged(nameof(EnableServos));
        }
    }

    #endregion

    #region ServoOutputs_DisableServos Flags

    public bool IsServoOutput2Enabled_DisableServos
    {
        get
        {
            return DisableServos.HasFlag(ServoOutputs.ServoOutput2);
        }
        set
        {
            if (value)
            {
                DisableServos |= ServoOutputs.ServoOutput2;
            }
            else
            {
                DisableServos &= ~ServoOutputs.ServoOutput2;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsServoOutput2Enabled_DisableServos));
            this.RaisePropertyChanged(nameof(DisableServos));
        }
    }

    public bool IsServoOutput3Enabled_DisableServos
    {
        get
        {
            return DisableServos.HasFlag(ServoOutputs.ServoOutput3);
        }
        set
        {
            if (value)
            {
                DisableServos |= ServoOutputs.ServoOutput3;
            }
            else
            {
                DisableServos &= ~ServoOutputs.ServoOutput3;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsServoOutput3Enabled_DisableServos));
            this.RaisePropertyChanged(nameof(DisableServos));
        }
    }

    #endregion

    #region EncoderInputs_EnableEncoders Flags

    public bool IsEncoderPort2Enabled_EnableEncoders
    {
        get
        {
            return EnableEncoders.HasFlag(EncoderInputs.EncoderPort2);
        }
        set
        {
            if (value)
            {
                EnableEncoders |= EncoderInputs.EncoderPort2;
            }
            else
            {
                EnableEncoders &= ~EncoderInputs.EncoderPort2;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsEncoderPort2Enabled_EnableEncoders));
            this.RaisePropertyChanged(nameof(EnableEncoders));
        }
    }

    #endregion

    #region FrameAcquired_Camera0Frame Flags

    public bool IsFrameAcquiredEnabled_Camera0Frame
    {
        get
        {
            return Camera0Frame.HasFlag(FrameAcquired.FrameAcquired);
        }
        set
        {
            if (value)
            {
                Camera0Frame |= FrameAcquired.FrameAcquired;
            }
            else
            {
                Camera0Frame &= ~FrameAcquired.FrameAcquired;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsFrameAcquiredEnabled_Camera0Frame));
            this.RaisePropertyChanged(nameof(Camera0Frame));
        }
    }

    #endregion

    #region FrameAcquired_Camera1Frame Flags

    public bool IsFrameAcquiredEnabled_Camera1Frame
    {
        get
        {
            return Camera1Frame.HasFlag(FrameAcquired.FrameAcquired);
        }
        set
        {
            if (value)
            {
                Camera1Frame |= FrameAcquired.FrameAcquired;
            }
            else
            {
                Camera1Frame &= ~FrameAcquired.FrameAcquired;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsFrameAcquiredEnabled_Camera1Frame));
            this.RaisePropertyChanged(nameof(Camera1Frame));
        }
    }

    #endregion

    #region EncoderInputs_EncoderReset Flags

    public bool IsEncoderPort2Enabled_EncoderReset
    {
        get
        {
            return EncoderReset.HasFlag(EncoderInputs.EncoderPort2);
        }
        set
        {
            if (value)
            {
                EncoderReset |= EncoderInputs.EncoderPort2;
            }
            else
            {
                EncoderReset &= ~EncoderInputs.EncoderPort2;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsEncoderPort2Enabled_EncoderReset));
            this.RaisePropertyChanged(nameof(EncoderReset));
        }
    }

    #endregion

    #region Application State

    [ObservableAsProperty] public bool IsLoadingPorts { get; }
    [ObservableAsProperty] public bool IsConnecting { get; }
    [ObservableAsProperty] public bool IsResetting { get; }
    [ObservableAsProperty] public bool IsSaving { get; }

    [Reactive] public bool ShowWriteMessages { get; set; }
    [Reactive] public ObservableCollection<string> HarpEvents { get; set; } = new ObservableCollection<string>();
    [Reactive] public ObservableCollection<string> SentMessages { get; set; } = new ObservableCollection<string>();

    public ReactiveCommand<Unit, Unit> ShowAboutCommand { get; private set; }
    public ReactiveCommand<Unit, Unit> ClearMessagesCommand { get; private set; }
    public ReactiveCommand<Unit, Unit> ShowMessagesCommand { get; private set; }


    #endregion

    private Harp.Behavior.AsyncDevice? _device;
    private IObservable<string> _deviceEventsObservable;
    private IDisposable? _deviceEventsSubscription;

    public BehaviorViewModel()
    {
        var assembly = typeof(BehaviorViewModel).Assembly;
        var informationVersion = assembly.GetName().Version;
        if (informationVersion != null)
            AppVersion = $"v{informationVersion.Major}.{informationVersion.Minor}.{informationVersion.Build}";

        Ports = new ObservableCollection<string>();

        ClearMessagesCommand = ReactiveCommand.Create(() => { SentMessages.Clear(); });
        ShowMessagesCommand = ReactiveCommand.Create(() => { ShowWriteMessages = !ShowWriteMessages; });


        LoadDeviceInformation = ReactiveCommand.CreateFromObservable(LoadUsbInformation);
        LoadDeviceInformation.IsExecuting.ToPropertyEx(this, x => x.IsLoadingPorts);
        LoadDeviceInformation.ThrownExceptions.Subscribe(ex =>
            Console.WriteLine($"Error loading device information with exception: {ex.Message}"));
        //Log.Error(ex, "Error loading device information with exception: {Exception}", ex));

        // can connect if there is a selection and also if the new selection is different than the old one
        var canConnect = this.WhenAnyValue(x => x.SelectedPort)
            .Select(selectedPort => !string.IsNullOrEmpty(selectedPort));

        ShowAboutCommand = ReactiveCommand.CreateFromTask(async () =>
                await new About() { DataContext = new AboutViewModel() }.ShowDialog(
                    (Application.Current?.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)?.MainWindow));

        ConnectAndGetBaseInfoCommand = ReactiveCommand.CreateFromTask(ConnectAndGetBaseInfo, canConnect);
        ConnectAndGetBaseInfoCommand.IsExecuting.ToPropertyEx(this, x => x.IsConnecting);
        ConnectAndGetBaseInfoCommand.ThrownExceptions.Subscribe(ex =>
            //Log.Error(ex, "Error connecting to device with error: {Exception}", ex));
            Console.WriteLine($"Error connecting to device with error: {ex}"));

        var canChangeConfig = this.WhenAnyValue(x => x.Connected).Select(connected => connected);
        // Handle Save and Reset
        SaveConfigurationCommand =
            ReactiveCommand.CreateFromObservable<bool, Unit>(SaveConfiguration, canChangeConfig);
        SaveConfigurationCommand.IsExecuting.ToPropertyEx(this, x => x.IsSaving);
        SaveConfigurationCommand.ThrownExceptions.Subscribe(ex =>
            //Log.Error(ex, "Error saving configuration with error: {Exception}", ex));
            Console.WriteLine($"Error saving configuration with error: {ex}"));

/*        Rgb0ApplyConfigurationCommand =
            ReactiveCommand.CreateFromObservable<Unit, Unit>(ExecuteRgb0ApplyConfiguration);*/
        //Rgb0ApplyConfigurationCommand.IsExecuting.ToPropertyEx(this, x => x.IsSaving);
        //Rgb0ApplyConfigurationCommand.ThrownExceptions.Subscribe(ex =>
        //    //Log.Error(ex, "Error saving configuration with error: {Exception}", ex));
        //    Console.WriteLine($"Error saving configuration with error: {ex}"));

        

        ResetConfigurationCommand = ReactiveCommand.CreateFromObservable(ResetConfiguration, canChangeConfig);
        ResetConfigurationCommand.IsExecuting.ToPropertyEx(this, x => x.IsResetting);
        ResetConfigurationCommand.ThrownExceptions.Subscribe(ex =>
            //Log.Error(ex, "Error resetting device configuration with error: {Exception}", ex));
            Console.WriteLine($"Error resetting device configuration with error: {ex}"));

        this.WhenAnyValue(x => x.Connected)
            .Subscribe(x => { ConnectButtonText = x ? "Disconnect" : "Connect"; });

        this.WhenAnyValue(x => x.EventEnable)
            .Subscribe(x =>
            {
                IsPortDIEnabled = x.HasFlag(Events.PortDI);
                IsPortDIOEnabled = x.HasFlag(Events.PortDIO);
                IsAnalogDataEnabled = x.HasFlag(Events.AnalogData);
                IsCamera0Enabled = x.HasFlag(Events.Camera0);
                IsCamera1Enabled = x.HasFlag(Events.Camera1);
            });

        RgbAllAdapter = new RgbRegisterAdapter(
            "RgbAll",
            2,
            true,
            RgbKind.Array);
        Rgb0Adapter = new RgbRegisterAdapter(
            "Rgb0",
            1,
            true,
            RgbKind.Simple);
        Rgb1Adapter = new RgbRegisterAdapter(
            "Rgb1",
            1,
            true,
            RgbKind.Simple);

        // handle the events from the device
        // When Connected changes subscribe/unsubscribe the device events.
        this.WhenAnyValue(x => x.Connected)
            .ObserveOn(RxApp.MainThreadScheduler)
            .Subscribe(isConnected =>
            {
                if (isConnected && _deviceEventsObservable != null)
                {
                    // Subscribe on the UI thread so that the HarpEvents collection can be updated safely.
                    SubscribeToEvents();
                }
                else
                {
                    // Dispose subscription and clear messages.
                    _deviceEventsSubscription?.Dispose();
                    _deviceEventsSubscription = null;
                }
            });

        this.WhenAnyValue(x => x.DigitalInputState)
            .Subscribe(x =>
            {
                IsDIPort0Enabled_DigitalInputState = x.HasFlag(DigitalInputs.DIPort0);
                IsDIPort1Enabled_DigitalInputState = x.HasFlag(DigitalInputs.DIPort1);
                IsDIPort2Enabled_DigitalInputState = x.HasFlag(DigitalInputs.DIPort2);
                IsDI3Enabled_DigitalInputState = x.HasFlag(DigitalInputs.DI3);
            });

        this.WhenAnyValue(x => x.OutputSet)
            .Subscribe(x =>
            {
                IsDOPort0Enabled_OutputSet = x.HasFlag(DigitalOutputs.DOPort0);
                IsDOPort1Enabled_OutputSet = x.HasFlag(DigitalOutputs.DOPort1);
                IsDOPort2Enabled_OutputSet = x.HasFlag(DigitalOutputs.DOPort2);
                IsSupplyPort0Enabled_OutputSet = x.HasFlag(DigitalOutputs.SupplyPort0);
                IsSupplyPort1Enabled_OutputSet = x.HasFlag(DigitalOutputs.SupplyPort1);
                IsSupplyPort2Enabled_OutputSet = x.HasFlag(DigitalOutputs.SupplyPort2);
                IsLed0Enabled_OutputSet = x.HasFlag(DigitalOutputs.Led0);
                IsLed1Enabled_OutputSet = x.HasFlag(DigitalOutputs.Led1);
                IsRgb0Enabled_OutputSet = x.HasFlag(DigitalOutputs.Rgb0);
                IsRgb1Enabled_OutputSet = x.HasFlag(DigitalOutputs.Rgb1);
                IsDO0Enabled_OutputSet = x.HasFlag(DigitalOutputs.DO0);
                IsDO1Enabled_OutputSet = x.HasFlag(DigitalOutputs.DO1);
                IsDO2Enabled_OutputSet = x.HasFlag(DigitalOutputs.DO2);
                IsDO3Enabled_OutputSet = x.HasFlag(DigitalOutputs.DO3);
            });

        this.WhenAnyValue(x => x.OutputClear)
            .Subscribe(x =>
            {
                IsDOPort0Enabled_OutputClear = x.HasFlag(DigitalOutputs.DOPort0);
                IsDOPort1Enabled_OutputClear = x.HasFlag(DigitalOutputs.DOPort1);
                IsDOPort2Enabled_OutputClear = x.HasFlag(DigitalOutputs.DOPort2);
                IsSupplyPort0Enabled_OutputClear = x.HasFlag(DigitalOutputs.SupplyPort0);
                IsSupplyPort1Enabled_OutputClear = x.HasFlag(DigitalOutputs.SupplyPort1);
                IsSupplyPort2Enabled_OutputClear = x.HasFlag(DigitalOutputs.SupplyPort2);
                IsLed0Enabled_OutputClear = x.HasFlag(DigitalOutputs.Led0);
                IsLed1Enabled_OutputClear = x.HasFlag(DigitalOutputs.Led1);
                IsRgb0Enabled_OutputClear = x.HasFlag(DigitalOutputs.Rgb0);
                IsRgb1Enabled_OutputClear = x.HasFlag(DigitalOutputs.Rgb1);
                IsDO0Enabled_OutputClear = x.HasFlag(DigitalOutputs.DO0);
                IsDO1Enabled_OutputClear = x.HasFlag(DigitalOutputs.DO1);
                IsDO2Enabled_OutputClear = x.HasFlag(DigitalOutputs.DO2);
                IsDO3Enabled_OutputClear = x.HasFlag(DigitalOutputs.DO3);
            });

        this.WhenAnyValue(x => x.OutputToggle)
            .Subscribe(x =>
            {
                IsDOPort0Enabled_OutputToggle = x.HasFlag(DigitalOutputs.DOPort0);
                IsDOPort1Enabled_OutputToggle = x.HasFlag(DigitalOutputs.DOPort1);
                IsDOPort2Enabled_OutputToggle = x.HasFlag(DigitalOutputs.DOPort2);
                IsSupplyPort0Enabled_OutputToggle = x.HasFlag(DigitalOutputs.SupplyPort0);
                IsSupplyPort1Enabled_OutputToggle = x.HasFlag(DigitalOutputs.SupplyPort1);
                IsSupplyPort2Enabled_OutputToggle = x.HasFlag(DigitalOutputs.SupplyPort2);
                IsLed0Enabled_OutputToggle = x.HasFlag(DigitalOutputs.Led0);
                IsLed1Enabled_OutputToggle = x.HasFlag(DigitalOutputs.Led1);
                IsRgb0Enabled_OutputToggle = x.HasFlag(DigitalOutputs.Rgb0);
                IsRgb1Enabled_OutputToggle = x.HasFlag(DigitalOutputs.Rgb1);
                IsDO0Enabled_OutputToggle = x.HasFlag(DigitalOutputs.DO0);
                IsDO1Enabled_OutputToggle = x.HasFlag(DigitalOutputs.DO1);
                IsDO2Enabled_OutputToggle = x.HasFlag(DigitalOutputs.DO2);
                IsDO3Enabled_OutputToggle = x.HasFlag(DigitalOutputs.DO3);
            });

        this.WhenAnyValue(x => x.OutputState)
            .Subscribe(x =>
            {
                IsDOPort0Enabled_OutputState = x.HasFlag(DigitalOutputs.DOPort0);
                IsDOPort1Enabled_OutputState = x.HasFlag(DigitalOutputs.DOPort1);
                IsDOPort2Enabled_OutputState = x.HasFlag(DigitalOutputs.DOPort2);
                IsSupplyPort0Enabled_OutputState = x.HasFlag(DigitalOutputs.SupplyPort0);
                IsSupplyPort1Enabled_OutputState = x.HasFlag(DigitalOutputs.SupplyPort1);
                IsSupplyPort2Enabled_OutputState = x.HasFlag(DigitalOutputs.SupplyPort2);
                IsLed0Enabled_OutputState = x.HasFlag(DigitalOutputs.Led0);
                IsLed1Enabled_OutputState = x.HasFlag(DigitalOutputs.Led1);
                IsRgb0Enabled_OutputState = x.HasFlag(DigitalOutputs.Rgb0);
                IsRgb1Enabled_OutputState = x.HasFlag(DigitalOutputs.Rgb1);
                IsDO0Enabled_OutputState = x.HasFlag(DigitalOutputs.DO0);
                IsDO1Enabled_OutputState = x.HasFlag(DigitalOutputs.DO1);
                IsDO2Enabled_OutputState = x.HasFlag(DigitalOutputs.DO2);
                IsDO3Enabled_OutputState = x.HasFlag(DigitalOutputs.DO3);
            });

        this.WhenAnyValue(x => x.PortDIOSet)
            .Subscribe(x =>
            {
                IsDIO0Enabled_PortDIOSet = x.HasFlag(PortDigitalIOS.DIO0);
                IsDIO1Enabled_PortDIOSet = x.HasFlag(PortDigitalIOS.DIO1);
                IsDIO2Enabled_PortDIOSet = x.HasFlag(PortDigitalIOS.DIO2);
            });

        this.WhenAnyValue(x => x.PortDIOClear)
            .Subscribe(x =>
            {
                IsDIO0Enabled_PortDIOClear = x.HasFlag(PortDigitalIOS.DIO0);
                IsDIO1Enabled_PortDIOClear = x.HasFlag(PortDigitalIOS.DIO1);
                IsDIO2Enabled_PortDIOClear = x.HasFlag(PortDigitalIOS.DIO2);
            });

        this.WhenAnyValue(x => x.PortDIOToggle)
            .Subscribe(x =>
            {
                IsDIO0Enabled_PortDIOToggle = x.HasFlag(PortDigitalIOS.DIO0);
                IsDIO1Enabled_PortDIOToggle = x.HasFlag(PortDigitalIOS.DIO1);
                IsDIO2Enabled_PortDIOToggle = x.HasFlag(PortDigitalIOS.DIO2);
            });

        this.WhenAnyValue(x => x.PortDIOState)
            .Subscribe(x =>
            {
                IsDIO0Enabled_PortDIOState = x.HasFlag(PortDigitalIOS.DIO0);
                IsDIO1Enabled_PortDIOState = x.HasFlag(PortDigitalIOS.DIO1);
                IsDIO2Enabled_PortDIOState = x.HasFlag(PortDigitalIOS.DIO2);
            });

        this.WhenAnyValue(x => x.PortDIODirection)
            .Subscribe(x =>
            {
                IsDIO0Enabled_PortDIODirection = x.HasFlag(PortDigitalIOS.DIO0);
                IsDIO1Enabled_PortDIODirection = x.HasFlag(PortDigitalIOS.DIO1);
                IsDIO2Enabled_PortDIODirection = x.HasFlag(PortDigitalIOS.DIO2);
            });

        this.WhenAnyValue(x => x.PortDIOStateEvent)
            .Subscribe(x =>
            {
                IsDIO0Enabled_PortDIOStateEvent = x.HasFlag(PortDigitalIOS.DIO0);
                IsDIO1Enabled_PortDIOStateEvent = x.HasFlag(PortDigitalIOS.DIO1);
                IsDIO2Enabled_PortDIOStateEvent = x.HasFlag(PortDigitalIOS.DIO2);
            });

        this.WhenAnyValue(x => x.OutputPulseEnable)
            .Subscribe(x =>
            {
                IsDOPort0Enabled_OutputPulseEnable = x.HasFlag(DigitalOutputs.DOPort0);
                IsDOPort1Enabled_OutputPulseEnable = x.HasFlag(DigitalOutputs.DOPort1);
                IsDOPort2Enabled_OutputPulseEnable = x.HasFlag(DigitalOutputs.DOPort2);
                IsSupplyPort0Enabled_OutputPulseEnable = x.HasFlag(DigitalOutputs.SupplyPort0);
                IsSupplyPort1Enabled_OutputPulseEnable = x.HasFlag(DigitalOutputs.SupplyPort1);
                IsSupplyPort2Enabled_OutputPulseEnable = x.HasFlag(DigitalOutputs.SupplyPort2);
                IsLed0Enabled_OutputPulseEnable = x.HasFlag(DigitalOutputs.Led0);
                IsLed1Enabled_OutputPulseEnable = x.HasFlag(DigitalOutputs.Led1);
                IsRgb0Enabled_OutputPulseEnable = x.HasFlag(DigitalOutputs.Rgb0);
                IsRgb1Enabled_OutputPulseEnable = x.HasFlag(DigitalOutputs.Rgb1);
                IsDO0Enabled_OutputPulseEnable = x.HasFlag(DigitalOutputs.DO0);
                IsDO1Enabled_OutputPulseEnable = x.HasFlag(DigitalOutputs.DO1);
                IsDO2Enabled_OutputPulseEnable = x.HasFlag(DigitalOutputs.DO2);
                IsDO3Enabled_OutputPulseEnable = x.HasFlag(DigitalOutputs.DO3);
            });

        this.WhenAnyValue(x => x.PwmStart)
            .Subscribe(x =>
            {
                IsPwmDO0Enabled_PwmStart = x.HasFlag(PwmOutputs.PwmDO0);
                IsPwmDO1Enabled_PwmStart = x.HasFlag(PwmOutputs.PwmDO1);
                IsPwmDO2Enabled_PwmStart = x.HasFlag(PwmOutputs.PwmDO2);
                IsPwmDO3Enabled_PwmStart = x.HasFlag(PwmOutputs.PwmDO3);
            });

        this.WhenAnyValue(x => x.PwmStop)
            .Subscribe(x =>
            {
                IsPwmDO0Enabled_PwmStop = x.HasFlag(PwmOutputs.PwmDO0);
                IsPwmDO1Enabled_PwmStop = x.HasFlag(PwmOutputs.PwmDO1);
                IsPwmDO2Enabled_PwmStop = x.HasFlag(PwmOutputs.PwmDO2);
                IsPwmDO3Enabled_PwmStop = x.HasFlag(PwmOutputs.PwmDO3);
            });

        this.WhenAnyValue(x => x.StartCameras)
            .Subscribe(x =>
            {
                IsCameraOutput0Enabled_StartCameras = x.HasFlag(CameraOutputs.CameraOutput0);
                IsCameraOutput1Enabled_StartCameras = x.HasFlag(CameraOutputs.CameraOutput1);
            });

        this.WhenAnyValue(x => x.StopCameras)
            .Subscribe(x =>
            {
                IsCameraOutput0Enabled_StopCameras = x.HasFlag(CameraOutputs.CameraOutput0);
                IsCameraOutput1Enabled_StopCameras = x.HasFlag(CameraOutputs.CameraOutput1);
            });

        this.WhenAnyValue(x => x.EnableServos)
            .Subscribe(x =>
            {
                IsServoOutput2Enabled_EnableServos = x.HasFlag(ServoOutputs.ServoOutput2);
                IsServoOutput3Enabled_EnableServos = x.HasFlag(ServoOutputs.ServoOutput3);
            });

        this.WhenAnyValue(x => x.DisableServos)
            .Subscribe(x =>
            {
                IsServoOutput2Enabled_DisableServos = x.HasFlag(ServoOutputs.ServoOutput2);
                IsServoOutput3Enabled_DisableServos = x.HasFlag(ServoOutputs.ServoOutput3);
            });

        this.WhenAnyValue(x => x.EnableEncoders)
            .Subscribe(x =>
            {
                IsEncoderPort2Enabled_EnableEncoders = x.HasFlag(EncoderInputs.EncoderPort2);
            });

        this.WhenAnyValue(x => x.Camera0Frame)
            .Subscribe(x =>
            {
                IsFrameAcquiredEnabled_Camera0Frame = x.HasFlag(FrameAcquired.FrameAcquired);
            });

        this.WhenAnyValue(x => x.Camera1Frame)
            .Subscribe(x =>
            {
                IsFrameAcquiredEnabled_Camera1Frame = x.HasFlag(FrameAcquired.FrameAcquired);
            });

        this.WhenAnyValue(x => x.EncoderReset)
            .Subscribe(x =>
            {
                IsEncoderPort2Enabled_EncoderReset = x.HasFlag(EncoderInputs.EncoderPort2);
            });




        DO0SetCommand = ReactiveCommand.CreateFromObservable(ExecuteDO0Set, canChangeConfig);
        DO0ClearCommand = ReactiveCommand.CreateFromObservable(ExecuteDO0Clear, canChangeConfig);
        DO1SetCommand = ReactiveCommand.CreateFromObservable(ExecuteDO1Set, canChangeConfig);
        DO1ClearCommand = ReactiveCommand.CreateFromObservable(ExecuteDO1Clear, canChangeConfig);
        DO2SetCommand = ReactiveCommand.CreateFromObservable(ExecuteDO2Set, canChangeConfig);
        DO2ClearCommand = ReactiveCommand.CreateFromObservable(ExecuteDO2Clear, canChangeConfig);
        DO3SetCommand = ReactiveCommand.CreateFromObservable(ExecuteDO3Set, canChangeConfig);
        DO3ClearCommand = ReactiveCommand.CreateFromObservable(ExecuteDO3Clear, canChangeConfig);

        DOPort0SetCommand = ReactiveCommand.CreateFromObservable(ExecuteDOPort0Set, canChangeConfig);
        DOPort0ClearCommand = ReactiveCommand.CreateFromObservable(ExecuteDOPort0Clear, canChangeConfig);
        DOPort1SetCommand = ReactiveCommand.CreateFromObservable(ExecuteDOPort1Set, canChangeConfig);
        DOPort1ClearCommand = ReactiveCommand.CreateFromObservable(ExecuteDOPort1Clear, canChangeConfig);
        DOPort2SetCommand = ReactiveCommand.CreateFromObservable(ExecuteDOPort2Set, canChangeConfig);
        DOPort2ClearCommand = ReactiveCommand.CreateFromObservable(ExecuteDOPort2Clear, canChangeConfig);
        SupplyPort0SetCommand = ReactiveCommand.CreateFromObservable(ExecuteSupplyPort0Set, canChangeConfig);
        SupplyPort0ClearCommand = ReactiveCommand.CreateFromObservable(ExecuteSupplyPort0Clear, canChangeConfig);
        SupplyPort1SetCommand = ReactiveCommand.CreateFromObservable(ExecuteSupplyPort1Set, canChangeConfig);
        SupplyPort1ClearCommand = ReactiveCommand.CreateFromObservable(ExecuteSupplyPort1Clear, canChangeConfig);
        SupplyPort2SetCommand = ReactiveCommand.CreateFromObservable(ExecuteSupplyPort2Set, canChangeConfig);
        SupplyPort2ClearCommand = ReactiveCommand.CreateFromObservable(ExecuteSupplyPort2Clear, canChangeConfig);
        DIOPort0SetCommand = ReactiveCommand.CreateFromObservable(ExecuteDIOPort0Set, canChangeConfig);
        DIOPort0ClearCommand = ReactiveCommand.CreateFromObservable(ExecuteDIOPort0Clear, canChangeConfig);
        DIOPort1SetCommand = ReactiveCommand.CreateFromObservable(ExecuteDIOPort1Set, canChangeConfig);
        DIOPort1ClearCommand = ReactiveCommand.CreateFromObservable(ExecuteDIOPort1Clear, canChangeConfig);
        DIOPort2SetCommand = ReactiveCommand.CreateFromObservable(ExecuteDIOPort2Set, canChangeConfig);
        DIOPort2ClearCommand = ReactiveCommand.CreateFromObservable(ExecuteDIOPort2Clear, canChangeConfig);

        Led0SetCommand = ReactiveCommand.CreateFromObservable(ExecuteLed0Set, canChangeConfig);
        Led0ClearCommand = ReactiveCommand.CreateFromObservable(ExecuteLed0Clear, canChangeConfig);
        Led1SetCommand = ReactiveCommand.CreateFromObservable(ExecuteLed1Set, canChangeConfig);
        Led1ClearCommand = ReactiveCommand.CreateFromObservable(ExecuteLed1Clear, canChangeConfig);
        Rgb0SetCommand = ReactiveCommand.CreateFromObservable(ExecuteRgb0Set, canChangeConfig);
        Rgb0ClearCommand = ReactiveCommand.CreateFromObservable(ExecuteRgb0Clear, canChangeConfig);
        Rgb1SetCommand = ReactiveCommand.CreateFromObservable(ExecuteRgb1Set, canChangeConfig);
        Rgb1ClearCommand = ReactiveCommand.CreateFromObservable(ExecuteRgb1Clear, canChangeConfig);


        //Apply Buttons
        PwmDO0StartCommand = ReactiveCommand.CreateFromObservable(ExecutePwmDO0Start, canChangeConfig);
        PwmDO0StopCommand = ReactiveCommand.CreateFromObservable(ExecutePwmDO0Stop, canChangeConfig);
        PwmDO1StartCommand = ReactiveCommand.CreateFromObservable(ExecutePwmDO1Start, canChangeConfig);
        PwmDO1StopCommand = ReactiveCommand.CreateFromObservable(ExecutePwmDO1Stop, canChangeConfig);
        PwmDO2StartCommand = ReactiveCommand.CreateFromObservable(ExecutePwmDO2Start, canChangeConfig);
        PwmDO2StopCommand = ReactiveCommand.CreateFromObservable(ExecutePwmDO2Stop, canChangeConfig);
        PwmDO3StartCommand = ReactiveCommand.CreateFromObservable(ExecutePwmDO3Start, canChangeConfig);
        PwmDO3StopCommand = ReactiveCommand.CreateFromObservable(ExecutePwmDO3Stop, canChangeConfig);

        ServoOutput2StartCommand = ReactiveCommand.CreateFromObservable(ExecuteServoOuput2Start, canChangeConfig);
        ServoOutput2StopCommand = ReactiveCommand.CreateFromObservable(ExecuteServoOuput2Stop, canChangeConfig);
        ServoOutput3StartCommand = ReactiveCommand.CreateFromObservable(ExecuteServoOuput3Start, canChangeConfig);
        ServoOutput3StopCommand = ReactiveCommand.CreateFromObservable(ExecuteServoOuput3Stop, canChangeConfig);

        Camera0StartCommand = ReactiveCommand.CreateFromObservable(ExecuteCamera0Start, canChangeConfig);
        Camera0StopCommand = ReactiveCommand.CreateFromObservable(ExecuteCamera0Stop, canChangeConfig);
        Camera1StartCommand = ReactiveCommand.CreateFromObservable(ExecuteCamera1Start, canChangeConfig);
        Camera1StopCommand = ReactiveCommand.CreateFromObservable(ExecuteCamera1Stop, canChangeConfig);


        //Rgb0ApplyConfigurationCommand = ReactiveCommand.CreateFromObservable(
        //    (bool parameter) => ExecuteRgb0ApplyConfiguration(parameter),
        //    canChangeConfig
        //);

        DOApplyConfigurationCommand = ReactiveCommand.CreateFromObservable<int, Unit>(
            doIndex => ExecuteDOApplyConfiguration(doIndex),
            canChangeConfig
            );

        //Port0ApplyConfigurationCommand = ReactiveCommand.CreateFromObservable<int, Unit>(
        //    port0Index => ExecutePort0ApplyConfiguration(port0Index),
        //    canChangeConfig
        //    );

        //Port1ApplyConfigurationCommand = ReactiveCommand.CreateFromObservable<int, Unit>(
        //    port1Index => ExecutePort1ApplyConfiguration(port1Index),
        //    canChangeConfig
        //    );
        //Port2ApplyConfigurationCommand = ReactiveCommand.CreateFromObservable<int, Unit>(
        //    port2Index => ExecutePort2ApplyConfiguration(port2Index),
        //    canChangeConfig
        //);

        CameraApplyConfigurationCommand = ReactiveCommand.CreateFromObservable<int, Unit>(
            cameraIndex => ExecuteCameraApplyConfiguration(cameraIndex),
            canChangeConfig
        );

        ServoApplyConfigurationCommand = ReactiveCommand.CreateFromObservable<int, Unit>(
            servoIndex => ExecuteServoApplyConfiguration(servoIndex),
            canChangeConfig
        );

        EncoderApplyConfigurationCommand = ReactiveCommand.CreateFromObservable(
            ExecuteEncoderApplyConfiguration,
            canChangeConfig
        );



        Rgb0ApplyConfigurationCommand = ReactiveCommand.CreateFromObservable(ExecuteRgb0ApplyConfiguration, canChangeConfig);
        Rgb1ApplyConfigurationCommand = ReactiveCommand.CreateFromObservable(ExecuteRgb1ApplyConfiguration, canChangeConfig);

        LedApplyConfigurationCommand = ReactiveCommand.CreateFromObservable<int, Unit>(
            ledIndex => ExecuteLedApplyConfiguration(ledIndex),
            canChangeConfig
        );

        SerialTimestampApplyConfigurationCommand = ReactiveCommand.CreateFromObservable(ExecuteSerialTimestampApplyConfiguration, canChangeConfig);

        
        SavePulseConfigDO0Command = ReactiveCommand.CreateFromObservable(ExecuteSavePulseConfigDO0, canChangeConfig);
        SavePulseConfigDO1Command = ReactiveCommand.CreateFromObservable(ExecuteSavePulseConfigDO1, canChangeConfig);
        SavePulseConfigDO2Command = ReactiveCommand.CreateFromObservable(ExecuteSavePulseConfigDO2, canChangeConfig);
        SavePulseConfigDO3Command = ReactiveCommand.CreateFromObservable(ExecuteSavePulseConfigDO3, canChangeConfig);

        SavePulseConfigDOPort0Command = ReactiveCommand.CreateFromObservable(ExecuteSavePulseConfigDOPort0, canChangeConfig);
        SavePulseConfigDOPort1Command = ReactiveCommand.CreateFromObservable(ExecuteSavePulseConfigDOPort1, canChangeConfig);
        SavePulseConfigDOPort2Command = ReactiveCommand.CreateFromObservable(ExecuteSavePulseConfigDOPort2, canChangeConfig);
        SavePulseConfigSupplyPort0Command = ReactiveCommand.CreateFromObservable(ExecuteSavePulseConfigSupplyPort0, canChangeConfig);
        SavePulseConfigSupplyPort1Command = ReactiveCommand.CreateFromObservable(ExecuteSavePulseConfigSupplyPort1, canChangeConfig);
        SavePulseConfigSupplyPort2Command = ReactiveCommand.CreateFromObservable(ExecuteSavePulseConfigSupplyPort2, canChangeConfig);

        SavePulseConfigRgb0Command = ReactiveCommand.CreateFromObservable(ExecuteSavePulseConfigRgb0, canChangeConfig);
        SavePulseConfigRgb1Command = ReactiveCommand.CreateFromObservable(ExecuteSavePulseConfigRgb1, canChangeConfig);
        SavePulseConfigLed0Command = ReactiveCommand.CreateFromObservable(ExecuteSavePulseConfigLed0, canChangeConfig);
        SavePulseConfigLed1Command = ReactiveCommand.CreateFromObservable(ExecuteSavePulseConfigLed1, canChangeConfig);

        SaveDirectionConfigCommand = ReactiveCommand.CreateFromObservable(ExecuteSaveDirectionConfig, canChangeConfig);
        SaveMimicConfigPort0IRCommand = ReactiveCommand.CreateFromObservable(ExecuteSaveMimicConfigPort0IR, canChangeConfig);
        SaveMimicConfigPort1IRCommand = ReactiveCommand.CreateFromObservable(ExecuteSaveMimicConfigPort1IR, canChangeConfig);
        SaveMimicConfigPort2IRCommand = ReactiveCommand.CreateFromObservable(ExecuteSaveMimicConfigPort2IR, canChangeConfig);
        SaveMimicConfigPort0ValveCommand = ReactiveCommand.CreateFromObservable(ExecuteSaveMimicConfigPort0Valve, canChangeConfig);
        SaveMimicConfigPort1ValveCommand = ReactiveCommand.CreateFromObservable(ExecuteSaveMimicConfigPort1Valve, canChangeConfig);
        SaveMimicConfigPort2ValveCommand = ReactiveCommand.CreateFromObservable(ExecuteSaveMimicConfigPort2Valve, canChangeConfig);

        // LED Configuration Commands
        SaveLedConfigCurrentLed0Command = ReactiveCommand.CreateFromObservable(ExecuteSaveLedConfigCurrentLed0, canChangeConfig);
        SaveLedConfigCurrentLed1Command = ReactiveCommand.CreateFromObservable(ExecuteSaveLedConfigCurrentLed1, canChangeConfig);

        // RGB Configuration Commands
        SaveRgbConfigColorRgb0Command = ReactiveCommand.CreateFromObservable(ExecuteSaveRgbConfigColorRgb0, canChangeConfig);
        SaveRgbConfigColorRgb1Command = ReactiveCommand.CreateFromObservable(ExecuteSaveRgbConfigColorRgb1, canChangeConfig);







        // force initial population of currently connected ports
        LoadUsbInformation();
    }
    private IObservable<Unit> ExecuteSavePulseConfigDO0()
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;
            await WriteAndLogAsync(
                value => _device.WriteOutputPulseEnableAsync(value),
                OutputPulseEnable,
                "OutputPulseEnable");
            await WriteAndLogAsync(
                value => _device.WritePulseDO0Async(value),
                PulseDO0,
                "PulseDO0");
        });
    }
    private IObservable<Unit> ExecuteSavePulseConfigDO1()
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;
            ushort pulseValue = 0;
            pulseValue = PulseDO1;

            // Enable
            await WriteAndLogAsync(
                value => _device.WriteOutputPulseEnableAsync(value),
                OutputPulseEnable,
                "OutputPulseEnable");

            // Duration
            await WriteAndLogAsync(
                value => _device.WritePulseDO1Async(value),
                pulseValue,
                "PulseDO1");
        });
    }
    private IObservable<Unit> ExecuteSavePulseConfigDO2()
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;
            await WriteAndLogAsync(
                value => _device.WriteOutputPulseEnableAsync(value),
                OutputPulseEnable,
                "OutputPulseEnable");
            await WriteAndLogAsync(
                value => _device.WritePulseDO2Async(value),
                PulseDO2,
                "PulseDO2");
        });
    }
    private IObservable<Unit> ExecuteSavePulseConfigDO3()
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;
            await WriteAndLogAsync(
                value => _device.WriteOutputPulseEnableAsync(value),
                OutputPulseEnable,
                "OutputPulseEnable");
            await WriteAndLogAsync(
                value => _device.WritePulseDO3Async(value),
                PulseDO3,
                "PulseDO3");
        });
    }

    // Port 0 DO pulse configuration
    private IObservable<Unit> ExecuteSavePulseConfigDOPort0()
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;

            await WriteAndLogAsync(
                value => _device.WriteOutputPulseEnableAsync(value),
                OutputPulseEnable,
                "OutputPulseEnable");
            await WriteAndLogAsync(
                value => _device.WritePulseDOPort0Async(value),
                PulseDOPort0,
                "PulseDOPort0");
        });
    }

    // Port 1 DO pulse configuration
    private IObservable<Unit> ExecuteSavePulseConfigDOPort1()
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;

            await WriteAndLogAsync(
                value => _device.WriteOutputPulseEnableAsync(value),
                OutputPulseEnable,
                "OutputPulseEnable");
            await WriteAndLogAsync(
                value => _device.WritePulseDOPort1Async(value),
                PulseDOPort1,
                "PulseDOPort1");
        });
    }

    // Port 2 DO pulse configuration
    private IObservable<Unit> ExecuteSavePulseConfigDOPort2()
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;

            await WriteAndLogAsync(
                value => _device.WriteOutputPulseEnableAsync(value),
                OutputPulseEnable,
                "OutputPulseEnable");
            await WriteAndLogAsync(
                value => _device.WritePulseDOPort2Async(value),
                PulseDOPort2,
                "PulseDOPort2");
        });
    }

    // Port 0 12V Supply pulse configuration
    private IObservable<Unit> ExecuteSavePulseConfigSupplyPort0()
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;

            await WriteAndLogAsync(
                value => _device.WriteOutputPulseEnableAsync(value),
                OutputPulseEnable,
                "OutputPulseEnable");
            await WriteAndLogAsync(
                value => _device.WritePulseSupplyPort0Async(value),
                PulseSupplyPort0,
                "PulseSupplyPort0");
        });
    }

    // Port 1 12V Supply pulse configuration
    private IObservable<Unit> ExecuteSavePulseConfigSupplyPort1()
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;

            await WriteAndLogAsync(
                value => _device.WriteOutputPulseEnableAsync(value),
                OutputPulseEnable,
                "OutputPulseEnable");
            await WriteAndLogAsync(
                value => _device.WritePulseSupplyPort1Async(value),
                PulseSupplyPort1,
                "PulseSupplyPort1");
        });
    }

    // Port 2 12V Supply pulse configuration
    private IObservable<Unit> ExecuteSavePulseConfigSupplyPort2()
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;

            await WriteAndLogAsync(
                value => _device.WriteOutputPulseEnableAsync(value),
                OutputPulseEnable,
                "OutputPulseEnable");
            await WriteAndLogAsync(
                value => _device.WritePulseSupplyPort2Async(value),
                PulseSupplyPort2,
                "PulseSupplyPort2");
        });
    }

    // RGB 0 pulse configuration
    private IObservable<Unit> ExecuteSavePulseConfigRgb0()
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;

            await WriteAndLogAsync(
                value => _device.WriteOutputPulseEnableAsync(value),
                OutputPulseEnable,
                "OutputPulseEnable");
            await WriteAndLogAsync(
                value => _device.WritePulseRgb0Async(value),
                PulseRgb0,
                "PulseRgb0");
        });
    }

    // RGB 1 pulse configuration
    private IObservable<Unit> ExecuteSavePulseConfigRgb1()
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;

            await WriteAndLogAsync(
                value => _device.WriteOutputPulseEnableAsync(value),
                OutputPulseEnable,
                "OutputPulseEnable");
            await WriteAndLogAsync(
                value => _device.WritePulseRgb1Async(value),
                PulseRgb1,
                "PulseRgb1");
        });
    }

    // LED 0 pulse configuration
    private IObservable<Unit> ExecuteSavePulseConfigLed0()
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;

            await WriteAndLogAsync(
                value => _device.WriteOutputPulseEnableAsync(value),
                OutputPulseEnable,
                "OutputPulseEnable");
            await WriteAndLogAsync(
                value => _device.WritePulseLed0Async(value),
                PulseLed0,
                "PulseLed0");
        });
    }

    // LED 1 pulse configuration
    private IObservable<Unit> ExecuteSavePulseConfigLed1()
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;

            await WriteAndLogAsync(
                value => _device.WriteOutputPulseEnableAsync(value),
                OutputPulseEnable,
                "OutputPulseEnable");
            await WriteAndLogAsync(
                value => _device.WritePulseLed1Async(value),
                PulseLed1,
                "PulseLed1");
        });
    }

    // Port DIO Direction configuration
    private IObservable<Unit> ExecuteSaveDirectionConfig()
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;

            await WriteAndLogAsync(
                value => _device.WritePortDIODirectionAsync(value),
                PortDIODirection,
                "PortDIODirection");
        });
    }

    // Port 0 IR Mimic configuration
    private IObservable<Unit> ExecuteSaveMimicConfigPort0IR()
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;

            await WriteAndLogAsync(
                value => _device.WriteMimicPort0IRAsync(value),
                MimicPort0IR,
                "MimicPort0IR");
        });
    }

    // Port 1 IR Mimic configuration
    private IObservable<Unit> ExecuteSaveMimicConfigPort1IR()
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;

            await WriteAndLogAsync(
                value => _device.WriteMimicPort1IRAsync(value),
                MimicPort1IR,
                "MimicPort1IR");
        });
    }

    // Port 2 IR Mimic configuration
    private IObservable<Unit> ExecuteSaveMimicConfigPort2IR()
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;

            await WriteAndLogAsync(
                value => _device.WriteMimicPort2IRAsync(value),
                MimicPort2IR,
                "MimicPort2IR");
        });
    }

    // Port 0 Valve Mimic configuration
    private IObservable<Unit> ExecuteSaveMimicConfigPort0Valve()
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;

            await WriteAndLogAsync(
                value => _device.WriteMimicPort0ValveAsync(value),
                MimicPort0Valve,
                "MimicPort0Valve");
        });
    }

    // Port 1 Valve Mimic configuration
    private IObservable<Unit> ExecuteSaveMimicConfigPort1Valve()
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;

            await WriteAndLogAsync(
                value => _device.WriteMimicPort1ValveAsync(value),
                MimicPort1Valve,
                "MimicPort1Valve");
        });
    }

    // Port 2 Valve Mimic configuration
    private IObservable<Unit> ExecuteSaveMimicConfigPort2Valve()
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;

            await WriteAndLogAsync(
                value => _device.WriteMimicPort2ValveAsync(value),
                MimicPort2Valve,
                "MimicPort2Valve");
        });
    }

    // LED 0 Configuration
    private IObservable<Unit> ExecuteSaveLedConfigCurrentLed0()
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;

            // Write LED current settings
            await WriteAndLogAsync(
                value => _device.WriteLed0CurrentAsync(value),
                Led0Current,
                "Led0Current");

            await WriteAndLogAsync(
                value => _device.WriteLed0MaxCurrentAsync(value),
                Led0MaxCurrent,
                "Led0MaxCurrent");

            // Write pulse configuration
            await WriteAndLogAsync(
                value => _device.WritePulseLed0Async(value),
                PulseLed0,
                "PulseLed0");

            // Write pulse enable state
            await WriteAndLogAsync(
                value => _device.WriteOutputPulseEnableAsync(value),
                OutputPulseEnable,
                "OutputPulseEnable");
        });
    }

    // LED 1 Configuration
    private IObservable<Unit> ExecuteSaveLedConfigCurrentLed1()
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;

            // Write LED current settings
            await WriteAndLogAsync(
                value => _device.WriteLed1CurrentAsync(value),
                Led1Current,
                "Led1Current");

            await WriteAndLogAsync(
                value => _device.WriteLed1MaxCurrentAsync(value),
                Led1MaxCurrent,
                "Led1MaxCurrent");

            // Write pulse configuration
            await WriteAndLogAsync(
                value => _device.WritePulseLed1Async(value),
                PulseLed1,
                "PulseLed1");

            // Write pulse enable state
            await WriteAndLogAsync(
                value => _device.WriteOutputPulseEnableAsync(value),
                OutputPulseEnable,
                "OutputPulseEnable");
        });
    }

    // RGB 0 Configuration
    private IObservable<Unit> ExecuteSaveRgbConfigColorRgb0()
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;

            // Update RGB values from adapter
            var c0 = Rgb0Adapter.Color;
            Rgb0 = new RgbPayload(c0.R, c0.G, c0.B);

            // Write RGB color
            await WriteAndLogAsync(
                value => _device.WriteRgb0Async(value),
                Rgb0,
                "Rgb0");

            // Write pulse configuration
            await WriteAndLogAsync(
                value => _device.WritePulseRgb0Async(value),
                PulseRgb0,
                "PulseRgb0");

            // Write pulse enable state
            await WriteAndLogAsync(
                value => _device.WriteOutputPulseEnableAsync(value),
                OutputPulseEnable,
                "OutputPulseEnable");

            // Update RgbAll register to keep it in sync
            var c1 = Rgb1Adapter.Color;
            RgbAll = new RgbAllPayload(
                c0.R, c0.G, c0.B,
                c1.R, c1.G, c1.B);

            await WriteAndLogAsync(
                value => _device.WriteRgbAllAsync(value),
                RgbAll,
                "RgbAll");
        });
    }

    // RGB 1 Configuration
    private IObservable<Unit> ExecuteSaveRgbConfigColorRgb1()
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;

            // Update RGB values from adapter
            var c1 = Rgb1Adapter.Color;
            Rgb1 = new RgbPayload(c1.R, c1.G, c1.B);

            // Write RGB color
            await WriteAndLogAsync(
                value => _device.WriteRgb1Async(value),
                Rgb1,
                "Rgb1");

            // Write pulse configuration
            await WriteAndLogAsync(
                value => _device.WritePulseRgb1Async(value),
                PulseRgb1,
                "PulseRgb1");

            // Write pulse enable state
            await WriteAndLogAsync(
                value => _device.WriteOutputPulseEnableAsync(value),
                OutputPulseEnable,
                "OutputPulseEnable");

            // Update RgbAll register to keep it in sync
            var c0 = Rgb0Adapter.Color;
            RgbAll = new RgbAllPayload(
                c0.R, c0.G, c0.B,
                c1.R, c1.G, c1.B);

            await WriteAndLogAsync(
                value => _device.WriteRgbAllAsync(value),
                RgbAll,
                "RgbAll");
        });
    }




    private IObservable<Unit> ExecuteSerialTimestampApplyConfiguration()
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;

            await WriteAndLogAsync(
                value => _device.WriteEnableSerialTimestampAsync(value),
                EnableSerialTimestamp,
                "EnableSerialTimestamp");
        });
    }

    private IObservable<Unit> ExecuteRgb0ApplyConfiguration()
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;

            await WriteAndLogAsync(
                value => _device.WriteRgbAllAsync(value),
                RgbAll,
                "RgbAll");

            var c0 = Rgb0Adapter.Color;
            var c1 = Rgb1Adapter.Color;
            RgbAll = new RgbAllPayload(
                c0.R, c0.G, c0.B,
                c1.R, c1.G, c1.B);

            Rgb0 = new RgbPayload(c0.R, c0.G, c0.B);
            Rgb1 = new RgbPayload(c1.R, c1.G, c1.B);
            await WriteAndLogAsync(
                value => _device.WriteRgb0Async(value),
                Rgb0,
                "Rgb0");
            await WriteAndLogAsync(
                value => _device.WritePulseRgb0Async(value),
                PulseRgb0,
                "PulseRgb0");
            await WriteAndLogAsync(
                value => _device.WriteOutputSetAsync(value),
                DigitalOutputs.Rgb0,
                "OutputSet");
            await WriteAndLogAsync(
                value => _device.WriteOutputClearAsync(value),
                OutputClear,
                "OutputClear");
        });
    }

    private IObservable<Unit> ExecuteRgb1ApplyConfiguration()
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;

            // Update Rgb1 and RgbAll from adapter
            var c0 = Rgb0Adapter.Color;
            var c1 = Rgb1Adapter.Color;
            RgbAll = new RgbAllPayload(
                c0.R, c0.G, c0.B,
                c1.R, c1.G, c1.B);

            Rgb0 = new RgbPayload(c0.R, c0.G, c0.B);
            Rgb1 = new RgbPayload(c1.R, c1.G, c1.B);

            // Write Rgb1 and related registers
            await WriteAndLogAsync(
                value => _device.WriteRgb1Async(value),
                Rgb1,
                "Rgb1");
            await WriteAndLogAsync(
                value => _device.WritePulseRgb1Async(value),
                PulseRgb1,
                "PulseRgb1");
            await WriteAndLogAsync(
                value => _device.WriteOutputSetAsync(value),
                DigitalOutputs.Rgb1,
                "OutputSet");
            await WriteAndLogAsync(
                value => _device.WriteOutputClearAsync(value),
                OutputClear,
                "OutputClear");
        });
    }

    private IObservable<Unit> ExecuteDOApplyConfiguration(int doIndex)
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;

            DigitalOutputs doOutput;
            ushort pulseValue = 0;
            ushort pwmFrequency = 0;
            byte pwmDutyCycle = 0;
            PwmOutputs pwmOutput = 0;

            // Enable pulse
            await WriteAndLogAsync(
                value => _device.WriteOutputPulseEnableAsync(value),
                OutputPulseEnable,
                "OutputPulseEnable");

            switch (doIndex)
            {
                case 0:
                    doOutput = DigitalOutputs.DO0;
                    pulseValue = PulseDO0;
                    pwmFrequency = PwmFrequencyDO0;
                    pwmDutyCycle = PwmDutyCycleDO0;
                    pwmOutput = PwmOutputs.PwmDO0;
                    await WriteAndLogAsync(
                        value => _device.WritePulseDO0Async(value),
                        pulseValue,
                        "PulseDO0");
                    await WriteAndLogAsync(
                        value => _device.WritePwmFrequencyDO0Async(value),
                        pwmFrequency,
                        "PwmFrequencyDO0");
                    await WriteAndLogAsync(
                        value => _device.WritePwmDutyCycleDO0Async(value),
                        pwmDutyCycle,
                        "PwmDutyCycleDO0");
                    await WriteAndLogAsync(
                        value => _device.WritePwmStartAsync(value),
                        PwmOutputs.PwmDO0,
                        "PwmStart");
                    await WriteAndLogAsync(
                        value => _device.WritePwmStartAsync(value),
                        pwmOutput,
                        "PwmStop");
                    break;
                case 1:
                    doOutput = DigitalOutputs.DO1;
                    pulseValue = PulseDO1;
                    pwmFrequency = PwmFrequencyDO1;
                    pwmDutyCycle = PwmDutyCycleDO1;
                    //pwmOutput = PwmOutputs.PwmDO1;
                    await WriteAndLogAsync(
                        value => _device.WritePulseDO1Async(value),
                        pulseValue,
                        "PulseDO1");
                    await WriteAndLogAsync(
                        value => _device.WritePwmFrequencyDO1Async(value),
                        pwmFrequency,
                        "PwmFrequencyDO1");
                    await WriteAndLogAsync(
                        value => _device.WritePwmDutyCycleDO1Async(value),
                        pwmDutyCycle,
                        "PwmDutyCycleDO1");
                    await WriteAndLogAsync(
                        value => _device.WritePwmStartAsync(value),
                        PwmOutputs.PwmDO1,
                        "PwmStart");
                    await WriteAndLogAsync(
                        value => _device.WritePwmStartAsync(value),
                        PwmOutputs.PwmDO1,
                        "PwmStop");
                    break;
                case 2:
                    doOutput = DigitalOutputs.DO2;
                    pulseValue = PulseDO2;
                    pwmFrequency = PwmFrequencyDO2;
                    pwmDutyCycle = PwmDutyCycleDO2;
                    //pwmOutput = PwmOutputs.PwmDO2;
                    await WriteAndLogAsync(
                        value => _device.WritePulseDO2Async(value),
                        pulseValue,
                        "PulseDO2");
                    await WriteAndLogAsync(
                        value => _device.WritePwmFrequencyDO2Async(value),
                        pwmFrequency,
                        "PwmFrequencyDO2");
                    await WriteAndLogAsync(
                        value => _device.WritePwmDutyCycleDO2Async(value),
                        pwmDutyCycle,
                        "PwmDutyCycleDO2");
                    await WriteAndLogAsync(
                        value => _device.WritePwmStartAsync(value),
                        PwmOutputs.PwmDO2,
                        "PwmStart");
                    await WriteAndLogAsync(
                        value => _device.WritePwmStartAsync(value),
                        PwmOutputs.PwmDO2,
                        "PwmStop");
                    break;
                case 3:
                    doOutput = DigitalOutputs.DO3;
                    pulseValue = PulseDO3;
                    pwmFrequency = PwmFrequencyDO3;
                    pwmDutyCycle = PwmDutyCycleDO3;
                    //pwmOutput = PwmOutputs.PwmDO3;
                    await WriteAndLogAsync(
                        value => _device.WritePulseDO3Async(value),
                        pulseValue,
                        "PulseDO3");
                    await WriteAndLogAsync(
                        value => _device.WritePwmFrequencyDO3Async(value),
                        pwmFrequency,
                        "PwmFrequencyDO3");
                    await WriteAndLogAsync(
                        value => _device.WritePwmDutyCycleDO3Async(value),
                        pwmDutyCycle,
                        "PwmDutyCycleDO3");
                    await WriteAndLogAsync(
                        value => _device.WritePwmStartAsync(value),
                        PwmOutputs.PwmDO3,
                        "PwmStart");
                    await WriteAndLogAsync(
                        value => _device.WritePwmStartAsync(value),
                        PwmOutputs.PwmDO3,
                        "PwmStop");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(doIndex), "DO index must be 0-3.");
            }

            //// Start PWM
            //await WriteAndLogAsync(
            //    value => _device.WritePwmStartAsync(value),
            //    PwmOutputs.PwmDO0,
            //    "PwmStart");

            // Optionally, you may want to stop PWM as well, depending on my logic lol not sure how this is going
            await WriteAndLogAsync(
                value => _device.WritePwmStopAsync(value),
                PwmStop,
                "PwmStop");
            // Set the output
            await WriteAndLogAsync(
                value => _device.WriteOutputSetAsync(value),
                doOutput,
                "OutputSet");

            // Clear the output (if needed, or use OutputClear property)
            await WriteAndLogAsync(
                value => _device.WriteOutputClearAsync(value),
                OutputClear,
                "OutputClear");
        });
    }

    private IObservable<Unit> ExecuteLedApplyConfiguration(int ledIndex)
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;

            if (ledIndex == 0)
            {
                // Write current and max current for LED 0
                await WriteAndLogAsync(
                    value => _device.WriteLed0CurrentAsync(value),
                    Led0Current,
                    "Led0Current");

                await WriteAndLogAsync(
                    value => _device.WriteLed0MaxCurrentAsync(value),
                    Led0MaxCurrent,
                    "Led0MaxCurrent");

                // Write pulse duration for LED 0
                await WriteAndLogAsync(
                    value => _device.WritePulseLed0Async(value),
                    PulseLed0,
                    "PulseLed0");

                // Optionally, set/clear/toggle LED 0 in the digital outputs if needed
                await WriteAndLogAsync(
                    value => _device.WriteOutputSetAsync(value),
                    DigitalOutputs.Led0,
                    "OutputSet");

                await WriteAndLogAsync(
                    value => _device.WriteOutputClearAsync(value),
                    OutputClear,
                    "OutputClear");

                await WriteAndLogAsync(
                    value => _device.WriteOutputPulseEnableAsync(value),
                    OutputPulseEnable,
                    "OutputPulseEnable");
            }
            else if (ledIndex == 1)
            {
                // Write current and max current for LED 1
                await WriteAndLogAsync(
                    value => _device.WriteLed1CurrentAsync(value),
                    Led1Current,
                    "Led1Current");

                await WriteAndLogAsync(
                    value => _device.WriteLed1MaxCurrentAsync(value),
                    Led1MaxCurrent,
                    "Led1MaxCurrent");

                // Write pulse duration for LED 1
                await WriteAndLogAsync(
                    value => _device.WritePulseLed1Async(value),
                    PulseLed1,
                    "PulseLed1");

                // Optionally, set/clear/toggle LED 1 in the digital outputs if needed
                await WriteAndLogAsync(
                    value => _device.WriteOutputSetAsync(value),
                    DigitalOutputs.Led1,
                    "OutputSet");

                await WriteAndLogAsync(
                    value => _device.WriteOutputClearAsync(value),
                    OutputClear,
                    "OutputClear");

                await WriteAndLogAsync(
                    value => _device.WriteOutputPulseEnableAsync(value),
                    OutputPulseEnable,
                    "OutputPulseEnable");
            }

        });
    }


    private IObservable<Unit> ExecutePort0ApplyConfiguration(int port0Index)
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;

            await WriteAndLogAsync(
                value => _device.WriteOutputPulseEnableAsync(value),
                OutputPulseEnable,
                "OutputPulseEnable");

            switch (port0Index)
            {
                case 0: // DO
                    await WriteAndLogAsync(
                        value => _device.WritePulseDOPort0Async(value),
                        PulseDOPort0,
                        "PulseDOPort0");
                    await WriteAndLogAsync(
                        value => _device.WriteOutputSetAsync(value),
                        DigitalOutputs.DOPort0,
                        "OutputSet");
                    await WriteAndLogAsync(
                        value => _device.WriteOutputClearAsync(value),
                        OutputClear,
                        "OutputClear");
                    break;

                case 1: // 12 V
                    await WriteAndLogAsync(
                        value => _device.WritePulseSupplyPort0Async(value),
                        PulseSupplyPort0,
                        "PulseSupplyPort0");

                    await WriteAndLogAsync(
                        value => _device.WriteOutputSetAsync(value),
                        DigitalOutputs.SupplyPort0,
                        "OutputSet");

                    //await WriteAndLogAsync(
                    //    value => _device.WriteOutputClearAsync(value),
                    //    DigitalOutputs.SupplyPort0,
                    //    "OutputClear");
                    await WriteAndLogAsync(
                        value => _device.WriteOutputClearAsync(value),
                        OutputClear,
                        "OutputClear");
                    break;

                case 2: // DIO
                    await WriteAndLogAsync(
                        value => _device.WritePortDIODirectionAsync(value),
                        PortDIODirection,
                        "PortDIODirection");
                    await WriteAndLogAsync(
                        value => _device.WritePortDIOSetAsync(value),
                        PortDIOSet,
                        "PortDIOSet");
                    await WriteAndLogAsync(
                        value => _device.WritePortDIOClearAsync(value),
                        PortDIOClear,
                        "PortDIOClear");
                    break;

                case 3: // DI
                    await WriteAndLogAsync(
                       value => _device.WriteMimicPort0IRAsync(value),
                       MimicPort0IR,
                       "MimicPort0IR");
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(port0Index), "Port0 index must be 0 (DO), 1 (12V), or 2 (DIO).");
            }



            
        });
    }

    private IObservable<Unit> ExecutePort1ApplyConfiguration(int port1Index)
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;

            await WriteAndLogAsync(
                value => _device.WriteOutputPulseEnableAsync(value),
                OutputPulseEnable,
                "OutputPulseEnable");

            switch (port1Index)
            {
                case 0: // DO
                    await WriteAndLogAsync(
                        value => _device.WritePulseDOPort1Async(value),
                        PulseDOPort1,
                        "PulseDOPort1");

                    await WriteAndLogAsync(
                        value => _device.WriteOutputSetAsync(value),
                        DigitalOutputs.DOPort1,
                        "OutputSet");

                    await WriteAndLogAsync(
                        value => _device.WriteOutputClearAsync(value),
                        OutputClear,
                        "OutputClear");
                    break;

                case 1: // 12 V
                    await WriteAndLogAsync(
                        value => _device.WritePulseSupplyPort1Async(value),
                        PulseSupplyPort1,
                        "PulseSupplyPort1");

                    await WriteAndLogAsync(
                        value => _device.WriteOutputSetAsync(value),
                        DigitalOutputs.SupplyPort1,
                        "OutputSet");

                    await WriteAndLogAsync(
                        value => _device.WriteOutputClearAsync(value),
                        OutputClear,
                        "OutputClear");
                    break;

                case 2: // DIO
                    await WriteAndLogAsync(
                        value => _device.WritePortDIODirectionAsync(value),
                        PortDIODirection,
                        "PortDIODirection");
                    await WriteAndLogAsync(
                        value => _device.WritePortDIOSetAsync(value),
                        PortDIOSet,
                        "PortDIOSet");
                    await WriteAndLogAsync(
                        value => _device.WritePortDIOClearAsync(value),
                        PortDIOClear,
                        "PortDIOClear");
                    break;

                    case 3:
                    await WriteAndLogAsync(
                        value => _device.WriteMimicPort1IRAsync(value),
                        MimicPort1IR,
                        "MimicPort1IR");
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(port1Index), "Port1 index must be 0 (DO), 1 (12V), or 2 (DIO).");
            }
        });
    }

    private IObservable<Unit> ExecutePort2ApplyConfiguration(int port2Index)
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;

            await WriteAndLogAsync(
                value => _device.WriteOutputPulseEnableAsync(value),
                OutputPulseEnable,
                "OutputPulseEnable");

            switch (port2Index)
            {
                case 0: // DO
                    await WriteAndLogAsync(
                        value => _device.WritePulseDOPort2Async(value),
                        PulseDOPort2,
                        "PulseDOPort2");
                    await WriteAndLogAsync(
                        value => _device.WriteOutputSetAsync(value),
                        DigitalOutputs.DOPort2,
                        "OutputSet");

                    await WriteAndLogAsync(
                        value => _device.WriteOutputClearAsync(value),
                        OutputClear,
                        "OutputClear");
                    break;

                case 1: // 12 V
                    await WriteAndLogAsync(
                        value => _device.WritePulseSupplyPort2Async(value),
                        PulseSupplyPort2,
                        "PulseSupplyPort2");

                    await WriteAndLogAsync(
                        value => _device.WriteOutputSetAsync(value),
                        DigitalOutputs.SupplyPort2,
                        "OutputSet");

                    await WriteAndLogAsync(
                        value => _device.WriteOutputClearAsync(value),
                        OutputClear,
                        "OutputClear");
                    break;

                case 2: // DIO
                    await WriteAndLogAsync(
                        value => _device.WritePortDIODirectionAsync(value),
                        PortDIODirection,
                        "PortDIODirection");
                    await WriteAndLogAsync(
                        value => _device.WritePortDIOSetAsync(value),
                        PortDIOSet,
                        "PortDIOSet");
                    await WriteAndLogAsync(
                        value => _device.WritePortDIOClearAsync(value),
                        PortDIOClear,
                        "PortDIOClear");
                    break;

                case 3: // DI
                    await WriteAndLogAsync(
                        value => _device.WriteMimicPort2IRAsync(value),
                        MimicPort2IR,
                        "MimicPort2IR");
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(port2Index), "Port2 index must be 0 (DO), 1 (12V), or 2 (DIO).");
            }
        });
    }

    private IObservable<Unit> ExecuteCameraApplyConfiguration(int cameraIndex)
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;

            switch (cameraIndex)
            {
                case 0: // Camera 0
                    await WriteAndLogAsync(
                       value => _device.WriteCamera0FrequencyAsync(value),
                       Camera0Frequency,
                       "Camera0Frequency");

                    await WriteAndLogAsync(
                      value => _device.WriteStartCamerasAsync(value),
                      StartCameras,
                      "StartCameras");

                    await WriteAndLogAsync(
                      value => _device.WriteStopCamerasAsync(value),
                      StopCameras,
                      "StopCameras");

                    break;

                case 1: // Camera 1
                    await WriteAndLogAsync(
                       value => _device.WriteCamera0FrequencyAsync(value),
                       Camera1Frequency,
                       "Camera1Frequency");

                    await WriteAndLogAsync(
                      value => _device.WriteStartCamerasAsync(value),
                      StartCameras,
                      "StartCameras");

                    await WriteAndLogAsync(
                      value => _device.WriteStopCamerasAsync(value),
                      StopCameras,
                      "StopCameras");
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(cameraIndex), "Port0 index must be 0 (DO), 1 (12V), or 2 (DIO).");
            }




        });
    }

    private IObservable<Unit> ExecuteServoApplyConfiguration(int servoIndex)
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;

            switch (servoIndex)
            {
                case 0: // Servo 2
                    await WriteAndLogAsync(
                       value => _device.WriteServoMotor2PeriodAsync(value),
                       ServoMotor2Period,
                       "ServoMotor2Period");

                    await WriteAndLogAsync(
                   value => _device.WriteServoMotor2PulseAsync(value),
                   ServoMotor2Pulse,
                       "ServoMotor2Pulse");

                    await WriteAndLogAsync(
                       value => _device.WriteEnableServosAsync(value),
                       EnableServos,
                       "EnableServos");
                    await WriteAndLogAsync(
                        value => _device.WriteDisableServosAsync(value),
                        DisableServos,
                        "DisableServos");

                    break;

                case 1: // Servo 3
                    await WriteAndLogAsync(
                       value => _device.WriteServoMotor3PeriodAsync(value),
                       ServoMotor3Period,
                       "ServoMotor3Period");

                    await WriteAndLogAsync(
                   value => _device.WriteServoMotor3PulseAsync(value),
                   ServoMotor3Pulse,
                       "ServoMotor3Pulse");

                    await WriteAndLogAsync(
                       value => _device.WriteEnableServosAsync(value),
                       EnableServos,
                       "EnableServos");
                    await WriteAndLogAsync(
                        value => _device.WriteDisableServosAsync(value),
                        DisableServos,
                        "DisableServos");
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(servoIndex), "Port0 index must be 0 (DO), 1 (12V), or 2 (DIO).");
            }




        });
    }

    public IObservable<Unit> ExecuteEncoderApplyConfiguration()
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;
            await WriteAndLogAsync(
                value => _device.WriteEnableEncodersAsync(value),
                EnableEncoders,
                "EnableEncoders");
            await WriteAndLogAsync(
                value => _device.WriteEncoderModeAsync(value),
                EncoderMode,
                "EncoderMode");

            await WriteAndLogAsync(
               value => _device.WriteEncoderResetAsync(value),
               EncoderReset,
               "EncoderReset");

            IsEncoderPort2Enabled_EncoderReset = false;
        });
        
    }

    private IObservable<Unit> ExecuteDO0Set()
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;

            IsDO0Enabled_OutputSet = true;
            IsDO0Enabled_OutputClear = false;

            await WriteAndLogAsync(
                value => _device.WriteOutputSetAsync(value),
                DigitalOutputs.DO0, 
                "OutputSet");
        });
    }
    private IObservable<Unit> ExecuteDO0Clear()
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;

            IsDO0Enabled_OutputSet = false;
            IsDO0Enabled_OutputClear = true;

            await WriteAndLogAsync(
                value => _device.WriteOutputClearAsync(value),
                DigitalOutputs.DO0,
                "OutputClear");
        });

    }

    private IObservable<Unit> ExecuteDO1Set()
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;

            IsDO1Enabled_OutputSet = true;
            IsDO1Enabled_OutputClear = false;

            await WriteAndLogAsync(
                value => _device.WriteOutputSetAsync(value),
                DigitalOutputs.DO1,
                "OutputSet");
        });
    }
    private IObservable<Unit> ExecuteDO1Clear()
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;

            IsDO1Enabled_OutputSet = false;
            IsDO1Enabled_OutputClear = true;

            await WriteAndLogAsync(
                value => _device.WriteOutputClearAsync(value),
                DigitalOutputs.DO1,
                "OutputClear");
        });

    }

    private IObservable<Unit> ExecuteDO2Set()
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;

            IsDO2Enabled_OutputSet = true;
            IsDO2Enabled_OutputClear = false;

            await WriteAndLogAsync(
                value => _device.WriteOutputSetAsync(value),
                DigitalOutputs.DO2,
                "OutputSet");
        });
    }
    private IObservable<Unit> ExecuteDO2Clear()
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;

            IsDO2Enabled_OutputSet = false;
            IsDO2Enabled_OutputClear = true;

            await WriteAndLogAsync(
                value => _device.WriteOutputClearAsync(value),
                DigitalOutputs.DO2,
                "OutputClear");
        });

    }

    private IObservable<Unit> ExecuteDO3Set()
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;

            IsDO3Enabled_OutputSet = true;
            IsDO3Enabled_OutputClear = false;

            await WriteAndLogAsync(
                value => _device.WriteOutputSetAsync(value),
                DigitalOutputs.DO3,
                "OutputSet");
        });
    }
    private IObservable<Unit> ExecuteDO3Clear()
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;

            IsDO3Enabled_OutputSet = false;
            IsDO3Enabled_OutputClear = true;

            await WriteAndLogAsync(
                value => _device.WriteOutputClearAsync(value),
                DigitalOutputs.DO3,
                "OutputClear");
        });

    }

    private IObservable<Unit> ExecuteDOPort0Set()
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;

            IsDOPort0Enabled_OutputSet = true;
            IsDOPort0Enabled_OutputClear = false;

            await WriteAndLogAsync(
                value => _device.WriteOutputSetAsync(value),
                DigitalOutputs.DOPort0,
                "OutputSet");
        });
    }

    private IObservable<Unit> ExecuteDOPort0Clear()
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;

            IsDOPort0Enabled_OutputSet = false;
            IsDOPort0Enabled_OutputClear = true;

            await WriteAndLogAsync(
                value => _device.WriteOutputClearAsync(value),
                DigitalOutputs.DOPort0,
                "OutputClear");
        });
    }

    private IObservable<Unit> ExecuteDOPort1Set()
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;

            IsDOPort1Enabled_OutputSet = true;
            IsDOPort1Enabled_OutputClear = false;

            await WriteAndLogAsync(
                value => _device.WriteOutputSetAsync(value),
                DigitalOutputs.DOPort1,
                "OutputSet");
        });
    }

    private IObservable<Unit> ExecuteDOPort1Clear()
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;

            IsDOPort1Enabled_OutputSet = false;
            IsDOPort1Enabled_OutputClear = true;

            await WriteAndLogAsync(
                value => _device.WriteOutputClearAsync(value),
                DigitalOutputs.DOPort1,
                "OutputClear");
        });
    }

    private IObservable<Unit> ExecuteDOPort2Set()
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;

            IsDOPort2Enabled_OutputSet = true;
            IsDOPort2Enabled_OutputClear = false;

            await WriteAndLogAsync(
                value => _device.WriteOutputSetAsync(value),
                DigitalOutputs.DOPort2,
                "OutputSet");
        });
    }

    private IObservable<Unit> ExecuteDOPort2Clear()
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;

            IsDOPort2Enabled_OutputSet = false;
            IsDOPort2Enabled_OutputClear = true;

            await WriteAndLogAsync(
                value => _device.WriteOutputClearAsync(value),
                DigitalOutputs.DOPort2,
                "OutputClear");
        });
    }

    private IObservable<Unit> ExecuteSupplyPort0Set()
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;

            IsSupplyPort0Enabled_OutputSet = true;
            IsSupplyPort0Enabled_OutputClear = false;

            await WriteAndLogAsync(
                value => _device.WriteOutputSetAsync(value),
                DigitalOutputs.SupplyPort0,
                "OutputSet");
        });
    }

    private IObservable<Unit> ExecuteSupplyPort0Clear()
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;

            IsSupplyPort0Enabled_OutputSet = false;
            IsSupplyPort0Enabled_OutputClear = true;

            await WriteAndLogAsync(
                value => _device.WriteOutputClearAsync(value),
                DigitalOutputs.SupplyPort0,
                "OutputClear");
        });
    }

    private IObservable<Unit> ExecuteSupplyPort1Set()
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;

            IsSupplyPort1Enabled_OutputSet = true;
            IsSupplyPort1Enabled_OutputClear = false;

            await WriteAndLogAsync(
                value => _device.WriteOutputSetAsync(value),
                DigitalOutputs.SupplyPort1,
                "OutputSet");
        });
    }

    private IObservable<Unit> ExecuteSupplyPort1Clear()
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;

            IsSupplyPort1Enabled_OutputSet = false;
            IsSupplyPort1Enabled_OutputClear = true;

            await WriteAndLogAsync(
                value => _device.WriteOutputClearAsync(value),
                DigitalOutputs.SupplyPort1,
                "OutputClear");
        });
    }

    private IObservable<Unit> ExecuteSupplyPort2Set()
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;

            IsSupplyPort2Enabled_OutputSet = true;
            IsSupplyPort2Enabled_OutputClear = false;

            await WriteAndLogAsync(
                value => _device.WriteOutputSetAsync(value),
                DigitalOutputs.SupplyPort2,
                "OutputSet");
        });
    }

    private IObservable<Unit> ExecuteSupplyPort2Clear()
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;

            IsSupplyPort2Enabled_OutputSet = false;
            IsSupplyPort2Enabled_OutputClear = true;

            await WriteAndLogAsync(
                value => _device.WriteOutputClearAsync(value),
                DigitalOutputs.SupplyPort2,
                "OutputClear");
        });
    }

    private IObservable<Unit> ExecuteDIOPort0Set()
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;

            IsDIO0Enabled_PortDIOSet = true;
            IsDIO0Enabled_PortDIOClear = false;

            await WriteAndLogAsync(
                value => _device.WritePortDIOSetAsync(value),
                PortDigitalIOS.DIO0,
                "PortDIOSet");
        });
    }

    private IObservable<Unit> ExecuteDIOPort0Clear()
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;

            IsDIO0Enabled_PortDIOSet = false;
            IsDIO0Enabled_PortDIOClear = true;

            await WriteAndLogAsync(
                value => _device.WritePortDIOClearAsync(value),
                PortDigitalIOS.DIO0,
                "PortDIOClear");
        });
    }

    private IObservable<Unit> ExecuteDIOPort1Set()
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;

            IsDIO1Enabled_PortDIOSet = true;
            IsDIO1Enabled_PortDIOClear = false;

            await WriteAndLogAsync(
                value => _device.WritePortDIOSetAsync(value),
                PortDigitalIOS.DIO1,
                "PortDIOSet");
        });
    }

    private IObservable<Unit> ExecuteDIOPort1Clear()
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;

            IsDIO1Enabled_PortDIOSet = false;
            IsDIO1Enabled_PortDIOClear = true;

            await WriteAndLogAsync(
                value => _device.WritePortDIOClearAsync(value),
                PortDigitalIOS.DIO1,
                "PortDIOClear");
        });
    }

    private IObservable<Unit> ExecuteDIOPort2Set()
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;

            IsDIO2Enabled_PortDIOSet = true;
            IsDIO2Enabled_PortDIOClear = false;

            await WriteAndLogAsync(
                value => _device.WritePortDIOSetAsync(value),
                PortDigitalIOS.DIO2,
                "PortDIOSet");
        });
    }

    private IObservable<Unit> ExecuteDIOPort2Clear()
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;

            IsDIO2Enabled_PortDIOSet = false;
            IsDIO2Enabled_PortDIOClear = true;

            await WriteAndLogAsync(
                value => _device.WritePortDIOClearAsync(value),
                PortDigitalIOS.DIO2,
                "PortDIOClear");
        });
    }

    private IObservable<Unit> ExecuteLed0Set()
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;

            IsLed0Enabled_OutputSet = true;
            IsLed0Enabled_OutputClear = false;

            await WriteAndLogAsync(
                value => _device.WriteOutputSetAsync(value),
                DigitalOutputs.Led0,
                "OutputSet");
        });
    }

    private IObservable<Unit> ExecuteLed0Clear()
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;

            IsLed0Enabled_OutputSet = false;
            IsLed0Enabled_OutputClear = true;

            await WriteAndLogAsync(
                value => _device.WriteOutputClearAsync(value),
                DigitalOutputs.Led0,
                "OutputClear");
        });
    }

    private IObservable<Unit> ExecuteLed1Set()
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;

            IsLed1Enabled_OutputSet = true;
            IsLed1Enabled_OutputClear = false;

            await WriteAndLogAsync(
                value => _device.WriteOutputSetAsync(value),
                DigitalOutputs.Led1,
                "OutputSet");
        });
    }

    private IObservable<Unit> ExecuteLed1Clear()
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;

            IsLed1Enabled_OutputSet = false;
            IsLed1Enabled_OutputClear = true;

            await WriteAndLogAsync(
                value => _device.WriteOutputClearAsync(value),
                DigitalOutputs.Led1,
                "OutputClear");
        });
    }

    private IObservable<Unit> ExecuteRgb0Set()
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;

            IsRgb0Enabled_OutputSet = true;
            IsRgb0Enabled_OutputClear = false;

            await WriteAndLogAsync(
                value => _device.WriteOutputSetAsync(value),
                DigitalOutputs.Rgb0,
                "OutputSet");
        });
    }

    private IObservable<Unit> ExecuteRgb0Clear()
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;

            IsRgb0Enabled_OutputSet = false;
            IsRgb0Enabled_OutputClear = true;

            await WriteAndLogAsync(
                value => _device.WriteOutputClearAsync(value),
                DigitalOutputs.Rgb0,
                "OutputClear");
        });
    }

    private IObservable<Unit> ExecuteRgb1Set()
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;

            IsRgb1Enabled_OutputSet = true;
            IsRgb1Enabled_OutputClear = false;

            await WriteAndLogAsync(
                value => _device.WriteOutputSetAsync(value),
                DigitalOutputs.Rgb1,
                "OutputSet");
        });
    }

    private IObservable<Unit> ExecuteRgb1Clear()
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;

            IsRgb1Enabled_OutputSet = false;
            IsRgb1Enabled_OutputClear = true;

            await WriteAndLogAsync(
                value => _device.WriteOutputClearAsync(value),
                DigitalOutputs.Rgb1,
                "OutputClear");
        });
    }


    private IObservable<Unit> ExecutePwmDO0Start()
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;

            IsPwmDO0Enabled_PwmStart = true;
            IsPwmDO0Enabled_PwmStop = false;

            await WriteAndLogAsync(
                value => _device.WritePwmFrequencyDO0Async(value),
                PwmFrequencyDO0,
                "PwmFrequencyDO0");
            await WriteAndLogAsync(
                value => _device.WritePwmDutyCycleDO0Async(value),
                PwmDutyCycleDO0,
                "PwmDutyCycleDO0");
            await WriteAndLogAsync(
                value => _device.WritePwmStartAsync(value),
                PwmOutputs.PwmDO0,
                "PwmStart");
        });

    }
    private IObservable<Unit> ExecutePwmDO0Stop()
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;

            IsPwmDO0Enabled_PwmStart = false;
            IsPwmDO0Enabled_PwmStop = true;

            await WriteAndLogAsync(
                value => _device.WritePwmStopAsync(value),
                PwmOutputs.PwmDO0,
                "PwmStop");
        });

    }
    private IObservable<Unit> ExecutePwmDO1Start()
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;

            IsPwmDO1Enabled_PwmStart = true;
            IsPwmDO1Enabled_PwmStop = false;

            await WriteAndLogAsync(
                value => _device.WritePwmFrequencyDO1Async(value),
                PwmFrequencyDO1,
                "PwmFrequencyDO1");
            await WriteAndLogAsync(
                value => _device.WritePwmDutyCycleDO1Async(value),
                PwmDutyCycleDO1,
                "PwmDutyCycleDO1");
            await WriteAndLogAsync(
                value => _device.WritePwmStartAsync(value),
                PwmOutputs.PwmDO1,
                "PwmStart");
        });
    }

    private IObservable<Unit> ExecutePwmDO1Stop()
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;

            IsPwmDO1Enabled_PwmStart = false;
            IsPwmDO1Enabled_PwmStop = true;

            await WriteAndLogAsync(
                value => _device.WritePwmStopAsync(value),
                PwmOutputs.PwmDO1,
                "PwmStop");
        });
    }

    private IObservable<Unit> ExecutePwmDO2Start()
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;

            IsPwmDO2Enabled_PwmStart = true;
            IsPwmDO2Enabled_PwmStop = false;

            await WriteAndLogAsync(
                value => _device.WritePwmFrequencyDO2Async(value),
                PwmFrequencyDO2,
                "PwmFrequencyDO2");
            await WriteAndLogAsync(
                value => _device.WritePwmDutyCycleDO2Async(value),
                PwmDutyCycleDO2,
                "PwmDutyCycleDO2");
            await WriteAndLogAsync(
                value => _device.WritePwmStartAsync(value),
                PwmOutputs.PwmDO2,
                "PwmStart");
        });
    }

    private IObservable<Unit> ExecutePwmDO2Stop()
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;

            IsPwmDO2Enabled_PwmStart = false;
            IsPwmDO2Enabled_PwmStop = true;

            await WriteAndLogAsync(
                value => _device.WritePwmStopAsync(value),
                PwmOutputs.PwmDO2,
                "PwmStop");
        });
    }

    private IObservable<Unit> ExecutePwmDO3Start()
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;

            IsPwmDO3Enabled_PwmStart = true;
            IsPwmDO3Enabled_PwmStop = false;

            await WriteAndLogAsync(
                value => _device.WritePwmFrequencyDO3Async(value),
                PwmFrequencyDO3,
                "PwmFrequencyDO3");
            await WriteAndLogAsync(
                value => _device.WritePwmDutyCycleDO3Async(value),
                PwmDutyCycleDO3,
                "PwmDutyCycleDO3");
            await WriteAndLogAsync(
                value => _device.WritePwmStartAsync(value),
                PwmOutputs.PwmDO3,
                "PwmStart");
        });
    }

    private IObservable<Unit> ExecutePwmDO3Stop()
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;

            IsPwmDO3Enabled_PwmStart = false;
            IsPwmDO3Enabled_PwmStop = true;

            await WriteAndLogAsync(
                value => _device.WritePwmStopAsync(value),
                PwmOutputs.PwmDO3,
                "PwmStop");
        });
    }

    private IObservable<Unit> ExecuteServoOuput2Start()
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;

            IsServoOutput2Enabled_EnableServos = true;
            IsServoOutput2Enabled_DisableServos = false;

            await WriteAndLogAsync(
                value => _device.WriteEnableServosAsync(value),
                ServoOutputs.ServoOutput2,
                "EnableServos");
        });
    }

    private IObservable<Unit> ExecuteServoOuput2Stop()
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;

            IsServoOutput2Enabled_EnableServos = false;
            IsServoOutput2Enabled_DisableServos = true;

            await WriteAndLogAsync(
                value => _device.WriteDisableServosAsync(value),
                ServoOutputs.ServoOutput2,
                "DisableServos");
        });
    }

    private IObservable<Unit> ExecuteServoOuput3Start()
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;

            IsServoOutput3Enabled_EnableServos = true;
            IsServoOutput3Enabled_DisableServos = false;

            await WriteAndLogAsync(
                value => _device.WriteEnableServosAsync(value),
                ServoOutputs.ServoOutput3,
                "EnableServos");
        });
    }

    private IObservable<Unit> ExecuteServoOuput3Stop()
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;

            IsServoOutput3Enabled_EnableServos = false;
            IsServoOutput3Enabled_DisableServos = true;

            await WriteAndLogAsync(
                value => _device.WriteDisableServosAsync(value),
                ServoOutputs.ServoOutput3,
                "DisableServos");
        });
    }

    private IObservable<Unit> ExecuteCamera0Start()
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;

            IsCameraOutput0Enabled_StartCameras = true;
            IsCameraOutput0Enabled_StopCameras = false;

            await WriteAndLogAsync(
                value => _device.WriteStartCamerasAsync(value),
                CameraOutputs.CameraOutput0,
                "StartCameras");
        });
    }

    private IObservable<Unit> ExecuteCamera0Stop()
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;

            IsCameraOutput0Enabled_StartCameras = false;
            IsCameraOutput0Enabled_StopCameras = true;

            await WriteAndLogAsync(
                value => _device.WriteStopCamerasAsync(value),
                CameraOutputs.CameraOutput0,
                "StopCameras");
        });
    }

    private IObservable<Unit> ExecuteCamera1Start()
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;

            IsCameraOutput1Enabled_StartCameras = true;
            IsCameraOutput1Enabled_StopCameras = false;

            await WriteAndLogAsync(
                value => _device.WriteStartCamerasAsync(value),
                CameraOutputs.CameraOutput1,
                "StartCameras");
        });
    }

    private IObservable<Unit> ExecuteCamera1Stop()
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                return;

            IsCameraOutput1Enabled_StartCameras = false;
            IsCameraOutput1Enabled_StopCameras = true;

            await WriteAndLogAsync(
                value => _device.WriteStopCamerasAsync(value),
                CameraOutputs.CameraOutput1,
                "StopCameras");
        });
    }


    private IObservable<Unit> LoadUsbInformation()
    {
        return Observable.Start(() =>
        {
            var devices = SerialPort.GetPortNames();

            if (OperatingSystem.IsMacOS())
                // except with Bluetooth in the name
                Ports = new ObservableCollection<string>(devices.Where(d => d.Contains("cu.")).Except(devices.Where(d => d.Contains("Bluetooth"))));
            else
                Ports = new ObservableCollection<string>(devices);

            Console.WriteLine("Loaded USB information");
            //Log.Information("Loaded USB information");
        });
    }

    private async Task ConnectAndGetBaseInfo()
    {
        if (string.IsNullOrEmpty(SelectedPort))
            throw new Exception("invalid parameter");

        if (Connected)
        {
            _device?.Dispose();
            _device = null;
            Connected = false;
            SentMessages.Clear();
            return;
        }

        try
        {
            using var cts = new CancellationTokenSource(TimeSpan.FromMilliseconds(500));
            _device = await Harp.Behavior.Device.CreateAsync(SelectedPort, cts.Token);
        }
        catch (OperationCanceledException ex)
        {
            Console.WriteLine($"Error connecting to device with error: {ex}");
            //Log.Error(ex, "Error connecting to device with error: {Exception}", ex);
            var messageBoxStandardWindow = MessageBoxManager
                .GetMessageBoxStandard("Unexpected device found",
                    "Timeout when trying to connect to a device. Most likely not an Harp device.",
                    icon: Icon.Error);
            await messageBoxStandardWindow.ShowAsync();
            _device?.Dispose();
            _device = null;
            return;

        }
        catch (HarpException ex)
        {
            Console.WriteLine($"Error connecting to device with error: {ex}");
            //Log.Error(ex, "Error connecting to device with error: {Exception}", ex);

            var messageBoxStandardWindow = MessageBoxManager
                .GetMessageBoxStandard("Unexpected device found",
                    ex.Message,
                    icon: Icon.Error);
            await messageBoxStandardWindow.ShowAsync();

            _device?.Dispose();
            _device = null;
            return;
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine($"COM port still in use and most likely not the expected Harp device");
            var messageBoxStandardWindow = MessageBoxManager
                .GetMessageBoxStandard("Unexpected device found",
                    $"COM port still in use and most likely not the expected Harp device.{Environment.NewLine}Specific error: {ex.Message}",
                    icon: Icon.Error);
            await messageBoxStandardWindow.ShowAsync();

            _device?.Dispose();
            _device = null;
            return;
        }

        // Clear the sent messages list
        SentMessages.Clear();

        //Log.Information("Attempting connection to port \'{SelectedPort}\'", SelectedPort);
        Console.WriteLine($"Attempting connection to port \'{SelectedPort}\'");

        DeviceID = await _device.ReadWhoAmIAsync();
        DeviceName = await _device.ReadDeviceNameAsync();
        HardwareVersion = await _device.ReadHardwareVersionAsync();
        FirmwareVersion = await _device.ReadFirmwareVersionAsync();
        try
        {
            // some devices may not have a serial number
            SerialNumber = await _device.ReadSerialNumberAsync();
        }
        catch (HarpException)
        {
            // Device does not have a serial number, simply continue by ignoring the exception
        }

        /*****************************************************************
        * TODO: Please REVIEW all these registers and update the values
        * ****************************************************************/
        DigitalInputState = await _device.ReadDigitalInputStateAsync();
        OutputSet = await _device.ReadOutputSetAsync();
        OutputClear = await _device.ReadOutputClearAsync();
        OutputToggle = await _device.ReadOutputToggleAsync();
        OutputState = await _device.ReadOutputStateAsync();
        PortDIOSet = await _device.ReadPortDIOSetAsync();
        PortDIOClear = await _device.ReadPortDIOClearAsync();
        PortDIOToggle = await _device.ReadPortDIOToggleAsync();
        PortDIOState = await _device.ReadPortDIOStateAsync();
        PortDIODirection = await _device.ReadPortDIODirectionAsync();
        PortDIOStateEvent = await _device.ReadPortDIOStateEventAsync();
        AnalogData = await _device.ReadAnalogDataAsync();
        OutputPulseEnable = await _device.ReadOutputPulseEnableAsync();
        PulseDOPort0 = await _device.ReadPulseDOPort0Async();
        PulseDOPort1 = await _device.ReadPulseDOPort1Async();
        PulseDOPort2 = await _device.ReadPulseDOPort2Async();
        PulseSupplyPort0 = await _device.ReadPulseSupplyPort0Async();
        PulseSupplyPort1 = await _device.ReadPulseSupplyPort1Async();
        PulseSupplyPort2 = await _device.ReadPulseSupplyPort2Async();
        PulseLed0 = await _device.ReadPulseLed0Async();
        PulseLed1 = await _device.ReadPulseLed1Async();
        PulseRgb0 = await _device.ReadPulseRgb0Async();
        PulseRgb1 = await _device.ReadPulseRgb1Async();
        PulseDO0 = await _device.ReadPulseDO0Async();
        PulseDO1 = await _device.ReadPulseDO1Async();
        PulseDO2 = await _device.ReadPulseDO2Async();
        PulseDO3 = await _device.ReadPulseDO3Async();
        PwmFrequencyDO0 = await _device.ReadPwmFrequencyDO0Async();
        PwmFrequencyDO1 = await _device.ReadPwmFrequencyDO1Async();
        PwmFrequencyDO2 = await _device.ReadPwmFrequencyDO2Async();
        PwmFrequencyDO3 = await _device.ReadPwmFrequencyDO3Async();
        PwmDutyCycleDO0 = await _device.ReadPwmDutyCycleDO0Async();
        PwmDutyCycleDO1 = await _device.ReadPwmDutyCycleDO1Async();
        PwmDutyCycleDO2 = await _device.ReadPwmDutyCycleDO2Async();
        PwmDutyCycleDO3 = await _device.ReadPwmDutyCycleDO3Async();
        PwmStart = await _device.ReadPwmStartAsync();
        PwmStop = await _device.ReadPwmStopAsync();
        RgbAll = await _device.ReadRgbAllAsync();
        Rgb0 = await _device.ReadRgb0Async();
        Rgb1 = await _device.ReadRgb1Async();
        Led0Current = await _device.ReadLed0CurrentAsync();
        Led1Current = await _device.ReadLed1CurrentAsync();
        Led0MaxCurrent = await _device.ReadLed0MaxCurrentAsync();
        Led1MaxCurrent = await _device.ReadLed1MaxCurrentAsync();
        EventEnable = await _device.ReadEventEnableAsync();
        StartCameras = await _device.ReadStartCamerasAsync();
        StopCameras = await _device.ReadStopCamerasAsync();
        EnableServos = await _device.ReadEnableServosAsync();
        DisableServos = await _device.ReadDisableServosAsync();
        EnableEncoders = await _device.ReadEnableEncodersAsync();
        EncoderMode = await _device.ReadEncoderModeAsync();
        Camera0Frame = await _device.ReadCamera0FrameAsync();
        Camera0Frequency = await _device.ReadCamera0FrequencyAsync();
        Camera1Frame = await _device.ReadCamera1FrameAsync();
        Camera1Frequency = await _device.ReadCamera1FrequencyAsync();
        ServoMotor2Period = await _device.ReadServoMotor2PeriodAsync();
        ServoMotor2Pulse = await _device.ReadServoMotor2PulseAsync();
        ServoMotor3Period = await _device.ReadServoMotor3PeriodAsync();
        ServoMotor3Pulse = await _device.ReadServoMotor3PulseAsync();
        EncoderReset = await _device.ReadEncoderResetAsync();
        EnableSerialTimestamp = await _device.ReadEnableSerialTimestampAsync();
        MimicPort0IR = await _device.ReadMimicPort0IRAsync();
        MimicPort1IR = await _device.ReadMimicPort1IRAsync();
        MimicPort2IR = await _device.ReadMimicPort2IRAsync();
        MimicPort0Valve = await _device.ReadMimicPort0ValveAsync();
        MimicPort1Valve = await _device.ReadMimicPort1ValveAsync();
        MimicPort2Valve = await _device.ReadMimicPort2ValveAsync();
        PokeInputFilter = await _device.ReadPokeInputFilterAsync();

        RgbAllAdapter.UpdateFromRegister(RgbAll);
        Rgb0Adapter.UpdateFromRegister(Rgb0);
        Rgb1Adapter.UpdateFromRegister(Rgb1);
        Rgb0Adapter.LinkToParent(RgbAllAdapter, 0);
        Rgb1Adapter.LinkToParent(RgbAllAdapter, 1);

        // generate observable for the _deviceSync
        _deviceEventsObservable = GenerateEventMessages();

        Connected = true;

        //Log.Information("Connected to device");
        Console.WriteLine("Connected to device");
    }

    public IObservable<string> GenerateEventMessages()
    {
        return Observable.Create<string>(async (observer, cancellationToken) =>
        {
            // Loop until cancellation is requested or the device is no longer available.
            while (!cancellationToken.IsCancellationRequested && _device != null)
            {
                // Capture local reference and check for null.
                var device = _device;
                if (device == null)
                {
                    observer.OnCompleted();
                    break;
                }

                try
                {
                    // Check if PortDI event is enabled
                    if (IsPortDIEnabled)
                    {
                        var result = await device.ReadPortDIOSetAsync(cancellationToken);
                        // Update the corresponding property with the result
                        // FIXME: this might not be the most appropriate action, please review for each case
                        PortDIOSet = result;
                        observer.OnNext($"PortDI: {result}");
                    }

                    // Check if PortDIO event is enabled
                    if (IsPortDIOEnabled)
                    {
                        var result = await device.ReadPortDIOSetAsync(cancellationToken);
                        // Update the corresponding property with the result
                        // FIXME: this might not be the most appropriate action, please review for each case
                        PortDIOSet = result;
                        observer.OnNext($"PortDIO: {result}");
                    }

                    // Check if AnalogData event is enabled
                    if (IsAnalogDataEnabled)
                    {
                        var result = await device.ReadAnalogDataAsync(cancellationToken);
                        // Update the corresponding property with the result
                        // FIXME: this might not be the most appropriate action, please review for each case
                        AnalogData = result;
                        observer.OnNext($"AnalogData: {result}");
                    }

                    // Check if Camera0 event is enabled
                    if (IsCamera0Enabled)
                    {
                        var result = await device.ReadCamera0FrameAsync(cancellationToken);
                        // Update the corresponding property with the result
                        // FIXME: this might not be the most appropriate action, please review for each case
                        Camera0Frame = result;
                        observer.OnNext($"Camera0: {result}");
                    }

                    // Check if Camera1 event is enabled
                    if (IsCamera1Enabled)
                    {
                        var result = await device.ReadCamera1FrameAsync(cancellationToken);
                        // Update the corresponding property with the result
                        // FIXME: this might not be the most appropriate action, please review for each case
                        Camera1Frame = result;
                        observer.OnNext($"Camera1: {result}");
                    }


                    // NOTE: Move the below entries to the correct event validation.
                    // The following registers have Event access but don't have a direct mapping to event flags
                    // These should be moved to appropriate event validation sections once their triggering events are identified
                    var DigitalInputStateResult = await device.ReadDigitalInputStateAsync(cancellationToken);
                    DigitalInputState = DigitalInputStateResult;
                    observer.OnNext($"DigitalInputState: {DigitalInputStateResult}");

                    var PortDIOStateEventResult = await device.ReadPortDIOStateEventAsync(cancellationToken);
                    PortDIOStateEvent = PortDIOStateEventResult;
                    observer.OnNext($"PortDIOStateEvent: {PortDIOStateEventResult}");
                    var StopCamerasResult = await device.ReadStopCamerasAsync(cancellationToken);
                    observer.OnNext($"StopCameras: {StopCamerasResult}");


                    // NOTE: These in the yalm are yet not considered events but write
                    var OutputStateResult = await device.ReadOutputStateAsync(cancellationToken);
                    OutputState = OutputStateResult;
                    observer.OnNext($"OutputState: {OutputStateResult}");


                    //var resultRgb0 = await device.ReadRgb0Async(cancellationToken);
                    //Rgb0 = resultRgb0;

                    //observer.OnNext($"Rgb0: {resultRgb0}");

                    //var resultRgb1 = await device.ReadRgb1Async(cancellationToken);
                    //Rgb1 = resultRgb1;
                    //observer.OnNext($"Rgb1: {resultRgb1}");

                    //var resultRgbAll = await device.ReadRgbAllAsync(cancellationToken);
                    //RgbAll = resultRgbAll;
                    //observer.OnNext($"RgbAll: {resultRgbAll}");

                    // var PortDIOStateResult = await device.

                    // Wait a short while before polling again. Adjust delay as necessary.
                    await Task.Delay(TimeSpan.FromMilliseconds(10), cancellationToken);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    observer.OnError(ex);
                    break;
                }
            }
            observer.OnCompleted();
            return Disposable.Empty;
        });
    }

    private IObservable<Unit> SaveConfiguration(bool savePermanently)
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                throw new Exception("You need to connect to the device first");

            //RgbAllAdapter.UpdateFromRegister(RgbAll);
            //Rgb0Adapter.UpdateFromRegister(Rgb0);
            //Rgb1Adapter.UpdateFromRegister(Rgb1);
            //Rgb0Adapter.LinkToParent(RgbAllAdapter, 0);
            //Rgb1Adapter.LinkToParent(RgbAllAdapter, 1);

            //Rgb1Adapter.ToRegisterValue();

            /*****************************************************************
            * TODO: Please REVIEW all these registers and update the values
            * ****************************************************************/
            //await WriteAndLogAsync(
            //    value => _device.WriteOutputSetAsync(value),
            //    OutputSet,
            //    "OutputSet");
            //await WriteAndLogAsync(
            //    value => _device.WriteOutputClearAsync(value),
            //    OutputClear,
            //    "OutputClear");
            //await WriteAndLogAsync(
            //    value => _device.WriteOutputToggleAsync(value),
            //    OutputToggle,
            //    "OutputToggle");
            //await WriteAndLogAsync(
            //    value => _device.WriteOutputStateAsync(value),
            //    OutputState,
            //    "OutputState");
            await WriteAndLogAsync(
                value => _device.WritePortDIOSetAsync(value),
                PortDIOSet,
                "PortDIOSet");
            await WriteAndLogAsync(
                value => _device.WritePortDIOClearAsync(value),
                PortDIOClear,
                "PortDIOClear");
            await WriteAndLogAsync(
                value => _device.WritePortDIOToggleAsync(value),
                PortDIOToggle,
                "PortDIOToggle");
            await WriteAndLogAsync(
                value => _device.WritePortDIOStateAsync(value),
                PortDIOState,
                "PortDIOState");
            await WriteAndLogAsync(
                value => _device.WritePortDIODirectionAsync(value),
                PortDIODirection,
                "PortDIODirection");
            await WriteAndLogAsync(
                value => _device.WriteOutputPulseEnableAsync(value),
                OutputPulseEnable,
                "OutputPulseEnable");
            await WriteAndLogAsync(
                value => _device.WritePulseDOPort0Async(value),
                PulseDOPort0,
                "PulseDOPort0");
            await WriteAndLogAsync(
                value => _device.WritePulseDOPort1Async(value),
                PulseDOPort1,
                "PulseDOPort1");
            await WriteAndLogAsync(
                value => _device.WritePulseDOPort2Async(value),
                PulseDOPort2,
                "PulseDOPort2");
            await WriteAndLogAsync(
                value => _device.WritePulseSupplyPort0Async(value),
                PulseSupplyPort0,
                "PulseSupplyPort0");
            await WriteAndLogAsync(
                value => _device.WritePulseSupplyPort1Async(value),
                PulseSupplyPort1,
                "PulseSupplyPort1");
            await WriteAndLogAsync(
                value => _device.WritePulseSupplyPort2Async(value),
                PulseSupplyPort2,
                "PulseSupplyPort2");
            await WriteAndLogAsync(
                value => _device.WritePulseLed0Async(value),
                PulseLed0,
                "PulseLed0");
            await WriteAndLogAsync(
                value => _device.WritePulseLed1Async(value),
                PulseLed1,
                "PulseLed1");
            await WriteAndLogAsync(
                value => _device.WritePulseRgb0Async(value),
                PulseRgb0,
                "PulseRgb0");
            await WriteAndLogAsync(
                value => _device.WritePulseRgb1Async(value),
                PulseRgb1,
                "PulseRgb1");
            await WriteAndLogAsync(
                value => _device.WritePulseDO0Async(value),
                PulseDO0,
                "PulseDO0");
            await WriteAndLogAsync(
                value => _device.WritePulseDO1Async(value),
                PulseDO1,
                "PulseDO1");
            await WriteAndLogAsync(
                value => _device.WritePulseDO2Async(value),
                PulseDO2,
                "PulseDO2");
            await WriteAndLogAsync(
                value => _device.WritePulseDO3Async(value),
                PulseDO3,
                "PulseDO3");
            await WriteAndLogAsync(
                value => _device.WritePwmFrequencyDO0Async(value),
                PwmFrequencyDO0,
                "PwmFrequencyDO0");
            await WriteAndLogAsync(
                value => _device.WritePwmFrequencyDO1Async(value),
                PwmFrequencyDO1,
                "PwmFrequencyDO1");
            await WriteAndLogAsync(
                value => _device.WritePwmFrequencyDO2Async(value),
                PwmFrequencyDO2,
                "PwmFrequencyDO2");
            await WriteAndLogAsync(
                value => _device.WritePwmFrequencyDO3Async(value),
                PwmFrequencyDO3,
                "PwmFrequencyDO3");
            await WriteAndLogAsync(
                value => _device.WritePwmDutyCycleDO0Async(value),
                PwmDutyCycleDO0,
                "PwmDutyCycleDO0");
            await WriteAndLogAsync(
                value => _device.WritePwmDutyCycleDO1Async(value),
                PwmDutyCycleDO1,
                "PwmDutyCycleDO1");
            await WriteAndLogAsync(
                value => _device.WritePwmDutyCycleDO2Async(value),
                PwmDutyCycleDO2,
                "PwmDutyCycleDO2");
            await WriteAndLogAsync(
                value => _device.WritePwmDutyCycleDO3Async(value),
                PwmDutyCycleDO3,
                "PwmDutyCycleDO3");
            await WriteAndLogAsync(
                value => _device.WritePwmStartAsync(value),
                PwmStart,
                "PwmStart");
            await WriteAndLogAsync(
                value => _device.WritePwmStopAsync(value),
                PwmStop,
                "PwmStop");
            

            var c0 = Rgb0Adapter.Color;
            var c1 = Rgb1Adapter.Color;
            RgbAll = new RgbAllPayload(
                c0.R, c0.G, c0.B,
                c1.R, c1.G, c1.B);

            Rgb0 = new RgbPayload(c0.R, c0.G, c0.B);
            Rgb1 = new RgbPayload(c1.R, c1.G, c1.B);
            //Rgb0 = Rgb0Adapter.ToRegisterValue();
            //Rgb0 = Rgb0Adapter.ToRegisterValue();
            await WriteAndLogAsync(
                value => _device.WriteRgb0Async(value),
                Rgb0,
                "Rgb0");
            await WriteAndLogAsync(
                value => _device.WriteRgb1Async(value),
                Rgb1,
                "Rgb1");
            await WriteAndLogAsync(
                value => _device.WritePulseRgb0Async(value),
                PulseRgb0,
                "PulseRgb0");
            await WriteAndLogAsync(
                value => _device.WritePulseRgb1Async(value),
                PulseRgb1,
                "PulseRgb1");
            await WriteAndLogAsync(
                value => _device.WriteRgbAllAsync(value),
                RgbAll,
                "RgbAll");
            await WriteAndLogAsync(
                value => _device.WriteLed0CurrentAsync(value), 
                Led0Current,
                "Led0Current");
            await WriteAndLogAsync(
                value => _device.WriteLed1CurrentAsync(value),
                Led1Current,
                "Led1Current");
            await WriteAndLogAsync(
                value => _device.WriteLed0MaxCurrentAsync(value),
                Led0MaxCurrent,
                "Led0MaxCurrent");
            await WriteAndLogAsync(
                value => _device.WriteLed1MaxCurrentAsync(value),
                Led1MaxCurrent,
                "Led1MaxCurrent");
            await WriteAndLogAsync(
                value => _device.WriteEventEnableAsync(value),
                EventEnable,
                "EventEnable");
            await WriteAndLogAsync(
                value => _device.WriteStartCamerasAsync(value),
                StartCameras,
                "StartCameras");
            await WriteAndLogAsync(
                value => _device.WriteEnableServosAsync(value),
                EnableServos,
                "EnableServos");
            await WriteAndLogAsync(
                value => _device.WriteDisableServosAsync(value),
                DisableServos,
                "DisableServos");
            await WriteAndLogAsync(
                value => _device.WriteEnableEncodersAsync(value),
                EnableEncoders,
                "EnableEncoders");
            await WriteAndLogAsync(
                value => _device.WriteEncoderModeAsync(value),
                EncoderMode,
                "EncoderMode");
            await WriteAndLogAsync(
                value => _device.WriteCamera0FrequencyAsync(value),
                Camera0Frequency,
                "Camera0Frequency");
            await WriteAndLogAsync(
                value => _device.WriteCamera1FrequencyAsync(value),
                Camera1Frequency,
                "Camera1Frequency");
            await WriteAndLogAsync(
                value => _device.WriteServoMotor2PeriodAsync(value),
                ServoMotor2Period,
                "ServoMotor2Period");
            await WriteAndLogAsync(
                value => _device.WriteServoMotor2PulseAsync(value),
                ServoMotor2Pulse,
                "ServoMotor2Pulse");
            await WriteAndLogAsync(
                value => _device.WriteServoMotor3PeriodAsync(value),
                ServoMotor3Period,
                "ServoMotor3Period");
            await WriteAndLogAsync(
                value => _device.WriteServoMotor3PulseAsync(value),
                ServoMotor3Pulse,
                "ServoMotor3Pulse");
            await WriteAndLogAsync(
                value => _device.WriteEncoderResetAsync(value),
                EncoderReset,
                "EncoderReset");
            await WriteAndLogAsync(
                value => _device.WriteEnableSerialTimestampAsync(value),
                EnableSerialTimestamp,
                "EnableSerialTimestamp");
            await WriteAndLogAsync(
                value => _device.WriteMimicPort0IRAsync(value),
                MimicPort0IR,
                "MimicPort0IR");
            await WriteAndLogAsync(
                value => _device.WriteMimicPort1IRAsync(value),
                MimicPort1IR,
                "MimicPort1IR");
            await WriteAndLogAsync(
                value => _device.WriteMimicPort2IRAsync(value),
                MimicPort2IR,
                "MimicPort2IR");
            await WriteAndLogAsync(
                value => _device.WriteMimicPort0ValveAsync(value),
                MimicPort0Valve,
                "MimicPort0Valve");
            await WriteAndLogAsync(
                value => _device.WriteMimicPort1ValveAsync(value),
                MimicPort1Valve,
                "MimicPort1Valve");
            await WriteAndLogAsync(
                value => _device.WriteMimicPort2ValveAsync(value),
                MimicPort2Valve,
                "MimicPort2Valve");
            await WriteAndLogAsync(
                value => _device.WritePokeInputFilterAsync(value),
                PokeInputFilter,
                "PokeInputFilter");
            await WriteAndLogAsync(
                value => _device.WriteOutputSetAsync(value),
                OutputSet,
                "OutputSet");
            await WriteAndLogAsync(
                value => _device.WriteOutputClearAsync(value),
                OutputClear,
                "OutputClear");

            IsEncoderPort2Enabled_EncoderReset = false; // Uncheck after apply
            //await WriteAndLogAsync(
            //    value => _device.WriteOutputToggleAsync(value),
            //    OutputToggle,
            //    "OutputToggle");
            //await WriteAndLogAsync(
            //    value => _device.WriteOutputStateAsync(value),
            //    OutputState,
            //    "OutputState");

            // Save the configuration to the device permanently
            if (savePermanently)
            {
                // To prevent multiple calls to the device while it is resetting
                _deviceEventsSubscription?.Dispose();
                _deviceEventsSubscription = null;

                await WriteAndLogAsync(
                    value => _device.WriteResetDeviceAsync(value),
                    ResetFlags.Save,
                    "SavePermanently");

                // Wait to ensure the device is ready after the reset
                await Task.Delay(4000);

                // Re-subscribe to the device events observable
                SubscribeToEvents();
            }
        });
    }

    private IObservable<Unit> ResetConfiguration()
    {
        return Observable.StartAsync(async () =>
        {
            if (_device != null)
            {
                await WriteAndLogAsync(
                    value => _device.WriteResetDeviceAsync(value),
                    ResetFlags.RestoreDefault,
                    "ResetDevice");
            }
        });
    }

    private async Task WriteAndLogAsync<T>(Func<T, Task> writeFunc, T value, string registerName)
    {
        if (_device == null)
            throw new Exception("Device is not connected");

        await writeFunc(value);

        // Log the message to the SentMessages collection on the UI thread
        RxApp.MainThreadScheduler.Schedule(() =>
        {
            SentMessages.Add($"{DateTime.Now:HH:mm:ss.fff} - Write {registerName}: {value}");
        });
    }

    private void SubscribeToEvents()
    {
        _deviceEventsSubscription = _deviceEventsObservable
            .ObserveOn(RxApp.MainThreadScheduler)
            .Subscribe(
                msg => HarpEvents.Add(msg.ToString()),
                ex => Debug.WriteLine($"Error in device events: {ex}")
            );
    }


    public class ArrayItemWrapper<T> : ReactiveObject
    {
        public int Index { get; }

        [Reactive]
        public T Value { get; set; }

        public ArrayItemWrapper(int index, T value)
        {
            Index = index;
            Value = value;
        }
    }

    private void UpdateArrayCollection<T>(T[] array, ObservableCollection<ArrayItemWrapper<T>> collection)
    {
        if (array == null)
            return;

        RxApp.MainThreadScheduler.Schedule(() =>
        {
            collection.Clear();

            for (int i = 0; i < array.Length; i++)
            {
                collection.Add(new ArrayItemWrapper<T>(i, array[i]));
            }
        });
    }

    public class RgbColorItem : ReactiveObject
    {
        [Reactive] public int Index { get; set; }
        [Reactive] public Color Color { get; set; }
        [Reactive] public byte Red { get; set; }
        [Reactive] public byte Green { get; set; }
        [Reactive] public byte Blue { get; set; }

        public RgbColorItem(int index, byte red, byte green, byte blue)
        {
            Index = index;
            Red = red;
            Green = green;
            Blue = blue;
            Color = Color.FromRgb(red, green, blue);

            // Setup reactive properties to update the array when color changes
            this.WhenAnyValue(x => x.Color)
                .Subscribe(color =>
                {
                    Red = color.R;
                    Green = color.G;
                    Blue = color.B;
                });

            // Update Color when individual RGB components change
            this.WhenAnyValue(x => x.Red, x => x.Green, x => x.Blue)
                .Skip(1)
                .Subscribe(tuple =>
                {
                    var (r, g, b) = tuple;
                    Color = Color.FromRgb(r, g, b);
                });
        }
    }

    private void UpdateRgbArrayCollection(byte[] array, ObservableCollection<RgbColorItem> collection)
    {
        if (array == null)
            return;

        RxApp.MainThreadScheduler.Schedule(() =>
        {
            collection.Clear();

            for (int i = 0; i < array.Length; i += 3)
            {
                if (i + 2 < array.Length)
                {
                    collection.Add(new RgbColorItem(i / 3, array[i], array[i + 1], array[i + 2]));
                }
            }
        });
    }

    private void SetupRgbCollectionChangeTracking(ObservableCollection<RgbColorItem> collection, string propertyName)
    {
        // Subscribe to changes in the Items property of the collection
        collection.CollectionChanged += (sender, args) =>
        {
            // When items are added, subscribe to their Color property changes
            if (args.NewItems != null)
            {
                foreach (RgbColorItem item in args.NewItems)
                {
                    // Subscribe to this individual item's Color property changes
                    item.WhenAnyValue(x => x.Color)
                        .Skip(1) // Skip initial setting
                        .Subscribe(color =>
                        {
                            // Find which byte array the collection is for using propertyName
                            var byteArray = this.GetType().GetProperty(propertyName)?.GetValue(this) as byte[];
                            if (byteArray != null && item.Index >= 0 && (item.Index * 3 + 2) < byteArray.Length)
                            {
                                // Update just this RGB triplet in the array
                                byteArray[item.Index * 3] = color.R;
                                byteArray[item.Index * 3 + 1] = color.G;
                                byteArray[item.Index * 3 + 2] = color.B;

                                // Notify that the array property has changed
                                RxApp.MainThreadScheduler.Schedule(() =>
                                {
                                    this.RaisePropertyChanged(propertyName);
                                });
                            }
                        });
                }
            }
        };
    }
}
