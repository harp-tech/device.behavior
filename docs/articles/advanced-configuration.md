# Advanced Configuration

These advanced configuration settings affect which data the Behavior board reports. By default, all events are enabled, but you can selectively disable certain events. The device can also transmit serial timestamps on the poke port **P2** for logging and post-hoc alignment on non-Harp devices. Refer to the [connections](./connections.md?tabs=breakout#connections) article to set up the [Breakout](./peripherals/peripherals-portbreakout.md) board on **P2** for the serial timestamp.

This article covers how to visualize the default event streams, select the active events, and transmit serial timestamps in Bonsai.

The complete workflow is shown below. Copy and paste it into Bonsai to get started immediately, or build each section by following the step-by-step instructions in the following sections.

:::workflow
![Advanced Configuration](../workflows/advancedconfiguration-toplevel.bonsai)
:::

## Visualize Analog Data

This workflow uses [`AnalogData`] as an example to demonstrate how to disable a data stream that is enabled by default. First, visualize the [`AnalogData`] stream, which reports the analog inputs at 1 kHz (see [Acquire Analog Data](acquire-analog-data.md) for more details):

:::workflow
![Visualize Analog Data](../workflows/advancedconfiguration-visualizeanalog.bonsai)
:::

- Insert a [`SubscribeSubject`] operator named `Behavior Events`.
- Insert a [`Parse`] operator and configure the `Register` property to `AnalogData`.
- Insert a [`VisualizerWindow`] operator.

Run the workflow. The visualizer immediately displays a rapid stream of [`AnalogData`] values because this event stream is enabled by default.

## Enable Event Streams

The [`EventEnable`] register selects which event streams the device reports. All five are enabled by default: `PortDI`, `PortDIO`, `Camera0`, `Camera1` and `AnalogData`. Write a new selection to choose the streams you want to keep; any event not specified is disabled.

:::workflow
![Enable Event Streams](../workflows/advancedconfiguration-eventenable.bonsai)
:::

- Insert a [`KeyDown`] operator and set the `Filter` property to `A`.
- Insert a [`CreateMessage`] operator and configure the following properties:
    - `Payload` - Select `EventEnablePayload`.
    - `EventEnable` - Enter `PortDI, PortDIO, Camera0, Camera1` to disable the `AnalogData` event and keep the rest.
- Insert a [`MulticastSubject`] operator named `Behavior Commands`.

Run the workflow and press <kbd>A</kbd>. The visualizer will stop showing any new [`AnalogData`](acquire-analog-data.md) events.

## Transmit Serial Timestamps

The [`EnableSerialTimestamp`] register enables the serial timestamp transmission, an auxiliary output that can be used by non-Harp devices to log Harp timestamps for time alignment.

At the start of every second, the device transmits its current time in whole seconds as a 32-bit unsigned integer in 4 bytes, least significant byte first. The transmission format is serial (UART) data at 1000 bps, with 8 data bits, no parity, and one stop bit.

Record this signal on a general purpose digital input port of an external device and decode it to align the data on the non-Harp device with other Harp data.

To enable the transmission:

:::workflow
![Transmit Serial Timestamps](../workflows/advancedconfiguration-serialtimestamp.bonsai)
:::

- Insert a [`KeyDown`] operator and set the `Filter` property to `S`.
- Insert a [`CreateMessage`] operator and configure the following properties:
    - `Payload` - Select `EnableSerialTimestampPayload`.
    - `EnableSerialTimestamp` - Select `TimestampPort2`. On older package versions, if this dropdown option is not visible, enter `4` (this value enables the [`SerialTimestampPort`]).
- Insert a [`MulticastSubject`] operator named `Behavior Commands`.

Run the workflow and press <kbd>S</kbd>. The timestamp transmission on the **P2** serial line begins at the starting boundary of the next second of the Harp clock.  Verify the signal on the digital input line on the external device, it should appear as a series of digital input transitions at one second intervals.

> [!NOTE]
> The serial timestamp transmission is not the same as the [Harp synchronization clock](https://harp-tech.org/protocol/SynchronizationClock.html), which keeps connected Harp devices synchronized. The Behavior board does not generate the Harp synchronization clock and can only receive it. For a Harp synchronization clock generator, consider the [Harp Timestamp Generator Gen3](https://github.com/harp-tech/device.timestampgeneratorgen3/).

[!INCLUDE [](../includes/version-footer.md)]

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