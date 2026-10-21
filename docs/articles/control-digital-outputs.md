# Control Digital Outputs

The Behavior board has four general-purpose digital outputs **DO0**–**DO3** on the **Output** connector.  Refer to the [connections](./connections.md?tabs=digitaloutput#connections) article to connect an LED to **DO0** as an indicator, which we will use for the rest of these examples.

This article covers how to set, clear, toggle, and pulse any of these outputs in Bonsai.

The complete workflow is shown below. Copy and paste it into Bonsai to get started immediately, or build each section by following the step-by-step instructions in the following sections.

:::workflow
![Control Digital Outputs](../workflows/controldigitaloutputs-toplevel.bonsai)
:::

> [!NOTE]
> The commands in these examples can be used to control every output line of the Behavior board. Their location on the Behavior board and names in Bonsai are:
> - General-purpose digital outputs **DO0**–**DO3**: `DO0` - `DO3`.
> - Poke ports **P0**–**P2**: the 5 V digital output lines are represented as `DOPort0`–`DOPort2` and 12 V digital output lines for valve control as `SupplyPort0`–`SupplyPort2`. See [Control Poke Peripheral](control-poke.md).
> - LED connectors **L0**, **L1** and **RGB**: represented as `Led0`, `Led1`, `Rgb0` and `Rgb1` in Bonsai. See [Control LEDs](control-leds.md).

> [!NOTE]
> If you need more digital outputs, the [Breakout](./peripherals/peripherals-portbreakout.md) board provides access to the poke port output lines through screw terminals.

## Set and clear outputs

Use the [`OutputSet`] and [`OutputClear`] registers to set the output lines high and low.

:::workflow
![Set and Clear Outputs](../workflows/controldigitaloutputs-setclear.bonsai)
:::

- Insert a [`KeyDown`] operator and set the `Filter` property to `A`.
- Insert a [`CreateMessage`] operator and configure the following properties:
    - `Payload` - Select `OutputSetPayload`.
    - `OutputSet` - Select the output line to set high (e.g. `DO0`).
- Insert a [`MulticastSubject`] operator named `Behavior Commands`.

In a separate branch:

- Insert a [`KeyDown`] operator and set the `Filter` property to `S`.
- Insert a [`CreateMessage`] operator and configure the following properties:
    - `Payload` - Select `OutputClearPayload`.
    - `OutputClear` - Select the output line to set low (e.g. `DO0`).
- Insert a [`MulticastSubject`] operator named `Behavior Commands`.

Run the workflow, then press <kbd>A</kbd> to set the **DO0** line high to turn the LED on and <kbd>S</kbd> to set it low and turn the LED off.

> [!NOTE]
> A single command can modify several outputs at once. To set or clear multiple lines simultaneously, type the names separated by a comma (e.g. `DO0, DO1`) in the `OutputSet` or `OutputClear` field. Output lines not specified remain unchanged.

## Toggle outputs

The [`OutputToggle`] register inverts the current state of the selected output lines.

:::workflow
![Toggle Outputs](../workflows/controldigitaloutputs-toggle.bonsai)
:::

- Insert a [`KeyDown`] operator and set the `Filter` property to `D`.
- Insert a [`CreateMessage`] operator and configure the following properties:
    - `Payload` - Select `OutputTogglePayload`.
    - `OutputToggle` - Select the output line to toggle (e.g. `DO0`).
- Insert a [`MulticastSubject`] operator named `Behavior Commands`.

Run the workflow and press <kbd>D</kbd> repeatedly. The **DO0** line inverts its state on every press. With an LED, it alternates between on and off.

## Write all outputs

The [`OutputState`] register writes every output line in a single command: selected lines are set high and all other lines are set low. Use it to set the whole output bank to a known state, for example when initializing an experiment.

:::workflow
![Write All Outputs](../workflows/controldigitaloutputs-state.bonsai)
:::

- Insert a [`KeyDown`] operator and set the `Filter` property to `F`.
- Insert a [`CreateMessage`] operator and configure the following properties:
    - `Payload` - Select `OutputStatePayload`.
    - `OutputState` - Specify the output lines to set high (e.g. `DO0, DO1`). All other lines are set low.
- Insert a [`MulticastSubject`] operator named `Behavior Commands`.

Run the workflow and press <kbd>F</kbd>. The **DO0** and **DO1** lines go high and every other output goes low. Try setting other outputs first with the commands from the previous sections; the [`OutputState`] write overrides them all. The LED on **DO0** turns on and stays lit.

> [!WARNING]
> [`OutputState`] acts on every output of the Behavior board, including the poke port and LED lines. To change a few lines without modifying the rest, specify multiple lines in [`OutputSet`], [`OutputClear`], or [`OutputToggle`].

## Pulse outputs

The Behavior board can generate hardware-timed pulses with a defined duration. Enable pulse mode by selecting output lines in the [`OutputPulseEnable`] register, and set the duration in the output line's pulse-duration register (e.g. [`PulseDO0`] for **DO0**, in milliseconds).
Once the output line is configured in pulse mode, sending an [`OutputSet`] command sets the line high and then low after the set time has elapsed. This ensures precise pulse timing by eliminating the delay associated with sending a separate command to set the line low.

:::workflow
![Pulse Outputs](../workflows/controldigitaloutputs-pulse.bonsai)
:::

- Insert a [`KeyDown`] operator and set the `Filter` property to `G`.
- Insert a [`CreateMessage`] operator and configure the following properties:
    - `Payload` - Select `OutputPulseEnablePayload`.
    - `OutputPulseEnable` - Select the line to enable pulse mode (e.g. `DO0`). 
- Insert a [`MulticastSubject`] operator named `Behavior Commands`.
- Insert a [`CreateMessage`] operator and configure the following properties:
    - `Payload` - Select `PulseDO0Payload`.
    - `PulseDO0` - Set the pulse duration in ms (e.g. `500`).
- Insert a [`MulticastSubject`] operator named `Behavior Commands`.

Run the workflow, press <kbd>G</kbd> once to configure the pulse, then send a set command with <kbd>A</kbd> from [Set and Clear Outputs](#set-and-clear-outputs). The **DO0** line goes high for 500 ms and returns to low on its own, which translates to a brief flash on the LED.

[!INCLUDE [](outputpulseenable-warning.md)]

> [!TIP]
> The poke valve outputs (`SupplyPort0`–`SupplyPort2`) boot with pulse mode already enabled and a default duration of 15 ms; see [Deliver Rewards on Poke](control-poke.md#deliver-rewards-on-poke). All other outputs boot with pulse mode disabled and stay on until cleared.

> [!TIP]
> Pulse mode is preferred to timing pulses in software (e.g. pairing a set command with a delay and a clear command). Software timing is subject to non-real-time OS latency and jitter, which are most noticeable on short pulses.

## Alternative: set outputs with Timer

You can replace [`KeyDown`] with other operators to set outputs with other triggers in Bonsai, for instance a [`Timer`] to send triggers after a set time has elapsed.

:::workflow
![Set Outputs Timer](../workflows/controldigitaloutputs-timer.bonsai)
:::

- Insert a [`Timer`] operator and configure the following properties:
    - `DueTime` - Set the number of seconds to wait after the workflow starts to set the output (e.g. `00:00:02` for two seconds).
    - `Period` - Leave this unspecified so that the [`Timer`] only triggers once.
- Insert a [`CreateMessage`] operator and configure the following properties:
    - `Payload` - Select `OutputSetPayload`.
    - `OutputSet` - Select the output line to set high (e.g. `DO0`).
- Insert a [`MulticastSubject`] operator named `Behavior Commands`.

Run the workflow. The **DO0** line goes high 2 seconds after the workflow starts. It will go low automatically if pulse mode is enabled. With an LED on **DO0**, it lights 2 seconds after you start the workflow.

[!INCLUDE [](version-footer.md)]

<!--Reference Style Links -->
[`KeyDown`]: xref:Bonsai.Windows.Input.KeyDown
[`Timer`]: xref:Bonsai.Reactive.Timer
[`CreateMessage`]: xref:Harp.Behavior.CreateMessage
[`MulticastSubject`]: xref:Bonsai.Expressions.MulticastSubject
[`OutputSet`]: xref:Harp.Behavior.OutputSet
[`OutputClear`]: xref:Harp.Behavior.OutputClear
[`OutputToggle`]: xref:Harp.Behavior.OutputToggle
[`OutputState`]: xref:Harp.Behavior.OutputState
[`OutputPulseEnable`]: xref:Harp.Behavior.OutputPulseEnable
[`PulseDO0`]: xref:Harp.Behavior.PulseDO0
