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
        /// Gets a read-only mapping from address to register name.
        /// </summary>
        public static new IReadOnlyDictionary<int, string> RegisterMap { get; } = new Dictionary<int, string>
            (Bonsai.Harp.Device.RegisterMap.ToDictionary(entry => entry.Key, entry => entry.Value))
        {
            { 32, "PortDigitalInput" },
            { 34, "OutputSet" },
            { 35, "OutputClear" },
            { 36, "OutputToggle" },
            { 37, "OutputState" },
            { 38, "PortDIOSet" },
            { 39, "PortDIOClear" },
            { 40, "PortDIOToggle" },
            { 41, "PortDIOState" },
            { 42, "PortDIODirection" },
            { 43, "PortDIOStateEvent" },
            { 44, "AnalogData" },
            { 45, "OutputPulseEnable" },
            { 46, "PulseDOPort0" },
            { 47, "PulseDOPort1" },
            { 48, "PulseDOPort2" },
            { 49, "PulseSupplyPort0" },
            { 50, "PulseSupplyPort1" },
            { 51, "PulseSupplyPort2" },
            { 52, "PulseLed0" },
            { 53, "PulseLed1" },
            { 54, "PulseRgb0" },
            { 55, "PulseRgb1" },
            { 56, "PulseDO0" },
            { 57, "PulseDO1" },
            { 58, "PulseDO2" },
            { 59, "PulseDO3" },
            { 60, "PwmFrequencyDO0" },
            { 61, "PwmFrequencyDO1" },
            { 62, "PwmFrequencyDO2" },
            { 63, "PwmFrequencyDO3" },
            { 64, "PwmDutyCycleDO0" },
            { 65, "PwmDutyCycleDO1" },
            { 66, "PwmDutyCycleDO2" },
            { 67, "PwmDutyCycleDO3" },
            { 68, "PwmStart" },
            { 69, "PwmStop" },
            { 70, "RgbAll" },
            { 71, "Rgb0" },
            { 72, "Rgb1" },
            { 73, "Led0Current" },
            { 74, "Led1Current" },
            { 75, "Led0MaxCurrent" },
            { 76, "Led1MaxCurrent" },
            { 77, "EventEnable" },
            { 78, "StartCameras" },
            { 79, "StopCameras" },
            { 80, "EnableServos" },
            { 81, "DisableServos" },
            { 82, "EnableEncoders" },
            { 92, "Camera0Frame" },
            { 93, "Camera0Frequency" },
            { 94, "Camera1Frame" },
            { 95, "Camera1Frequency" },
            { 100, "ServoMotor2Period" },
            { 101, "ServoMotor2Pulse" },
            { 102, "ServoMotor3Period" },
            { 103, "ServoMotor3Pulse" },
            { 108, "EncoderReset" },
            { 110, "EnableSerialTimestamp" },
            { 111, "MimicPort0IR" },
            { 112, "MimicPort1IR" },
            { 113, "MimicPort2IR" },
            { 117, "MimicPort0Valve" },
            { 118, "MimicPort1Valve" },
            { 119, "MimicPort2Valve" },
            { 122, "PokeInputFilter" }
        };
    }

    /// <summary>
    /// Represents an operator that groups the sequence of <see cref="Behavior"/>" messages by register name.
    /// </summary>
    [Description("Groups the sequence of Behavior messages by register name.")]
    public partial class GroupByRegister : Combinator<HarpMessage, IGroupedObservable<string, HarpMessage>>
    {
        /// <summary>
        /// Groups an observable sequence of <see cref="Behavior"/> messages
        /// by register name.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of observable groups, each of which corresponds to a unique
        /// <see cref="Behavior"/> register.
        /// </returns>
        public override IObservable<IGroupedObservable<string, HarpMessage>> Process(IObservable<HarpMessage> source)
        {
            return source.GroupBy(message => Device.RegisterMap[message.Address]);
        }
    }

    /// <summary>
    /// Represents an operator that filters register-specific messages
    /// reported by the <see cref="Behavior"/> device.
    /// </summary>
    [XmlInclude(typeof(Bonsai.Expressions.TypeMapping<PortDigitalInput>))]
    [XmlInclude(typeof(Bonsai.Expressions.TypeMapping<OutputSet>))]
    [XmlInclude(typeof(Bonsai.Expressions.TypeMapping<OutputClear>))]
    [XmlInclude(typeof(Bonsai.Expressions.TypeMapping<OutputToggle>))]
    [XmlInclude(typeof(Bonsai.Expressions.TypeMapping<OutputState>))]
    [XmlInclude(typeof(Bonsai.Expressions.TypeMapping<PortDIOSet>))]
    [XmlInclude(typeof(Bonsai.Expressions.TypeMapping<PortDIOClear>))]
    [XmlInclude(typeof(Bonsai.Expressions.TypeMapping<PortDIOToggle>))]
    [XmlInclude(typeof(Bonsai.Expressions.TypeMapping<PortDIOState>))]
    [XmlInclude(typeof(Bonsai.Expressions.TypeMapping<PortDIODirection>))]
    [XmlInclude(typeof(Bonsai.Expressions.TypeMapping<PortDIOStateEvent>))]
    [XmlInclude(typeof(Bonsai.Expressions.TypeMapping<AnalogData>))]
    [XmlInclude(typeof(Bonsai.Expressions.TypeMapping<OutputPulseEnable>))]
    [XmlInclude(typeof(Bonsai.Expressions.TypeMapping<PulseDOPort0>))]
    [XmlInclude(typeof(Bonsai.Expressions.TypeMapping<PulseDOPort1>))]
    [XmlInclude(typeof(Bonsai.Expressions.TypeMapping<PulseDOPort2>))]
    [XmlInclude(typeof(Bonsai.Expressions.TypeMapping<PulseSupplyPort0>))]
    [XmlInclude(typeof(Bonsai.Expressions.TypeMapping<PulseSupplyPort1>))]
    [XmlInclude(typeof(Bonsai.Expressions.TypeMapping<PulseSupplyPort2>))]
    [XmlInclude(typeof(Bonsai.Expressions.TypeMapping<PulseLed0>))]
    [XmlInclude(typeof(Bonsai.Expressions.TypeMapping<PulseLed1>))]
    [XmlInclude(typeof(Bonsai.Expressions.TypeMapping<PulseRgb0>))]
    [XmlInclude(typeof(Bonsai.Expressions.TypeMapping<PulseRgb1>))]
    [XmlInclude(typeof(Bonsai.Expressions.TypeMapping<PulseDO0>))]
    [XmlInclude(typeof(Bonsai.Expressions.TypeMapping<PulseDO1>))]
    [XmlInclude(typeof(Bonsai.Expressions.TypeMapping<PulseDO2>))]
    [XmlInclude(typeof(Bonsai.Expressions.TypeMapping<PulseDO3>))]
    [XmlInclude(typeof(Bonsai.Expressions.TypeMapping<PwmFrequencyDO0>))]
    [XmlInclude(typeof(Bonsai.Expressions.TypeMapping<PwmFrequencyDO1>))]
    [XmlInclude(typeof(Bonsai.Expressions.TypeMapping<PwmFrequencyDO2>))]
    [XmlInclude(typeof(Bonsai.Expressions.TypeMapping<PwmFrequencyDO3>))]
    [XmlInclude(typeof(Bonsai.Expressions.TypeMapping<PwmDutyCycleDO0>))]
    [XmlInclude(typeof(Bonsai.Expressions.TypeMapping<PwmDutyCycleDO1>))]
    [XmlInclude(typeof(Bonsai.Expressions.TypeMapping<PwmDutyCycleDO2>))]
    [XmlInclude(typeof(Bonsai.Expressions.TypeMapping<PwmDutyCycleDO3>))]
    [XmlInclude(typeof(Bonsai.Expressions.TypeMapping<PwmStart>))]
    [XmlInclude(typeof(Bonsai.Expressions.TypeMapping<PwmStop>))]
    [XmlInclude(typeof(Bonsai.Expressions.TypeMapping<RgbAll>))]
    [XmlInclude(typeof(Bonsai.Expressions.TypeMapping<Rgb0>))]
    [XmlInclude(typeof(Bonsai.Expressions.TypeMapping<Rgb1>))]
    [XmlInclude(typeof(Bonsai.Expressions.TypeMapping<Led0Current>))]
    [XmlInclude(typeof(Bonsai.Expressions.TypeMapping<Led1Current>))]
    [XmlInclude(typeof(Bonsai.Expressions.TypeMapping<Led0MaxCurrent>))]
    [XmlInclude(typeof(Bonsai.Expressions.TypeMapping<Led1MaxCurrent>))]
    [XmlInclude(typeof(Bonsai.Expressions.TypeMapping<EventEnable>))]
    [XmlInclude(typeof(Bonsai.Expressions.TypeMapping<StartCameras>))]
    [XmlInclude(typeof(Bonsai.Expressions.TypeMapping<StopCameras>))]
    [XmlInclude(typeof(Bonsai.Expressions.TypeMapping<EnableServos>))]
    [XmlInclude(typeof(Bonsai.Expressions.TypeMapping<DisableServos>))]
    [XmlInclude(typeof(Bonsai.Expressions.TypeMapping<EnableEncoders>))]
    [XmlInclude(typeof(Bonsai.Expressions.TypeMapping<Camera0Frame>))]
    [XmlInclude(typeof(Bonsai.Expressions.TypeMapping<Camera0Frequency>))]
    [XmlInclude(typeof(Bonsai.Expressions.TypeMapping<Camera1Frame>))]
    [XmlInclude(typeof(Bonsai.Expressions.TypeMapping<Camera1Frequency>))]
    [XmlInclude(typeof(Bonsai.Expressions.TypeMapping<ServoMotor2Period>))]
    [XmlInclude(typeof(Bonsai.Expressions.TypeMapping<ServoMotor2Pulse>))]
    [XmlInclude(typeof(Bonsai.Expressions.TypeMapping<ServoMotor3Period>))]
    [XmlInclude(typeof(Bonsai.Expressions.TypeMapping<ServoMotor3Pulse>))]
    [XmlInclude(typeof(Bonsai.Expressions.TypeMapping<EncoderReset>))]
    [XmlInclude(typeof(Bonsai.Expressions.TypeMapping<EnableSerialTimestamp>))]
    [XmlInclude(typeof(Bonsai.Expressions.TypeMapping<MimicPort0IR>))]
    [XmlInclude(typeof(Bonsai.Expressions.TypeMapping<MimicPort1IR>))]
    [XmlInclude(typeof(Bonsai.Expressions.TypeMapping<MimicPort2IR>))]
    [XmlInclude(typeof(Bonsai.Expressions.TypeMapping<MimicPort0Valve>))]
    [XmlInclude(typeof(Bonsai.Expressions.TypeMapping<MimicPort1Valve>))]
    [XmlInclude(typeof(Bonsai.Expressions.TypeMapping<MimicPort2Valve>))]
    [XmlInclude(typeof(Bonsai.Expressions.TypeMapping<PokeInputFilter>))]
    [Description("Filters register-specific messages reported by the Behavior device.")]
    public class FilterMessage : FilterMessageBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FilterMessage"/> class.
        /// </summary>
        public FilterMessage()
        {
            Register = new Bonsai.Expressions.TypeMapping<PortDigitalInput>();
        }
    }

    /// <summary>
    /// Represents an operator which filters and selects specific messages
    /// reported by the Behavior device.
    /// </summary>
    /// <seealso cref="PortDigitalInput"/>
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
    [XmlInclude(typeof(PortDigitalInput))]
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
    [XmlInclude(typeof(TimestampedPortDigitalInput))]
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
            Register = new PortDigitalInput();
        }

        string INamedElement.Name => $"{nameof(Behavior)}.{GetElementDisplayName(Register)}";
    }

    /// <summary>
    /// Represents an operator which formats a sequence of values as specific
    /// Behavior register messages.
    /// </summary>
    /// <seealso cref="PortDigitalInput"/>
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
    [XmlInclude(typeof(PortDigitalInput))]
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
            Register = new PortDigitalInput();
        }

        string INamedElement.Name => $"{nameof(Behavior)}.{GetElementDisplayName(Register)}";
    }

    /// <summary>
    /// Represents an operator that reflects the state of DI digital lines of each Port.
    /// </summary>
    [Description("Reflects the state of DI digital lines of each Port")]
    public partial class PortDigitalInput : HarpCombinator
    {
        /// <summary>
        /// Represents the address of the <see cref="PortDigitalInput"/> register. This field is constant.
        /// </summary>
        public const int Address = 32;

        /// <summary>
        /// Represents the payload type of the <see cref="PortDigitalInput"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="PortDigitalInput"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Initializes a new instance of the <see cref="PortDigitalInput"/> class.
        /// </summary>
        public PortDigitalInput()
        {
            MessageType = MessageType.Event;
        }

        /// <summary>
        /// Returns the payload data for <see cref="PortDigitalInput"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static DigitalInputs GetPayload(HarpMessage message)
        {
            return (DigitalInputs)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="PortDigitalInput"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalInputs> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((DigitalInputs)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="PortDigitalInput"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="PortDigitalInput"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, DigitalInputs value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="PortDigitalInput"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="PortDigitalInput"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, DigitalInputs value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }

        /// <summary>
        /// Filters and selects an observable sequence of messages from the
        /// <see cref="PortDigitalInput"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of <see cref="DigitalInputs"/> objects representing the
        /// message payload.
        /// </returns>
        public IObservable<DigitalInputs> Process(IObservable<HarpMessage> source)
        {
            return source.Where(Address, MessageType).Select(GetPayload);
        }

        /// <summary>
        /// Formats an observable sequence of values into Harp messages
        /// for the <see cref="PortDigitalInput"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of <see cref="DigitalInputs"/> objects representing the
        /// message payload.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects formatted for the
        /// <see cref="PortDigitalInput"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<DigitalInputs> source)
        {
            return source.Select(value => FromPayload(MessageType, value));
        }

        /// <summary>
        /// Formats an observable sequence of values into timestamped Harp messages
        /// for the <see cref="PortDigitalInput"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of timestamped <see cref="DigitalInputs"/> objects representing
        /// the message payload.
        /// </param>
        /// <returns>
        /// A sequence of timestamped <see cref="HarpMessage"/> objects formatted for
        /// the <see cref="PortDigitalInput"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<Timestamped<DigitalInputs>> source)
        {
            return source.Select(payload => FromPayload(payload.Seconds, MessageType, payload.Value));
        }
    }

    /// <summary>
    /// Represents an operator that filters and selects a sequence of timestamped messages
    /// from the PortDigitalInput register.
    /// </summary>
    [Description("Filters and selects timestamped messages from the PortDigitalInput register.")]
    public partial class TimestampedPortDigitalInput : HarpCombinator
    {
        /// <summary>
        /// Filters and selects an observable sequence of timestamped messages from
        /// the <see cref="PortDigitalInput"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of timestamped <see cref="DigitalInputs"/> objects
        /// representing the register payload.
        /// </returns>
        public IObservable<Timestamped<DigitalInputs>> Process(IObservable<HarpMessage> source)
        {
            return source.Where(PortDigitalInput.Address, MessageType).Select(PortDigitalInput.GetTimestampedPayload);
        }
    }

    /// <summary>
    /// Represents an operator that set the specified digital output lines.
    /// </summary>
    [Description("Set the specified digital output lines.")]
    public partial class OutputSet : HarpCombinator
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

        /// <summary>
        /// Filters and selects an observable sequence of messages from the
        /// <see cref="OutputSet"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of <see cref="DigitalOutputs"/> objects representing the
        /// message payload.
        /// </returns>
        public IObservable<DigitalOutputs> Process(IObservable<HarpMessage> source)
        {
            return source.Where(Address, MessageType).Select(GetPayload);
        }

        /// <summary>
        /// Formats an observable sequence of values into Harp messages
        /// for the <see cref="OutputSet"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of <see cref="DigitalOutputs"/> objects representing the
        /// message payload.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects formatted for the
        /// <see cref="OutputSet"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<DigitalOutputs> source)
        {
            return source.Select(value => FromPayload(MessageType, value));
        }

        /// <summary>
        /// Formats an observable sequence of values into timestamped Harp messages
        /// for the <see cref="OutputSet"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of timestamped <see cref="DigitalOutputs"/> objects representing
        /// the message payload.
        /// </param>
        /// <returns>
        /// A sequence of timestamped <see cref="HarpMessage"/> objects formatted for
        /// the <see cref="OutputSet"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<Timestamped<DigitalOutputs>> source)
        {
            return source.Select(payload => FromPayload(payload.Seconds, MessageType, payload.Value));
        }
    }

    /// <summary>
    /// Represents an operator that filters and selects a sequence of timestamped messages
    /// from the OutputSet register.
    /// </summary>
    [Description("Filters and selects timestamped messages from the OutputSet register.")]
    public partial class TimestampedOutputSet : HarpCombinator
    {
        /// <summary>
        /// Filters and selects an observable sequence of timestamped messages from
        /// the <see cref="OutputSet"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of timestamped <see cref="DigitalOutputs"/> objects
        /// representing the register payload.
        /// </returns>
        public IObservable<Timestamped<DigitalOutputs>> Process(IObservable<HarpMessage> source)
        {
            return source.Where(OutputSet.Address, MessageType).Select(OutputSet.GetTimestampedPayload);
        }
    }

    /// <summary>
    /// Represents an operator that clear the specified digital output lines.
    /// </summary>
    [Description("Clear the specified digital output lines")]
    public partial class OutputClear : HarpCombinator
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

        /// <summary>
        /// Filters and selects an observable sequence of messages from the
        /// <see cref="OutputClear"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of <see cref="DigitalOutputs"/> objects representing the
        /// message payload.
        /// </returns>
        public IObservable<DigitalOutputs> Process(IObservable<HarpMessage> source)
        {
            return source.Where(Address, MessageType).Select(GetPayload);
        }

        /// <summary>
        /// Formats an observable sequence of values into Harp messages
        /// for the <see cref="OutputClear"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of <see cref="DigitalOutputs"/> objects representing the
        /// message payload.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects formatted for the
        /// <see cref="OutputClear"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<DigitalOutputs> source)
        {
            return source.Select(value => FromPayload(MessageType, value));
        }

        /// <summary>
        /// Formats an observable sequence of values into timestamped Harp messages
        /// for the <see cref="OutputClear"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of timestamped <see cref="DigitalOutputs"/> objects representing
        /// the message payload.
        /// </param>
        /// <returns>
        /// A sequence of timestamped <see cref="HarpMessage"/> objects formatted for
        /// the <see cref="OutputClear"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<Timestamped<DigitalOutputs>> source)
        {
            return source.Select(payload => FromPayload(payload.Seconds, MessageType, payload.Value));
        }
    }

    /// <summary>
    /// Represents an operator that filters and selects a sequence of timestamped messages
    /// from the OutputClear register.
    /// </summary>
    [Description("Filters and selects timestamped messages from the OutputClear register.")]
    public partial class TimestampedOutputClear : HarpCombinator
    {
        /// <summary>
        /// Filters and selects an observable sequence of timestamped messages from
        /// the <see cref="OutputClear"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of timestamped <see cref="DigitalOutputs"/> objects
        /// representing the register payload.
        /// </returns>
        public IObservable<Timestamped<DigitalOutputs>> Process(IObservable<HarpMessage> source)
        {
            return source.Where(OutputClear.Address, MessageType).Select(OutputClear.GetTimestampedPayload);
        }
    }

    /// <summary>
    /// Represents an operator that toggle the specified digital output lines.
    /// </summary>
    [Description("Toggle the specified digital output lines")]
    public partial class OutputToggle : HarpCombinator
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

        /// <summary>
        /// Filters and selects an observable sequence of messages from the
        /// <see cref="OutputToggle"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of <see cref="DigitalOutputs"/> objects representing the
        /// message payload.
        /// </returns>
        public IObservable<DigitalOutputs> Process(IObservable<HarpMessage> source)
        {
            return source.Where(Address, MessageType).Select(GetPayload);
        }

        /// <summary>
        /// Formats an observable sequence of values into Harp messages
        /// for the <see cref="OutputToggle"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of <see cref="DigitalOutputs"/> objects representing the
        /// message payload.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects formatted for the
        /// <see cref="OutputToggle"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<DigitalOutputs> source)
        {
            return source.Select(value => FromPayload(MessageType, value));
        }

        /// <summary>
        /// Formats an observable sequence of values into timestamped Harp messages
        /// for the <see cref="OutputToggle"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of timestamped <see cref="DigitalOutputs"/> objects representing
        /// the message payload.
        /// </param>
        /// <returns>
        /// A sequence of timestamped <see cref="HarpMessage"/> objects formatted for
        /// the <see cref="OutputToggle"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<Timestamped<DigitalOutputs>> source)
        {
            return source.Select(payload => FromPayload(payload.Seconds, MessageType, payload.Value));
        }
    }

    /// <summary>
    /// Represents an operator that filters and selects a sequence of timestamped messages
    /// from the OutputToggle register.
    /// </summary>
    [Description("Filters and selects timestamped messages from the OutputToggle register.")]
    public partial class TimestampedOutputToggle : HarpCombinator
    {
        /// <summary>
        /// Filters and selects an observable sequence of timestamped messages from
        /// the <see cref="OutputToggle"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of timestamped <see cref="DigitalOutputs"/> objects
        /// representing the register payload.
        /// </returns>
        public IObservable<Timestamped<DigitalOutputs>> Process(IObservable<HarpMessage> source)
        {
            return source.Where(OutputToggle.Address, MessageType).Select(OutputToggle.GetTimestampedPayload);
        }
    }

    /// <summary>
    /// Represents an operator that write the state of all digital output lines.
    /// </summary>
    [Description("Write the state of all digital output lines")]
    public partial class OutputState : HarpCombinator
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

        /// <summary>
        /// Filters and selects an observable sequence of messages from the
        /// <see cref="OutputState"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of <see cref="DigitalOutputs"/> objects representing the
        /// message payload.
        /// </returns>
        public IObservable<DigitalOutputs> Process(IObservable<HarpMessage> source)
        {
            return source.Where(Address, MessageType).Select(GetPayload);
        }

        /// <summary>
        /// Formats an observable sequence of values into Harp messages
        /// for the <see cref="OutputState"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of <see cref="DigitalOutputs"/> objects representing the
        /// message payload.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects formatted for the
        /// <see cref="OutputState"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<DigitalOutputs> source)
        {
            return source.Select(value => FromPayload(MessageType, value));
        }

        /// <summary>
        /// Formats an observable sequence of values into timestamped Harp messages
        /// for the <see cref="OutputState"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of timestamped <see cref="DigitalOutputs"/> objects representing
        /// the message payload.
        /// </param>
        /// <returns>
        /// A sequence of timestamped <see cref="HarpMessage"/> objects formatted for
        /// the <see cref="OutputState"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<Timestamped<DigitalOutputs>> source)
        {
            return source.Select(payload => FromPayload(payload.Seconds, MessageType, payload.Value));
        }
    }

    /// <summary>
    /// Represents an operator that filters and selects a sequence of timestamped messages
    /// from the OutputState register.
    /// </summary>
    [Description("Filters and selects timestamped messages from the OutputState register.")]
    public partial class TimestampedOutputState : HarpCombinator
    {
        /// <summary>
        /// Filters and selects an observable sequence of timestamped messages from
        /// the <see cref="OutputState"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of timestamped <see cref="DigitalOutputs"/> objects
        /// representing the register payload.
        /// </returns>
        public IObservable<Timestamped<DigitalOutputs>> Process(IObservable<HarpMessage> source)
        {
            return source.Where(OutputState.Address, MessageType).Select(OutputState.GetTimestampedPayload);
        }
    }

    /// <summary>
    /// Represents an operator that set the specified port DIO lines.
    /// </summary>
    [Description("Set the specified port DIO lines")]
    public partial class PortDIOSet : HarpCombinator
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

        /// <summary>
        /// Filters and selects an observable sequence of messages from the
        /// <see cref="PortDIOSet"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of <see cref="PortDigitalIOS"/> objects representing the
        /// message payload.
        /// </returns>
        public IObservable<PortDigitalIOS> Process(IObservable<HarpMessage> source)
        {
            return source.Where(Address, MessageType).Select(GetPayload);
        }

        /// <summary>
        /// Formats an observable sequence of values into Harp messages
        /// for the <see cref="PortDIOSet"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of <see cref="PortDigitalIOS"/> objects representing the
        /// message payload.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects formatted for the
        /// <see cref="PortDIOSet"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<PortDigitalIOS> source)
        {
            return source.Select(value => FromPayload(MessageType, value));
        }

        /// <summary>
        /// Formats an observable sequence of values into timestamped Harp messages
        /// for the <see cref="PortDIOSet"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of timestamped <see cref="PortDigitalIOS"/> objects representing
        /// the message payload.
        /// </param>
        /// <returns>
        /// A sequence of timestamped <see cref="HarpMessage"/> objects formatted for
        /// the <see cref="PortDIOSet"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<Timestamped<PortDigitalIOS>> source)
        {
            return source.Select(payload => FromPayload(payload.Seconds, MessageType, payload.Value));
        }
    }

    /// <summary>
    /// Represents an operator that filters and selects a sequence of timestamped messages
    /// from the PortDIOSet register.
    /// </summary>
    [Description("Filters and selects timestamped messages from the PortDIOSet register.")]
    public partial class TimestampedPortDIOSet : HarpCombinator
    {
        /// <summary>
        /// Filters and selects an observable sequence of timestamped messages from
        /// the <see cref="PortDIOSet"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of timestamped <see cref="PortDigitalIOS"/> objects
        /// representing the register payload.
        /// </returns>
        public IObservable<Timestamped<PortDigitalIOS>> Process(IObservable<HarpMessage> source)
        {
            return source.Where(PortDIOSet.Address, MessageType).Select(PortDIOSet.GetTimestampedPayload);
        }
    }

    /// <summary>
    /// Represents an operator that clear the specified port DIO lines.
    /// </summary>
    [Description("Clear the specified port DIO lines")]
    public partial class PortDIOClear : HarpCombinator
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

        /// <summary>
        /// Filters and selects an observable sequence of messages from the
        /// <see cref="PortDIOClear"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of <see cref="PortDigitalIOS"/> objects representing the
        /// message payload.
        /// </returns>
        public IObservable<PortDigitalIOS> Process(IObservable<HarpMessage> source)
        {
            return source.Where(Address, MessageType).Select(GetPayload);
        }

        /// <summary>
        /// Formats an observable sequence of values into Harp messages
        /// for the <see cref="PortDIOClear"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of <see cref="PortDigitalIOS"/> objects representing the
        /// message payload.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects formatted for the
        /// <see cref="PortDIOClear"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<PortDigitalIOS> source)
        {
            return source.Select(value => FromPayload(MessageType, value));
        }

        /// <summary>
        /// Formats an observable sequence of values into timestamped Harp messages
        /// for the <see cref="PortDIOClear"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of timestamped <see cref="PortDigitalIOS"/> objects representing
        /// the message payload.
        /// </param>
        /// <returns>
        /// A sequence of timestamped <see cref="HarpMessage"/> objects formatted for
        /// the <see cref="PortDIOClear"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<Timestamped<PortDigitalIOS>> source)
        {
            return source.Select(payload => FromPayload(payload.Seconds, MessageType, payload.Value));
        }
    }

    /// <summary>
    /// Represents an operator that filters and selects a sequence of timestamped messages
    /// from the PortDIOClear register.
    /// </summary>
    [Description("Filters and selects timestamped messages from the PortDIOClear register.")]
    public partial class TimestampedPortDIOClear : HarpCombinator
    {
        /// <summary>
        /// Filters and selects an observable sequence of timestamped messages from
        /// the <see cref="PortDIOClear"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of timestamped <see cref="PortDigitalIOS"/> objects
        /// representing the register payload.
        /// </returns>
        public IObservable<Timestamped<PortDigitalIOS>> Process(IObservable<HarpMessage> source)
        {
            return source.Where(PortDIOClear.Address, MessageType).Select(PortDIOClear.GetTimestampedPayload);
        }
    }

    /// <summary>
    /// Represents an operator that toggle the specified port DIO lines.
    /// </summary>
    [Description("Toggle the specified port DIO lines")]
    public partial class PortDIOToggle : HarpCombinator
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

        /// <summary>
        /// Filters and selects an observable sequence of messages from the
        /// <see cref="PortDIOToggle"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of <see cref="PortDigitalIOS"/> objects representing the
        /// message payload.
        /// </returns>
        public IObservable<PortDigitalIOS> Process(IObservable<HarpMessage> source)
        {
            return source.Where(Address, MessageType).Select(GetPayload);
        }

        /// <summary>
        /// Formats an observable sequence of values into Harp messages
        /// for the <see cref="PortDIOToggle"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of <see cref="PortDigitalIOS"/> objects representing the
        /// message payload.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects formatted for the
        /// <see cref="PortDIOToggle"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<PortDigitalIOS> source)
        {
            return source.Select(value => FromPayload(MessageType, value));
        }

        /// <summary>
        /// Formats an observable sequence of values into timestamped Harp messages
        /// for the <see cref="PortDIOToggle"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of timestamped <see cref="PortDigitalIOS"/> objects representing
        /// the message payload.
        /// </param>
        /// <returns>
        /// A sequence of timestamped <see cref="HarpMessage"/> objects formatted for
        /// the <see cref="PortDIOToggle"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<Timestamped<PortDigitalIOS>> source)
        {
            return source.Select(payload => FromPayload(payload.Seconds, MessageType, payload.Value));
        }
    }

    /// <summary>
    /// Represents an operator that filters and selects a sequence of timestamped messages
    /// from the PortDIOToggle register.
    /// </summary>
    [Description("Filters and selects timestamped messages from the PortDIOToggle register.")]
    public partial class TimestampedPortDIOToggle : HarpCombinator
    {
        /// <summary>
        /// Filters and selects an observable sequence of timestamped messages from
        /// the <see cref="PortDIOToggle"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of timestamped <see cref="PortDigitalIOS"/> objects
        /// representing the register payload.
        /// </returns>
        public IObservable<Timestamped<PortDigitalIOS>> Process(IObservable<HarpMessage> source)
        {
            return source.Where(PortDIOToggle.Address, MessageType).Select(PortDIOToggle.GetTimestampedPayload);
        }
    }

    /// <summary>
    /// Represents an operator that write the state of all port DIO lines.
    /// </summary>
    [Description("Write the state of all port DIO lines")]
    public partial class PortDIOState : HarpCombinator
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

        /// <summary>
        /// Filters and selects an observable sequence of messages from the
        /// <see cref="PortDIOState"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of <see cref="PortDigitalIOS"/> objects representing the
        /// message payload.
        /// </returns>
        public IObservable<PortDigitalIOS> Process(IObservable<HarpMessage> source)
        {
            return source.Where(Address, MessageType).Select(GetPayload);
        }

        /// <summary>
        /// Formats an observable sequence of values into Harp messages
        /// for the <see cref="PortDIOState"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of <see cref="PortDigitalIOS"/> objects representing the
        /// message payload.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects formatted for the
        /// <see cref="PortDIOState"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<PortDigitalIOS> source)
        {
            return source.Select(value => FromPayload(MessageType, value));
        }

        /// <summary>
        /// Formats an observable sequence of values into timestamped Harp messages
        /// for the <see cref="PortDIOState"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of timestamped <see cref="PortDigitalIOS"/> objects representing
        /// the message payload.
        /// </param>
        /// <returns>
        /// A sequence of timestamped <see cref="HarpMessage"/> objects formatted for
        /// the <see cref="PortDIOState"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<Timestamped<PortDigitalIOS>> source)
        {
            return source.Select(payload => FromPayload(payload.Seconds, MessageType, payload.Value));
        }
    }

    /// <summary>
    /// Represents an operator that filters and selects a sequence of timestamped messages
    /// from the PortDIOState register.
    /// </summary>
    [Description("Filters and selects timestamped messages from the PortDIOState register.")]
    public partial class TimestampedPortDIOState : HarpCombinator
    {
        /// <summary>
        /// Filters and selects an observable sequence of timestamped messages from
        /// the <see cref="PortDIOState"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of timestamped <see cref="PortDigitalIOS"/> objects
        /// representing the register payload.
        /// </returns>
        public IObservable<Timestamped<PortDigitalIOS>> Process(IObservable<HarpMessage> source)
        {
            return source.Where(PortDIOState.Address, MessageType).Select(PortDIOState.GetTimestampedPayload);
        }
    }

    /// <summary>
    /// Represents an operator that specifies which of the port DIO lines are outputs.
    /// </summary>
    [Description("Specifies which of the port DIO lines are outputs")]
    public partial class PortDIODirection : HarpCombinator
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

        /// <summary>
        /// Filters and selects an observable sequence of messages from the
        /// <see cref="PortDIODirection"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of <see cref="PortDigitalIOS"/> objects representing the
        /// message payload.
        /// </returns>
        public IObservable<PortDigitalIOS> Process(IObservable<HarpMessage> source)
        {
            return source.Where(Address, MessageType).Select(GetPayload);
        }

        /// <summary>
        /// Formats an observable sequence of values into Harp messages
        /// for the <see cref="PortDIODirection"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of <see cref="PortDigitalIOS"/> objects representing the
        /// message payload.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects formatted for the
        /// <see cref="PortDIODirection"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<PortDigitalIOS> source)
        {
            return source.Select(value => FromPayload(MessageType, value));
        }

        /// <summary>
        /// Formats an observable sequence of values into timestamped Harp messages
        /// for the <see cref="PortDIODirection"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of timestamped <see cref="PortDigitalIOS"/> objects representing
        /// the message payload.
        /// </param>
        /// <returns>
        /// A sequence of timestamped <see cref="HarpMessage"/> objects formatted for
        /// the <see cref="PortDIODirection"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<Timestamped<PortDigitalIOS>> source)
        {
            return source.Select(payload => FromPayload(payload.Seconds, MessageType, payload.Value));
        }
    }

    /// <summary>
    /// Represents an operator that filters and selects a sequence of timestamped messages
    /// from the PortDIODirection register.
    /// </summary>
    [Description("Filters and selects timestamped messages from the PortDIODirection register.")]
    public partial class TimestampedPortDIODirection : HarpCombinator
    {
        /// <summary>
        /// Filters and selects an observable sequence of timestamped messages from
        /// the <see cref="PortDIODirection"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of timestamped <see cref="PortDigitalIOS"/> objects
        /// representing the register payload.
        /// </returns>
        public IObservable<Timestamped<PortDigitalIOS>> Process(IObservable<HarpMessage> source)
        {
            return source.Where(PortDIODirection.Address, MessageType).Select(PortDIODirection.GetTimestampedPayload);
        }
    }

    /// <summary>
    /// Represents an operator that specifies the state of the port DIO lines on a line change.
    /// </summary>
    [Description("Specifies the state of the port DIO lines on a line change")]
    public partial class PortDIOStateEvent : HarpCombinator
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
        /// Initializes a new instance of the <see cref="PortDIOStateEvent"/> class.
        /// </summary>
        public PortDIOStateEvent()
        {
            MessageType = MessageType.Event;
        }

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

        /// <summary>
        /// Filters and selects an observable sequence of messages from the
        /// <see cref="PortDIOStateEvent"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of <see cref="PortDigitalIOS"/> objects representing the
        /// message payload.
        /// </returns>
        public IObservable<PortDigitalIOS> Process(IObservable<HarpMessage> source)
        {
            return source.Where(Address, MessageType).Select(GetPayload);
        }

        /// <summary>
        /// Formats an observable sequence of values into Harp messages
        /// for the <see cref="PortDIOStateEvent"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of <see cref="PortDigitalIOS"/> objects representing the
        /// message payload.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects formatted for the
        /// <see cref="PortDIOStateEvent"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<PortDigitalIOS> source)
        {
            return source.Select(value => FromPayload(MessageType, value));
        }

        /// <summary>
        /// Formats an observable sequence of values into timestamped Harp messages
        /// for the <see cref="PortDIOStateEvent"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of timestamped <see cref="PortDigitalIOS"/> objects representing
        /// the message payload.
        /// </param>
        /// <returns>
        /// A sequence of timestamped <see cref="HarpMessage"/> objects formatted for
        /// the <see cref="PortDIOStateEvent"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<Timestamped<PortDigitalIOS>> source)
        {
            return source.Select(payload => FromPayload(payload.Seconds, MessageType, payload.Value));
        }
    }

    /// <summary>
    /// Represents an operator that filters and selects a sequence of timestamped messages
    /// from the PortDIOStateEvent register.
    /// </summary>
    [Description("Filters and selects timestamped messages from the PortDIOStateEvent register.")]
    public partial class TimestampedPortDIOStateEvent : HarpCombinator
    {
        /// <summary>
        /// Filters and selects an observable sequence of timestamped messages from
        /// the <see cref="PortDIOStateEvent"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of timestamped <see cref="PortDigitalIOS"/> objects
        /// representing the register payload.
        /// </returns>
        public IObservable<Timestamped<PortDigitalIOS>> Process(IObservable<HarpMessage> source)
        {
            return source.Where(PortDIOStateEvent.Address, MessageType).Select(PortDIOStateEvent.GetTimestampedPayload);
        }
    }

    /// <summary>
    /// Represents an operator that voltage at the ADC input and encoder value on Port 2.
    /// </summary>
    [Description("Voltage at the ADC input and encoder value on Port 2")]
    public partial class AnalogData : HarpCombinator
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
        public const int RegisterLength = 2;

        /// <summary>
        /// Initializes a new instance of the <see cref="AnalogData"/> class.
        /// </summary>
        public AnalogData()
        {
            MessageType = MessageType.Event;
        }

        static AnalogDataPayload ParsePayload(short[] payload)
        {
            AnalogDataPayload result;
            result.AnalogInput = payload[0];
            result.Encoder = payload[1];
            return result;
        }

        static short[] FormatPayload(AnalogDataPayload value)
        {
            short[] result;
            result = new short[2];
            result[0] = value.AnalogInput;
            result[1] = value.Encoder;
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

        /// <summary>
        /// Filters and selects an observable sequence of messages from the
        /// <see cref="AnalogData"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of <see cref="AnalogDataPayload"/> objects representing the
        /// message payload.
        /// </returns>
        public IObservable<AnalogDataPayload> Process(IObservable<HarpMessage> source)
        {
            return source.Where(Address, MessageType).Select(GetPayload);
        }

        /// <summary>
        /// Formats an observable sequence of values into Harp messages
        /// for the <see cref="AnalogData"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of <see cref="AnalogDataPayload"/> objects representing the
        /// message payload.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects formatted for the
        /// <see cref="AnalogData"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<AnalogDataPayload> source)
        {
            return source.Select(value => FromPayload(MessageType, value));
        }

        /// <summary>
        /// Formats an observable sequence of values into timestamped Harp messages
        /// for the <see cref="AnalogData"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of timestamped <see cref="AnalogDataPayload"/> objects representing
        /// the message payload.
        /// </param>
        /// <returns>
        /// A sequence of timestamped <see cref="HarpMessage"/> objects formatted for
        /// the <see cref="AnalogData"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<Timestamped<AnalogDataPayload>> source)
        {
            return source.Select(payload => FromPayload(payload.Seconds, MessageType, payload.Value));
        }
    }

    /// <summary>
    /// Represents an operator that filters and selects a sequence of timestamped messages
    /// from the AnalogData register.
    /// </summary>
    [Description("Filters and selects timestamped messages from the AnalogData register.")]
    public partial class TimestampedAnalogData : HarpCombinator
    {
        /// <summary>
        /// Filters and selects an observable sequence of timestamped messages from
        /// the <see cref="AnalogData"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of timestamped <see cref="AnalogDataPayload"/> objects
        /// representing the register payload.
        /// </returns>
        public IObservable<Timestamped<AnalogDataPayload>> Process(IObservable<HarpMessage> source)
        {
            return source.Where(AnalogData.Address, MessageType).Select(AnalogData.GetTimestampedPayload);
        }
    }

    /// <summary>
    /// Represents an operator that enables the pulse function for the specified output lines.
    /// </summary>
    [Description("Enables the pulse function for the specified output lines")]
    public partial class OutputPulseEnable : HarpCombinator
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

        /// <summary>
        /// Filters and selects an observable sequence of messages from the
        /// <see cref="OutputPulseEnable"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of <see cref="DigitalOutputs"/> objects representing the
        /// message payload.
        /// </returns>
        public IObservable<DigitalOutputs> Process(IObservable<HarpMessage> source)
        {
            return source.Where(Address, MessageType).Select(GetPayload);
        }

        /// <summary>
        /// Formats an observable sequence of values into Harp messages
        /// for the <see cref="OutputPulseEnable"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of <see cref="DigitalOutputs"/> objects representing the
        /// message payload.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects formatted for the
        /// <see cref="OutputPulseEnable"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<DigitalOutputs> source)
        {
            return source.Select(value => FromPayload(MessageType, value));
        }

        /// <summary>
        /// Formats an observable sequence of values into timestamped Harp messages
        /// for the <see cref="OutputPulseEnable"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of timestamped <see cref="DigitalOutputs"/> objects representing
        /// the message payload.
        /// </param>
        /// <returns>
        /// A sequence of timestamped <see cref="HarpMessage"/> objects formatted for
        /// the <see cref="OutputPulseEnable"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<Timestamped<DigitalOutputs>> source)
        {
            return source.Select(payload => FromPayload(payload.Seconds, MessageType, payload.Value));
        }
    }

    /// <summary>
    /// Represents an operator that filters and selects a sequence of timestamped messages
    /// from the OutputPulseEnable register.
    /// </summary>
    [Description("Filters and selects timestamped messages from the OutputPulseEnable register.")]
    public partial class TimestampedOutputPulseEnable : HarpCombinator
    {
        /// <summary>
        /// Filters and selects an observable sequence of timestamped messages from
        /// the <see cref="OutputPulseEnable"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of timestamped <see cref="DigitalOutputs"/> objects
        /// representing the register payload.
        /// </returns>
        public IObservable<Timestamped<DigitalOutputs>> Process(IObservable<HarpMessage> source)
        {
            return source.Where(OutputPulseEnable.Address, MessageType).Select(OutputPulseEnable.GetTimestampedPayload);
        }
    }

    /// <summary>
    /// Represents an operator that specifies the duration of the output pulse in milliseconds.
    /// </summary>
    [Description("Specifies the duration of the output pulse in milliseconds.")]
    public partial class PulseDOPort0 : HarpCombinator
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

        /// <summary>
        /// Filters and selects an observable sequence of messages from the
        /// <see cref="PulseDOPort0"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of <see cref="ushort"/> objects representing the
        /// message payload.
        /// </returns>
        public IObservable<ushort> Process(IObservable<HarpMessage> source)
        {
            return source.Where(Address, MessageType).Select(GetPayload);
        }

        /// <summary>
        /// Formats an observable sequence of values into Harp messages
        /// for the <see cref="PulseDOPort0"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of <see cref="ushort"/> objects representing the
        /// message payload.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects formatted for the
        /// <see cref="PulseDOPort0"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<ushort> source)
        {
            return source.Select(value => FromPayload(MessageType, value));
        }

        /// <summary>
        /// Formats an observable sequence of values into timestamped Harp messages
        /// for the <see cref="PulseDOPort0"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of timestamped <see cref="ushort"/> objects representing
        /// the message payload.
        /// </param>
        /// <returns>
        /// A sequence of timestamped <see cref="HarpMessage"/> objects formatted for
        /// the <see cref="PulseDOPort0"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<Timestamped<ushort>> source)
        {
            return source.Select(payload => FromPayload(payload.Seconds, MessageType, payload.Value));
        }
    }

    /// <summary>
    /// Represents an operator that filters and selects a sequence of timestamped messages
    /// from the PulseDOPort0 register.
    /// </summary>
    [Description("Filters and selects timestamped messages from the PulseDOPort0 register.")]
    public partial class TimestampedPulseDOPort0 : HarpCombinator
    {
        /// <summary>
        /// Filters and selects an observable sequence of timestamped messages from
        /// the <see cref="PulseDOPort0"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of timestamped <see cref="ushort"/> objects
        /// representing the register payload.
        /// </returns>
        public IObservable<Timestamped<ushort>> Process(IObservable<HarpMessage> source)
        {
            return source.Where(PulseDOPort0.Address, MessageType).Select(PulseDOPort0.GetTimestampedPayload);
        }
    }

    /// <summary>
    /// Represents an operator that specifies the duration of the output pulse in milliseconds.
    /// </summary>
    [Description("Specifies the duration of the output pulse in milliseconds.")]
    public partial class PulseDOPort1 : HarpCombinator
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

        /// <summary>
        /// Filters and selects an observable sequence of messages from the
        /// <see cref="PulseDOPort1"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of <see cref="ushort"/> objects representing the
        /// message payload.
        /// </returns>
        public IObservable<ushort> Process(IObservable<HarpMessage> source)
        {
            return source.Where(Address, MessageType).Select(GetPayload);
        }

        /// <summary>
        /// Formats an observable sequence of values into Harp messages
        /// for the <see cref="PulseDOPort1"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of <see cref="ushort"/> objects representing the
        /// message payload.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects formatted for the
        /// <see cref="PulseDOPort1"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<ushort> source)
        {
            return source.Select(value => FromPayload(MessageType, value));
        }

        /// <summary>
        /// Formats an observable sequence of values into timestamped Harp messages
        /// for the <see cref="PulseDOPort1"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of timestamped <see cref="ushort"/> objects representing
        /// the message payload.
        /// </param>
        /// <returns>
        /// A sequence of timestamped <see cref="HarpMessage"/> objects formatted for
        /// the <see cref="PulseDOPort1"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<Timestamped<ushort>> source)
        {
            return source.Select(payload => FromPayload(payload.Seconds, MessageType, payload.Value));
        }
    }

    /// <summary>
    /// Represents an operator that filters and selects a sequence of timestamped messages
    /// from the PulseDOPort1 register.
    /// </summary>
    [Description("Filters and selects timestamped messages from the PulseDOPort1 register.")]
    public partial class TimestampedPulseDOPort1 : HarpCombinator
    {
        /// <summary>
        /// Filters and selects an observable sequence of timestamped messages from
        /// the <see cref="PulseDOPort1"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of timestamped <see cref="ushort"/> objects
        /// representing the register payload.
        /// </returns>
        public IObservable<Timestamped<ushort>> Process(IObservable<HarpMessage> source)
        {
            return source.Where(PulseDOPort1.Address, MessageType).Select(PulseDOPort1.GetTimestampedPayload);
        }
    }

    /// <summary>
    /// Represents an operator that specifies the duration of the output pulse in milliseconds.
    /// </summary>
    [Description("Specifies the duration of the output pulse in milliseconds.")]
    public partial class PulseDOPort2 : HarpCombinator
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

        /// <summary>
        /// Filters and selects an observable sequence of messages from the
        /// <see cref="PulseDOPort2"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of <see cref="ushort"/> objects representing the
        /// message payload.
        /// </returns>
        public IObservable<ushort> Process(IObservable<HarpMessage> source)
        {
            return source.Where(Address, MessageType).Select(GetPayload);
        }

        /// <summary>
        /// Formats an observable sequence of values into Harp messages
        /// for the <see cref="PulseDOPort2"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of <see cref="ushort"/> objects representing the
        /// message payload.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects formatted for the
        /// <see cref="PulseDOPort2"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<ushort> source)
        {
            return source.Select(value => FromPayload(MessageType, value));
        }

        /// <summary>
        /// Formats an observable sequence of values into timestamped Harp messages
        /// for the <see cref="PulseDOPort2"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of timestamped <see cref="ushort"/> objects representing
        /// the message payload.
        /// </param>
        /// <returns>
        /// A sequence of timestamped <see cref="HarpMessage"/> objects formatted for
        /// the <see cref="PulseDOPort2"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<Timestamped<ushort>> source)
        {
            return source.Select(payload => FromPayload(payload.Seconds, MessageType, payload.Value));
        }
    }

    /// <summary>
    /// Represents an operator that filters and selects a sequence of timestamped messages
    /// from the PulseDOPort2 register.
    /// </summary>
    [Description("Filters and selects timestamped messages from the PulseDOPort2 register.")]
    public partial class TimestampedPulseDOPort2 : HarpCombinator
    {
        /// <summary>
        /// Filters and selects an observable sequence of timestamped messages from
        /// the <see cref="PulseDOPort2"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of timestamped <see cref="ushort"/> objects
        /// representing the register payload.
        /// </returns>
        public IObservable<Timestamped<ushort>> Process(IObservable<HarpMessage> source)
        {
            return source.Where(PulseDOPort2.Address, MessageType).Select(PulseDOPort2.GetTimestampedPayload);
        }
    }

    /// <summary>
    /// Represents an operator that specifies the duration of the output pulse in milliseconds.
    /// </summary>
    [Description("Specifies the duration of the output pulse in milliseconds.")]
    public partial class PulseSupplyPort0 : HarpCombinator
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

        /// <summary>
        /// Filters and selects an observable sequence of messages from the
        /// <see cref="PulseSupplyPort0"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of <see cref="ushort"/> objects representing the
        /// message payload.
        /// </returns>
        public IObservable<ushort> Process(IObservable<HarpMessage> source)
        {
            return source.Where(Address, MessageType).Select(GetPayload);
        }

        /// <summary>
        /// Formats an observable sequence of values into Harp messages
        /// for the <see cref="PulseSupplyPort0"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of <see cref="ushort"/> objects representing the
        /// message payload.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects formatted for the
        /// <see cref="PulseSupplyPort0"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<ushort> source)
        {
            return source.Select(value => FromPayload(MessageType, value));
        }

        /// <summary>
        /// Formats an observable sequence of values into timestamped Harp messages
        /// for the <see cref="PulseSupplyPort0"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of timestamped <see cref="ushort"/> objects representing
        /// the message payload.
        /// </param>
        /// <returns>
        /// A sequence of timestamped <see cref="HarpMessage"/> objects formatted for
        /// the <see cref="PulseSupplyPort0"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<Timestamped<ushort>> source)
        {
            return source.Select(payload => FromPayload(payload.Seconds, MessageType, payload.Value));
        }
    }

    /// <summary>
    /// Represents an operator that filters and selects a sequence of timestamped messages
    /// from the PulseSupplyPort0 register.
    /// </summary>
    [Description("Filters and selects timestamped messages from the PulseSupplyPort0 register.")]
    public partial class TimestampedPulseSupplyPort0 : HarpCombinator
    {
        /// <summary>
        /// Filters and selects an observable sequence of timestamped messages from
        /// the <see cref="PulseSupplyPort0"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of timestamped <see cref="ushort"/> objects
        /// representing the register payload.
        /// </returns>
        public IObservable<Timestamped<ushort>> Process(IObservable<HarpMessage> source)
        {
            return source.Where(PulseSupplyPort0.Address, MessageType).Select(PulseSupplyPort0.GetTimestampedPayload);
        }
    }

    /// <summary>
    /// Represents an operator that specifies the duration of the output pulse in milliseconds.
    /// </summary>
    [Description("Specifies the duration of the output pulse in milliseconds.")]
    public partial class PulseSupplyPort1 : HarpCombinator
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

        /// <summary>
        /// Filters and selects an observable sequence of messages from the
        /// <see cref="PulseSupplyPort1"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of <see cref="ushort"/> objects representing the
        /// message payload.
        /// </returns>
        public IObservable<ushort> Process(IObservable<HarpMessage> source)
        {
            return source.Where(Address, MessageType).Select(GetPayload);
        }

        /// <summary>
        /// Formats an observable sequence of values into Harp messages
        /// for the <see cref="PulseSupplyPort1"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of <see cref="ushort"/> objects representing the
        /// message payload.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects formatted for the
        /// <see cref="PulseSupplyPort1"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<ushort> source)
        {
            return source.Select(value => FromPayload(MessageType, value));
        }

        /// <summary>
        /// Formats an observable sequence of values into timestamped Harp messages
        /// for the <see cref="PulseSupplyPort1"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of timestamped <see cref="ushort"/> objects representing
        /// the message payload.
        /// </param>
        /// <returns>
        /// A sequence of timestamped <see cref="HarpMessage"/> objects formatted for
        /// the <see cref="PulseSupplyPort1"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<Timestamped<ushort>> source)
        {
            return source.Select(payload => FromPayload(payload.Seconds, MessageType, payload.Value));
        }
    }

    /// <summary>
    /// Represents an operator that filters and selects a sequence of timestamped messages
    /// from the PulseSupplyPort1 register.
    /// </summary>
    [Description("Filters and selects timestamped messages from the PulseSupplyPort1 register.")]
    public partial class TimestampedPulseSupplyPort1 : HarpCombinator
    {
        /// <summary>
        /// Filters and selects an observable sequence of timestamped messages from
        /// the <see cref="PulseSupplyPort1"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of timestamped <see cref="ushort"/> objects
        /// representing the register payload.
        /// </returns>
        public IObservable<Timestamped<ushort>> Process(IObservable<HarpMessage> source)
        {
            return source.Where(PulseSupplyPort1.Address, MessageType).Select(PulseSupplyPort1.GetTimestampedPayload);
        }
    }

    /// <summary>
    /// Represents an operator that specifies the duration of the output pulse in milliseconds.
    /// </summary>
    [Description("Specifies the duration of the output pulse in milliseconds.")]
    public partial class PulseSupplyPort2 : HarpCombinator
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

        /// <summary>
        /// Filters and selects an observable sequence of messages from the
        /// <see cref="PulseSupplyPort2"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of <see cref="ushort"/> objects representing the
        /// message payload.
        /// </returns>
        public IObservable<ushort> Process(IObservable<HarpMessage> source)
        {
            return source.Where(Address, MessageType).Select(GetPayload);
        }

        /// <summary>
        /// Formats an observable sequence of values into Harp messages
        /// for the <see cref="PulseSupplyPort2"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of <see cref="ushort"/> objects representing the
        /// message payload.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects formatted for the
        /// <see cref="PulseSupplyPort2"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<ushort> source)
        {
            return source.Select(value => FromPayload(MessageType, value));
        }

        /// <summary>
        /// Formats an observable sequence of values into timestamped Harp messages
        /// for the <see cref="PulseSupplyPort2"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of timestamped <see cref="ushort"/> objects representing
        /// the message payload.
        /// </param>
        /// <returns>
        /// A sequence of timestamped <see cref="HarpMessage"/> objects formatted for
        /// the <see cref="PulseSupplyPort2"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<Timestamped<ushort>> source)
        {
            return source.Select(payload => FromPayload(payload.Seconds, MessageType, payload.Value));
        }
    }

    /// <summary>
    /// Represents an operator that filters and selects a sequence of timestamped messages
    /// from the PulseSupplyPort2 register.
    /// </summary>
    [Description("Filters and selects timestamped messages from the PulseSupplyPort2 register.")]
    public partial class TimestampedPulseSupplyPort2 : HarpCombinator
    {
        /// <summary>
        /// Filters and selects an observable sequence of timestamped messages from
        /// the <see cref="PulseSupplyPort2"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of timestamped <see cref="ushort"/> objects
        /// representing the register payload.
        /// </returns>
        public IObservable<Timestamped<ushort>> Process(IObservable<HarpMessage> source)
        {
            return source.Where(PulseSupplyPort2.Address, MessageType).Select(PulseSupplyPort2.GetTimestampedPayload);
        }
    }

    /// <summary>
    /// Represents an operator that specifies the duration of the output pulse in milliseconds.
    /// </summary>
    [Description("Specifies the duration of the output pulse in milliseconds.")]
    public partial class PulseLed0 : HarpCombinator
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

        /// <summary>
        /// Filters and selects an observable sequence of messages from the
        /// <see cref="PulseLed0"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of <see cref="ushort"/> objects representing the
        /// message payload.
        /// </returns>
        public IObservable<ushort> Process(IObservable<HarpMessage> source)
        {
            return source.Where(Address, MessageType).Select(GetPayload);
        }

        /// <summary>
        /// Formats an observable sequence of values into Harp messages
        /// for the <see cref="PulseLed0"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of <see cref="ushort"/> objects representing the
        /// message payload.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects formatted for the
        /// <see cref="PulseLed0"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<ushort> source)
        {
            return source.Select(value => FromPayload(MessageType, value));
        }

        /// <summary>
        /// Formats an observable sequence of values into timestamped Harp messages
        /// for the <see cref="PulseLed0"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of timestamped <see cref="ushort"/> objects representing
        /// the message payload.
        /// </param>
        /// <returns>
        /// A sequence of timestamped <see cref="HarpMessage"/> objects formatted for
        /// the <see cref="PulseLed0"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<Timestamped<ushort>> source)
        {
            return source.Select(payload => FromPayload(payload.Seconds, MessageType, payload.Value));
        }
    }

    /// <summary>
    /// Represents an operator that filters and selects a sequence of timestamped messages
    /// from the PulseLed0 register.
    /// </summary>
    [Description("Filters and selects timestamped messages from the PulseLed0 register.")]
    public partial class TimestampedPulseLed0 : HarpCombinator
    {
        /// <summary>
        /// Filters and selects an observable sequence of timestamped messages from
        /// the <see cref="PulseLed0"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of timestamped <see cref="ushort"/> objects
        /// representing the register payload.
        /// </returns>
        public IObservable<Timestamped<ushort>> Process(IObservable<HarpMessage> source)
        {
            return source.Where(PulseLed0.Address, MessageType).Select(PulseLed0.GetTimestampedPayload);
        }
    }

    /// <summary>
    /// Represents an operator that specifies the duration of the output pulse in milliseconds.
    /// </summary>
    [Description("Specifies the duration of the output pulse in milliseconds.")]
    public partial class PulseLed1 : HarpCombinator
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

        /// <summary>
        /// Filters and selects an observable sequence of messages from the
        /// <see cref="PulseLed1"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of <see cref="ushort"/> objects representing the
        /// message payload.
        /// </returns>
        public IObservable<ushort> Process(IObservable<HarpMessage> source)
        {
            return source.Where(Address, MessageType).Select(GetPayload);
        }

        /// <summary>
        /// Formats an observable sequence of values into Harp messages
        /// for the <see cref="PulseLed1"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of <see cref="ushort"/> objects representing the
        /// message payload.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects formatted for the
        /// <see cref="PulseLed1"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<ushort> source)
        {
            return source.Select(value => FromPayload(MessageType, value));
        }

        /// <summary>
        /// Formats an observable sequence of values into timestamped Harp messages
        /// for the <see cref="PulseLed1"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of timestamped <see cref="ushort"/> objects representing
        /// the message payload.
        /// </param>
        /// <returns>
        /// A sequence of timestamped <see cref="HarpMessage"/> objects formatted for
        /// the <see cref="PulseLed1"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<Timestamped<ushort>> source)
        {
            return source.Select(payload => FromPayload(payload.Seconds, MessageType, payload.Value));
        }
    }

    /// <summary>
    /// Represents an operator that filters and selects a sequence of timestamped messages
    /// from the PulseLed1 register.
    /// </summary>
    [Description("Filters and selects timestamped messages from the PulseLed1 register.")]
    public partial class TimestampedPulseLed1 : HarpCombinator
    {
        /// <summary>
        /// Filters and selects an observable sequence of timestamped messages from
        /// the <see cref="PulseLed1"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of timestamped <see cref="ushort"/> objects
        /// representing the register payload.
        /// </returns>
        public IObservable<Timestamped<ushort>> Process(IObservable<HarpMessage> source)
        {
            return source.Where(PulseLed1.Address, MessageType).Select(PulseLed1.GetTimestampedPayload);
        }
    }

    /// <summary>
    /// Represents an operator that specifies the duration of the output pulse in milliseconds.
    /// </summary>
    [Description("Specifies the duration of the output pulse in milliseconds.")]
    public partial class PulseRgb0 : HarpCombinator
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

        /// <summary>
        /// Filters and selects an observable sequence of messages from the
        /// <see cref="PulseRgb0"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of <see cref="ushort"/> objects representing the
        /// message payload.
        /// </returns>
        public IObservable<ushort> Process(IObservable<HarpMessage> source)
        {
            return source.Where(Address, MessageType).Select(GetPayload);
        }

        /// <summary>
        /// Formats an observable sequence of values into Harp messages
        /// for the <see cref="PulseRgb0"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of <see cref="ushort"/> objects representing the
        /// message payload.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects formatted for the
        /// <see cref="PulseRgb0"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<ushort> source)
        {
            return source.Select(value => FromPayload(MessageType, value));
        }

        /// <summary>
        /// Formats an observable sequence of values into timestamped Harp messages
        /// for the <see cref="PulseRgb0"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of timestamped <see cref="ushort"/> objects representing
        /// the message payload.
        /// </param>
        /// <returns>
        /// A sequence of timestamped <see cref="HarpMessage"/> objects formatted for
        /// the <see cref="PulseRgb0"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<Timestamped<ushort>> source)
        {
            return source.Select(payload => FromPayload(payload.Seconds, MessageType, payload.Value));
        }
    }

    /// <summary>
    /// Represents an operator that filters and selects a sequence of timestamped messages
    /// from the PulseRgb0 register.
    /// </summary>
    [Description("Filters and selects timestamped messages from the PulseRgb0 register.")]
    public partial class TimestampedPulseRgb0 : HarpCombinator
    {
        /// <summary>
        /// Filters and selects an observable sequence of timestamped messages from
        /// the <see cref="PulseRgb0"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of timestamped <see cref="ushort"/> objects
        /// representing the register payload.
        /// </returns>
        public IObservable<Timestamped<ushort>> Process(IObservable<HarpMessage> source)
        {
            return source.Where(PulseRgb0.Address, MessageType).Select(PulseRgb0.GetTimestampedPayload);
        }
    }

    /// <summary>
    /// Represents an operator that specifies the duration of the output pulse in milliseconds.
    /// </summary>
    [Description("Specifies the duration of the output pulse in milliseconds.")]
    public partial class PulseRgb1 : HarpCombinator
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

        /// <summary>
        /// Filters and selects an observable sequence of messages from the
        /// <see cref="PulseRgb1"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of <see cref="ushort"/> objects representing the
        /// message payload.
        /// </returns>
        public IObservable<ushort> Process(IObservable<HarpMessage> source)
        {
            return source.Where(Address, MessageType).Select(GetPayload);
        }

        /// <summary>
        /// Formats an observable sequence of values into Harp messages
        /// for the <see cref="PulseRgb1"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of <see cref="ushort"/> objects representing the
        /// message payload.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects formatted for the
        /// <see cref="PulseRgb1"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<ushort> source)
        {
            return source.Select(value => FromPayload(MessageType, value));
        }

        /// <summary>
        /// Formats an observable sequence of values into timestamped Harp messages
        /// for the <see cref="PulseRgb1"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of timestamped <see cref="ushort"/> objects representing
        /// the message payload.
        /// </param>
        /// <returns>
        /// A sequence of timestamped <see cref="HarpMessage"/> objects formatted for
        /// the <see cref="PulseRgb1"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<Timestamped<ushort>> source)
        {
            return source.Select(payload => FromPayload(payload.Seconds, MessageType, payload.Value));
        }
    }

    /// <summary>
    /// Represents an operator that filters and selects a sequence of timestamped messages
    /// from the PulseRgb1 register.
    /// </summary>
    [Description("Filters and selects timestamped messages from the PulseRgb1 register.")]
    public partial class TimestampedPulseRgb1 : HarpCombinator
    {
        /// <summary>
        /// Filters and selects an observable sequence of timestamped messages from
        /// the <see cref="PulseRgb1"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of timestamped <see cref="ushort"/> objects
        /// representing the register payload.
        /// </returns>
        public IObservable<Timestamped<ushort>> Process(IObservable<HarpMessage> source)
        {
            return source.Where(PulseRgb1.Address, MessageType).Select(PulseRgb1.GetTimestampedPayload);
        }
    }

    /// <summary>
    /// Represents an operator that specifies the duration of the output pulse in milliseconds.
    /// </summary>
    [Description("Specifies the duration of the output pulse in milliseconds.")]
    public partial class PulseDO0 : HarpCombinator
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

        /// <summary>
        /// Filters and selects an observable sequence of messages from the
        /// <see cref="PulseDO0"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of <see cref="ushort"/> objects representing the
        /// message payload.
        /// </returns>
        public IObservable<ushort> Process(IObservable<HarpMessage> source)
        {
            return source.Where(Address, MessageType).Select(GetPayload);
        }

        /// <summary>
        /// Formats an observable sequence of values into Harp messages
        /// for the <see cref="PulseDO0"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of <see cref="ushort"/> objects representing the
        /// message payload.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects formatted for the
        /// <see cref="PulseDO0"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<ushort> source)
        {
            return source.Select(value => FromPayload(MessageType, value));
        }

        /// <summary>
        /// Formats an observable sequence of values into timestamped Harp messages
        /// for the <see cref="PulseDO0"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of timestamped <see cref="ushort"/> objects representing
        /// the message payload.
        /// </param>
        /// <returns>
        /// A sequence of timestamped <see cref="HarpMessage"/> objects formatted for
        /// the <see cref="PulseDO0"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<Timestamped<ushort>> source)
        {
            return source.Select(payload => FromPayload(payload.Seconds, MessageType, payload.Value));
        }
    }

    /// <summary>
    /// Represents an operator that filters and selects a sequence of timestamped messages
    /// from the PulseDO0 register.
    /// </summary>
    [Description("Filters and selects timestamped messages from the PulseDO0 register.")]
    public partial class TimestampedPulseDO0 : HarpCombinator
    {
        /// <summary>
        /// Filters and selects an observable sequence of timestamped messages from
        /// the <see cref="PulseDO0"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of timestamped <see cref="ushort"/> objects
        /// representing the register payload.
        /// </returns>
        public IObservable<Timestamped<ushort>> Process(IObservable<HarpMessage> source)
        {
            return source.Where(PulseDO0.Address, MessageType).Select(PulseDO0.GetTimestampedPayload);
        }
    }

    /// <summary>
    /// Represents an operator that specifies the duration of the output pulse in milliseconds.
    /// </summary>
    [Description("Specifies the duration of the output pulse in milliseconds.")]
    public partial class PulseDO1 : HarpCombinator
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

        /// <summary>
        /// Filters and selects an observable sequence of messages from the
        /// <see cref="PulseDO1"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of <see cref="ushort"/> objects representing the
        /// message payload.
        /// </returns>
        public IObservable<ushort> Process(IObservable<HarpMessage> source)
        {
            return source.Where(Address, MessageType).Select(GetPayload);
        }

        /// <summary>
        /// Formats an observable sequence of values into Harp messages
        /// for the <see cref="PulseDO1"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of <see cref="ushort"/> objects representing the
        /// message payload.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects formatted for the
        /// <see cref="PulseDO1"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<ushort> source)
        {
            return source.Select(value => FromPayload(MessageType, value));
        }

        /// <summary>
        /// Formats an observable sequence of values into timestamped Harp messages
        /// for the <see cref="PulseDO1"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of timestamped <see cref="ushort"/> objects representing
        /// the message payload.
        /// </param>
        /// <returns>
        /// A sequence of timestamped <see cref="HarpMessage"/> objects formatted for
        /// the <see cref="PulseDO1"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<Timestamped<ushort>> source)
        {
            return source.Select(payload => FromPayload(payload.Seconds, MessageType, payload.Value));
        }
    }

    /// <summary>
    /// Represents an operator that filters and selects a sequence of timestamped messages
    /// from the PulseDO1 register.
    /// </summary>
    [Description("Filters and selects timestamped messages from the PulseDO1 register.")]
    public partial class TimestampedPulseDO1 : HarpCombinator
    {
        /// <summary>
        /// Filters and selects an observable sequence of timestamped messages from
        /// the <see cref="PulseDO1"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of timestamped <see cref="ushort"/> objects
        /// representing the register payload.
        /// </returns>
        public IObservable<Timestamped<ushort>> Process(IObservable<HarpMessage> source)
        {
            return source.Where(PulseDO1.Address, MessageType).Select(PulseDO1.GetTimestampedPayload);
        }
    }

    /// <summary>
    /// Represents an operator that specifies the duration of the output pulse in milliseconds.
    /// </summary>
    [Description("Specifies the duration of the output pulse in milliseconds.")]
    public partial class PulseDO2 : HarpCombinator
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

        /// <summary>
        /// Filters and selects an observable sequence of messages from the
        /// <see cref="PulseDO2"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of <see cref="ushort"/> objects representing the
        /// message payload.
        /// </returns>
        public IObservable<ushort> Process(IObservable<HarpMessage> source)
        {
            return source.Where(Address, MessageType).Select(GetPayload);
        }

        /// <summary>
        /// Formats an observable sequence of values into Harp messages
        /// for the <see cref="PulseDO2"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of <see cref="ushort"/> objects representing the
        /// message payload.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects formatted for the
        /// <see cref="PulseDO2"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<ushort> source)
        {
            return source.Select(value => FromPayload(MessageType, value));
        }

        /// <summary>
        /// Formats an observable sequence of values into timestamped Harp messages
        /// for the <see cref="PulseDO2"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of timestamped <see cref="ushort"/> objects representing
        /// the message payload.
        /// </param>
        /// <returns>
        /// A sequence of timestamped <see cref="HarpMessage"/> objects formatted for
        /// the <see cref="PulseDO2"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<Timestamped<ushort>> source)
        {
            return source.Select(payload => FromPayload(payload.Seconds, MessageType, payload.Value));
        }
    }

    /// <summary>
    /// Represents an operator that filters and selects a sequence of timestamped messages
    /// from the PulseDO2 register.
    /// </summary>
    [Description("Filters and selects timestamped messages from the PulseDO2 register.")]
    public partial class TimestampedPulseDO2 : HarpCombinator
    {
        /// <summary>
        /// Filters and selects an observable sequence of timestamped messages from
        /// the <see cref="PulseDO2"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of timestamped <see cref="ushort"/> objects
        /// representing the register payload.
        /// </returns>
        public IObservable<Timestamped<ushort>> Process(IObservable<HarpMessage> source)
        {
            return source.Where(PulseDO2.Address, MessageType).Select(PulseDO2.GetTimestampedPayload);
        }
    }

    /// <summary>
    /// Represents an operator that specifies the duration of the output pulse in milliseconds.
    /// </summary>
    [Description("Specifies the duration of the output pulse in milliseconds.")]
    public partial class PulseDO3 : HarpCombinator
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

        /// <summary>
        /// Filters and selects an observable sequence of messages from the
        /// <see cref="PulseDO3"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of <see cref="ushort"/> objects representing the
        /// message payload.
        /// </returns>
        public IObservable<ushort> Process(IObservable<HarpMessage> source)
        {
            return source.Where(Address, MessageType).Select(GetPayload);
        }

        /// <summary>
        /// Formats an observable sequence of values into Harp messages
        /// for the <see cref="PulseDO3"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of <see cref="ushort"/> objects representing the
        /// message payload.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects formatted for the
        /// <see cref="PulseDO3"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<ushort> source)
        {
            return source.Select(value => FromPayload(MessageType, value));
        }

        /// <summary>
        /// Formats an observable sequence of values into timestamped Harp messages
        /// for the <see cref="PulseDO3"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of timestamped <see cref="ushort"/> objects representing
        /// the message payload.
        /// </param>
        /// <returns>
        /// A sequence of timestamped <see cref="HarpMessage"/> objects formatted for
        /// the <see cref="PulseDO3"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<Timestamped<ushort>> source)
        {
            return source.Select(payload => FromPayload(payload.Seconds, MessageType, payload.Value));
        }
    }

    /// <summary>
    /// Represents an operator that filters and selects a sequence of timestamped messages
    /// from the PulseDO3 register.
    /// </summary>
    [Description("Filters and selects timestamped messages from the PulseDO3 register.")]
    public partial class TimestampedPulseDO3 : HarpCombinator
    {
        /// <summary>
        /// Filters and selects an observable sequence of timestamped messages from
        /// the <see cref="PulseDO3"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of timestamped <see cref="ushort"/> objects
        /// representing the register payload.
        /// </returns>
        public IObservable<Timestamped<ushort>> Process(IObservable<HarpMessage> source)
        {
            return source.Where(PulseDO3.Address, MessageType).Select(PulseDO3.GetTimestampedPayload);
        }
    }

    /// <summary>
    /// Represents an operator that specifies the frequency of the PWM at DO0.
    /// </summary>
    [Description("Specifies the frequency of the PWM at DO0.")]
    public partial class PwmFrequencyDO0 : HarpCombinator
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

        /// <summary>
        /// Filters and selects an observable sequence of messages from the
        /// <see cref="PwmFrequencyDO0"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of <see cref="ushort"/> objects representing the
        /// message payload.
        /// </returns>
        public IObservable<ushort> Process(IObservable<HarpMessage> source)
        {
            return source.Where(Address, MessageType).Select(GetPayload);
        }

        /// <summary>
        /// Formats an observable sequence of values into Harp messages
        /// for the <see cref="PwmFrequencyDO0"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of <see cref="ushort"/> objects representing the
        /// message payload.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects formatted for the
        /// <see cref="PwmFrequencyDO0"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<ushort> source)
        {
            return source.Select(value => FromPayload(MessageType, value));
        }

        /// <summary>
        /// Formats an observable sequence of values into timestamped Harp messages
        /// for the <see cref="PwmFrequencyDO0"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of timestamped <see cref="ushort"/> objects representing
        /// the message payload.
        /// </param>
        /// <returns>
        /// A sequence of timestamped <see cref="HarpMessage"/> objects formatted for
        /// the <see cref="PwmFrequencyDO0"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<Timestamped<ushort>> source)
        {
            return source.Select(payload => FromPayload(payload.Seconds, MessageType, payload.Value));
        }
    }

    /// <summary>
    /// Represents an operator that filters and selects a sequence of timestamped messages
    /// from the PwmFrequencyDO0 register.
    /// </summary>
    [Description("Filters and selects timestamped messages from the PwmFrequencyDO0 register.")]
    public partial class TimestampedPwmFrequencyDO0 : HarpCombinator
    {
        /// <summary>
        /// Filters and selects an observable sequence of timestamped messages from
        /// the <see cref="PwmFrequencyDO0"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of timestamped <see cref="ushort"/> objects
        /// representing the register payload.
        /// </returns>
        public IObservable<Timestamped<ushort>> Process(IObservable<HarpMessage> source)
        {
            return source.Where(PwmFrequencyDO0.Address, MessageType).Select(PwmFrequencyDO0.GetTimestampedPayload);
        }
    }

    /// <summary>
    /// Represents an operator that specifies the frequency of the PWM at DO1.
    /// </summary>
    [Description("Specifies the frequency of the PWM at DO1.")]
    public partial class PwmFrequencyDO1 : HarpCombinator
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

        /// <summary>
        /// Filters and selects an observable sequence of messages from the
        /// <see cref="PwmFrequencyDO1"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of <see cref="ushort"/> objects representing the
        /// message payload.
        /// </returns>
        public IObservable<ushort> Process(IObservable<HarpMessage> source)
        {
            return source.Where(Address, MessageType).Select(GetPayload);
        }

        /// <summary>
        /// Formats an observable sequence of values into Harp messages
        /// for the <see cref="PwmFrequencyDO1"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of <see cref="ushort"/> objects representing the
        /// message payload.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects formatted for the
        /// <see cref="PwmFrequencyDO1"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<ushort> source)
        {
            return source.Select(value => FromPayload(MessageType, value));
        }

        /// <summary>
        /// Formats an observable sequence of values into timestamped Harp messages
        /// for the <see cref="PwmFrequencyDO1"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of timestamped <see cref="ushort"/> objects representing
        /// the message payload.
        /// </param>
        /// <returns>
        /// A sequence of timestamped <see cref="HarpMessage"/> objects formatted for
        /// the <see cref="PwmFrequencyDO1"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<Timestamped<ushort>> source)
        {
            return source.Select(payload => FromPayload(payload.Seconds, MessageType, payload.Value));
        }
    }

    /// <summary>
    /// Represents an operator that filters and selects a sequence of timestamped messages
    /// from the PwmFrequencyDO1 register.
    /// </summary>
    [Description("Filters and selects timestamped messages from the PwmFrequencyDO1 register.")]
    public partial class TimestampedPwmFrequencyDO1 : HarpCombinator
    {
        /// <summary>
        /// Filters and selects an observable sequence of timestamped messages from
        /// the <see cref="PwmFrequencyDO1"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of timestamped <see cref="ushort"/> objects
        /// representing the register payload.
        /// </returns>
        public IObservable<Timestamped<ushort>> Process(IObservable<HarpMessage> source)
        {
            return source.Where(PwmFrequencyDO1.Address, MessageType).Select(PwmFrequencyDO1.GetTimestampedPayload);
        }
    }

    /// <summary>
    /// Represents an operator that specifies the frequency of the PWM at DO2.
    /// </summary>
    [Description("Specifies the frequency of the PWM at DO2.")]
    public partial class PwmFrequencyDO2 : HarpCombinator
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

        /// <summary>
        /// Filters and selects an observable sequence of messages from the
        /// <see cref="PwmFrequencyDO2"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of <see cref="ushort"/> objects representing the
        /// message payload.
        /// </returns>
        public IObservable<ushort> Process(IObservable<HarpMessage> source)
        {
            return source.Where(Address, MessageType).Select(GetPayload);
        }

        /// <summary>
        /// Formats an observable sequence of values into Harp messages
        /// for the <see cref="PwmFrequencyDO2"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of <see cref="ushort"/> objects representing the
        /// message payload.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects formatted for the
        /// <see cref="PwmFrequencyDO2"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<ushort> source)
        {
            return source.Select(value => FromPayload(MessageType, value));
        }

        /// <summary>
        /// Formats an observable sequence of values into timestamped Harp messages
        /// for the <see cref="PwmFrequencyDO2"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of timestamped <see cref="ushort"/> objects representing
        /// the message payload.
        /// </param>
        /// <returns>
        /// A sequence of timestamped <see cref="HarpMessage"/> objects formatted for
        /// the <see cref="PwmFrequencyDO2"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<Timestamped<ushort>> source)
        {
            return source.Select(payload => FromPayload(payload.Seconds, MessageType, payload.Value));
        }
    }

    /// <summary>
    /// Represents an operator that filters and selects a sequence of timestamped messages
    /// from the PwmFrequencyDO2 register.
    /// </summary>
    [Description("Filters and selects timestamped messages from the PwmFrequencyDO2 register.")]
    public partial class TimestampedPwmFrequencyDO2 : HarpCombinator
    {
        /// <summary>
        /// Filters and selects an observable sequence of timestamped messages from
        /// the <see cref="PwmFrequencyDO2"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of timestamped <see cref="ushort"/> objects
        /// representing the register payload.
        /// </returns>
        public IObservable<Timestamped<ushort>> Process(IObservable<HarpMessage> source)
        {
            return source.Where(PwmFrequencyDO2.Address, MessageType).Select(PwmFrequencyDO2.GetTimestampedPayload);
        }
    }

    /// <summary>
    /// Represents an operator that specifies the frequency of the PWM at DO3.
    /// </summary>
    [Description("Specifies the frequency of the PWM at DO3.")]
    public partial class PwmFrequencyDO3 : HarpCombinator
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

        /// <summary>
        /// Filters and selects an observable sequence of messages from the
        /// <see cref="PwmFrequencyDO3"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of <see cref="ushort"/> objects representing the
        /// message payload.
        /// </returns>
        public IObservable<ushort> Process(IObservable<HarpMessage> source)
        {
            return source.Where(Address, MessageType).Select(GetPayload);
        }

        /// <summary>
        /// Formats an observable sequence of values into Harp messages
        /// for the <see cref="PwmFrequencyDO3"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of <see cref="ushort"/> objects representing the
        /// message payload.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects formatted for the
        /// <see cref="PwmFrequencyDO3"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<ushort> source)
        {
            return source.Select(value => FromPayload(MessageType, value));
        }

        /// <summary>
        /// Formats an observable sequence of values into timestamped Harp messages
        /// for the <see cref="PwmFrequencyDO3"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of timestamped <see cref="ushort"/> objects representing
        /// the message payload.
        /// </param>
        /// <returns>
        /// A sequence of timestamped <see cref="HarpMessage"/> objects formatted for
        /// the <see cref="PwmFrequencyDO3"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<Timestamped<ushort>> source)
        {
            return source.Select(payload => FromPayload(payload.Seconds, MessageType, payload.Value));
        }
    }

    /// <summary>
    /// Represents an operator that filters and selects a sequence of timestamped messages
    /// from the PwmFrequencyDO3 register.
    /// </summary>
    [Description("Filters and selects timestamped messages from the PwmFrequencyDO3 register.")]
    public partial class TimestampedPwmFrequencyDO3 : HarpCombinator
    {
        /// <summary>
        /// Filters and selects an observable sequence of timestamped messages from
        /// the <see cref="PwmFrequencyDO3"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of timestamped <see cref="ushort"/> objects
        /// representing the register payload.
        /// </returns>
        public IObservable<Timestamped<ushort>> Process(IObservable<HarpMessage> source)
        {
            return source.Where(PwmFrequencyDO3.Address, MessageType).Select(PwmFrequencyDO3.GetTimestampedPayload);
        }
    }

    /// <summary>
    /// Represents an operator that specifies the duty cycle of the PWM at DO0.
    /// </summary>
    [Description("Specifies the duty cycle of the PWM at DO0.")]
    public partial class PwmDutyCycleDO0 : HarpCombinator
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

        /// <summary>
        /// Filters and selects an observable sequence of messages from the
        /// <see cref="PwmDutyCycleDO0"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of <see cref="byte"/> objects representing the
        /// message payload.
        /// </returns>
        public IObservable<byte> Process(IObservable<HarpMessage> source)
        {
            return source.Where(Address, MessageType).Select(GetPayload);
        }

        /// <summary>
        /// Formats an observable sequence of values into Harp messages
        /// for the <see cref="PwmDutyCycleDO0"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of <see cref="byte"/> objects representing the
        /// message payload.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects formatted for the
        /// <see cref="PwmDutyCycleDO0"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<byte> source)
        {
            return source.Select(value => FromPayload(MessageType, value));
        }

        /// <summary>
        /// Formats an observable sequence of values into timestamped Harp messages
        /// for the <see cref="PwmDutyCycleDO0"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of timestamped <see cref="byte"/> objects representing
        /// the message payload.
        /// </param>
        /// <returns>
        /// A sequence of timestamped <see cref="HarpMessage"/> objects formatted for
        /// the <see cref="PwmDutyCycleDO0"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<Timestamped<byte>> source)
        {
            return source.Select(payload => FromPayload(payload.Seconds, MessageType, payload.Value));
        }
    }

    /// <summary>
    /// Represents an operator that filters and selects a sequence of timestamped messages
    /// from the PwmDutyCycleDO0 register.
    /// </summary>
    [Description("Filters and selects timestamped messages from the PwmDutyCycleDO0 register.")]
    public partial class TimestampedPwmDutyCycleDO0 : HarpCombinator
    {
        /// <summary>
        /// Filters and selects an observable sequence of timestamped messages from
        /// the <see cref="PwmDutyCycleDO0"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of timestamped <see cref="byte"/> objects
        /// representing the register payload.
        /// </returns>
        public IObservable<Timestamped<byte>> Process(IObservable<HarpMessage> source)
        {
            return source.Where(PwmDutyCycleDO0.Address, MessageType).Select(PwmDutyCycleDO0.GetTimestampedPayload);
        }
    }

    /// <summary>
    /// Represents an operator that specifies the duty cycle of the PWM at DO1.
    /// </summary>
    [Description("Specifies the duty cycle of the PWM at DO1.")]
    public partial class PwmDutyCycleDO1 : HarpCombinator
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

        /// <summary>
        /// Filters and selects an observable sequence of messages from the
        /// <see cref="PwmDutyCycleDO1"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of <see cref="byte"/> objects representing the
        /// message payload.
        /// </returns>
        public IObservable<byte> Process(IObservable<HarpMessage> source)
        {
            return source.Where(Address, MessageType).Select(GetPayload);
        }

        /// <summary>
        /// Formats an observable sequence of values into Harp messages
        /// for the <see cref="PwmDutyCycleDO1"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of <see cref="byte"/> objects representing the
        /// message payload.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects formatted for the
        /// <see cref="PwmDutyCycleDO1"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<byte> source)
        {
            return source.Select(value => FromPayload(MessageType, value));
        }

        /// <summary>
        /// Formats an observable sequence of values into timestamped Harp messages
        /// for the <see cref="PwmDutyCycleDO1"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of timestamped <see cref="byte"/> objects representing
        /// the message payload.
        /// </param>
        /// <returns>
        /// A sequence of timestamped <see cref="HarpMessage"/> objects formatted for
        /// the <see cref="PwmDutyCycleDO1"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<Timestamped<byte>> source)
        {
            return source.Select(payload => FromPayload(payload.Seconds, MessageType, payload.Value));
        }
    }

    /// <summary>
    /// Represents an operator that filters and selects a sequence of timestamped messages
    /// from the PwmDutyCycleDO1 register.
    /// </summary>
    [Description("Filters and selects timestamped messages from the PwmDutyCycleDO1 register.")]
    public partial class TimestampedPwmDutyCycleDO1 : HarpCombinator
    {
        /// <summary>
        /// Filters and selects an observable sequence of timestamped messages from
        /// the <see cref="PwmDutyCycleDO1"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of timestamped <see cref="byte"/> objects
        /// representing the register payload.
        /// </returns>
        public IObservable<Timestamped<byte>> Process(IObservable<HarpMessage> source)
        {
            return source.Where(PwmDutyCycleDO1.Address, MessageType).Select(PwmDutyCycleDO1.GetTimestampedPayload);
        }
    }

    /// <summary>
    /// Represents an operator that specifies the duty cycle of the PWM at DO2.
    /// </summary>
    [Description("Specifies the duty cycle of the PWM at DO2.")]
    public partial class PwmDutyCycleDO2 : HarpCombinator
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

        /// <summary>
        /// Filters and selects an observable sequence of messages from the
        /// <see cref="PwmDutyCycleDO2"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of <see cref="byte"/> objects representing the
        /// message payload.
        /// </returns>
        public IObservable<byte> Process(IObservable<HarpMessage> source)
        {
            return source.Where(Address, MessageType).Select(GetPayload);
        }

        /// <summary>
        /// Formats an observable sequence of values into Harp messages
        /// for the <see cref="PwmDutyCycleDO2"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of <see cref="byte"/> objects representing the
        /// message payload.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects formatted for the
        /// <see cref="PwmDutyCycleDO2"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<byte> source)
        {
            return source.Select(value => FromPayload(MessageType, value));
        }

        /// <summary>
        /// Formats an observable sequence of values into timestamped Harp messages
        /// for the <see cref="PwmDutyCycleDO2"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of timestamped <see cref="byte"/> objects representing
        /// the message payload.
        /// </param>
        /// <returns>
        /// A sequence of timestamped <see cref="HarpMessage"/> objects formatted for
        /// the <see cref="PwmDutyCycleDO2"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<Timestamped<byte>> source)
        {
            return source.Select(payload => FromPayload(payload.Seconds, MessageType, payload.Value));
        }
    }

    /// <summary>
    /// Represents an operator that filters and selects a sequence of timestamped messages
    /// from the PwmDutyCycleDO2 register.
    /// </summary>
    [Description("Filters and selects timestamped messages from the PwmDutyCycleDO2 register.")]
    public partial class TimestampedPwmDutyCycleDO2 : HarpCombinator
    {
        /// <summary>
        /// Filters and selects an observable sequence of timestamped messages from
        /// the <see cref="PwmDutyCycleDO2"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of timestamped <see cref="byte"/> objects
        /// representing the register payload.
        /// </returns>
        public IObservable<Timestamped<byte>> Process(IObservable<HarpMessage> source)
        {
            return source.Where(PwmDutyCycleDO2.Address, MessageType).Select(PwmDutyCycleDO2.GetTimestampedPayload);
        }
    }

    /// <summary>
    /// Represents an operator that specifies the duty cycle of the PWM at DO3.
    /// </summary>
    [Description("Specifies the duty cycle of the PWM at DO3.")]
    public partial class PwmDutyCycleDO3 : HarpCombinator
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

        /// <summary>
        /// Filters and selects an observable sequence of messages from the
        /// <see cref="PwmDutyCycleDO3"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of <see cref="byte"/> objects representing the
        /// message payload.
        /// </returns>
        public IObservable<byte> Process(IObservable<HarpMessage> source)
        {
            return source.Where(Address, MessageType).Select(GetPayload);
        }

        /// <summary>
        /// Formats an observable sequence of values into Harp messages
        /// for the <see cref="PwmDutyCycleDO3"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of <see cref="byte"/> objects representing the
        /// message payload.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects formatted for the
        /// <see cref="PwmDutyCycleDO3"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<byte> source)
        {
            return source.Select(value => FromPayload(MessageType, value));
        }

        /// <summary>
        /// Formats an observable sequence of values into timestamped Harp messages
        /// for the <see cref="PwmDutyCycleDO3"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of timestamped <see cref="byte"/> objects representing
        /// the message payload.
        /// </param>
        /// <returns>
        /// A sequence of timestamped <see cref="HarpMessage"/> objects formatted for
        /// the <see cref="PwmDutyCycleDO3"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<Timestamped<byte>> source)
        {
            return source.Select(payload => FromPayload(payload.Seconds, MessageType, payload.Value));
        }
    }

    /// <summary>
    /// Represents an operator that filters and selects a sequence of timestamped messages
    /// from the PwmDutyCycleDO3 register.
    /// </summary>
    [Description("Filters and selects timestamped messages from the PwmDutyCycleDO3 register.")]
    public partial class TimestampedPwmDutyCycleDO3 : HarpCombinator
    {
        /// <summary>
        /// Filters and selects an observable sequence of timestamped messages from
        /// the <see cref="PwmDutyCycleDO3"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of timestamped <see cref="byte"/> objects
        /// representing the register payload.
        /// </returns>
        public IObservable<Timestamped<byte>> Process(IObservable<HarpMessage> source)
        {
            return source.Where(PwmDutyCycleDO3.Address, MessageType).Select(PwmDutyCycleDO3.GetTimestampedPayload);
        }
    }

    /// <summary>
    /// Represents an operator that starts the PWM on the selected output lines.
    /// </summary>
    [Description("Starts the PWM on the selected output lines.")]
    public partial class PwmStart : HarpCombinator
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

        /// <summary>
        /// Filters and selects an observable sequence of messages from the
        /// <see cref="PwmStart"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of <see cref="PwmOutputs"/> objects representing the
        /// message payload.
        /// </returns>
        public IObservable<PwmOutputs> Process(IObservable<HarpMessage> source)
        {
            return source.Where(Address, MessageType).Select(GetPayload);
        }

        /// <summary>
        /// Formats an observable sequence of values into Harp messages
        /// for the <see cref="PwmStart"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of <see cref="PwmOutputs"/> objects representing the
        /// message payload.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects formatted for the
        /// <see cref="PwmStart"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<PwmOutputs> source)
        {
            return source.Select(value => FromPayload(MessageType, value));
        }

        /// <summary>
        /// Formats an observable sequence of values into timestamped Harp messages
        /// for the <see cref="PwmStart"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of timestamped <see cref="PwmOutputs"/> objects representing
        /// the message payload.
        /// </param>
        /// <returns>
        /// A sequence of timestamped <see cref="HarpMessage"/> objects formatted for
        /// the <see cref="PwmStart"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<Timestamped<PwmOutputs>> source)
        {
            return source.Select(payload => FromPayload(payload.Seconds, MessageType, payload.Value));
        }
    }

    /// <summary>
    /// Represents an operator that filters and selects a sequence of timestamped messages
    /// from the PwmStart register.
    /// </summary>
    [Description("Filters and selects timestamped messages from the PwmStart register.")]
    public partial class TimestampedPwmStart : HarpCombinator
    {
        /// <summary>
        /// Filters and selects an observable sequence of timestamped messages from
        /// the <see cref="PwmStart"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of timestamped <see cref="PwmOutputs"/> objects
        /// representing the register payload.
        /// </returns>
        public IObservable<Timestamped<PwmOutputs>> Process(IObservable<HarpMessage> source)
        {
            return source.Where(PwmStart.Address, MessageType).Select(PwmStart.GetTimestampedPayload);
        }
    }

    /// <summary>
    /// Represents an operator that stops the PWM on the selected output lines.
    /// </summary>
    [Description("Stops the PWM on the selected output lines.")]
    public partial class PwmStop : HarpCombinator
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
        public static byte GetPayload(HarpMessage message)
        {
            return message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="PwmStop"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<byte> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadByte();
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
        public static HarpMessage FromPayload(MessageType messageType, byte value)
        {
            return HarpMessage.FromByte(Address, messageType, value);
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
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, byte value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, value);
        }

        /// <summary>
        /// Filters and selects an observable sequence of messages from the
        /// <see cref="PwmStop"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of <see cref="byte"/> objects representing the
        /// message payload.
        /// </returns>
        public IObservable<byte> Process(IObservable<HarpMessage> source)
        {
            return source.Where(Address, MessageType).Select(GetPayload);
        }

        /// <summary>
        /// Formats an observable sequence of values into Harp messages
        /// for the <see cref="PwmStop"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of <see cref="byte"/> objects representing the
        /// message payload.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects formatted for the
        /// <see cref="PwmStop"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<byte> source)
        {
            return source.Select(value => FromPayload(MessageType, value));
        }

        /// <summary>
        /// Formats an observable sequence of values into timestamped Harp messages
        /// for the <see cref="PwmStop"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of timestamped <see cref="byte"/> objects representing
        /// the message payload.
        /// </param>
        /// <returns>
        /// A sequence of timestamped <see cref="HarpMessage"/> objects formatted for
        /// the <see cref="PwmStop"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<Timestamped<byte>> source)
        {
            return source.Select(payload => FromPayload(payload.Seconds, MessageType, payload.Value));
        }
    }

    /// <summary>
    /// Represents an operator that filters and selects a sequence of timestamped messages
    /// from the PwmStop register.
    /// </summary>
    [Description("Filters and selects timestamped messages from the PwmStop register.")]
    public partial class TimestampedPwmStop : HarpCombinator
    {
        /// <summary>
        /// Filters and selects an observable sequence of timestamped messages from
        /// the <see cref="PwmStop"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of timestamped <see cref="byte"/> objects
        /// representing the register payload.
        /// </returns>
        public IObservable<Timestamped<byte>> Process(IObservable<HarpMessage> source)
        {
            return source.Where(PwmStop.Address, MessageType).Select(PwmStop.GetTimestampedPayload);
        }
    }

    /// <summary>
    /// Represents an operator that specifies the state of all RGB LED channels.
    /// </summary>
    [Description("Specifies the state of all RGB LED channels.")]
    public partial class RgbAll : HarpCombinator
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

        /// <summary>
        /// Filters and selects an observable sequence of messages from the
        /// <see cref="RgbAll"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of <see cref="RgbAllPayload"/> objects representing the
        /// message payload.
        /// </returns>
        public IObservable<RgbAllPayload> Process(IObservable<HarpMessage> source)
        {
            return source.Where(Address, MessageType).Select(GetPayload);
        }

        /// <summary>
        /// Formats an observable sequence of values into Harp messages
        /// for the <see cref="RgbAll"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of <see cref="RgbAllPayload"/> objects representing the
        /// message payload.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects formatted for the
        /// <see cref="RgbAll"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<RgbAllPayload> source)
        {
            return source.Select(value => FromPayload(MessageType, value));
        }

        /// <summary>
        /// Formats an observable sequence of values into timestamped Harp messages
        /// for the <see cref="RgbAll"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of timestamped <see cref="RgbAllPayload"/> objects representing
        /// the message payload.
        /// </param>
        /// <returns>
        /// A sequence of timestamped <see cref="HarpMessage"/> objects formatted for
        /// the <see cref="RgbAll"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<Timestamped<RgbAllPayload>> source)
        {
            return source.Select(payload => FromPayload(payload.Seconds, MessageType, payload.Value));
        }
    }

    /// <summary>
    /// Represents an operator that filters and selects a sequence of timestamped messages
    /// from the RgbAll register.
    /// </summary>
    [Description("Filters and selects timestamped messages from the RgbAll register.")]
    public partial class TimestampedRgbAll : HarpCombinator
    {
        /// <summary>
        /// Filters and selects an observable sequence of timestamped messages from
        /// the <see cref="RgbAll"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of timestamped <see cref="RgbAllPayload"/> objects
        /// representing the register payload.
        /// </returns>
        public IObservable<Timestamped<RgbAllPayload>> Process(IObservable<HarpMessage> source)
        {
            return source.Where(RgbAll.Address, MessageType).Select(RgbAll.GetTimestampedPayload);
        }
    }

    /// <summary>
    /// Represents an operator that specifies the state of the RGB0 LED channels.
    /// </summary>
    [Description("Specifies the state of the RGB0 LED channels.")]
    public partial class Rgb0 : HarpCombinator
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

        /// <summary>
        /// Filters and selects an observable sequence of messages from the
        /// <see cref="Rgb0"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of <see cref="RgbPayload"/> objects representing the
        /// message payload.
        /// </returns>
        public IObservable<RgbPayload> Process(IObservable<HarpMessage> source)
        {
            return source.Where(Address, MessageType).Select(GetPayload);
        }

        /// <summary>
        /// Formats an observable sequence of values into Harp messages
        /// for the <see cref="Rgb0"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of <see cref="RgbPayload"/> objects representing the
        /// message payload.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects formatted for the
        /// <see cref="Rgb0"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<RgbPayload> source)
        {
            return source.Select(value => FromPayload(MessageType, value));
        }

        /// <summary>
        /// Formats an observable sequence of values into timestamped Harp messages
        /// for the <see cref="Rgb0"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of timestamped <see cref="RgbPayload"/> objects representing
        /// the message payload.
        /// </param>
        /// <returns>
        /// A sequence of timestamped <see cref="HarpMessage"/> objects formatted for
        /// the <see cref="Rgb0"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<Timestamped<RgbPayload>> source)
        {
            return source.Select(payload => FromPayload(payload.Seconds, MessageType, payload.Value));
        }
    }

    /// <summary>
    /// Represents an operator that filters and selects a sequence of timestamped messages
    /// from the Rgb0 register.
    /// </summary>
    [Description("Filters and selects timestamped messages from the Rgb0 register.")]
    public partial class TimestampedRgb0 : HarpCombinator
    {
        /// <summary>
        /// Filters and selects an observable sequence of timestamped messages from
        /// the <see cref="Rgb0"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of timestamped <see cref="RgbPayload"/> objects
        /// representing the register payload.
        /// </returns>
        public IObservable<Timestamped<RgbPayload>> Process(IObservable<HarpMessage> source)
        {
            return source.Where(Rgb0.Address, MessageType).Select(Rgb0.GetTimestampedPayload);
        }
    }

    /// <summary>
    /// Represents an operator that specifies the state of the RGB1 LED channels.
    /// </summary>
    [Description("Specifies the state of the RGB1 LED channels.")]
    public partial class Rgb1 : HarpCombinator
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

        /// <summary>
        /// Filters and selects an observable sequence of messages from the
        /// <see cref="Rgb1"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of <see cref="RgbPayload"/> objects representing the
        /// message payload.
        /// </returns>
        public IObservable<RgbPayload> Process(IObservable<HarpMessage> source)
        {
            return source.Where(Address, MessageType).Select(GetPayload);
        }

        /// <summary>
        /// Formats an observable sequence of values into Harp messages
        /// for the <see cref="Rgb1"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of <see cref="RgbPayload"/> objects representing the
        /// message payload.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects formatted for the
        /// <see cref="Rgb1"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<RgbPayload> source)
        {
            return source.Select(value => FromPayload(MessageType, value));
        }

        /// <summary>
        /// Formats an observable sequence of values into timestamped Harp messages
        /// for the <see cref="Rgb1"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of timestamped <see cref="RgbPayload"/> objects representing
        /// the message payload.
        /// </param>
        /// <returns>
        /// A sequence of timestamped <see cref="HarpMessage"/> objects formatted for
        /// the <see cref="Rgb1"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<Timestamped<RgbPayload>> source)
        {
            return source.Select(payload => FromPayload(payload.Seconds, MessageType, payload.Value));
        }
    }

    /// <summary>
    /// Represents an operator that filters and selects a sequence of timestamped messages
    /// from the Rgb1 register.
    /// </summary>
    [Description("Filters and selects timestamped messages from the Rgb1 register.")]
    public partial class TimestampedRgb1 : HarpCombinator
    {
        /// <summary>
        /// Filters and selects an observable sequence of timestamped messages from
        /// the <see cref="Rgb1"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of timestamped <see cref="RgbPayload"/> objects
        /// representing the register payload.
        /// </returns>
        public IObservable<Timestamped<RgbPayload>> Process(IObservable<HarpMessage> source)
        {
            return source.Where(Rgb1.Address, MessageType).Select(Rgb1.GetTimestampedPayload);
        }
    }

    /// <summary>
    /// Represents an operator that specifies the configuration of current to drive LED 0.
    /// </summary>
    [Description("Specifies the configuration of current to drive LED 0.")]
    public partial class Led0Current : HarpCombinator
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

        /// <summary>
        /// Filters and selects an observable sequence of messages from the
        /// <see cref="Led0Current"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of <see cref="byte"/> objects representing the
        /// message payload.
        /// </returns>
        public IObservable<byte> Process(IObservable<HarpMessage> source)
        {
            return source.Where(Address, MessageType).Select(GetPayload);
        }

        /// <summary>
        /// Formats an observable sequence of values into Harp messages
        /// for the <see cref="Led0Current"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of <see cref="byte"/> objects representing the
        /// message payload.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects formatted for the
        /// <see cref="Led0Current"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<byte> source)
        {
            return source.Select(value => FromPayload(MessageType, value));
        }

        /// <summary>
        /// Formats an observable sequence of values into timestamped Harp messages
        /// for the <see cref="Led0Current"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of timestamped <see cref="byte"/> objects representing
        /// the message payload.
        /// </param>
        /// <returns>
        /// A sequence of timestamped <see cref="HarpMessage"/> objects formatted for
        /// the <see cref="Led0Current"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<Timestamped<byte>> source)
        {
            return source.Select(payload => FromPayload(payload.Seconds, MessageType, payload.Value));
        }
    }

    /// <summary>
    /// Represents an operator that filters and selects a sequence of timestamped messages
    /// from the Led0Current register.
    /// </summary>
    [Description("Filters and selects timestamped messages from the Led0Current register.")]
    public partial class TimestampedLed0Current : HarpCombinator
    {
        /// <summary>
        /// Filters and selects an observable sequence of timestamped messages from
        /// the <see cref="Led0Current"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of timestamped <see cref="byte"/> objects
        /// representing the register payload.
        /// </returns>
        public IObservable<Timestamped<byte>> Process(IObservable<HarpMessage> source)
        {
            return source.Where(Led0Current.Address, MessageType).Select(Led0Current.GetTimestampedPayload);
        }
    }

    /// <summary>
    /// Represents an operator that specifies the configuration of current to drive LED 1.
    /// </summary>
    [Description("Specifies the configuration of current to drive LED 1.")]
    public partial class Led1Current : HarpCombinator
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

        /// <summary>
        /// Filters and selects an observable sequence of messages from the
        /// <see cref="Led1Current"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of <see cref="byte"/> objects representing the
        /// message payload.
        /// </returns>
        public IObservable<byte> Process(IObservable<HarpMessage> source)
        {
            return source.Where(Address, MessageType).Select(GetPayload);
        }

        /// <summary>
        /// Formats an observable sequence of values into Harp messages
        /// for the <see cref="Led1Current"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of <see cref="byte"/> objects representing the
        /// message payload.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects formatted for the
        /// <see cref="Led1Current"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<byte> source)
        {
            return source.Select(value => FromPayload(MessageType, value));
        }

        /// <summary>
        /// Formats an observable sequence of values into timestamped Harp messages
        /// for the <see cref="Led1Current"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of timestamped <see cref="byte"/> objects representing
        /// the message payload.
        /// </param>
        /// <returns>
        /// A sequence of timestamped <see cref="HarpMessage"/> objects formatted for
        /// the <see cref="Led1Current"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<Timestamped<byte>> source)
        {
            return source.Select(payload => FromPayload(payload.Seconds, MessageType, payload.Value));
        }
    }

    /// <summary>
    /// Represents an operator that filters and selects a sequence of timestamped messages
    /// from the Led1Current register.
    /// </summary>
    [Description("Filters and selects timestamped messages from the Led1Current register.")]
    public partial class TimestampedLed1Current : HarpCombinator
    {
        /// <summary>
        /// Filters and selects an observable sequence of timestamped messages from
        /// the <see cref="Led1Current"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of timestamped <see cref="byte"/> objects
        /// representing the register payload.
        /// </returns>
        public IObservable<Timestamped<byte>> Process(IObservable<HarpMessage> source)
        {
            return source.Where(Led1Current.Address, MessageType).Select(Led1Current.GetTimestampedPayload);
        }
    }

    /// <summary>
    /// Represents an operator that specifies the configuration of current to drive LED 0.
    /// </summary>
    [Description("Specifies the configuration of current to drive LED 0.")]
    public partial class Led0MaxCurrent : HarpCombinator
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

        /// <summary>
        /// Filters and selects an observable sequence of messages from the
        /// <see cref="Led0MaxCurrent"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of <see cref="byte"/> objects representing the
        /// message payload.
        /// </returns>
        public IObservable<byte> Process(IObservable<HarpMessage> source)
        {
            return source.Where(Address, MessageType).Select(GetPayload);
        }

        /// <summary>
        /// Formats an observable sequence of values into Harp messages
        /// for the <see cref="Led0MaxCurrent"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of <see cref="byte"/> objects representing the
        /// message payload.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects formatted for the
        /// <see cref="Led0MaxCurrent"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<byte> source)
        {
            return source.Select(value => FromPayload(MessageType, value));
        }

        /// <summary>
        /// Formats an observable sequence of values into timestamped Harp messages
        /// for the <see cref="Led0MaxCurrent"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of timestamped <see cref="byte"/> objects representing
        /// the message payload.
        /// </param>
        /// <returns>
        /// A sequence of timestamped <see cref="HarpMessage"/> objects formatted for
        /// the <see cref="Led0MaxCurrent"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<Timestamped<byte>> source)
        {
            return source.Select(payload => FromPayload(payload.Seconds, MessageType, payload.Value));
        }
    }

    /// <summary>
    /// Represents an operator that filters and selects a sequence of timestamped messages
    /// from the Led0MaxCurrent register.
    /// </summary>
    [Description("Filters and selects timestamped messages from the Led0MaxCurrent register.")]
    public partial class TimestampedLed0MaxCurrent : HarpCombinator
    {
        /// <summary>
        /// Filters and selects an observable sequence of timestamped messages from
        /// the <see cref="Led0MaxCurrent"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of timestamped <see cref="byte"/> objects
        /// representing the register payload.
        /// </returns>
        public IObservable<Timestamped<byte>> Process(IObservable<HarpMessage> source)
        {
            return source.Where(Led0MaxCurrent.Address, MessageType).Select(Led0MaxCurrent.GetTimestampedPayload);
        }
    }

    /// <summary>
    /// Represents an operator that specifies the configuration of current to drive LED 1.
    /// </summary>
    [Description("Specifies the configuration of current to drive LED 1.")]
    public partial class Led1MaxCurrent : HarpCombinator
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

        /// <summary>
        /// Filters and selects an observable sequence of messages from the
        /// <see cref="Led1MaxCurrent"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of <see cref="byte"/> objects representing the
        /// message payload.
        /// </returns>
        public IObservable<byte> Process(IObservable<HarpMessage> source)
        {
            return source.Where(Address, MessageType).Select(GetPayload);
        }

        /// <summary>
        /// Formats an observable sequence of values into Harp messages
        /// for the <see cref="Led1MaxCurrent"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of <see cref="byte"/> objects representing the
        /// message payload.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects formatted for the
        /// <see cref="Led1MaxCurrent"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<byte> source)
        {
            return source.Select(value => FromPayload(MessageType, value));
        }

        /// <summary>
        /// Formats an observable sequence of values into timestamped Harp messages
        /// for the <see cref="Led1MaxCurrent"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of timestamped <see cref="byte"/> objects representing
        /// the message payload.
        /// </param>
        /// <returns>
        /// A sequence of timestamped <see cref="HarpMessage"/> objects formatted for
        /// the <see cref="Led1MaxCurrent"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<Timestamped<byte>> source)
        {
            return source.Select(payload => FromPayload(payload.Seconds, MessageType, payload.Value));
        }
    }

    /// <summary>
    /// Represents an operator that filters and selects a sequence of timestamped messages
    /// from the Led1MaxCurrent register.
    /// </summary>
    [Description("Filters and selects timestamped messages from the Led1MaxCurrent register.")]
    public partial class TimestampedLed1MaxCurrent : HarpCombinator
    {
        /// <summary>
        /// Filters and selects an observable sequence of timestamped messages from
        /// the <see cref="Led1MaxCurrent"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of timestamped <see cref="byte"/> objects
        /// representing the register payload.
        /// </returns>
        public IObservable<Timestamped<byte>> Process(IObservable<HarpMessage> source)
        {
            return source.Where(Led1MaxCurrent.Address, MessageType).Select(Led1MaxCurrent.GetTimestampedPayload);
        }
    }

    /// <summary>
    /// Represents an operator that specifies the active events in the device.
    /// </summary>
    [Description("Specifies the active events in the device.")]
    public partial class EventEnable : HarpCombinator
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

        /// <summary>
        /// Filters and selects an observable sequence of messages from the
        /// <see cref="EventEnable"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of <see cref="Events"/> objects representing the
        /// message payload.
        /// </returns>
        public IObservable<Events> Process(IObservable<HarpMessage> source)
        {
            return source.Where(Address, MessageType).Select(GetPayload);
        }

        /// <summary>
        /// Formats an observable sequence of values into Harp messages
        /// for the <see cref="EventEnable"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of <see cref="Events"/> objects representing the
        /// message payload.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects formatted for the
        /// <see cref="EventEnable"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<Events> source)
        {
            return source.Select(value => FromPayload(MessageType, value));
        }

        /// <summary>
        /// Formats an observable sequence of values into timestamped Harp messages
        /// for the <see cref="EventEnable"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of timestamped <see cref="Events"/> objects representing
        /// the message payload.
        /// </param>
        /// <returns>
        /// A sequence of timestamped <see cref="HarpMessage"/> objects formatted for
        /// the <see cref="EventEnable"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<Timestamped<Events>> source)
        {
            return source.Select(payload => FromPayload(payload.Seconds, MessageType, payload.Value));
        }
    }

    /// <summary>
    /// Represents an operator that filters and selects a sequence of timestamped messages
    /// from the EventEnable register.
    /// </summary>
    [Description("Filters and selects timestamped messages from the EventEnable register.")]
    public partial class TimestampedEventEnable : HarpCombinator
    {
        /// <summary>
        /// Filters and selects an observable sequence of timestamped messages from
        /// the <see cref="EventEnable"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of timestamped <see cref="Events"/> objects
        /// representing the register payload.
        /// </returns>
        public IObservable<Timestamped<Events>> Process(IObservable<HarpMessage> source)
        {
            return source.Where(EventEnable.Address, MessageType).Select(EventEnable.GetTimestampedPayload);
        }
    }

    /// <summary>
    /// Represents an operator that specifies the camera outputs to enable in the device.
    /// </summary>
    [Description("Specifies the camera outputs to enable in the device.")]
    public partial class StartCameras : HarpCombinator
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

        /// <summary>
        /// Filters and selects an observable sequence of messages from the
        /// <see cref="StartCameras"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of <see cref="CameraOutputs"/> objects representing the
        /// message payload.
        /// </returns>
        public IObservable<CameraOutputs> Process(IObservable<HarpMessage> source)
        {
            return source.Where(Address, MessageType).Select(GetPayload);
        }

        /// <summary>
        /// Formats an observable sequence of values into Harp messages
        /// for the <see cref="StartCameras"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of <see cref="CameraOutputs"/> objects representing the
        /// message payload.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects formatted for the
        /// <see cref="StartCameras"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<CameraOutputs> source)
        {
            return source.Select(value => FromPayload(MessageType, value));
        }

        /// <summary>
        /// Formats an observable sequence of values into timestamped Harp messages
        /// for the <see cref="StartCameras"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of timestamped <see cref="CameraOutputs"/> objects representing
        /// the message payload.
        /// </param>
        /// <returns>
        /// A sequence of timestamped <see cref="HarpMessage"/> objects formatted for
        /// the <see cref="StartCameras"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<Timestamped<CameraOutputs>> source)
        {
            return source.Select(payload => FromPayload(payload.Seconds, MessageType, payload.Value));
        }
    }

    /// <summary>
    /// Represents an operator that filters and selects a sequence of timestamped messages
    /// from the StartCameras register.
    /// </summary>
    [Description("Filters and selects timestamped messages from the StartCameras register.")]
    public partial class TimestampedStartCameras : HarpCombinator
    {
        /// <summary>
        /// Filters and selects an observable sequence of timestamped messages from
        /// the <see cref="StartCameras"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of timestamped <see cref="CameraOutputs"/> objects
        /// representing the register payload.
        /// </returns>
        public IObservable<Timestamped<CameraOutputs>> Process(IObservable<HarpMessage> source)
        {
            return source.Where(StartCameras.Address, MessageType).Select(StartCameras.GetTimestampedPayload);
        }
    }

    /// <summary>
    /// Represents an operator that specifies the camera outputs to disable in the device.
    /// </summary>
    [Description("Specifies the camera outputs to disable in the device.")]
    public partial class StopCameras : HarpCombinator
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

        /// <summary>
        /// Filters and selects an observable sequence of messages from the
        /// <see cref="StopCameras"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of <see cref="CameraOutputs"/> objects representing the
        /// message payload.
        /// </returns>
        public IObservable<CameraOutputs> Process(IObservable<HarpMessage> source)
        {
            return source.Where(Address, MessageType).Select(GetPayload);
        }

        /// <summary>
        /// Formats an observable sequence of values into Harp messages
        /// for the <see cref="StopCameras"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of <see cref="CameraOutputs"/> objects representing the
        /// message payload.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects formatted for the
        /// <see cref="StopCameras"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<CameraOutputs> source)
        {
            return source.Select(value => FromPayload(MessageType, value));
        }

        /// <summary>
        /// Formats an observable sequence of values into timestamped Harp messages
        /// for the <see cref="StopCameras"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of timestamped <see cref="CameraOutputs"/> objects representing
        /// the message payload.
        /// </param>
        /// <returns>
        /// A sequence of timestamped <see cref="HarpMessage"/> objects formatted for
        /// the <see cref="StopCameras"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<Timestamped<CameraOutputs>> source)
        {
            return source.Select(payload => FromPayload(payload.Seconds, MessageType, payload.Value));
        }
    }

    /// <summary>
    /// Represents an operator that filters and selects a sequence of timestamped messages
    /// from the StopCameras register.
    /// </summary>
    [Description("Filters and selects timestamped messages from the StopCameras register.")]
    public partial class TimestampedStopCameras : HarpCombinator
    {
        /// <summary>
        /// Filters and selects an observable sequence of timestamped messages from
        /// the <see cref="StopCameras"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of timestamped <see cref="CameraOutputs"/> objects
        /// representing the register payload.
        /// </returns>
        public IObservable<Timestamped<CameraOutputs>> Process(IObservable<HarpMessage> source)
        {
            return source.Where(StopCameras.Address, MessageType).Select(StopCameras.GetTimestampedPayload);
        }
    }

    /// <summary>
    /// Represents an operator that specifies the servo outputs to enable in the device.
    /// </summary>
    [Description("Specifies the servo outputs to enable in the device.")]
    public partial class EnableServos : HarpCombinator
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

        /// <summary>
        /// Filters and selects an observable sequence of messages from the
        /// <see cref="EnableServos"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of <see cref="ServoOutputs"/> objects representing the
        /// message payload.
        /// </returns>
        public IObservable<ServoOutputs> Process(IObservable<HarpMessage> source)
        {
            return source.Where(Address, MessageType).Select(GetPayload);
        }

        /// <summary>
        /// Formats an observable sequence of values into Harp messages
        /// for the <see cref="EnableServos"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of <see cref="ServoOutputs"/> objects representing the
        /// message payload.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects formatted for the
        /// <see cref="EnableServos"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<ServoOutputs> source)
        {
            return source.Select(value => FromPayload(MessageType, value));
        }

        /// <summary>
        /// Formats an observable sequence of values into timestamped Harp messages
        /// for the <see cref="EnableServos"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of timestamped <see cref="ServoOutputs"/> objects representing
        /// the message payload.
        /// </param>
        /// <returns>
        /// A sequence of timestamped <see cref="HarpMessage"/> objects formatted for
        /// the <see cref="EnableServos"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<Timestamped<ServoOutputs>> source)
        {
            return source.Select(payload => FromPayload(payload.Seconds, MessageType, payload.Value));
        }
    }

    /// <summary>
    /// Represents an operator that filters and selects a sequence of timestamped messages
    /// from the EnableServos register.
    /// </summary>
    [Description("Filters and selects timestamped messages from the EnableServos register.")]
    public partial class TimestampedEnableServos : HarpCombinator
    {
        /// <summary>
        /// Filters and selects an observable sequence of timestamped messages from
        /// the <see cref="EnableServos"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of timestamped <see cref="ServoOutputs"/> objects
        /// representing the register payload.
        /// </returns>
        public IObservable<Timestamped<ServoOutputs>> Process(IObservable<HarpMessage> source)
        {
            return source.Where(EnableServos.Address, MessageType).Select(EnableServos.GetTimestampedPayload);
        }
    }

    /// <summary>
    /// Represents an operator that specifies the servo outputs to disable in the device.
    /// </summary>
    [Description("Specifies the servo outputs to disable in the device.")]
    public partial class DisableServos : HarpCombinator
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

        /// <summary>
        /// Filters and selects an observable sequence of messages from the
        /// <see cref="DisableServos"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of <see cref="ServoOutputs"/> objects representing the
        /// message payload.
        /// </returns>
        public IObservable<ServoOutputs> Process(IObservable<HarpMessage> source)
        {
            return source.Where(Address, MessageType).Select(GetPayload);
        }

        /// <summary>
        /// Formats an observable sequence of values into Harp messages
        /// for the <see cref="DisableServos"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of <see cref="ServoOutputs"/> objects representing the
        /// message payload.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects formatted for the
        /// <see cref="DisableServos"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<ServoOutputs> source)
        {
            return source.Select(value => FromPayload(MessageType, value));
        }

        /// <summary>
        /// Formats an observable sequence of values into timestamped Harp messages
        /// for the <see cref="DisableServos"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of timestamped <see cref="ServoOutputs"/> objects representing
        /// the message payload.
        /// </param>
        /// <returns>
        /// A sequence of timestamped <see cref="HarpMessage"/> objects formatted for
        /// the <see cref="DisableServos"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<Timestamped<ServoOutputs>> source)
        {
            return source.Select(payload => FromPayload(payload.Seconds, MessageType, payload.Value));
        }
    }

    /// <summary>
    /// Represents an operator that filters and selects a sequence of timestamped messages
    /// from the DisableServos register.
    /// </summary>
    [Description("Filters and selects timestamped messages from the DisableServos register.")]
    public partial class TimestampedDisableServos : HarpCombinator
    {
        /// <summary>
        /// Filters and selects an observable sequence of timestamped messages from
        /// the <see cref="DisableServos"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of timestamped <see cref="ServoOutputs"/> objects
        /// representing the register payload.
        /// </returns>
        public IObservable<Timestamped<ServoOutputs>> Process(IObservable<HarpMessage> source)
        {
            return source.Where(DisableServos.Address, MessageType).Select(DisableServos.GetTimestampedPayload);
        }
    }

    /// <summary>
    /// Represents an operator that specifies the port quadrature counters to enable in the device.
    /// </summary>
    [Description("Specifies the port quadrature counters to enable in the device.")]
    public partial class EnableEncoders : HarpCombinator
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

        /// <summary>
        /// Filters and selects an observable sequence of messages from the
        /// <see cref="EnableEncoders"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of <see cref="EncoderInputs"/> objects representing the
        /// message payload.
        /// </returns>
        public IObservable<EncoderInputs> Process(IObservable<HarpMessage> source)
        {
            return source.Where(Address, MessageType).Select(GetPayload);
        }

        /// <summary>
        /// Formats an observable sequence of values into Harp messages
        /// for the <see cref="EnableEncoders"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of <see cref="EncoderInputs"/> objects representing the
        /// message payload.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects formatted for the
        /// <see cref="EnableEncoders"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<EncoderInputs> source)
        {
            return source.Select(value => FromPayload(MessageType, value));
        }

        /// <summary>
        /// Formats an observable sequence of values into timestamped Harp messages
        /// for the <see cref="EnableEncoders"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of timestamped <see cref="EncoderInputs"/> objects representing
        /// the message payload.
        /// </param>
        /// <returns>
        /// A sequence of timestamped <see cref="HarpMessage"/> objects formatted for
        /// the <see cref="EnableEncoders"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<Timestamped<EncoderInputs>> source)
        {
            return source.Select(payload => FromPayload(payload.Seconds, MessageType, payload.Value));
        }
    }

    /// <summary>
    /// Represents an operator that filters and selects a sequence of timestamped messages
    /// from the EnableEncoders register.
    /// </summary>
    [Description("Filters and selects timestamped messages from the EnableEncoders register.")]
    public partial class TimestampedEnableEncoders : HarpCombinator
    {
        /// <summary>
        /// Filters and selects an observable sequence of timestamped messages from
        /// the <see cref="EnableEncoders"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of timestamped <see cref="EncoderInputs"/> objects
        /// representing the register payload.
        /// </returns>
        public IObservable<Timestamped<EncoderInputs>> Process(IObservable<HarpMessage> source)
        {
            return source.Where(EnableEncoders.Address, MessageType).Select(EnableEncoders.GetTimestampedPayload);
        }
    }

    /// <summary>
    /// Represents an operator that specifies that a frame was acquired on camera 0.
    /// </summary>
    [Description("Specifies that a frame was acquired on camera 0.")]
    public partial class Camera0Frame : HarpCombinator
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
        /// Initializes a new instance of the <see cref="Camera0Frame"/> class.
        /// </summary>
        public Camera0Frame()
        {
            MessageType = MessageType.Event;
        }

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

        /// <summary>
        /// Filters and selects an observable sequence of messages from the
        /// <see cref="Camera0Frame"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of <see cref="FrameAcquired"/> objects representing the
        /// message payload.
        /// </returns>
        public IObservable<FrameAcquired> Process(IObservable<HarpMessage> source)
        {
            return source.Where(Address, MessageType).Select(GetPayload);
        }

        /// <summary>
        /// Formats an observable sequence of values into Harp messages
        /// for the <see cref="Camera0Frame"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of <see cref="FrameAcquired"/> objects representing the
        /// message payload.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects formatted for the
        /// <see cref="Camera0Frame"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<FrameAcquired> source)
        {
            return source.Select(value => FromPayload(MessageType, value));
        }

        /// <summary>
        /// Formats an observable sequence of values into timestamped Harp messages
        /// for the <see cref="Camera0Frame"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of timestamped <see cref="FrameAcquired"/> objects representing
        /// the message payload.
        /// </param>
        /// <returns>
        /// A sequence of timestamped <see cref="HarpMessage"/> objects formatted for
        /// the <see cref="Camera0Frame"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<Timestamped<FrameAcquired>> source)
        {
            return source.Select(payload => FromPayload(payload.Seconds, MessageType, payload.Value));
        }
    }

    /// <summary>
    /// Represents an operator that filters and selects a sequence of timestamped messages
    /// from the Camera0Frame register.
    /// </summary>
    [Description("Filters and selects timestamped messages from the Camera0Frame register.")]
    public partial class TimestampedCamera0Frame : HarpCombinator
    {
        /// <summary>
        /// Filters and selects an observable sequence of timestamped messages from
        /// the <see cref="Camera0Frame"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of timestamped <see cref="FrameAcquired"/> objects
        /// representing the register payload.
        /// </returns>
        public IObservable<Timestamped<FrameAcquired>> Process(IObservable<HarpMessage> source)
        {
            return source.Where(Camera0Frame.Address, MessageType).Select(Camera0Frame.GetTimestampedPayload);
        }
    }

    /// <summary>
    /// Represents an operator that specifies the trigger frequency for camera 0.
    /// </summary>
    [Description("Specifies the trigger frequency for camera 0.")]
    public partial class Camera0Frequency : HarpCombinator
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

        /// <summary>
        /// Filters and selects an observable sequence of messages from the
        /// <see cref="Camera0Frequency"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of <see cref="ushort"/> objects representing the
        /// message payload.
        /// </returns>
        public IObservable<ushort> Process(IObservable<HarpMessage> source)
        {
            return source.Where(Address, MessageType).Select(GetPayload);
        }

        /// <summary>
        /// Formats an observable sequence of values into Harp messages
        /// for the <see cref="Camera0Frequency"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of <see cref="ushort"/> objects representing the
        /// message payload.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects formatted for the
        /// <see cref="Camera0Frequency"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<ushort> source)
        {
            return source.Select(value => FromPayload(MessageType, value));
        }

        /// <summary>
        /// Formats an observable sequence of values into timestamped Harp messages
        /// for the <see cref="Camera0Frequency"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of timestamped <see cref="ushort"/> objects representing
        /// the message payload.
        /// </param>
        /// <returns>
        /// A sequence of timestamped <see cref="HarpMessage"/> objects formatted for
        /// the <see cref="Camera0Frequency"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<Timestamped<ushort>> source)
        {
            return source.Select(payload => FromPayload(payload.Seconds, MessageType, payload.Value));
        }
    }

    /// <summary>
    /// Represents an operator that filters and selects a sequence of timestamped messages
    /// from the Camera0Frequency register.
    /// </summary>
    [Description("Filters and selects timestamped messages from the Camera0Frequency register.")]
    public partial class TimestampedCamera0Frequency : HarpCombinator
    {
        /// <summary>
        /// Filters and selects an observable sequence of timestamped messages from
        /// the <see cref="Camera0Frequency"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of timestamped <see cref="ushort"/> objects
        /// representing the register payload.
        /// </returns>
        public IObservable<Timestamped<ushort>> Process(IObservable<HarpMessage> source)
        {
            return source.Where(Camera0Frequency.Address, MessageType).Select(Camera0Frequency.GetTimestampedPayload);
        }
    }

    /// <summary>
    /// Represents an operator that specifies that a frame was acquired on camera 1.
    /// </summary>
    [Description("Specifies that a frame was acquired on camera 1.")]
    public partial class Camera1Frame : HarpCombinator
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
        /// Initializes a new instance of the <see cref="Camera1Frame"/> class.
        /// </summary>
        public Camera1Frame()
        {
            MessageType = MessageType.Event;
        }

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

        /// <summary>
        /// Filters and selects an observable sequence of messages from the
        /// <see cref="Camera1Frame"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of <see cref="FrameAcquired"/> objects representing the
        /// message payload.
        /// </returns>
        public IObservable<FrameAcquired> Process(IObservable<HarpMessage> source)
        {
            return source.Where(Address, MessageType).Select(GetPayload);
        }

        /// <summary>
        /// Formats an observable sequence of values into Harp messages
        /// for the <see cref="Camera1Frame"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of <see cref="FrameAcquired"/> objects representing the
        /// message payload.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects formatted for the
        /// <see cref="Camera1Frame"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<FrameAcquired> source)
        {
            return source.Select(value => FromPayload(MessageType, value));
        }

        /// <summary>
        /// Formats an observable sequence of values into timestamped Harp messages
        /// for the <see cref="Camera1Frame"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of timestamped <see cref="FrameAcquired"/> objects representing
        /// the message payload.
        /// </param>
        /// <returns>
        /// A sequence of timestamped <see cref="HarpMessage"/> objects formatted for
        /// the <see cref="Camera1Frame"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<Timestamped<FrameAcquired>> source)
        {
            return source.Select(payload => FromPayload(payload.Seconds, MessageType, payload.Value));
        }
    }

    /// <summary>
    /// Represents an operator that filters and selects a sequence of timestamped messages
    /// from the Camera1Frame register.
    /// </summary>
    [Description("Filters and selects timestamped messages from the Camera1Frame register.")]
    public partial class TimestampedCamera1Frame : HarpCombinator
    {
        /// <summary>
        /// Filters and selects an observable sequence of timestamped messages from
        /// the <see cref="Camera1Frame"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of timestamped <see cref="FrameAcquired"/> objects
        /// representing the register payload.
        /// </returns>
        public IObservable<Timestamped<FrameAcquired>> Process(IObservable<HarpMessage> source)
        {
            return source.Where(Camera1Frame.Address, MessageType).Select(Camera1Frame.GetTimestampedPayload);
        }
    }

    /// <summary>
    /// Represents an operator that specifies the trigger frequency for camera 1.
    /// </summary>
    [Description("Specifies the trigger frequency for camera 1.")]
    public partial class Camera1Frequency : HarpCombinator
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

        /// <summary>
        /// Filters and selects an observable sequence of messages from the
        /// <see cref="Camera1Frequency"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of <see cref="ushort"/> objects representing the
        /// message payload.
        /// </returns>
        public IObservable<ushort> Process(IObservable<HarpMessage> source)
        {
            return source.Where(Address, MessageType).Select(GetPayload);
        }

        /// <summary>
        /// Formats an observable sequence of values into Harp messages
        /// for the <see cref="Camera1Frequency"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of <see cref="ushort"/> objects representing the
        /// message payload.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects formatted for the
        /// <see cref="Camera1Frequency"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<ushort> source)
        {
            return source.Select(value => FromPayload(MessageType, value));
        }

        /// <summary>
        /// Formats an observable sequence of values into timestamped Harp messages
        /// for the <see cref="Camera1Frequency"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of timestamped <see cref="ushort"/> objects representing
        /// the message payload.
        /// </param>
        /// <returns>
        /// A sequence of timestamped <see cref="HarpMessage"/> objects formatted for
        /// the <see cref="Camera1Frequency"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<Timestamped<ushort>> source)
        {
            return source.Select(payload => FromPayload(payload.Seconds, MessageType, payload.Value));
        }
    }

    /// <summary>
    /// Represents an operator that filters and selects a sequence of timestamped messages
    /// from the Camera1Frequency register.
    /// </summary>
    [Description("Filters and selects timestamped messages from the Camera1Frequency register.")]
    public partial class TimestampedCamera1Frequency : HarpCombinator
    {
        /// <summary>
        /// Filters and selects an observable sequence of timestamped messages from
        /// the <see cref="Camera1Frequency"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of timestamped <see cref="ushort"/> objects
        /// representing the register payload.
        /// </returns>
        public IObservable<Timestamped<ushort>> Process(IObservable<HarpMessage> source)
        {
            return source.Where(Camera1Frequency.Address, MessageType).Select(Camera1Frequency.GetTimestampedPayload);
        }
    }

    /// <summary>
    /// Represents an operator that specifies the period of the servo motor in DO2, in microseconds.
    /// </summary>
    [Description("Specifies the period of the servo motor in DO2, in microseconds.")]
    public partial class ServoMotor2Period : HarpCombinator
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

        /// <summary>
        /// Filters and selects an observable sequence of messages from the
        /// <see cref="ServoMotor2Period"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of <see cref="ushort"/> objects representing the
        /// message payload.
        /// </returns>
        public IObservable<ushort> Process(IObservable<HarpMessage> source)
        {
            return source.Where(Address, MessageType).Select(GetPayload);
        }

        /// <summary>
        /// Formats an observable sequence of values into Harp messages
        /// for the <see cref="ServoMotor2Period"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of <see cref="ushort"/> objects representing the
        /// message payload.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects formatted for the
        /// <see cref="ServoMotor2Period"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<ushort> source)
        {
            return source.Select(value => FromPayload(MessageType, value));
        }

        /// <summary>
        /// Formats an observable sequence of values into timestamped Harp messages
        /// for the <see cref="ServoMotor2Period"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of timestamped <see cref="ushort"/> objects representing
        /// the message payload.
        /// </param>
        /// <returns>
        /// A sequence of timestamped <see cref="HarpMessage"/> objects formatted for
        /// the <see cref="ServoMotor2Period"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<Timestamped<ushort>> source)
        {
            return source.Select(payload => FromPayload(payload.Seconds, MessageType, payload.Value));
        }
    }

    /// <summary>
    /// Represents an operator that filters and selects a sequence of timestamped messages
    /// from the ServoMotor2Period register.
    /// </summary>
    [Description("Filters and selects timestamped messages from the ServoMotor2Period register.")]
    public partial class TimestampedServoMotor2Period : HarpCombinator
    {
        /// <summary>
        /// Filters and selects an observable sequence of timestamped messages from
        /// the <see cref="ServoMotor2Period"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of timestamped <see cref="ushort"/> objects
        /// representing the register payload.
        /// </returns>
        public IObservable<Timestamped<ushort>> Process(IObservable<HarpMessage> source)
        {
            return source.Where(ServoMotor2Period.Address, MessageType).Select(ServoMotor2Period.GetTimestampedPayload);
        }
    }

    /// <summary>
    /// Represents an operator that specifies the pulse of the servo motor in DO2, in microseconds.
    /// </summary>
    [Description("Specifies the pulse of the servo motor in DO2, in microseconds.")]
    public partial class ServoMotor2Pulse : HarpCombinator
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

        /// <summary>
        /// Filters and selects an observable sequence of messages from the
        /// <see cref="ServoMotor2Pulse"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of <see cref="ushort"/> objects representing the
        /// message payload.
        /// </returns>
        public IObservable<ushort> Process(IObservable<HarpMessage> source)
        {
            return source.Where(Address, MessageType).Select(GetPayload);
        }

        /// <summary>
        /// Formats an observable sequence of values into Harp messages
        /// for the <see cref="ServoMotor2Pulse"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of <see cref="ushort"/> objects representing the
        /// message payload.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects formatted for the
        /// <see cref="ServoMotor2Pulse"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<ushort> source)
        {
            return source.Select(value => FromPayload(MessageType, value));
        }

        /// <summary>
        /// Formats an observable sequence of values into timestamped Harp messages
        /// for the <see cref="ServoMotor2Pulse"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of timestamped <see cref="ushort"/> objects representing
        /// the message payload.
        /// </param>
        /// <returns>
        /// A sequence of timestamped <see cref="HarpMessage"/> objects formatted for
        /// the <see cref="ServoMotor2Pulse"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<Timestamped<ushort>> source)
        {
            return source.Select(payload => FromPayload(payload.Seconds, MessageType, payload.Value));
        }
    }

    /// <summary>
    /// Represents an operator that filters and selects a sequence of timestamped messages
    /// from the ServoMotor2Pulse register.
    /// </summary>
    [Description("Filters and selects timestamped messages from the ServoMotor2Pulse register.")]
    public partial class TimestampedServoMotor2Pulse : HarpCombinator
    {
        /// <summary>
        /// Filters and selects an observable sequence of timestamped messages from
        /// the <see cref="ServoMotor2Pulse"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of timestamped <see cref="ushort"/> objects
        /// representing the register payload.
        /// </returns>
        public IObservable<Timestamped<ushort>> Process(IObservable<HarpMessage> source)
        {
            return source.Where(ServoMotor2Pulse.Address, MessageType).Select(ServoMotor2Pulse.GetTimestampedPayload);
        }
    }

    /// <summary>
    /// Represents an operator that specifies the period of the servo motor in DO3, in microseconds.
    /// </summary>
    [Description("Specifies the period of the servo motor in DO3, in microseconds.")]
    public partial class ServoMotor3Period : HarpCombinator
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

        /// <summary>
        /// Filters and selects an observable sequence of messages from the
        /// <see cref="ServoMotor3Period"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of <see cref="ushort"/> objects representing the
        /// message payload.
        /// </returns>
        public IObservable<ushort> Process(IObservable<HarpMessage> source)
        {
            return source.Where(Address, MessageType).Select(GetPayload);
        }

        /// <summary>
        /// Formats an observable sequence of values into Harp messages
        /// for the <see cref="ServoMotor3Period"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of <see cref="ushort"/> objects representing the
        /// message payload.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects formatted for the
        /// <see cref="ServoMotor3Period"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<ushort> source)
        {
            return source.Select(value => FromPayload(MessageType, value));
        }

        /// <summary>
        /// Formats an observable sequence of values into timestamped Harp messages
        /// for the <see cref="ServoMotor3Period"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of timestamped <see cref="ushort"/> objects representing
        /// the message payload.
        /// </param>
        /// <returns>
        /// A sequence of timestamped <see cref="HarpMessage"/> objects formatted for
        /// the <see cref="ServoMotor3Period"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<Timestamped<ushort>> source)
        {
            return source.Select(payload => FromPayload(payload.Seconds, MessageType, payload.Value));
        }
    }

    /// <summary>
    /// Represents an operator that filters and selects a sequence of timestamped messages
    /// from the ServoMotor3Period register.
    /// </summary>
    [Description("Filters and selects timestamped messages from the ServoMotor3Period register.")]
    public partial class TimestampedServoMotor3Period : HarpCombinator
    {
        /// <summary>
        /// Filters and selects an observable sequence of timestamped messages from
        /// the <see cref="ServoMotor3Period"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of timestamped <see cref="ushort"/> objects
        /// representing the register payload.
        /// </returns>
        public IObservable<Timestamped<ushort>> Process(IObservable<HarpMessage> source)
        {
            return source.Where(ServoMotor3Period.Address, MessageType).Select(ServoMotor3Period.GetTimestampedPayload);
        }
    }

    /// <summary>
    /// Represents an operator that specifies the pulse of the servo motor in DO3, in microseconds.
    /// </summary>
    [Description("Specifies the pulse of the servo motor in DO3, in microseconds.")]
    public partial class ServoMotor3Pulse : HarpCombinator
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

        /// <summary>
        /// Filters and selects an observable sequence of messages from the
        /// <see cref="ServoMotor3Pulse"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of <see cref="ushort"/> objects representing the
        /// message payload.
        /// </returns>
        public IObservable<ushort> Process(IObservable<HarpMessage> source)
        {
            return source.Where(Address, MessageType).Select(GetPayload);
        }

        /// <summary>
        /// Formats an observable sequence of values into Harp messages
        /// for the <see cref="ServoMotor3Pulse"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of <see cref="ushort"/> objects representing the
        /// message payload.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects formatted for the
        /// <see cref="ServoMotor3Pulse"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<ushort> source)
        {
            return source.Select(value => FromPayload(MessageType, value));
        }

        /// <summary>
        /// Formats an observable sequence of values into timestamped Harp messages
        /// for the <see cref="ServoMotor3Pulse"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of timestamped <see cref="ushort"/> objects representing
        /// the message payload.
        /// </param>
        /// <returns>
        /// A sequence of timestamped <see cref="HarpMessage"/> objects formatted for
        /// the <see cref="ServoMotor3Pulse"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<Timestamped<ushort>> source)
        {
            return source.Select(payload => FromPayload(payload.Seconds, MessageType, payload.Value));
        }
    }

    /// <summary>
    /// Represents an operator that filters and selects a sequence of timestamped messages
    /// from the ServoMotor3Pulse register.
    /// </summary>
    [Description("Filters and selects timestamped messages from the ServoMotor3Pulse register.")]
    public partial class TimestampedServoMotor3Pulse : HarpCombinator
    {
        /// <summary>
        /// Filters and selects an observable sequence of timestamped messages from
        /// the <see cref="ServoMotor3Pulse"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of timestamped <see cref="ushort"/> objects
        /// representing the register payload.
        /// </returns>
        public IObservable<Timestamped<ushort>> Process(IObservable<HarpMessage> source)
        {
            return source.Where(ServoMotor3Pulse.Address, MessageType).Select(ServoMotor3Pulse.GetTimestampedPayload);
        }
    }

    /// <summary>
    /// Represents an operator that reset the counter of the specified encoders to zero.
    /// </summary>
    [Description("Reset the counter of the specified encoders to zero.")]
    public partial class EncoderReset : HarpCombinator
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

        /// <summary>
        /// Filters and selects an observable sequence of messages from the
        /// <see cref="EncoderReset"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of <see cref="EncoderInputs"/> objects representing the
        /// message payload.
        /// </returns>
        public IObservable<EncoderInputs> Process(IObservable<HarpMessage> source)
        {
            return source.Where(Address, MessageType).Select(GetPayload);
        }

        /// <summary>
        /// Formats an observable sequence of values into Harp messages
        /// for the <see cref="EncoderReset"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of <see cref="EncoderInputs"/> objects representing the
        /// message payload.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects formatted for the
        /// <see cref="EncoderReset"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<EncoderInputs> source)
        {
            return source.Select(value => FromPayload(MessageType, value));
        }

        /// <summary>
        /// Formats an observable sequence of values into timestamped Harp messages
        /// for the <see cref="EncoderReset"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of timestamped <see cref="EncoderInputs"/> objects representing
        /// the message payload.
        /// </param>
        /// <returns>
        /// A sequence of timestamped <see cref="HarpMessage"/> objects formatted for
        /// the <see cref="EncoderReset"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<Timestamped<EncoderInputs>> source)
        {
            return source.Select(payload => FromPayload(payload.Seconds, MessageType, payload.Value));
        }
    }

    /// <summary>
    /// Represents an operator that filters and selects a sequence of timestamped messages
    /// from the EncoderReset register.
    /// </summary>
    [Description("Filters and selects timestamped messages from the EncoderReset register.")]
    public partial class TimestampedEncoderReset : HarpCombinator
    {
        /// <summary>
        /// Filters and selects an observable sequence of timestamped messages from
        /// the <see cref="EncoderReset"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of timestamped <see cref="EncoderInputs"/> objects
        /// representing the register payload.
        /// </returns>
        public IObservable<Timestamped<EncoderInputs>> Process(IObservable<HarpMessage> source)
        {
            return source.Where(EncoderReset.Address, MessageType).Select(EncoderReset.GetTimestampedPayload);
        }
    }

    /// <summary>
    /// Represents an operator that enables the timestamp for serial TX.
    /// </summary>
    [Description("Enables the timestamp for serial TX.")]
    public partial class EnableSerialTimestamp : HarpCombinator
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

        /// <summary>
        /// Filters and selects an observable sequence of messages from the
        /// <see cref="EnableSerialTimestamp"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of <see cref="byte"/> objects representing the
        /// message payload.
        /// </returns>
        public IObservable<byte> Process(IObservable<HarpMessage> source)
        {
            return source.Where(Address, MessageType).Select(GetPayload);
        }

        /// <summary>
        /// Formats an observable sequence of values into Harp messages
        /// for the <see cref="EnableSerialTimestamp"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of <see cref="byte"/> objects representing the
        /// message payload.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects formatted for the
        /// <see cref="EnableSerialTimestamp"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<byte> source)
        {
            return source.Select(value => FromPayload(MessageType, value));
        }

        /// <summary>
        /// Formats an observable sequence of values into timestamped Harp messages
        /// for the <see cref="EnableSerialTimestamp"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of timestamped <see cref="byte"/> objects representing
        /// the message payload.
        /// </param>
        /// <returns>
        /// A sequence of timestamped <see cref="HarpMessage"/> objects formatted for
        /// the <see cref="EnableSerialTimestamp"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<Timestamped<byte>> source)
        {
            return source.Select(payload => FromPayload(payload.Seconds, MessageType, payload.Value));
        }
    }

    /// <summary>
    /// Represents an operator that filters and selects a sequence of timestamped messages
    /// from the EnableSerialTimestamp register.
    /// </summary>
    [Description("Filters and selects timestamped messages from the EnableSerialTimestamp register.")]
    public partial class TimestampedEnableSerialTimestamp : HarpCombinator
    {
        /// <summary>
        /// Filters and selects an observable sequence of timestamped messages from
        /// the <see cref="EnableSerialTimestamp"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of timestamped <see cref="byte"/> objects
        /// representing the register payload.
        /// </returns>
        public IObservable<Timestamped<byte>> Process(IObservable<HarpMessage> source)
        {
            return source.Where(EnableSerialTimestamp.Address, MessageType).Select(EnableSerialTimestamp.GetTimestampedPayload);
        }
    }

    /// <summary>
    /// Represents an operator that specifies the digital output to mimic the Port 0 IR state.
    /// </summary>
    [Description("Specifies the digital output to mimic the Port 0 IR state.")]
    public partial class MimicPort0IR : HarpCombinator
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

        /// <summary>
        /// Filters and selects an observable sequence of messages from the
        /// <see cref="MimicPort0IR"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of <see cref="MimicOutput"/> objects representing the
        /// message payload.
        /// </returns>
        public IObservable<MimicOutput> Process(IObservable<HarpMessage> source)
        {
            return source.Where(Address, MessageType).Select(GetPayload);
        }

        /// <summary>
        /// Formats an observable sequence of values into Harp messages
        /// for the <see cref="MimicPort0IR"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of <see cref="MimicOutput"/> objects representing the
        /// message payload.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects formatted for the
        /// <see cref="MimicPort0IR"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<MimicOutput> source)
        {
            return source.Select(value => FromPayload(MessageType, value));
        }

        /// <summary>
        /// Formats an observable sequence of values into timestamped Harp messages
        /// for the <see cref="MimicPort0IR"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of timestamped <see cref="MimicOutput"/> objects representing
        /// the message payload.
        /// </param>
        /// <returns>
        /// A sequence of timestamped <see cref="HarpMessage"/> objects formatted for
        /// the <see cref="MimicPort0IR"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<Timestamped<MimicOutput>> source)
        {
            return source.Select(payload => FromPayload(payload.Seconds, MessageType, payload.Value));
        }
    }

    /// <summary>
    /// Represents an operator that filters and selects a sequence of timestamped messages
    /// from the MimicPort0IR register.
    /// </summary>
    [Description("Filters and selects timestamped messages from the MimicPort0IR register.")]
    public partial class TimestampedMimicPort0IR : HarpCombinator
    {
        /// <summary>
        /// Filters and selects an observable sequence of timestamped messages from
        /// the <see cref="MimicPort0IR"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of timestamped <see cref="MimicOutput"/> objects
        /// representing the register payload.
        /// </returns>
        public IObservable<Timestamped<MimicOutput>> Process(IObservable<HarpMessage> source)
        {
            return source.Where(MimicPort0IR.Address, MessageType).Select(MimicPort0IR.GetTimestampedPayload);
        }
    }

    /// <summary>
    /// Represents an operator that specifies the digital output to mimic the Port 1 IR state.
    /// </summary>
    [Description("Specifies the digital output to mimic the Port 1 IR state.")]
    public partial class MimicPort1IR : HarpCombinator
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

        /// <summary>
        /// Filters and selects an observable sequence of messages from the
        /// <see cref="MimicPort1IR"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of <see cref="MimicOutput"/> objects representing the
        /// message payload.
        /// </returns>
        public IObservable<MimicOutput> Process(IObservable<HarpMessage> source)
        {
            return source.Where(Address, MessageType).Select(GetPayload);
        }

        /// <summary>
        /// Formats an observable sequence of values into Harp messages
        /// for the <see cref="MimicPort1IR"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of <see cref="MimicOutput"/> objects representing the
        /// message payload.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects formatted for the
        /// <see cref="MimicPort1IR"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<MimicOutput> source)
        {
            return source.Select(value => FromPayload(MessageType, value));
        }

        /// <summary>
        /// Formats an observable sequence of values into timestamped Harp messages
        /// for the <see cref="MimicPort1IR"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of timestamped <see cref="MimicOutput"/> objects representing
        /// the message payload.
        /// </param>
        /// <returns>
        /// A sequence of timestamped <see cref="HarpMessage"/> objects formatted for
        /// the <see cref="MimicPort1IR"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<Timestamped<MimicOutput>> source)
        {
            return source.Select(payload => FromPayload(payload.Seconds, MessageType, payload.Value));
        }
    }

    /// <summary>
    /// Represents an operator that filters and selects a sequence of timestamped messages
    /// from the MimicPort1IR register.
    /// </summary>
    [Description("Filters and selects timestamped messages from the MimicPort1IR register.")]
    public partial class TimestampedMimicPort1IR : HarpCombinator
    {
        /// <summary>
        /// Filters and selects an observable sequence of timestamped messages from
        /// the <see cref="MimicPort1IR"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of timestamped <see cref="MimicOutput"/> objects
        /// representing the register payload.
        /// </returns>
        public IObservable<Timestamped<MimicOutput>> Process(IObservable<HarpMessage> source)
        {
            return source.Where(MimicPort1IR.Address, MessageType).Select(MimicPort1IR.GetTimestampedPayload);
        }
    }

    /// <summary>
    /// Represents an operator that specifies the digital output to mimic the Port 2 IR state.
    /// </summary>
    [Description("Specifies the digital output to mimic the Port 2 IR state.")]
    public partial class MimicPort2IR : HarpCombinator
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

        /// <summary>
        /// Filters and selects an observable sequence of messages from the
        /// <see cref="MimicPort2IR"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of <see cref="MimicOutput"/> objects representing the
        /// message payload.
        /// </returns>
        public IObservable<MimicOutput> Process(IObservable<HarpMessage> source)
        {
            return source.Where(Address, MessageType).Select(GetPayload);
        }

        /// <summary>
        /// Formats an observable sequence of values into Harp messages
        /// for the <see cref="MimicPort2IR"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of <see cref="MimicOutput"/> objects representing the
        /// message payload.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects formatted for the
        /// <see cref="MimicPort2IR"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<MimicOutput> source)
        {
            return source.Select(value => FromPayload(MessageType, value));
        }

        /// <summary>
        /// Formats an observable sequence of values into timestamped Harp messages
        /// for the <see cref="MimicPort2IR"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of timestamped <see cref="MimicOutput"/> objects representing
        /// the message payload.
        /// </param>
        /// <returns>
        /// A sequence of timestamped <see cref="HarpMessage"/> objects formatted for
        /// the <see cref="MimicPort2IR"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<Timestamped<MimicOutput>> source)
        {
            return source.Select(payload => FromPayload(payload.Seconds, MessageType, payload.Value));
        }
    }

    /// <summary>
    /// Represents an operator that filters and selects a sequence of timestamped messages
    /// from the MimicPort2IR register.
    /// </summary>
    [Description("Filters and selects timestamped messages from the MimicPort2IR register.")]
    public partial class TimestampedMimicPort2IR : HarpCombinator
    {
        /// <summary>
        /// Filters and selects an observable sequence of timestamped messages from
        /// the <see cref="MimicPort2IR"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of timestamped <see cref="MimicOutput"/> objects
        /// representing the register payload.
        /// </returns>
        public IObservable<Timestamped<MimicOutput>> Process(IObservable<HarpMessage> source)
        {
            return source.Where(MimicPort2IR.Address, MessageType).Select(MimicPort2IR.GetTimestampedPayload);
        }
    }

    /// <summary>
    /// Represents an operator that specifies the digital output to mimic the Port 0 valve state.
    /// </summary>
    [Description("Specifies the digital output to mimic the Port 0 valve state.")]
    public partial class MimicPort0Valve : HarpCombinator
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

        /// <summary>
        /// Filters and selects an observable sequence of messages from the
        /// <see cref="MimicPort0Valve"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of <see cref="MimicOutput"/> objects representing the
        /// message payload.
        /// </returns>
        public IObservable<MimicOutput> Process(IObservable<HarpMessage> source)
        {
            return source.Where(Address, MessageType).Select(GetPayload);
        }

        /// <summary>
        /// Formats an observable sequence of values into Harp messages
        /// for the <see cref="MimicPort0Valve"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of <see cref="MimicOutput"/> objects representing the
        /// message payload.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects formatted for the
        /// <see cref="MimicPort0Valve"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<MimicOutput> source)
        {
            return source.Select(value => FromPayload(MessageType, value));
        }

        /// <summary>
        /// Formats an observable sequence of values into timestamped Harp messages
        /// for the <see cref="MimicPort0Valve"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of timestamped <see cref="MimicOutput"/> objects representing
        /// the message payload.
        /// </param>
        /// <returns>
        /// A sequence of timestamped <see cref="HarpMessage"/> objects formatted for
        /// the <see cref="MimicPort0Valve"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<Timestamped<MimicOutput>> source)
        {
            return source.Select(payload => FromPayload(payload.Seconds, MessageType, payload.Value));
        }
    }

    /// <summary>
    /// Represents an operator that filters and selects a sequence of timestamped messages
    /// from the MimicPort0Valve register.
    /// </summary>
    [Description("Filters and selects timestamped messages from the MimicPort0Valve register.")]
    public partial class TimestampedMimicPort0Valve : HarpCombinator
    {
        /// <summary>
        /// Filters and selects an observable sequence of timestamped messages from
        /// the <see cref="MimicPort0Valve"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of timestamped <see cref="MimicOutput"/> objects
        /// representing the register payload.
        /// </returns>
        public IObservable<Timestamped<MimicOutput>> Process(IObservable<HarpMessage> source)
        {
            return source.Where(MimicPort0Valve.Address, MessageType).Select(MimicPort0Valve.GetTimestampedPayload);
        }
    }

    /// <summary>
    /// Represents an operator that specifies the digital output to mimic the Port 1 valve state.
    /// </summary>
    [Description("Specifies the digital output to mimic the Port 1 valve state.")]
    public partial class MimicPort1Valve : HarpCombinator
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

        /// <summary>
        /// Filters and selects an observable sequence of messages from the
        /// <see cref="MimicPort1Valve"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of <see cref="MimicOutput"/> objects representing the
        /// message payload.
        /// </returns>
        public IObservable<MimicOutput> Process(IObservable<HarpMessage> source)
        {
            return source.Where(Address, MessageType).Select(GetPayload);
        }

        /// <summary>
        /// Formats an observable sequence of values into Harp messages
        /// for the <see cref="MimicPort1Valve"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of <see cref="MimicOutput"/> objects representing the
        /// message payload.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects formatted for the
        /// <see cref="MimicPort1Valve"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<MimicOutput> source)
        {
            return source.Select(value => FromPayload(MessageType, value));
        }

        /// <summary>
        /// Formats an observable sequence of values into timestamped Harp messages
        /// for the <see cref="MimicPort1Valve"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of timestamped <see cref="MimicOutput"/> objects representing
        /// the message payload.
        /// </param>
        /// <returns>
        /// A sequence of timestamped <see cref="HarpMessage"/> objects formatted for
        /// the <see cref="MimicPort1Valve"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<Timestamped<MimicOutput>> source)
        {
            return source.Select(payload => FromPayload(payload.Seconds, MessageType, payload.Value));
        }
    }

    /// <summary>
    /// Represents an operator that filters and selects a sequence of timestamped messages
    /// from the MimicPort1Valve register.
    /// </summary>
    [Description("Filters and selects timestamped messages from the MimicPort1Valve register.")]
    public partial class TimestampedMimicPort1Valve : HarpCombinator
    {
        /// <summary>
        /// Filters and selects an observable sequence of timestamped messages from
        /// the <see cref="MimicPort1Valve"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of timestamped <see cref="MimicOutput"/> objects
        /// representing the register payload.
        /// </returns>
        public IObservable<Timestamped<MimicOutput>> Process(IObservable<HarpMessage> source)
        {
            return source.Where(MimicPort1Valve.Address, MessageType).Select(MimicPort1Valve.GetTimestampedPayload);
        }
    }

    /// <summary>
    /// Represents an operator that specifies the digital output to mimic the Port 2 valve state.
    /// </summary>
    [Description("Specifies the digital output to mimic the Port 2 valve state.")]
    public partial class MimicPort2Valve : HarpCombinator
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

        /// <summary>
        /// Filters and selects an observable sequence of messages from the
        /// <see cref="MimicPort2Valve"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of <see cref="MimicOutput"/> objects representing the
        /// message payload.
        /// </returns>
        public IObservable<MimicOutput> Process(IObservable<HarpMessage> source)
        {
            return source.Where(Address, MessageType).Select(GetPayload);
        }

        /// <summary>
        /// Formats an observable sequence of values into Harp messages
        /// for the <see cref="MimicPort2Valve"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of <see cref="MimicOutput"/> objects representing the
        /// message payload.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects formatted for the
        /// <see cref="MimicPort2Valve"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<MimicOutput> source)
        {
            return source.Select(value => FromPayload(MessageType, value));
        }

        /// <summary>
        /// Formats an observable sequence of values into timestamped Harp messages
        /// for the <see cref="MimicPort2Valve"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of timestamped <see cref="MimicOutput"/> objects representing
        /// the message payload.
        /// </param>
        /// <returns>
        /// A sequence of timestamped <see cref="HarpMessage"/> objects formatted for
        /// the <see cref="MimicPort2Valve"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<Timestamped<MimicOutput>> source)
        {
            return source.Select(payload => FromPayload(payload.Seconds, MessageType, payload.Value));
        }
    }

    /// <summary>
    /// Represents an operator that filters and selects a sequence of timestamped messages
    /// from the MimicPort2Valve register.
    /// </summary>
    [Description("Filters and selects timestamped messages from the MimicPort2Valve register.")]
    public partial class TimestampedMimicPort2Valve : HarpCombinator
    {
        /// <summary>
        /// Filters and selects an observable sequence of timestamped messages from
        /// the <see cref="MimicPort2Valve"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of timestamped <see cref="MimicOutput"/> objects
        /// representing the register payload.
        /// </returns>
        public IObservable<Timestamped<MimicOutput>> Process(IObservable<HarpMessage> source)
        {
            return source.Where(MimicPort2Valve.Address, MessageType).Select(MimicPort2Valve.GetTimestampedPayload);
        }
    }

    /// <summary>
    /// Represents an operator that specifies the low pass filter time value for poke inputs, in ms.
    /// </summary>
    [Description("Specifies the low pass filter time value for poke inputs, in ms.")]
    public partial class PokeInputFilter : HarpCombinator
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

        /// <summary>
        /// Filters and selects an observable sequence of messages from the
        /// <see cref="PokeInputFilter"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of <see cref="byte"/> objects representing the
        /// message payload.
        /// </returns>
        public IObservable<byte> Process(IObservable<HarpMessage> source)
        {
            return source.Where(Address, MessageType).Select(GetPayload);
        }

        /// <summary>
        /// Formats an observable sequence of values into Harp messages
        /// for the <see cref="PokeInputFilter"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of <see cref="byte"/> objects representing the
        /// message payload.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects formatted for the
        /// <see cref="PokeInputFilter"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<byte> source)
        {
            return source.Select(value => FromPayload(MessageType, value));
        }

        /// <summary>
        /// Formats an observable sequence of values into timestamped Harp messages
        /// for the <see cref="PokeInputFilter"/> register.
        /// </summary>
        /// <param name="source">
        /// A sequence of timestamped <see cref="byte"/> objects representing
        /// the message payload.
        /// </param>
        /// <returns>
        /// A sequence of timestamped <see cref="HarpMessage"/> objects formatted for
        /// the <see cref="PokeInputFilter"/> register.
        /// </returns>
        public IObservable<HarpMessage> Process(IObservable<Timestamped<byte>> source)
        {
            return source.Select(payload => FromPayload(payload.Seconds, MessageType, payload.Value));
        }
    }

    /// <summary>
    /// Represents an operator that filters and selects a sequence of timestamped messages
    /// from the PokeInputFilter register.
    /// </summary>
    [Description("Filters and selects timestamped messages from the PokeInputFilter register.")]
    public partial class TimestampedPokeInputFilter : HarpCombinator
    {
        /// <summary>
        /// Filters and selects an observable sequence of timestamped messages from
        /// the <see cref="PokeInputFilter"/> register.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of timestamped <see cref="byte"/> objects
        /// representing the register payload.
        /// </returns>
        public IObservable<Timestamped<byte>> Process(IObservable<HarpMessage> source)
        {
            return source.Where(PokeInputFilter.Address, MessageType).Select(PokeInputFilter.GetTimestampedPayload);
        }
    }

    /// <summary>
    /// Represents an operator which creates standard message payloads for the
    /// Behavior device.
    /// </summary>
    /// <seealso cref="CreateOutputSetPayload"/>
    /// <seealso cref="CreateOutputClearPayload"/>
    /// <seealso cref="CreateOutputTogglePayload"/>
    /// <seealso cref="CreateOutputStatePayload"/>
    /// <seealso cref="CreatePortDIOSetPayload"/>
    /// <seealso cref="CreatePortDIOClearPayload"/>
    /// <seealso cref="CreatePortDIOTogglePayload"/>
    /// <seealso cref="CreatePortDIOStatePayload"/>
    /// <seealso cref="CreatePortDIODirectionPayload"/>
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
    /// <seealso cref="CreateCamera0FrequencyPayload"/>
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
    [XmlInclude(typeof(CreateOutputSetPayload))]
    [XmlInclude(typeof(CreateOutputClearPayload))]
    [XmlInclude(typeof(CreateOutputTogglePayload))]
    [XmlInclude(typeof(CreateOutputStatePayload))]
    [XmlInclude(typeof(CreatePortDIOSetPayload))]
    [XmlInclude(typeof(CreatePortDIOClearPayload))]
    [XmlInclude(typeof(CreatePortDIOTogglePayload))]
    [XmlInclude(typeof(CreatePortDIOStatePayload))]
    [XmlInclude(typeof(CreatePortDIODirectionPayload))]
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
    [XmlInclude(typeof(CreateCamera0FrequencyPayload))]
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
    [Description("Creates standard message payloads for the Behavior device.")]
    public partial class CreateMessage : CreateMessageBuilder, INamedElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateMessage"/> class.
        /// </summary>
        public CreateMessage()
        {
            Payload = new CreateOutputSetPayload();
        }

        string INamedElement.Name => $"{nameof(Behavior)}.{GetElementDisplayName(Payload)}";
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that set the specified digital output lines.
    /// </summary>
    [DisplayName("OutputSetPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that set the specified digital output lines.")]
    public partial class CreateOutputSetPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that set the specified digital output lines.
        /// </summary>
        [Description("The value that set the specified digital output lines.")]
        public DigitalOutputs Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that set the specified digital output lines.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that set the specified digital output lines.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => OutputSet.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that clear the specified digital output lines.
    /// </summary>
    [DisplayName("OutputClearPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that clear the specified digital output lines.")]
    public partial class CreateOutputClearPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that clear the specified digital output lines.
        /// </summary>
        [Description("The value that clear the specified digital output lines.")]
        public DigitalOutputs Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that clear the specified digital output lines.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that clear the specified digital output lines.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => OutputClear.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that toggle the specified digital output lines.
    /// </summary>
    [DisplayName("OutputTogglePayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that toggle the specified digital output lines.")]
    public partial class CreateOutputTogglePayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that toggle the specified digital output lines.
        /// </summary>
        [Description("The value that toggle the specified digital output lines.")]
        public DigitalOutputs Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that toggle the specified digital output lines.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that toggle the specified digital output lines.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => OutputToggle.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that write the state of all digital output lines.
    /// </summary>
    [DisplayName("OutputStatePayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that write the state of all digital output lines.")]
    public partial class CreateOutputStatePayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that write the state of all digital output lines.
        /// </summary>
        [Description("The value that write the state of all digital output lines.")]
        public DigitalOutputs Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that write the state of all digital output lines.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that write the state of all digital output lines.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => OutputState.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that set the specified port DIO lines.
    /// </summary>
    [DisplayName("PortDIOSetPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that set the specified port DIO lines.")]
    public partial class CreatePortDIOSetPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that set the specified port DIO lines.
        /// </summary>
        [Description("The value that set the specified port DIO lines.")]
        public PortDigitalIOS Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that set the specified port DIO lines.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that set the specified port DIO lines.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => PortDIOSet.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that clear the specified port DIO lines.
    /// </summary>
    [DisplayName("PortDIOClearPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that clear the specified port DIO lines.")]
    public partial class CreatePortDIOClearPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that clear the specified port DIO lines.
        /// </summary>
        [Description("The value that clear the specified port DIO lines.")]
        public PortDigitalIOS Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that clear the specified port DIO lines.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that clear the specified port DIO lines.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => PortDIOClear.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that toggle the specified port DIO lines.
    /// </summary>
    [DisplayName("PortDIOTogglePayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that toggle the specified port DIO lines.")]
    public partial class CreatePortDIOTogglePayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that toggle the specified port DIO lines.
        /// </summary>
        [Description("The value that toggle the specified port DIO lines.")]
        public PortDigitalIOS Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that toggle the specified port DIO lines.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that toggle the specified port DIO lines.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => PortDIOToggle.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that write the state of all port DIO lines.
    /// </summary>
    [DisplayName("PortDIOStatePayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that write the state of all port DIO lines.")]
    public partial class CreatePortDIOStatePayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that write the state of all port DIO lines.
        /// </summary>
        [Description("The value that write the state of all port DIO lines.")]
        public PortDigitalIOS Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that write the state of all port DIO lines.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that write the state of all port DIO lines.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => PortDIOState.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that specifies which of the port DIO lines are outputs.
    /// </summary>
    [DisplayName("PortDIODirectionPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that specifies which of the port DIO lines are outputs.")]
    public partial class CreatePortDIODirectionPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that specifies which of the port DIO lines are outputs.
        /// </summary>
        [Description("The value that specifies which of the port DIO lines are outputs.")]
        public PortDigitalIOS Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that specifies which of the port DIO lines are outputs.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that specifies which of the port DIO lines are outputs.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => PortDIODirection.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that enables the pulse function for the specified output lines.
    /// </summary>
    [DisplayName("OutputPulseEnablePayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that enables the pulse function for the specified output lines.")]
    public partial class CreateOutputPulseEnablePayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that enables the pulse function for the specified output lines.
        /// </summary>
        [Description("The value that enables the pulse function for the specified output lines.")]
        public DigitalOutputs Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that enables the pulse function for the specified output lines.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that enables the pulse function for the specified output lines.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => OutputPulseEnable.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that specifies the duration of the output pulse in milliseconds.
    /// </summary>
    [DisplayName("PulseDOPort0Payload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that specifies the duration of the output pulse in milliseconds.")]
    public partial class CreatePulseDOPort0Payload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        [Range(min: 1, max: long.MaxValue)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that specifies the duration of the output pulse in milliseconds.")]
        public ushort Value { get; set; } = 1;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => PulseDOPort0.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that specifies the duration of the output pulse in milliseconds.
    /// </summary>
    [DisplayName("PulseDOPort1Payload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that specifies the duration of the output pulse in milliseconds.")]
    public partial class CreatePulseDOPort1Payload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        [Range(min: 1, max: long.MaxValue)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that specifies the duration of the output pulse in milliseconds.")]
        public ushort Value { get; set; } = 1;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => PulseDOPort1.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that specifies the duration of the output pulse in milliseconds.
    /// </summary>
    [DisplayName("PulseDOPort2Payload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that specifies the duration of the output pulse in milliseconds.")]
    public partial class CreatePulseDOPort2Payload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        [Range(min: 1, max: long.MaxValue)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that specifies the duration of the output pulse in milliseconds.")]
        public ushort Value { get; set; } = 1;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => PulseDOPort2.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that specifies the duration of the output pulse in milliseconds.
    /// </summary>
    [DisplayName("PulseSupplyPort0Payload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that specifies the duration of the output pulse in milliseconds.")]
    public partial class CreatePulseSupplyPort0Payload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        [Range(min: 1, max: long.MaxValue)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that specifies the duration of the output pulse in milliseconds.")]
        public ushort Value { get; set; } = 1;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => PulseSupplyPort0.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that specifies the duration of the output pulse in milliseconds.
    /// </summary>
    [DisplayName("PulseSupplyPort1Payload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that specifies the duration of the output pulse in milliseconds.")]
    public partial class CreatePulseSupplyPort1Payload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        [Range(min: 1, max: long.MaxValue)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that specifies the duration of the output pulse in milliseconds.")]
        public ushort Value { get; set; } = 1;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => PulseSupplyPort1.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that specifies the duration of the output pulse in milliseconds.
    /// </summary>
    [DisplayName("PulseSupplyPort2Payload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that specifies the duration of the output pulse in milliseconds.")]
    public partial class CreatePulseSupplyPort2Payload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        [Range(min: 1, max: long.MaxValue)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that specifies the duration of the output pulse in milliseconds.")]
        public ushort Value { get; set; } = 1;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => PulseSupplyPort2.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that specifies the duration of the output pulse in milliseconds.
    /// </summary>
    [DisplayName("PulseLed0Payload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that specifies the duration of the output pulse in milliseconds.")]
    public partial class CreatePulseLed0Payload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        [Range(min: 1, max: long.MaxValue)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that specifies the duration of the output pulse in milliseconds.")]
        public ushort Value { get; set; } = 1;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => PulseLed0.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that specifies the duration of the output pulse in milliseconds.
    /// </summary>
    [DisplayName("PulseLed1Payload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that specifies the duration of the output pulse in milliseconds.")]
    public partial class CreatePulseLed1Payload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        [Range(min: 1, max: long.MaxValue)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that specifies the duration of the output pulse in milliseconds.")]
        public ushort Value { get; set; } = 1;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => PulseLed1.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that specifies the duration of the output pulse in milliseconds.
    /// </summary>
    [DisplayName("PulseRgb0Payload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that specifies the duration of the output pulse in milliseconds.")]
    public partial class CreatePulseRgb0Payload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        [Range(min: 1, max: long.MaxValue)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that specifies the duration of the output pulse in milliseconds.")]
        public ushort Value { get; set; } = 1;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => PulseRgb0.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that specifies the duration of the output pulse in milliseconds.
    /// </summary>
    [DisplayName("PulseRgb1Payload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that specifies the duration of the output pulse in milliseconds.")]
    public partial class CreatePulseRgb1Payload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        [Range(min: 1, max: long.MaxValue)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that specifies the duration of the output pulse in milliseconds.")]
        public ushort Value { get; set; } = 1;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => PulseRgb1.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that specifies the duration of the output pulse in milliseconds.
    /// </summary>
    [DisplayName("PulseDO0Payload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that specifies the duration of the output pulse in milliseconds.")]
    public partial class CreatePulseDO0Payload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        [Range(min: 1, max: long.MaxValue)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that specifies the duration of the output pulse in milliseconds.")]
        public ushort Value { get; set; } = 1;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => PulseDO0.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that specifies the duration of the output pulse in milliseconds.
    /// </summary>
    [DisplayName("PulseDO1Payload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that specifies the duration of the output pulse in milliseconds.")]
    public partial class CreatePulseDO1Payload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        [Range(min: 1, max: long.MaxValue)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that specifies the duration of the output pulse in milliseconds.")]
        public ushort Value { get; set; } = 1;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => PulseDO1.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that specifies the duration of the output pulse in milliseconds.
    /// </summary>
    [DisplayName("PulseDO2Payload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that specifies the duration of the output pulse in milliseconds.")]
    public partial class CreatePulseDO2Payload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        [Range(min: 1, max: long.MaxValue)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that specifies the duration of the output pulse in milliseconds.")]
        public ushort Value { get; set; } = 1;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => PulseDO2.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that specifies the duration of the output pulse in milliseconds.
    /// </summary>
    [DisplayName("PulseDO3Payload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that specifies the duration of the output pulse in milliseconds.")]
    public partial class CreatePulseDO3Payload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        [Range(min: 1, max: long.MaxValue)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that specifies the duration of the output pulse in milliseconds.")]
        public ushort Value { get; set; } = 1;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that specifies the duration of the output pulse in milliseconds.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => PulseDO3.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that specifies the frequency of the PWM at DO0.
    /// </summary>
    [DisplayName("PwmFrequencyDO0Payload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that specifies the frequency of the PWM at DO0.")]
    public partial class CreatePwmFrequencyDO0Payload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that specifies the frequency of the PWM at DO0.
        /// </summary>
        [Range(min: 1, max: long.MaxValue)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that specifies the frequency of the PWM at DO0.")]
        public ushort Value { get; set; } = 1;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that specifies the frequency of the PWM at DO0.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that specifies the frequency of the PWM at DO0.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => PwmFrequencyDO0.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that specifies the frequency of the PWM at DO1.
    /// </summary>
    [DisplayName("PwmFrequencyDO1Payload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that specifies the frequency of the PWM at DO1.")]
    public partial class CreatePwmFrequencyDO1Payload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that specifies the frequency of the PWM at DO1.
        /// </summary>
        [Range(min: 1, max: long.MaxValue)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that specifies the frequency of the PWM at DO1.")]
        public ushort Value { get; set; } = 1;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that specifies the frequency of the PWM at DO1.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that specifies the frequency of the PWM at DO1.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => PwmFrequencyDO1.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that specifies the frequency of the PWM at DO2.
    /// </summary>
    [DisplayName("PwmFrequencyDO2Payload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that specifies the frequency of the PWM at DO2.")]
    public partial class CreatePwmFrequencyDO2Payload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that specifies the frequency of the PWM at DO2.
        /// </summary>
        [Range(min: 1, max: long.MaxValue)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that specifies the frequency of the PWM at DO2.")]
        public ushort Value { get; set; } = 1;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that specifies the frequency of the PWM at DO2.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that specifies the frequency of the PWM at DO2.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => PwmFrequencyDO2.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that specifies the frequency of the PWM at DO3.
    /// </summary>
    [DisplayName("PwmFrequencyDO3Payload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that specifies the frequency of the PWM at DO3.")]
    public partial class CreatePwmFrequencyDO3Payload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that specifies the frequency of the PWM at DO3.
        /// </summary>
        [Range(min: 1, max: long.MaxValue)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that specifies the frequency of the PWM at DO3.")]
        public ushort Value { get; set; } = 1;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that specifies the frequency of the PWM at DO3.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that specifies the frequency of the PWM at DO3.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => PwmFrequencyDO3.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that specifies the duty cycle of the PWM at DO0.
    /// </summary>
    [DisplayName("PwmDutyCycleDO0Payload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that specifies the duty cycle of the PWM at DO0.")]
    public partial class CreatePwmDutyCycleDO0Payload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that specifies the duty cycle of the PWM at DO0.
        /// </summary>
        [Range(min: 1, max: 99)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that specifies the duty cycle of the PWM at DO0.")]
        public byte Value { get; set; } = 1;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that specifies the duty cycle of the PWM at DO0.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that specifies the duty cycle of the PWM at DO0.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => PwmDutyCycleDO0.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that specifies the duty cycle of the PWM at DO1.
    /// </summary>
    [DisplayName("PwmDutyCycleDO1Payload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that specifies the duty cycle of the PWM at DO1.")]
    public partial class CreatePwmDutyCycleDO1Payload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that specifies the duty cycle of the PWM at DO1.
        /// </summary>
        [Range(min: 1, max: 99)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that specifies the duty cycle of the PWM at DO1.")]
        public byte Value { get; set; } = 1;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that specifies the duty cycle of the PWM at DO1.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that specifies the duty cycle of the PWM at DO1.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => PwmDutyCycleDO1.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that specifies the duty cycle of the PWM at DO2.
    /// </summary>
    [DisplayName("PwmDutyCycleDO2Payload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that specifies the duty cycle of the PWM at DO2.")]
    public partial class CreatePwmDutyCycleDO2Payload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that specifies the duty cycle of the PWM at DO2.
        /// </summary>
        [Range(min: 1, max: 99)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that specifies the duty cycle of the PWM at DO2.")]
        public byte Value { get; set; } = 1;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that specifies the duty cycle of the PWM at DO2.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that specifies the duty cycle of the PWM at DO2.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => PwmDutyCycleDO2.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that specifies the duty cycle of the PWM at DO3.
    /// </summary>
    [DisplayName("PwmDutyCycleDO3Payload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that specifies the duty cycle of the PWM at DO3.")]
    public partial class CreatePwmDutyCycleDO3Payload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that specifies the duty cycle of the PWM at DO3.
        /// </summary>
        [Range(min: 1, max: 99)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that specifies the duty cycle of the PWM at DO3.")]
        public byte Value { get; set; } = 1;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that specifies the duty cycle of the PWM at DO3.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that specifies the duty cycle of the PWM at DO3.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => PwmDutyCycleDO3.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that starts the PWM on the selected output lines.
    /// </summary>
    [DisplayName("PwmStartPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that starts the PWM on the selected output lines.")]
    public partial class CreatePwmStartPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that starts the PWM on the selected output lines.
        /// </summary>
        [Description("The value that starts the PWM on the selected output lines.")]
        public PwmOutputs Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that starts the PWM on the selected output lines.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that starts the PWM on the selected output lines.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => PwmStart.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that stops the PWM on the selected output lines.
    /// </summary>
    [DisplayName("PwmStopPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that stops the PWM on the selected output lines.")]
    public partial class CreatePwmStopPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that stops the PWM on the selected output lines.
        /// </summary>
        [Range(min: 1, max: 99)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that stops the PWM on the selected output lines.")]
        public byte Value { get; set; } = 1;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that stops the PWM on the selected output lines.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that stops the PWM on the selected output lines.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => PwmStop.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that specifies the state of all RGB LED channels.
    /// </summary>
    [DisplayName("RgbAllPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that specifies the state of all RGB LED channels.")]
    public partial class CreateRgbAllPayload : HarpCombinator
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
        /// Creates an observable sequence that contains a single message
        /// that specifies the state of all RGB LED channels.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that specifies the state of all RGB LED channels.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ =>
            {
                RgbAllPayload value;
                value.Green0 = Green0;
                value.Red0 = Red0;
                value.Blue0 = Blue0;
                value.Green1 = Green1;
                value.Red1 = Red1;
                value.Blue1 = Blue1;
                return RgbAll.FromPayload(MessageType, value);
            });
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that specifies the state of the RGB0 LED channels.
    /// </summary>
    [DisplayName("Rgb0Payload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that specifies the state of the RGB0 LED channels.")]
    public partial class CreateRgb0Payload : HarpCombinator
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
        /// Creates an observable sequence that contains a single message
        /// that specifies the state of the RGB0 LED channels.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that specifies the state of the RGB0 LED channels.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ =>
            {
                RgbPayload value;
                value.Green = Green;
                value.Red = Red;
                value.Blue = Blue;
                return Rgb0.FromPayload(MessageType, value);
            });
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that specifies the state of the RGB1 LED channels.
    /// </summary>
    [DisplayName("Rgb1Payload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that specifies the state of the RGB1 LED channels.")]
    public partial class CreateRgb1Payload : HarpCombinator
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
        /// Creates an observable sequence that contains a single message
        /// that specifies the state of the RGB1 LED channels.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that specifies the state of the RGB1 LED channels.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ =>
            {
                RgbPayload value;
                value.Green = Green;
                value.Red = Red;
                value.Blue = Blue;
                return Rgb1.FromPayload(MessageType, value);
            });
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that specifies the configuration of current to drive LED 0.
    /// </summary>
    [DisplayName("Led0CurrentPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that specifies the configuration of current to drive LED 0.")]
    public partial class CreateLed0CurrentPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that specifies the configuration of current to drive LED 0.
        /// </summary>
        [Range(min: 2, max: 100)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that specifies the configuration of current to drive LED 0.")]
        public byte Value { get; set; } = 2;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that specifies the configuration of current to drive LED 0.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that specifies the configuration of current to drive LED 0.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => Led0Current.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that specifies the configuration of current to drive LED 1.
    /// </summary>
    [DisplayName("Led1CurrentPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that specifies the configuration of current to drive LED 1.")]
    public partial class CreateLed1CurrentPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that specifies the configuration of current to drive LED 1.
        /// </summary>
        [Range(min: 2, max: 100)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that specifies the configuration of current to drive LED 1.")]
        public byte Value { get; set; } = 2;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that specifies the configuration of current to drive LED 1.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that specifies the configuration of current to drive LED 1.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => Led1Current.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that specifies the configuration of current to drive LED 0.
    /// </summary>
    [DisplayName("Led0MaxCurrentPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that specifies the configuration of current to drive LED 0.")]
    public partial class CreateLed0MaxCurrentPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that specifies the configuration of current to drive LED 0.
        /// </summary>
        [Range(min: 5, max: 100)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that specifies the configuration of current to drive LED 0.")]
        public byte Value { get; set; } = 5;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that specifies the configuration of current to drive LED 0.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that specifies the configuration of current to drive LED 0.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => Led0MaxCurrent.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that specifies the configuration of current to drive LED 1.
    /// </summary>
    [DisplayName("Led1MaxCurrentPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that specifies the configuration of current to drive LED 1.")]
    public partial class CreateLed1MaxCurrentPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that specifies the configuration of current to drive LED 1.
        /// </summary>
        [Range(min: 5, max: 100)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that specifies the configuration of current to drive LED 1.")]
        public byte Value { get; set; } = 5;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that specifies the configuration of current to drive LED 1.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that specifies the configuration of current to drive LED 1.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => Led1MaxCurrent.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that specifies the active events in the device.
    /// </summary>
    [DisplayName("EventEnablePayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that specifies the active events in the device.")]
    public partial class CreateEventEnablePayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that specifies the active events in the device.
        /// </summary>
        [Description("The value that specifies the active events in the device.")]
        public Events Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that specifies the active events in the device.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that specifies the active events in the device.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => EventEnable.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that specifies the camera outputs to enable in the device.
    /// </summary>
    [DisplayName("StartCamerasPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that specifies the camera outputs to enable in the device.")]
    public partial class CreateStartCamerasPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that specifies the camera outputs to enable in the device.
        /// </summary>
        [Description("The value that specifies the camera outputs to enable in the device.")]
        public CameraOutputs Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that specifies the camera outputs to enable in the device.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that specifies the camera outputs to enable in the device.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => StartCameras.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that specifies the camera outputs to disable in the device.
    /// </summary>
    [DisplayName("StopCamerasPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that specifies the camera outputs to disable in the device.")]
    public partial class CreateStopCamerasPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that specifies the camera outputs to disable in the device.
        /// </summary>
        [Description("The value that specifies the camera outputs to disable in the device.")]
        public CameraOutputs Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that specifies the camera outputs to disable in the device.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that specifies the camera outputs to disable in the device.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => StopCameras.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that specifies the servo outputs to enable in the device.
    /// </summary>
    [DisplayName("EnableServosPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that specifies the servo outputs to enable in the device.")]
    public partial class CreateEnableServosPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that specifies the servo outputs to enable in the device.
        /// </summary>
        [Description("The value that specifies the servo outputs to enable in the device.")]
        public ServoOutputs Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that specifies the servo outputs to enable in the device.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that specifies the servo outputs to enable in the device.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => EnableServos.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that specifies the servo outputs to disable in the device.
    /// </summary>
    [DisplayName("DisableServosPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that specifies the servo outputs to disable in the device.")]
    public partial class CreateDisableServosPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that specifies the servo outputs to disable in the device.
        /// </summary>
        [Description("The value that specifies the servo outputs to disable in the device.")]
        public ServoOutputs Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that specifies the servo outputs to disable in the device.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that specifies the servo outputs to disable in the device.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => DisableServos.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that specifies the port quadrature counters to enable in the device.
    /// </summary>
    [DisplayName("EnableEncodersPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that specifies the port quadrature counters to enable in the device.")]
    public partial class CreateEnableEncodersPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that specifies the port quadrature counters to enable in the device.
        /// </summary>
        [Description("The value that specifies the port quadrature counters to enable in the device.")]
        public EncoderInputs Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that specifies the port quadrature counters to enable in the device.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that specifies the port quadrature counters to enable in the device.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => EnableEncoders.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that specifies the trigger frequency for camera 0.
    /// </summary>
    [DisplayName("Camera0FrequencyPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that specifies the trigger frequency for camera 0.")]
    public partial class CreateCamera0FrequencyPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that specifies the trigger frequency for camera 0.
        /// </summary>
        [Range(min: 1, max: 600)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that specifies the trigger frequency for camera 0.")]
        public ushort Value { get; set; } = 1;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that specifies the trigger frequency for camera 0.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that specifies the trigger frequency for camera 0.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => Camera0Frequency.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that specifies the trigger frequency for camera 1.
    /// </summary>
    [DisplayName("Camera1FrequencyPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that specifies the trigger frequency for camera 1.")]
    public partial class CreateCamera1FrequencyPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that specifies the trigger frequency for camera 1.
        /// </summary>
        [Range(min: 1, max: 600)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that specifies the trigger frequency for camera 1.")]
        public ushort Value { get; set; } = 1;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that specifies the trigger frequency for camera 1.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that specifies the trigger frequency for camera 1.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => Camera1Frequency.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that specifies the period of the servo motor in DO2, in microseconds.
    /// </summary>
    [DisplayName("ServoMotor2PeriodPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that specifies the period of the servo motor in DO2, in microseconds.")]
    public partial class CreateServoMotor2PeriodPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that specifies the period of the servo motor in DO2, in microseconds.
        /// </summary>
        [Description("The value that specifies the period of the servo motor in DO2, in microseconds.")]
        public ushort Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that specifies the period of the servo motor in DO2, in microseconds.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that specifies the period of the servo motor in DO2, in microseconds.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => ServoMotor2Period.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that specifies the pulse of the servo motor in DO2, in microseconds.
    /// </summary>
    [DisplayName("ServoMotor2PulsePayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that specifies the pulse of the servo motor in DO2, in microseconds.")]
    public partial class CreateServoMotor2PulsePayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that specifies the pulse of the servo motor in DO2, in microseconds.
        /// </summary>
        [Description("The value that specifies the pulse of the servo motor in DO2, in microseconds.")]
        public ushort Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that specifies the pulse of the servo motor in DO2, in microseconds.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that specifies the pulse of the servo motor in DO2, in microseconds.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => ServoMotor2Pulse.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that specifies the period of the servo motor in DO3, in microseconds.
    /// </summary>
    [DisplayName("ServoMotor3PeriodPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that specifies the period of the servo motor in DO3, in microseconds.")]
    public partial class CreateServoMotor3PeriodPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that specifies the period of the servo motor in DO3, in microseconds.
        /// </summary>
        [Description("The value that specifies the period of the servo motor in DO3, in microseconds.")]
        public ushort Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that specifies the period of the servo motor in DO3, in microseconds.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that specifies the period of the servo motor in DO3, in microseconds.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => ServoMotor3Period.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that specifies the pulse of the servo motor in DO3, in microseconds.
    /// </summary>
    [DisplayName("ServoMotor3PulsePayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that specifies the pulse of the servo motor in DO3, in microseconds.")]
    public partial class CreateServoMotor3PulsePayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that specifies the pulse of the servo motor in DO3, in microseconds.
        /// </summary>
        [Description("The value that specifies the pulse of the servo motor in DO3, in microseconds.")]
        public ushort Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that specifies the pulse of the servo motor in DO3, in microseconds.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that specifies the pulse of the servo motor in DO3, in microseconds.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => ServoMotor3Pulse.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that reset the counter of the specified encoders to zero.
    /// </summary>
    [DisplayName("EncoderResetPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that reset the counter of the specified encoders to zero.")]
    public partial class CreateEncoderResetPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that reset the counter of the specified encoders to zero.
        /// </summary>
        [Description("The value that reset the counter of the specified encoders to zero.")]
        public EncoderInputs Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that reset the counter of the specified encoders to zero.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that reset the counter of the specified encoders to zero.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => EncoderReset.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that enables the timestamp for serial TX.
    /// </summary>
    [DisplayName("EnableSerialTimestampPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that enables the timestamp for serial TX.")]
    public partial class CreateEnableSerialTimestampPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that enables the timestamp for serial TX.
        /// </summary>
        [Description("The value that enables the timestamp for serial TX.")]
        public byte Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that enables the timestamp for serial TX.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that enables the timestamp for serial TX.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => EnableSerialTimestamp.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that specifies the digital output to mimic the Port 0 IR state.
    /// </summary>
    [DisplayName("MimicPort0IRPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that specifies the digital output to mimic the Port 0 IR state.")]
    public partial class CreateMimicPort0IRPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that specifies the digital output to mimic the Port 0 IR state.
        /// </summary>
        [Description("The value that specifies the digital output to mimic the Port 0 IR state.")]
        public MimicOutput Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that specifies the digital output to mimic the Port 0 IR state.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that specifies the digital output to mimic the Port 0 IR state.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => MimicPort0IR.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that specifies the digital output to mimic the Port 1 IR state.
    /// </summary>
    [DisplayName("MimicPort1IRPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that specifies the digital output to mimic the Port 1 IR state.")]
    public partial class CreateMimicPort1IRPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that specifies the digital output to mimic the Port 1 IR state.
        /// </summary>
        [Description("The value that specifies the digital output to mimic the Port 1 IR state.")]
        public MimicOutput Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that specifies the digital output to mimic the Port 1 IR state.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that specifies the digital output to mimic the Port 1 IR state.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => MimicPort1IR.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that specifies the digital output to mimic the Port 2 IR state.
    /// </summary>
    [DisplayName("MimicPort2IRPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that specifies the digital output to mimic the Port 2 IR state.")]
    public partial class CreateMimicPort2IRPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that specifies the digital output to mimic the Port 2 IR state.
        /// </summary>
        [Description("The value that specifies the digital output to mimic the Port 2 IR state.")]
        public MimicOutput Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that specifies the digital output to mimic the Port 2 IR state.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that specifies the digital output to mimic the Port 2 IR state.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => MimicPort2IR.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that specifies the digital output to mimic the Port 0 valve state.
    /// </summary>
    [DisplayName("MimicPort0ValvePayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that specifies the digital output to mimic the Port 0 valve state.")]
    public partial class CreateMimicPort0ValvePayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that specifies the digital output to mimic the Port 0 valve state.
        /// </summary>
        [Description("The value that specifies the digital output to mimic the Port 0 valve state.")]
        public MimicOutput Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that specifies the digital output to mimic the Port 0 valve state.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that specifies the digital output to mimic the Port 0 valve state.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => MimicPort0Valve.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that specifies the digital output to mimic the Port 1 valve state.
    /// </summary>
    [DisplayName("MimicPort1ValvePayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that specifies the digital output to mimic the Port 1 valve state.")]
    public partial class CreateMimicPort1ValvePayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that specifies the digital output to mimic the Port 1 valve state.
        /// </summary>
        [Description("The value that specifies the digital output to mimic the Port 1 valve state.")]
        public MimicOutput Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that specifies the digital output to mimic the Port 1 valve state.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that specifies the digital output to mimic the Port 1 valve state.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => MimicPort1Valve.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that specifies the digital output to mimic the Port 2 valve state.
    /// </summary>
    [DisplayName("MimicPort2ValvePayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that specifies the digital output to mimic the Port 2 valve state.")]
    public partial class CreateMimicPort2ValvePayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that specifies the digital output to mimic the Port 2 valve state.
        /// </summary>
        [Description("The value that specifies the digital output to mimic the Port 2 valve state.")]
        public MimicOutput Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that specifies the digital output to mimic the Port 2 valve state.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that specifies the digital output to mimic the Port 2 valve state.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => MimicPort2Valve.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that specifies the low pass filter time value for poke inputs, in ms.
    /// </summary>
    [DisplayName("PokeInputFilterPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that specifies the low pass filter time value for poke inputs, in ms.")]
    public partial class CreatePokeInputFilterPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that specifies the low pass filter time value for poke inputs, in ms.
        /// </summary>
        [Description("The value that specifies the low pass filter time value for poke inputs, in ms.")]
        public byte Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that specifies the low pass filter time value for poke inputs, in ms.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that specifies the low pass filter time value for poke inputs, in ms.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => PokeInputFilter.FromPayload(MessageType, Value));
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
        /// <param name="analogInput">The voltage at the output of the ADC</param>
        /// <param name="encoder">The quadrature counter value on Port 2</param>
        public AnalogDataPayload(
            short analogInput,
            short encoder)
        {
            AnalogInput = analogInput;
            Encoder = encoder;
        }

        /// <summary>
        /// The voltage at the output of the ADC
        /// </summary>
        public short AnalogInput;

        /// <summary>
        /// The quadrature counter value on Port 2
        /// </summary>
        public short Encoder;
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
        DI0 = 0x1,
        DI1 = 0x2,
        DI2 = 0x4,
        DI3 = 0x8
    }

    /// <summary>
    /// Specifies the state of port digital output lines.
    /// </summary>
    [Flags]
    public enum DigitalOutputs : ushort
    {
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
        CameraOutput0 = 0x1,
        CameraOutput1 = 0x2
    }

    /// <summary>
    /// Specifies servo output enable bits.
    /// </summary>
    [Flags]
    public enum ServoOutputs : byte
    {
        ServoOutput2 = 0x4,
        ServoOutput3 = 0x8
    }

    /// <summary>
    /// Specifies quadrature counter enable bits.
    /// </summary>
    [Flags]
    public enum EncoderInputs : byte
    {
        EncoderPort2 = 0x4
    }

    /// <summary>
    /// Specifies that camera frame was acquired.
    /// </summary>
    [Flags]
    public enum FrameAcquired : byte
    {
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
}
