# Mimic Poke Signals

The Behavior board can mimic or mirror the poke peripheral's infrared beam signal and valve output on other output lines. This is useful for triggering external equipment directly or recording reward delivery on an external acquisition system without the round-trip latency introduced by routing signals through the computer.

Refer to the [Mice Poke connection](./connections.md?tabs=poke#connections) section to set up the [Mice Poke](./peripherals/peripherals-micepoke.md) peripheral on port **P0** and the [Digital Output connection](./connections.md?tabs=digitaloutput#connections) section to connect an LED to **DO0** as an indicator. We will use these ports for the rest of the exercises.

> [!WARNING]
> Due to a [bug](https://github.com/harp-tech/device.behavior/issues/44) in the current firmware (v3.3), only `DO0` and `DO2` can be used as mimic output lines.

This article covers how to mimic the poke peripheral's infrared signal and valve outputs in Bonsai.

The complete workflow is shown below. Copy and paste it into Bonsai to get started immediately, or build each section by following the step-by-step instructions in the following sections.

:::workflow
![Mimic Poke Signals](../workflows/mimicpokesignals-toplevel.bonsai)
:::

## Enable poke infrared mimic

To mimic the infrared signal on the poke ports, first select the mimic infrared port register that corresponds to the port (e.g. [`MimicPort0IR`] for **P0**). Then, specify an output line to mimic the signal on (e.g. `DO0` for **DO0**). No further configuration is necessary as the poke infrared detector is enabled by default.

:::workflow
![Enable Poke Infrared Mimic](../workflows/mimicpokesignals-mimicir.bonsai)
:::

- Insert a [`KeyDown`] operator and set the `Filter` property to `A`.
- Insert a [`CreateMessage`] operator and configure the following properties:
    - `Payload` - Select `MimicPort0IRPayload`.
    - `MimicPort0IR` - Select the output line to mimic the signal on (e.g. `DO0`). To disable the mimic in the future, select `None`.
- Insert a [`MulticastSubject`] operator named `Behavior Commands`.

Run the workflow, press <kbd>A</kbd> to enable the poke infrared mimic, and trigger the infrared poke detector on **P0**. The LED indicator connected to **DO0** should follow the beam state.

> [!WARNING]
> A mimic output line still responds to commands that modify digital outputs, such as [`OutputSet`] and [`OutputClear`]. Other mimic registers can also select the same line. To avoid conflicts, dedicate each mimic output line to a single source and clear any leftover configurations.

## Enable poke valve mimic

To mimic the valve output signal on the poke ports, first select the mimic valve register that corresponds to the port (e.g. [`MimicPort0Valve`] for **P0**). Then, specify an output line to mimic the signal on (e.g. `DO0` for **DO0**). The second branch in the workflow adds a keyboard trigger to turn on the valve output and demonstrate the mimic functionality.

:::workflow
![Enable Poke Valve Mimic](../workflows/mimicpokesignals-mimicvalve.bonsai)
:::

- Insert a [`KeyDown`] operator and set the `Filter` property to `S`.
- Insert a [`CreateMessage`] operator and configure the following properties:
    - `Payload` - Select `MimicPort0ValvePayload`.
    - `MimicPort0Valve` - Select the output line to mimic the signal on (e.g. `DO0`). To disable the mimic in the future, select `None`.
- Insert a [`MulticastSubject`] operator named `Behavior Commands`.

To trigger the valve output, send an [`OutputSet`] command in a separate branch:

- Insert a [`KeyDown`] operator and set the `Filter` property to `D`.
- Insert a [`CreateMessage`] operator and configure the following properties:
    - `Payload` - Select `OutputSetPayload`.
    - `OutputSet` - Select the valve to open (e.g. `SupplyPort0` for **P0**).
- Insert a [`MulticastSubject`] operator named `Behavior Commands`.

Run the workflow, press <kbd>S</kbd>, then press <kbd>D</kbd> to open the **P0** valve. The valve supply lines boot with [pulse mode](control-digital-outputs.md#pulse-outputs) enabled, so the LED indicator on **DO0** should flash briefly.

[!INCLUDE [](../includes/version-footer.md)]

<!--Reference Style Links -->
[`KeyDown`]: xref:Bonsai.Windows.Input.KeyDown
[`CreateMessage`]: xref:Harp.Behavior.CreateMessage
[`MulticastSubject`]: xref:Bonsai.Expressions.MulticastSubject
[`MimicPort0IR`]: xref:Harp.Behavior.MimicPort0IR
[`MimicPort0Valve`]: xref:Harp.Behavior.MimicPort0Valve
[`OutputSet`]: xref:Harp.Behavior.OutputSet
[`OutputClear`]: xref:Harp.Behavior.OutputClear
