using Bonsai.Harp;
using System.Threading;
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
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous initialization operation. The value of
        /// the <see cref="Task{TResult}.Result"/> parameter contains a new instance of
        /// the <see cref="AsyncDevice"/> class.
        /// </returns>
        public static async Task<AsyncDevice> CreateAsync(string portName, CancellationToken cancellationToken = default)
        {
            var device = new AsyncDevice(portName);
            var whoAmI = await device.ReadWhoAmIAsync(cancellationToken);
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
        /// Asynchronously reads the contents of the DigitalInputState register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<DigitalInputs> ReadDigitalInputStateAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DigitalInputState.Address), cancellationToken);
            return DigitalInputState.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DigitalInputState register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<DigitalInputs>> ReadTimestampedDigitalInputStateAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DigitalInputState.Address), cancellationToken);
            return DigitalInputState.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the contents of the OutputSet register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<DigitalOutputs> ReadOutputSetAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(OutputSet.Address), cancellationToken);
            return OutputSet.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the OutputSet register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<DigitalOutputs>> ReadTimestampedOutputSetAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(OutputSet.Address), cancellationToken);
            return OutputSet.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the OutputSet register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteOutputSetAsync(DigitalOutputs value, CancellationToken cancellationToken = default)
        {
            var request = OutputSet.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the OutputClear register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<DigitalOutputs> ReadOutputClearAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(OutputClear.Address), cancellationToken);
            return OutputClear.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the OutputClear register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<DigitalOutputs>> ReadTimestampedOutputClearAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(OutputClear.Address), cancellationToken);
            return OutputClear.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the OutputClear register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteOutputClearAsync(DigitalOutputs value, CancellationToken cancellationToken = default)
        {
            var request = OutputClear.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the OutputToggle register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<DigitalOutputs> ReadOutputToggleAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(OutputToggle.Address), cancellationToken);
            return OutputToggle.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the OutputToggle register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<DigitalOutputs>> ReadTimestampedOutputToggleAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(OutputToggle.Address), cancellationToken);
            return OutputToggle.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the OutputToggle register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteOutputToggleAsync(DigitalOutputs value, CancellationToken cancellationToken = default)
        {
            var request = OutputToggle.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the OutputState register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<DigitalOutputs> ReadOutputStateAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(OutputState.Address), cancellationToken);
            return OutputState.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the OutputState register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<DigitalOutputs>> ReadTimestampedOutputStateAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(OutputState.Address), cancellationToken);
            return OutputState.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the OutputState register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteOutputStateAsync(DigitalOutputs value, CancellationToken cancellationToken = default)
        {
            var request = OutputState.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the PortDIOSet register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<PortDigitalIOS> ReadPortDIOSetAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(PortDIOSet.Address), cancellationToken);
            return PortDIOSet.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the PortDIOSet register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<PortDigitalIOS>> ReadTimestampedPortDIOSetAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(PortDIOSet.Address), cancellationToken);
            return PortDIOSet.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the PortDIOSet register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePortDIOSetAsync(PortDigitalIOS value, CancellationToken cancellationToken = default)
        {
            var request = PortDIOSet.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the PortDIOClear register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<PortDigitalIOS> ReadPortDIOClearAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(PortDIOClear.Address), cancellationToken);
            return PortDIOClear.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the PortDIOClear register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<PortDigitalIOS>> ReadTimestampedPortDIOClearAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(PortDIOClear.Address), cancellationToken);
            return PortDIOClear.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the PortDIOClear register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePortDIOClearAsync(PortDigitalIOS value, CancellationToken cancellationToken = default)
        {
            var request = PortDIOClear.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the PortDIOToggle register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<PortDigitalIOS> ReadPortDIOToggleAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(PortDIOToggle.Address), cancellationToken);
            return PortDIOToggle.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the PortDIOToggle register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<PortDigitalIOS>> ReadTimestampedPortDIOToggleAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(PortDIOToggle.Address), cancellationToken);
            return PortDIOToggle.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the PortDIOToggle register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePortDIOToggleAsync(PortDigitalIOS value, CancellationToken cancellationToken = default)
        {
            var request = PortDIOToggle.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the PortDIOState register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<PortDigitalIOS> ReadPortDIOStateAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(PortDIOState.Address), cancellationToken);
            return PortDIOState.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the PortDIOState register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<PortDigitalIOS>> ReadTimestampedPortDIOStateAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(PortDIOState.Address), cancellationToken);
            return PortDIOState.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the PortDIOState register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePortDIOStateAsync(PortDigitalIOS value, CancellationToken cancellationToken = default)
        {
            var request = PortDIOState.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the PortDIODirection register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<PortDigitalIOS> ReadPortDIODirectionAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(PortDIODirection.Address), cancellationToken);
            return PortDIODirection.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the PortDIODirection register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<PortDigitalIOS>> ReadTimestampedPortDIODirectionAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(PortDIODirection.Address), cancellationToken);
            return PortDIODirection.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the PortDIODirection register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePortDIODirectionAsync(PortDigitalIOS value, CancellationToken cancellationToken = default)
        {
            var request = PortDIODirection.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the PortDIOStateEvent register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<PortDigitalIOS> ReadPortDIOStateEventAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(PortDIOStateEvent.Address), cancellationToken);
            return PortDIOStateEvent.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the PortDIOStateEvent register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<PortDigitalIOS>> ReadTimestampedPortDIOStateEventAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(PortDIOStateEvent.Address), cancellationToken);
            return PortDIOStateEvent.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the contents of the AnalogData register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<AnalogDataPayload> ReadAnalogDataAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt16(AnalogData.Address), cancellationToken);
            return AnalogData.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the AnalogData register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<AnalogDataPayload>> ReadTimestampedAnalogDataAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadInt16(AnalogData.Address), cancellationToken);
            return AnalogData.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the contents of the OutputPulseEnable register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<DigitalOutputs> ReadOutputPulseEnableAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(OutputPulseEnable.Address), cancellationToken);
            return OutputPulseEnable.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the OutputPulseEnable register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<DigitalOutputs>> ReadTimestampedOutputPulseEnableAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(OutputPulseEnable.Address), cancellationToken);
            return OutputPulseEnable.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the OutputPulseEnable register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteOutputPulseEnableAsync(DigitalOutputs value, CancellationToken cancellationToken = default)
        {
            var request = OutputPulseEnable.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the PulseDOPort0 register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadPulseDOPort0Async(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(PulseDOPort0.Address), cancellationToken);
            return PulseDOPort0.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the PulseDOPort0 register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedPulseDOPort0Async(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(PulseDOPort0.Address), cancellationToken);
            return PulseDOPort0.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the PulseDOPort0 register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePulseDOPort0Async(ushort value, CancellationToken cancellationToken = default)
        {
            var request = PulseDOPort0.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the PulseDOPort1 register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadPulseDOPort1Async(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(PulseDOPort1.Address), cancellationToken);
            return PulseDOPort1.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the PulseDOPort1 register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedPulseDOPort1Async(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(PulseDOPort1.Address), cancellationToken);
            return PulseDOPort1.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the PulseDOPort1 register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePulseDOPort1Async(ushort value, CancellationToken cancellationToken = default)
        {
            var request = PulseDOPort1.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the PulseDOPort2 register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadPulseDOPort2Async(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(PulseDOPort2.Address), cancellationToken);
            return PulseDOPort2.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the PulseDOPort2 register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedPulseDOPort2Async(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(PulseDOPort2.Address), cancellationToken);
            return PulseDOPort2.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the PulseDOPort2 register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePulseDOPort2Async(ushort value, CancellationToken cancellationToken = default)
        {
            var request = PulseDOPort2.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the PulseSupplyPort0 register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadPulseSupplyPort0Async(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(PulseSupplyPort0.Address), cancellationToken);
            return PulseSupplyPort0.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the PulseSupplyPort0 register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedPulseSupplyPort0Async(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(PulseSupplyPort0.Address), cancellationToken);
            return PulseSupplyPort0.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the PulseSupplyPort0 register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePulseSupplyPort0Async(ushort value, CancellationToken cancellationToken = default)
        {
            var request = PulseSupplyPort0.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the PulseSupplyPort1 register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadPulseSupplyPort1Async(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(PulseSupplyPort1.Address), cancellationToken);
            return PulseSupplyPort1.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the PulseSupplyPort1 register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedPulseSupplyPort1Async(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(PulseSupplyPort1.Address), cancellationToken);
            return PulseSupplyPort1.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the PulseSupplyPort1 register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePulseSupplyPort1Async(ushort value, CancellationToken cancellationToken = default)
        {
            var request = PulseSupplyPort1.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the PulseSupplyPort2 register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadPulseSupplyPort2Async(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(PulseSupplyPort2.Address), cancellationToken);
            return PulseSupplyPort2.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the PulseSupplyPort2 register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedPulseSupplyPort2Async(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(PulseSupplyPort2.Address), cancellationToken);
            return PulseSupplyPort2.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the PulseSupplyPort2 register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePulseSupplyPort2Async(ushort value, CancellationToken cancellationToken = default)
        {
            var request = PulseSupplyPort2.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the PulseLed0 register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadPulseLed0Async(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(PulseLed0.Address), cancellationToken);
            return PulseLed0.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the PulseLed0 register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedPulseLed0Async(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(PulseLed0.Address), cancellationToken);
            return PulseLed0.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the PulseLed0 register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePulseLed0Async(ushort value, CancellationToken cancellationToken = default)
        {
            var request = PulseLed0.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the PulseLed1 register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadPulseLed1Async(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(PulseLed1.Address), cancellationToken);
            return PulseLed1.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the PulseLed1 register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedPulseLed1Async(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(PulseLed1.Address), cancellationToken);
            return PulseLed1.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the PulseLed1 register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePulseLed1Async(ushort value, CancellationToken cancellationToken = default)
        {
            var request = PulseLed1.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the PulseRgb0 register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadPulseRgb0Async(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(PulseRgb0.Address), cancellationToken);
            return PulseRgb0.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the PulseRgb0 register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedPulseRgb0Async(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(PulseRgb0.Address), cancellationToken);
            return PulseRgb0.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the PulseRgb0 register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePulseRgb0Async(ushort value, CancellationToken cancellationToken = default)
        {
            var request = PulseRgb0.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the PulseRgb1 register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadPulseRgb1Async(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(PulseRgb1.Address), cancellationToken);
            return PulseRgb1.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the PulseRgb1 register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedPulseRgb1Async(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(PulseRgb1.Address), cancellationToken);
            return PulseRgb1.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the PulseRgb1 register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePulseRgb1Async(ushort value, CancellationToken cancellationToken = default)
        {
            var request = PulseRgb1.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the PulseDO0 register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadPulseDO0Async(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(PulseDO0.Address), cancellationToken);
            return PulseDO0.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the PulseDO0 register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedPulseDO0Async(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(PulseDO0.Address), cancellationToken);
            return PulseDO0.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the PulseDO0 register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePulseDO0Async(ushort value, CancellationToken cancellationToken = default)
        {
            var request = PulseDO0.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the PulseDO1 register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadPulseDO1Async(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(PulseDO1.Address), cancellationToken);
            return PulseDO1.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the PulseDO1 register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedPulseDO1Async(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(PulseDO1.Address), cancellationToken);
            return PulseDO1.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the PulseDO1 register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePulseDO1Async(ushort value, CancellationToken cancellationToken = default)
        {
            var request = PulseDO1.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the PulseDO2 register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadPulseDO2Async(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(PulseDO2.Address), cancellationToken);
            return PulseDO2.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the PulseDO2 register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedPulseDO2Async(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(PulseDO2.Address), cancellationToken);
            return PulseDO2.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the PulseDO2 register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePulseDO2Async(ushort value, CancellationToken cancellationToken = default)
        {
            var request = PulseDO2.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the PulseDO3 register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadPulseDO3Async(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(PulseDO3.Address), cancellationToken);
            return PulseDO3.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the PulseDO3 register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedPulseDO3Async(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(PulseDO3.Address), cancellationToken);
            return PulseDO3.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the PulseDO3 register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePulseDO3Async(ushort value, CancellationToken cancellationToken = default)
        {
            var request = PulseDO3.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the PwmFrequencyDO0 register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadPwmFrequencyDO0Async(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(PwmFrequencyDO0.Address), cancellationToken);
            return PwmFrequencyDO0.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the PwmFrequencyDO0 register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedPwmFrequencyDO0Async(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(PwmFrequencyDO0.Address), cancellationToken);
            return PwmFrequencyDO0.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the PwmFrequencyDO0 register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePwmFrequencyDO0Async(ushort value, CancellationToken cancellationToken = default)
        {
            var request = PwmFrequencyDO0.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the PwmFrequencyDO1 register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadPwmFrequencyDO1Async(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(PwmFrequencyDO1.Address), cancellationToken);
            return PwmFrequencyDO1.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the PwmFrequencyDO1 register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedPwmFrequencyDO1Async(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(PwmFrequencyDO1.Address), cancellationToken);
            return PwmFrequencyDO1.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the PwmFrequencyDO1 register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePwmFrequencyDO1Async(ushort value, CancellationToken cancellationToken = default)
        {
            var request = PwmFrequencyDO1.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the PwmFrequencyDO2 register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadPwmFrequencyDO2Async(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(PwmFrequencyDO2.Address), cancellationToken);
            return PwmFrequencyDO2.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the PwmFrequencyDO2 register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedPwmFrequencyDO2Async(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(PwmFrequencyDO2.Address), cancellationToken);
            return PwmFrequencyDO2.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the PwmFrequencyDO2 register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePwmFrequencyDO2Async(ushort value, CancellationToken cancellationToken = default)
        {
            var request = PwmFrequencyDO2.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the PwmFrequencyDO3 register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadPwmFrequencyDO3Async(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(PwmFrequencyDO3.Address), cancellationToken);
            return PwmFrequencyDO3.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the PwmFrequencyDO3 register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedPwmFrequencyDO3Async(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(PwmFrequencyDO3.Address), cancellationToken);
            return PwmFrequencyDO3.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the PwmFrequencyDO3 register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePwmFrequencyDO3Async(ushort value, CancellationToken cancellationToken = default)
        {
            var request = PwmFrequencyDO3.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the PwmDutyCycleDO0 register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<byte> ReadPwmDutyCycleDO0Async(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(PwmDutyCycleDO0.Address), cancellationToken);
            return PwmDutyCycleDO0.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the PwmDutyCycleDO0 register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<byte>> ReadTimestampedPwmDutyCycleDO0Async(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(PwmDutyCycleDO0.Address), cancellationToken);
            return PwmDutyCycleDO0.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the PwmDutyCycleDO0 register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePwmDutyCycleDO0Async(byte value, CancellationToken cancellationToken = default)
        {
            var request = PwmDutyCycleDO0.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the PwmDutyCycleDO1 register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<byte> ReadPwmDutyCycleDO1Async(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(PwmDutyCycleDO1.Address), cancellationToken);
            return PwmDutyCycleDO1.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the PwmDutyCycleDO1 register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<byte>> ReadTimestampedPwmDutyCycleDO1Async(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(PwmDutyCycleDO1.Address), cancellationToken);
            return PwmDutyCycleDO1.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the PwmDutyCycleDO1 register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePwmDutyCycleDO1Async(byte value, CancellationToken cancellationToken = default)
        {
            var request = PwmDutyCycleDO1.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the PwmDutyCycleDO2 register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<byte> ReadPwmDutyCycleDO2Async(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(PwmDutyCycleDO2.Address), cancellationToken);
            return PwmDutyCycleDO2.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the PwmDutyCycleDO2 register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<byte>> ReadTimestampedPwmDutyCycleDO2Async(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(PwmDutyCycleDO2.Address), cancellationToken);
            return PwmDutyCycleDO2.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the PwmDutyCycleDO2 register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePwmDutyCycleDO2Async(byte value, CancellationToken cancellationToken = default)
        {
            var request = PwmDutyCycleDO2.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the PwmDutyCycleDO3 register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<byte> ReadPwmDutyCycleDO3Async(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(PwmDutyCycleDO3.Address), cancellationToken);
            return PwmDutyCycleDO3.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the PwmDutyCycleDO3 register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<byte>> ReadTimestampedPwmDutyCycleDO3Async(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(PwmDutyCycleDO3.Address), cancellationToken);
            return PwmDutyCycleDO3.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the PwmDutyCycleDO3 register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePwmDutyCycleDO3Async(byte value, CancellationToken cancellationToken = default)
        {
            var request = PwmDutyCycleDO3.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the PwmStart register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<PwmOutputs> ReadPwmStartAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(PwmStart.Address), cancellationToken);
            return PwmStart.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the PwmStart register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<PwmOutputs>> ReadTimestampedPwmStartAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(PwmStart.Address), cancellationToken);
            return PwmStart.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the PwmStart register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePwmStartAsync(PwmOutputs value, CancellationToken cancellationToken = default)
        {
            var request = PwmStart.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the PwmStop register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<PwmOutputs> ReadPwmStopAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(PwmStop.Address), cancellationToken);
            return PwmStop.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the PwmStop register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<PwmOutputs>> ReadTimestampedPwmStopAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(PwmStop.Address), cancellationToken);
            return PwmStop.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the PwmStop register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePwmStopAsync(PwmOutputs value, CancellationToken cancellationToken = default)
        {
            var request = PwmStop.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the RgbAll register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<RgbAllPayload> ReadRgbAllAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(RgbAll.Address), cancellationToken);
            return RgbAll.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the RgbAll register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<RgbAllPayload>> ReadTimestampedRgbAllAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(RgbAll.Address), cancellationToken);
            return RgbAll.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the RgbAll register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteRgbAllAsync(RgbAllPayload value, CancellationToken cancellationToken = default)
        {
            var request = RgbAll.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Rgb0 register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<RgbPayload> ReadRgb0Async(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Rgb0.Address), cancellationToken);
            return Rgb0.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Rgb0 register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<RgbPayload>> ReadTimestampedRgb0Async(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Rgb0.Address), cancellationToken);
            return Rgb0.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Rgb0 register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteRgb0Async(RgbPayload value, CancellationToken cancellationToken = default)
        {
            var request = Rgb0.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Rgb1 register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<RgbPayload> ReadRgb1Async(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Rgb1.Address), cancellationToken);
            return Rgb1.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Rgb1 register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<RgbPayload>> ReadTimestampedRgb1Async(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Rgb1.Address), cancellationToken);
            return Rgb1.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Rgb1 register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteRgb1Async(RgbPayload value, CancellationToken cancellationToken = default)
        {
            var request = Rgb1.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Led0Current register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<byte> ReadLed0CurrentAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Led0Current.Address), cancellationToken);
            return Led0Current.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Led0Current register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<byte>> ReadTimestampedLed0CurrentAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Led0Current.Address), cancellationToken);
            return Led0Current.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Led0Current register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteLed0CurrentAsync(byte value, CancellationToken cancellationToken = default)
        {
            var request = Led0Current.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Led1Current register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<byte> ReadLed1CurrentAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Led1Current.Address), cancellationToken);
            return Led1Current.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Led1Current register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<byte>> ReadTimestampedLed1CurrentAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Led1Current.Address), cancellationToken);
            return Led1Current.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Led1Current register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteLed1CurrentAsync(byte value, CancellationToken cancellationToken = default)
        {
            var request = Led1Current.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Led0MaxCurrent register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<byte> ReadLed0MaxCurrentAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Led0MaxCurrent.Address), cancellationToken);
            return Led0MaxCurrent.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Led0MaxCurrent register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<byte>> ReadTimestampedLed0MaxCurrentAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Led0MaxCurrent.Address), cancellationToken);
            return Led0MaxCurrent.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Led0MaxCurrent register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteLed0MaxCurrentAsync(byte value, CancellationToken cancellationToken = default)
        {
            var request = Led0MaxCurrent.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Led1MaxCurrent register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<byte> ReadLed1MaxCurrentAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Led1MaxCurrent.Address), cancellationToken);
            return Led1MaxCurrent.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Led1MaxCurrent register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<byte>> ReadTimestampedLed1MaxCurrentAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Led1MaxCurrent.Address), cancellationToken);
            return Led1MaxCurrent.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Led1MaxCurrent register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteLed1MaxCurrentAsync(byte value, CancellationToken cancellationToken = default)
        {
            var request = Led1MaxCurrent.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the EventEnable register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<Events> ReadEventEnableAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(EventEnable.Address), cancellationToken);
            return EventEnable.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the EventEnable register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<Events>> ReadTimestampedEventEnableAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(EventEnable.Address), cancellationToken);
            return EventEnable.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the EventEnable register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteEventEnableAsync(Events value, CancellationToken cancellationToken = default)
        {
            var request = EventEnable.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the StartCameras register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<CameraOutputs> ReadStartCamerasAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(StartCameras.Address), cancellationToken);
            return StartCameras.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the StartCameras register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<CameraOutputs>> ReadTimestampedStartCamerasAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(StartCameras.Address), cancellationToken);
            return StartCameras.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the StartCameras register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteStartCamerasAsync(CameraOutputs value, CancellationToken cancellationToken = default)
        {
            var request = StartCameras.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the StopCameras register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<CameraOutputs> ReadStopCamerasAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(StopCameras.Address), cancellationToken);
            return StopCameras.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the StopCameras register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<CameraOutputs>> ReadTimestampedStopCamerasAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(StopCameras.Address), cancellationToken);
            return StopCameras.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the StopCameras register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteStopCamerasAsync(CameraOutputs value, CancellationToken cancellationToken = default)
        {
            var request = StopCameras.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the EnableServos register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ServoOutputs> ReadEnableServosAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(EnableServos.Address), cancellationToken);
            return EnableServos.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the EnableServos register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ServoOutputs>> ReadTimestampedEnableServosAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(EnableServos.Address), cancellationToken);
            return EnableServos.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the EnableServos register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteEnableServosAsync(ServoOutputs value, CancellationToken cancellationToken = default)
        {
            var request = EnableServos.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DisableServos register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ServoOutputs> ReadDisableServosAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DisableServos.Address), cancellationToken);
            return DisableServos.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DisableServos register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ServoOutputs>> ReadTimestampedDisableServosAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DisableServos.Address), cancellationToken);
            return DisableServos.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DisableServos register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDisableServosAsync(ServoOutputs value, CancellationToken cancellationToken = default)
        {
            var request = DisableServos.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the EnableEncoders register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<EncoderInputs> ReadEnableEncodersAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(EnableEncoders.Address), cancellationToken);
            return EnableEncoders.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the EnableEncoders register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<EncoderInputs>> ReadTimestampedEnableEncodersAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(EnableEncoders.Address), cancellationToken);
            return EnableEncoders.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the EnableEncoders register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteEnableEncodersAsync(EncoderInputs value, CancellationToken cancellationToken = default)
        {
            var request = EnableEncoders.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the EncoderMode register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<EncoderModeConfig> ReadEncoderModeAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(EncoderMode.Address), cancellationToken);
            return EncoderMode.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the EncoderMode register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<EncoderModeConfig>> ReadTimestampedEncoderModeAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(EncoderMode.Address), cancellationToken);
            return EncoderMode.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the EncoderMode register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteEncoderModeAsync(EncoderModeConfig value, CancellationToken cancellationToken = default)
        {
            var request = EncoderMode.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Camera0Frame register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<FrameAcquired> ReadCamera0FrameAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Camera0Frame.Address), cancellationToken);
            return Camera0Frame.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Camera0Frame register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<FrameAcquired>> ReadTimestampedCamera0FrameAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Camera0Frame.Address), cancellationToken);
            return Camera0Frame.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Camera0Frequency register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadCamera0FrequencyAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(Camera0Frequency.Address), cancellationToken);
            return Camera0Frequency.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Camera0Frequency register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedCamera0FrequencyAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(Camera0Frequency.Address), cancellationToken);
            return Camera0Frequency.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Camera0Frequency register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteCamera0FrequencyAsync(ushort value, CancellationToken cancellationToken = default)
        {
            var request = Camera0Frequency.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Camera1Frame register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<FrameAcquired> ReadCamera1FrameAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Camera1Frame.Address), cancellationToken);
            return Camera1Frame.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Camera1Frame register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<FrameAcquired>> ReadTimestampedCamera1FrameAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Camera1Frame.Address), cancellationToken);
            return Camera1Frame.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Camera1Frequency register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadCamera1FrequencyAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(Camera1Frequency.Address), cancellationToken);
            return Camera1Frequency.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Camera1Frequency register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedCamera1FrequencyAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(Camera1Frequency.Address), cancellationToken);
            return Camera1Frequency.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Camera1Frequency register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteCamera1FrequencyAsync(ushort value, CancellationToken cancellationToken = default)
        {
            var request = Camera1Frequency.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the ServoMotor2Period register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadServoMotor2PeriodAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(ServoMotor2Period.Address), cancellationToken);
            return ServoMotor2Period.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the ServoMotor2Period register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedServoMotor2PeriodAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(ServoMotor2Period.Address), cancellationToken);
            return ServoMotor2Period.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the ServoMotor2Period register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteServoMotor2PeriodAsync(ushort value, CancellationToken cancellationToken = default)
        {
            var request = ServoMotor2Period.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the ServoMotor2Pulse register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadServoMotor2PulseAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(ServoMotor2Pulse.Address), cancellationToken);
            return ServoMotor2Pulse.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the ServoMotor2Pulse register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedServoMotor2PulseAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(ServoMotor2Pulse.Address), cancellationToken);
            return ServoMotor2Pulse.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the ServoMotor2Pulse register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteServoMotor2PulseAsync(ushort value, CancellationToken cancellationToken = default)
        {
            var request = ServoMotor2Pulse.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the ServoMotor3Period register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadServoMotor3PeriodAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(ServoMotor3Period.Address), cancellationToken);
            return ServoMotor3Period.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the ServoMotor3Period register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedServoMotor3PeriodAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(ServoMotor3Period.Address), cancellationToken);
            return ServoMotor3Period.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the ServoMotor3Period register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteServoMotor3PeriodAsync(ushort value, CancellationToken cancellationToken = default)
        {
            var request = ServoMotor3Period.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the ServoMotor3Pulse register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadServoMotor3PulseAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(ServoMotor3Pulse.Address), cancellationToken);
            return ServoMotor3Pulse.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the ServoMotor3Pulse register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedServoMotor3PulseAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(ServoMotor3Pulse.Address), cancellationToken);
            return ServoMotor3Pulse.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the ServoMotor3Pulse register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteServoMotor3PulseAsync(ushort value, CancellationToken cancellationToken = default)
        {
            var request = ServoMotor3Pulse.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the EncoderReset register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<EncoderInputs> ReadEncoderResetAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(EncoderReset.Address), cancellationToken);
            return EncoderReset.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the EncoderReset register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<EncoderInputs>> ReadTimestampedEncoderResetAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(EncoderReset.Address), cancellationToken);
            return EncoderReset.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the EncoderReset register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteEncoderResetAsync(EncoderInputs value, CancellationToken cancellationToken = default)
        {
            var request = EncoderReset.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the EnableSerialTimestamp register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<byte> ReadEnableSerialTimestampAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(EnableSerialTimestamp.Address), cancellationToken);
            return EnableSerialTimestamp.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the EnableSerialTimestamp register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<byte>> ReadTimestampedEnableSerialTimestampAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(EnableSerialTimestamp.Address), cancellationToken);
            return EnableSerialTimestamp.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the EnableSerialTimestamp register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteEnableSerialTimestampAsync(byte value, CancellationToken cancellationToken = default)
        {
            var request = EnableSerialTimestamp.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the MimicPort0IR register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<MimicOutput> ReadMimicPort0IRAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(MimicPort0IR.Address), cancellationToken);
            return MimicPort0IR.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the MimicPort0IR register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<MimicOutput>> ReadTimestampedMimicPort0IRAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(MimicPort0IR.Address), cancellationToken);
            return MimicPort0IR.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the MimicPort0IR register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMimicPort0IRAsync(MimicOutput value, CancellationToken cancellationToken = default)
        {
            var request = MimicPort0IR.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the MimicPort1IR register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<MimicOutput> ReadMimicPort1IRAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(MimicPort1IR.Address), cancellationToken);
            return MimicPort1IR.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the MimicPort1IR register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<MimicOutput>> ReadTimestampedMimicPort1IRAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(MimicPort1IR.Address), cancellationToken);
            return MimicPort1IR.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the MimicPort1IR register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMimicPort1IRAsync(MimicOutput value, CancellationToken cancellationToken = default)
        {
            var request = MimicPort1IR.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the MimicPort2IR register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<MimicOutput> ReadMimicPort2IRAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(MimicPort2IR.Address), cancellationToken);
            return MimicPort2IR.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the MimicPort2IR register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<MimicOutput>> ReadTimestampedMimicPort2IRAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(MimicPort2IR.Address), cancellationToken);
            return MimicPort2IR.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the MimicPort2IR register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMimicPort2IRAsync(MimicOutput value, CancellationToken cancellationToken = default)
        {
            var request = MimicPort2IR.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the MimicPort0Valve register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<MimicOutput> ReadMimicPort0ValveAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(MimicPort0Valve.Address), cancellationToken);
            return MimicPort0Valve.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the MimicPort0Valve register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<MimicOutput>> ReadTimestampedMimicPort0ValveAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(MimicPort0Valve.Address), cancellationToken);
            return MimicPort0Valve.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the MimicPort0Valve register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMimicPort0ValveAsync(MimicOutput value, CancellationToken cancellationToken = default)
        {
            var request = MimicPort0Valve.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the MimicPort1Valve register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<MimicOutput> ReadMimicPort1ValveAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(MimicPort1Valve.Address), cancellationToken);
            return MimicPort1Valve.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the MimicPort1Valve register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<MimicOutput>> ReadTimestampedMimicPort1ValveAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(MimicPort1Valve.Address), cancellationToken);
            return MimicPort1Valve.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the MimicPort1Valve register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMimicPort1ValveAsync(MimicOutput value, CancellationToken cancellationToken = default)
        {
            var request = MimicPort1Valve.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the MimicPort2Valve register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<MimicOutput> ReadMimicPort2ValveAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(MimicPort2Valve.Address), cancellationToken);
            return MimicPort2Valve.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the MimicPort2Valve register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<MimicOutput>> ReadTimestampedMimicPort2ValveAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(MimicPort2Valve.Address), cancellationToken);
            return MimicPort2Valve.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the MimicPort2Valve register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMimicPort2ValveAsync(MimicOutput value, CancellationToken cancellationToken = default)
        {
            var request = MimicPort2Valve.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the PokeInputFilter register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<byte> ReadPokeInputFilterAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(PokeInputFilter.Address), cancellationToken);
            return PokeInputFilter.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the PokeInputFilter register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<byte>> ReadTimestampedPokeInputFilterAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(PokeInputFilter.Address), cancellationToken);
            return PokeInputFilter.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the PokeInputFilter register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePokeInputFilterAsync(byte value, CancellationToken cancellationToken = default)
        {
            var request = PokeInputFilter.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }
    }
}
