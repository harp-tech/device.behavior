## Control Digital Outputs

The Behavior board has four general-purpose digital outputs **DO0** – **DO3** on the **Output** screw terminal. Refer to the [connections](./connections.md) article to set up a digital output on **DO0**, which we will use for the rest of these examples.

This article covers how to set, clear, toggle, and pulse any of these outputs in Bonsai.

The complete workflow is shown below. Copy and paste it into Bonsai or build each section by following the step-by-step instructions below.

:::workflow
![Control Digital Outputs](../workflows/controldigitaloutputs-toplevel.bonsai)
:::

> [!NOTE]
> Besides the general purpose digital output lines (`DO0` -`DO3`), the commands below can be used to control the peripheral port output lines (`DOPort0`–`DOPort2`), valve lines (`SupplyPort0`–`SupplyPort2`), and LED lines (`Led0`, `Led1`, `Rgb0`, `Rgb1`).

[!INCLUDE [](breakout-note.md)]

### Set and Clear Outputs

Use the [`OutputSet`] and [`OutputClear`] registers to turn output lines on and off.

:::workflow
![Set and Clear Outputs](../workflows/controldigitaloutputs-setclear.bonsai)
:::

<details>
<summary>Step-by-step instructions</summary>

- Insert a [`KeyDown`] source and set the `Filter` property to `A`. Every time the letter A is pressed on the keyboard, this triggers the command generation.
- Insert a [`CreateMessage`] operator to construct the [`HarpMessage`] command that will be sent to the Behavior board once the key is pressed. Configure these properties to define that you want to turn an output line on, and which line it is:
    - `Payload` - Select `OutputSetPayload`.
    - `OutputSet` - Select `DO0`.
- Insert a [`MulticastSubject`] operator named `Behavior Commands`. This makes the output of this section feed into the Behavior device pattern we defined at the top of the workflow to actually send the commands to the device.

In a separate branch:

- Insert a [`KeyDown`] source and set the `Filter` property to `S`. This triggers the opposite command.
- Insert a [`CreateMessage`] operator and configure these properties to define that you want to turn an output line off, and which line it is:
    - `Payload` - Select `OutputClearPayload`.
    - `OutputClear` - Select `DO0`.
- Insert a [`MulticastSubject`] operator named `Behavior Commands`. This sends the clear command through the same device pattern.

</details>

Run the workflow, then press <kbd>A</kbd> to set the **DO0** line high and <kbd>S</kbd> to set it low.

> [!NOTE]
>  A single command can drive several outputs at once. To select multiple lines, type the names separated by a comma (e.g. `DO0`, `DO1`) in the payload field. Any lines not selected are left untouched. 

### Toggle Outputs

The [`OutputToggle`] register inverts the current state of the selected output lines.

:::workflow
![Toggle Outputs](../workflows/controldigitaloutputs-toggle.bonsai)
:::

<details>
<summary>Step-by-step instructions</summary>

- Insert a [`KeyDown`] source and set the `Filter` property to `D`. Every time the letter D is pressed on the keyboard, this triggers the command generation.
- Insert a [`CreateMessage`] operator to construct the command. Configure these properties to define that you want to invert the state of an output line, and which line it is:
    - `Payload` - Select `OutputTogglePayload`.
    - `OutputToggle` - Select `DO0`.
- Insert a [`MulticastSubject`] operator named `Behavior Commands`. This feeds the command into the Behavior device pattern to send it to the device.

</details>

Run the workflow and press <kbd>D</kbd> repeatedly. The **DO0** line inverts its state on every press.

### Write All Outputs

The [`OutputState`] register writes every output line in a single command: selected lines are set and all other lines are cleared. Use it to drive the whole output bank to a known state, for example when initializing an experiment.

:::workflow
![Write All Outputs](../workflows/controldigitaloutputs-state.bonsai)
:::

<details>
<summary>Step-by-step instructions</summary>

- Insert a [`KeyDown`] source and set the `Filter` property to `G`. Every time the letter G is pressed on the keyboard, this triggers the command generation.
- Insert a [`CreateMessage`] operator to construct the command. Configure these properties to define the new state of the whole output bank, naming the lines that go high:
    - `Payload` - Select `OutputStatePayload`.
    - `OutputState` - Select `DO0` and `DO1`. Every line not selected here is cleared.
- Insert a [`MulticastSubject`] operator named `Behavior Commands`. This feeds the command into the Behavior device pattern to send it to the device.

</details>

Run the workflow and press <kbd>G</kbd>. The **DO0** and **DO1** lines go high and every other output goes low. Try setting other outputs first with the commands from the previous sections; the [`OutputState`] write overrides them all.

> [!WARNING]
> [`OutputState`] acts on every output of the Behavior board, including the peripheral port and LED lines. To change a few lines while leaving the rest untouched, use [`OutputSet`], [`OutputClear`] or [`OutputToggle`] instead.

### Pulse Outputs

The device can generate hardware-timed pulses with a defined duration. Enable pulse mode by selecting output lines in the [`OutputPulseEnable`] register, and set the duration in the output line's pulse-duration register (e.g. [`PulseDO0`] for **DO0**, in milliseconds). Sending an [`OutputSet`] command then starts a pulse that turns off after the elapsed time instead of remaining on.

:::workflow
![Pulse Outputs](../workflows/controldigitaloutputs-pulse.bonsai)
:::

<details>
<summary>Step-by-step instructions</summary>

- Insert a [`KeyDown`] source and set the `Filter` property to `F`. Every time the letter F is pressed on the keyboard, this triggers both configuration commands.
- Insert a [`CreateMessage`] operator to construct the first configuration command. Configure these properties to define that you want pulse mode enabled, and on which line:
    - `Payload` - Select `OutputPulseEnablePayload`.
    - `OutputPulseEnable` - Select `DO0` to enable pulse mode on **DO0**.
- Insert a [`MulticastSubject`] operator named `Behavior Commands`. This feeds the command into the Behavior device pattern to send it to the device.
- Insert a second [`CreateMessage`] operator to construct the second configuration command. Configure these properties to define how long each pulse on that line lasts:
    - `Payload` - Select `PulseDO0Payload`.
    - `PulseDO0` - Set the pulse duration to 500 ms.
- Insert a [`MulticastSubject`] operator named `Behavior Commands`. This sends the duration command through the same device pattern.

</details>

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

<details>
<summary>Step-by-step instructions</summary>

- Insert a [`Timer`] source and set the `DueTime` property to the number of seconds to wait before setting the output (e.g. 2 seconds). When the time elapses, the [`Timer`] triggers the command generation, in place of a key press.
- Insert a [`CreateMessage`] operator and configure the following properties:
    - `Payload` - Select `OutputSetPayload`.
    - `OutputSet` - Select `DO0`.
- Insert a [`MulticastSubject`] operator named `Behavior Commands`. This feeds the command into the Behavior device pattern to send it to the device.

</details>

Run the workflow and the **DO0** line goes high after 2 seconds and turns off automatically if pulse mode is enabled.

[!INCLUDE [](version-footer.md)]

<!--Reference Style Links -->
[`KeyDown`]: xref:Bonsai.Windows.Input.KeyDown
[`HarpMessage`]: xref:Bonsai.Harp.HarpMessage
[`Timer`]: xref:Bonsai.Reactive.Timer
[`CreateMessage`]: xref:Harp.Behavior.CreateMessage
[`MulticastSubject`]: xref:Bonsai.Expressions.MulticastSubject
[`OutputSet`]: xref:Harp.Behavior.OutputSet
[`OutputClear`]: xref:Harp.Behavior.OutputClear
[`OutputToggle`]: xref:Harp.Behavior.OutputToggle
[`OutputState`]: xref:Harp.Behavior.OutputState
[`OutputPulseEnable`]: xref:Harp.Behavior.OutputPulseEnable
[`PulseDO0`]: xref:Harp.Behavior.PulseDO0
