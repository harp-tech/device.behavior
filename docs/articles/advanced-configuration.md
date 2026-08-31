## Advanced Configuration

This article covers device-wide configuration that goes beyond a single peripheral or output: settings that affect which data the Behavior board broadcasts.

This article covers how to select the active events and stream timestamps in Bonsai.

The complete workflow is shown below:

:::workflow
![Advanced Configuration](../workflows/advancedconfiguration-toplevel.bonsai)
:::

### Select Active Events

The [`EventEnable`] register selects which event streams the device broadcasts. All events are enabled by default; write a new selection to silence the streams you don't need.

:::workflow
![Select Active Events](../workflows/advancedconfiguration-eventenable.bonsai)
:::

- Insert a [`KeyDown`] operator and set the `Filter` property to `A`.
- Insert a [`CreateMessage`] operator and configure the following properties:
    - `Payload` - Select `EventEnablePayload`.
    - `EventEnable` - Select `PortDI` and `PortDIO` to keep only the poke and DIO events.
- Insert a [`MulticastSubject`] operator named `Behavior Commands`.

Run the workflow and press <kbd>A</kbd>. The device stops broadcasting [`AnalogData`](acquire-analog-data.md) and camera frame events, and keeps the [poke events](control-poke.md).

> [!WARNING]
> [`EventEnable`] writes the whole selection at once: any event **not** selected in the payload is disabled, including the 1 kHz analog stream and the camera frame events.

### Stream Timestamps

The [`EnableSerialTimestamp`] register enables a serial transmission of the device clock on the Port 2 connector: at the start of every second, the current Harp timestamp is sent as 4 bytes (32-bit seconds, least significant byte first) at 1000 bps. External equipment can decode this stream to align its own data with the Harp clock.

:::workflow
![Stream Timestamps](../workflows/advancedconfiguration-serialtimestamp.bonsai)
:::

- Insert a [`KeyDown`] operator and set the `Filter` property to `S`.
- Insert a [`CreateMessage`] operator and configure the following properties:
    - `Payload` - Select `EnableSerialTimestampPayload`.
    - `EnableSerialTimestamp` - Select `TimestampPort2`.
- Insert a [`MulticastSubject`] operator named `Behavior Commands`.

Run the workflow and press <kbd>S</kbd>. The timestamp stream starts on the Port 2 serial line at the next second boundary.

[!INCLUDE [](version-footer.md)]

<!--Reference Style Links -->
[`KeyDown`]: xref:Bonsai.Windows.Input.KeyDown
[`CreateMessage`]: xref:Harp.Behavior.CreateMessage
[`MulticastSubject`]: xref:Bonsai.Expressions.MulticastSubject
[`EventEnable`]: xref:Harp.Behavior.EventEnable
[`EnableSerialTimestamp`]: xref:Harp.Behavior.EnableSerialTimestamp
