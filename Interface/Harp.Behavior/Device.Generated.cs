using Bonsai;
using Bonsai.Harp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Xml.Serialization;

namespace Harp.Behavior
{
    /// <summary>
    /// Generates events and processes commands for the Behavior device connected
    /// at the specified serial port.
    /// </summary>
    [Combinator(MethodName = nameof(Generate))]
    [WorkflowElementCategory(ElementCategory.Source)]
    [Description("Generates events and processes commands for the Behavior device.")]
    public partial class Device : Bonsai.Harp.Device, INamedElement
    {
        /// <summary>
        /// Represents the unique identity class of the <see cref="Behavior"/> device.
        /// This field is constant.
        /// </summary>
        public const int WhoAmI = 1216;

        /// <summary>
        /// Initializes a new instance of the <see cref="Device"/> class.
        /// </summary>
        public Device() : base(WhoAmI) { }

        string INamedElement.Name => nameof(Behavior);

        /// <summary>
        /// Gets a read-only mapping from address to register type.
        /// </summary>
        public static new IReadOnlyDictionary<int, Type> RegisterMap { get; } = new Dictionary<int, Type>
            (Bonsai.Harp.Device.RegisterMap.ToDictionary(entry => entry.Key, entry => entry.Value))
        {
            { 32, typeof(DigitalInputState) },
            { 33, typeof(Reserved0) },
            { 34, typeof(OutputSet) },
            { 35, typeof(OutputClear) },
            { 36, typeof(OutputToggle) },
            { 37, typeof(OutputState) },
            { 38, typeof(PortDIOSet) },
            { 39, typeof(PortDIOClear) },
            { 40, typeof(PortDIOToggle) },
            { 41, typeof(PortDIOState) },
            { 42, typeof(PortDIODirection) },
            { 43, typeof(PortDIOStateEvent) },
            { 44, typeof(AnalogData) },
            { 45, typeof(OutputPulseEnable) },
            { 46, typeof(PulseDOPort0) },
            { 47, typeof(PulseDOPort1) },
            { 48, typeof(PulseDOPort2) },
            { 49, typeof(PulseSupplyPort0) },
            { 50, typeof(PulseSupplyPort1) },
            { 51, typeof(PulseSupplyPort2) },
            { 52, typeof(PulseLed0) },
            { 53, typeof(PulseLed1) },
            { 54, typeof(PulseRgb0) },
            { 55, typeof(PulseRgb1) },
            { 56, typeof(PulseDO0) },
            { 57, typeof(PulseDO1) },
            { 58, typeof(PulseDO2) },
            { 59, typeof(PulseDO3) },
            { 60, typeof(PwmFrequencyDO0) },
            { 61, typeof(PwmFrequencyDO1) },
            { 62, typeof(PwmFrequencyDO2) },
            { 63, typeof(PwmFrequencyDO3) },
            { 64, typeof(PwmDutyCycleDO0) },
            { 65, typeof(PwmDutyCycleDO1) },
            { 66, typeof(PwmDutyCycleDO2) },
            { 67, typeof(PwmDutyCycleDO3) },
            { 68, typeof(PwmStart) },
            { 69, typeof(PwmStop) },
            { 70, typeof(RgbAll) },
            { 71, typeof(Rgb0) },
            { 72, typeof(Rgb1) },
            { 73, typeof(Led0Current) },
            { 74, typeof(Led1Current) },
            { 75, typeof(Led0MaxCurrent) },
            { 76, typeof(Led1MaxCurrent) },
            { 77, typeof(EventEnable) },
            { 78, typeof(StartCameras) },
            { 79, typeof(StopCameras) },
            { 80, typeof(EnableServos) },
            { 81, typeof(DisableServos) },
            { 82, typeof(EnableEncoders) },
            { 83, typeof(EncoderMode) },
            { 84, typeof(Reserved2) },
            { 85, typeof(Reserved3) },
            { 86, typeof(Reserved4) },
            { 87, typeof(Reserved5) },
            { 88, typeof(Reserved6) },
            { 89, typeof(Reserved7) },
            { 90, typeof(Reserved8) },
            { 91, typeof(Reserved9) },
            { 92, typeof(Camera0Frame) },
            { 93, typeof(Camera0Frequency) },
            { 94, typeof(Camera1Frame) },
            { 95, typeof(Camera1Frequency) },
            { 96, typeof(Reserved10) },
            { 97, typeof(Reserved11) },
            { 98, typeof(Reserved12) },
            { 99, typeof(Reserved13) },
            { 100, typeof(ServoMotor2Period) },
            { 101, typeof(ServoMotor2Pulse) },
            { 102, typeof(ServoMotor3Period) },
            { 103, typeof(ServoMotor3Pulse) },
            { 104, typeof(Reserved14) },
            { 105, typeof(Reserved15) },
            { 106, typeof(Reserved16) },
            { 107, typeof(Reserved17) },
            { 108, typeof(EncoderReset) },
            { 109, typeof(Reserved18) },
            { 110, typeof(EnableSerialTimestamp) },
            { 111, typeof(MimicPort0IR) },
            { 112, typeof(MimicPort1IR) },
            { 113, typeof(MimicPort2IR) },
            { 114, typeof(Reserved20) },
            { 115, typeof(Reserved21) },
            { 116, typeof(Reserved22) },
            { 117, typeof(MimicPort0Valve) },
            { 118, typeof(MimicPort1Valve) },
            { 119, typeof(MimicPort2Valve) },
            { 120, typeof(Reserved23) },
            { 121, typeof(Reserved24) },
            { 122, typeof(PokeInputFilter) }
        };
    }

    /// <summary>
    /// Represents an operator that groups the sequence of <see cref="Behavior"/>" messages by register type.
    /// </summary>
    [Description("Groups the sequence of Behavior messages by register type.")]
    public partial class GroupByRegister : Combinator<HarpMessage, IGroupedObservable<Type, HarpMessage>>
    {
        /// <summary>
        /// Groups an observable sequence of <see cref="Behavior"/> messages
        /// by register type.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of observable groups, each of which corresponds to a unique
        /// <see cref="Behavior"/> register.
        /// </returns>
        public override IObservable<IGroupedObservable<Type, HarpMessage>> Process(IObservable<HarpMessage> source)
        {
            return source.GroupBy(message => Device.RegisterMap[message.Address]);
        }
    }

    /// <summary>
    /// Represents an operator that filters register-specific messages
    /// reported by the <see cref="Behavior"/> device.
    /// </summary>
    /// <seealso cref="DigitalInputState"/>
    /// <seealso cref="OutputSet"/>
    /// <seealso cref="OutputClear"/>
    /// <seealso cref="OutputToggle"/>
    /// <seealso cref="OutputState"/>
    /// <seealso cref="PortDIOSet"/>
    /// <seealso cref="PortDIOClear"/>
    /// <seealso cref="PortDIOToggle"/>
    /// <seealso cref="PortDIOState"/>
    /// <seealso cref="PortDIODirection"/>
    /// <seealso cref="PortDIOStateEvent"/>
    /// <seealso cref="AnalogData"/>
    /// <seealso cref="OutputPulseEnable"/>
    /// <seealso cref="PulseDOPort0"/>
    /// <seealso cref="PulseDOPort1"/>
    /// <seealso cref="PulseDOPort2"/>
    /// <seealso cref="PulseSupplyPort0"/>
    /// <seealso cref="PulseSupplyPort1"/>
    /// <seealso cref="PulseSupplyPort2"/>
    /// <seealso cref="PulseLed0"/>
    /// <seealso cref="PulseLed1"/>
    /// <seealso cref="PulseRgb0"/>
    /// <seealso cref="PulseRgb1"/>
    /// <seealso cref="PulseDO0"/>
    /// <seealso cref="PulseDO1"/>
    /// <seealso cref="PulseDO2"/>
    /// <seealso cref="PulseDO3"/>
    /// <seealso cref="PwmFrequencyDO0"/>
    /// <seealso cref="PwmFrequencyDO1"/>
    /// <seealso cref="PwmFrequencyDO2"/>
    /// <seealso cref="PwmFrequencyDO3"/>
    /// <seealso cref="PwmDutyCycleDO0"/>
    /// <seealso cref="PwmDutyCycleDO1"/>
    /// <seealso cref="PwmDutyCycleDO2"/>
    /// <seealso cref="PwmDutyCycleDO3"/>
    /// <seealso cref="PwmStart"/>
    /// <seealso cref="PwmStop"/>
    /// <seealso cref="RgbAll"/>
    /// <seealso cref="Rgb0"/>
    /// <seealso cref="Rgb1"/>
    /// <seealso cref="Led0Current"/>
    /// <seealso cref="Led1Current"/>
    /// <seealso cref="Led0MaxCurrent"/>
    /// <seealso cref="Led1MaxCurrent"/>
    /// <seealso cref="EventEnable"/>
    /// <seealso cref="StartCameras"/>
    /// <seealso cref="StopCameras"/>
    /// <seealso cref="EnableServos"/>
    /// <seealso cref="DisableServos"/>
    /// <seealso cref="EnableEncoders"/>
    /// <seealso cref="EncoderMode"/>
    /// <seealso cref="Camera0Frame"/>
    /// <seealso cref="Camera0Frequency"/>
    /// <seealso cref="Camera1Frame"/>
    /// <seealso cref="Camera1Frequency"/>
    /// <seealso cref="ServoMotor2Period"/>
    /// <seealso cref="ServoMotor2Pulse"/>
    /// <seealso cref="ServoMotor3Period"/>
    /// <seealso cref="ServoMotor3Pulse"/>
    /// <seealso cref="EncoderReset"/>
    /// <seealso cref="EnableSerialTimestamp"/>
    /// <seealso cref="MimicPort0IR"/>
    /// <seealso cref="MimicPort1IR"/>
    /// <seealso cref="MimicPort2IR"/>
    /// <seealso cref="MimicPort0Valve"/>
    /// <seealso cref="MimicPort1Valve"/>
    /// <seealso cref="MimicPort2Valve"/>
    /// <seealso cref="PokeInputFilter"/>
    [XmlInclude(typeof(DigitalInputState))]
    [XmlInclude(typeof(OutputSet))]
    [XmlInclude(typeof(OutputClear))]
    [XmlInclude(typeof(OutputToggle))]
    [XmlInclude(typeof(OutputState))]
    [XmlInclude(typeof(PortDIOSet))]
    [XmlInclude(typeof(PortDIOClear))]
    [XmlInclude(typeof(PortDIOToggle))]
    [XmlInclude(typeof(PortDIOState))]
    [XmlInclude(typeof(PortDIODirection))]
    [XmlInclude(typeof(PortDIOStateEvent))]
    [XmlInclude(typeof(AnalogData))]
    [XmlInclude(typeof(OutputPulseEnable))]
    [XmlInclude(typeof(PulseDOPort0))]
    [XmlInclude(typeof(PulseDOPort1))]
    [XmlInclude(typeof(PulseDOPort2))]
    [XmlInclude(typeof(PulseSupplyPort0))]
    [XmlInclude(typeof(PulseSupplyPort1))]
    [XmlInclude(typeof(PulseSupplyPort2))]
    [XmlInclude(typeof(PulseLed0))]
    [XmlInclude(typeof(PulseLed1))]
    [XmlInclude(typeof(PulseRgb0))]
    [XmlInclude(typeof(PulseRgb1))]
    [XmlInclude(typeof(PulseDO0))]
    [XmlInclude(typeof(PulseDO1))]
    [XmlInclude(typeof(PulseDO2))]
    [XmlInclude(typeof(PulseDO3))]
    [XmlInclude(typeof(PwmFrequencyDO0))]
    [XmlInclude(typeof(PwmFrequencyDO1))]
    [XmlInclude(typeof(PwmFrequencyDO2))]
    [XmlInclude(typeof(PwmFrequencyDO3))]
    [XmlInclude(typeof(PwmDutyCycleDO0))]
    [XmlInclude(typeof(PwmDutyCycleDO1))]
    [XmlInclude(typeof(PwmDutyCycleDO2))]
    [XmlInclude(typeof(PwmDutyCycleDO3))]
    [XmlInclude(typeof(PwmStart))]
    [XmlInclude(typeof(PwmStop))]
    [XmlInclude(typeof(RgbAll))]
    [XmlInclude(typeof(Rgb0))]
    [XmlInclude(typeof(Rgb1))]
    [XmlInclude(typeof(Led0Current))]
    [XmlInclude(typeof(Led1Current))]
    [XmlInclude(typeof(Led0MaxCurrent))]
    [XmlInclude(typeof(Led1MaxCurrent))]
    [XmlInclude(typeof(EventEnable))]
    [XmlInclude(typeof(StartCameras))]
    [XmlInclude(typeof(StopCameras))]
    [XmlInclude(typeof(EnableServos))]
    [XmlInclude(typeof(DisableServos))]
    [XmlInclude(typeof(EnableEncoders))]
    [XmlInclude(typeof(EncoderMode))]
    [XmlInclude(typeof(Camera0Frame))]
    [XmlInclude(typeof(Camera0Frequency))]
    [XmlInclude(typeof(Camera1Frame))]
    [XmlInclude(typeof(Camera1Frequency))]
    [XmlInclude(typeof(ServoMotor2Period))]
    [XmlInclude(typeof(ServoMotor2Pulse))]
    [XmlInclude(typeof(ServoMotor3Period))]
    [XmlInclude(typeof(ServoMotor3Pulse))]
    [XmlInclude(typeof(EncoderReset))]
    [XmlInclude(typeof(EnableSerialTimestamp))]
    [XmlInclude(typeof(MimicPort0IR))]
    [XmlInclude(typeof(MimicPort1IR))]
    [XmlInclude(typeof(MimicPort2IR))]
    [XmlInclude(typeof(MimicPort0Valve))]
    [XmlInclude(typeof(MimicPort1Valve))]
    [XmlInclude(typeof(MimicPort2Valve))]
    [XmlInclude(typeof(PokeInputFilter))]
    [Description("Filters register-specific messages reported by the Behavior device.")]
    public class FilterRegister : FilterRegisterBuilder, INamedElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FilterRegister"/> class.
        /// </summary>
        public FilterRegister()
        {
            Register = new DigitalInputState();
        }

        string INamedElement.Name
        {
            get => $"{nameof(Behavior)}.{GetElementDisplayName(Register)}";
        }
    }

    /// <summary>
    /// Represents an operator which filters and selects specific messages
    /// reported by the Behavior device.
    /// </summary>
    /// <seealso cref="DigitalInputState"/>
    /// <seealso cref="OutputSet"/>
    /// <seealso cref="OutputClear"/>
    /// <seealso cref="OutputToggle"/>
    /// <seealso cref="OutputState"/>
    /// <seealso cref="PortDIOSet"/>
    /// <seealso cref="PortDIOClear"/>
    /// <seealso cref="PortDIOToggle"/>
    /// <seealso cref="PortDIOState"/>
    /// <seealso cref="PortDIODirection"/>
    /// <seealso cref="PortDIOStateEvent"/>
    /// <seealso cref="AnalogData"/>
    /// <seealso cref="OutputPulseEnable"/>
    /// <seealso cref="PulseDOPort0"/>
    /// <seealso cref="PulseDOPort1"/>
    /// <seealso cref="PulseDOPort2"/>
    /// <seealso cref="PulseSupplyPort0"/>
    /// <seealso cref="PulseSupplyPort1"/>
    /// <seealso cref="PulseSupplyPort2"/>
    /// <seealso cref="PulseLed0"/>
    /// <seealso cref="PulseLed1"/>
    /// <seealso cref="PulseRgb0"/>
    /// <seealso cref="PulseRgb1"/>
    /// <seealso cref="PulseDO0"/>
    /// <seealso cref="PulseDO1"/>
    /// <seealso cref="PulseDO2"/>
    /// <seealso cref="PulseDO3"/>
    /// <seealso cref="PwmFrequencyDO0"/>
    /// <seealso cref="PwmFrequencyDO1"/>
    /// <seealso cref="PwmFrequencyDO2"/>
    /// <seealso cref="PwmFrequencyDO3"/>
    /// <seealso cref="PwmDutyCycleDO0"/>
    /// <seealso cref="PwmDutyCycleDO1"/>
    /// <seealso cref="PwmDutyCycleDO2"/>
    /// <seealso cref="PwmDutyCycleDO3"/>
    /// <seealso cref="PwmStart"/>
    /// <seealso cref="PwmStop"/>
    /// <seealso cref="RgbAll"/>
    /// <seealso cref="Rgb0"/>
    /// <seealso cref="Rgb1"/>
    /// <seealso cref="Led0Current"/>
    /// <seealso cref="Led1Current"/>
    /// <seealso cref="Led0MaxCurrent"/>
    /// <seealso cref="Led1MaxCurrent"/>
    /// <seealso cref="EventEnable"/>
    /// <seealso cref="StartCameras"/>
    /// <seealso cref="StopCameras"/>
    /// <seealso cref="EnableServos"/>
    /// <seealso cref="DisableServos"/>
    /// <seealso cref="EnableEncoders"/>
    /// <seealso cref="EncoderMode"/>
    /// <seealso cref="Camera0Frame"/>
    /// <seealso cref="Camera0Frequency"/>
    /// <seealso cref="Camera1Frame"/>
    /// <seealso cref="Camera1Frequency"/>
    /// <seealso cref="ServoMotor2Period"/>
    /// <seealso cref="ServoMotor2Pulse"/>
    /// <seealso cref="ServoMotor3Period"/>
    /// <seealso cref="ServoMotor3Pulse"/>
    /// <seealso cref="EncoderReset"/>
    /// <seealso cref="EnableSerialTimestamp"/>
    /// <seealso cref="MimicPort0IR"/>
    /// <seealso cref="MimicPort1IR"/>
    /// <seealso cref="MimicPort2IR"/>
    /// <seealso cref="MimicPort0Valve"/>
    /// <seealso cref="MimicPort1Valve"/>
    /// <seealso cref="MimicPort2Valve"/>
    /// <seealso cref="PokeInputFilter"/>
    [XmlInclude(typeof(DigitalInputState))]
    [XmlInclude(typeof(OutputSet))]
    [XmlInclude(typeof(OutputClear))]
    [XmlInclude(typeof(OutputToggle))]
    [XmlInclude(typeof(OutputState))]
    [XmlInclude(typeof(PortDIOSet))]
    [XmlInclude(typeof(PortDIOClear))]
    [XmlInclude(typeof(PortDIOToggle))]
    [XmlInclude(typeof(PortDIOState))]
    [XmlInclude(typeof(PortDIODirection))]
    [XmlInclude(typeof(PortDIOStateEvent))]
    [XmlInclude(typeof(AnalogData))]
    [XmlInclude(typeof(OutputPulseEnable))]
    [XmlInclude(typeof(PulseDOPort0))]
    [XmlInclude(typeof(PulseDOPort1))]
    [XmlInclude(typeof(PulseDOPort2))]
    [XmlInclude(typeof(PulseSupplyPort0))]
    [XmlInclude(typeof(PulseSupplyPort1))]
    [XmlInclude(typeof(PulseSupplyPort2))]
    [XmlInclude(typeof(PulseLed0))]
    [XmlInclude(typeof(PulseLed1))]
    [XmlInclude(typeof(PulseRgb0))]
    [XmlInclude(typeof(PulseRgb1))]
    [XmlInclude(typeof(PulseDO0))]
    [XmlInclude(typeof(PulseDO1))]
    [XmlInclude(typeof(PulseDO2))]
    [XmlInclude(typeof(PulseDO3))]
    [XmlInclude(typeof(PwmFrequencyDO0))]
    [XmlInclude(typeof(PwmFrequencyDO1))]
    [XmlInclude(typeof(PwmFrequencyDO2))]
    [XmlInclude(typeof(PwmFrequencyDO3))]
    [XmlInclude(typeof(PwmDutyCycleDO0))]
    [XmlInclude(typeof(PwmDutyCycleDO1))]
    [XmlInclude(typeof(PwmDutyCycleDO2))]
    [XmlInclude(typeof(PwmDutyCycleDO3))]
    [XmlInclude(typeof(PwmStart))]
    [XmlInclude(typeof(PwmStop))]
    [XmlInclude(typeof(RgbAll))]
    [XmlInclude(typeof(Rgb0))]
    [XmlInclude(typeof(Rgb1))]
    [XmlInclude(typeof(Led0Current))]
    [XmlInclude(typeof(Led1Current))]
    [XmlInclude(typeof(Led0MaxCurrent))]
    [XmlInclude(typeof(Led1MaxCurrent))]
    [XmlInclude(typeof(EventEnable))]
    [XmlInclude(typeof(StartCameras))]
    [XmlInclude(typeof(StopCameras))]
    [XmlInclude(typeof(EnableServos))]
    [XmlInclude(typeof(DisableServos))]
    [XmlInclude(typeof(EnableEncoders))]
    [XmlInclude(typeof(EncoderMode))]
    [XmlInclude(typeof(Camera0Frame))]
    [XmlInclude(typeof(Camera0Frequency))]
    [XmlInclude(typeof(Camera1Frame))]
    [XmlInclude(typeof(Camera1Frequency))]
    [XmlInclude(typeof(ServoMotor2Period))]
    [XmlInclude(typeof(ServoMotor2Pulse))]
    [XmlInclude(typeof(ServoMotor3Period))]
    [XmlInclude(typeof(ServoMotor3Pulse))]
    [XmlInclude(typeof(EncoderReset))]
    [XmlInclude(typeof(EnableSerialTimestamp))]
    [XmlInclude(typeof(MimicPort0IR))]
    [XmlInclude(typeof(MimicPort1IR))]
    [XmlInclude(typeof(MimicPort2IR))]
    [XmlInclude(typeof(MimicPort0Valve))]
    [XmlInclude(typeof(MimicPort1Valve))]
    [XmlInclude(typeof(MimicPort2Valve))]
    [XmlInclude(typeof(PokeInputFilter))]
    [XmlInclude(typeof(TimestampedDigitalInputState))]
    [XmlInclude(typeof(TimestampedOutputSet))]
    [XmlInclude(typeof(TimestampedOutputClear))]
    [XmlInclude(typeof(TimestampedOutputToggle))]
    [XmlInclude(typeof(TimestampedOutputState))]
    [XmlInclude(typeof(TimestampedPortDIOSet))]
    [XmlInclude(typeof(TimestampedPortDIOClear))]
    [XmlInclude(typeof(TimestampedPortDIOToggle))]
    [XmlInclude(typeof(TimestampedPortDIOState))]
    [XmlInclude(typeof(TimestampedPortDIODirection))]
    [XmlInclude(typeof(TimestampedPortDIOStateEvent))]
    [XmlInclude(typeof(TimestampedAnalogData))]
    [XmlInclude(typeof(TimestampedOutputPulseEnable))]
    [XmlInclude(typeof(TimestampedPulseDOPort0))]
    [XmlInclude(typeof(TimestampedPulseDOPort1))]
    [XmlInclude(typeof(TimestampedPulseDOPort2))]
    [XmlInclude(typeof(TimestampedPulseSupplyPort0))]
    [XmlInclude(typeof(TimestampedPulseSupplyPort1))]
    [XmlInclude(typeof(TimestampedPulseSupplyPort2))]
    [XmlInclude(typeof(TimestampedPulseLed0))]
    [XmlInclude(typeof(TimestampedPulseLed1))]
    [XmlInclude(typeof(TimestampedPulseRgb0))]
    [XmlInclude(typeof(TimestampedPulseRgb1))]
    [XmlInclude(typeof(TimestampedPulseDO0))]
    [XmlInclude(typeof(TimestampedPulseDO1))]
    [XmlInclude(typeof(TimestampedPulseDO2))]
    [XmlInclude(typeof(TimestampedPulseDO3))]
    [XmlInclude(typeof(TimestampedPwmFrequencyDO0))]
    [XmlInclude(typeof(TimestampedPwmFrequencyDO1))]
    [XmlInclude(typeof(TimestampedPwmFrequencyDO2))]
    [XmlInclude(typeof(TimestampedPwmFrequencyDO3))]
    [XmlInclude(typeof(TimestampedPwmDutyCycleDO0))]
    [XmlInclude(typeof(TimestampedPwmDutyCycleDO1))]
    [XmlInclude(typeof(TimestampedPwmDutyCycleDO2))]
    [XmlInclude(typeof(TimestampedPwmDutyCycleDO3))]
    [XmlInclude(typeof(TimestampedPwmStart))]
    [XmlInclude(typeof(TimestampedPwmStop))]
    [XmlInclude(typeof(TimestampedRgbAll))]
    [XmlInclude(typeof(TimestampedRgb0))]
    [XmlInclude(typeof(TimestampedRgb1))]
    [XmlInclude(typeof(TimestampedLed0Current))]
    [XmlInclude(typeof(TimestampedLed1Current))]
    [XmlInclude(typeof(TimestampedLed0MaxCurrent))]
    [XmlInclude(typeof(TimestampedLed1MaxCurrent))]
    [XmlInclude(typeof(TimestampedEventEnable))]
    [XmlInclude(typeof(TimestampedStartCameras))]
    [XmlInclude(typeof(TimestampedStopCameras))]
    [XmlInclude(typeof(TimestampedEnableServos))]
    [XmlInclude(typeof(TimestampedDisableServos))]
    [XmlInclude(typeof(TimestampedEnableEncoders))]
    [XmlInclude(typeof(TimestampedEncoderMode))]
    [XmlInclude(typeof(TimestampedCamera0Frame))]
    [XmlInclude(typeof(TimestampedCamera0Frequency))]
    [XmlInclude(typeof(TimestampedCamera1Frame))]
    [XmlInclude(typeof(TimestampedCamera1Frequency))]
    [XmlInclude(typeof(TimestampedServoMotor2Period))]
    [XmlInclude(typeof(TimestampedServoMotor2Pulse))]
    [XmlInclude(typeof(TimestampedServoMotor3Period))]
    [XmlInclude(typeof(TimestampedServoMotor3Pulse))]
    [XmlInclude(typeof(TimestampedEncoderReset))]
    [XmlInclude(typeof(TimestampedEnableSerialTimestamp))]
    [XmlInclude(typeof(TimestampedMimicPort0IR))]
    [XmlInclude(typeof(TimestampedMimicPort1IR))]
    [XmlInclude(typeof(TimestampedMimicPort2IR))]
    [XmlInclude(typeof(TimestampedMimicPort0Valve))]
    [XmlInclude(typeof(TimestampedMimicPort1Valve))]
    [XmlInclude(typeof(TimestampedMimicPort2Valve))]
    [XmlInclude(typeof(TimestampedPokeInputFilter))]
    [Description("Filters and selects specific messages reported by the Behavior device.")]
    public partial class Parse : ParseBuilder, INamedElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Parse"/> class.
        /// </summary>
        public Parse()
        {
            Register = new DigitalInputState();
        }

        string INamedElement.Name => $"{nameof(Behavior)}.{GetElementDisplayName(Register)}";
    }

    /// <summary>
    /// Represents an operator which formats a sequence of values as specific
    /// Behavior register messages.
    /// </summary>
    /// <seealso cref="DigitalInputState"/>
    /// <seealso cref="OutputSet"/>
    /// <seealso cref="OutputClear"/>
    /// <seealso cref="OutputToggle"/>
    /// <seealso cref="OutputState"/>
    /// <seealso cref="PortDIOSet"/>
    /// <seealso cref="PortDIOClear"/>
    /// <seealso cref="PortDIOToggle"/>
    /// <seealso cref="PortDIOState"/>
    /// <seealso cref="PortDIODirection"/>
    /// <seealso cref="PortDIOStateEvent"/>
    /// <seealso cref="AnalogData"/>
    /// <seealso cref="OutputPulseEnable"/>
    /// <seealso cref="PulseDOPort0"/>
    /// <seealso cref="PulseDOPort1"/>
    /// <seealso cref="PulseDOPort2"/>
    /// <seealso cref="PulseSupplyPort0"/>
    /// <seealso cref="PulseSupplyPort1"/>
    /// <seealso cref="PulseSupplyPort2"/>
    /// <seealso cref="PulseLed0"/>
    /// <seealso cref="PulseLed1"/>
    /// <seealso cref="PulseRgb0"/>
    /// <seealso cref="PulseRgb1"/>
    /// <seealso cref="PulseDO0"/>
    /// <seealso cref="PulseDO1"/>
    /// <seealso cref="PulseDO2"/>
    /// <seealso cref="PulseDO3"/>
    /// <seealso cref="PwmFrequencyDO0"/>
    /// <seealso cref="PwmFrequencyDO1"/>
    /// <seealso cref="PwmFrequencyDO2"/>
    /// <seealso cref="PwmFrequencyDO3"/>
    /// <seealso cref="PwmDutyCycleDO0"/>
    /// <seealso cref="PwmDutyCycleDO1"/>
    /// <seealso cref="PwmDutyCycleDO2"/>
    /// <seealso cref="PwmDutyCycleDO3"/>
    /// <seealso cref="PwmStart"/>
    /// <seealso cref="PwmStop"/>
    /// <seealso cref="RgbAll"/>
    /// <seealso cref="Rgb0"/>
    /// <seealso cref="Rgb1"/>
    /// <seealso cref="Led0Current"/>
    /// <seealso cref="Led1Current"/>
    /// <seealso cref="Led0MaxCurrent"/>
    /// <seealso cref="Led1MaxCurrent"/>
    /// <seealso cref="EventEnable"/>
    /// <seealso cref="StartCameras"/>
    /// <seealso cref="StopCameras"/>
    /// <seealso cref="EnableServos"/>
    /// <seealso cref="DisableServos"/>
    /// <seealso cref="EnableEncoders"/>
    /// <seealso cref="EncoderMode"/>
    /// <seealso cref="Camera0Frame"/>
    /// <seealso cref="Camera0Frequency"/>
    /// <seealso cref="Camera1Frame"/>
    /// <seealso cref="Camera1Frequency"/>
    /// <seealso cref="ServoMotor2Period"/>
    /// <seealso cref="ServoMotor2Pulse"/>
    /// <seealso cref="ServoMotor3Period"/>
    /// <seealso cref="ServoMotor3Pulse"/>
    /// <seealso cref="EncoderReset"/>
    /// <seealso cref="EnableSerialTimestamp"/>
    /// <seealso cref="MimicPort0IR"/>
    /// <seealso cref="MimicPort1IR"/>
    /// <seealso cref="MimicPort2IR"/>
    /// <seealso cref="MimicPort0Valve"/>
    /// <seealso cref="MimicPort1Valve"/>
    /// <seealso cref="MimicPort2Valve"/>
    /// <seealso cref="PokeInputFilter"/>
    [XmlInclude(typeof(DigitalInputState))]
    [XmlInclude(typeof(OutputSet))]
    [XmlInclude(typeof(OutputClear))]
    [XmlInclude(typeof(OutputToggle))]
    [XmlInclude(typeof(OutputState))]
    [XmlInclude(typeof(PortDIOSet))]
    [XmlInclude(typeof(PortDIOClear))]
    [XmlInclude(typeof(PortDIOToggle))]
    [XmlInclude(typeof(PortDIOState))]
    [XmlInclude(typeof(PortDIODirection))]
    [XmlInclude(typeof(PortDIOStateEvent))]
    [XmlInclude(typeof(AnalogData))]
    [XmlInclude(typeof(OutputPulseEnable))]
    [XmlInclude(typeof(PulseDOPort0))]
    [XmlInclude(typeof(PulseDOPort1))]
    [XmlInclude(typeof(PulseDOPort2))]
    [XmlInclude(typeof(PulseSupplyPort0))]
    [XmlInclude(typeof(PulseSupplyPort1))]
    [XmlInclude(typeof(PulseSupplyPort2))]
    [XmlInclude(typeof(PulseLed0))]
    [XmlInclude(typeof(PulseLed1))]
    [XmlInclude(typeof(PulseRgb0))]
    [XmlInclude(typeof(PulseRgb1))]
    [XmlInclude(typeof(PulseDO0))]
    [XmlInclude(typeof(PulseDO1))]
    [XmlInclude(typeof(PulseDO2))]
    [XmlInclude(typeof(PulseDO3))]
    [XmlInclude(typeof(PwmFrequencyDO0))]
    [XmlInclude(typeof(PwmFrequencyDO1))]
    [XmlInclude(typeof(PwmFrequencyDO2))]
    [XmlInclude(typeof(PwmFrequencyDO3))]
    [XmlInclude(typeof(PwmDutyCycleDO0))]
    [XmlInclude(typeof(PwmDutyCycleDO1))]
    [XmlInclude(typeof(PwmDutyCycleDO2))]
    [XmlInclude(typeof(PwmDutyCycleDO3))]
    [XmlInclude(typeof(PwmStart))]
    [XmlInclude(typeof(PwmStop))]
    [XmlInclude(typeof(RgbAll))]
    [XmlInclude(typeof(Rgb0))]
    [XmlInclude(typeof(Rgb1))]
    [XmlInclude(typeof(Led0Current))]
    [XmlInclude(typeof(Led1Current))]
    [XmlInclude(typeof(Led0MaxCurrent))]
    [XmlInclude(typeof(Led1MaxCurrent))]
    [XmlInclude(typeof(EventEnable))]
    [XmlInclude(typeof(StartCameras))]
    [XmlInclude(typeof(StopCameras))]
    [XmlInclude(typeof(EnableServos))]
    [XmlInclude(typeof(DisableServos))]
    [XmlInclude(typeof(EnableEncoders))]
    [XmlInclude(typeof(EncoderMode))]
    [XmlInclude(typeof(Camera0Frame))]
    [XmlInclude(typeof(Camera0Frequency))]
    [XmlInclude(typeof(Camera1Frame))]
    [XmlInclude(typeof(Camera1Frequency))]
    [XmlInclude(typeof(ServoMotor2Period))]
    [XmlInclude(typeof(ServoMotor2Pulse))]
    [XmlInclude(typeof(ServoMotor3Period))]
    [XmlInclude(typeof(ServoMotor3Pulse))]
    [XmlInclude(typeof(EncoderReset))]
    [XmlInclude(typeof(EnableSerialTimestamp))]
    [XmlInclude(typeof(MimicPort0IR))]
    [XmlInclude(typeof(MimicPort1IR))]
    [XmlInclude(typeof(MimicPort2IR))]
    [XmlInclude(typeof(MimicPort0Valve))]
    [XmlInclude(typeof(MimicPort1Valve))]
    [XmlInclude(typeof(MimicPort2Valve))]
    [XmlInclude(typeof(PokeInputFilter))]
    [Description("Formats a sequence of values as specific Behavior register messages.")]
    public partial class Format : FormatBuilder, INamedElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Format"/> class.
        /// </summary>
        public Format()
        {
            Register = new DigitalInputState();
        }

        string INamedElement.Name => $"{nameof(Behavior)}.{GetElementDisplayName(Register)}";
    }

    /// <summary>
    /// Represents a register that reflects the state of DI digital lines of each Port.
    /// </summary>
    [Description("Reflects the state of DI digital lines of each Port")]
    public partial class DigitalInputState
    {
        /// <summary>
        /// Represents the address of the <see cref="DigitalInputState"/> register. This field is constant.
        /// </summary>
        public const int Address = 32;

        /// <summary>
        /// Represents the payload type of the <see cref="DigitalInputState"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="DigitalInputState"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DigitalInputState"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static DigitalInputs GetPayload(HarpMessage message)
        {
            return (DigitalInputs)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DigitalInputState"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalInputs> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((DigitalInputs)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DigitalInputState"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DigitalInputState"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, DigitalInputs value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DigitalInputState"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DigitalInputState"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, DigitalInputs value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DigitalInputState register.
    /// </summary>
    /// <seealso cref="DigitalInputState"/>
    [Description("Filters and selects timestamped messages from the DigitalInputState register.")]
    public partial class TimestampedDigitalInputState
    {
        /// <summary>
        /// Represents the address of the <see cref="DigitalInputState"/> register. This field is constant.
        /// </summary>
        public const int Address = DigitalInputState.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DigitalInputState"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalInputs> GetPayload(HarpMessage message)
        {
            return DigitalInputState.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that reserved for future use.
    /// </summary>
    [Description("Reserved for future use")]
    internal partial class Reserved0
    {
        /// <summary>
        /// Represents the address of the <see cref="Reserved0"/> register. This field is constant.
        /// </summary>
        public const int Address = 33;

        /// <summary>
        /// Represents the payload type of the <see cref="Reserved0"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Reserved0"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;
    }

    /// <summary>
    /// Represents a register that set the specified digital output lines.
    /// </summary>
    [Description("Set the specified digital output lines.")]
    public partial class OutputSet
    {
        /// <summary>
        /// Represents the address of the <see cref="OutputSet"/> register. This field is constant.
        /// </summary>
        public const int Address = 34;

        /// <summary>
        /// Represents the payload type of the <see cref="OutputSet"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="OutputSet"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="OutputSet"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static DigitalOutputs GetPayload(HarpMessage message)
        {
            return (DigitalOutputs)message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="OutputSet"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalOutputs> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadUInt16();
            return Timestamped.Create((DigitalOutputs)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="OutputSet"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="OutputSet"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, DigitalOutputs value)
        {
            return HarpMessage.FromUInt16(Address, messageType, (ushort)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="OutputSet"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="OutputSet"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, DigitalOutputs value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, (ushort)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// OutputSet register.
    /// </summary>
    /// <seealso cref="OutputSet"/>
    [Description("Filters and selects timestamped messages from the OutputSet register.")]
    public partial class TimestampedOutputSet
    {
        /// <summary>
        /// Represents the address of the <see cref="OutputSet"/> register. This field is constant.
        /// </summary>
        public const int Address = OutputSet.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="OutputSet"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalOutputs> GetPayload(HarpMessage message)
        {
            return OutputSet.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that clear the specified digital output lines.
    /// </summary>
    [Description("Clear the specified digital output lines")]
    public partial class OutputClear
    {
        /// <summary>
        /// Represents the address of the <see cref="OutputClear"/> register. This field is constant.
        /// </summary>
        public const int Address = 35;

        /// <summary>
        /// Represents the payload type of the <see cref="OutputClear"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="OutputClear"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="OutputClear"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static DigitalOutputs GetPayload(HarpMessage message)
        {
            return (DigitalOutputs)message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="OutputClear"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalOutputs> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadUInt16();
            return Timestamped.Create((DigitalOutputs)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="OutputClear"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="OutputClear"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, DigitalOutputs value)
        {
            return HarpMessage.FromUInt16(Address, messageType, (ushort)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="OutputClear"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="OutputClear"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, DigitalOutputs value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, (ushort)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// OutputClear register.
    /// </summary>
    /// <seealso cref="OutputClear"/>
    [Description("Filters and selects timestamped messages from the OutputClear register.")]
    public partial class TimestampedOutputClear
    {
        /// <summary>
        /// Represents the address of the <see cref="OutputClear"/> register. This field is constant.
        /// </summary>
        public const int Address = OutputClear.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="OutputClear"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalOutputs> GetPayload(HarpMessage message)
        {
            return OutputClear.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that toggle the specified digital output lines.
    /// </summary>
    [Description("Toggle the specified digital output lines")]
    public partial class OutputToggle
    {
        /// <summary>
        /// Represents the address of the <see cref="OutputToggle"/> register. This field is constant.
        /// </summary>
        public const int Address = 36;

        /// <summary>
        /// Represents the payload type of the <see cref="OutputToggle"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="OutputToggle"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="OutputToggle"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static DigitalOutputs GetPayload(HarpMessage message)
        {
            return (DigitalOutputs)message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="OutputToggle"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalOutputs> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadUInt16();
            return Timestamped.Create((DigitalOutputs)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="OutputToggle"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="OutputToggle"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, DigitalOutputs value)
        {
            return HarpMessage.FromUInt16(Address, messageType, (ushort)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="OutputToggle"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="OutputToggle"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, DigitalOutputs value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, (ushort)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// OutputToggle register.
    /// </summary>
    /// <seealso cref="OutputToggle"/>
    [Description("Filters and selects timestamped messages from the OutputToggle register.")]
    public partial class TimestampedOutputToggle
    {
        /// <summary>
        /// Represents the address of the <see cref="OutputToggle"/> register. This field is constant.
        /// </summary>
        public const int Address = OutputToggle.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="OutputToggle"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalOutputs> GetPayload(HarpMessage message)
        {
            return OutputToggle.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that write the state of all digital output lines.
    /// </summary>
    [Description("Write the state of all digital output lines")]
    public partial class OutputState
    {
        /// <summary>
        /// Represents the address of the <see cref="OutputState"/> register. This field is constant.
        /// </summary>
        public const int Address = 37;

        /// <summary>
        /// Represents the payload type of the <see cref="OutputState"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="OutputState"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="OutputState"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static DigitalOutputs GetPayload(HarpMessage message)
        {
            return (DigitalOutputs)message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="OutputState"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalOutputs> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadUInt16();
            return Timestamped.Create((DigitalOutputs)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="OutputState"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="OutputState"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, DigitalOutputs value)
        {
            return HarpMessage.FromUInt16(Address, messageType, (ushort)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="OutputState"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="OutputState"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, DigitalOutputs value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, (ushort)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// OutputState register.
    /// </summary>
    /// <seealso cref="OutputState"/>
    [Description("Filters and selects timestamped messages from the OutputState register.")]
    public partial class TimestampedOutputState
    {
        /// <summary>
        /// Represents the address of the <see cref="OutputState"/> register. This field is constant.
        /// </summary>
        public const int Address = OutputState.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="OutputState"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalOutputs> GetPayload(HarpMessage message)
        {
            return OutputState.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that set the specified port DIO lines.
    /// </summary>
    [Description("Set the specified port DIO lines")]
    public partial class PortDIOSet
    {
        /// <summary>
        /// Represents the address of the <see cref="PortDIOSet"/> register. This field is constant.
        /// </summary>
        public const int Address = 38;

        /// <summary>
        /// Represents the payload type of the <see cref="PortDIOSet"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="PortDIOSet"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="PortDIOSet"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static PortDigitalIOS GetPayload(HarpMessage message)
        {
            return (PortDigitalIOS)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="PortDIOSet"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<PortDigitalIOS> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((PortDigitalIOS)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="PortDIOSet"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="PortDIOSet"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, PortDigitalIOS value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="PortDIOSet"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="PortDIOSet"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, PortDigitalIOS value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// PortDIOSet register.
    /// </summary>
    /// <seealso cref="PortDIOSet"/>
    [Description("Filters and selects timestamped messages from the PortDIOSet register.")]
    public partial class TimestampedPortDIOSet
    {
        /// <summary>
        /// Represents the address of the <see cref="PortDIOSet"/> register. This field is constant.
        /// </summary>
        public const int Address = PortDIOSet.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="PortDIOSet"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<PortDigitalIOS> GetPayload(HarpMessage message)
        {
            return PortDIOSet.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that clear the specified port DIO lines.
    /// </summary>
    [Description("Clear the specified port DIO lines")]
    public partial class PortDIOClear
    {
        /// <summary>
        /// Represents the address of the <see cref="PortDIOClear"/> register. This field is constant.
        /// </summary>
        public const int Address = 39;

        /// <summary>
        /// Represents the payload type of the <see cref="PortDIOClear"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="PortDIOClear"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="PortDIOClear"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static PortDigitalIOS GetPayload(HarpMessage message)
        {
            return (PortDigitalIOS)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="PortDIOClear"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<PortDigitalIOS> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((PortDigitalIOS)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="PortDIOClear"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="PortDIOClear"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, PortDigitalIOS value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="PortDIOClear"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="PortDIOClear"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, PortDigitalIOS value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// PortDIOClear register.
    /// </summary>
    /// <seealso cref="PortDIOClear"/>
    [Description("Filters and selects timestamped messages from the PortDIOClear register.")]
    public partial class TimestampedPortDIOClear
    {
        /// <summary>
        /// Represents the address of the <see cref="PortDIOClear"/> register. This field is constant.
        /// </summary>
        public const int Address = PortDIOClear.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="PortDIOClear"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<PortDigitalIOS> GetPayload(HarpMessage message)
        {
            return PortDIOClear.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that toggle the specified port DIO lines.
    /// </summary>
    [Description("Toggle the specified port DIO lines")]
    public partial class PortDIOToggle
    {
        /// <summary>
        /// Represents the address of the <see cref="PortDIOToggle"/> register. This field is constant.
        /// </summary>
        public const int Address = 40;

        /// <summary>
        /// Represents the payload type of the <see cref="PortDIOToggle"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="PortDIOToggle"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="PortDIOToggle"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static PortDigitalIOS GetPayload(HarpMessage message)
        {
            return (PortDigitalIOS)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="PortDIOToggle"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<PortDigitalIOS> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((PortDigitalIOS)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="PortDIOToggle"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="PortDIOToggle"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, PortDigitalIOS value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="PortDIOToggle"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="PortDIOToggle"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, PortDigitalIOS value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// PortDIOToggle register.
    /// </summary>
    /// <seealso cref="PortDIOToggle"/>
    [Description("Filters and selects timestamped messages from the PortDIOToggle register.")]
    public partial class TimestampedPortDIOToggle
    {
        /// <summary>
        /// Represents the address of the <see cref="PortDIOToggle"/> register. This field is constant.
        /// </summary>
        public const int Address = PortDIOToggle.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="PortDIOToggle"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<PortDigitalIOS> GetPayload(HarpMessage message)
        {
            return PortDIOToggle.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that write the state of all port DIO lines.
    /// </summary>
    [Description("Write the state of all port DIO lines")]
    public partial class PortDIOState
    {
        /// <summary>
        /// Represents the address of the <see cref="PortDIOState"/> register. This field is constant.
        /// </summary>
        public const int Address = 41;

        /// <summary>
        /// Represents the payload type of the <see cref="PortDIOState"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="PortDIOState"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="PortDIOState"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static PortDigitalIOS GetPayload(HarpMessage message)
        {
            return (PortDigitalIOS)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="PortDIOState"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<PortDigitalIOS> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((PortDigitalIOS)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="PortDIOState"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="PortDIOState"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, PortDigitalIOS value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="PortDIOState"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="PortDIOState"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, PortDigitalIOS value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// PortDIOState register.
    /// </summary>
    /// <seealso cref="PortDIOState"/>
    [Description("Filters and selects timestamped messages from the PortDIOState register.")]
    public partial class TimestampedPortDIOState
    {
        /// <summary>
        /// Represents the address of the <see cref="PortDIOState"/> register. This field is constant.
        /// </summary>
        public const int Address = PortDIOState.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="PortDIOState"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<PortDigitalIOS> GetPayload(HarpMessage message)
        {
            return PortDIOState.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that specifies which of the port DIO lines are outputs.
    /// </summary>
    [Description("Specifies which of the port DIO lines are outputs")]
    public partial class PortDIODirection
    {
        /// <summary>
        /// Represents the address of the <see cref="PortDIODirection"/> register. This field is constant.
        /// </summary>
        public const int Address = 42;

        /// <summary>
        /// Represents the payload type of the <see cref="PortDIODirection"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="PortDIODirection"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="PortDIODirection"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static PortDigitalIOS GetPayload(HarpMessage message)
        {
            return (PortDigitalIOS)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="PortDIODirection"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<PortDigitalIOS> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((PortDigitalIOS)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="PortDIODirection"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="PortDIODirection"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, PortDigitalIOS value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="PortDIODirection"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="PortDIODirection"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, PortDigitalIOS value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// PortDIODirection register.
    /// </summary>
    /// <seealso cref="PortDIODirection"/>
    [Description("Filters and selects timestamped messages from the PortDIODirection register.")]
    public partial class TimestampedPortDIODirection
    {
        /// <summary>
        /// Represents the address of the <see cref="PortDIODirection"/> register. This field is constant.
        /// </summary>
        public const int Address = PortDIODirection.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="PortDIODirection"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<PortDigitalIOS> GetPayload(HarpMessage message)
        {
            return PortDIODirection.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that specifies the state of the port DIO lines on a line change.
    /// </summary>
    [Description("Specifies the state of the port DIO lines on a line change")]
    public partial class PortDIOStateEvent
    {
        /// <summary>
        /// Represents the address of the <see cref="PortDIOStateEvent"/> register. This field is constant.
        /// </summary>
        public const int Address = 43;

        /// <summary>
        /// Represents the payload type of the <see cref="PortDIOStateEvent"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="PortDIOStateEvent"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="PortDIOStateEvent"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static PortDigitalIOS GetPayload(HarpMessage message)
        {
            return (PortDigitalIOS)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="PortDIOStateEvent"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<PortDigitalIOS> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((PortDigitalIOS)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="PortDIOStateEvent"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="PortDIOStateEvent"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, PortDigitalIOS value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="PortDIOStateEvent"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="PortDIOStateEvent"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, PortDigitalIOS value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// PortDIOStateEvent register.
    /// </summary>
    /// <seealso cref="PortDIOStateEvent"/>
    [Description("Filters and selects timestamped messages from the PortDIOStateEvent register.")]
    public partial class TimestampedPortDIOStateEvent
    {
        /// <summary>
        /// Represents the address of the <see cref="PortDIOStateEvent"/> register. This field is constant.
        /// </summary>
        public const int Address = PortDIOStateEvent.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="PortDIOStateEvent"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<PortDigitalIOS> GetPayload(HarpMessage message)
        {
            return PortDIOStateEvent.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that voltage at the ADC input and encoder value on Port 2.
    /// </summary>
    [Description("Voltage at the ADC input and encoder value on Port 2")]
    public partial class AnalogData
    {
        /// <summary>
        /// Represents the address of the <see cref="AnalogData"/> register. This field is constant.
        /// </summary>
        public const int Address = 44;

        /// <summary>
        /// Represents the payload type of the <see cref="AnalogData"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.S16;

        /// <summary>
        /// Represents the length of the <see cref="AnalogData"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 3;

        static AnalogDataPayload ParsePayload(short[] payload)
        {
            AnalogDataPayload result;
            result.AnalogInput0 = payload[0];
            result.Encoder = payload[1];
            result.AnalogInput1 = payload[2];
            return result;
        }

        static short[] FormatPayload(AnalogDataPayload value)
        {
            short[] result;
            result = new short[3];
            result[0] = value.AnalogInput0;
            result[1] = value.Encoder;
            result[2] = value.AnalogInput1;
            return result;
        }

        /// <summary>
        /// Returns the payload data for <see cref="AnalogData"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static AnalogDataPayload GetPayload(HarpMessage message)
        {
            return ParsePayload(message.GetPayloadArray<short>());
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="AnalogData"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<AnalogDataPayload> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadArray<short>();
            return Timestamped.Create(ParsePayload(payload.Value), payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="AnalogData"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="AnalogData"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, AnalogDataPayload value)
        {
            return HarpMessage.FromInt16(Address, messageType, FormatPayload(value));
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="AnalogData"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="AnalogData"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, AnalogDataPayload value)
        {
            return HarpMessage.FromInt16(Address, timestamp, messageType, FormatPayload(value));
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// AnalogData register.
    /// </summary>
    /// <seealso cref="AnalogData"/>
    [Description("Filters and selects timestamped messages from the AnalogData register.")]
    public partial class TimestampedAnalogData
    {
        /// <summary>
        /// Represents the address of the <see cref="AnalogData"/> register. This field is constant.
        /// </summary>
        public const int Address = AnalogData.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="AnalogData"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<AnalogDataPayload> GetPayload(HarpMessage message)
        {
            return AnalogData.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that enables the pulse function for the specified output lines.
    /// </summary>
    [Description("Enables the pulse function for the specified output lines")]
    public partial class OutputPulseEnable
    {
        /// <summary>
        /// Represents the address of the <see cref="OutputPulseEnable"/> register. This field is constant.
        /// </summary>
        public const int Address = 45;

        /// <summary>
        /// Represents the payload type of the <see cref="OutputPulseEnable"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="OutputPulseEnable"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="OutputPulseEnable"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static DigitalOutputs GetPayload(HarpMessage message)
        {
            return (DigitalOutputs)message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="OutputPulseEnable"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalOutputs> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadUInt16();
            return Timestamped.Create((DigitalOutputs)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="OutputPulseEnable"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="OutputPulseEnable"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, DigitalOutputs value)
        {
            return HarpMessage.FromUInt16(Address, messageType, (ushort)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="OutputPulseEnable"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="OutputPulseEnable"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, DigitalOutputs value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, (ushort)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// OutputPulseEnable register.
    /// </summary>
    /// <seealso cref="OutputPulseEnable"/>
    [Description("Filters and selects timestamped messages from the OutputPulseEnable register.")]
    public partial class TimestampedOutputPulseEnable
    {
        /// <summary>
        /// Represents the address of the <see cref="OutputPulseEnable"/> register. This field is constant.
        /// </summary>
        public const int Address = OutputPulseEnable.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="OutputPulseEnable"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalOutputs> GetPayload(HarpMessage message)
        {
            return OutputPulseEnable.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that specifies the duration of the output pulse in milliseconds.
    /// </summary>
    [Description("Specifies the duration of the output pulse in milliseconds.")]
    public partial class PulseDOPort0
    {
        /// <summary>
        /// Represents the address of the <see cref="PulseDOPort0"/> register. This field is constant.
        /// </summary>
        public const int Address = 46;

        /// <summary>
        /// Represents the payload type of the <see cref="PulseDOPort0"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="PulseDOPort0"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="PulseDOPort0"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="PulseDOPort0"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="PulseDOPort0"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="PulseDOPort0"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="PulseDOPort0"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="PulseDOPort0"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// PulseDOPort0 register.
    /// </summary>
    /// <seealso cref="PulseDOPort0"/>
    [Description("Filters and selects timestamped messages from the PulseDOPort0 register.")]
    public partial class TimestampedPulseDOPort0
    {
        /// <summary>
        /// Represents the address of the <see cref="PulseDOPort0"/> register. This field is constant.
        /// </summary>
        public const int Address = PulseDOPort0.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="PulseDOPort0"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return PulseDOPort0.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that specifies the duration of the output pulse in milliseconds.
    /// </summary>
    [Description("Specifies the duration of the output pulse in milliseconds.")]
    public partial class PulseDOPort1
    {
        /// <summary>
        /// Represents the address of the <see cref="PulseDOPort1"/> register. This field is constant.
        /// </summary>
        public const int Address = 47;

        /// <summary>
        /// Represents the payload type of the <see cref="PulseDOPort1"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="PulseDOPort1"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="PulseDOPort1"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="PulseDOPort1"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="PulseDOPort1"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="PulseDOPort1"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="PulseDOPort1"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="PulseDOPort1"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// PulseDOPort1 register.
    /// </summary>
    /// <seealso cref="PulseDOPort1"/>
    [Description("Filters and selects timestamped messages from the PulseDOPort1 register.")]
    public partial class TimestampedPulseDOPort1
    {
        /// <summary>
        /// Represents the address of the <see cref="PulseDOPort1"/> register. This field is constant.
        /// </summary>
        public const int Address = PulseDOPort1.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="PulseDOPort1"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return PulseDOPort1.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that specifies the duration of the output pulse in milliseconds.
    /// </summary>
    [Description("Specifies the duration of the output pulse in milliseconds.")]
    public partial class PulseDOPort2
    {
        /// <summary>
        /// Represents the address of the <see cref="PulseDOPort2"/> register. This field is constant.
        /// </summary>
        public const int Address = 48;

        /// <summary>
        /// Represents the payload type of the <see cref="PulseDOPort2"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="PulseDOPort2"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="PulseDOPort2"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="PulseDOPort2"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="PulseDOPort2"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="PulseDOPort2"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="PulseDOPort2"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="PulseDOPort2"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// PulseDOPort2 register.
    /// </summary>
    /// <seealso cref="PulseDOPort2"/>
    [Description("Filters and selects timestamped messages from the PulseDOPort2 register.")]
    public partial class TimestampedPulseDOPort2
    {
        /// <summary>
        /// Represents the address of the <see cref="PulseDOPort2"/> register. This field is constant.
        /// </summary>
        public const int Address = PulseDOPort2.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="PulseDOPort2"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return PulseDOPort2.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that specifies the duration of the output pulse in milliseconds.
    /// </summary>
    [Description("Specifies the duration of the output pulse in milliseconds.")]
    public partial class PulseSupplyPort0
    {
        /// <summary>
        /// Represents the address of the <see cref="PulseSupplyPort0"/> register. This field is constant.
        /// </summary>
        public const int Address = 49;

        /// <summary>
        /// Represents the payload type of the <see cref="PulseSupplyPort0"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="PulseSupplyPort0"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="PulseSupplyPort0"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="PulseSupplyPort0"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="PulseSupplyPort0"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="PulseSupplyPort0"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="PulseSupplyPort0"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="PulseSupplyPort0"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// PulseSupplyPort0 register.
    /// </summary>
    /// <seealso cref="PulseSupplyPort0"/>
    [Description("Filters and selects timestamped messages from the PulseSupplyPort0 register.")]
    public partial class TimestampedPulseSupplyPort0
    {
        /// <summary>
        /// Represents the address of the <see cref="PulseSupplyPort0"/> register. This field is constant.
        /// </summary>
        public const int Address = PulseSupplyPort0.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="PulseSupplyPort0"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return PulseSupplyPort0.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that specifies the duration of the output pulse in milliseconds.
    /// </summary>
    [Description("Specifies the duration of the output pulse in milliseconds.")]
    public partial class PulseSupplyPort1
    {
        /// <summary>
        /// Represents the address of the <see cref="PulseSupplyPort1"/> register. This field is constant.
        /// </summary>
        public const int Address = 50;

        /// <summary>
        /// Represents the payload type of the <see cref="PulseSupplyPort1"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="PulseSupplyPort1"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="PulseSupplyPort1"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="PulseSupplyPort1"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="PulseSupplyPort1"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="PulseSupplyPort1"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="PulseSupplyPort1"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="PulseSupplyPort1"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// PulseSupplyPort1 register.
    /// </summary>
    /// <seealso cref="PulseSupplyPort1"/>
    [Description("Filters and selects timestamped messages from the PulseSupplyPort1 register.")]
    public partial class TimestampedPulseSupplyPort1
    {
        /// <summary>
        /// Represents the address of the <see cref="PulseSupplyPort1"/> register. This field is constant.
        /// </summary>
        public const int Address = PulseSupplyPort1.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="PulseSupplyPort1"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return PulseSupplyPort1.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that specifies the duration of the output pulse in milliseconds.
    /// </summary>
    [Description("Specifies the duration of the output pulse in milliseconds.")]
    public partial class PulseSupplyPort2
    {
        /// <summary>
        /// Represents the address of the <see cref="PulseSupplyPort2"/> register. This field is constant.
        /// </summary>
        public const int Address = 51;

        /// <summary>
        /// Represents the payload type of the <see cref="PulseSupplyPort2"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="PulseSupplyPort2"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="PulseSupplyPort2"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="PulseSupplyPort2"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="PulseSupplyPort2"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="PulseSupplyPort2"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="PulseSupplyPort2"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="PulseSupplyPort2"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// PulseSupplyPort2 register.
    /// </summary>
    /// <seealso cref="PulseSupplyPort2"/>
    [Description("Filters and selects timestamped messages from the PulseSupplyPort2 register.")]
    public partial class TimestampedPulseSupplyPort2
    {
        /// <summary>
        /// Represents the address of the <see cref="PulseSupplyPort2"/> register. This field is constant.
        /// </summary>
        public const int Address = PulseSupplyPort2.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="PulseSupplyPort2"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return PulseSupplyPort2.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that specifies the duration of the output pulse in milliseconds.
    /// </summary>
    [Description("Specifies the duration of the output pulse in milliseconds.")]
    public partial class PulseLed0
    {
        /// <summary>
        /// Represents the address of the <see cref="PulseLed0"/> register. This field is constant.
        /// </summary>
        public const int Address = 52;

        /// <summary>
        /// Represents the payload type of the <see cref="PulseLed0"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="PulseLed0"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="PulseLed0"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="PulseLed0"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="PulseLed0"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="PulseLed0"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="PulseLed0"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="PulseLed0"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// PulseLed0 register.
    /// </summary>
    /// <seealso cref="PulseLed0"/>
    [Description("Filters and selects timestamped messages from the PulseLed0 register.")]
    public partial class TimestampedPulseLed0
    {
        /// <summary>
        /// Represents the address of the <see cref="PulseLed0"/> register. This field is constant.
        /// </summary>
        public const int Address = PulseLed0.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="PulseLed0"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return PulseLed0.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that specifies the duration of the output pulse in milliseconds.
    /// </summary>
    [Description("Specifies the duration of the output pulse in milliseconds.")]
    public partial class PulseLed1
    {
        /// <summary>
        /// Represents the address of the <see cref="PulseLed1"/> register. This field is constant.
        /// </summary>
        public const int Address = 53;

        /// <summary>
        /// Represents the payload type of the <see cref="PulseLed1"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="PulseLed1"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="PulseLed1"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="PulseLed1"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="PulseLed1"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="PulseLed1"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="PulseLed1"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="PulseLed1"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// PulseLed1 register.
    /// </summary>
    /// <seealso cref="PulseLed1"/>
    [Description("Filters and selects timestamped messages from the PulseLed1 register.")]
    public partial class TimestampedPulseLed1
    {
        /// <summary>
        /// Represents the address of the <see cref="PulseLed1"/> register. This field is constant.
        /// </summary>
        public const int Address = PulseLed1.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="PulseLed1"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return PulseLed1.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that specifies the duration of the output pulse in milliseconds.
    /// </summary>
    [Description("Specifies the duration of the output pulse in milliseconds.")]
    public partial class PulseRgb0
    {
        /// <summary>
        /// Represents the address of the <see cref="PulseRgb0"/> register. This field is constant.
        /// </summary>
        public const int Address = 54;

        /// <summary>
        /// Represents the payload type of the <see cref="PulseRgb0"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="PulseRgb0"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="PulseRgb0"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="PulseRgb0"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="PulseRgb0"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="PulseRgb0"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="PulseRgb0"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="PulseRgb0"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// PulseRgb0 register.
    /// </summary>
    /// <seealso cref="PulseRgb0"/>
    [Description("Filters and selects timestamped messages from the PulseRgb0 register.")]
    public partial class TimestampedPulseRgb0
    {
        /// <summary>
        /// Represents the address of the <see cref="PulseRgb0"/> register. This field is constant.
        /// </summary>
        public const int Address = PulseRgb0.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="PulseRgb0"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return PulseRgb0.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that specifies the duration of the output pulse in milliseconds.
    /// </summary>
    [Description("Specifies the duration of the output pulse in milliseconds.")]
    public partial class PulseRgb1
    {
        /// <summary>
        /// Represents the address of the <see cref="PulseRgb1"/> register. This field is constant.
        /// </summary>
        public const int Address = 55;

        /// <summary>
        /// Represents the payload type of the <see cref="PulseRgb1"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="PulseRgb1"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="PulseRgb1"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="PulseRgb1"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="PulseRgb1"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="PulseRgb1"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="PulseRgb1"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="PulseRgb1"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// PulseRgb1 register.
    /// </summary>
    /// <seealso cref="PulseRgb1"/>
    [Description("Filters and selects timestamped messages from the PulseRgb1 register.")]
    public partial class TimestampedPulseRgb1
    {
        /// <summary>
        /// Represents the address of the <see cref="PulseRgb1"/> register. This field is constant.
        /// </summary>
        public const int Address = PulseRgb1.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="PulseRgb1"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return PulseRgb1.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that specifies the duration of the output pulse in milliseconds.
    /// </summary>
    [Description("Specifies the duration of the output pulse in milliseconds.")]
    public partial class PulseDO0
    {
        /// <summary>
        /// Represents the address of the <see cref="PulseDO0"/> register. This field is constant.
        /// </summary>
        public const int Address = 56;

        /// <summary>
        /// Represents the payload type of the <see cref="PulseDO0"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="PulseDO0"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="PulseDO0"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="PulseDO0"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="PulseDO0"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="PulseDO0"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="PulseDO0"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="PulseDO0"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// PulseDO0 register.
    /// </summary>
    /// <seealso cref="PulseDO0"/>
    [Description("Filters and selects timestamped messages from the PulseDO0 register.")]
    public partial class TimestampedPulseDO0
    {
        /// <summary>
        /// Represents the address of the <see cref="PulseDO0"/> register. This field is constant.
        /// </summary>
        public const int Address = PulseDO0.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="PulseDO0"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return PulseDO0.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that specifies the duration of the output pulse in milliseconds.
    /// </summary>
    [Description("Specifies the duration of the output pulse in milliseconds.")]
    public partial class PulseDO1
    {
        /// <summary>
        /// Represents the address of the <see cref="PulseDO1"/> register. This field is constant.
        /// </summary>
        public const int Address = 57;

        /// <summary>
        /// Represents the payload type of the <see cref="PulseDO1"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="PulseDO1"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="PulseDO1"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="PulseDO1"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="PulseDO1"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="PulseDO1"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="PulseDO1"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="PulseDO1"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// PulseDO1 register.
    /// </summary>
    /// <seealso cref="PulseDO1"/>
    [Description("Filters and selects timestamped messages from the PulseDO1 register.")]
    public partial class TimestampedPulseDO1
    {
        /// <summary>
        /// Represents the address of the <see cref="PulseDO1"/> register. This field is constant.
        /// </summary>
        public const int Address = PulseDO1.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="PulseDO1"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return PulseDO1.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that specifies the duration of the output pulse in milliseconds.
    /// </summary>
    [Description("Specifies the duration of the output pulse in milliseconds.")]
    public partial class PulseDO2
    {
        /// <summary>
        /// Represents the address of the <see cref="PulseDO2"/> register. This field is constant.
        /// </summary>
        public const int Address = 58;

        /// <summary>
        /// Represents the payload type of the <see cref="PulseDO2"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="PulseDO2"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="PulseDO2"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="PulseDO2"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="PulseDO2"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="PulseDO2"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="PulseDO2"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="PulseDO2"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// PulseDO2 register.
    /// </summary>
    /// <seealso cref="PulseDO2"/>
    [Description("Filters and selects timestamped messages from the PulseDO2 register.")]
    public partial class TimestampedPulseDO2
    {
        /// <summary>
        /// Represents the address of the <see cref="PulseDO2"/> register. This field is constant.
        /// </summary>
        public const int Address = PulseDO2.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="PulseDO2"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return PulseDO2.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that specifies the duration of the output pulse in milliseconds.
    /// </summary>
    [Description("Specifies the duration of the output pulse in milliseconds.")]
    public partial class PulseDO3
    {
        /// <summary>
        /// Represents the address of the <see cref="PulseDO3"/> register. This field is constant.
        /// </summary>
        public const int Address = 59;

        /// <summary>
        /// Represents the payload type of the <see cref="PulseDO3"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="PulseDO3"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="PulseDO3"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="PulseDO3"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="PulseDO3"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="PulseDO3"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="PulseDO3"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="PulseDO3"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// PulseDO3 register.
    /// </summary>
    /// <seealso cref="PulseDO3"/>
    [Description("Filters and selects timestamped messages from the PulseDO3 register.")]
    public partial class TimestampedPulseDO3
    {
        /// <summary>
        /// Represents the address of the <see cref="PulseDO3"/> register. This field is constant.
        /// </summary>
        public const int Address = PulseDO3.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="PulseDO3"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return PulseDO3.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that specifies the frequency of the PWM at DO0.
    /// </summary>
    [Description("Specifies the frequency of the PWM at DO0.")]
    public partial class PwmFrequencyDO0
    {
        /// <summary>
        /// Represents the address of the <see cref="PwmFrequencyDO0"/> register. This field is constant.
        /// </summary>
        public const int Address = 60;

        /// <summary>
        /// Represents the payload type of the <see cref="PwmFrequencyDO0"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="PwmFrequencyDO0"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="PwmFrequencyDO0"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="PwmFrequencyDO0"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="PwmFrequencyDO0"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="PwmFrequencyDO0"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="PwmFrequencyDO0"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="PwmFrequencyDO0"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// PwmFrequencyDO0 register.
    /// </summary>
    /// <seealso cref="PwmFrequencyDO0"/>
    [Description("Filters and selects timestamped messages from the PwmFrequencyDO0 register.")]
    public partial class TimestampedPwmFrequencyDO0
    {
        /// <summary>
        /// Represents the address of the <see cref="PwmFrequencyDO0"/> register. This field is constant.
        /// </summary>
        public const int Address = PwmFrequencyDO0.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="PwmFrequencyDO0"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return PwmFrequencyDO0.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that specifies the frequency of the PWM at DO1.
    /// </summary>
    [Description("Specifies the frequency of the PWM at DO1.")]
    public partial class PwmFrequencyDO1
    {
        /// <summary>
        /// Represents the address of the <see cref="PwmFrequencyDO1"/> register. This field is constant.
        /// </summary>
        public const int Address = 61;

        /// <summary>
        /// Represents the payload type of the <see cref="PwmFrequencyDO1"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="PwmFrequencyDO1"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="PwmFrequencyDO1"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="PwmFrequencyDO1"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="PwmFrequencyDO1"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="PwmFrequencyDO1"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="PwmFrequencyDO1"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="PwmFrequencyDO1"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// PwmFrequencyDO1 register.
    /// </summary>
    /// <seealso cref="PwmFrequencyDO1"/>
    [Description("Filters and selects timestamped messages from the PwmFrequencyDO1 register.")]
    public partial class TimestampedPwmFrequencyDO1
    {
        /// <summary>
        /// Represents the address of the <see cref="PwmFrequencyDO1"/> register. This field is constant.
        /// </summary>
        public const int Address = PwmFrequencyDO1.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="PwmFrequencyDO1"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return PwmFrequencyDO1.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that specifies the frequency of the PWM at DO2.
    /// </summary>
    [Description("Specifies the frequency of the PWM at DO2.")]
    public partial class PwmFrequencyDO2
    {
        /// <summary>
        /// Represents the address of the <see cref="PwmFrequencyDO2"/> register. This field is constant.
        /// </summary>
        public const int Address = 62;

        /// <summary>
        /// Represents the payload type of the <see cref="PwmFrequencyDO2"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="PwmFrequencyDO2"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="PwmFrequencyDO2"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="PwmFrequencyDO2"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="PwmFrequencyDO2"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="PwmFrequencyDO2"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="PwmFrequencyDO2"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="PwmFrequencyDO2"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// PwmFrequencyDO2 register.
    /// </summary>
    /// <seealso cref="PwmFrequencyDO2"/>
    [Description("Filters and selects timestamped messages from the PwmFrequencyDO2 register.")]
    public partial class TimestampedPwmFrequencyDO2
    {
        /// <summary>
        /// Represents the address of the <see cref="PwmFrequencyDO2"/> register. This field is constant.
        /// </summary>
        public const int Address = PwmFrequencyDO2.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="PwmFrequencyDO2"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return PwmFrequencyDO2.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that specifies the frequency of the PWM at DO3.
    /// </summary>
    [Description("Specifies the frequency of the PWM at DO3.")]
    public partial class PwmFrequencyDO3
    {
        /// <summary>
        /// Represents the address of the <see cref="PwmFrequencyDO3"/> register. This field is constant.
        /// </summary>
        public const int Address = 63;

        /// <summary>
        /// Represents the payload type of the <see cref="PwmFrequencyDO3"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="PwmFrequencyDO3"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="PwmFrequencyDO3"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="PwmFrequencyDO3"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="PwmFrequencyDO3"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="PwmFrequencyDO3"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="PwmFrequencyDO3"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="PwmFrequencyDO3"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// PwmFrequencyDO3 register.
    /// </summary>
    /// <seealso cref="PwmFrequencyDO3"/>
    [Description("Filters and selects timestamped messages from the PwmFrequencyDO3 register.")]
    public partial class TimestampedPwmFrequencyDO3
    {
        /// <summary>
        /// Represents the address of the <see cref="PwmFrequencyDO3"/> register. This field is constant.
        /// </summary>
        public const int Address = PwmFrequencyDO3.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="PwmFrequencyDO3"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return PwmFrequencyDO3.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that specifies the duty cycle of the PWM at DO0.
    /// </summary>
    [Description("Specifies the duty cycle of the PWM at DO0.")]
    public partial class PwmDutyCycleDO0
    {
        /// <summary>
        /// Represents the address of the <see cref="PwmDutyCycleDO0"/> register. This field is constant.
        /// </summary>
        public const int Address = 64;

        /// <summary>
        /// Represents the payload type of the <see cref="PwmDutyCycleDO0"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="PwmDutyCycleDO0"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="PwmDutyCycleDO0"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static byte GetPayload(HarpMessage message)
        {
            return message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="PwmDutyCycleDO0"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<byte> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadByte();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="PwmDutyCycleDO0"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="PwmDutyCycleDO0"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, byte value)
        {
            return HarpMessage.FromByte(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="PwmDutyCycleDO0"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="PwmDutyCycleDO0"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, byte value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// PwmDutyCycleDO0 register.
    /// </summary>
    /// <seealso cref="PwmDutyCycleDO0"/>
    [Description("Filters and selects timestamped messages from the PwmDutyCycleDO0 register.")]
    public partial class TimestampedPwmDutyCycleDO0
    {
        /// <summary>
        /// Represents the address of the <see cref="PwmDutyCycleDO0"/> register. This field is constant.
        /// </summary>
        public const int Address = PwmDutyCycleDO0.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="PwmDutyCycleDO0"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<byte> GetPayload(HarpMessage message)
        {
            return PwmDutyCycleDO0.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that specifies the duty cycle of the PWM at DO1.
    /// </summary>
    [Description("Specifies the duty cycle of the PWM at DO1.")]
    public partial class PwmDutyCycleDO1
    {
        /// <summary>
        /// Represents the address of the <see cref="PwmDutyCycleDO1"/> register. This field is constant.
        /// </summary>
        public const int Address = 65;

        /// <summary>
        /// Represents the payload type of the <see cref="PwmDutyCycleDO1"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="PwmDutyCycleDO1"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="PwmDutyCycleDO1"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static byte GetPayload(HarpMessage message)
        {
            return message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="PwmDutyCycleDO1"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<byte> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadByte();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="PwmDutyCycleDO1"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="PwmDutyCycleDO1"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, byte value)
        {
            return HarpMessage.FromByte(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="PwmDutyCycleDO1"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="PwmDutyCycleDO1"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, byte value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// PwmDutyCycleDO1 register.
    /// </summary>
    /// <seealso cref="PwmDutyCycleDO1"/>
    [Description("Filters and selects timestamped messages from the PwmDutyCycleDO1 register.")]
    public partial class TimestampedPwmDutyCycleDO1
    {
        /// <summary>
        /// Represents the address of the <see cref="PwmDutyCycleDO1"/> register. This field is constant.
        /// </summary>
        public const int Address = PwmDutyCycleDO1.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="PwmDutyCycleDO1"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<byte> GetPayload(HarpMessage message)
        {
            return PwmDutyCycleDO1.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that specifies the duty cycle of the PWM at DO2.
    /// </summary>
    [Description("Specifies the duty cycle of the PWM at DO2.")]
    public partial class PwmDutyCycleDO2
    {
        /// <summary>
        /// Represents the address of the <see cref="PwmDutyCycleDO2"/> register. This field is constant.
        /// </summary>
        public const int Address = 66;

        /// <summary>
        /// Represents the payload type of the <see cref="PwmDutyCycleDO2"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="PwmDutyCycleDO2"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="PwmDutyCycleDO2"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static byte GetPayload(HarpMessage message)
        {
            return message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="PwmDutyCycleDO2"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<byte> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadByte();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="PwmDutyCycleDO2"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="PwmDutyCycleDO2"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, byte value)
        {
            return HarpMessage.FromByte(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="PwmDutyCycleDO2"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="PwmDutyCycleDO2"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, byte value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// PwmDutyCycleDO2 register.
    /// </summary>
    /// <seealso cref="PwmDutyCycleDO2"/>
    [Description("Filters and selects timestamped messages from the PwmDutyCycleDO2 register.")]
    public partial class TimestampedPwmDutyCycleDO2
    {
        /// <summary>
        /// Represents the address of the <see cref="PwmDutyCycleDO2"/> register. This field is constant.
        /// </summary>
        public const int Address = PwmDutyCycleDO2.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="PwmDutyCycleDO2"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<byte> GetPayload(HarpMessage message)
        {
            return PwmDutyCycleDO2.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that specifies the duty cycle of the PWM at DO3.
    /// </summary>
    [Description("Specifies the duty cycle of the PWM at DO3.")]
    public partial class PwmDutyCycleDO3
    {
        /// <summary>
        /// Represents the address of the <see cref="PwmDutyCycleDO3"/> register. This field is constant.
        /// </summary>
        public const int Address = 67;

        /// <summary>
        /// Represents the payload type of the <see cref="PwmDutyCycleDO3"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="PwmDutyCycleDO3"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="PwmDutyCycleDO3"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static byte GetPayload(HarpMessage message)
        {
            return message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="PwmDutyCycleDO3"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<byte> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadByte();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="PwmDutyCycleDO3"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="PwmDutyCycleDO3"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, byte value)
        {
            return HarpMessage.FromByte(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="PwmDutyCycleDO3"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="PwmDutyCycleDO3"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, byte value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// PwmDutyCycleDO3 register.
    /// </summary>
    /// <seealso cref="PwmDutyCycleDO3"/>
    [Description("Filters and selects timestamped messages from the PwmDutyCycleDO3 register.")]
    public partial class TimestampedPwmDutyCycleDO3
    {
        /// <summary>
        /// Represents the address of the <see cref="PwmDutyCycleDO3"/> register. This field is constant.
        /// </summary>
        public const int Address = PwmDutyCycleDO3.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="PwmDutyCycleDO3"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<byte> GetPayload(HarpMessage message)
        {
            return PwmDutyCycleDO3.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that starts the PWM on the selected output lines.
    /// </summary>
    [Description("Starts the PWM on the selected output lines.")]
    public partial class PwmStart
    {
        /// <summary>
        /// Represents the address of the <see cref="PwmStart"/> register. This field is constant.
        /// </summary>
        public const int Address = 68;

        /// <summary>
        /// Represents the payload type of the <see cref="PwmStart"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="PwmStart"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="PwmStart"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static PwmOutputs GetPayload(HarpMessage message)
        {
            return (PwmOutputs)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="PwmStart"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<PwmOutputs> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((PwmOutputs)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="PwmStart"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="PwmStart"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, PwmOutputs value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="PwmStart"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="PwmStart"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, PwmOutputs value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// PwmStart register.
    /// </summary>
    /// <seealso cref="PwmStart"/>
    [Description("Filters and selects timestamped messages from the PwmStart register.")]
    public partial class TimestampedPwmStart
    {
        /// <summary>
        /// Represents the address of the <see cref="PwmStart"/> register. This field is constant.
        /// </summary>
        public const int Address = PwmStart.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="PwmStart"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<PwmOutputs> GetPayload(HarpMessage message)
        {
            return PwmStart.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that stops the PWM on the selected output lines.
    /// </summary>
    [Description("Stops the PWM on the selected output lines.")]
    public partial class PwmStop
    {
        /// <summary>
        /// Represents the address of the <see cref="PwmStop"/> register. This field is constant.
        /// </summary>
        public const int Address = 69;

        /// <summary>
        /// Represents the payload type of the <see cref="PwmStop"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="PwmStop"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="PwmStop"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static PwmOutputs GetPayload(HarpMessage message)
        {
            return (PwmOutputs)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="PwmStop"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<PwmOutputs> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((PwmOutputs)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="PwmStop"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="PwmStop"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, PwmOutputs value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="PwmStop"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="PwmStop"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, PwmOutputs value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// PwmStop register.
    /// </summary>
    /// <seealso cref="PwmStop"/>
    [Description("Filters and selects timestamped messages from the PwmStop register.")]
    public partial class TimestampedPwmStop
    {
        /// <summary>
        /// Represents the address of the <see cref="PwmStop"/> register. This field is constant.
        /// </summary>
        public const int Address = PwmStop.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="PwmStop"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<PwmOutputs> GetPayload(HarpMessage message)
        {
            return PwmStop.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that specifies the state of all RGB LED channels.
    /// </summary>
    [Description("Specifies the state of all RGB LED channels.")]
    public partial class RgbAll
    {
        /// <summary>
        /// Represents the address of the <see cref="RgbAll"/> register. This field is constant.
        /// </summary>
        public const int Address = 70;

        /// <summary>
        /// Represents the payload type of the <see cref="RgbAll"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="RgbAll"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 6;

        static RgbAllPayload ParsePayload(byte[] payload)
        {
            RgbAllPayload result;
            result.Green0 = payload[0];
            result.Red0 = payload[1];
            result.Blue0 = payload[2];
            result.Green1 = payload[3];
            result.Red1 = payload[4];
            result.Blue1 = payload[5];
            return result;
        }

        static byte[] FormatPayload(RgbAllPayload value)
        {
            byte[] result;
            result = new byte[6];
            result[0] = value.Green0;
            result[1] = value.Red0;
            result[2] = value.Blue0;
            result[3] = value.Green1;
            result[4] = value.Red1;
            result[5] = value.Blue1;
            return result;
        }

        /// <summary>
        /// Returns the payload data for <see cref="RgbAll"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static RgbAllPayload GetPayload(HarpMessage message)
        {
            return ParsePayload(message.GetPayloadArray<byte>());
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="RgbAll"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<RgbAllPayload> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadArray<byte>();
            return Timestamped.Create(ParsePayload(payload.Value), payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="RgbAll"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="RgbAll"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, RgbAllPayload value)
        {
            return HarpMessage.FromByte(Address, messageType, FormatPayload(value));
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="RgbAll"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="RgbAll"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, RgbAllPayload value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, FormatPayload(value));
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// RgbAll register.
    /// </summary>
    /// <seealso cref="RgbAll"/>
    [Description("Filters and selects timestamped messages from the RgbAll register.")]
    public partial class TimestampedRgbAll
    {
        /// <summary>
        /// Represents the address of the <see cref="RgbAll"/> register. This field is constant.
        /// </summary>
        public const int Address = RgbAll.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="RgbAll"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<RgbAllPayload> GetPayload(HarpMessage message)
        {
            return RgbAll.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that specifies the state of the RGB0 LED channels.
    /// </summary>
    [Description("Specifies the state of the RGB0 LED channels.")]
    public partial class Rgb0
    {
        /// <summary>
        /// Represents the address of the <see cref="Rgb0"/> register. This field is constant.
        /// </summary>
        public const int Address = 71;

        /// <summary>
        /// Represents the payload type of the <see cref="Rgb0"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Rgb0"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 3;

        static RgbPayload ParsePayload(byte[] payload)
        {
            RgbPayload result;
            result.Green = payload[0];
            result.Red = payload[1];
            result.Blue = payload[2];
            return result;
        }

        static byte[] FormatPayload(RgbPayload value)
        {
            byte[] result;
            result = new byte[3];
            result[0] = value.Green;
            result[1] = value.Red;
            result[2] = value.Blue;
            return result;
        }

        /// <summary>
        /// Returns the payload data for <see cref="Rgb0"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static RgbPayload GetPayload(HarpMessage message)
        {
            return ParsePayload(message.GetPayloadArray<byte>());
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Rgb0"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<RgbPayload> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadArray<byte>();
            return Timestamped.Create(ParsePayload(payload.Value), payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Rgb0"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Rgb0"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, RgbPayload value)
        {
            return HarpMessage.FromByte(Address, messageType, FormatPayload(value));
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Rgb0"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Rgb0"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, RgbPayload value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, FormatPayload(value));
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Rgb0 register.
    /// </summary>
    /// <seealso cref="Rgb0"/>
    [Description("Filters and selects timestamped messages from the Rgb0 register.")]
    public partial class TimestampedRgb0
    {
        /// <summary>
        /// Represents the address of the <see cref="Rgb0"/> register. This field is constant.
        /// </summary>
        public const int Address = Rgb0.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Rgb0"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<RgbPayload> GetPayload(HarpMessage message)
        {
            return Rgb0.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that specifies the state of the RGB1 LED channels.
    /// </summary>
    [Description("Specifies the state of the RGB1 LED channels.")]
    public partial class Rgb1
    {
        /// <summary>
        /// Represents the address of the <see cref="Rgb1"/> register. This field is constant.
        /// </summary>
        public const int Address = 72;

        /// <summary>
        /// Represents the payload type of the <see cref="Rgb1"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Rgb1"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 3;

        static RgbPayload ParsePayload(byte[] payload)
        {
            RgbPayload result;
            result.Green = payload[0];
            result.Red = payload[1];
            result.Blue = payload[2];
            return result;
        }

        static byte[] FormatPayload(RgbPayload value)
        {
            byte[] result;
            result = new byte[3];
            result[0] = value.Green;
            result[1] = value.Red;
            result[2] = value.Blue;
            return result;
        }

        /// <summary>
        /// Returns the payload data for <see cref="Rgb1"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static RgbPayload GetPayload(HarpMessage message)
        {
            return ParsePayload(message.GetPayloadArray<byte>());
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Rgb1"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<RgbPayload> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadArray<byte>();
            return Timestamped.Create(ParsePayload(payload.Value), payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Rgb1"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Rgb1"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, RgbPayload value)
        {
            return HarpMessage.FromByte(Address, messageType, FormatPayload(value));
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Rgb1"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Rgb1"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, RgbPayload value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, FormatPayload(value));
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Rgb1 register.
    /// </summary>
    /// <seealso cref="Rgb1"/>
    [Description("Filters and selects timestamped messages from the Rgb1 register.")]
    public partial class TimestampedRgb1
    {
        /// <summary>
        /// Represents the address of the <see cref="Rgb1"/> register. This field is constant.
        /// </summary>
        public const int Address = Rgb1.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Rgb1"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<RgbPayload> GetPayload(HarpMessage message)
        {
            return Rgb1.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that specifies the configuration of current to drive LED 0.
    /// </summary>
    [Description("Specifies the configuration of current to drive LED 0.")]
    public partial class Led0Current
    {
        /// <summary>
        /// Represents the address of the <see cref="Led0Current"/> register. This field is constant.
        /// </summary>
        public const int Address = 73;

        /// <summary>
        /// Represents the payload type of the <see cref="Led0Current"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Led0Current"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Led0Current"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static byte GetPayload(HarpMessage message)
        {
            return message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Led0Current"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<byte> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadByte();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Led0Current"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Led0Current"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, byte value)
        {
            return HarpMessage.FromByte(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Led0Current"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Led0Current"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, byte value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Led0Current register.
    /// </summary>
    /// <seealso cref="Led0Current"/>
    [Description("Filters and selects timestamped messages from the Led0Current register.")]
    public partial class TimestampedLed0Current
    {
        /// <summary>
        /// Represents the address of the <see cref="Led0Current"/> register. This field is constant.
        /// </summary>
        public const int Address = Led0Current.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Led0Current"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<byte> GetPayload(HarpMessage message)
        {
            return Led0Current.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that specifies the configuration of current to drive LED 1.
    /// </summary>
    [Description("Specifies the configuration of current to drive LED 1.")]
    public partial class Led1Current
    {
        /// <summary>
        /// Represents the address of the <see cref="Led1Current"/> register. This field is constant.
        /// </summary>
        public const int Address = 74;

        /// <summary>
        /// Represents the payload type of the <see cref="Led1Current"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Led1Current"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Led1Current"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static byte GetPayload(HarpMessage message)
        {
            return message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Led1Current"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<byte> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadByte();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Led1Current"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Led1Current"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, byte value)
        {
            return HarpMessage.FromByte(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Led1Current"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Led1Current"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, byte value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Led1Current register.
    /// </summary>
    /// <seealso cref="Led1Current"/>
    [Description("Filters and selects timestamped messages from the Led1Current register.")]
    public partial class TimestampedLed1Current
    {
        /// <summary>
        /// Represents the address of the <see cref="Led1Current"/> register. This field is constant.
        /// </summary>
        public const int Address = Led1Current.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Led1Current"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<byte> GetPayload(HarpMessage message)
        {
            return Led1Current.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that specifies the configuration of current to drive LED 0.
    /// </summary>
    [Description("Specifies the configuration of current to drive LED 0.")]
    public partial class Led0MaxCurrent
    {
        /// <summary>
        /// Represents the address of the <see cref="Led0MaxCurrent"/> register. This field is constant.
        /// </summary>
        public const int Address = 75;

        /// <summary>
        /// Represents the payload type of the <see cref="Led0MaxCurrent"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Led0MaxCurrent"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Led0MaxCurrent"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static byte GetPayload(HarpMessage message)
        {
            return message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Led0MaxCurrent"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<byte> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadByte();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Led0MaxCurrent"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Led0MaxCurrent"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, byte value)
        {
            return HarpMessage.FromByte(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Led0MaxCurrent"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Led0MaxCurrent"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, byte value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Led0MaxCurrent register.
    /// </summary>
    /// <seealso cref="Led0MaxCurrent"/>
    [Description("Filters and selects timestamped messages from the Led0MaxCurrent register.")]
    public partial class TimestampedLed0MaxCurrent
    {
        /// <summary>
        /// Represents the address of the <see cref="Led0MaxCurrent"/> register. This field is constant.
        /// </summary>
        public const int Address = Led0MaxCurrent.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Led0MaxCurrent"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<byte> GetPayload(HarpMessage message)
        {
            return Led0MaxCurrent.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that specifies the configuration of current to drive LED 1.
    /// </summary>
    [Description("Specifies the configuration of current to drive LED 1.")]
    public partial class Led1MaxCurrent
    {
        /// <summary>
        /// Represents the address of the <see cref="Led1MaxCurrent"/> register. This field is constant.
        /// </summary>
        public const int Address = 76;

        /// <summary>
        /// Represents the payload type of the <see cref="Led1MaxCurrent"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Led1MaxCurrent"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Led1MaxCurrent"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static byte GetPayload(HarpMessage message)
        {
            return message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Led1MaxCurrent"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<byte> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadByte();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Led1MaxCurrent"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Led1MaxCurrent"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, byte value)
        {
            return HarpMessage.FromByte(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Led1MaxCurrent"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Led1MaxCurrent"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, byte value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Led1MaxCurrent register.
    /// </summary>
    /// <seealso cref="Led1MaxCurrent"/>
    [Description("Filters and selects timestamped messages from the Led1MaxCurrent register.")]
    public partial class TimestampedLed1MaxCurrent
    {
        /// <summary>
        /// Represents the address of the <see cref="Led1MaxCurrent"/> register. This field is constant.
        /// </summary>
        public const int Address = Led1MaxCurrent.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Led1MaxCurrent"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<byte> GetPayload(HarpMessage message)
        {
            return Led1MaxCurrent.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that specifies the active events in the device.
    /// </summary>
    [Description("Specifies the active events in the device.")]
    public partial class EventEnable
    {
        /// <summary>
        /// Represents the address of the <see cref="EventEnable"/> register. This field is constant.
        /// </summary>
        public const int Address = 77;

        /// <summary>
        /// Represents the payload type of the <see cref="EventEnable"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="EventEnable"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="EventEnable"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static Events GetPayload(HarpMessage message)
        {
            return (Events)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="EventEnable"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<Events> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((Events)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="EventEnable"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="EventEnable"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, Events value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="EventEnable"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="EventEnable"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, Events value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// EventEnable register.
    /// </summary>
    /// <seealso cref="EventEnable"/>
    [Description("Filters and selects timestamped messages from the EventEnable register.")]
    public partial class TimestampedEventEnable
    {
        /// <summary>
        /// Represents the address of the <see cref="EventEnable"/> register. This field is constant.
        /// </summary>
        public const int Address = EventEnable.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="EventEnable"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<Events> GetPayload(HarpMessage message)
        {
            return EventEnable.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that specifies the camera outputs to enable in the device.
    /// </summary>
    [Description("Specifies the camera outputs to enable in the device.")]
    public partial class StartCameras
    {
        /// <summary>
        /// Represents the address of the <see cref="StartCameras"/> register. This field is constant.
        /// </summary>
        public const int Address = 78;

        /// <summary>
        /// Represents the payload type of the <see cref="StartCameras"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="StartCameras"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="StartCameras"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static CameraOutputs GetPayload(HarpMessage message)
        {
            return (CameraOutputs)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="StartCameras"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<CameraOutputs> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((CameraOutputs)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="StartCameras"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="StartCameras"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, CameraOutputs value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="StartCameras"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="StartCameras"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, CameraOutputs value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// StartCameras register.
    /// </summary>
    /// <seealso cref="StartCameras"/>
    [Description("Filters and selects timestamped messages from the StartCameras register.")]
    public partial class TimestampedStartCameras
    {
        /// <summary>
        /// Represents the address of the <see cref="StartCameras"/> register. This field is constant.
        /// </summary>
        public const int Address = StartCameras.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="StartCameras"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<CameraOutputs> GetPayload(HarpMessage message)
        {
            return StartCameras.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that specifies the camera outputs to disable in the device.
    /// </summary>
    [Description("Specifies the camera outputs to disable in the device.")]
    public partial class StopCameras
    {
        /// <summary>
        /// Represents the address of the <see cref="StopCameras"/> register. This field is constant.
        /// </summary>
        public const int Address = 79;

        /// <summary>
        /// Represents the payload type of the <see cref="StopCameras"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="StopCameras"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="StopCameras"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static CameraOutputs GetPayload(HarpMessage message)
        {
            return (CameraOutputs)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="StopCameras"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<CameraOutputs> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((CameraOutputs)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="StopCameras"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="StopCameras"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, CameraOutputs value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="StopCameras"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="StopCameras"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, CameraOutputs value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// StopCameras register.
    /// </summary>
    /// <seealso cref="StopCameras"/>
    [Description("Filters and selects timestamped messages from the StopCameras register.")]
    public partial class TimestampedStopCameras
    {
        /// <summary>
        /// Represents the address of the <see cref="StopCameras"/> register. This field is constant.
        /// </summary>
        public const int Address = StopCameras.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="StopCameras"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<CameraOutputs> GetPayload(HarpMessage message)
        {
            return StopCameras.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that specifies the servo outputs to enable in the device.
    /// </summary>
    [Description("Specifies the servo outputs to enable in the device.")]
    public partial class EnableServos
    {
        /// <summary>
        /// Represents the address of the <see cref="EnableServos"/> register. This field is constant.
        /// </summary>
        public const int Address = 80;

        /// <summary>
        /// Represents the payload type of the <see cref="EnableServos"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="EnableServos"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="EnableServos"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ServoOutputs GetPayload(HarpMessage message)
        {
            return (ServoOutputs)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="EnableServos"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ServoOutputs> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((ServoOutputs)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="EnableServos"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="EnableServos"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ServoOutputs value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="EnableServos"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="EnableServos"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ServoOutputs value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// EnableServos register.
    /// </summary>
    /// <seealso cref="EnableServos"/>
    [Description("Filters and selects timestamped messages from the EnableServos register.")]
    public partial class TimestampedEnableServos
    {
        /// <summary>
        /// Represents the address of the <see cref="EnableServos"/> register. This field is constant.
        /// </summary>
        public const int Address = EnableServos.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="EnableServos"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ServoOutputs> GetPayload(HarpMessage message)
        {
            return EnableServos.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that specifies the servo outputs to disable in the device.
    /// </summary>
    [Description("Specifies the servo outputs to disable in the device.")]
    public partial class DisableServos
    {
        /// <summary>
        /// Represents the address of the <see cref="DisableServos"/> register. This field is constant.
        /// </summary>
        public const int Address = 81;

        /// <summary>
        /// Represents the payload type of the <see cref="DisableServos"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="DisableServos"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DisableServos"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ServoOutputs GetPayload(HarpMessage message)
        {
            return (ServoOutputs)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DisableServos"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ServoOutputs> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((ServoOutputs)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DisableServos"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DisableServos"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ServoOutputs value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DisableServos"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DisableServos"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ServoOutputs value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DisableServos register.
    /// </summary>
    /// <seealso cref="DisableServos"/>
    [Description("Filters and selects timestamped messages from the DisableServos register.")]
    public partial class TimestampedDisableServos
    {
        /// <summary>
        /// Represents the address of the <see cref="DisableServos"/> register. This field is constant.
        /// </summary>
        public const int Address = DisableServos.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DisableServos"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ServoOutputs> GetPayload(HarpMessage message)
        {
            return DisableServos.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that specifies the port quadrature counters to enable in the device.
    /// </summary>
    [Description("Specifies the port quadrature counters to enable in the device.")]
    public partial class EnableEncoders
    {
        /// <summary>
        /// Represents the address of the <see cref="EnableEncoders"/> register. This field is constant.
        /// </summary>
        public const int Address = 82;

        /// <summary>
        /// Represents the payload type of the <see cref="EnableEncoders"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="EnableEncoders"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="EnableEncoders"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static EncoderInputs GetPayload(HarpMessage message)
        {
            return (EncoderInputs)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="EnableEncoders"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<EncoderInputs> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((EncoderInputs)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="EnableEncoders"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="EnableEncoders"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, EncoderInputs value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="EnableEncoders"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="EnableEncoders"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, EncoderInputs value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// EnableEncoders register.
    /// </summary>
    /// <seealso cref="EnableEncoders"/>
    [Description("Filters and selects timestamped messages from the EnableEncoders register.")]
    public partial class TimestampedEnableEncoders
    {
        /// <summary>
        /// Represents the address of the <see cref="EnableEncoders"/> register. This field is constant.
        /// </summary>
        public const int Address = EnableEncoders.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="EnableEncoders"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<EncoderInputs> GetPayload(HarpMessage message)
        {
            return EnableEncoders.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that configures the operation mode of the quadrature encoders.
    /// </summary>
    [Description("Configures the operation mode of the quadrature encoders.")]
    public partial class EncoderMode
    {
        /// <summary>
        /// Represents the address of the <see cref="EncoderMode"/> register. This field is constant.
        /// </summary>
        public const int Address = 83;

        /// <summary>
        /// Represents the payload type of the <see cref="EncoderMode"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="EncoderMode"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="EncoderMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static EncoderModeConfig GetPayload(HarpMessage message)
        {
            return (EncoderModeConfig)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="EncoderMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<EncoderModeConfig> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((EncoderModeConfig)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="EncoderMode"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="EncoderMode"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, EncoderModeConfig value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="EncoderMode"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="EncoderMode"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, EncoderModeConfig value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// EncoderMode register.
    /// </summary>
    /// <seealso cref="EncoderMode"/>
    [Description("Filters and selects timestamped messages from the EncoderMode register.")]
    public partial class TimestampedEncoderMode
    {
        /// <summary>
        /// Represents the address of the <see cref="EncoderMode"/> register. This field is constant.
        /// </summary>
        public const int Address = EncoderMode.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="EncoderMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<EncoderModeConfig> GetPayload(HarpMessage message)
        {
            return EncoderMode.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that reserved for future use.
    /// </summary>
    [Description("Reserved for future use")]
    internal partial class Reserved2
    {
        /// <summary>
        /// Represents the address of the <see cref="Reserved2"/> register. This field is constant.
        /// </summary>
        public const int Address = 84;

        /// <summary>
        /// Represents the payload type of the <see cref="Reserved2"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Reserved2"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;
    }

    /// <summary>
    /// Represents a register that reserved for future use.
    /// </summary>
    [Description("Reserved for future use")]
    internal partial class Reserved3
    {
        /// <summary>
        /// Represents the address of the <see cref="Reserved3"/> register. This field is constant.
        /// </summary>
        public const int Address = 85;

        /// <summary>
        /// Represents the payload type of the <see cref="Reserved3"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Reserved3"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;
    }

    /// <summary>
    /// Represents a register that reserved for future use.
    /// </summary>
    [Description("Reserved for future use")]
    internal partial class Reserved4
    {
        /// <summary>
        /// Represents the address of the <see cref="Reserved4"/> register. This field is constant.
        /// </summary>
        public const int Address = 86;

        /// <summary>
        /// Represents the payload type of the <see cref="Reserved4"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Reserved4"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;
    }

    /// <summary>
    /// Represents a register that reserved for future use.
    /// </summary>
    [Description("Reserved for future use")]
    internal partial class Reserved5
    {
        /// <summary>
        /// Represents the address of the <see cref="Reserved5"/> register. This field is constant.
        /// </summary>
        public const int Address = 87;

        /// <summary>
        /// Represents the payload type of the <see cref="Reserved5"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Reserved5"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;
    }

    /// <summary>
    /// Represents a register that reserved for future use.
    /// </summary>
    [Description("Reserved for future use")]
    internal partial class Reserved6
    {
        /// <summary>
        /// Represents the address of the <see cref="Reserved6"/> register. This field is constant.
        /// </summary>
        public const int Address = 88;

        /// <summary>
        /// Represents the payload type of the <see cref="Reserved6"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Reserved6"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;
    }

    /// <summary>
    /// Represents a register that reserved for future use.
    /// </summary>
    [Description("Reserved for future use")]
    internal partial class Reserved7
    {
        /// <summary>
        /// Represents the address of the <see cref="Reserved7"/> register. This field is constant.
        /// </summary>
        public const int Address = 89;

        /// <summary>
        /// Represents the payload type of the <see cref="Reserved7"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Reserved7"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;
    }

    /// <summary>
    /// Represents a register that reserved for future use.
    /// </summary>
    [Description("Reserved for future use")]
    internal partial class Reserved8
    {
        /// <summary>
        /// Represents the address of the <see cref="Reserved8"/> register. This field is constant.
        /// </summary>
        public const int Address = 90;

        /// <summary>
        /// Represents the payload type of the <see cref="Reserved8"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Reserved8"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;
    }

    /// <summary>
    /// Represents a register that reserved for future use.
    /// </summary>
    [Description("Reserved for future use")]
    internal partial class Reserved9
    {
        /// <summary>
        /// Represents the address of the <see cref="Reserved9"/> register. This field is constant.
        /// </summary>
        public const int Address = 91;

        /// <summary>
        /// Represents the payload type of the <see cref="Reserved9"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Reserved9"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;
    }

    /// <summary>
    /// Represents a register that specifies that a frame was acquired on camera 0.
    /// </summary>
    [Description("Specifies that a frame was acquired on camera 0.")]
    public partial class Camera0Frame
    {
        /// <summary>
        /// Represents the address of the <see cref="Camera0Frame"/> register. This field is constant.
        /// </summary>
        public const int Address = 92;

        /// <summary>
        /// Represents the payload type of the <see cref="Camera0Frame"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Camera0Frame"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Camera0Frame"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static FrameAcquired GetPayload(HarpMessage message)
        {
            return (FrameAcquired)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Camera0Frame"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<FrameAcquired> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((FrameAcquired)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Camera0Frame"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Camera0Frame"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, FrameAcquired value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Camera0Frame"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Camera0Frame"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, FrameAcquired value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Camera0Frame register.
    /// </summary>
    /// <seealso cref="Camera0Frame"/>
    [Description("Filters and selects timestamped messages from the Camera0Frame register.")]
    public partial class TimestampedCamera0Frame
    {
        /// <summary>
        /// Represents the address of the <see cref="Camera0Frame"/> register. This field is constant.
        /// </summary>
        public const int Address = Camera0Frame.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Camera0Frame"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<FrameAcquired> GetPayload(HarpMessage message)
        {
            return Camera0Frame.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that specifies the trigger frequency for camera 0.
    /// </summary>
    [Description("Specifies the trigger frequency for camera 0.")]
    public partial class Camera0Frequency
    {
        /// <summary>
        /// Represents the address of the <see cref="Camera0Frequency"/> register. This field is constant.
        /// </summary>
        public const int Address = 93;

        /// <summary>
        /// Represents the payload type of the <see cref="Camera0Frequency"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="Camera0Frequency"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Camera0Frequency"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Camera0Frequency"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Camera0Frequency"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Camera0Frequency"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Camera0Frequency"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Camera0Frequency"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Camera0Frequency register.
    /// </summary>
    /// <seealso cref="Camera0Frequency"/>
    [Description("Filters and selects timestamped messages from the Camera0Frequency register.")]
    public partial class TimestampedCamera0Frequency
    {
        /// <summary>
        /// Represents the address of the <see cref="Camera0Frequency"/> register. This field is constant.
        /// </summary>
        public const int Address = Camera0Frequency.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Camera0Frequency"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return Camera0Frequency.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that specifies that a frame was acquired on camera 1.
    /// </summary>
    [Description("Specifies that a frame was acquired on camera 1.")]
    public partial class Camera1Frame
    {
        /// <summary>
        /// Represents the address of the <see cref="Camera1Frame"/> register. This field is constant.
        /// </summary>
        public const int Address = 94;

        /// <summary>
        /// Represents the payload type of the <see cref="Camera1Frame"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Camera1Frame"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Camera1Frame"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static FrameAcquired GetPayload(HarpMessage message)
        {
            return (FrameAcquired)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Camera1Frame"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<FrameAcquired> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((FrameAcquired)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Camera1Frame"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Camera1Frame"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, FrameAcquired value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Camera1Frame"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Camera1Frame"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, FrameAcquired value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Camera1Frame register.
    /// </summary>
    /// <seealso cref="Camera1Frame"/>
    [Description("Filters and selects timestamped messages from the Camera1Frame register.")]
    public partial class TimestampedCamera1Frame
    {
        /// <summary>
        /// Represents the address of the <see cref="Camera1Frame"/> register. This field is constant.
        /// </summary>
        public const int Address = Camera1Frame.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Camera1Frame"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<FrameAcquired> GetPayload(HarpMessage message)
        {
            return Camera1Frame.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that specifies the trigger frequency for camera 1.
    /// </summary>
    [Description("Specifies the trigger frequency for camera 1.")]
    public partial class Camera1Frequency
    {
        /// <summary>
        /// Represents the address of the <see cref="Camera1Frequency"/> register. This field is constant.
        /// </summary>
        public const int Address = 95;

        /// <summary>
        /// Represents the payload type of the <see cref="Camera1Frequency"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="Camera1Frequency"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Camera1Frequency"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Camera1Frequency"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Camera1Frequency"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Camera1Frequency"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Camera1Frequency"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Camera1Frequency"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Camera1Frequency register.
    /// </summary>
    /// <seealso cref="Camera1Frequency"/>
    [Description("Filters and selects timestamped messages from the Camera1Frequency register.")]
    public partial class TimestampedCamera1Frequency
    {
        /// <summary>
        /// Represents the address of the <see cref="Camera1Frequency"/> register. This field is constant.
        /// </summary>
        public const int Address = Camera1Frequency.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Camera1Frequency"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return Camera1Frequency.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that reserved for future use.
    /// </summary>
    [Description("Reserved for future use")]
    internal partial class Reserved10
    {
        /// <summary>
        /// Represents the address of the <see cref="Reserved10"/> register. This field is constant.
        /// </summary>
        public const int Address = 96;

        /// <summary>
        /// Represents the payload type of the <see cref="Reserved10"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Reserved10"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;
    }

    /// <summary>
    /// Represents a register that reserved for future use.
    /// </summary>
    [Description("Reserved for future use")]
    internal partial class Reserved11
    {
        /// <summary>
        /// Represents the address of the <see cref="Reserved11"/> register. This field is constant.
        /// </summary>
        public const int Address = 97;

        /// <summary>
        /// Represents the payload type of the <see cref="Reserved11"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Reserved11"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;
    }

    /// <summary>
    /// Represents a register that reserved for future use.
    /// </summary>
    [Description("Reserved for future use")]
    internal partial class Reserved12
    {
        /// <summary>
        /// Represents the address of the <see cref="Reserved12"/> register. This field is constant.
        /// </summary>
        public const int Address = 98;

        /// <summary>
        /// Represents the payload type of the <see cref="Reserved12"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Reserved12"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;
    }

    /// <summary>
    /// Represents a register that reserved for future use.
    /// </summary>
    [Description("Reserved for future use")]
    internal partial class Reserved13
    {
        /// <summary>
        /// Represents the address of the <see cref="Reserved13"/> register. This field is constant.
        /// </summary>
        public const int Address = 99;

        /// <summary>
        /// Represents the payload type of the <see cref="Reserved13"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Reserved13"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;
    }

    /// <summary>
    /// Represents a register that specifies the period of the servo motor in DO2, in microseconds.
    /// </summary>
    [Description("Specifies the period of the servo motor in DO2, in microseconds.")]
    public partial class ServoMotor2Period
    {
        /// <summary>
        /// Represents the address of the <see cref="ServoMotor2Period"/> register. This field is constant.
        /// </summary>
        public const int Address = 100;

        /// <summary>
        /// Represents the payload type of the <see cref="ServoMotor2Period"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="ServoMotor2Period"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="ServoMotor2Period"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="ServoMotor2Period"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="ServoMotor2Period"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="ServoMotor2Period"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="ServoMotor2Period"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="ServoMotor2Period"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// ServoMotor2Period register.
    /// </summary>
    /// <seealso cref="ServoMotor2Period"/>
    [Description("Filters and selects timestamped messages from the ServoMotor2Period register.")]
    public partial class TimestampedServoMotor2Period
    {
        /// <summary>
        /// Represents the address of the <see cref="ServoMotor2Period"/> register. This field is constant.
        /// </summary>
        public const int Address = ServoMotor2Period.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="ServoMotor2Period"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return ServoMotor2Period.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that specifies the pulse of the servo motor in DO2, in microseconds.
    /// </summary>
    [Description("Specifies the pulse of the servo motor in DO2, in microseconds.")]
    public partial class ServoMotor2Pulse
    {
        /// <summary>
        /// Represents the address of the <see cref="ServoMotor2Pulse"/> register. This field is constant.
        /// </summary>
        public const int Address = 101;

        /// <summary>
        /// Represents the payload type of the <see cref="ServoMotor2Pulse"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="ServoMotor2Pulse"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="ServoMotor2Pulse"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="ServoMotor2Pulse"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="ServoMotor2Pulse"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="ServoMotor2Pulse"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="ServoMotor2Pulse"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="ServoMotor2Pulse"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// ServoMotor2Pulse register.
    /// </summary>
    /// <seealso cref="ServoMotor2Pulse"/>
    [Description("Filters and selects timestamped messages from the ServoMotor2Pulse register.")]
    public partial class TimestampedServoMotor2Pulse
    {
        /// <summary>
        /// Represents the address of the <see cref="ServoMotor2Pulse"/> register. This field is constant.
        /// </summary>
        public const int Address = ServoMotor2Pulse.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="ServoMotor2Pulse"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return ServoMotor2Pulse.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that specifies the period of the servo motor in DO3, in microseconds.
    /// </summary>
    [Description("Specifies the period of the servo motor in DO3, in microseconds.")]
    public partial class ServoMotor3Period
    {
        /// <summary>
        /// Represents the address of the <see cref="ServoMotor3Period"/> register. This field is constant.
        /// </summary>
        public const int Address = 102;

        /// <summary>
        /// Represents the payload type of the <see cref="ServoMotor3Period"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="ServoMotor3Period"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="ServoMotor3Period"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="ServoMotor3Period"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="ServoMotor3Period"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="ServoMotor3Period"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="ServoMotor3Period"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="ServoMotor3Period"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// ServoMotor3Period register.
    /// </summary>
    /// <seealso cref="ServoMotor3Period"/>
    [Description("Filters and selects timestamped messages from the ServoMotor3Period register.")]
    public partial class TimestampedServoMotor3Period
    {
        /// <summary>
        /// Represents the address of the <see cref="ServoMotor3Period"/> register. This field is constant.
        /// </summary>
        public const int Address = ServoMotor3Period.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="ServoMotor3Period"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return ServoMotor3Period.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that specifies the pulse of the servo motor in DO3, in microseconds.
    /// </summary>
    [Description("Specifies the pulse of the servo motor in DO3, in microseconds.")]
    public partial class ServoMotor3Pulse
    {
        /// <summary>
        /// Represents the address of the <see cref="ServoMotor3Pulse"/> register. This field is constant.
        /// </summary>
        public const int Address = 103;

        /// <summary>
        /// Represents the payload type of the <see cref="ServoMotor3Pulse"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="ServoMotor3Pulse"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="ServoMotor3Pulse"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="ServoMotor3Pulse"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="ServoMotor3Pulse"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="ServoMotor3Pulse"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="ServoMotor3Pulse"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="ServoMotor3Pulse"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// ServoMotor3Pulse register.
    /// </summary>
    /// <seealso cref="ServoMotor3Pulse"/>
    [Description("Filters and selects timestamped messages from the ServoMotor3Pulse register.")]
    public partial class TimestampedServoMotor3Pulse
    {
        /// <summary>
        /// Represents the address of the <see cref="ServoMotor3Pulse"/> register. This field is constant.
        /// </summary>
        public const int Address = ServoMotor3Pulse.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="ServoMotor3Pulse"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return ServoMotor3Pulse.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that reserved for future use.
    /// </summary>
    [Description("Reserved for future use")]
    internal partial class Reserved14
    {
        /// <summary>
        /// Represents the address of the <see cref="Reserved14"/> register. This field is constant.
        /// </summary>
        public const int Address = 104;

        /// <summary>
        /// Represents the payload type of the <see cref="Reserved14"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Reserved14"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;
    }

    /// <summary>
    /// Represents a register that reserved for future use.
    /// </summary>
    [Description("Reserved for future use")]
    internal partial class Reserved15
    {
        /// <summary>
        /// Represents the address of the <see cref="Reserved15"/> register. This field is constant.
        /// </summary>
        public const int Address = 105;

        /// <summary>
        /// Represents the payload type of the <see cref="Reserved15"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Reserved15"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;
    }

    /// <summary>
    /// Represents a register that reserved for future use.
    /// </summary>
    [Description("Reserved for future use")]
    internal partial class Reserved16
    {
        /// <summary>
        /// Represents the address of the <see cref="Reserved16"/> register. This field is constant.
        /// </summary>
        public const int Address = 106;

        /// <summary>
        /// Represents the payload type of the <see cref="Reserved16"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Reserved16"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;
    }

    /// <summary>
    /// Represents a register that reserved for future use.
    /// </summary>
    [Description("Reserved for future use")]
    internal partial class Reserved17
    {
        /// <summary>
        /// Represents the address of the <see cref="Reserved17"/> register. This field is constant.
        /// </summary>
        public const int Address = 107;

        /// <summary>
        /// Represents the payload type of the <see cref="Reserved17"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Reserved17"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;
    }

    /// <summary>
    /// Represents a register that reset the counter of the specified encoders to zero.
    /// </summary>
    [Description("Reset the counter of the specified encoders to zero.")]
    public partial class EncoderReset
    {
        /// <summary>
        /// Represents the address of the <see cref="EncoderReset"/> register. This field is constant.
        /// </summary>
        public const int Address = 108;

        /// <summary>
        /// Represents the payload type of the <see cref="EncoderReset"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="EncoderReset"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="EncoderReset"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static EncoderInputs GetPayload(HarpMessage message)
        {
            return (EncoderInputs)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="EncoderReset"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<EncoderInputs> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((EncoderInputs)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="EncoderReset"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="EncoderReset"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, EncoderInputs value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="EncoderReset"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="EncoderReset"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, EncoderInputs value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// EncoderReset register.
    /// </summary>
    /// <seealso cref="EncoderReset"/>
    [Description("Filters and selects timestamped messages from the EncoderReset register.")]
    public partial class TimestampedEncoderReset
    {
        /// <summary>
        /// Represents the address of the <see cref="EncoderReset"/> register. This field is constant.
        /// </summary>
        public const int Address = EncoderReset.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="EncoderReset"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<EncoderInputs> GetPayload(HarpMessage message)
        {
            return EncoderReset.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that reserved for future use.
    /// </summary>
    [Description("Reserved for future use")]
    internal partial class Reserved18
    {
        /// <summary>
        /// Represents the address of the <see cref="Reserved18"/> register. This field is constant.
        /// </summary>
        public const int Address = 109;

        /// <summary>
        /// Represents the payload type of the <see cref="Reserved18"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Reserved18"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;
    }

    /// <summary>
    /// Represents a register that enables the timestamp for serial TX.
    /// </summary>
    [Description("Enables the timestamp for serial TX.")]
    public partial class EnableSerialTimestamp
    {
        /// <summary>
        /// Represents the address of the <see cref="EnableSerialTimestamp"/> register. This field is constant.
        /// </summary>
        public const int Address = 110;

        /// <summary>
        /// Represents the payload type of the <see cref="EnableSerialTimestamp"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="EnableSerialTimestamp"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="EnableSerialTimestamp"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static byte GetPayload(HarpMessage message)
        {
            return message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="EnableSerialTimestamp"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<byte> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadByte();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="EnableSerialTimestamp"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="EnableSerialTimestamp"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, byte value)
        {
            return HarpMessage.FromByte(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="EnableSerialTimestamp"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="EnableSerialTimestamp"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, byte value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// EnableSerialTimestamp register.
    /// </summary>
    /// <seealso cref="EnableSerialTimestamp"/>
    [Description("Filters and selects timestamped messages from the EnableSerialTimestamp register.")]
    public partial class TimestampedEnableSerialTimestamp
    {
        /// <summary>
        /// Represents the address of the <see cref="EnableSerialTimestamp"/> register. This field is constant.
        /// </summary>
        public const int Address = EnableSerialTimestamp.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="EnableSerialTimestamp"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<byte> GetPayload(HarpMessage message)
        {
            return EnableSerialTimestamp.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that specifies the digital output to mimic the Port 0 IR state.
    /// </summary>
    [Description("Specifies the digital output to mimic the Port 0 IR state.")]
    public partial class MimicPort0IR
    {
        /// <summary>
        /// Represents the address of the <see cref="MimicPort0IR"/> register. This field is constant.
        /// </summary>
        public const int Address = 111;

        /// <summary>
        /// Represents the payload type of the <see cref="MimicPort0IR"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="MimicPort0IR"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="MimicPort0IR"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static MimicOutput GetPayload(HarpMessage message)
        {
            return (MimicOutput)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="MimicPort0IR"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<MimicOutput> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((MimicOutput)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="MimicPort0IR"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="MimicPort0IR"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, MimicOutput value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="MimicPort0IR"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="MimicPort0IR"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, MimicOutput value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// MimicPort0IR register.
    /// </summary>
    /// <seealso cref="MimicPort0IR"/>
    [Description("Filters and selects timestamped messages from the MimicPort0IR register.")]
    public partial class TimestampedMimicPort0IR
    {
        /// <summary>
        /// Represents the address of the <see cref="MimicPort0IR"/> register. This field is constant.
        /// </summary>
        public const int Address = MimicPort0IR.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="MimicPort0IR"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<MimicOutput> GetPayload(HarpMessage message)
        {
            return MimicPort0IR.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that specifies the digital output to mimic the Port 1 IR state.
    /// </summary>
    [Description("Specifies the digital output to mimic the Port 1 IR state.")]
    public partial class MimicPort1IR
    {
        /// <summary>
        /// Represents the address of the <see cref="MimicPort1IR"/> register. This field is constant.
        /// </summary>
        public const int Address = 112;

        /// <summary>
        /// Represents the payload type of the <see cref="MimicPort1IR"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="MimicPort1IR"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="MimicPort1IR"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static MimicOutput GetPayload(HarpMessage message)
        {
            return (MimicOutput)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="MimicPort1IR"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<MimicOutput> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((MimicOutput)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="MimicPort1IR"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="MimicPort1IR"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, MimicOutput value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="MimicPort1IR"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="MimicPort1IR"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, MimicOutput value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// MimicPort1IR register.
    /// </summary>
    /// <seealso cref="MimicPort1IR"/>
    [Description("Filters and selects timestamped messages from the MimicPort1IR register.")]
    public partial class TimestampedMimicPort1IR
    {
        /// <summary>
        /// Represents the address of the <see cref="MimicPort1IR"/> register. This field is constant.
        /// </summary>
        public const int Address = MimicPort1IR.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="MimicPort1IR"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<MimicOutput> GetPayload(HarpMessage message)
        {
            return MimicPort1IR.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that specifies the digital output to mimic the Port 2 IR state.
    /// </summary>
    [Description("Specifies the digital output to mimic the Port 2 IR state.")]
    public partial class MimicPort2IR
    {
        /// <summary>
        /// Represents the address of the <see cref="MimicPort2IR"/> register. This field is constant.
        /// </summary>
        public const int Address = 113;

        /// <summary>
        /// Represents the payload type of the <see cref="MimicPort2IR"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="MimicPort2IR"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="MimicPort2IR"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static MimicOutput GetPayload(HarpMessage message)
        {
            return (MimicOutput)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="MimicPort2IR"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<MimicOutput> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((MimicOutput)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="MimicPort2IR"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="MimicPort2IR"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, MimicOutput value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="MimicPort2IR"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="MimicPort2IR"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, MimicOutput value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// MimicPort2IR register.
    /// </summary>
    /// <seealso cref="MimicPort2IR"/>
    [Description("Filters and selects timestamped messages from the MimicPort2IR register.")]
    public partial class TimestampedMimicPort2IR
    {
        /// <summary>
        /// Represents the address of the <see cref="MimicPort2IR"/> register. This field is constant.
        /// </summary>
        public const int Address = MimicPort2IR.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="MimicPort2IR"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<MimicOutput> GetPayload(HarpMessage message)
        {
            return MimicPort2IR.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that reserved for future use.
    /// </summary>
    [Description("Reserved for future use")]
    internal partial class Reserved20
    {
        /// <summary>
        /// Represents the address of the <see cref="Reserved20"/> register. This field is constant.
        /// </summary>
        public const int Address = 114;

        /// <summary>
        /// Represents the payload type of the <see cref="Reserved20"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Reserved20"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;
    }

    /// <summary>
    /// Represents a register that reserved for future use.
    /// </summary>
    [Description("Reserved for future use")]
    internal partial class Reserved21
    {
        /// <summary>
        /// Represents the address of the <see cref="Reserved21"/> register. This field is constant.
        /// </summary>
        public const int Address = 115;

        /// <summary>
        /// Represents the payload type of the <see cref="Reserved21"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Reserved21"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;
    }

    /// <summary>
    /// Represents a register that reserved for future use.
    /// </summary>
    [Description("Reserved for future use")]
    internal partial class Reserved22
    {
        /// <summary>
        /// Represents the address of the <see cref="Reserved22"/> register. This field is constant.
        /// </summary>
        public const int Address = 116;

        /// <summary>
        /// Represents the payload type of the <see cref="Reserved22"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Reserved22"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;
    }

    /// <summary>
    /// Represents a register that specifies the digital output to mimic the Port 0 valve state.
    /// </summary>
    [Description("Specifies the digital output to mimic the Port 0 valve state.")]
    public partial class MimicPort0Valve
    {
        /// <summary>
        /// Represents the address of the <see cref="MimicPort0Valve"/> register. This field is constant.
        /// </summary>
        public const int Address = 117;

        /// <summary>
        /// Represents the payload type of the <see cref="MimicPort0Valve"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="MimicPort0Valve"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="MimicPort0Valve"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static MimicOutput GetPayload(HarpMessage message)
        {
            return (MimicOutput)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="MimicPort0Valve"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<MimicOutput> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((MimicOutput)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="MimicPort0Valve"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="MimicPort0Valve"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, MimicOutput value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="MimicPort0Valve"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="MimicPort0Valve"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, MimicOutput value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// MimicPort0Valve register.
    /// </summary>
    /// <seealso cref="MimicPort0Valve"/>
    [Description("Filters and selects timestamped messages from the MimicPort0Valve register.")]
    public partial class TimestampedMimicPort0Valve
    {
        /// <summary>
        /// Represents the address of the <see cref="MimicPort0Valve"/> register. This field is constant.
        /// </summary>
        public const int Address = MimicPort0Valve.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="MimicPort0Valve"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<MimicOutput> GetPayload(HarpMessage message)
        {
            return MimicPort0Valve.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that specifies the digital output to mimic the Port 1 valve state.
    /// </summary>
    [Description("Specifies the digital output to mimic the Port 1 valve state.")]
    public partial class MimicPort1Valve
    {
        /// <summary>
        /// Represents the address of the <see cref="MimicPort1Valve"/> register. This field is constant.
        /// </summary>
        public const int Address = 118;

        /// <summary>
        /// Represents the payload type of the <see cref="MimicPort1Valve"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="MimicPort1Valve"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="MimicPort1Valve"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static MimicOutput GetPayload(HarpMessage message)
        {
            return (MimicOutput)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="MimicPort1Valve"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<MimicOutput> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((MimicOutput)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="MimicPort1Valve"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="MimicPort1Valve"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, MimicOutput value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="MimicPort1Valve"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="MimicPort1Valve"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, MimicOutput value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// MimicPort1Valve register.
    /// </summary>
    /// <seealso cref="MimicPort1Valve"/>
    [Description("Filters and selects timestamped messages from the MimicPort1Valve register.")]
    public partial class TimestampedMimicPort1Valve
    {
        /// <summary>
        /// Represents the address of the <see cref="MimicPort1Valve"/> register. This field is constant.
        /// </summary>
        public const int Address = MimicPort1Valve.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="MimicPort1Valve"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<MimicOutput> GetPayload(HarpMessage message)
        {
            return MimicPort1Valve.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that specifies the digital output to mimic the Port 2 valve state.
    /// </summary>
    [Description("Specifies the digital output to mimic the Port 2 valve state.")]
    public partial class MimicPort2Valve
    {
        /// <summary>
        /// Represents the address of the <see cref="MimicPort2Valve"/> register. This field is constant.
        /// </summary>
        public const int Address = 119;

        /// <summary>
        /// Represents the payload type of the <see cref="MimicPort2Valve"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="MimicPort2Valve"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="MimicPort2Valve"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static MimicOutput GetPayload(HarpMessage message)
        {
            return (MimicOutput)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="MimicPort2Valve"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<MimicOutput> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((MimicOutput)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="MimicPort2Valve"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="MimicPort2Valve"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, MimicOutput value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="MimicPort2Valve"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="MimicPort2Valve"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, MimicOutput value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// MimicPort2Valve register.
    /// </summary>
    /// <seealso cref="MimicPort2Valve"/>
    [Description("Filters and selects timestamped messages from the MimicPort2Valve register.")]
    public partial class TimestampedMimicPort2Valve
    {
        /// <summary>
        /// Represents the address of the <see cref="MimicPort2Valve"/> register. This field is constant.
        /// </summary>
        public const int Address = MimicPort2Valve.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="MimicPort2Valve"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<MimicOutput> GetPayload(HarpMessage message)
        {
            return MimicPort2Valve.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that reserved for future use.
    /// </summary>
    [Description("Reserved for future use")]
    internal partial class Reserved23
    {
        /// <summary>
        /// Represents the address of the <see cref="Reserved23"/> register. This field is constant.
        /// </summary>
        public const int Address = 120;

        /// <summary>
        /// Represents the payload type of the <see cref="Reserved23"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Reserved23"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;
    }

    /// <summary>
    /// Represents a register that reserved for future use.
    /// </summary>
    [Description("Reserved for future use")]
    internal partial class Reserved24
    {
        /// <summary>
        /// Represents the address of the <see cref="Reserved24"/> register. This field is constant.
        /// </summary>
        public const int Address = 121;

        /// <summary>
        /// Represents the payload type of the <see cref="Reserved24"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Reserved24"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;
    }

    /// <summary>
    /// Represents a register that specifies the low pass filter time value for poke inputs, in ms.
    /// </summary>
    [Description("Specifies the low pass filter time value for poke inputs, in ms.")]
    public partial class PokeInputFilter
    {
        /// <summary>
        /// Represents the address of the <see cref="PokeInputFilter"/> register. This field is constant.
        /// </summary>
        public const int Address = 122;

        /// <summary>
        /// Represents the payload type of the <see cref="PokeInputFilter"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="PokeInputFilter"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="PokeInputFilter"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static byte GetPayload(HarpMessage message)
        {
            return message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="PokeInputFilter"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<byte> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadByte();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="PokeInputFilter"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="PokeInputFilter"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, byte value)
        {
            return HarpMessage.FromByte(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="PokeInputFilter"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="PokeInputFilter"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, byte value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// PokeInputFilter register.
    /// </summary>
    /// <seealso cref="PokeInputFilter"/>
    [Description("Filters and selects timestamped messages from the PokeInputFilter register.")]
    public partial class TimestampedPokeInputFilter
    {
        /// <summary>
        /// Represents the address of the <see cref="PokeInputFilter"/> register. This field is constant.
        /// </summary>
        public const int Address = PokeInputFilter.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="PokeInputFilter"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<byte> GetPayload(HarpMessage message)
        {
            return PokeInputFilter.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents an operator which creates standard message payloads for the
    /// Behavior device.
    /// </summary>
    /// <seealso cref="CreateDigitalInputStatePayload"/>
    /// <seealso cref="CreateOutputSetPayload"/>
    /// <seealso cref="CreateOutputClearPayload"/>
    /// <seealso cref="CreateOutputTogglePayload"/>
    /// <seealso cref="CreateOutputStatePayload"/>
    /// <seealso cref="CreatePortDIOSetPayload"/>
    /// <seealso cref="CreatePortDIOClearPayload"/>
    /// <seealso cref="CreatePortDIOTogglePayload"/>
    /// <seealso cref="CreatePortDIOStatePayload"/>
    /// <seealso cref="CreatePortDIODirectionPayload"/>
    /// <seealso cref="CreatePortDIOStateEventPayload"/>
    /// <seealso cref="CreateAnalogDataPayload"/>
    /// <seealso cref="CreateOutputPulseEnablePayload"/>
    /// <seealso cref="CreatePulseDOPort0Payload"/>
    /// <seealso cref="CreatePulseDOPort1Payload"/>
    /// <seealso cref="CreatePulseDOPort2Payload"/>
    /// <seealso cref="CreatePulseSupplyPort0Payload"/>
    /// <seealso cref="CreatePulseSupplyPort1Payload"/>
    /// <seealso cref="CreatePulseSupplyPort2Payload"/>
    /// <seealso cref="CreatePulseLed0Payload"/>
    /// <seealso cref="CreatePulseLed1Payload"/>
    /// <seealso cref="CreatePulseRgb0Payload"/>
    /// <seealso cref="CreatePulseRgb1Payload"/>
    /// <seealso cref="CreatePulseDO0Payload"/>
    /// <seealso cref="CreatePulseDO1Payload"/>
    /// <seealso cref="CreatePulseDO2Payload"/>
    /// <seealso cref="CreatePulseDO3Payload"/>
    /// <seealso cref="CreatePwmFrequencyDO0Payload"/>
    /// <seealso cref="CreatePwmFrequencyDO1Payload"/>
    /// <seealso cref="CreatePwmFrequencyDO2Payload"/>
    /// <seealso cref="CreatePwmFrequencyDO3Payload"/>
    /// <seealso cref="CreatePwmDutyCycleDO0Payload"/>
    /// <seealso cref="CreatePwmDutyCycleDO1Payload"/>
    /// <seealso cref="CreatePwmDutyCycleDO2Payload"/>
    /// <seealso cref="CreatePwmDutyCycleDO3Payload"/>
    /// <seealso cref="CreatePwmStartPayload"/>
    /// <seealso cref="CreatePwmStopPayload"/>
    /// <seealso cref="CreateRgbAllPayload"/>
    /// <seealso cref="CreateRgb0Payload"/>
    /// <seealso cref="CreateRgb1Payload"/>
    /// <seealso cref="CreateLed0CurrentPayload"/>
    /// <seealso cref="CreateLed1CurrentPayload"/>
    /// <seealso cref="CreateLed0MaxCurrentPayload"/>
    /// <seealso cref="CreateLed1MaxCurrentPayload"/>
    /// <seealso cref="CreateEventEnablePayload"/>
    /// <seealso cref="CreateStartCamerasPayload"/>
    /// <seealso cref="CreateStopCamerasPayload"/>
    /// <seealso cref="CreateEnableServosPayload"/>
    /// <seealso cref="CreateDisableServosPayload"/>
    /// <seealso cref="CreateEnableEncodersPayload"/>
    /// <seealso cref="CreateEncoderModePayload"/>
    /// <seealso cref="CreateCamera0FramePayload"/>
    /// <seealso cref="CreateCamera0FrequencyPayload"/>
    /// <seealso cref="CreateCamera1FramePayload"/>
    /// <seealso cref="CreateCamera1FrequencyPayload"/>
    /// <seealso cref="CreateServoMotor2PeriodPayload"/>
    /// <seealso cref="CreateServoMotor2PulsePayload"/>
    /// <seealso cref="CreateServoMotor3PeriodPayload"/>
    /// <seealso cref="CreateServoMotor3PulsePayload"/>
    /// <seealso cref="CreateEncoderResetPayload"/>
    /// <seealso cref="CreateEnableSerialTimestampPayload"/>
    /// <seealso cref="CreateMimicPort0IRPayload"/>
    /// <seealso cref="CreateMimicPort1IRPayload"/>
    /// <seealso cref="CreateMimicPort2IRPayload"/>
    /// <seealso cref="CreateMimicPort0ValvePayload"/>
    /// <seealso cref="CreateMimicPort1ValvePayload"/>
    /// <seealso cref="CreateMimicPort2ValvePayload"/>
    /// <seealso cref="CreatePokeInputFilterPayload"/>
    [XmlInclude(typeof(CreateDigitalInputStatePayload))]
    [XmlInclude(typeof(CreateOutputSetPayload))]
    [XmlInclude(typeof(CreateOutputClearPayload))]
    [XmlInclude(typeof(CreateOutputTogglePayload))]
    [XmlInclude(typeof(CreateOutputStatePayload))]
    [XmlInclude(typeof(CreatePortDIOSetPayload))]
    [XmlInclude(typeof(CreatePortDIOClearPayload))]
    [XmlInclude(typeof(CreatePortDIOTogglePayload))]
    [XmlInclude(typeof(CreatePortDIOStatePayload))]
    [XmlInclude(typeof(CreatePortDIODirectionPayload))]
    [XmlInclude(typeof(CreatePortDIOStateEventPayload))]
    [XmlInclude(typeof(CreateAnalogDataPayload))]
    [XmlInclude(typeof(CreateOutputPulseEnablePayload))]
    [XmlInclude(typeof(CreatePulseDOPort0Payload))]
    [XmlInclude(typeof(CreatePulseDOPort1Payload))]
    [XmlInclude(typeof(CreatePulseDOPort2Payload))]
    [XmlInclude(typeof(CreatePulseSupplyPort0Payload))]
    [XmlInclude(typeof(CreatePulseSupplyPort1Payload))]
    [XmlInclude(typeof(CreatePulseSupplyPort2Payload))]
    [XmlInclude(typeof(CreatePulseLed0Payload))]
    [XmlInclude(typeof(CreatePulseLed1Payload))]
    [XmlInclude(typeof(CreatePulseRgb0Payload))]
    [XmlInclude(typeof(CreatePulseRgb1Payload))]
    [XmlInclude(typeof(CreatePulseDO0Payload))]
    [XmlInclude(typeof(CreatePulseDO1Payload))]
    [XmlInclude(typeof(CreatePulseDO2Payload))]
    [XmlInclude(typeof(CreatePulseDO3Payload))]
    [XmlInclude(typeof(CreatePwmFrequencyDO0Payload))]
    [XmlInclude(typeof(CreatePwmFrequencyDO1Payload))]
    [XmlInclude(typeof(CreatePwmFrequencyDO2Payload))]
    [XmlInclude(typeof(CreatePwmFrequencyDO3Payload))]
    [XmlInclude(typeof(CreatePwmDutyCycleDO0Payload))]
    [XmlInclude(typeof(CreatePwmDutyCycleDO1Payload))]
    [XmlInclude(typeof(CreatePwmDutyCycleDO2Payload))]
    [XmlInclude(typeof(CreatePwmDutyCycleDO3Payload))]
    [XmlInclude(typeof(CreatePwmStartPayload))]
    [XmlInclude(typeof(CreatePwmStopPayload))]
    [XmlInclude(typeof(CreateRgbAllPayload))]
    [XmlInclude(typeof(CreateRgb0Payload))]
    [XmlInclude(typeof(CreateRgb1Payload))]
    [XmlInclude(typeof(CreateLed0CurrentPayload))]
    [XmlInclude(typeof(CreateLed1CurrentPayload))]
    [XmlInclude(typeof(CreateLed0MaxCurrentPayload))]
    [XmlInclude(typeof(CreateLed1MaxCurrentPayload))]
    [XmlInclude(typeof(CreateEventEnablePayload))]
    [XmlInclude(typeof(CreateStartCamerasPayload))]
    [XmlInclude(typeof(CreateStopCamerasPayload))]
    [XmlInclude(typeof(CreateEnableServosPayload))]
    [XmlInclude(typeof(CreateDisableServosPayload))]
    [XmlInclude(typeof(CreateEnableEncodersPayload))]
    [XmlInclude(typeof(CreateEncoderModePayload))]
    [XmlInclude(typeof(CreateCamera0FramePayload))]
    [XmlInclude(typeof(CreateCamera0FrequencyPayload))]
    [XmlInclude(typeof(CreateCamera1FramePayload))]
    [XmlInclude(typeof(CreateCamera1FrequencyPayload))]
    [XmlInclude(typeof(CreateServoMotor2PeriodPayload))]
    [XmlInclude(typeof(CreateServoMotor2PulsePayload))]
    [XmlInclude(typeof(CreateServoMotor3PeriodPayload))]
    [XmlInclude(typeof(CreateServoMotor3PulsePayload))]
    [XmlInclude(typeof(CreateEncoderResetPayload))]
    [XmlInclude(typeof(CreateEnableSerialTimestampPayload))]
    [XmlInclude(typeof(CreateMimicPort0IRPayload))]
    [XmlInclude(typeof(CreateMimicPort1IRPayload))]
    [XmlInclude(typeof(CreateMimicPort2IRPayload))]
    [XmlInclude(typeof(CreateMimicPort0ValvePayload))]
    [XmlInclude(typeof(CreateMimicPort1ValvePayload))]
    [XmlInclude(typeof(CreateMimicPort2ValvePayload))]
    [XmlInclude(typeof(CreatePokeInputFilterPayload))]
    [XmlInclude(typeof(CreateTimestampedDigitalInputStatePayload))]
    [XmlInclude(typeof(CreateTimestampedOutputSetPayload))]
    [XmlInclude(typeof(CreateTimestampedOutputClearPayload))]
    [XmlInclude(typeof(CreateTimestampedOutputTogglePayload))]
    [XmlInclude(typeof(CreateTimestampedOutputStatePayload))]
    [XmlInclude(typeof(CreateTimestampedPortDIOSetPayload))]
    [XmlInclude(typeof(CreateTimestampedPortDIOClearPayload))]
    [XmlInclude(typeof(CreateTimestampedPortDIOTogglePayload))]
    [XmlInclude(typeof(CreateTimestampedPortDIOStatePayload))]
    [XmlInclude(typeof(CreateTimestampedPortDIODirectionPayload))]
    [XmlInclude(typeof(CreateTimestampedPortDIOStateEventPayload))]
    [XmlInclude(typeof(CreateTimestampedAnalogDataPayload))]
    [XmlInclude(typeof(CreateTimestampedOutputPulseEnablePayload))]
    [XmlInclude(typeof(CreateTimestampedPulseDOPort0Payload))]
    [XmlInclude(typeof(CreateTimestampedPulseDOPort1Payload))]
    [XmlInclude(typeof(CreateTimestampedPulseDOPort2Payload))]
    [XmlInclude(typeof(CreateTimestampedPulseSupplyPort0Payload))]
    [XmlInclude(typeof(CreateTimestampedPulseSupplyPort1Payload))]
    [XmlInclude(typeof(CreateTimestampedPulseSupplyPort2Payload))]
    [XmlInclude(typeof(CreateTimestampedPulseLed0Payload))]
    [XmlInclude(typeof(CreateTimestampedPulseLed1Payload))]
    [XmlInclude(typeof(CreateTimestampedPulseRgb0Payload))]
    [XmlInclude(typeof(CreateTimestampedPulseRgb1Payload))]
    [XmlInclude(typeof(CreateTimestampedPulseDO0Payload))]
    [XmlInclude(typeof(CreateTimestampedPulseDO1Payload))]
    [XmlInclude(typeof(CreateTimestampedPulseDO2Payload))]
    [XmlInclude(typeof(CreateTimestampedPulseDO3Payload))]
    [XmlInclude(typeof(CreateTimestampedPwmFrequencyDO0Payload))]
    [XmlInclude(typeof(CreateTimestampedPwmFrequencyDO1Payload))]
    [XmlInclude(typeof(CreateTimestampedPwmFrequencyDO2Payload))]
    [XmlInclude(typeof(CreateTimestampedPwmFrequencyDO3Payload))]
    [XmlInclude(typeof(CreateTimestampedPwmDutyCycleDO0Payload))]
    [XmlInclude(typeof(CreateTimestampedPwmDutyCycleDO1Payload))]
    [XmlInclude(typeof(CreateTimestampedPwmDutyCycleDO2Payload))]
    [XmlInclude(typeof(CreateTimestampedPwmDutyCycleDO3Payload))]
    [XmlInclude(typeof(CreateTimestampedPwmStartPayload))]
    [XmlInclude(typeof(CreateTimestampedPwmStopPayload))]
    [XmlInclude(typeof(CreateTimestampedRgbAllPayload))]
    [XmlInclude(typeof(CreateTimestampedRgb0Payload))]
    [XmlInclude(typeof(CreateTimestampedRgb1Payload))]
    [XmlInclude(typeof(CreateTimestampedLed0CurrentPayload))]
    [XmlInclude(typeof(CreateTimestampedLed1CurrentPayload))]
    [XmlInclude(typeof(CreateTimestampedLed0MaxCurrentPayload))]
    [XmlInclude(typeof(CreateTimestampedLed1MaxCurrentPayload))]
    [XmlInclude(typeof(CreateTimestampedEventEnablePayload))]
    [XmlInclude(typeof(CreateTimestampedStartCamerasPayload))]
    [XmlInclude(typeof(CreateTimestampedStopCamerasPayload))]
    [XmlInclude(typeof(CreateTimestampedEnableServosPayload))]
    [XmlInclude(typeof(CreateTimestampedDisableServosPayload))]
    [XmlInclude(typeof(CreateTimestampedEnableEncodersPayload))]
    [XmlInclude(typeof(CreateTimestampedEncoderModePayload))]
    [XmlInclude(typeof(CreateTimestampedCamera0FramePayload))]
    [XmlInclude(typeof(CreateTimestampedCamera0FrequencyPayload))]
    [XmlInclude(typeof(CreateTimestampedCamera1FramePayload))]
    [XmlInclude(typeof(CreateTimestampedCamera1FrequencyPayload))]
    [XmlInclude(typeof(CreateTimestampedServoMotor2PeriodPayload))]
    [XmlInclude(typeof(CreateTimestampedServoMotor2PulsePayload))]
    [XmlInclude(typeof(CreateTimestampedServoMotor3PeriodPayload))]
    [XmlInclude(typeof(CreateTimestampedServoMotor3PulsePayload))]
    [XmlInclude(typeof(CreateTimestampedEncoderResetPayload))]
    [XmlInclude(typeof(CreateTimestampedEnableSerialTimestampPayload))]
    [XmlInclude(typeof(CreateTimestampedMimicPort0IRPayload))]
    [XmlInclude(typeof(CreateTimestampedMimicPort1IRPayload))]
    [XmlInclude(typeof(CreateTimestampedMimicPort2IRPayload))]
    [XmlInclude(typeof(CreateTimestampedMimicPort0ValvePayload))]
    [XmlInclude(typeof(CreateTimestampedMimicPort1ValvePayload))]
    [XmlInclude(typeof(CreateTimestampedMimicPort2ValvePayload))]
    [XmlInclude(typeof(CreateTimestampedPokeInputFilterPayload))]
    [Description("Creates standard message payloads for the Behavior device.")]
    public partial class CreateMessage : CreateMessageBuilder, INamedElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateMessage"/> class.
        /// </summary>
        public CreateMessage()
        {
            Payload = new CreateDigitalInputStatePayload();
        }

        string INamedElement.Name => $"{nameof(Behavior)}.{GetElementDisplayName(Payload)}";
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that reflects the state of DI digital lines of each Port.
    /// </summary>
    [DisplayName("DigitalInputStatePayload")]
    [Description("Creates a message payload that reflects the state of DI digital lines of each Port.")]
    public partial class CreateDigitalInputStatePayload
    {
        /// <summary>
        /// Gets or sets the value that reflects the state of DI digital lines of each Port.
        /// </summary>
        [Description("The value that reflects the state of DI digital lines of each Port.")]
        public DigitalInputs DigitalInputState { get; set; }

        /// <summary>
        /// Creates a message payload for the DigitalInputState register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public DigitalInputs GetPayload()
        {
            return DigitalInputState;
        }

        /// <summary>
        /// Creates a message that reflects the state of DI digital lines of each Port.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DigitalInputState register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.Behavior.DigitalInputState.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that reflects the state of DI digital lines of each Port.
    /// </summary>
    [DisplayName("TimestampedDigitalInputStatePayload")]
    [Description("Creates a timestamped message payload that reflects the state of DI digital lines of each Port.")]
    public partial class CreateTimestampedDigitalInputStatePayload : CreateDigitalInputStatePayload
    {
        /// <summary>
        /// Creates a timestamped message that reflects the state of DI digital lines of each Port.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DigitalInputState register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.Behavior.DigitalInputState.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that set the specified digital output lines.
    /// </summary>
    [DisplayName("OutputSetPayload")]
    [Description("Creates a message payload that set the specified digital output lines.")]
    public partial class CreateOutputSetPayload
    {
        /// <summary>
        /// Gets or sets the value that set the specified digital output lines.
        /// </summary>
        [Description("The value that set the specified digital output lines.")]
        public DigitalOutputs OutputSet { get; set; }

        /// <summary>
        /// Creates a message payload for the OutputSet register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public DigitalOutputs GetPayload()
        {
            return OutputSet;
        }

        /// <summary>
        /// Creates a message that set the specified digital output lines.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the OutputSet register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.Behavior.OutputSet.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that set the specified digital output lines.
    /// </summary>
    [DisplayName("TimestampedOutputSetPayload")]
    [Description("Creates a timestamped message payload that set the specified digital output lines.")]
    public partial class CreateTimestampedOutputSetPayload : CreateOutputSetPayload
    {
        /// <summary>
        /// Creates a timestamped message that set the specified digital output lines.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the OutputSet register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.Behavior.OutputSet.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that clear the specified digital output lines.
    /// </summary>
    [DisplayName("OutputClearPayload")]
    [Description("Creates a message payload that clear the specified digital output lines.")]
    public partial class CreateOutputClearPayload
    {
        /// <summary>
        /// Gets or sets the value that clear the specified digital output lines.
        /// </summary>
        [Description("The value that clear the specified digital output lines.")]
        public DigitalOutputs OutputClear { get; set; }

        /// <summary>
        /// Creates a message payload for the OutputClear register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public DigitalOutputs GetPayload()
        {
            return OutputClear;
        }

        /// <summary>
        /// Creates a message that clear the specified digital output lines.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the OutputClear register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.Behavior.OutputClear.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that clear the specified digital output lines.
    /// </summary>
    [DisplayName("TimestampedOutputClearPayload")]
    [Description("Creates a timestamped message payload that clear the specified digital output lines.")]
    public partial class CreateTimestampedOutputClearPayload : CreateOutputClearPayload
    {
        /// <summary>
        /// Creates a timestamped message that clear the specified digital output lines.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the OutputClear register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.Behavior.OutputClear.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that toggle the specified digital output lines.
    /// </summary>
    [DisplayName("OutputTogglePayload")]
    [Description("Creates a message payload that toggle the specified digital output lines.")]
    public partial class CreateOutputTogglePayload
    {
        /// <summary>
        /// Gets or sets the value that toggle the specified digital output lines.
        /// </summary>
        [Description("The value that toggle the specified digital output lines.")]
        public DigitalOutputs OutputToggle { get; set; }

        /// <summary>
        /// Creates a message payload for the OutputToggle register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public DigitalOutputs GetPayload()
        {
            return OutputToggle;
        }

        /// <summary>
        /// Creates a message that toggle the specified digital output lines.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the OutputToggle register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.Behavior.OutputToggle.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that toggle the specified digital output lines.
    /// </summary>
    [DisplayName("TimestampedOutputTogglePayload")]
    [Description("Creates a timestamped message payload that toggle the specified digital output lines.")]
    public partial class CreateTimestampedOutputTogglePayload : CreateOutputTogglePayload
    {
        /// <summary>
        /// Creates a timestamped message that toggle the specified digital output lines.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the OutputToggle register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.Behavior.OutputToggle.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that write the state of all digital output lines.
    /// </summary>
    [DisplayName("OutputStatePayload")]
    [Description("Creates a message payload that write the state of all digital output lines.")]
    public partial class CreateOutputStatePayload
    {
        /// <summary>
        /// Gets or sets the value that write the state of all digital output lines.
        /// </summary>
        [Description("The value that write the state of all digital output lines.")]
        public DigitalOutputs OutputState { get; set; }

        /// <summary>
        /// Creates a message payload for the OutputState register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public DigitalOutputs GetPayload()
        {
            return OutputState;
        }

        /// <summary>
        /// Creates a message that write the state of all digital output lines.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the OutputState register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.Behavior.OutputState.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that write the state of all digital output lines.
    /// </summary>
    [DisplayName("TimestampedOutputStatePayload")]
    [Description("Creates a timestamped message payload that write the state of all digital output lines.")]
    public partial class CreateTimestampedOutputStatePayload : CreateOutputStatePayload
    {
        /// <summary>
        /// Creates a timestamped message that write the state of all digital output lines.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the OutputState register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.Behavior.OutputState.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that set the specified port DIO lines.
    /// </summary>
    [DisplayName("PortDIOSetPayload")]
    [Description("Creates a message payload that set the specified port DIO lines.")]
    public partial class CreatePortDIOSetPayload
    {
        /// <summary>
        /// Gets or sets the value that set the specified port DIO lines.
        /// </summary>
        [Description("The value that set the specified port DIO lines.")]
        public PortDigitalIOS PortDIOSet { get; set; }

        /// <summary>
        /// Creates a message payload for the PortDIOSet register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public PortDigitalIOS GetPayload()
        {
            return PortDIOSet;
        }

        /// <summary>
        /// Creates a message that set the specified port DIO lines.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the PortDIOSet register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.Behavior.PortDIOSet.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that set the specified port DIO lines.
    /// </summary>
    [DisplayName("TimestampedPortDIOSetPayload")]
    [Description("Creates a timestamped message payload that set the specified port DIO lines.")]
    public partial class CreateTimestampedPortDIOSetPayload : CreatePortDIOSetPayload
    {
        /// <summary>
        /// Creates a timestamped message that set the specified port DIO lines.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the PortDIOSet register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.Behavior.PortDIOSet.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that clear the specified port DIO lines.
    /// </summary>
    [DisplayName("PortDIOClearPayload")]
    [Description("Creates a message payload that clear the specified port DIO lines.")]
    public partial class CreatePortDIOClearPayload
    {
        /// <summary>
        /// Gets or sets the value that clear the specified port DIO lines.
        /// </summary>
        [Description("The value that clear the specified port DIO lines.")]
        public PortDigitalIOS PortDIOClear { get; set; }

        /// <summary>
        /// Creates a message payload for the PortDIOClear register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public PortDigitalIOS GetPayload()
        {
            return PortDIOClear;
        }

        /// <summary>
        /// Creates a message that clear the specified port DIO lines.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the PortDIOClear register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.Behavior.PortDIOClear.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that clear the specified port DIO lines.
    /// </summary>
    [DisplayName("TimestampedPortDIOClearPayload")]
    [Description("Creates a timestamped message payload that clear the specified port DIO lines.")]
    public partial class CreateTimestampedPortDIOClearPayload : CreatePortDIOClearPayload
    {
        /// <summary>
        /// Creates a timestamped message that clear the specified port DIO lines.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the PortDIOClear register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.Behavior.PortDIOClear.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that toggle the specified port DIO lines.
    /// </summary>
    [DisplayName("PortDIOTogglePayload")]
    [Description("Creates a message payload that toggle the specified port DIO lines.")]
    public partial class CreatePortDIOTogglePayload
    {
        /// <summary>
        /// Gets or sets the value that toggle the specified port DIO lines.
        /// </summary>
        [Description("The value that toggle the specified port DIO lines.")]
        public PortDigitalIOS PortDIOToggle { get; set; }

        /// <summary>
        /// Creates a message payload for the PortDIOToggle register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public PortDigitalIOS GetPayload()
        {
            return PortDIOToggle;
        }

        /// <summary>
        /// Creates a message that toggle the specified port DIO lines.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the PortDIOToggle register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.Behavior.PortDIOToggle.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that toggle the specified port DIO lines.
    /// </summary>
    [DisplayName("TimestampedPortDIOTogglePayload")]
    [Description("Creates a timestamped message payload that toggle the specified port DIO lines.")]
    public partial class CreateTimestampedPortDIOTogglePayload : CreatePortDIOTogglePayload
    {
        /// <summary>
        /// Creates a timestamped message that toggle the specified port DIO lines.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the PortDIOToggle register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.Behavior.PortDIOToggle.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that write the state of all port DIO lines.
    /// </summary>
    [DisplayName("PortDIOStatePayload")]
    [Description("Creates a message payload that write the state of all port DIO lines.")]
    public partial class CreatePortDIOStatePayload
    {
        /// <summary>
        /// Gets or sets the value that write the state of all port DIO lines.
        /// </summary>
        [Description("The value that write the state of all port DIO lines.")]
        public PortDigitalIOS PortDIOState { get; set; }

        /// <summary>
        /// Creates a message payload for the PortDIOState register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public PortDigitalIOS GetPayload()
        {
            return PortDIOState;
        }

        /// <summary>
        /// Creates a message that write the state of all port DIO lines.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the PortDIOState register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.Behavior.PortDIOState.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that write the state of all port DIO lines.
    /// </summary>
    [DisplayName("TimestampedPortDIOStatePayload")]
    [Description("Creates a timestamped message payload that write the state of all port DIO lines.")]
    public partial class CreateTimestampedPortDIOStatePayload : CreatePortDIOStatePayload
    {
        /// <summary>
        /// Creates a timestamped message that write the state of all port DIO lines.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the PortDIOState register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.Behavior.PortDIOState.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that specifies which of the port DIO lines are outputs.
    /// </summary>
    [DisplayName("PortDIODirectionPayload")]
    [Description("Creates a message payload that specifies which of the port DIO lines are outputs.")]
    public partial class CreatePortDIODirectionPayload
    {
        /// <summary>
        /// Gets or sets the value that specifies which of the port DIO lines are outputs.
        /// </summary>
        [Description("The value that specifies which of the port DIO lines are outputs.")]
        public PortDigitalIOS PortDIODirection { get; set; }

        /// <summary>
        /// Creates a message payload for the PortDIODirection register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public PortDigitalIOS GetPayload()
        {
            return PortDIODirection;
        }

        /// <summary>
        /// Creates a message that specifies which of the port DIO lines are outputs.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the PortDIODirection register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.Behavior.PortDIODirection.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that specifies which of the port DIO lines are outputs.
    /// </summary>
    [DisplayName("TimestampedPortDIODirectionPayload")]
    [Description("Creates a timestamped message payload that specifies which of the port DIO lines are outputs.")]
    public partial class CreateTimestampedPortDIODirectionPayload : CreatePortDIODirectionPayload
    {
        /// <summary>
        /// Creates a timestamped message that specifies which of the port DIO lines are outputs.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the PortDIODirection register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.Behavior.PortDIODirection.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that specifies the state of the port DIO lines on a line change.
    /// </summary>
    [DisplayName("PortDIOStateEventPayload")]
    [Description("Creates a message payload that specifies the state of the port DIO lines on a line change.")]
    public partial class CreatePortDIOStateEventPayload
    {
        /// <summary>
        /// Gets or sets the value that specifies the state of the port DIO lines on a line change.
        /// </summary>
        [Description("The value that specifies the state of the port DIO lines on a line change.")]
        public PortDigitalIOS PortDIOStateEvent { get; set; }

        /// <summary>
        /// Creates a message payload for the PortDIOStateEvent register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public PortDigitalIOS GetPayload()
        {
            return PortDIOStateEvent;
        }

        /// <summary>
        /// Creates a message that specifies the state of the port DIO lines on a line change.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the PortDIOStateEvent register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.Behavior.PortDIOStateEvent.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that specifies the state of the port DIO lines on a line change.
    /// </summary>
    [DisplayName("TimestampedPortDIOStateEventPayload")]
    [Description("Creates a timestamped message payload that specifies the state of the port DIO lines on a line change.")]
    public partial class CreateTimestampedPortDIOStateEventPayload : CreatePortDIOStateEventPayload
    {
        /// <summary>
        /// Creates a timestamped message that specifies the state of the port DIO lines on a line change.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the PortDIOStateEvent register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.Behavior.PortDIOStateEvent.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that voltage at the ADC input and encoder value on Port 2.
    /// </summary>
    [DisplayName("AnalogDataPayload")]
    [Description("Creates a message payload that voltage at the ADC input and encoder value on Port 2.")]
    public partial class CreateAnalogDataPayload
    {
        /// <summary>
        /// Gets or sets a value that the voltage at the output of the ADC channel 0.
        /// </summary>
        [Description("The voltage at the output of the ADC channel 0.")]
        public short AnalogInput0 { get; set; }

        /// <summary>
        /// Gets or sets a value that the quadrature counter value on Port 2.
        /// </summary>
        [Description("The quadrature counter value on Port 2")]
        public short Encoder { get; set; }

        /// <summary>
        /// Gets or sets a value that the voltage at the output of the ADC channel 1.
        /// </summary>
        [Description("The voltage at the output of the ADC channel 1.")]
        public short AnalogInput1 { get; set; }

        /// <summary>
        /// Creates a message payload for the AnalogData register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public AnalogDataPayload GetPayload()
        {
            AnalogDataPayload value;
            value.AnalogInput0 = AnalogInput0;
            value.Encoder = Encoder;
            value.AnalogInput1 = AnalogInput1;
            return value;
        }

        /// <summary>
        /// Creates a message that voltage at the ADC input and encoder value on Port 2.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the AnalogData register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.Behavior.AnalogData.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that voltage at the ADC input and encoder value on Port 2.
    /// </summary>
    [DisplayName("TimestampedAnalogDataPayload")]
    [Description("Creates a timestamped message payload that voltage at the ADC input and encoder value on Port 2.")]
    public partial class CreateTimestampedAnalogDataPayload : CreateAnalogDataPayload
    {
        /// <summary>
        /// Creates a timestamped message that voltage at the ADC input and encoder value on Port 2.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the AnalogData register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.Behavior.AnalogData.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that enables the pulse function for the specified output lines.
    /// </summary>
    [DisplayName("OutputPulseEnablePayload")]
    [Description("Creates a message payload that enables the pulse function for the specified output lines.")]
    public partial class CreateOutputPulseEnablePayload
    {
        /// <summary>
        /// Gets or sets the value that enables the pulse function for the specified output lines.
        /// </summary>
        [Description("The value that enables the pulse function for the specified output lines.")]
        public DigitalOutputs OutputPulseEnable { get; set; }

        /// <summary>
        /// Creates a message payload for the OutputPulseEnable register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public DigitalOutputs GetPayload()
        {
            return OutputPulseEnable;
        }

        /// <summary>
        /// Creates a message that enables the pulse function for the specified output lines.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the OutputPulseEnable register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.Behavior.OutputPulseEnable.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that enables the pulse function for the specified output lines.
    /// </summary>
    [DisplayName("TimestampedOutputPulseEnablePayload")]
    [Description("Creates a timestamped message payload that enables the pulse function for the specified output lines.")]
    public partial class CreateTimestampedOutputPulseEnablePayload : CreateOutputPulseEnablePayload
    {
        /// <summary>
        /// Creates a timestamped message that enables the pulse function for the specified output lines.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the OutputPulseEnable register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.Behavior.OutputPulseEnable.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that specifies the duration of the output pulse in milliseconds.
    /// </summary>
    [DisplayName("PulseDOPort0Payload")]
    [Description("Creates a message payload that specifies the duration of the output pulse in milliseconds.")]
    public partial class CreatePulseDOPort0Payload
    {
        /// <summary>
        /// Gets or sets the value that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        [Range(min: 1, max: long.MaxValue)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that specifies the duration of the output pulse in milliseconds.")]
        public ushort PulseDOPort0 { get; set; } = 1;

        /// <summary>
        /// Creates a message payload for the PulseDOPort0 register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return PulseDOPort0;
        }

        /// <summary>
        /// Creates a message that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the PulseDOPort0 register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.Behavior.PulseDOPort0.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that specifies the duration of the output pulse in milliseconds.
    /// </summary>
    [DisplayName("TimestampedPulseDOPort0Payload")]
    [Description("Creates a timestamped message payload that specifies the duration of the output pulse in milliseconds.")]
    public partial class CreateTimestampedPulseDOPort0Payload : CreatePulseDOPort0Payload
    {
        /// <summary>
        /// Creates a timestamped message that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the PulseDOPort0 register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.Behavior.PulseDOPort0.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that specifies the duration of the output pulse in milliseconds.
    /// </summary>
    [DisplayName("PulseDOPort1Payload")]
    [Description("Creates a message payload that specifies the duration of the output pulse in milliseconds.")]
    public partial class CreatePulseDOPort1Payload
    {
        /// <summary>
        /// Gets or sets the value that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        [Range(min: 1, max: long.MaxValue)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that specifies the duration of the output pulse in milliseconds.")]
        public ushort PulseDOPort1 { get; set; } = 1;

        /// <summary>
        /// Creates a message payload for the PulseDOPort1 register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return PulseDOPort1;
        }

        /// <summary>
        /// Creates a message that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the PulseDOPort1 register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.Behavior.PulseDOPort1.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that specifies the duration of the output pulse in milliseconds.
    /// </summary>
    [DisplayName("TimestampedPulseDOPort1Payload")]
    [Description("Creates a timestamped message payload that specifies the duration of the output pulse in milliseconds.")]
    public partial class CreateTimestampedPulseDOPort1Payload : CreatePulseDOPort1Payload
    {
        /// <summary>
        /// Creates a timestamped message that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the PulseDOPort1 register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.Behavior.PulseDOPort1.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that specifies the duration of the output pulse in milliseconds.
    /// </summary>
    [DisplayName("PulseDOPort2Payload")]
    [Description("Creates a message payload that specifies the duration of the output pulse in milliseconds.")]
    public partial class CreatePulseDOPort2Payload
    {
        /// <summary>
        /// Gets or sets the value that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        [Range(min: 1, max: long.MaxValue)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that specifies the duration of the output pulse in milliseconds.")]
        public ushort PulseDOPort2 { get; set; } = 1;

        /// <summary>
        /// Creates a message payload for the PulseDOPort2 register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return PulseDOPort2;
        }

        /// <summary>
        /// Creates a message that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the PulseDOPort2 register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.Behavior.PulseDOPort2.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that specifies the duration of the output pulse in milliseconds.
    /// </summary>
    [DisplayName("TimestampedPulseDOPort2Payload")]
    [Description("Creates a timestamped message payload that specifies the duration of the output pulse in milliseconds.")]
    public partial class CreateTimestampedPulseDOPort2Payload : CreatePulseDOPort2Payload
    {
        /// <summary>
        /// Creates a timestamped message that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the PulseDOPort2 register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.Behavior.PulseDOPort2.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that specifies the duration of the output pulse in milliseconds.
    /// </summary>
    [DisplayName("PulseSupplyPort0Payload")]
    [Description("Creates a message payload that specifies the duration of the output pulse in milliseconds.")]
    public partial class CreatePulseSupplyPort0Payload
    {
        /// <summary>
        /// Gets or sets the value that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        [Range(min: 1, max: long.MaxValue)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that specifies the duration of the output pulse in milliseconds.")]
        public ushort PulseSupplyPort0 { get; set; } = 1;

        /// <summary>
        /// Creates a message payload for the PulseSupplyPort0 register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return PulseSupplyPort0;
        }

        /// <summary>
        /// Creates a message that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the PulseSupplyPort0 register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.Behavior.PulseSupplyPort0.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that specifies the duration of the output pulse in milliseconds.
    /// </summary>
    [DisplayName("TimestampedPulseSupplyPort0Payload")]
    [Description("Creates a timestamped message payload that specifies the duration of the output pulse in milliseconds.")]
    public partial class CreateTimestampedPulseSupplyPort0Payload : CreatePulseSupplyPort0Payload
    {
        /// <summary>
        /// Creates a timestamped message that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the PulseSupplyPort0 register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.Behavior.PulseSupplyPort0.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that specifies the duration of the output pulse in milliseconds.
    /// </summary>
    [DisplayName("PulseSupplyPort1Payload")]
    [Description("Creates a message payload that specifies the duration of the output pulse in milliseconds.")]
    public partial class CreatePulseSupplyPort1Payload
    {
        /// <summary>
        /// Gets or sets the value that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        [Range(min: 1, max: long.MaxValue)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that specifies the duration of the output pulse in milliseconds.")]
        public ushort PulseSupplyPort1 { get; set; } = 1;

        /// <summary>
        /// Creates a message payload for the PulseSupplyPort1 register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return PulseSupplyPort1;
        }

        /// <summary>
        /// Creates a message that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the PulseSupplyPort1 register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.Behavior.PulseSupplyPort1.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that specifies the duration of the output pulse in milliseconds.
    /// </summary>
    [DisplayName("TimestampedPulseSupplyPort1Payload")]
    [Description("Creates a timestamped message payload that specifies the duration of the output pulse in milliseconds.")]
    public partial class CreateTimestampedPulseSupplyPort1Payload : CreatePulseSupplyPort1Payload
    {
        /// <summary>
        /// Creates a timestamped message that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the PulseSupplyPort1 register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.Behavior.PulseSupplyPort1.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that specifies the duration of the output pulse in milliseconds.
    /// </summary>
    [DisplayName("PulseSupplyPort2Payload")]
    [Description("Creates a message payload that specifies the duration of the output pulse in milliseconds.")]
    public partial class CreatePulseSupplyPort2Payload
    {
        /// <summary>
        /// Gets or sets the value that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        [Range(min: 1, max: long.MaxValue)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that specifies the duration of the output pulse in milliseconds.")]
        public ushort PulseSupplyPort2 { get; set; } = 1;

        /// <summary>
        /// Creates a message payload for the PulseSupplyPort2 register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return PulseSupplyPort2;
        }

        /// <summary>
        /// Creates a message that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the PulseSupplyPort2 register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.Behavior.PulseSupplyPort2.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that specifies the duration of the output pulse in milliseconds.
    /// </summary>
    [DisplayName("TimestampedPulseSupplyPort2Payload")]
    [Description("Creates a timestamped message payload that specifies the duration of the output pulse in milliseconds.")]
    public partial class CreateTimestampedPulseSupplyPort2Payload : CreatePulseSupplyPort2Payload
    {
        /// <summary>
        /// Creates a timestamped message that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the PulseSupplyPort2 register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.Behavior.PulseSupplyPort2.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that specifies the duration of the output pulse in milliseconds.
    /// </summary>
    [DisplayName("PulseLed0Payload")]
    [Description("Creates a message payload that specifies the duration of the output pulse in milliseconds.")]
    public partial class CreatePulseLed0Payload
    {
        /// <summary>
        /// Gets or sets the value that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        [Range(min: 1, max: long.MaxValue)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that specifies the duration of the output pulse in milliseconds.")]
        public ushort PulseLed0 { get; set; } = 1;

        /// <summary>
        /// Creates a message payload for the PulseLed0 register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return PulseLed0;
        }

        /// <summary>
        /// Creates a message that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the PulseLed0 register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.Behavior.PulseLed0.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that specifies the duration of the output pulse in milliseconds.
    /// </summary>
    [DisplayName("TimestampedPulseLed0Payload")]
    [Description("Creates a timestamped message payload that specifies the duration of the output pulse in milliseconds.")]
    public partial class CreateTimestampedPulseLed0Payload : CreatePulseLed0Payload
    {
        /// <summary>
        /// Creates a timestamped message that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the PulseLed0 register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.Behavior.PulseLed0.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that specifies the duration of the output pulse in milliseconds.
    /// </summary>
    [DisplayName("PulseLed1Payload")]
    [Description("Creates a message payload that specifies the duration of the output pulse in milliseconds.")]
    public partial class CreatePulseLed1Payload
    {
        /// <summary>
        /// Gets or sets the value that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        [Range(min: 1, max: long.MaxValue)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that specifies the duration of the output pulse in milliseconds.")]
        public ushort PulseLed1 { get; set; } = 1;

        /// <summary>
        /// Creates a message payload for the PulseLed1 register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return PulseLed1;
        }

        /// <summary>
        /// Creates a message that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the PulseLed1 register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.Behavior.PulseLed1.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that specifies the duration of the output pulse in milliseconds.
    /// </summary>
    [DisplayName("TimestampedPulseLed1Payload")]
    [Description("Creates a timestamped message payload that specifies the duration of the output pulse in milliseconds.")]
    public partial class CreateTimestampedPulseLed1Payload : CreatePulseLed1Payload
    {
        /// <summary>
        /// Creates a timestamped message that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the PulseLed1 register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.Behavior.PulseLed1.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that specifies the duration of the output pulse in milliseconds.
    /// </summary>
    [DisplayName("PulseRgb0Payload")]
    [Description("Creates a message payload that specifies the duration of the output pulse in milliseconds.")]
    public partial class CreatePulseRgb0Payload
    {
        /// <summary>
        /// Gets or sets the value that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        [Range(min: 1, max: long.MaxValue)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that specifies the duration of the output pulse in milliseconds.")]
        public ushort PulseRgb0 { get; set; } = 1;

        /// <summary>
        /// Creates a message payload for the PulseRgb0 register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return PulseRgb0;
        }

        /// <summary>
        /// Creates a message that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the PulseRgb0 register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.Behavior.PulseRgb0.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that specifies the duration of the output pulse in milliseconds.
    /// </summary>
    [DisplayName("TimestampedPulseRgb0Payload")]
    [Description("Creates a timestamped message payload that specifies the duration of the output pulse in milliseconds.")]
    public partial class CreateTimestampedPulseRgb0Payload : CreatePulseRgb0Payload
    {
        /// <summary>
        /// Creates a timestamped message that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the PulseRgb0 register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.Behavior.PulseRgb0.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that specifies the duration of the output pulse in milliseconds.
    /// </summary>
    [DisplayName("PulseRgb1Payload")]
    [Description("Creates a message payload that specifies the duration of the output pulse in milliseconds.")]
    public partial class CreatePulseRgb1Payload
    {
        /// <summary>
        /// Gets or sets the value that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        [Range(min: 1, max: long.MaxValue)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that specifies the duration of the output pulse in milliseconds.")]
        public ushort PulseRgb1 { get; set; } = 1;

        /// <summary>
        /// Creates a message payload for the PulseRgb1 register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return PulseRgb1;
        }

        /// <summary>
        /// Creates a message that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the PulseRgb1 register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.Behavior.PulseRgb1.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that specifies the duration of the output pulse in milliseconds.
    /// </summary>
    [DisplayName("TimestampedPulseRgb1Payload")]
    [Description("Creates a timestamped message payload that specifies the duration of the output pulse in milliseconds.")]
    public partial class CreateTimestampedPulseRgb1Payload : CreatePulseRgb1Payload
    {
        /// <summary>
        /// Creates a timestamped message that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the PulseRgb1 register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.Behavior.PulseRgb1.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that specifies the duration of the output pulse in milliseconds.
    /// </summary>
    [DisplayName("PulseDO0Payload")]
    [Description("Creates a message payload that specifies the duration of the output pulse in milliseconds.")]
    public partial class CreatePulseDO0Payload
    {
        /// <summary>
        /// Gets or sets the value that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        [Range(min: 1, max: long.MaxValue)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that specifies the duration of the output pulse in milliseconds.")]
        public ushort PulseDO0 { get; set; } = 1;

        /// <summary>
        /// Creates a message payload for the PulseDO0 register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return PulseDO0;
        }

        /// <summary>
        /// Creates a message that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the PulseDO0 register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.Behavior.PulseDO0.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that specifies the duration of the output pulse in milliseconds.
    /// </summary>
    [DisplayName("TimestampedPulseDO0Payload")]
    [Description("Creates a timestamped message payload that specifies the duration of the output pulse in milliseconds.")]
    public partial class CreateTimestampedPulseDO0Payload : CreatePulseDO0Payload
    {
        /// <summary>
        /// Creates a timestamped message that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the PulseDO0 register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.Behavior.PulseDO0.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that specifies the duration of the output pulse in milliseconds.
    /// </summary>
    [DisplayName("PulseDO1Payload")]
    [Description("Creates a message payload that specifies the duration of the output pulse in milliseconds.")]
    public partial class CreatePulseDO1Payload
    {
        /// <summary>
        /// Gets or sets the value that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        [Range(min: 1, max: long.MaxValue)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that specifies the duration of the output pulse in milliseconds.")]
        public ushort PulseDO1 { get; set; } = 1;

        /// <summary>
        /// Creates a message payload for the PulseDO1 register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return PulseDO1;
        }

        /// <summary>
        /// Creates a message that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the PulseDO1 register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.Behavior.PulseDO1.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that specifies the duration of the output pulse in milliseconds.
    /// </summary>
    [DisplayName("TimestampedPulseDO1Payload")]
    [Description("Creates a timestamped message payload that specifies the duration of the output pulse in milliseconds.")]
    public partial class CreateTimestampedPulseDO1Payload : CreatePulseDO1Payload
    {
        /// <summary>
        /// Creates a timestamped message that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the PulseDO1 register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.Behavior.PulseDO1.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that specifies the duration of the output pulse in milliseconds.
    /// </summary>
    [DisplayName("PulseDO2Payload")]
    [Description("Creates a message payload that specifies the duration of the output pulse in milliseconds.")]
    public partial class CreatePulseDO2Payload
    {
        /// <summary>
        /// Gets or sets the value that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        [Range(min: 1, max: long.MaxValue)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that specifies the duration of the output pulse in milliseconds.")]
        public ushort PulseDO2 { get; set; } = 1;

        /// <summary>
        /// Creates a message payload for the PulseDO2 register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return PulseDO2;
        }

        /// <summary>
        /// Creates a message that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the PulseDO2 register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.Behavior.PulseDO2.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that specifies the duration of the output pulse in milliseconds.
    /// </summary>
    [DisplayName("TimestampedPulseDO2Payload")]
    [Description("Creates a timestamped message payload that specifies the duration of the output pulse in milliseconds.")]
    public partial class CreateTimestampedPulseDO2Payload : CreatePulseDO2Payload
    {
        /// <summary>
        /// Creates a timestamped message that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the PulseDO2 register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.Behavior.PulseDO2.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that specifies the duration of the output pulse in milliseconds.
    /// </summary>
    [DisplayName("PulseDO3Payload")]
    [Description("Creates a message payload that specifies the duration of the output pulse in milliseconds.")]
    public partial class CreatePulseDO3Payload
    {
        /// <summary>
        /// Gets or sets the value that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        [Range(min: 1, max: long.MaxValue)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that specifies the duration of the output pulse in milliseconds.")]
        public ushort PulseDO3 { get; set; } = 1;

        /// <summary>
        /// Creates a message payload for the PulseDO3 register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return PulseDO3;
        }

        /// <summary>
        /// Creates a message that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the PulseDO3 register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.Behavior.PulseDO3.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that specifies the duration of the output pulse in milliseconds.
    /// </summary>
    [DisplayName("TimestampedPulseDO3Payload")]
    [Description("Creates a timestamped message payload that specifies the duration of the output pulse in milliseconds.")]
    public partial class CreateTimestampedPulseDO3Payload : CreatePulseDO3Payload
    {
        /// <summary>
        /// Creates a timestamped message that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the PulseDO3 register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.Behavior.PulseDO3.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that specifies the frequency of the PWM at DO0.
    /// </summary>
    [DisplayName("PwmFrequencyDO0Payload")]
    [Description("Creates a message payload that specifies the frequency of the PWM at DO0.")]
    public partial class CreatePwmFrequencyDO0Payload
    {
        /// <summary>
        /// Gets or sets the value that specifies the frequency of the PWM at DO0.
        /// </summary>
        [Range(min: 1, max: long.MaxValue)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that specifies the frequency of the PWM at DO0.")]
        public ushort PwmFrequencyDO0 { get; set; } = 1;

        /// <summary>
        /// Creates a message payload for the PwmFrequencyDO0 register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return PwmFrequencyDO0;
        }

        /// <summary>
        /// Creates a message that specifies the frequency of the PWM at DO0.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the PwmFrequencyDO0 register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.Behavior.PwmFrequencyDO0.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that specifies the frequency of the PWM at DO0.
    /// </summary>
    [DisplayName("TimestampedPwmFrequencyDO0Payload")]
    [Description("Creates a timestamped message payload that specifies the frequency of the PWM at DO0.")]
    public partial class CreateTimestampedPwmFrequencyDO0Payload : CreatePwmFrequencyDO0Payload
    {
        /// <summary>
        /// Creates a timestamped message that specifies the frequency of the PWM at DO0.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the PwmFrequencyDO0 register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.Behavior.PwmFrequencyDO0.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that specifies the frequency of the PWM at DO1.
    /// </summary>
    [DisplayName("PwmFrequencyDO1Payload")]
    [Description("Creates a message payload that specifies the frequency of the PWM at DO1.")]
    public partial class CreatePwmFrequencyDO1Payload
    {
        /// <summary>
        /// Gets or sets the value that specifies the frequency of the PWM at DO1.
        /// </summary>
        [Range(min: 1, max: long.MaxValue)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that specifies the frequency of the PWM at DO1.")]
        public ushort PwmFrequencyDO1 { get; set; } = 1;

        /// <summary>
        /// Creates a message payload for the PwmFrequencyDO1 register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return PwmFrequencyDO1;
        }

        /// <summary>
        /// Creates a message that specifies the frequency of the PWM at DO1.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the PwmFrequencyDO1 register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.Behavior.PwmFrequencyDO1.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that specifies the frequency of the PWM at DO1.
    /// </summary>
    [DisplayName("TimestampedPwmFrequencyDO1Payload")]
    [Description("Creates a timestamped message payload that specifies the frequency of the PWM at DO1.")]
    public partial class CreateTimestampedPwmFrequencyDO1Payload : CreatePwmFrequencyDO1Payload
    {
        /// <summary>
        /// Creates a timestamped message that specifies the frequency of the PWM at DO1.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the PwmFrequencyDO1 register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.Behavior.PwmFrequencyDO1.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that specifies the frequency of the PWM at DO2.
    /// </summary>
    [DisplayName("PwmFrequencyDO2Payload")]
    [Description("Creates a message payload that specifies the frequency of the PWM at DO2.")]
    public partial class CreatePwmFrequencyDO2Payload
    {
        /// <summary>
        /// Gets or sets the value that specifies the frequency of the PWM at DO2.
        /// </summary>
        [Range(min: 1, max: long.MaxValue)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that specifies the frequency of the PWM at DO2.")]
        public ushort PwmFrequencyDO2 { get; set; } = 1;

        /// <summary>
        /// Creates a message payload for the PwmFrequencyDO2 register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return PwmFrequencyDO2;
        }

        /// <summary>
        /// Creates a message that specifies the frequency of the PWM at DO2.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the PwmFrequencyDO2 register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.Behavior.PwmFrequencyDO2.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that specifies the frequency of the PWM at DO2.
    /// </summary>
    [DisplayName("TimestampedPwmFrequencyDO2Payload")]
    [Description("Creates a timestamped message payload that specifies the frequency of the PWM at DO2.")]
    public partial class CreateTimestampedPwmFrequencyDO2Payload : CreatePwmFrequencyDO2Payload
    {
        /// <summary>
        /// Creates a timestamped message that specifies the frequency of the PWM at DO2.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the PwmFrequencyDO2 register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.Behavior.PwmFrequencyDO2.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that specifies the frequency of the PWM at DO3.
    /// </summary>
    [DisplayName("PwmFrequencyDO3Payload")]
    [Description("Creates a message payload that specifies the frequency of the PWM at DO3.")]
    public partial class CreatePwmFrequencyDO3Payload
    {
        /// <summary>
        /// Gets or sets the value that specifies the frequency of the PWM at DO3.
        /// </summary>
        [Range(min: 1, max: long.MaxValue)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that specifies the frequency of the PWM at DO3.")]
        public ushort PwmFrequencyDO3 { get; set; } = 1;

        /// <summary>
        /// Creates a message payload for the PwmFrequencyDO3 register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return PwmFrequencyDO3;
        }

        /// <summary>
        /// Creates a message that specifies the frequency of the PWM at DO3.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the PwmFrequencyDO3 register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.Behavior.PwmFrequencyDO3.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that specifies the frequency of the PWM at DO3.
    /// </summary>
    [DisplayName("TimestampedPwmFrequencyDO3Payload")]
    [Description("Creates a timestamped message payload that specifies the frequency of the PWM at DO3.")]
    public partial class CreateTimestampedPwmFrequencyDO3Payload : CreatePwmFrequencyDO3Payload
    {
        /// <summary>
        /// Creates a timestamped message that specifies the frequency of the PWM at DO3.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the PwmFrequencyDO3 register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.Behavior.PwmFrequencyDO3.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that specifies the duty cycle of the PWM at DO0.
    /// </summary>
    [DisplayName("PwmDutyCycleDO0Payload")]
    [Description("Creates a message payload that specifies the duty cycle of the PWM at DO0.")]
    public partial class CreatePwmDutyCycleDO0Payload
    {
        /// <summary>
        /// Gets or sets the value that specifies the duty cycle of the PWM at DO0.
        /// </summary>
        [Range(min: 1, max: 99)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that specifies the duty cycle of the PWM at DO0.")]
        public byte PwmDutyCycleDO0 { get; set; } = 1;

        /// <summary>
        /// Creates a message payload for the PwmDutyCycleDO0 register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public byte GetPayload()
        {
            return PwmDutyCycleDO0;
        }

        /// <summary>
        /// Creates a message that specifies the duty cycle of the PWM at DO0.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the PwmDutyCycleDO0 register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.Behavior.PwmDutyCycleDO0.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that specifies the duty cycle of the PWM at DO0.
    /// </summary>
    [DisplayName("TimestampedPwmDutyCycleDO0Payload")]
    [Description("Creates a timestamped message payload that specifies the duty cycle of the PWM at DO0.")]
    public partial class CreateTimestampedPwmDutyCycleDO0Payload : CreatePwmDutyCycleDO0Payload
    {
        /// <summary>
        /// Creates a timestamped message that specifies the duty cycle of the PWM at DO0.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the PwmDutyCycleDO0 register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.Behavior.PwmDutyCycleDO0.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that specifies the duty cycle of the PWM at DO1.
    /// </summary>
    [DisplayName("PwmDutyCycleDO1Payload")]
    [Description("Creates a message payload that specifies the duty cycle of the PWM at DO1.")]
    public partial class CreatePwmDutyCycleDO1Payload
    {
        /// <summary>
        /// Gets or sets the value that specifies the duty cycle of the PWM at DO1.
        /// </summary>
        [Range(min: 1, max: 99)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that specifies the duty cycle of the PWM at DO1.")]
        public byte PwmDutyCycleDO1 { get; set; } = 1;

        /// <summary>
        /// Creates a message payload for the PwmDutyCycleDO1 register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public byte GetPayload()
        {
            return PwmDutyCycleDO1;
        }

        /// <summary>
        /// Creates a message that specifies the duty cycle of the PWM at DO1.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the PwmDutyCycleDO1 register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.Behavior.PwmDutyCycleDO1.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that specifies the duty cycle of the PWM at DO1.
    /// </summary>
    [DisplayName("TimestampedPwmDutyCycleDO1Payload")]
    [Description("Creates a timestamped message payload that specifies the duty cycle of the PWM at DO1.")]
    public partial class CreateTimestampedPwmDutyCycleDO1Payload : CreatePwmDutyCycleDO1Payload
    {
        /// <summary>
        /// Creates a timestamped message that specifies the duty cycle of the PWM at DO1.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the PwmDutyCycleDO1 register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.Behavior.PwmDutyCycleDO1.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that specifies the duty cycle of the PWM at DO2.
    /// </summary>
    [DisplayName("PwmDutyCycleDO2Payload")]
    [Description("Creates a message payload that specifies the duty cycle of the PWM at DO2.")]
    public partial class CreatePwmDutyCycleDO2Payload
    {
        /// <summary>
        /// Gets or sets the value that specifies the duty cycle of the PWM at DO2.
        /// </summary>
        [Range(min: 1, max: 99)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that specifies the duty cycle of the PWM at DO2.")]
        public byte PwmDutyCycleDO2 { get; set; } = 1;

        /// <summary>
        /// Creates a message payload for the PwmDutyCycleDO2 register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public byte GetPayload()
        {
            return PwmDutyCycleDO2;
        }

        /// <summary>
        /// Creates a message that specifies the duty cycle of the PWM at DO2.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the PwmDutyCycleDO2 register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.Behavior.PwmDutyCycleDO2.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that specifies the duty cycle of the PWM at DO2.
    /// </summary>
    [DisplayName("TimestampedPwmDutyCycleDO2Payload")]
    [Description("Creates a timestamped message payload that specifies the duty cycle of the PWM at DO2.")]
    public partial class CreateTimestampedPwmDutyCycleDO2Payload : CreatePwmDutyCycleDO2Payload
    {
        /// <summary>
        /// Creates a timestamped message that specifies the duty cycle of the PWM at DO2.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the PwmDutyCycleDO2 register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.Behavior.PwmDutyCycleDO2.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that specifies the duty cycle of the PWM at DO3.
    /// </summary>
    [DisplayName("PwmDutyCycleDO3Payload")]
    [Description("Creates a message payload that specifies the duty cycle of the PWM at DO3.")]
    public partial class CreatePwmDutyCycleDO3Payload
    {
        /// <summary>
        /// Gets or sets the value that specifies the duty cycle of the PWM at DO3.
        /// </summary>
        [Range(min: 1, max: 99)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that specifies the duty cycle of the PWM at DO3.")]
        public byte PwmDutyCycleDO3 { get; set; } = 1;

        /// <summary>
        /// Creates a message payload for the PwmDutyCycleDO3 register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public byte GetPayload()
        {
            return PwmDutyCycleDO3;
        }

        /// <summary>
        /// Creates a message that specifies the duty cycle of the PWM at DO3.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the PwmDutyCycleDO3 register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.Behavior.PwmDutyCycleDO3.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that specifies the duty cycle of the PWM at DO3.
    /// </summary>
    [DisplayName("TimestampedPwmDutyCycleDO3Payload")]
    [Description("Creates a timestamped message payload that specifies the duty cycle of the PWM at DO3.")]
    public partial class CreateTimestampedPwmDutyCycleDO3Payload : CreatePwmDutyCycleDO3Payload
    {
        /// <summary>
        /// Creates a timestamped message that specifies the duty cycle of the PWM at DO3.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the PwmDutyCycleDO3 register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.Behavior.PwmDutyCycleDO3.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that starts the PWM on the selected output lines.
    /// </summary>
    [DisplayName("PwmStartPayload")]
    [Description("Creates a message payload that starts the PWM on the selected output lines.")]
    public partial class CreatePwmStartPayload
    {
        /// <summary>
        /// Gets or sets the value that starts the PWM on the selected output lines.
        /// </summary>
        [Description("The value that starts the PWM on the selected output lines.")]
        public PwmOutputs PwmStart { get; set; }

        /// <summary>
        /// Creates a message payload for the PwmStart register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public PwmOutputs GetPayload()
        {
            return PwmStart;
        }

        /// <summary>
        /// Creates a message that starts the PWM on the selected output lines.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the PwmStart register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.Behavior.PwmStart.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that starts the PWM on the selected output lines.
    /// </summary>
    [DisplayName("TimestampedPwmStartPayload")]
    [Description("Creates a timestamped message payload that starts the PWM on the selected output lines.")]
    public partial class CreateTimestampedPwmStartPayload : CreatePwmStartPayload
    {
        /// <summary>
        /// Creates a timestamped message that starts the PWM on the selected output lines.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the PwmStart register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.Behavior.PwmStart.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that stops the PWM on the selected output lines.
    /// </summary>
    [DisplayName("PwmStopPayload")]
    [Description("Creates a message payload that stops the PWM on the selected output lines.")]
    public partial class CreatePwmStopPayload
    {
        /// <summary>
        /// Gets or sets the value that stops the PWM on the selected output lines.
        /// </summary>
        [Description("The value that stops the PWM on the selected output lines.")]
        public PwmOutputs PwmStop { get; set; }

        /// <summary>
        /// Creates a message payload for the PwmStop register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public PwmOutputs GetPayload()
        {
            return PwmStop;
        }

        /// <summary>
        /// Creates a message that stops the PWM on the selected output lines.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the PwmStop register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.Behavior.PwmStop.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that stops the PWM on the selected output lines.
    /// </summary>
    [DisplayName("TimestampedPwmStopPayload")]
    [Description("Creates a timestamped message payload that stops the PWM on the selected output lines.")]
    public partial class CreateTimestampedPwmStopPayload : CreatePwmStopPayload
    {
        /// <summary>
        /// Creates a timestamped message that stops the PWM on the selected output lines.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the PwmStop register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.Behavior.PwmStop.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that specifies the state of all RGB LED channels.
    /// </summary>
    [DisplayName("RgbAllPayload")]
    [Description("Creates a message payload that specifies the state of all RGB LED channels.")]
    public partial class CreateRgbAllPayload
    {
        /// <summary>
        /// Gets or sets a value that the intensity of the green channel in the RGB0 LED.
        /// </summary>
        [Description("The intensity of the green channel in the RGB0 LED.")]
        public byte Green0 { get; set; }

        /// <summary>
        /// Gets or sets a value that the intensity of the red channel in the RGB0 LED.
        /// </summary>
        [Description("The intensity of the red channel in the RGB0 LED.")]
        public byte Red0 { get; set; }

        /// <summary>
        /// Gets or sets a value that the intensity of the blue channel in the RGB0 LED.
        /// </summary>
        [Description("The intensity of the blue channel in the RGB0 LED.")]
        public byte Blue0 { get; set; }

        /// <summary>
        /// Gets or sets a value that the intensity of the green channel in the RGB1 LED.
        /// </summary>
        [Description("The intensity of the green channel in the RGB1 LED.")]
        public byte Green1 { get; set; }

        /// <summary>
        /// Gets or sets a value that the intensity of the red channel in the RGB1 LED.
        /// </summary>
        [Description("The intensity of the red channel in the RGB1 LED.")]
        public byte Red1 { get; set; }

        /// <summary>
        /// Gets or sets a value that the intensity of the blue channel in the RGB1 LED.
        /// </summary>
        [Description("The intensity of the blue channel in the RGB1 LED.")]
        public byte Blue1 { get; set; }

        /// <summary>
        /// Creates a message payload for the RgbAll register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public RgbAllPayload GetPayload()
        {
            RgbAllPayload value;
            value.Green0 = Green0;
            value.Red0 = Red0;
            value.Blue0 = Blue0;
            value.Green1 = Green1;
            value.Red1 = Red1;
            value.Blue1 = Blue1;
            return value;
        }

        /// <summary>
        /// Creates a message that specifies the state of all RGB LED channels.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the RgbAll register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.Behavior.RgbAll.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that specifies the state of all RGB LED channels.
    /// </summary>
    [DisplayName("TimestampedRgbAllPayload")]
    [Description("Creates a timestamped message payload that specifies the state of all RGB LED channels.")]
    public partial class CreateTimestampedRgbAllPayload : CreateRgbAllPayload
    {
        /// <summary>
        /// Creates a timestamped message that specifies the state of all RGB LED channels.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the RgbAll register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.Behavior.RgbAll.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that specifies the state of the RGB0 LED channels.
    /// </summary>
    [DisplayName("Rgb0Payload")]
    [Description("Creates a message payload that specifies the state of the RGB0 LED channels.")]
    public partial class CreateRgb0Payload
    {
        /// <summary>
        /// Gets or sets a value that the intensity of the green channel in the RGB LED.
        /// </summary>
        [Description("The intensity of the green channel in the RGB LED.")]
        public byte Green { get; set; }

        /// <summary>
        /// Gets or sets a value that the intensity of the red channel in the RGB LED.
        /// </summary>
        [Description("The intensity of the red channel in the RGB LED.")]
        public byte Red { get; set; }

        /// <summary>
        /// Gets or sets a value that the intensity of the blue channel in the RGB LED.
        /// </summary>
        [Description("The intensity of the blue channel in the RGB LED.")]
        public byte Blue { get; set; }

        /// <summary>
        /// Creates a message payload for the Rgb0 register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public RgbPayload GetPayload()
        {
            RgbPayload value;
            value.Green = Green;
            value.Red = Red;
            value.Blue = Blue;
            return value;
        }

        /// <summary>
        /// Creates a message that specifies the state of the RGB0 LED channels.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Rgb0 register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.Behavior.Rgb0.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that specifies the state of the RGB0 LED channels.
    /// </summary>
    [DisplayName("TimestampedRgb0Payload")]
    [Description("Creates a timestamped message payload that specifies the state of the RGB0 LED channels.")]
    public partial class CreateTimestampedRgb0Payload : CreateRgb0Payload
    {
        /// <summary>
        /// Creates a timestamped message that specifies the state of the RGB0 LED channels.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Rgb0 register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.Behavior.Rgb0.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that specifies the state of the RGB1 LED channels.
    /// </summary>
    [DisplayName("Rgb1Payload")]
    [Description("Creates a message payload that specifies the state of the RGB1 LED channels.")]
    public partial class CreateRgb1Payload
    {
        /// <summary>
        /// Gets or sets a value that the intensity of the green channel in the RGB LED.
        /// </summary>
        [Description("The intensity of the green channel in the RGB LED.")]
        public byte Green { get; set; }

        /// <summary>
        /// Gets or sets a value that the intensity of the red channel in the RGB LED.
        /// </summary>
        [Description("The intensity of the red channel in the RGB LED.")]
        public byte Red { get; set; }

        /// <summary>
        /// Gets or sets a value that the intensity of the blue channel in the RGB LED.
        /// </summary>
        [Description("The intensity of the blue channel in the RGB LED.")]
        public byte Blue { get; set; }

        /// <summary>
        /// Creates a message payload for the Rgb1 register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public RgbPayload GetPayload()
        {
            RgbPayload value;
            value.Green = Green;
            value.Red = Red;
            value.Blue = Blue;
            return value;
        }

        /// <summary>
        /// Creates a message that specifies the state of the RGB1 LED channels.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Rgb1 register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.Behavior.Rgb1.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that specifies the state of the RGB1 LED channels.
    /// </summary>
    [DisplayName("TimestampedRgb1Payload")]
    [Description("Creates a timestamped message payload that specifies the state of the RGB1 LED channels.")]
    public partial class CreateTimestampedRgb1Payload : CreateRgb1Payload
    {
        /// <summary>
        /// Creates a timestamped message that specifies the state of the RGB1 LED channels.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Rgb1 register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.Behavior.Rgb1.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that specifies the configuration of current to drive LED 0.
    /// </summary>
    [DisplayName("Led0CurrentPayload")]
    [Description("Creates a message payload that specifies the configuration of current to drive LED 0.")]
    public partial class CreateLed0CurrentPayload
    {
        /// <summary>
        /// Gets or sets the value that specifies the configuration of current to drive LED 0.
        /// </summary>
        [Range(min: 2, max: 100)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that specifies the configuration of current to drive LED 0.")]
        public byte Led0Current { get; set; } = 2;

        /// <summary>
        /// Creates a message payload for the Led0Current register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public byte GetPayload()
        {
            return Led0Current;
        }

        /// <summary>
        /// Creates a message that specifies the configuration of current to drive LED 0.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Led0Current register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.Behavior.Led0Current.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that specifies the configuration of current to drive LED 0.
    /// </summary>
    [DisplayName("TimestampedLed0CurrentPayload")]
    [Description("Creates a timestamped message payload that specifies the configuration of current to drive LED 0.")]
    public partial class CreateTimestampedLed0CurrentPayload : CreateLed0CurrentPayload
    {
        /// <summary>
        /// Creates a timestamped message that specifies the configuration of current to drive LED 0.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Led0Current register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.Behavior.Led0Current.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that specifies the configuration of current to drive LED 1.
    /// </summary>
    [DisplayName("Led1CurrentPayload")]
    [Description("Creates a message payload that specifies the configuration of current to drive LED 1.")]
    public partial class CreateLed1CurrentPayload
    {
        /// <summary>
        /// Gets or sets the value that specifies the configuration of current to drive LED 1.
        /// </summary>
        [Range(min: 2, max: 100)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that specifies the configuration of current to drive LED 1.")]
        public byte Led1Current { get; set; } = 2;

        /// <summary>
        /// Creates a message payload for the Led1Current register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public byte GetPayload()
        {
            return Led1Current;
        }

        /// <summary>
        /// Creates a message that specifies the configuration of current to drive LED 1.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Led1Current register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.Behavior.Led1Current.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that specifies the configuration of current to drive LED 1.
    /// </summary>
    [DisplayName("TimestampedLed1CurrentPayload")]
    [Description("Creates a timestamped message payload that specifies the configuration of current to drive LED 1.")]
    public partial class CreateTimestampedLed1CurrentPayload : CreateLed1CurrentPayload
    {
        /// <summary>
        /// Creates a timestamped message that specifies the configuration of current to drive LED 1.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Led1Current register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.Behavior.Led1Current.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that specifies the configuration of current to drive LED 0.
    /// </summary>
    [DisplayName("Led0MaxCurrentPayload")]
    [Description("Creates a message payload that specifies the configuration of current to drive LED 0.")]
    public partial class CreateLed0MaxCurrentPayload
    {
        /// <summary>
        /// Gets or sets the value that specifies the configuration of current to drive LED 0.
        /// </summary>
        [Range(min: 5, max: 100)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that specifies the configuration of current to drive LED 0.")]
        public byte Led0MaxCurrent { get; set; } = 5;

        /// <summary>
        /// Creates a message payload for the Led0MaxCurrent register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public byte GetPayload()
        {
            return Led0MaxCurrent;
        }

        /// <summary>
        /// Creates a message that specifies the configuration of current to drive LED 0.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Led0MaxCurrent register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.Behavior.Led0MaxCurrent.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that specifies the configuration of current to drive LED 0.
    /// </summary>
    [DisplayName("TimestampedLed0MaxCurrentPayload")]
    [Description("Creates a timestamped message payload that specifies the configuration of current to drive LED 0.")]
    public partial class CreateTimestampedLed0MaxCurrentPayload : CreateLed0MaxCurrentPayload
    {
        /// <summary>
        /// Creates a timestamped message that specifies the configuration of current to drive LED 0.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Led0MaxCurrent register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.Behavior.Led0MaxCurrent.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that specifies the configuration of current to drive LED 1.
    /// </summary>
    [DisplayName("Led1MaxCurrentPayload")]
    [Description("Creates a message payload that specifies the configuration of current to drive LED 1.")]
    public partial class CreateLed1MaxCurrentPayload
    {
        /// <summary>
        /// Gets or sets the value that specifies the configuration of current to drive LED 1.
        /// </summary>
        [Range(min: 5, max: 100)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that specifies the configuration of current to drive LED 1.")]
        public byte Led1MaxCurrent { get; set; } = 5;

        /// <summary>
        /// Creates a message payload for the Led1MaxCurrent register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public byte GetPayload()
        {
            return Led1MaxCurrent;
        }

        /// <summary>
        /// Creates a message that specifies the configuration of current to drive LED 1.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Led1MaxCurrent register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.Behavior.Led1MaxCurrent.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that specifies the configuration of current to drive LED 1.
    /// </summary>
    [DisplayName("TimestampedLed1MaxCurrentPayload")]
    [Description("Creates a timestamped message payload that specifies the configuration of current to drive LED 1.")]
    public partial class CreateTimestampedLed1MaxCurrentPayload : CreateLed1MaxCurrentPayload
    {
        /// <summary>
        /// Creates a timestamped message that specifies the configuration of current to drive LED 1.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Led1MaxCurrent register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.Behavior.Led1MaxCurrent.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that specifies the active events in the device.
    /// </summary>
    [DisplayName("EventEnablePayload")]
    [Description("Creates a message payload that specifies the active events in the device.")]
    public partial class CreateEventEnablePayload
    {
        /// <summary>
        /// Gets or sets the value that specifies the active events in the device.
        /// </summary>
        [Description("The value that specifies the active events in the device.")]
        public Events EventEnable { get; set; }

        /// <summary>
        /// Creates a message payload for the EventEnable register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public Events GetPayload()
        {
            return EventEnable;
        }

        /// <summary>
        /// Creates a message that specifies the active events in the device.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the EventEnable register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.Behavior.EventEnable.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that specifies the active events in the device.
    /// </summary>
    [DisplayName("TimestampedEventEnablePayload")]
    [Description("Creates a timestamped message payload that specifies the active events in the device.")]
    public partial class CreateTimestampedEventEnablePayload : CreateEventEnablePayload
    {
        /// <summary>
        /// Creates a timestamped message that specifies the active events in the device.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the EventEnable register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.Behavior.EventEnable.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that specifies the camera outputs to enable in the device.
    /// </summary>
    [DisplayName("StartCamerasPayload")]
    [Description("Creates a message payload that specifies the camera outputs to enable in the device.")]
    public partial class CreateStartCamerasPayload
    {
        /// <summary>
        /// Gets or sets the value that specifies the camera outputs to enable in the device.
        /// </summary>
        [Description("The value that specifies the camera outputs to enable in the device.")]
        public CameraOutputs StartCameras { get; set; }

        /// <summary>
        /// Creates a message payload for the StartCameras register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public CameraOutputs GetPayload()
        {
            return StartCameras;
        }

        /// <summary>
        /// Creates a message that specifies the camera outputs to enable in the device.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the StartCameras register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.Behavior.StartCameras.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that specifies the camera outputs to enable in the device.
    /// </summary>
    [DisplayName("TimestampedStartCamerasPayload")]
    [Description("Creates a timestamped message payload that specifies the camera outputs to enable in the device.")]
    public partial class CreateTimestampedStartCamerasPayload : CreateStartCamerasPayload
    {
        /// <summary>
        /// Creates a timestamped message that specifies the camera outputs to enable in the device.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the StartCameras register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.Behavior.StartCameras.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that specifies the camera outputs to disable in the device.
    /// </summary>
    [DisplayName("StopCamerasPayload")]
    [Description("Creates a message payload that specifies the camera outputs to disable in the device.")]
    public partial class CreateStopCamerasPayload
    {
        /// <summary>
        /// Gets or sets the value that specifies the camera outputs to disable in the device.
        /// </summary>
        [Description("The value that specifies the camera outputs to disable in the device.")]
        public CameraOutputs StopCameras { get; set; }

        /// <summary>
        /// Creates a message payload for the StopCameras register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public CameraOutputs GetPayload()
        {
            return StopCameras;
        }

        /// <summary>
        /// Creates a message that specifies the camera outputs to disable in the device.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the StopCameras register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.Behavior.StopCameras.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that specifies the camera outputs to disable in the device.
    /// </summary>
    [DisplayName("TimestampedStopCamerasPayload")]
    [Description("Creates a timestamped message payload that specifies the camera outputs to disable in the device.")]
    public partial class CreateTimestampedStopCamerasPayload : CreateStopCamerasPayload
    {
        /// <summary>
        /// Creates a timestamped message that specifies the camera outputs to disable in the device.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the StopCameras register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.Behavior.StopCameras.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that specifies the servo outputs to enable in the device.
    /// </summary>
    [DisplayName("EnableServosPayload")]
    [Description("Creates a message payload that specifies the servo outputs to enable in the device.")]
    public partial class CreateEnableServosPayload
    {
        /// <summary>
        /// Gets or sets the value that specifies the servo outputs to enable in the device.
        /// </summary>
        [Description("The value that specifies the servo outputs to enable in the device.")]
        public ServoOutputs EnableServos { get; set; }

        /// <summary>
        /// Creates a message payload for the EnableServos register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ServoOutputs GetPayload()
        {
            return EnableServos;
        }

        /// <summary>
        /// Creates a message that specifies the servo outputs to enable in the device.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the EnableServos register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.Behavior.EnableServos.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that specifies the servo outputs to enable in the device.
    /// </summary>
    [DisplayName("TimestampedEnableServosPayload")]
    [Description("Creates a timestamped message payload that specifies the servo outputs to enable in the device.")]
    public partial class CreateTimestampedEnableServosPayload : CreateEnableServosPayload
    {
        /// <summary>
        /// Creates a timestamped message that specifies the servo outputs to enable in the device.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the EnableServos register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.Behavior.EnableServos.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that specifies the servo outputs to disable in the device.
    /// </summary>
    [DisplayName("DisableServosPayload")]
    [Description("Creates a message payload that specifies the servo outputs to disable in the device.")]
    public partial class CreateDisableServosPayload
    {
        /// <summary>
        /// Gets or sets the value that specifies the servo outputs to disable in the device.
        /// </summary>
        [Description("The value that specifies the servo outputs to disable in the device.")]
        public ServoOutputs DisableServos { get; set; }

        /// <summary>
        /// Creates a message payload for the DisableServos register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ServoOutputs GetPayload()
        {
            return DisableServos;
        }

        /// <summary>
        /// Creates a message that specifies the servo outputs to disable in the device.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DisableServos register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.Behavior.DisableServos.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that specifies the servo outputs to disable in the device.
    /// </summary>
    [DisplayName("TimestampedDisableServosPayload")]
    [Description("Creates a timestamped message payload that specifies the servo outputs to disable in the device.")]
    public partial class CreateTimestampedDisableServosPayload : CreateDisableServosPayload
    {
        /// <summary>
        /// Creates a timestamped message that specifies the servo outputs to disable in the device.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DisableServos register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.Behavior.DisableServos.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that specifies the port quadrature counters to enable in the device.
    /// </summary>
    [DisplayName("EnableEncodersPayload")]
    [Description("Creates a message payload that specifies the port quadrature counters to enable in the device.")]
    public partial class CreateEnableEncodersPayload
    {
        /// <summary>
        /// Gets or sets the value that specifies the port quadrature counters to enable in the device.
        /// </summary>
        [Description("The value that specifies the port quadrature counters to enable in the device.")]
        public EncoderInputs EnableEncoders { get; set; }

        /// <summary>
        /// Creates a message payload for the EnableEncoders register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public EncoderInputs GetPayload()
        {
            return EnableEncoders;
        }

        /// <summary>
        /// Creates a message that specifies the port quadrature counters to enable in the device.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the EnableEncoders register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.Behavior.EnableEncoders.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that specifies the port quadrature counters to enable in the device.
    /// </summary>
    [DisplayName("TimestampedEnableEncodersPayload")]
    [Description("Creates a timestamped message payload that specifies the port quadrature counters to enable in the device.")]
    public partial class CreateTimestampedEnableEncodersPayload : CreateEnableEncodersPayload
    {
        /// <summary>
        /// Creates a timestamped message that specifies the port quadrature counters to enable in the device.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the EnableEncoders register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.Behavior.EnableEncoders.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that configures the operation mode of the quadrature encoders.
    /// </summary>
    [DisplayName("EncoderModePayload")]
    [Description("Creates a message payload that configures the operation mode of the quadrature encoders.")]
    public partial class CreateEncoderModePayload
    {
        /// <summary>
        /// Gets or sets the value that configures the operation mode of the quadrature encoders.
        /// </summary>
        [Description("The value that configures the operation mode of the quadrature encoders.")]
        public EncoderModeConfig EncoderMode { get; set; }

        /// <summary>
        /// Creates a message payload for the EncoderMode register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public EncoderModeConfig GetPayload()
        {
            return EncoderMode;
        }

        /// <summary>
        /// Creates a message that configures the operation mode of the quadrature encoders.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the EncoderMode register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.Behavior.EncoderMode.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that configures the operation mode of the quadrature encoders.
    /// </summary>
    [DisplayName("TimestampedEncoderModePayload")]
    [Description("Creates a timestamped message payload that configures the operation mode of the quadrature encoders.")]
    public partial class CreateTimestampedEncoderModePayload : CreateEncoderModePayload
    {
        /// <summary>
        /// Creates a timestamped message that configures the operation mode of the quadrature encoders.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the EncoderMode register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.Behavior.EncoderMode.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that specifies that a frame was acquired on camera 0.
    /// </summary>
    [DisplayName("Camera0FramePayload")]
    [Description("Creates a message payload that specifies that a frame was acquired on camera 0.")]
    public partial class CreateCamera0FramePayload
    {
        /// <summary>
        /// Gets or sets the value that specifies that a frame was acquired on camera 0.
        /// </summary>
        [Description("The value that specifies that a frame was acquired on camera 0.")]
        public FrameAcquired Camera0Frame { get; set; }

        /// <summary>
        /// Creates a message payload for the Camera0Frame register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public FrameAcquired GetPayload()
        {
            return Camera0Frame;
        }

        /// <summary>
        /// Creates a message that specifies that a frame was acquired on camera 0.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Camera0Frame register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.Behavior.Camera0Frame.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that specifies that a frame was acquired on camera 0.
    /// </summary>
    [DisplayName("TimestampedCamera0FramePayload")]
    [Description("Creates a timestamped message payload that specifies that a frame was acquired on camera 0.")]
    public partial class CreateTimestampedCamera0FramePayload : CreateCamera0FramePayload
    {
        /// <summary>
        /// Creates a timestamped message that specifies that a frame was acquired on camera 0.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Camera0Frame register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.Behavior.Camera0Frame.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that specifies the trigger frequency for camera 0.
    /// </summary>
    [DisplayName("Camera0FrequencyPayload")]
    [Description("Creates a message payload that specifies the trigger frequency for camera 0.")]
    public partial class CreateCamera0FrequencyPayload
    {
        /// <summary>
        /// Gets or sets the value that specifies the trigger frequency for camera 0.
        /// </summary>
        [Range(min: 1, max: 600)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that specifies the trigger frequency for camera 0.")]
        public ushort Camera0Frequency { get; set; } = 1;

        /// <summary>
        /// Creates a message payload for the Camera0Frequency register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return Camera0Frequency;
        }

        /// <summary>
        /// Creates a message that specifies the trigger frequency for camera 0.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Camera0Frequency register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.Behavior.Camera0Frequency.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that specifies the trigger frequency for camera 0.
    /// </summary>
    [DisplayName("TimestampedCamera0FrequencyPayload")]
    [Description("Creates a timestamped message payload that specifies the trigger frequency for camera 0.")]
    public partial class CreateTimestampedCamera0FrequencyPayload : CreateCamera0FrequencyPayload
    {
        /// <summary>
        /// Creates a timestamped message that specifies the trigger frequency for camera 0.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Camera0Frequency register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.Behavior.Camera0Frequency.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that specifies that a frame was acquired on camera 1.
    /// </summary>
    [DisplayName("Camera1FramePayload")]
    [Description("Creates a message payload that specifies that a frame was acquired on camera 1.")]
    public partial class CreateCamera1FramePayload
    {
        /// <summary>
        /// Gets or sets the value that specifies that a frame was acquired on camera 1.
        /// </summary>
        [Description("The value that specifies that a frame was acquired on camera 1.")]
        public FrameAcquired Camera1Frame { get; set; }

        /// <summary>
        /// Creates a message payload for the Camera1Frame register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public FrameAcquired GetPayload()
        {
            return Camera1Frame;
        }

        /// <summary>
        /// Creates a message that specifies that a frame was acquired on camera 1.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Camera1Frame register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.Behavior.Camera1Frame.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that specifies that a frame was acquired on camera 1.
    /// </summary>
    [DisplayName("TimestampedCamera1FramePayload")]
    [Description("Creates a timestamped message payload that specifies that a frame was acquired on camera 1.")]
    public partial class CreateTimestampedCamera1FramePayload : CreateCamera1FramePayload
    {
        /// <summary>
        /// Creates a timestamped message that specifies that a frame was acquired on camera 1.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Camera1Frame register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.Behavior.Camera1Frame.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that specifies the trigger frequency for camera 1.
    /// </summary>
    [DisplayName("Camera1FrequencyPayload")]
    [Description("Creates a message payload that specifies the trigger frequency for camera 1.")]
    public partial class CreateCamera1FrequencyPayload
    {
        /// <summary>
        /// Gets or sets the value that specifies the trigger frequency for camera 1.
        /// </summary>
        [Range(min: 1, max: 600)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that specifies the trigger frequency for camera 1.")]
        public ushort Camera1Frequency { get; set; } = 1;

        /// <summary>
        /// Creates a message payload for the Camera1Frequency register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return Camera1Frequency;
        }

        /// <summary>
        /// Creates a message that specifies the trigger frequency for camera 1.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the Camera1Frequency register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.Behavior.Camera1Frequency.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that specifies the trigger frequency for camera 1.
    /// </summary>
    [DisplayName("TimestampedCamera1FrequencyPayload")]
    [Description("Creates a timestamped message payload that specifies the trigger frequency for camera 1.")]
    public partial class CreateTimestampedCamera1FrequencyPayload : CreateCamera1FrequencyPayload
    {
        /// <summary>
        /// Creates a timestamped message that specifies the trigger frequency for camera 1.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the Camera1Frequency register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.Behavior.Camera1Frequency.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that specifies the period of the servo motor in DO2, in microseconds.
    /// </summary>
    [DisplayName("ServoMotor2PeriodPayload")]
    [Description("Creates a message payload that specifies the period of the servo motor in DO2, in microseconds.")]
    public partial class CreateServoMotor2PeriodPayload
    {
        /// <summary>
        /// Gets or sets the value that specifies the period of the servo motor in DO2, in microseconds.
        /// </summary>
        [Range(min: 2, max: 65534)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that specifies the period of the servo motor in DO2, in microseconds.")]
        public ushort ServoMotor2Period { get; set; } = 2;

        /// <summary>
        /// Creates a message payload for the ServoMotor2Period register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return ServoMotor2Period;
        }

        /// <summary>
        /// Creates a message that specifies the period of the servo motor in DO2, in microseconds.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the ServoMotor2Period register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.Behavior.ServoMotor2Period.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that specifies the period of the servo motor in DO2, in microseconds.
    /// </summary>
    [DisplayName("TimestampedServoMotor2PeriodPayload")]
    [Description("Creates a timestamped message payload that specifies the period of the servo motor in DO2, in microseconds.")]
    public partial class CreateTimestampedServoMotor2PeriodPayload : CreateServoMotor2PeriodPayload
    {
        /// <summary>
        /// Creates a timestamped message that specifies the period of the servo motor in DO2, in microseconds.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the ServoMotor2Period register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.Behavior.ServoMotor2Period.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that specifies the pulse of the servo motor in DO2, in microseconds.
    /// </summary>
    [DisplayName("ServoMotor2PulsePayload")]
    [Description("Creates a message payload that specifies the pulse of the servo motor in DO2, in microseconds.")]
    public partial class CreateServoMotor2PulsePayload
    {
        /// <summary>
        /// Gets or sets the value that specifies the pulse of the servo motor in DO2, in microseconds.
        /// </summary>
        [Range(min: 6, max: 65530)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that specifies the pulse of the servo motor in DO2, in microseconds.")]
        public ushort ServoMotor2Pulse { get; set; } = 6;

        /// <summary>
        /// Creates a message payload for the ServoMotor2Pulse register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return ServoMotor2Pulse;
        }

        /// <summary>
        /// Creates a message that specifies the pulse of the servo motor in DO2, in microseconds.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the ServoMotor2Pulse register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.Behavior.ServoMotor2Pulse.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that specifies the pulse of the servo motor in DO2, in microseconds.
    /// </summary>
    [DisplayName("TimestampedServoMotor2PulsePayload")]
    [Description("Creates a timestamped message payload that specifies the pulse of the servo motor in DO2, in microseconds.")]
    public partial class CreateTimestampedServoMotor2PulsePayload : CreateServoMotor2PulsePayload
    {
        /// <summary>
        /// Creates a timestamped message that specifies the pulse of the servo motor in DO2, in microseconds.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the ServoMotor2Pulse register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.Behavior.ServoMotor2Pulse.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that specifies the period of the servo motor in DO3, in microseconds.
    /// </summary>
    [DisplayName("ServoMotor3PeriodPayload")]
    [Description("Creates a message payload that specifies the period of the servo motor in DO3, in microseconds.")]
    public partial class CreateServoMotor3PeriodPayload
    {
        /// <summary>
        /// Gets or sets the value that specifies the period of the servo motor in DO3, in microseconds.
        /// </summary>
        [Range(min: 2, max: 65534)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that specifies the period of the servo motor in DO3, in microseconds.")]
        public ushort ServoMotor3Period { get; set; } = 2;

        /// <summary>
        /// Creates a message payload for the ServoMotor3Period register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return ServoMotor3Period;
        }

        /// <summary>
        /// Creates a message that specifies the period of the servo motor in DO3, in microseconds.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the ServoMotor3Period register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.Behavior.ServoMotor3Period.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that specifies the period of the servo motor in DO3, in microseconds.
    /// </summary>
    [DisplayName("TimestampedServoMotor3PeriodPayload")]
    [Description("Creates a timestamped message payload that specifies the period of the servo motor in DO3, in microseconds.")]
    public partial class CreateTimestampedServoMotor3PeriodPayload : CreateServoMotor3PeriodPayload
    {
        /// <summary>
        /// Creates a timestamped message that specifies the period of the servo motor in DO3, in microseconds.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the ServoMotor3Period register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.Behavior.ServoMotor3Period.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that specifies the pulse of the servo motor in DO3, in microseconds.
    /// </summary>
    [DisplayName("ServoMotor3PulsePayload")]
    [Description("Creates a message payload that specifies the pulse of the servo motor in DO3, in microseconds.")]
    public partial class CreateServoMotor3PulsePayload
    {
        /// <summary>
        /// Gets or sets the value that specifies the pulse of the servo motor in DO3, in microseconds.
        /// </summary>
        [Range(min: 6, max: 65530)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that specifies the pulse of the servo motor in DO3, in microseconds.")]
        public ushort ServoMotor3Pulse { get; set; } = 6;

        /// <summary>
        /// Creates a message payload for the ServoMotor3Pulse register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return ServoMotor3Pulse;
        }

        /// <summary>
        /// Creates a message that specifies the pulse of the servo motor in DO3, in microseconds.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the ServoMotor3Pulse register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.Behavior.ServoMotor3Pulse.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that specifies the pulse of the servo motor in DO3, in microseconds.
    /// </summary>
    [DisplayName("TimestampedServoMotor3PulsePayload")]
    [Description("Creates a timestamped message payload that specifies the pulse of the servo motor in DO3, in microseconds.")]
    public partial class CreateTimestampedServoMotor3PulsePayload : CreateServoMotor3PulsePayload
    {
        /// <summary>
        /// Creates a timestamped message that specifies the pulse of the servo motor in DO3, in microseconds.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the ServoMotor3Pulse register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.Behavior.ServoMotor3Pulse.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that reset the counter of the specified encoders to zero.
    /// </summary>
    [DisplayName("EncoderResetPayload")]
    [Description("Creates a message payload that reset the counter of the specified encoders to zero.")]
    public partial class CreateEncoderResetPayload
    {
        /// <summary>
        /// Gets or sets the value that reset the counter of the specified encoders to zero.
        /// </summary>
        [Description("The value that reset the counter of the specified encoders to zero.")]
        public EncoderInputs EncoderReset { get; set; }

        /// <summary>
        /// Creates a message payload for the EncoderReset register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public EncoderInputs GetPayload()
        {
            return EncoderReset;
        }

        /// <summary>
        /// Creates a message that reset the counter of the specified encoders to zero.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the EncoderReset register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.Behavior.EncoderReset.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that reset the counter of the specified encoders to zero.
    /// </summary>
    [DisplayName("TimestampedEncoderResetPayload")]
    [Description("Creates a timestamped message payload that reset the counter of the specified encoders to zero.")]
    public partial class CreateTimestampedEncoderResetPayload : CreateEncoderResetPayload
    {
        /// <summary>
        /// Creates a timestamped message that reset the counter of the specified encoders to zero.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the EncoderReset register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.Behavior.EncoderReset.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that enables the timestamp for serial TX.
    /// </summary>
    [DisplayName("EnableSerialTimestampPayload")]
    [Description("Creates a message payload that enables the timestamp for serial TX.")]
    public partial class CreateEnableSerialTimestampPayload
    {
        /// <summary>
        /// Gets or sets the value that enables the timestamp for serial TX.
        /// </summary>
        [Description("The value that enables the timestamp for serial TX.")]
        public byte EnableSerialTimestamp { get; set; }

        /// <summary>
        /// Creates a message payload for the EnableSerialTimestamp register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public byte GetPayload()
        {
            return EnableSerialTimestamp;
        }

        /// <summary>
        /// Creates a message that enables the timestamp for serial TX.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the EnableSerialTimestamp register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.Behavior.EnableSerialTimestamp.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that enables the timestamp for serial TX.
    /// </summary>
    [DisplayName("TimestampedEnableSerialTimestampPayload")]
    [Description("Creates a timestamped message payload that enables the timestamp for serial TX.")]
    public partial class CreateTimestampedEnableSerialTimestampPayload : CreateEnableSerialTimestampPayload
    {
        /// <summary>
        /// Creates a timestamped message that enables the timestamp for serial TX.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the EnableSerialTimestamp register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.Behavior.EnableSerialTimestamp.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that specifies the digital output to mimic the Port 0 IR state.
    /// </summary>
    [DisplayName("MimicPort0IRPayload")]
    [Description("Creates a message payload that specifies the digital output to mimic the Port 0 IR state.")]
    public partial class CreateMimicPort0IRPayload
    {
        /// <summary>
        /// Gets or sets the value that specifies the digital output to mimic the Port 0 IR state.
        /// </summary>
        [Description("The value that specifies the digital output to mimic the Port 0 IR state.")]
        public MimicOutput MimicPort0IR { get; set; }

        /// <summary>
        /// Creates a message payload for the MimicPort0IR register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public MimicOutput GetPayload()
        {
            return MimicPort0IR;
        }

        /// <summary>
        /// Creates a message that specifies the digital output to mimic the Port 0 IR state.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the MimicPort0IR register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.Behavior.MimicPort0IR.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that specifies the digital output to mimic the Port 0 IR state.
    /// </summary>
    [DisplayName("TimestampedMimicPort0IRPayload")]
    [Description("Creates a timestamped message payload that specifies the digital output to mimic the Port 0 IR state.")]
    public partial class CreateTimestampedMimicPort0IRPayload : CreateMimicPort0IRPayload
    {
        /// <summary>
        /// Creates a timestamped message that specifies the digital output to mimic the Port 0 IR state.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the MimicPort0IR register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.Behavior.MimicPort0IR.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that specifies the digital output to mimic the Port 1 IR state.
    /// </summary>
    [DisplayName("MimicPort1IRPayload")]
    [Description("Creates a message payload that specifies the digital output to mimic the Port 1 IR state.")]
    public partial class CreateMimicPort1IRPayload
    {
        /// <summary>
        /// Gets or sets the value that specifies the digital output to mimic the Port 1 IR state.
        /// </summary>
        [Description("The value that specifies the digital output to mimic the Port 1 IR state.")]
        public MimicOutput MimicPort1IR { get; set; }

        /// <summary>
        /// Creates a message payload for the MimicPort1IR register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public MimicOutput GetPayload()
        {
            return MimicPort1IR;
        }

        /// <summary>
        /// Creates a message that specifies the digital output to mimic the Port 1 IR state.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the MimicPort1IR register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.Behavior.MimicPort1IR.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that specifies the digital output to mimic the Port 1 IR state.
    /// </summary>
    [DisplayName("TimestampedMimicPort1IRPayload")]
    [Description("Creates a timestamped message payload that specifies the digital output to mimic the Port 1 IR state.")]
    public partial class CreateTimestampedMimicPort1IRPayload : CreateMimicPort1IRPayload
    {
        /// <summary>
        /// Creates a timestamped message that specifies the digital output to mimic the Port 1 IR state.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the MimicPort1IR register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.Behavior.MimicPort1IR.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that specifies the digital output to mimic the Port 2 IR state.
    /// </summary>
    [DisplayName("MimicPort2IRPayload")]
    [Description("Creates a message payload that specifies the digital output to mimic the Port 2 IR state.")]
    public partial class CreateMimicPort2IRPayload
    {
        /// <summary>
        /// Gets or sets the value that specifies the digital output to mimic the Port 2 IR state.
        /// </summary>
        [Description("The value that specifies the digital output to mimic the Port 2 IR state.")]
        public MimicOutput MimicPort2IR { get; set; }

        /// <summary>
        /// Creates a message payload for the MimicPort2IR register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public MimicOutput GetPayload()
        {
            return MimicPort2IR;
        }

        /// <summary>
        /// Creates a message that specifies the digital output to mimic the Port 2 IR state.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the MimicPort2IR register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.Behavior.MimicPort2IR.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that specifies the digital output to mimic the Port 2 IR state.
    /// </summary>
    [DisplayName("TimestampedMimicPort2IRPayload")]
    [Description("Creates a timestamped message payload that specifies the digital output to mimic the Port 2 IR state.")]
    public partial class CreateTimestampedMimicPort2IRPayload : CreateMimicPort2IRPayload
    {
        /// <summary>
        /// Creates a timestamped message that specifies the digital output to mimic the Port 2 IR state.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the MimicPort2IR register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.Behavior.MimicPort2IR.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that specifies the digital output to mimic the Port 0 valve state.
    /// </summary>
    [DisplayName("MimicPort0ValvePayload")]
    [Description("Creates a message payload that specifies the digital output to mimic the Port 0 valve state.")]
    public partial class CreateMimicPort0ValvePayload
    {
        /// <summary>
        /// Gets or sets the value that specifies the digital output to mimic the Port 0 valve state.
        /// </summary>
        [Description("The value that specifies the digital output to mimic the Port 0 valve state.")]
        public MimicOutput MimicPort0Valve { get; set; }

        /// <summary>
        /// Creates a message payload for the MimicPort0Valve register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public MimicOutput GetPayload()
        {
            return MimicPort0Valve;
        }

        /// <summary>
        /// Creates a message that specifies the digital output to mimic the Port 0 valve state.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the MimicPort0Valve register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.Behavior.MimicPort0Valve.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that specifies the digital output to mimic the Port 0 valve state.
    /// </summary>
    [DisplayName("TimestampedMimicPort0ValvePayload")]
    [Description("Creates a timestamped message payload that specifies the digital output to mimic the Port 0 valve state.")]
    public partial class CreateTimestampedMimicPort0ValvePayload : CreateMimicPort0ValvePayload
    {
        /// <summary>
        /// Creates a timestamped message that specifies the digital output to mimic the Port 0 valve state.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the MimicPort0Valve register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.Behavior.MimicPort0Valve.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that specifies the digital output to mimic the Port 1 valve state.
    /// </summary>
    [DisplayName("MimicPort1ValvePayload")]
    [Description("Creates a message payload that specifies the digital output to mimic the Port 1 valve state.")]
    public partial class CreateMimicPort1ValvePayload
    {
        /// <summary>
        /// Gets or sets the value that specifies the digital output to mimic the Port 1 valve state.
        /// </summary>
        [Description("The value that specifies the digital output to mimic the Port 1 valve state.")]
        public MimicOutput MimicPort1Valve { get; set; }

        /// <summary>
        /// Creates a message payload for the MimicPort1Valve register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public MimicOutput GetPayload()
        {
            return MimicPort1Valve;
        }

        /// <summary>
        /// Creates a message that specifies the digital output to mimic the Port 1 valve state.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the MimicPort1Valve register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.Behavior.MimicPort1Valve.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that specifies the digital output to mimic the Port 1 valve state.
    /// </summary>
    [DisplayName("TimestampedMimicPort1ValvePayload")]
    [Description("Creates a timestamped message payload that specifies the digital output to mimic the Port 1 valve state.")]
    public partial class CreateTimestampedMimicPort1ValvePayload : CreateMimicPort1ValvePayload
    {
        /// <summary>
        /// Creates a timestamped message that specifies the digital output to mimic the Port 1 valve state.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the MimicPort1Valve register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.Behavior.MimicPort1Valve.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that specifies the digital output to mimic the Port 2 valve state.
    /// </summary>
    [DisplayName("MimicPort2ValvePayload")]
    [Description("Creates a message payload that specifies the digital output to mimic the Port 2 valve state.")]
    public partial class CreateMimicPort2ValvePayload
    {
        /// <summary>
        /// Gets or sets the value that specifies the digital output to mimic the Port 2 valve state.
        /// </summary>
        [Description("The value that specifies the digital output to mimic the Port 2 valve state.")]
        public MimicOutput MimicPort2Valve { get; set; }

        /// <summary>
        /// Creates a message payload for the MimicPort2Valve register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public MimicOutput GetPayload()
        {
            return MimicPort2Valve;
        }

        /// <summary>
        /// Creates a message that specifies the digital output to mimic the Port 2 valve state.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the MimicPort2Valve register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.Behavior.MimicPort2Valve.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that specifies the digital output to mimic the Port 2 valve state.
    /// </summary>
    [DisplayName("TimestampedMimicPort2ValvePayload")]
    [Description("Creates a timestamped message payload that specifies the digital output to mimic the Port 2 valve state.")]
    public partial class CreateTimestampedMimicPort2ValvePayload : CreateMimicPort2ValvePayload
    {
        /// <summary>
        /// Creates a timestamped message that specifies the digital output to mimic the Port 2 valve state.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the MimicPort2Valve register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.Behavior.MimicPort2Valve.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that specifies the low pass filter time value for poke inputs, in ms.
    /// </summary>
    [DisplayName("PokeInputFilterPayload")]
    [Description("Creates a message payload that specifies the low pass filter time value for poke inputs, in ms.")]
    public partial class CreatePokeInputFilterPayload
    {
        /// <summary>
        /// Gets or sets the value that specifies the low pass filter time value for poke inputs, in ms.
        /// </summary>
        [Description("The value that specifies the low pass filter time value for poke inputs, in ms.")]
        public byte PokeInputFilter { get; set; }

        /// <summary>
        /// Creates a message payload for the PokeInputFilter register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public byte GetPayload()
        {
            return PokeInputFilter;
        }

        /// <summary>
        /// Creates a message that specifies the low pass filter time value for poke inputs, in ms.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the PokeInputFilter register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.Behavior.PokeInputFilter.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that specifies the low pass filter time value for poke inputs, in ms.
    /// </summary>
    [DisplayName("TimestampedPokeInputFilterPayload")]
    [Description("Creates a timestamped message payload that specifies the low pass filter time value for poke inputs, in ms.")]
    public partial class CreateTimestampedPokeInputFilterPayload : CreatePokeInputFilterPayload
    {
        /// <summary>
        /// Creates a timestamped message that specifies the low pass filter time value for poke inputs, in ms.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the PokeInputFilter register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.Behavior.PokeInputFilter.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents the payload of the AnalogData register.
    /// </summary>
    public struct AnalogDataPayload
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AnalogDataPayload"/> structure.
        /// </summary>
        /// <param name="analogInput0">The voltage at the output of the ADC channel 0.</param>
        /// <param name="encoder">The quadrature counter value on Port 2</param>
        /// <param name="analogInput1">The voltage at the output of the ADC channel 1.</param>
        public AnalogDataPayload(
            short analogInput0,
            short encoder,
            short analogInput1)
        {
            AnalogInput0 = analogInput0;
            Encoder = encoder;
            AnalogInput1 = analogInput1;
        }

        /// <summary>
        /// The voltage at the output of the ADC channel 0.
        /// </summary>
        public short AnalogInput0;

        /// <summary>
        /// The quadrature counter value on Port 2
        /// </summary>
        public short Encoder;

        /// <summary>
        /// The voltage at the output of the ADC channel 1.
        /// </summary>
        public short AnalogInput1;
    }

    /// <summary>
    /// Represents the payload of the RgbAll register.
    /// </summary>
    public struct RgbAllPayload
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RgbAllPayload"/> structure.
        /// </summary>
        /// <param name="green0">The intensity of the green channel in the RGB0 LED.</param>
        /// <param name="red0">The intensity of the red channel in the RGB0 LED.</param>
        /// <param name="blue0">The intensity of the blue channel in the RGB0 LED.</param>
        /// <param name="green1">The intensity of the green channel in the RGB1 LED.</param>
        /// <param name="red1">The intensity of the red channel in the RGB1 LED.</param>
        /// <param name="blue1">The intensity of the blue channel in the RGB1 LED.</param>
        public RgbAllPayload(
            byte green0,
            byte red0,
            byte blue0,
            byte green1,
            byte red1,
            byte blue1)
        {
            Green0 = green0;
            Red0 = red0;
            Blue0 = blue0;
            Green1 = green1;
            Red1 = red1;
            Blue1 = blue1;
        }

        /// <summary>
        /// The intensity of the green channel in the RGB0 LED.
        /// </summary>
        public byte Green0;

        /// <summary>
        /// The intensity of the red channel in the RGB0 LED.
        /// </summary>
        public byte Red0;

        /// <summary>
        /// The intensity of the blue channel in the RGB0 LED.
        /// </summary>
        public byte Blue0;

        /// <summary>
        /// The intensity of the green channel in the RGB1 LED.
        /// </summary>
        public byte Green1;

        /// <summary>
        /// The intensity of the red channel in the RGB1 LED.
        /// </summary>
        public byte Red1;

        /// <summary>
        /// The intensity of the blue channel in the RGB1 LED.
        /// </summary>
        public byte Blue1;
    }

    /// <summary>
    /// Represents the payload of the Rgb register.
    /// </summary>
    public struct RgbPayload
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RgbPayload"/> structure.
        /// </summary>
        /// <param name="green">The intensity of the green channel in the RGB LED.</param>
        /// <param name="red">The intensity of the red channel in the RGB LED.</param>
        /// <param name="blue">The intensity of the blue channel in the RGB LED.</param>
        public RgbPayload(
            byte green,
            byte red,
            byte blue)
        {
            Green = green;
            Red = red;
            Blue = blue;
        }

        /// <summary>
        /// The intensity of the green channel in the RGB LED.
        /// </summary>
        public byte Green;

        /// <summary>
        /// The intensity of the red channel in the RGB LED.
        /// </summary>
        public byte Red;

        /// <summary>
        /// The intensity of the blue channel in the RGB LED.
        /// </summary>
        public byte Blue;
    }

    /// <summary>
    /// Specifies the state of port digital input lines.
    /// </summary>
    [Flags]
    public enum DigitalInputs : byte
    {
        None = 0x0,
        DIPort0 = 0x1,
        DIPort1 = 0x2,
        DIPort2 = 0x4,
        DI3 = 0x8
    }

    /// <summary>
    /// Specifies the state of port digital output lines.
    /// </summary>
    [Flags]
    public enum DigitalOutputs : ushort
    {
        None = 0x0,
        DOPort0 = 0x1,
        DOPort1 = 0x2,
        DOPort2 = 0x4,
        SupplyPort0 = 0x8,
        SupplyPort1 = 0x10,
        SupplyPort2 = 0x20,
        Led0 = 0x40,
        Led1 = 0x80,
        Rgb0 = 0x100,
        Rgb1 = 0x200,
        DO0 = 0x400,
        DO1 = 0x800,
        DO2 = 0x1000,
        DO3 = 0x2000
    }

    /// <summary>
    /// Specifies the state of the port DIO lines.
    /// </summary>
    [Flags]
    public enum PortDigitalIOS : byte
    {
        None = 0x0,
        DIO0 = 0x1,
        DIO1 = 0x2,
        DIO2 = 0x4
    }

    /// <summary>
    /// Specifies the state of PWM output lines.
    /// </summary>
    [Flags]
    public enum PwmOutputs : byte
    {
        None = 0x0,
        PwmDO0 = 0x1,
        PwmDO1 = 0x2,
        PwmDO2 = 0x4,
        PwmDO3 = 0x8
    }

    /// <summary>
    /// Specifies the active events in the device.
    /// </summary>
    [Flags]
    public enum Events : byte
    {
        None = 0x0,
        PortDI = 0x1,
        PortDIO = 0x2,
        AnalogData = 0x4,
        Camera0 = 0x8,
        Camera1 = 0x10
    }

    /// <summary>
    /// Specifies camera output enable bits.
    /// </summary>
    [Flags]
    public enum CameraOutputs : byte
    {
        None = 0x0,
        CameraOutput0 = 0x1,
        CameraOutput1 = 0x2
    }

    /// <summary>
    /// Specifies servo output enable bits.
    /// </summary>
    [Flags]
    public enum ServoOutputs : byte
    {
        None = 0x0,
        ServoOutput2 = 0x4,
        ServoOutput3 = 0x8
    }

    /// <summary>
    /// Specifies quadrature counter enable bits.
    /// </summary>
    [Flags]
    public enum EncoderInputs : byte
    {
        None = 0x0,
        EncoderPort2 = 0x4
    }

    /// <summary>
    /// Specifies that camera frame was acquired.
    /// </summary>
    [Flags]
    public enum FrameAcquired : byte
    {
        None = 0x0,
        FrameAcquired = 0x1
    }

    /// <summary>
    /// Specifies the target IO on which to mimic the specified register.
    /// </summary>
    public enum MimicOutput : byte
    {
        None = 0,
        DIO0 = 1,
        DIO1 = 2,
        DIO2 = 3,
        DO0 = 4,
        DO1 = 5,
        DO2 = 6,
        DO3 = 7
    }

    /// <summary>
    /// Specifies the type of reading made from the quadrature encoder.
    /// </summary>
    public enum EncoderModeConfig : byte
    {
        Position = 0,
        Displacement = 1
    }
}
