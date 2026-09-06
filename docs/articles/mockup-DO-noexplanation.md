## Control Digital Outputs

The Behavior board has four general-purpose digital outputs **DO0** – **DO3** on the **Output** screw terminal. Refer to the [connections](./connections.md) article to set up a digital output on **DO0**, which we will use for the rest of these examples.

This article covers how to set, clear, toggle, and pulse any of these outputs in Bonsai.

The complete workflow is shown below. Copy and paste it into Bonsai to get started. You can also copy and paste the individual examples in each section instead.

:::workflow
![Control Digital Outputs](../workflows/controldigitaloutputs-toplevel.bonsai)
:::

> [!NOTE]
> Besides the general purpose digital output lines (`DO0` -`DO3`), the commands below can be used to control the peripheral port output lines (`DOPort0`–`DOPort2`), valve lines (`SupplyPort0`–`SupplyPort2`), and LED lines (`Led0`, `Led1`, `Rgb0`, `Rgb1`).

[!INCLUDE [](breakout-note.md)]

### Set and Clear Outputs

Use the [`OutputSet`] and [`OutputClear`] registers to turn output lines on and off.

> [!NOTE]
> The examples in this article use key presses to generate the commands sent to the device. You can change which key triggers a command in the Filter property of the [`KeyDown`] operator. In the [`CreateMessage`] operator, the `Payload` field selects the register to target (e.g. `OutputSetPayload` for the [`OutputSet`] register), and the property of the same name (`OutputSet`) selects the output lines to act on (e.g. `DO0`). The examples come prefilled with sample values; remember to change them to suit your workflow.

:::workflow
![Set and Clear Outputs](../workflows/controldigitaloutputs-setclear.bonsai)
:::

Run the workflow, then press <kbd>A</kbd> to set the **DO0** line high and <kbd>S</kbd> to set it low.

> [!NOTE]
>  A single command can drive several outputs at once. To select multiple lines, type the names separated by a comma (e.g. `DO0`, `DO1`) in the payload field. Any lines not selected are left untouched. 

### Toggle Outputs

Use the [`OutputToggle`] register to invert the current state of the selected output lines.

:::workflow
![Toggle Outputs](../workflows/controldigitaloutputs-toggle.bonsai)
:::

Run the workflow and press <kbd>D</kbd> repeatedly. The **DO0** line inverts its state on every press.

### Write All Outputs

The [`OutputState`] register writes every output line in a single command: selected lines are set and all other lines are cleared. Use it to drive the whole output bank to a known state, for example when initializing an experiment.

:::workflow
![Write All Outputs](../workflows/controldigitaloutputs-state.bonsai)
:::

Run the workflow and press <kbd>G</kbd>. The **DO0** and **DO1** lines go high and every other output goes low. Try setting other outputs first with the commands from the previous sections; the [`OutputState`] write overrides them all.

> [!WARNING]
> [`OutputState`] acts on every output of the Behavior board, including the peripheral port and LED lines. To change a few lines while leaving the rest untouched, use [`OutputSet`], [`OutputClear`] or [`OutputToggle`] instead.

### Pulse Outputs

The device can generate hardware-timed pulses with a defined duration. Enable pulse mode by selecting output lines in the [`OutputPulseEnable`] register, and set the duration in the output line's pulse-duration register (e.g. [`PulseDO0`] for **DO0**, in milliseconds). Sending an [`OutputSet`] command then starts a pulse that turns off after the elapsed time instead of remaining on.

:::workflow
![Pulse Outputs](../workflows/controldigitaloutputs-pulse.bonsai)
:::

Run the workflow, press <kbd>F</kbd> once to configure the pulse, then send a set command with <kbd>A</kbd> from [Set and Clear Outputs](#set-and-clear-outputs). The **DO0** line goes high for 500 ms and returns low on its own.

[!INCLUDE [](outputpulseenable-warning.md)]

> [!TIP]
> The poke valve outputs (`SupplyPort0`–`SupplyPort2`) boot with pulse mode already enabled and a default duration of 15 ms; see [Deliver Rewards on Poke](control-poke.md#deliver-rewards-on-poke). All other outputs boot with pulse mode disabled and stay on until cleared.

> [!TIP]
> This method is preferred to timing pulses in software (e.g. pairing a set command with a delay and a clear command). Software timing is subject to non-real-time OS latency and jitter, which are most noticeable on short pulses.

### Alternative: Set Outputs with Timer

You can replace [`KeyDown`] with other operators to set outputs with other triggers in Bonsai, for instance a [`Timer`] to send triggers at a set time.

:::workflow
![Set Outputs Timer](../workflows/controldigitaloutputs-timer.bonsai)
:::

Run the workflow and the **DO0** line goes high after 2 seconds and turns off automatically if pulse mode is enabled.

[!INCLUDE [](version-footer.md)]

<!--Reference Style Links -->
[`KeyDown`]: xref:Bonsai.Windows.Input.KeyDown
[`CreateMessage`]: xref:Harp.Behavior.CreateMessage
[`Timer`]: xref:Bonsai.Reactive.Timer
[`OutputSet`]: xref:Harp.Behavior.OutputSet
[`OutputClear`]: xref:Harp.Behavior.OutputClear
[`OutputToggle`]: xref:Harp.Behavior.OutputToggle
[`OutputState`]: xref:Harp.Behavior.OutputState
[`OutputPulseEnable`]: xref:Harp.Behavior.OutputPulseEnable
[`PulseDO0`]: xref:Harp.Behavior.PulseDO0
