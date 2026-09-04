---
uid: Harp.Behavior.Device
---

Use the [Harp device pattern](https://harp-tech.org/articles/operators.html#device-pattern) to initialize the device, log data, and send commands to and receive messages from the Behavior board.

:::workflow
![Harp Device Pattern](../workflows/harp-devicepattern.bonsai)
:::

Check out the following in-depth guides to learn how to access the device functionality with the `Harp.Behavior` package:

- [Acquire Analog Data](../articles/acquire-analog-data.md)
- [Read Digital Inputs](../articles/read-digital-inputs.md)
- [Control Digital Outputs](../articles/control-digital-outputs.md)
- [Generate PWM](../articles/generate-pwm.md)
- [Configure LEDs](../articles/configure-leds.md)
- [Control Poke Peripheral](../articles/control-poke.md)
- [Track Rotary Encoder](../articles/track-rotary-encoder.md)
- [Trigger Cameras](../articles/trigger-cameras.md)
- [Drive Servos](../articles/drive-servos.md)
- [Mimic Poke Signals](../articles/mimic-poke-signals.md)
- [Advanced Configuration](../articles/advanced-configuration.md)

Refer to the register table below for a complete listing of the available registers on the device.

<table>
  <thead>
    <tr><th colspan="2">Behavior</th></tr>
  </thead>
  <tbody>
    <tr><td>whoAmI</td><td>1216</td></tr>
    <tr><td>firmwareVersion</td><td>3.4</td></tr>
    <tr><td>hardwareTargets</td><td>2.0</td></tr>
  </tbody>
</table>

### Registers

| name | address | type | length | access | description | range | interfaceType |
|-|-|-|-|-|-|-|-|
| [DigitalInputState](xref:Harp.Behavior.DigitalInputState) | 32 | U8 |  | Event | Reflects the state of DI digital lines of each Port |  | [DigitalInputs](xref:Harp.Behavior.DigitalInputs) |
| [OutputSet](xref:Harp.Behavior.OutputSet) | 34 | U16 |  | Write | Set the specified digital output lines. |  | [DigitalOutputs](xref:Harp.Behavior.DigitalOutputs) |
| [OutputClear](xref:Harp.Behavior.OutputClear) | 35 | U16 |  | Write | Clear the specified digital output lines |  | [DigitalOutputs](xref:Harp.Behavior.DigitalOutputs) |
| [OutputToggle](xref:Harp.Behavior.OutputToggle) | 36 | U16 |  | Write | Toggle the specified digital output lines |  | [DigitalOutputs](xref:Harp.Behavior.DigitalOutputs) |
| [OutputState](xref:Harp.Behavior.OutputState) | 37 | U16 |  | Write | Write the state of all digital output lines |  | [DigitalOutputs](xref:Harp.Behavior.DigitalOutputs) |
| [PortDIOSet](xref:Harp.Behavior.PortDIOSet) | 38 | U8 |  | Write | Set the specified port DIO lines |  | [PortDigitalIOS](xref:Harp.Behavior.PortDigitalIOS) |
| [PortDIOClear](xref:Harp.Behavior.PortDIOClear) | 39 | U8 |  | Write | Clear the specified port DIO lines |  | [PortDigitalIOS](xref:Harp.Behavior.PortDigitalIOS) |
| [PortDIOToggle](xref:Harp.Behavior.PortDIOToggle) | 40 | U8 |  | Write | Toggle the specified port DIO lines |  | [PortDigitalIOS](xref:Harp.Behavior.PortDigitalIOS) |
| [PortDIOState](xref:Harp.Behavior.PortDIOState) | 41 | U8 |  | Write | Write the state of all port DIO lines |  | [PortDigitalIOS](xref:Harp.Behavior.PortDigitalIOS) |
| [PortDIODirection](xref:Harp.Behavior.PortDIODirection) | 42 | U8 |  | Write | Specifies which of the port DIO lines are outputs |  | [PortDigitalIOS](xref:Harp.Behavior.PortDigitalIOS) |
| [PortDIOStateEvent](xref:Harp.Behavior.PortDIOStateEvent) | 43 | U8 |  | Event | Specifies the state of the port DIO lines on a line change |  | [PortDigitalIOS](xref:Harp.Behavior.PortDigitalIOS) |
| [AnalogData](xref:Harp.Behavior.AnalogData) | 44 | S16 | 3 | Event | Voltage at the ADC input and encoder value on Port 2 |  | [AnalogDataPayload](xref:Harp.Behavior.AnalogDataPayload) |
| [OutputPulseEnable](xref:Harp.Behavior.OutputPulseEnable) | 45 | U16 |  | Write | Enables the pulse function for the specified output lines |  | [DigitalOutputs](xref:Harp.Behavior.DigitalOutputs) |
| [PulseDOPort0](xref:Harp.Behavior.PulseDOPort0) | 46 | U16 |  | Write | Specifies the duration of the output pulse in milliseconds. | [1:] | |
| [PulseDOPort1](xref:Harp.Behavior.PulseDOPort1) | 47 | U16 |  | Write | Specifies the duration of the output pulse in milliseconds. | [1:] | |
| [PulseDOPort2](xref:Harp.Behavior.PulseDOPort2) | 48 | U16 |  | Write | Specifies the duration of the output pulse in milliseconds. | [1:] | |
| [PulseSupplyPort0](xref:Harp.Behavior.PulseSupplyPort0) | 49 | U16 |  | Write | Specifies the duration of the output pulse in milliseconds. | [1:] | |
| [PulseSupplyPort1](xref:Harp.Behavior.PulseSupplyPort1) | 50 | U16 |  | Write | Specifies the duration of the output pulse in milliseconds. | [1:] | |
| [PulseSupplyPort2](xref:Harp.Behavior.PulseSupplyPort2) | 51 | U16 |  | Write | Specifies the duration of the output pulse in milliseconds. | [1:] | |
| [PulseLed0](xref:Harp.Behavior.PulseLed0) | 52 | U16 |  | Write | Specifies the duration of the output pulse in milliseconds. | [1:] | |
| [PulseLed1](xref:Harp.Behavior.PulseLed1) | 53 | U16 |  | Write | Specifies the duration of the output pulse in milliseconds. | [1:] | |
| [PulseRgb0](xref:Harp.Behavior.PulseRgb0) | 54 | U16 |  | Write | Specifies the duration of the output pulse in milliseconds. | [1:] | |
| [PulseRgb1](xref:Harp.Behavior.PulseRgb1) | 55 | U16 |  | Write | Specifies the duration of the output pulse in milliseconds. | [1:] | |
| [PulseDO0](xref:Harp.Behavior.PulseDO0) | 56 | U16 |  | Write | Specifies the duration of the output pulse in milliseconds. | [1:] | |
| [PulseDO1](xref:Harp.Behavior.PulseDO1) | 57 | U16 |  | Write | Specifies the duration of the output pulse in milliseconds. | [1:] | |
| [PulseDO2](xref:Harp.Behavior.PulseDO2) | 58 | U16 |  | Write | Specifies the duration of the output pulse in milliseconds. | [1:] | |
| [PulseDO3](xref:Harp.Behavior.PulseDO3) | 59 | U16 |  | Write | Specifies the duration of the output pulse in milliseconds. | [1:] | |
| [PwmFrequencyDO0](xref:Harp.Behavior.PwmFrequencyDO0) | 60 | U16 |  | Write | Specifies the frequency of the PWM at DO0. | [1:] | |
| [PwmFrequencyDO1](xref:Harp.Behavior.PwmFrequencyDO1) | 61 | U16 |  | Write | Specifies the frequency of the PWM at DO1. | [1:] | |
| [PwmFrequencyDO2](xref:Harp.Behavior.PwmFrequencyDO2) | 62 | U16 |  | Write | Specifies the frequency of the PWM at DO2. | [1:] | |
| [PwmFrequencyDO3](xref:Harp.Behavior.PwmFrequencyDO3) | 63 | U16 |  | Write | Specifies the frequency of the PWM at DO3. | [1:] | |
| [PwmDutyCycleDO0](xref:Harp.Behavior.PwmDutyCycleDO0) | 64 | U8 |  | Write | Specifies the duty cycle of the PWM at DO0. | [1:99] | |
| [PwmDutyCycleDO1](xref:Harp.Behavior.PwmDutyCycleDO1) | 65 | U8 |  | Write | Specifies the duty cycle of the PWM at DO1. | [1:99] | |
| [PwmDutyCycleDO2](xref:Harp.Behavior.PwmDutyCycleDO2) | 66 | U8 |  | Write | Specifies the duty cycle of the PWM at DO2. | [1:99] | |
| [PwmDutyCycleDO3](xref:Harp.Behavior.PwmDutyCycleDO3) | 67 | U8 |  | Write | Specifies the duty cycle of the PWM at DO3. | [1:99] | |
| [PwmStart](xref:Harp.Behavior.PwmStart) | 68 | U8 |  | Write | Starts the PWM on the selected output lines. |  | [PwmOutputs](xref:Harp.Behavior.PwmOutputs) |
| [PwmStop](xref:Harp.Behavior.PwmStop) | 69 | U8 |  | Write | Stops the PWM on the selected output lines. |  | [PwmOutputs](xref:Harp.Behavior.PwmOutputs) |
| [RgbAll](xref:Harp.Behavior.RgbAll) | 70 | U8 | 6 | Write | Specifies the state of all RGB LED channels. |  | [RgbAllPayload](xref:Harp.Behavior.RgbAllPayload) |
| [Rgb0](xref:Harp.Behavior.Rgb0) | 71 | U8 | 3 | Write | Specifies the state of the RGB0 LED channels. |  | [RgbPayload](xref:Harp.Behavior.RgbPayload) |
| [Rgb1](xref:Harp.Behavior.Rgb1) | 72 | U8 | 3 | Write | Specifies the state of the RGB1 LED channels. |  | [RgbPayload](xref:Harp.Behavior.RgbPayload) |
| [Led0Current](xref:Harp.Behavior.Led0Current) | 73 | U8 |  | Write | Specifies the configuration of current to drive LED 0. | [2:100] | |
| [Led1Current](xref:Harp.Behavior.Led1Current) | 74 | U8 |  | Write | Specifies the configuration of current to drive LED 1. | [2:100] | |
| [Led0MaxCurrent](xref:Harp.Behavior.Led0MaxCurrent) | 75 | U8 |  | Write | Specifies the configuration of current to drive LED 0. | [5:100] | |
| [Led1MaxCurrent](xref:Harp.Behavior.Led1MaxCurrent) | 76 | U8 |  | Write | Specifies the configuration of current to drive LED 1. | [5:100] | |
| [EventEnable](xref:Harp.Behavior.EventEnable) | 77 | U8 |  | Write | Specifies the active events in the device. |  | [Events](xref:Harp.Behavior.Events) |
| [StartCameras](xref:Harp.Behavior.StartCameras) | 78 | U8 |  | Write | Specifies the camera outputs to enable in the device. |  | [CameraOutputs](xref:Harp.Behavior.CameraOutputs) |
| [StopCameras](xref:Harp.Behavior.StopCameras) | 79 | U8 |  | Write, Event | Specifies the camera outputs to disable in the device. An event will be issued when the trigger signal is actually stopped being generated. |  | [CameraOutputs](xref:Harp.Behavior.CameraOutputs) |
| [EnableServos](xref:Harp.Behavior.EnableServos) | 80 | U8 |  | Write | Specifies the servo outputs to enable in the device. |  | [ServoOutputs](xref:Harp.Behavior.ServoOutputs) |
| [DisableServos](xref:Harp.Behavior.DisableServos) | 81 | U8 |  | Write | Specifies the servo outputs to disable in the device. |  | [ServoOutputs](xref:Harp.Behavior.ServoOutputs) |
| [EnableEncoders](xref:Harp.Behavior.EnableEncoders) | 82 | U8 |  | Write | Specifies the port quadrature counters to enable in the device. |  | [EncoderInputs](xref:Harp.Behavior.EncoderInputs) |
| [EncoderMode](xref:Harp.Behavior.EncoderMode) | 83 | U8 |  | Write | Configures the operation mode of the quadrature encoders. |  | [EncoderModeMask](xref:Harp.Behavior.EncoderModeMask) |
| [Camera0Frame](xref:Harp.Behavior.Camera0Frame) | 92 | U8 |  | Event | Specifies that a frame was acquired on camera 0. |  | [FrameAcquired](xref:Harp.Behavior.FrameAcquired) |
| [Camera0Frequency](xref:Harp.Behavior.Camera0Frequency) | 93 | U16 |  | Write | Specifies the trigger frequency for camera 0. | [1:600] | |
| [Camera1Frame](xref:Harp.Behavior.Camera1Frame) | 94 | U8 |  | Event | Specifies that a frame was acquired on camera 1. |  | [FrameAcquired](xref:Harp.Behavior.FrameAcquired) |
| [Camera1Frequency](xref:Harp.Behavior.Camera1Frequency) | 95 | U16 |  | Write | Specifies the trigger frequency for camera 1. | [1:600] | |
| [ServoMotor2Period](xref:Harp.Behavior.ServoMotor2Period) | 100 | U16 |  | Write | Specifies the period of the servo motor in DO2, in microseconds. | [2:65534] | |
| [ServoMotor2Pulse](xref:Harp.Behavior.ServoMotor2Pulse) | 101 | U16 |  | Write | Specifies the pulse of the servo motor in DO2, in microseconds. | [6:65530] | |
| [ServoMotor3Period](xref:Harp.Behavior.ServoMotor3Period) | 102 | U16 |  | Write | Specifies the period of the servo motor in DO3, in microseconds. | [2:65534] | |
| [ServoMotor3Pulse](xref:Harp.Behavior.ServoMotor3Pulse) | 103 | U16 |  | Write | Specifies the pulse of the servo motor in DO3, in microseconds. | [6:65530] | |
| [EncoderReset](xref:Harp.Behavior.EncoderReset) | 108 | U8 |  | Write | Reset the counter of the specified encoders to zero. |  | [EncoderInputs](xref:Harp.Behavior.EncoderInputs) |
| [EnableSerialTimestamp](xref:Harp.Behavior.EnableSerialTimestamp) | 110 | U8 |  | Write | Enables the timestamp for serial TX. |  | [SerialTimestampPorts](xref:Harp.Behavior.SerialTimestampPorts) |
| [MimicPort0IR](xref:Harp.Behavior.MimicPort0IR) | 111 | U8 |  | Write | Specifies the digital output to mimic the Port 0 IR state. |  | [MimicOutput](xref:Harp.Behavior.MimicOutput) |
| [MimicPort1IR](xref:Harp.Behavior.MimicPort1IR) | 112 | U8 |  | Write | Specifies the digital output to mimic the Port 1 IR state. |  | [MimicOutput](xref:Harp.Behavior.MimicOutput) |
| [MimicPort2IR](xref:Harp.Behavior.MimicPort2IR) | 113 | U8 |  | Write | Specifies the digital output to mimic the Port 2 IR state. |  | [MimicOutput](xref:Harp.Behavior.MimicOutput) |
| [MimicPort0Valve](xref:Harp.Behavior.MimicPort0Valve) | 117 | U8 |  | Write | Specifies the digital output to mimic the Port 0 valve state. |  | [MimicOutput](xref:Harp.Behavior.MimicOutput) |
| [MimicPort1Valve](xref:Harp.Behavior.MimicPort1Valve) | 118 | U8 |  | Write | Specifies the digital output to mimic the Port 1 valve state. |  | [MimicOutput](xref:Harp.Behavior.MimicOutput) |
| [MimicPort2Valve](xref:Harp.Behavior.MimicPort2Valve) | 119 | U8 |  | Write | Specifies the digital output to mimic the Port 2 valve state. |  | [MimicOutput](xref:Harp.Behavior.MimicOutput) |
| [PokeInputFilter](xref:Harp.Behavior.PokeInputFilter) | 122 | U8 |  | Write | Specifies the low pass filter time value for poke inputs, in ms. |  | |
