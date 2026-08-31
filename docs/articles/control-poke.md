## Control Poke Peripheral

The [Mice Poke](./peripherals/peripherals-micepoke.md) is a specialized nose poke peripheral for the Behavior board which combines an infrared beam sensor for poke detection, cue LEDs, and a solenoid valve for reward delivery. Refer to the [connections](./connections.md) article to set up the hardware connection on peripheral port **P0**, which we will use for the rest of these examples.

This article covers how to visualize poke events, debounce noisy inputs, drive the cue LED, and deliver rewards with the Mice Poke peripheral in Bonsai.

The complete workflow is shown below:

:::workflow
![Detect Pokes](../workflows/controlpoke-toplevel.bonsai)
:::

> [!NOTE]
> To interface with other external devices or accessories, the [Breakout](./peripherals/peripherals-portbreakout.md) extension board makes the peripheral ports' pins available as regular screw-terminal inputs and outputs.

### Visualize Poke Events

Beam breaks in the Mice Poke peripheral are reported as digital input events.  The three peripheral ports **P0 - P2** are represented as `DIPort0` - `DIPort2` in the [`DigitalInputState`] event register.

:::workflow
![Detect Pokes Visualize Events](../workflows/controlpoke-visualizeevents.bonsai)
:::

- Insert a [`SubscribeSubject`] operator named `Behavior Events`. This will listen to [`HarpMessages`] broadcast from the [`PublishSubject`] named `Behavior Events` in the Harp device pattern.
- Insert a [`Parse`] operator and configure the `Register` property to `Timestamped<DigitalInputState>`.
- Insert a [`VisualizerWindow`] operator. This will automatically open a window displaying the parsed events when the workflow starts.

Run the workflow and block the infrared beam on the Mice Poke peripheral. The visualizer will display:

```text
DIPort0@3.213504
None@3.513823
```

The first value is the payload, listing the digital inputs that are currently active, and the second value is the timestamp on the device clock. Both beam breaks and beam restores generate an event, so `None` marks a poke exit.

### Configure Input Filter

A single poke can generate a burst of rapid transitions, for example, when the snout hovers at the edge of the infrared beam. The [`PokeInputFilter`] register sets a refractory period, in milliseconds. After each reported transition, further transitions on that port are ignored for the set time, so one poke reads as one entry and one exit.

:::workflow
![Detect Pokes Input Filter](../workflows/controlpoke-inputfilter.bonsai)
:::

- Insert a [`KeyDown`] operator and set the `Filter` property to `A`.
- Insert a [`CreateMessage`] operator and configure the following properties:
    - `Payload` - Select `PokeInputFilterPayload`.
    - `PokeInputFilter` - Set the filter time to 5 ms.
- Insert a [`MulticastSubject`] operator named `Behavior Commands`.

Run the workflow and press <kbd>A</kbd>. Rapid repeated transitions within 5 ms of a poke event no longer generate events. Set the value to 0 to disable the filter. The default filter time is 1 ms.

> [!TIP]
> The filter is blind to the cause of a transition, so values much longer than the animal's fastest re-entry will also ignore genuine consecutive pokes.

### Drive the Poke LED

Each peripheral port carries an LED drive line (`DOPort0` - `DOPort2`). The LED is controlled like any other digital output. For instance, you can turn it on and off with the [`OutputSet`] and [`OutputClear`] registers:

:::workflow
![Detect Pokes Drive LED](../workflows/controlpoke-driveled.bonsai)
:::

- Insert a [`KeyDown`] operator and set the `Filter` property to `S`.
- Insert a [`CreateMessage`] operator and configure the following properties:
    - `Payload` - Select `OutputSetPayload`.
    - `OutputSet` - Select `DOPort0`.
- Insert a [`MulticastSubject`] operator named `Behavior Commands`.

In a separate branch:

- Insert a [`KeyDown`] operator and set the `Filter` property to `D`.
- Insert a [`CreateMessage`] operator and configure the following properties:
    - `Payload` - Select `OutputClearPayload`.
    - `OutputClear` - Select `DOPort0`.
- Insert a [`MulticastSubject`] operator named `Behavior Commands`.

Run the workflow, then press <kbd>S</kbd> to turn the **P0** poke LED on and <kbd>D</kbd> to turn it off. See [Control Digital Outputs](control-digital-outputs.md) for other methods to control the LED.

### Deliver Rewards on Poke

Each peripheral port carries a 12 V valve drive line for a solenoid valve that gates reward delivery. The workflow below enables [pulse mode](control-digital-outputs.md#pulse-outputs) on the valve output and configures the pulse duration, then closes the loop in Bonsai where every poke at **P0** immediately triggers a reward at the same port.  The reward volume is calibrated by adjusting the pulse duration.

:::workflow
![Detect Pokes Deliver Rewards](../workflows/controlpoke-valve.bonsai)
:::

- Insert a [`KeyDown`] operator and set the `Filter` property to `F`.
- Insert a [`CreateMessage`] operator and configure the following properties:
    - `Payload` - Select `OutputPulseEnablePayload`.
    - `OutputPulseEnable` - Select `SupplyPort0` to enable pulse mode on the **P0** valve output.
- Insert a [`MulticastSubject`] operator named `Behavior Commands`.
- Insert a [`CreateMessage`] operator and configure the following properties:
    - `Payload` - Select `PulseSupplyPort0Payload`.
    - `PulseSupplyPort0` - Set the valve opening duration in ms (e.g. 15).
- Insert a [`MulticastSubject`] operator named `Behavior Commands`.

In a separate branch:

- Insert a [`SubscribeSubject`] operator named `Behavior Events`.
- Insert a [`Parse`] operator and configure the `Register` property to `DigitalInputState`.
- Insert a [`HasFlag`] operator and set the `Value` property to `DIPort0`.
- Insert a [`Condition`] operator, leaving its inner workflow at the default. Only beam-break events at **P0** pass through; beam restores and events from other ports are filtered out.
- Insert a [`CreateMessage`] operator and configure the following properties:
    - `Payload` - Select `OutputSetPayload`.
    - `OutputSet` - Select `SupplyPort0`.
- Insert a [`MulticastSubject`] operator named `Behavior Commands`.

Run the workflow, press <kbd>F</kbd> once to configure the valve pulse, then block the infrared beam on the **P0** poke. The valve will open for 15 ms and close on its own, delivering one reward per poke.

[!INCLUDE [](outputpulseenable-warning.md)]

[!INCLUDE [](version-footer.md)]

<!--Reference Style Links -->
[`KeyDown`]: xref:Bonsai.Windows.Input.KeyDown
[`CreateMessage`]: xref:Harp.Behavior.CreateMessage
[`MulticastSubject`]: xref:Bonsai.Expressions.MulticastSubject
[`SubscribeSubject`]: xref:Bonsai.Expressions.SubscribeSubject
[`PublishSubject`]: xref:Bonsai.Reactive.PublishSubject
[`Parse`]: xref:Harp.Behavior.Parse
[`VisualizerWindow`]: xref:Bonsai.Design.VisualizerWindow
[`HarpMessages`]: xref:Bonsai.Harp.HarpMessage
[`DigitalInputState`]: xref:Harp.Behavior.DigitalInputState
[`PokeInputFilter`]: xref:Harp.Behavior.PokeInputFilter
[`OutputSet`]: xref:Harp.Behavior.OutputSet
[`OutputClear`]: xref:Harp.Behavior.OutputClear
[`PulseSupplyPort0`]: xref:Harp.Behavior.PulseSupplyPort0
[`HasFlag`]: xref:Bonsai.Expressions.HasFlagBuilder
[`Condition`]: xref:Bonsai.Reactive.Condition
