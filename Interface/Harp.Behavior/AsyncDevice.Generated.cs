using Bonsai.Harp;
using System.Threading.Tasks;

namespace Harp.Behavior
{
    /// <inheritdoc/>
    public partial class Device
    {
        /// <summary>
        /// Initializes a new instance of the asynchronous API to configure and interface
        /// with Behavior devices on the specified serial port.
        /// </summary>
        /// <param name="portName">
        /// The name of the serial port used to communicate with the Harp device.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous initialization operation. The value of
        /// the <see cref="Task{TResult}.Result"/> parameter contains a new instance of
        /// the <see cref="AsyncDevice"/> class.
        /// </returns>
        public static async Task<AsyncDevice> CreateAsync(string portName)
        {
            var device = new AsyncDevice(portName);
            var whoAmI = await device.ReadWhoAmIAsync();
            if (whoAmI != Device.WhoAmI)
            {
                var errorMessage = string.Format(
                    "The device ID {1} on {0} was unexpected. Check whether a Behavior device is connected to the specified serial port.",
                    portName, whoAmI);
                throw new HarpException(errorMessage);
            }

            return device;
        }
    }

    /// <summary>
    /// Represents an asynchronous API to configure and interface with Behavior devices.
    /// </summary>
    public partial class AsyncDevice : Bonsai.Harp.AsyncDevice
    {
        internal AsyncDevice(string portName)
            : base(portName)
        {
        }

        /// <summary>
        /// Asynchronously reads the contents of the PortDigitalInput register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<DigitalInputs> ReadPortDigitalInputAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(PortDigitalInput.Address));
            return PortDigitalInput.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the PortDigitalInput register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<DigitalInputs>> ReadTimestampedPortDigitalInputAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(PortDigitalInput.Address));
            return PortDigitalInput.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the contents of the OutputSet register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<DigitalOutputs> ReadOutputSetAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(OutputSet.Address));
            return OutputSet.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the OutputSet register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<DigitalOutputs>> ReadTimestampedOutputSetAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(OutputSet.Address));
            return OutputSet.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the OutputSet register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteOutputSetAsync(DigitalOutputs value)
        {
            var request = OutputSet.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the OutputClear register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<DigitalOutputs> ReadOutputClearAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(OutputClear.Address));
            return OutputClear.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the OutputClear register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<DigitalOutputs>> ReadTimestampedOutputClearAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(OutputClear.Address));
            return OutputClear.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the OutputClear register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteOutputClearAsync(DigitalOutputs value)
        {
            var request = OutputClear.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the OutputToggle register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<DigitalOutputs> ReadOutputToggleAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(OutputToggle.Address));
            return OutputToggle.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the OutputToggle register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<DigitalOutputs>> ReadTimestampedOutputToggleAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(OutputToggle.Address));
            return OutputToggle.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the OutputToggle register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteOutputToggleAsync(DigitalOutputs value)
        {
            var request = OutputToggle.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the OutputState register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<DigitalOutputs> ReadOutputStateAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(OutputState.Address));
            return OutputState.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the OutputState register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<DigitalOutputs>> ReadTimestampedOutputStateAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(OutputState.Address));
            return OutputState.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the OutputState register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteOutputStateAsync(DigitalOutputs value)
        {
            var request = OutputState.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the PortDIOSet register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<PortDigitalIOS> ReadPortDIOSetAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(PortDIOSet.Address));
            return PortDIOSet.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the PortDIOSet register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<PortDigitalIOS>> ReadTimestampedPortDIOSetAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(PortDIOSet.Address));
            return PortDIOSet.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the PortDIOSet register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePortDIOSetAsync(PortDigitalIOS value)
        {
            var request = PortDIOSet.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the PortDIOClear register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<PortDigitalIOS> ReadPortDIOClearAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(PortDIOClear.Address));
            return PortDIOClear.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the PortDIOClear register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<PortDigitalIOS>> ReadTimestampedPortDIOClearAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(PortDIOClear.Address));
            return PortDIOClear.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the PortDIOClear register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePortDIOClearAsync(PortDigitalIOS value)
        {
            var request = PortDIOClear.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the PortDIOToggle register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<PortDigitalIOS> ReadPortDIOToggleAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(PortDIOToggle.Address));
            return PortDIOToggle.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the PortDIOToggle register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<PortDigitalIOS>> ReadTimestampedPortDIOToggleAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(PortDIOToggle.Address));
            return PortDIOToggle.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the PortDIOToggle register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePortDIOToggleAsync(PortDigitalIOS value)
        {
            var request = PortDIOToggle.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the PortDIOState register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<PortDigitalIOS> ReadPortDIOStateAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(PortDIOState.Address));
            return PortDIOState.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the PortDIOState register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<PortDigitalIOS>> ReadTimestampedPortDIOStateAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(PortDIOState.Address));
            return PortDIOState.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the PortDIOState register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePortDIOStateAsync(PortDigitalIOS value)
        {
            var request = PortDIOState.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the PortDIODirection register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<PortDigitalIOS> ReadPortDIODirectionAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(PortDIODirection.Address));
            return PortDIODirection.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the PortDIODirection register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<PortDigitalIOS>> ReadTimestampedPortDIODirectionAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(PortDIODirection.Address));
            return PortDIODirection.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the PortDIODirection register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePortDIODirectionAsync(PortDigitalIOS value)
        {
            var request = PortDIODirection.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the PortDIOStateEvent register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<PortDigitalIOS> ReadPortDIOStateEventAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(PortDIOStateEvent.Address));
            return PortDIOStateEvent.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the PortDIOStateEvent register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<PortDigitalIOS>> ReadTimestampedPortDIOStateEventAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(PortDIOStateEvent.Address));
            return PortDIOStateEvent.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the contents of the AnalogData register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<AnalogDataPayload> ReadAnalogDataAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadInt16(AnalogData.Address));
            return AnalogData.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the AnalogData register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<AnalogDataPayload>> ReadTimestampedAnalogDataAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadInt16(AnalogData.Address));
            return AnalogData.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the contents of the OutputPulseEnable register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<DigitalOutputs> ReadOutputPulseEnableAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(OutputPulseEnable.Address));
            return OutputPulseEnable.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the OutputPulseEnable register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<DigitalOutputs>> ReadTimestampedOutputPulseEnableAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(OutputPulseEnable.Address));
            return OutputPulseEnable.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the OutputPulseEnable register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteOutputPulseEnableAsync(DigitalOutputs value)
        {
            var request = OutputPulseEnable.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the PulseDOPort0 register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadPulseDOPort0Async()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(PulseDOPort0.Address));
            return PulseDOPort0.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the PulseDOPort0 register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedPulseDOPort0Async()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(PulseDOPort0.Address));
            return PulseDOPort0.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the PulseDOPort0 register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePulseDOPort0Async(ushort value)
        {
            var request = PulseDOPort0.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the PulseDOPort1 register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadPulseDOPort1Async()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(PulseDOPort1.Address));
            return PulseDOPort1.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the PulseDOPort1 register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedPulseDOPort1Async()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(PulseDOPort1.Address));
            return PulseDOPort1.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the PulseDOPort1 register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePulseDOPort1Async(ushort value)
        {
            var request = PulseDOPort1.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the PulseDOPort2 register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadPulseDOPort2Async()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(PulseDOPort2.Address));
            return PulseDOPort2.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the PulseDOPort2 register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedPulseDOPort2Async()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(PulseDOPort2.Address));
            return PulseDOPort2.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the PulseDOPort2 register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePulseDOPort2Async(ushort value)
        {
            var request = PulseDOPort2.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the PulseSupplyPort0 register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadPulseSupplyPort0Async()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(PulseSupplyPort0.Address));
            return PulseSupplyPort0.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the PulseSupplyPort0 register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedPulseSupplyPort0Async()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(PulseSupplyPort0.Address));
            return PulseSupplyPort0.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the PulseSupplyPort0 register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePulseSupplyPort0Async(ushort value)
        {
            var request = PulseSupplyPort0.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the PulseSupplyPort1 register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadPulseSupplyPort1Async()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(PulseSupplyPort1.Address));
            return PulseSupplyPort1.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the PulseSupplyPort1 register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedPulseSupplyPort1Async()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(PulseSupplyPort1.Address));
            return PulseSupplyPort1.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the PulseSupplyPort1 register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePulseSupplyPort1Async(ushort value)
        {
            var request = PulseSupplyPort1.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the PulseSupplyPort2 register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadPulseSupplyPort2Async()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(PulseSupplyPort2.Address));
            return PulseSupplyPort2.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the PulseSupplyPort2 register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedPulseSupplyPort2Async()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(PulseSupplyPort2.Address));
            return PulseSupplyPort2.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the PulseSupplyPort2 register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePulseSupplyPort2Async(ushort value)
        {
            var request = PulseSupplyPort2.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the PulseLed0 register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadPulseLed0Async()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(PulseLed0.Address));
            return PulseLed0.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the PulseLed0 register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedPulseLed0Async()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(PulseLed0.Address));
            return PulseLed0.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the PulseLed0 register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePulseLed0Async(ushort value)
        {
            var request = PulseLed0.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the PulseLed1 register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadPulseLed1Async()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(PulseLed1.Address));
            return PulseLed1.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the PulseLed1 register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedPulseLed1Async()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(PulseLed1.Address));
            return PulseLed1.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the PulseLed1 register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePulseLed1Async(ushort value)
        {
            var request = PulseLed1.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the PulseRgb0 register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadPulseRgb0Async()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(PulseRgb0.Address));
            return PulseRgb0.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the PulseRgb0 register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedPulseRgb0Async()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(PulseRgb0.Address));
            return PulseRgb0.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the PulseRgb0 register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePulseRgb0Async(ushort value)
        {
            var request = PulseRgb0.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the PulseRgb1 register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadPulseRgb1Async()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(PulseRgb1.Address));
            return PulseRgb1.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the PulseRgb1 register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedPulseRgb1Async()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(PulseRgb1.Address));
            return PulseRgb1.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the PulseRgb1 register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePulseRgb1Async(ushort value)
        {
            var request = PulseRgb1.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the PulseDO0 register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadPulseDO0Async()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(PulseDO0.Address));
            return PulseDO0.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the PulseDO0 register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedPulseDO0Async()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(PulseDO0.Address));
            return PulseDO0.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the PulseDO0 register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePulseDO0Async(ushort value)
        {
            var request = PulseDO0.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the PulseDO1 register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadPulseDO1Async()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(PulseDO1.Address));
            return PulseDO1.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the PulseDO1 register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedPulseDO1Async()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(PulseDO1.Address));
            return PulseDO1.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the PulseDO1 register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePulseDO1Async(ushort value)
        {
            var request = PulseDO1.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the PulseDO2 register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadPulseDO2Async()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(PulseDO2.Address));
            return PulseDO2.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the PulseDO2 register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedPulseDO2Async()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(PulseDO2.Address));
            return PulseDO2.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the PulseDO2 register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePulseDO2Async(ushort value)
        {
            var request = PulseDO2.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the PulseDO3 register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadPulseDO3Async()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(PulseDO3.Address));
            return PulseDO3.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the PulseDO3 register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedPulseDO3Async()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(PulseDO3.Address));
            return PulseDO3.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the PulseDO3 register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePulseDO3Async(ushort value)
        {
            var request = PulseDO3.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the PwmFrequencyDO0 register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadPwmFrequencyDO0Async()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(PwmFrequencyDO0.Address));
            return PwmFrequencyDO0.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the PwmFrequencyDO0 register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedPwmFrequencyDO0Async()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(PwmFrequencyDO0.Address));
            return PwmFrequencyDO0.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the PwmFrequencyDO0 register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePwmFrequencyDO0Async(ushort value)
        {
            var request = PwmFrequencyDO0.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the PwmFrequencyDO1 register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadPwmFrequencyDO1Async()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(PwmFrequencyDO1.Address));
            return PwmFrequencyDO1.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the PwmFrequencyDO1 register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedPwmFrequencyDO1Async()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(PwmFrequencyDO1.Address));
            return PwmFrequencyDO1.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the PwmFrequencyDO1 register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePwmFrequencyDO1Async(ushort value)
        {
            var request = PwmFrequencyDO1.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the PwmFrequencyDO2 register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadPwmFrequencyDO2Async()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(PwmFrequencyDO2.Address));
            return PwmFrequencyDO2.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the PwmFrequencyDO2 register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedPwmFrequencyDO2Async()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(PwmFrequencyDO2.Address));
            return PwmFrequencyDO2.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the PwmFrequencyDO2 register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePwmFrequencyDO2Async(ushort value)
        {
            var request = PwmFrequencyDO2.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the PwmFrequencyDO3 register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadPwmFrequencyDO3Async()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(PwmFrequencyDO3.Address));
            return PwmFrequencyDO3.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the PwmFrequencyDO3 register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedPwmFrequencyDO3Async()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(PwmFrequencyDO3.Address));
            return PwmFrequencyDO3.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the PwmFrequencyDO3 register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePwmFrequencyDO3Async(ushort value)
        {
            var request = PwmFrequencyDO3.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the PwmDutyCycleDO0 register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<byte> ReadPwmDutyCycleDO0Async()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(PwmDutyCycleDO0.Address));
            return PwmDutyCycleDO0.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the PwmDutyCycleDO0 register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<byte>> ReadTimestampedPwmDutyCycleDO0Async()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(PwmDutyCycleDO0.Address));
            return PwmDutyCycleDO0.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the PwmDutyCycleDO0 register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePwmDutyCycleDO0Async(byte value)
        {
            var request = PwmDutyCycleDO0.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the PwmDutyCycleDO1 register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<byte> ReadPwmDutyCycleDO1Async()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(PwmDutyCycleDO1.Address));
            return PwmDutyCycleDO1.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the PwmDutyCycleDO1 register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<byte>> ReadTimestampedPwmDutyCycleDO1Async()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(PwmDutyCycleDO1.Address));
            return PwmDutyCycleDO1.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the PwmDutyCycleDO1 register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePwmDutyCycleDO1Async(byte value)
        {
            var request = PwmDutyCycleDO1.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the PwmDutyCycleDO2 register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<byte> ReadPwmDutyCycleDO2Async()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(PwmDutyCycleDO2.Address));
            return PwmDutyCycleDO2.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the PwmDutyCycleDO2 register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<byte>> ReadTimestampedPwmDutyCycleDO2Async()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(PwmDutyCycleDO2.Address));
            return PwmDutyCycleDO2.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the PwmDutyCycleDO2 register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePwmDutyCycleDO2Async(byte value)
        {
            var request = PwmDutyCycleDO2.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the PwmDutyCycleDO3 register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<byte> ReadPwmDutyCycleDO3Async()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(PwmDutyCycleDO3.Address));
            return PwmDutyCycleDO3.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the PwmDutyCycleDO3 register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<byte>> ReadTimestampedPwmDutyCycleDO3Async()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(PwmDutyCycleDO3.Address));
            return PwmDutyCycleDO3.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the PwmDutyCycleDO3 register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePwmDutyCycleDO3Async(byte value)
        {
            var request = PwmDutyCycleDO3.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the PwmStart register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<PwmOutputs> ReadPwmStartAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(PwmStart.Address));
            return PwmStart.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the PwmStart register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<PwmOutputs>> ReadTimestampedPwmStartAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(PwmStart.Address));
            return PwmStart.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the PwmStart register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePwmStartAsync(PwmOutputs value)
        {
            var request = PwmStart.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the PwmStop register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<byte> ReadPwmStopAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(PwmStop.Address));
            return PwmStop.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the PwmStop register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<byte>> ReadTimestampedPwmStopAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(PwmStop.Address));
            return PwmStop.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the PwmStop register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePwmStopAsync(byte value)
        {
            var request = PwmStop.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the RgbAll register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<RgbAllPayload> ReadRgbAllAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(RgbAll.Address));
            return RgbAll.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the RgbAll register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<RgbAllPayload>> ReadTimestampedRgbAllAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(RgbAll.Address));
            return RgbAll.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the RgbAll register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteRgbAllAsync(RgbAllPayload value)
        {
            var request = RgbAll.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Rgb0 register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<RgbPayload> ReadRgb0Async()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Rgb0.Address));
            return Rgb0.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Rgb0 register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<RgbPayload>> ReadTimestampedRgb0Async()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Rgb0.Address));
            return Rgb0.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Rgb0 register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteRgb0Async(RgbPayload value)
        {
            var request = Rgb0.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Rgb1 register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<RgbPayload> ReadRgb1Async()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Rgb1.Address));
            return Rgb1.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Rgb1 register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<RgbPayload>> ReadTimestampedRgb1Async()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Rgb1.Address));
            return Rgb1.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Rgb1 register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteRgb1Async(RgbPayload value)
        {
            var request = Rgb1.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Led0Current register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<byte> ReadLed0CurrentAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Led0Current.Address));
            return Led0Current.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Led0Current register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<byte>> ReadTimestampedLed0CurrentAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Led0Current.Address));
            return Led0Current.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Led0Current register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteLed0CurrentAsync(byte value)
        {
            var request = Led0Current.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Led1Current register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<byte> ReadLed1CurrentAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Led1Current.Address));
            return Led1Current.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Led1Current register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<byte>> ReadTimestampedLed1CurrentAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Led1Current.Address));
            return Led1Current.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Led1Current register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteLed1CurrentAsync(byte value)
        {
            var request = Led1Current.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Led0MaxCurrent register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<byte> ReadLed0MaxCurrentAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Led0MaxCurrent.Address));
            return Led0MaxCurrent.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Led0MaxCurrent register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<byte>> ReadTimestampedLed0MaxCurrentAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Led0MaxCurrent.Address));
            return Led0MaxCurrent.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Led0MaxCurrent register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteLed0MaxCurrentAsync(byte value)
        {
            var request = Led0MaxCurrent.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Led1MaxCurrent register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<byte> ReadLed1MaxCurrentAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Led1MaxCurrent.Address));
            return Led1MaxCurrent.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Led1MaxCurrent register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<byte>> ReadTimestampedLed1MaxCurrentAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Led1MaxCurrent.Address));
            return Led1MaxCurrent.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Led1MaxCurrent register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteLed1MaxCurrentAsync(byte value)
        {
            var request = Led1MaxCurrent.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the EventEnable register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<Events> ReadEventEnableAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(EventEnable.Address));
            return EventEnable.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the EventEnable register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<Events>> ReadTimestampedEventEnableAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(EventEnable.Address));
            return EventEnable.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the EventEnable register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteEventEnableAsync(Events value)
        {
            var request = EventEnable.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the StartCameras register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<CameraOutputs> ReadStartCamerasAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(StartCameras.Address));
            return StartCameras.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the StartCameras register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<CameraOutputs>> ReadTimestampedStartCamerasAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(StartCameras.Address));
            return StartCameras.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the StartCameras register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteStartCamerasAsync(CameraOutputs value)
        {
            var request = StartCameras.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the StopCameras register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<CameraOutputs> ReadStopCamerasAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(StopCameras.Address));
            return StopCameras.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the StopCameras register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<CameraOutputs>> ReadTimestampedStopCamerasAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(StopCameras.Address));
            return StopCameras.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the StopCameras register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteStopCamerasAsync(CameraOutputs value)
        {
            var request = StopCameras.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the EnableServos register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ServoOutputs> ReadEnableServosAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(EnableServos.Address));
            return EnableServos.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the EnableServos register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ServoOutputs>> ReadTimestampedEnableServosAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(EnableServos.Address));
            return EnableServos.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the EnableServos register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteEnableServosAsync(ServoOutputs value)
        {
            var request = EnableServos.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DisableServos register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ServoOutputs> ReadDisableServosAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DisableServos.Address));
            return DisableServos.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DisableServos register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ServoOutputs>> ReadTimestampedDisableServosAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DisableServos.Address));
            return DisableServos.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DisableServos register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDisableServosAsync(ServoOutputs value)
        {
            var request = DisableServos.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the EnableEncoders register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<EncoderInputs> ReadEnableEncodersAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(EnableEncoders.Address));
            return EnableEncoders.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the EnableEncoders register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<EncoderInputs>> ReadTimestampedEnableEncodersAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(EnableEncoders.Address));
            return EnableEncoders.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the EnableEncoders register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteEnableEncodersAsync(EncoderInputs value)
        {
            var request = EnableEncoders.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Camera0Frame register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<FrameAcquired> ReadCamera0FrameAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Camera0Frame.Address));
            return Camera0Frame.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Camera0Frame register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<FrameAcquired>> ReadTimestampedCamera0FrameAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Camera0Frame.Address));
            return Camera0Frame.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Camera0Frequency register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadCamera0FrequencyAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(Camera0Frequency.Address));
            return Camera0Frequency.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Camera0Frequency register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedCamera0FrequencyAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(Camera0Frequency.Address));
            return Camera0Frequency.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Camera0Frequency register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteCamera0FrequencyAsync(ushort value)
        {
            var request = Camera0Frequency.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Camera1Frame register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<FrameAcquired> ReadCamera1FrameAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Camera1Frame.Address));
            return Camera1Frame.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Camera1Frame register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<FrameAcquired>> ReadTimestampedCamera1FrameAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Camera1Frame.Address));
            return Camera1Frame.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Camera1Frequency register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadCamera1FrequencyAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(Camera1Frequency.Address));
            return Camera1Frequency.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Camera1Frequency register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedCamera1FrequencyAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(Camera1Frequency.Address));
            return Camera1Frequency.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Camera1Frequency register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteCamera1FrequencyAsync(ushort value)
        {
            var request = Camera1Frequency.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the ServoMotor2Period register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadServoMotor2PeriodAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(ServoMotor2Period.Address));
            return ServoMotor2Period.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the ServoMotor2Period register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedServoMotor2PeriodAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(ServoMotor2Period.Address));
            return ServoMotor2Period.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the ServoMotor2Period register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteServoMotor2PeriodAsync(ushort value)
        {
            var request = ServoMotor2Period.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the ServoMotor2Pulse register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadServoMotor2PulseAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(ServoMotor2Pulse.Address));
            return ServoMotor2Pulse.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the ServoMotor2Pulse register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedServoMotor2PulseAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(ServoMotor2Pulse.Address));
            return ServoMotor2Pulse.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the ServoMotor2Pulse register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteServoMotor2PulseAsync(ushort value)
        {
            var request = ServoMotor2Pulse.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the ServoMotor3Period register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadServoMotor3PeriodAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(ServoMotor3Period.Address));
            return ServoMotor3Period.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the ServoMotor3Period register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedServoMotor3PeriodAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(ServoMotor3Period.Address));
            return ServoMotor3Period.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the ServoMotor3Period register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteServoMotor3PeriodAsync(ushort value)
        {
            var request = ServoMotor3Period.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the ServoMotor3Pulse register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadServoMotor3PulseAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(ServoMotor3Pulse.Address));
            return ServoMotor3Pulse.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the ServoMotor3Pulse register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedServoMotor3PulseAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(ServoMotor3Pulse.Address));
            return ServoMotor3Pulse.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the ServoMotor3Pulse register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteServoMotor3PulseAsync(ushort value)
        {
            var request = ServoMotor3Pulse.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the EncoderReset register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<EncoderInputs> ReadEncoderResetAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(EncoderReset.Address));
            return EncoderReset.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the EncoderReset register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<EncoderInputs>> ReadTimestampedEncoderResetAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(EncoderReset.Address));
            return EncoderReset.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the EncoderReset register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteEncoderResetAsync(EncoderInputs value)
        {
            var request = EncoderReset.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the EnableSerialTimestamp register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<byte> ReadEnableSerialTimestampAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(EnableSerialTimestamp.Address));
            return EnableSerialTimestamp.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the EnableSerialTimestamp register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<byte>> ReadTimestampedEnableSerialTimestampAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(EnableSerialTimestamp.Address));
            return EnableSerialTimestamp.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the EnableSerialTimestamp register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteEnableSerialTimestampAsync(byte value)
        {
            var request = EnableSerialTimestamp.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the MimicPort0IR register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<MimicOutput> ReadMimicPort0IRAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(MimicPort0IR.Address));
            return MimicPort0IR.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the MimicPort0IR register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<MimicOutput>> ReadTimestampedMimicPort0IRAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(MimicPort0IR.Address));
            return MimicPort0IR.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the MimicPort0IR register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMimicPort0IRAsync(MimicOutput value)
        {
            var request = MimicPort0IR.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the MimicPort1IR register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<MimicOutput> ReadMimicPort1IRAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(MimicPort1IR.Address));
            return MimicPort1IR.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the MimicPort1IR register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<MimicOutput>> ReadTimestampedMimicPort1IRAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(MimicPort1IR.Address));
            return MimicPort1IR.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the MimicPort1IR register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMimicPort1IRAsync(MimicOutput value)
        {
            var request = MimicPort1IR.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the MimicPort2IR register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<MimicOutput> ReadMimicPort2IRAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(MimicPort2IR.Address));
            return MimicPort2IR.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the MimicPort2IR register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<MimicOutput>> ReadTimestampedMimicPort2IRAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(MimicPort2IR.Address));
            return MimicPort2IR.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the MimicPort2IR register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMimicPort2IRAsync(MimicOutput value)
        {
            var request = MimicPort2IR.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the MimicPort0Valve register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<MimicOutput> ReadMimicPort0ValveAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(MimicPort0Valve.Address));
            return MimicPort0Valve.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the MimicPort0Valve register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<MimicOutput>> ReadTimestampedMimicPort0ValveAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(MimicPort0Valve.Address));
            return MimicPort0Valve.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the MimicPort0Valve register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMimicPort0ValveAsync(MimicOutput value)
        {
            var request = MimicPort0Valve.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the MimicPort1Valve register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<MimicOutput> ReadMimicPort1ValveAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(MimicPort1Valve.Address));
            return MimicPort1Valve.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the MimicPort1Valve register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<MimicOutput>> ReadTimestampedMimicPort1ValveAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(MimicPort1Valve.Address));
            return MimicPort1Valve.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the MimicPort1Valve register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMimicPort1ValveAsync(MimicOutput value)
        {
            var request = MimicPort1Valve.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the MimicPort2Valve register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<MimicOutput> ReadMimicPort2ValveAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(MimicPort2Valve.Address));
            return MimicPort2Valve.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the MimicPort2Valve register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<MimicOutput>> ReadTimestampedMimicPort2ValveAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(MimicPort2Valve.Address));
            return MimicPort2Valve.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the MimicPort2Valve register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMimicPort2ValveAsync(MimicOutput value)
        {
            var request = MimicPort2Valve.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the PokeInputFilter register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<byte> ReadPokeInputFilterAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(PokeInputFilter.Address));
            return PokeInputFilter.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the PokeInputFilter register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<byte>> ReadTimestampedPokeInputFilterAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(PokeInputFilter.Address));
            return PokeInputFilter.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the PokeInputFilter register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePokeInputFilterAsync(byte value)
        {
            var request = PokeInputFilter.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }
    }
}
