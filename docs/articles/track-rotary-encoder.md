# Track Rotary Encoder

The **P2** poke port of the Behavior board has an encoder mode that allows it to read a quadrature encoder (e.g. to track the rotation of a treadmill). As the encoder rotates, it generates counts that increase or decrease depending on the direction of rotation, allowing relative position to be tracked. Refer to the [connections](./connections.md?tabs=encoder#connections) article to set up either the [Rotary Encoder](./peripherals/peripherals-rotaryencoder.md) peripheral or attach an external quadrature encoder to the [Breakout](./peripherals/peripherals-portbreakout.md) board.

This article covers enabling the encoder mode, visualizing the reading, and resetting the count.

The complete workflow is shown below. Copy and paste it into Bonsai to get started immediately, or build each section by following the step-by-step instructions in the following sections.

:::workflow
![Track Rotary Encoder](../workflows/trackrotaryencoder-toplevel.bonsai)
:::

## Enable the encoder

The encoder mode is disabled by default and needs to be enabled with the [`EnableEncoders`] register. The reading can either reflect position or displacement, which is set with [`EncoderMode`].

:::workflow
![Enable the encoder](../workflows/trackrotaryencoder-enable.bonsai)
:::

- Insert a [`KeyDown`] operator and set the `Filter` property to `A`.
- Insert a [`CreateMessage`] operator and configure the following properties:
    - `Payload` - Select `EncoderModePayload`.
    - `EncoderMode` - Select `Position` to read the accumulated encoder count, or `Displacement` to read the change in count since the previous sample.
- Insert a [`MulticastSubject`] operator named `Behavior Commands`.
- Insert a [`CreateMessage`] operator and configure the following properties:
    - `Payload` - Select `EnableEncodersPayload`.
    - `EnableEncoders` - Select `EncoderPort2` to enable the encoder. To disable the encoder in the future, select `None`.
- Insert a [`MulticastSubject`] operator named `Behavior Commands`.

Run the workflow and press <kbd>A</kbd> to enable the encoder.

> [!WARNING]
> While the encoder is enabled, **P2** repurposes its infrared detector line for the quadrature counter. Poke events from **P2** are suspended until the encoder is disabled.

## Visualize encoder reading

The counter is then streamed in the `Encoder` field of the 1 kHz [`AnalogData`](acquire-analog-data.md) events.

:::workflow
![Visualize encoder reading](../workflows/trackrotaryencoder-visualize.bonsai)
:::

- Insert a [`SubscribeSubject`] operator named `Behavior Events`.
- Insert a [`Parse`] operator and configure the `Register` property to `AnalogData`.
- Right-click on the [`Parse`] operator, select the "Output (Harp.Behavior.AnalogDataPayload)" > "Encoder" option from the context menu. This will extract the encoding reading and create an `Encoder` node.
- Insert a [`VisualizerWindow`] operator. This will automatically open a window displaying the encoder count when the workflow starts.

Run the workflow, press <kbd>A</kbd> to enable the encoder if it is disabled, and turn the encoder shaft. The visualizer plots the count going up in one direction and down in the other.

> [!WARNING]
> In `Position` mode the count is a signed 16-bit value that ranges from -32768 to 32767 and wraps around silently when it passes either end. If the count is likely to hit those limits, reset the count every session or switch to `Displacement` mode and accumulate the per-sample changes in Bonsai with an [`Accumulate`] operator.

## Reset the encoder

The [`EncoderReset`] register sets the counter of the selected encoders back to zero:

:::workflow
![Reset the encoder](../workflows/trackrotaryencoder-reset.bonsai)
:::

- Insert a [`KeyDown`] operator and set the `Filter` property to `S`.
- Insert a [`CreateMessage`] operator and configure the following properties:
    - `Payload` - Select `EncoderResetPayload`.
    - `EncoderReset` - Select `EncoderPort2`.
- Insert a [`MulticastSubject`] operator named `Behavior Commands`.

Run the workflow and press <kbd>S</kbd>. The `Encoder` value in the analog stream returns to zero.

[!INCLUDE [](../includes/version-footer.md)]

<!--Reference Style Links -->
[`KeyDown`]: xref:Bonsai.Windows.Input.KeyDown
[`CreateMessage`]: xref:Harp.Behavior.CreateMessage
[`MulticastSubject`]: xref:Bonsai.Expressions.MulticastSubject
[`Accumulate`]: xref:Bonsai.Reactive.Accumulate
[`SubscribeSubject`]: xref:Bonsai.Expressions.SubscribeSubject
[`Parse`]: xref:Harp.Behavior.Parse
[`VisualizerWindow`]: xref:Bonsai.Design.VisualizerWindow
[`EnableEncoders`]: xref:Harp.Behavior.EnableEncoders
[`EncoderMode`]: xref:Harp.Behavior.EncoderMode
[`EncoderReset`]: xref:Harp.Behavior.EncoderReset
[`AnalogData`]: xref:Harp.Behavior.AnalogData
