## Read Digital Inputs

The Behavior board has one general-purpose 5 V digital input, **DI3**, located on the **Input** connector (hardware v2.0 or later boards only). Refer to the [connections](./connections.md?tabs=digitalinput#connections) article to set up the hardware connection on **DI3** or 

This article covers how to visualize the digital input events and read the peripheral port **DIO** lines as a digital input in Bonsai.

The complete workflow is shown below. Copy and paste it into Bonsai or build each section by following the step-by-step instructions below.

:::workflow
![Read Digital Inputs](../workflows/readdigitalinputs-toplevel.bonsai)
:::

[!INCLUDE [](breakout-note.md)]

### Visualize Digital Input Events

The Behavior board broadcasts changes on its digital input lines as [`DigitalInputState`] event messages. In addition to **DI3**, this register also reports events from the **DI** lines on the peripheral ports **P0**–**P2** (`DIPort0`–`DIPort2`), such as [poke events](control-poke.md) from the [Mice Poke](./peripherals/peripherals-micepoke.md) peripheral or other digital inputs connected to the [Breakout](./peripherals/peripherals-portbreakout.md) board. The workflow below subscribes, parses, and displays them in a visualizer window.

:::workflow
![Read Digital Inputs Visualize](../workflows/controlpoke-visualizeevents.bonsai)
:::

- Insert a [`SubscribeSubject`] operator named `Behavior Events`. This will listen to [`HarpMessages`] broadcast from the [`PublishSubject`] named `Behavior Events` in the Harp device pattern.
- Insert a [`Parse`] operator and configure the `Register` property to `TimestampedDigitalInputState`.
- Insert a [`VisualizerWindow`] operator. This will automatically open a window displaying the parsed events when the workflow starts.

Run the workflow and drive the **DI3** line high. The visualizer will display:

```text
DI3@12.001504
None@12.301823
```

The first value is the payload, listing the digital inputs that are currently active, and the second value is the timestamp on the device clock. Both rising and falling edges generate an event, so `None` marks the moment the line returned low.

> [!NOTE]
> The [`PokeInputFilter`](control-poke.md#configure-input-filter) debounce applies only to the peripheral port inputs; **DI3** events are not filtered.

### Visualize Port DIO Events

Each peripheral port also carries a **DIO** line. In the current firmware (v3.3) this line works as an extra digital input. It idles high at 5 V through onboard pull-ups, and the connected device must actively drive it low. Changes on the **DIO** lines are broadcast in the [`PortDIOStateEvent`] register, which can be visualized in the same way as the digital inputs:

:::workflow
![Read Digital Inputs Visualize Port DIO](../workflows/readdigitalinputs-visualizeportdio.bonsai)
:::

- Insert a [`SubscribeSubject`] operator named `Behavior Events`.
- Insert a [`Parse`] operator and configure the `Register` property to `TimestampedPortDIOStateEvent`.
- Insert a [`VisualizerWindow`] operator.

Run the workflow and drive the **DIO** line on **P0** low. The visualizer will display:

```text
DIO1, DIO2@20.154016
DIO0, DIO1, DIO2@20.575872
```

The payload lists the **DIO** lines that are currently high. Since the lines idle high, driving **DIO0** low removes it from the list, and releasing the line adds it back.

[!INCLUDE [](version-footer.md)]

<!--Reference Style Links -->
[`SubscribeSubject`]: xref:Bonsai.Expressions.SubscribeSubject
[`PublishSubject`]: xref:Bonsai.Reactive.PublishSubject
[`Parse`]: xref:Harp.Behavior.Parse
[`VisualizerWindow`]: xref:Bonsai.Design.VisualizerWindow
[`HarpMessages`]: xref:Bonsai.Harp.HarpMessage
[`DigitalInputState`]: xref:Harp.Behavior.DigitalInputState
[`PortDIOStateEvent`]: xref:Harp.Behavior.PortDIOStateEvent
