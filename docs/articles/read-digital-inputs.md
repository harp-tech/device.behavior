# Read Digital Inputs

The Behavior board has one digital input that is accessible without extra hardware: **DI3** on the **Input** connector. Each poke port also carries a general-purpose **DI** line, and a **DIO** line that can be used as an active-low digital input. Refer to the [connections](./connections.md?tabs=digitalinput#connections) article to set up the hardware connection on **DI3** or **DIO0**, which we will use for the rest of these examples.

This article covers how to visualize the digital input events and read the poke port **DIO** lines as a digital input in Bonsai.

The complete workflow is shown below. Copy and paste it into Bonsai to get started immediately, or build each section by following the step-by-step instructions in the following sections.

:::workflow
![Read Digital Inputs](../workflows/readdigitalinputs-toplevel.bonsai)
:::

> [!NOTE]
> If you need more digital inputs, the [Breakout](./peripherals/peripherals-portbreakout.md) board provides access to the poke port input lines through screw terminals.


## Visualize digital input events

The Behavior board broadcasts changes on its digital input lines as [`DigitalInputState`] event messages. In addition to **DI3**, this register also reports events from the **DI** lines on the poke ports **P0**–**P2** (represented as `DIPort0`–`DIPort2` in Bonsai), such as [poke events](control-poke.md) from the [Mice Poke](./peripherals/peripherals-micepoke.md) peripheral or other digital inputs connected to the [Breakout](./peripherals/peripherals-portbreakout.md) board. The workflow below subscribes, parses, and displays them in a visualizer window.

:::workflow
![Read Digital Inputs Visualize](../workflows/controlpoke-visualizeevents.bonsai)
:::

- Insert a [`SubscribeSubject`] operator named `Behavior Events`.
- Insert a [`Parse`] operator and configure the `Register` property to `TimestampedDigitalInputState`.
- Insert a [`VisualizerWindow`] operator.

Run the workflow and set the **DI3** line high, then low. The visualizer will display:

```text
DI3@12.001504
None@12.301823
```

The first value is the payload, listing the digital inputs that are currently active, and the second value is the timestamp on the device clock. Both rising and falling edges generate an event, so `None` marks the moment the line returned low.

## Visualize port DIO events

In the current firmware (v3.3) the **DIO** line acts only as an active-low digital input. It idles high at 5 V through onboard pull-up resistors, and the connected device must actively drive it low. Changes on the **DIO** lines are broadcast in a different register, [`PortDIOStateEvent`], which can be visualized in the same way as the digital inputs:

:::workflow
![Read Digital Inputs Visualize Port DIO](../workflows/readdigitalinputs-visualizeportdio.bonsai)
:::

- Insert a [`SubscribeSubject`] operator named `Behavior Events`.
- Insert a [`Parse`] operator and configure the `Register` property to `TimestampedPortDIOStateEvent`.
- Insert a [`VisualizerWindow`] operator.

Run the workflow and set the **DIO** line on **P0** low, then high. The visualizer will display:

```text
DIO1, DIO2@20.154016
DIO0, DIO1, DIO2@20.575872
```

The payload lists the **DIO** lines that are currently high. Since the lines idle high, driving **DIO0** low removes it from the list, and releasing the line adds it back.

[!INCLUDE [](../includes/version-footer.md)]

<!--Reference Style Links -->
[`SubscribeSubject`]: xref:Bonsai.Expressions.SubscribeSubject
[`PublishSubject`]: xref:Bonsai.Reactive.PublishSubject
[`Parse`]: xref:Harp.Behavior.Parse
[`VisualizerWindow`]: xref:Bonsai.Design.VisualizerWindow
[`HarpMessages`]: xref:Bonsai.Harp.HarpMessage
[`DigitalInputState`]: xref:Harp.Behavior.DigitalInputState
[`PortDIOStateEvent`]: xref:Harp.Behavior.PortDIOStateEvent
