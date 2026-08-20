## Read Digital Inputs

The Behavior board has one general-purpose 5 V digital input, **DI3**, located on the **Input** screw terminal. Refer to the [connections](./connections.md) article to set up the hardware connection.

The complete workflow is shown below:

:::workflow
![Read Digital Inputs](../workflows/readdigitalinputs-toplevel.bonsai)
:::

> [!NOTE]
> You can use the [Breakout](./peripherals/peripherals-portbreakout.md) extension board to adapt the poke ports as regular digital input/output pins if you need more digital inputs. Refer to the [Control Poke Peripheral](control-poke.md) article for more information on controlling those pins.

### Visualize Digital Input Events

:::workflow
![Read Digital Inputs Visualize](../workflows/controlpoke-visualizeevents.bonsai)
:::

- Insert a [`SubscribeSubject`] operator named `Behavior Events`. This will listen to [`HarpMessages`] broadcast from the [`PublishSubject`] named `Behavior Events` in the Harp device pattern.
- Insert a [`Parse`] operator and configure the `Register` property to `Timestamped<DigitalInputState>`.
- Insert a [`VisualizerWindow`] operator. This will automatically open a window displaying the parsed events when the workflow starts.

Run the workflow and drive the **DI3** line high. The visualizer will display:

```text
DI3@12.001504
None@12.301823
```

The first value is the payload, listing the digital inputs that are currently active, and the second value is the timestamp on the device clock. Both rising and falling edges generate an event, so `None` marks the moment the line returned low.

> [!NOTE]
> The [`PokeInputFilter`](control-poke.md#configure-input-filter) debounce applies only to the poke port inputs — **DI3** events are not filtered.

[!INCLUDE [](version-footer.md)]

<!--Reference Style Links -->
[`SubscribeSubject`]: xref:Bonsai.Expressions.SubscribeSubject
[`PublishSubject`]: xref:Bonsai.Reactive.PublishSubject
[`Parse`]: xref:Harp.Behavior.Parse
[`VisualizerWindow`]: xref:Bonsai.Design.VisualizerWindow
[`HarpMessages`]: xref:Bonsai.Harp.HarpMessage
[`DigitalInputState`]: xref:Harp.Behavior.DigitalInputState
