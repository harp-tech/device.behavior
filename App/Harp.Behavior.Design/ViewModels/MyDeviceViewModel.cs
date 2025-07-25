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
    public ReactiveCommand<Unit, Unit> PwmDO0StartCommand { get; }
    public ReactiveCommand<Unit, Unit> PwmDO0StopCommand { get; }
    public ReactiveCommand<Unit, Unit> PwmDO1StartCommand { get; }
    public ReactiveCommand<Unit, Unit> PwmDO1StopCommand { get; }
    public ReactiveCommand<Unit, Unit> PwmDO2StartCommand { get; }
    public ReactiveCommand<Unit, Unit> PwmDO2StopCommand { get; }
    public ReactiveCommand<Unit, Unit> PwmDO3StartCommand { get; }
    public ReactiveCommand<Unit, Unit> PwmDO3StopCommand { get; }

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

        PwmDO0StartCommand = ReactiveCommand.Create(ExecutePwmDO0Start, canChangeConfig);
        PwmDO0StopCommand = ReactiveCommand.Create(ExecutePwmDO0Stop, canChangeConfig);
        PwmDO1StartCommand = ReactiveCommand.Create(ExecutePwmDO1Start, canChangeConfig);
        PwmDO1StopCommand = ReactiveCommand.Create(ExecutePwmDO1Stop, canChangeConfig);
        PwmDO2StartCommand = ReactiveCommand.Create(ExecutePwmDO2Start, canChangeConfig);
        PwmDO2StopCommand = ReactiveCommand.Create(ExecutePwmDO2Stop, canChangeConfig);
        PwmDO3StartCommand = ReactiveCommand.Create(ExecutePwmDO3Start, canChangeConfig);
        PwmDO3StopCommand = ReactiveCommand.Create(ExecutePwmDO3Stop, canChangeConfig);

        ServoOutput2StartCommand = ReactiveCommand.Create(ExecuteServoOuput2Start, canChangeConfig);
        ServoOutput2StopCommand = ReactiveCommand.Create(ExecuteServoOuput2Stop, canChangeConfig);
        ServoOutput3StartCommand = ReactiveCommand.Create(ExecuteServoOuput3Start, canChangeConfig);
        ServoOutput3StopCommand = ReactiveCommand.Create(ExecuteServoOuput3Stop, canChangeConfig);

        Camera0StartCommand = ReactiveCommand.Create(ExecuteCamera0Start, canChangeConfig);
        Camera0StopCommand = ReactiveCommand.Create(ExecuteCamera0Stop, canChangeConfig);
        Camera1StartCommand = ReactiveCommand.Create(ExecuteCamera1Start, canChangeConfig);
        Camera1StopCommand = ReactiveCommand.Create(ExecuteCamera1Stop, canChangeConfig);

        // force initial population of currently connected ports
        LoadUsbInformation();
    }

    private void ExecutePwmDO0Start()
    {
        // Set the PWM DO0 start state
        IsPwmDO0Enabled_PwmStart = true;
        IsPwmDO0Enabled_PwmStop = false;
    }

    private void ExecutePwmDO0Stop()
    {
        // Set the PWM DO0 stop state  
        IsPwmDO0Enabled_PwmStop = true;
        IsPwmDO0Enabled_PwmStart = false;
    }

    private void ExecutePwmDO1Start()
    {
        // Set the PWM DO1 start state
        IsPwmDO1Enabled_PwmStart = true;
        IsPwmDO1Enabled_PwmStop = false;
    }

    private void ExecutePwmDO1Stop()
    {
        // Set the PWM DO1 stop state  
        IsPwmDO1Enabled_PwmStop = true;
        IsPwmDO1Enabled_PwmStart = false;
    }

    private void ExecutePwmDO2Start()
    {
        // Set the PWM DO2 start state
        IsPwmDO2Enabled_PwmStart = true;
        IsPwmDO2Enabled_PwmStop = false;
    }

    private void ExecutePwmDO2Stop()
    {
        // Set the PWM DO2 stop state  
        IsPwmDO2Enabled_PwmStop = true;
        IsPwmDO2Enabled_PwmStart = false;
    }

    private void ExecutePwmDO3Start()
    {
        // Set the PWM DO3 start state
        IsPwmDO3Enabled_PwmStart = true;
        IsPwmDO3Enabled_PwmStop = false;
    }

    private void ExecutePwmDO3Stop()
    {
        // Set the PWM DO3 stop state  
        IsPwmDO3Enabled_PwmStop = true;
        IsPwmDO3Enabled_PwmStart = false;
    }

    private void ExecuteServoOuput2Start()
    {
        IsServoOutput2Enabled_EnableServos = true;
        IsServoOutput2Enabled_DisableServos = false;
    }
    private void ExecuteServoOuput2Stop()
    {
        IsServoOutput2Enabled_EnableServos = false;
        IsServoOutput2Enabled_DisableServos = true;
    }
    private void ExecuteServoOuput3Start()
    {
        IsServoOutput3Enabled_EnableServos = true;
        IsServoOutput3Enabled_DisableServos = false;
    }
    private void ExecuteServoOuput3Stop()
    {
        IsServoOutput3Enabled_EnableServos = false;
        IsServoOutput3Enabled_DisableServos = true;
    }

    private void ExecuteCamera0Start()
    {
        IsCameraOutput0Enabled_StartCameras = true;
        IsCameraOutput0Enabled_StopCameras = false;
    }
    private void ExecuteCamera0Stop()
    {
        IsCameraOutput0Enabled_StartCameras = false;
        IsCameraOutput0Enabled_StopCameras = true;
    }
    private void ExecuteCamera1Start()
    {
        IsCameraOutput1Enabled_StartCameras = true;
        IsCameraOutput1Enabled_StopCameras = false;
    }
    private void ExecuteCamera1Stop()
    {
        IsCameraOutput1Enabled_StartCameras = false;
        IsCameraOutput1Enabled_StopCameras = true;
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
                    observer.OnNext($"DigitalInputState: {DigitalInputStateResult}");
                    var PortDIOStateEventResult = await device.ReadPortDIOStateEventAsync(cancellationToken);
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
