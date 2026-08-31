## Mimic Poke Signals

The Behavior board can mimic or mirror the poke peripheral's infrared port inputs and valve output on other output lines. This is useful for triggering external equipment directly or recording reward delivery on an external acquisition system and avoids round-trip latency from going through the computer. 

Refer to the [connections](./connections.md) article to set up the [Mice Poke](./peripherals/peripherals-micepoke.md) peripheral on port **P0** and connect an indicator (like an LED) to **DO0**, which we will use for the rest of the examples.

> [!WARNING]
> Due to a firmware bug, only `DO0` and `DO2` can be used as mimic targets.

This article covers how to mimic poke inputs and valve outputs in Bonsai.

The complete workflow is shown below:

:::workflow
![Mimic Signals](../workflows/mimicsignals-toplevel.bonsai)
:::

### Mimic Poke Inputs

Set a mimic target output line for the poke's infrared inputs with the [`MimicPort0IR`] register (ports 1 and 2 have their own counterparts).

:::workflow
![Mimic Poke Inputs](../workflows/mimicsignals-mimicir.bonsai)
:::

- Insert a [`KeyDown`] operator and set the `Filter` property to `A`.
- Insert a [`CreateMessage`] operator and configure the following properties:
    - `Payload` - Select `MimicPort0IRPayload`.
    - `MimicPort0IR` - Select `DO0` for the target output line.
- Insert a [`MulticastSubject`] operator named `Behavior Commands`.

In a separate branch:

- Insert a [`KeyDown`] operator and set the `Filter` property to `S`.
- Insert a [`CreateMessage`] operator and configure the following properties:
    - `Payload` - Select `MimicPort0IRPayload`.
    - `MimicPort0IR` - Select `None`.
- Insert a [`MulticastSubject`] operator named `Behavior Commands`.

Run the workflow, press <kbd>A</kbd> to enable the mimic target, and trigger the infrared poke detector on **P0**. The indicator on **DO0** should follow the beam state. Press <kbd>S</kbd> to remove the mimic target configuration.

> [!WARNING]
> A mimic target line still responds to commands that modify digital outputs, such as [`OutputSet`] and [`OutputClear`]. Other mimic registers can also select the same target. To avoid confusion, we recommend dedicating each mimic target to mirroring a single source and clearing any left over configurations.

### Mimic Poke Valves

Similarly, to mimic the valve output, set a mimic target in the [`MimicPort0Valve`] register.

:::workflow
![Mimic Poke Valves](../workflows/mimicsignals-mimicvalve.bonsai)
:::

- Insert a [`KeyDown`] operator and set the `Filter` property to `D`.
- Insert a [`CreateMessage`] operator and configure the following properties:
    - `Payload` - Select `MimicPort0ValvePayload`.
    - `MimicPort0Valve` - Select `DO0`.
- Insert a [`MulticastSubject`] operator named `Behavior Commands`.

In a separate branch:

- Insert a [`KeyDown`] operator and set the `Filter` property to `F`.
- Insert a [`CreateMessage`] operator and configure the following properties:
    - `Payload` - Select `MimicPort0ValvePayload`.
    - `MimicPort0Valve` - Select `None`.
- Insert a [`MulticastSubject`] operator named `Behavior Commands`.

To trigger the valve output, send a [`OutputSet`] command in a separate branch:

- Insert a [`KeyDown`] operator and set the `Filter` property to `G`.
- Insert a [`CreateMessage`] operator and configure the following properties:
    - `Payload` - Select `OutputSetPayload`.
    - `OutputSet` - Select `SupplyPort0` to open the Port 0 valve.
- Insert a [`MulticastSubject`] operator named `Behavior Commands`.

Run the workflow, press <kbd>D</kbd>, then press <kbd>G</kbd> to open the **P0** valve. **DO0** follows the valve state, including [timed pulses](control-digital-outputs.md#pulse-outputs). Press <kbd>F</kbd> to remove the mimic target configuration.

[!INCLUDE [](version-footer.md)]

<!--Reference Style Links -->
[`KeyDown`]: xref:Bonsai.Windows.Input.KeyDown
[`CreateMessage`]: xref:Harp.Behavior.CreateMessage
[`MulticastSubject`]: xref:Bonsai.Expressions.MulticastSubject
[`MimicPort0IR`]: xref:Harp.Behavior.MimicPort0IR
[`MimicPort0Valve`]: xref:Harp.Behavior.MimicPort0Valve
[`OutputSet`]: xref:Harp.Behavior.OutputSet
[`OutputClear`]: xref:Harp.Behavior.OutputClear
