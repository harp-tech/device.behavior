# Control Poke Peripheral

The [Mice Poke](./peripherals/peripherals-micepoke.md) is a nose poke peripheral for the Behavior board which uses an infrared beam sensor for poke detection. It also includes cue LEDs and an optional solenoid valve for controlled liquid delivery, typically used for rewards. Refer to the [connections](./connections.md?tabs=poke#connections) article to set up the hardware connection on poke port **P0**, which we will use for the rest of these examples.

This article covers how to visualize poke events, debounce noisy inputs, drive the cue LEDs, and dispense liquid rewards with the Mice Poke peripheral in Bonsai.

The complete workflow is shown below. Copy and paste it into Bonsai to get started immediately, or build each section by following the step-by-step instructions in the following sections.

:::workflow
![Detect Pokes](../workflows/controlpoke-toplevel.bonsai)
:::

## Visualize poke events

Beam breaks in the Mice Poke peripheral are reported as digital input events. The three poke ports **P0**–**P2** are represented as `DIPort0` - `DIPort2` in the [`DigitalInputState`] event register.

:::workflow
![Detect Pokes Visualize Events](../workflows/controlpoke-visualizeevents.bonsai)
:::

- Insert a [`SubscribeSubject`] operator named `Behavior Events`.
- Insert a [`Parse`] operator and configure the `Register` property to `TimestampedDigitalInputState`.
- Insert a [`VisualizerWindow`] operator.

Run the workflow, block and unblock the infrared beam on the Mice Poke peripheral briefly. The visualizer will display:

```text
DIPort0@3.213504
None@3.513823
```

The first value is the payload, listing the digital inputs that are currently active, and the second value is the timestamp on the device clock. Both beam breaks and beam restores generate an event, so `None` marks a poke exit.

## Configure input filter

A single poke can generate a burst of rapid transitions, for example, when the nose hovers at the edge of the infrared beam. Use the [`PokeInputFilter`] register to set a refractory period in milliseconds. After each reported transition, further transitions on that port are ignored for the set time.

:::workflow
![Detect Pokes Input Filter](../workflows/controlpoke-inputfilter.bonsai)
:::

- Insert a [`KeyDown`] operator and set the `Filter` property to `A`.
- Insert a [`CreateMessage`] operator and configure the following properties:
    - `Payload` - Select `PokeInputFilterPayload`.
    - `PokeInputFilter` - Set the filter time to the refractory period in ms (e.g. `5`).
- Insert a [`MulticastSubject`] operator named `Behavior Commands`.

Run the workflow and press <kbd>A</kbd>. Rapid repeated transitions within 5 ms of a poke event no longer generate events. Set the value to 0 to disable the filter. The default filter time is 1 ms.

> [!TIP]
> The filter is blind to the cause of a transition, so values much longer than the animal's fastest re-entry will also ignore genuine consecutive pokes.

> [!NOTE]
> [`PokeInputFilter`] applies only to the poke port inputs; **DI3** events are not filtered.

## Control poke LEDs

Each poke port carries an LED control line (`DOPort0` - `DOPort2`) that controls the two onboard LEDs, and the additional LED output on the screw terminal block. The LED line is controlled like any other digital output. For instance, you can turn it on and off with the [`OutputSet`] and [`OutputClear`] registers:

:::workflow
![Control Poke LEDs](../workflows/controlpoke-leds.bonsai)
:::

- Insert a [`KeyDown`] operator and set the `Filter` property to `S`.
- Insert a [`CreateMessage`] operator and configure the following properties:
    - `Payload` - Select `OutputSetPayload`.
    - `OutputSet` - Select the line to turn on (e.g. `DOPort0`).
- Insert a [`MulticastSubject`] operator named `Behavior Commands`.

In a separate branch:

- Insert a [`KeyDown`] operator and set the `Filter` property to `D`.
- Insert a [`CreateMessage`] operator and configure the following properties:
    - `Payload` - Select `OutputClearPayload`.
    - `OutputClear` - Select the line to turn off (e.g. `DOPort0`).
- Insert a [`MulticastSubject`] operator named `Behavior Commands`.

Run the workflow, then press <kbd>S</kbd> to turn the **P0** poke LEDs on and <kbd>D</kbd> to turn it off. See [Control Digital Outputs](control-digital-outputs.md) for other methods to control the LEDs, such as with a toggle, pulse mode, or timer. 

## Deliver rewards on poke

Each poke port carries a 12 V valve drive line for a solenoid valve that gates liquid delivery. The workflow below enables [pulse mode](control-digital-outputs.md#pulse-outputs) on the valve output and configures the pulse duration, which can be calibrated to adjust the volume of liquid dispensed. The second branch of the workflow implements closed-loop reward delivery in Bonsai: for every poke at **P0**, a liquid reward is immediately delivered at the same port.

:::workflow
![Detect Pokes Deliver Rewards](../workflows/controlpoke-valve.bonsai)
:::

- Insert a [`KeyDown`] operator and set the `Filter` property to `F`.
- Insert a [`CreateMessage`] operator and configure the following properties:
    - `Payload` - Select `OutputPulseEnablePayload`.
    - `OutputPulseEnable` - Select the valve output to enable pulse mode on (e.g. `SupplyPort0`).
- Insert a [`MulticastSubject`] operator named `Behavior Commands`.
- Insert a [`CreateMessage`] operator and configure the following properties:
    - `Payload` - Select `PulseSupplyPort0Payload`.
    - `PulseSupplyPort0` - Set the valve opening duration in ms (e.g. `15`).
- Insert a [`MulticastSubject`] operator named `Behavior Commands`.

In a separate branch:

- Insert a [`SubscribeSubject`] operator named `Behavior Events`.
- Insert a [`Parse`] operator and configure the `Register` property to `DigitalInputState`.
- Insert a [`HasFlag`] operator and set the `Value` property to `DIPort0`. This operator will generate a boolean value (e.g. `True`) if it detects a beam break that matches that pin.
- Insert a [`Condition`] operator, leaving its inner workflow at the default. This operator lets through only `True` elements corresponding to beam breaks at **P0**, which are used to trigger the next command.
- Insert a [`CreateMessage`] operator and configure the following properties:
    - `Payload` - Select `OutputSetPayload`.
    - `OutputSet` - Select the valve delivery line to turn on (e.g. `SupplyPort0` on **P0**).
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
