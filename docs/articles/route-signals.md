## Route Signals

The Behavior can route some of its signals to other hardware without a round-trip through the computer. The mimic registers ([`MimicPort0IR`], [`MimicPort0Valve`], and their Port 1/2 counterparts) copy a poke's infrared or valve state onto a digital output, and the [`EnableSerialTimestamp`] register streams the device clock over the Port 2 serial line. The examples below use Port 0 and the **DO0**/**DO2** outputs.

> [!WARNING]
> Only use `DO0` and `DO2` as mimic targets. In the current firmware, selecting `DIO0`–`DIO2` does not drive the DIO line, and selecting `DIO0`, `DIO2`, `DO1`, or `DO3` erroneously reconfigures the poke infrared inputs and stops poke detection on all ports until the device is reset.

The complete workflow is shown below:

:::workflow
![Route Signals](../workflows/routesignals-toplevel.bonsai)
:::

### Mimic Poke Inputs

When a mimic target is set for a poke's infrared beam, the target output follows the beam state in hardware — useful for triggering external equipment directly from a poke.

:::workflow
![Mimic Poke Inputs](../workflows/routesignals-mimicir.bonsai)
:::

- Insert a [`KeyDown`] operator and set the `Filter` property to `A`.
- Insert a [`CreateMessage`] operator and configure the following properties:
    - `Payload` - Select `MimicPort0IRPayload`.
    - `MimicPort0IR` - Select `DO0`.
- Insert a [`MulticastSubject`] operator named `Behavior Commands`.

In a separate branch:

- Insert a [`KeyDown`] operator and set the `Filter` property to `S`.
- Insert a [`CreateMessage`] operator and configure the following properties:
    - `Payload` - Select `MimicPort0IRPayload`.
    - `MimicPort0IR` - Select `None`.
- Insert a [`MulticastSubject`] operator named `Behavior Commands`.

Run the workflow, press <kbd>A</kbd>, and block the Port 0 infrared beam — **DO0** follows the beam state. Press <kbd>S</kbd> to remove the routing.

### Mimic Valves

The valve state can be routed the same way, for instance to record reward delivery on an external acquisition system:

:::workflow
![Mimic Valves](../workflows/routesignals-mimicvalve.bonsai)
:::

- Insert a [`KeyDown`] operator and set the `Filter` property to `D`.
- Insert a [`CreateMessage`] operator and configure the following properties:
    - `Payload` - Select `MimicPort0ValvePayload`.
    - `MimicPort0Valve` - Select `DO2`.
- Insert a [`MulticastSubject`] operator named `Behavior Commands`.

Run the workflow, press <kbd>D</kbd>, then open the Port 0 valve with [`OutputSet`](control-digital-outputs.md) — **DO2** follows the valve state, including [timed pulses](control-digital-outputs.md#pulse-outputs).

### Stream Timestamps

The [`EnableSerialTimestamp`] register enables a serial transmission of the device clock on the Port 2 connector: at the start of every second, the current Harp timestamp is sent as 4 bytes (32-bit seconds, least significant byte first) at 1000 bps. External equipment can decode this stream to align its own data with the Harp clock.

:::workflow
![Stream Timestamps](../workflows/routesignals-serialtimestamp.bonsai)
:::

- Insert a [`KeyDown`] operator and set the `Filter` property to `F`.
- Insert a [`CreateMessage`] operator and configure the following properties:
    - `Payload` - Select `EnableSerialTimestampPayload`.
    - `EnableSerialTimestamp` - Select `TimestampPort2`.
- Insert a [`MulticastSubject`] operator named `Behavior Commands`.

Run the workflow and press <kbd>F</kbd> — the timestamp stream starts on the Port 2 serial line at the next second boundary.

[!INCLUDE [](version-footer.md)]

<!--Reference Style Links -->
[`KeyDown`]: xref:Bonsai.Windows.Input.KeyDown
[`CreateMessage`]: xref:Harp.Behavior.CreateMessage
[`MulticastSubject`]: xref:Bonsai.Expressions.MulticastSubject
[`MimicPort0IR`]: xref:Harp.Behavior.MimicPort0IR
[`MimicPort0Valve`]: xref:Harp.Behavior.MimicPort0Valve
[`EnableSerialTimestamp`]: xref:Harp.Behavior.EnableSerialTimestamp
