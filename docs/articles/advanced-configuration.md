## Advanced Configuration

These advanced configuration settings affect which data the Behavior board broadcasts. By default, all events are enabled, but you can selectively disable certain events. The device can also transmit serial timestamps on the peripheral port **P2** for logging and post-hoc alignment on non-Harp devices. Refer to the [connections](./connections.md) article to set up the hardware connection for the serial timestamp.

This article covers how to visualize the default event streams, select the active events, and stream serial timestamps in Bonsai.

The complete workflow is shown below. Copy and paste it into Bonsai or build each section by following the step-by-step instructions below.

:::workflow
![Advanced Configuration](../workflows/advancedconfiguration-toplevel.bonsai)
:::

### Visualize Analog Data

To see one of the default streams, visualize the [`AnalogData`] event, which reports the analog inputs at 1 kHz (see [Acquire Analog Data](acquire-analog-data.md) for more details):

:::workflow
![Visualize Analog Data](../workflows/advancedconfiguration-visualizeanalog.bonsai)
:::

- Insert a [`SubscribeSubject`] operator named `Behavior Events`.
- Insert a [`Parse`] operator and configure the `Register` property to `AnalogData`.
- Insert a [`VisualizerWindow`] operator. This will automatically open a window displaying the parsed events when the workflow starts.

Run the workflow. The visualizer immediately displays a rapid stream of [`AnalogData`] values, confirming that the event is enabled by default.

### Select Active Events

The [`EventEnable`] register selects which event streams the device broadcasts, write a new selection to select the streams you want to keep. Any event **not** selected in the payload is disabled.

:::workflow
![Select Active Events](../workflows/advancedconfiguration-eventenable.bonsai)
:::

- Insert a [`KeyDown`] operator and set the `Filter` property to `A`.
- Insert a [`CreateMessage`] operator and configure the following properties:
    - `Payload` - Select `EventEnablePayload`.
    - `EventEnable` - Enter `PortDI`, `PortDIO`, `Camera0`, `Camera1` to disable the `AnalogData` event and keep the rest.
- Insert a [`MulticastSubject`] operator named `Behavior Commands`.

Run the workflow and press <kbd>A</kbd>. The visualizer will stop showing any new [`AnalogData`](acquire-analog-data.md) events

### Stream Timestamps

The [`EnableSerialTimestamp`] register enables the serial timestamp stream, an auxiliary output that can be used by non-Harp devices for logging and post-hoc alignment. 

At the start of every second, the device transmits its current time in whole seconds as a 32-bit unsigned integer in 4 bytes, least significant byte first. The transmission format is serial (UART) data at 1000 bps, with 8 data bits, no parity, and one stop bit.

Record this signal on a digital input and decode it offline during postprocessing to align the data on the non-Harp device with other Harp data.

To enable the stream:

:::workflow
![Stream Timestamps](../workflows/advancedconfiguration-serialtimestamp.bonsai)
:::

- Insert a [`KeyDown`] operator and set the `Filter` property to `S`.
- Insert a [`CreateMessage`] operator and configure the following properties:
    - `Payload` - Select `EnableSerialTimestampPayload`.
    - `EnableSerialTimestamp` - Select `TimestampPort2`. On older package versions, if this dropdown option is not visible, enter `4` (this value enables the [`SerialTimestampPort`]).
- Insert a [`MulticastSubject`] operator named `Behavior Commands`.

Run the workflow and press <kbd>S</kbd>. The timestamp stream starts on the **P2** serial line at the next second boundary. Verify the signal on the digital input line on the external device, it should appear as a series of digital input transitions at one second intervals.

> [!NOTE]
> The serial timestamp stream is not the same as the [Harp synchronization clock](https://harp-tech.org/protocol/SynchronizationClock.html), which keeps connected Harp devices synchronized. 
The Behavior board does not generate the Harp synchronization clock and can only receive it. For a Harp synchronization clock generator, consider the [Harp Timestamp Generator Gen3](https://github.com/harp-tech/device.timestampgeneratorgen3/).

> [!NOTE]
> Shawn's note - The technical information about the format should enable someone to figure out how to reconstruct the timestamp, but to take someone through the whole process will probably require a tutorial. I am not sure how much to publicize this feature or work on a tutorial for it, as it might change https://github.com/harp-tech/protocol/issues/128.

[!INCLUDE [](version-footer.md)]

<!--Reference Style Links -->
[`KeyDown`]: xref:Bonsai.Windows.Input.KeyDown
[`SubscribeSubject`]: xref:Bonsai.Expressions.SubscribeSubject
[`Parse`]: xref:Harp.Behavior.Parse
[`VisualizerWindow`]: xref:Bonsai.Design.VisualizerWindow
[`AnalogData`]: xref:Harp.Behavior.AnalogData
[`CreateMessage`]: xref:Harp.Behavior.CreateMessage
[`MulticastSubject`]: xref:Bonsai.Expressions.MulticastSubject
[`EventEnable`]: xref:Harp.Behavior.EventEnable
[`EnableSerialTimestamp`]: xref:Harp.Behavior.EnableSerialTimestamp
[`SerialTimestampPort`]: xref:Harp.Behavior.SerialTimestampPorts