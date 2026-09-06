## Track Rotary Encoder

The **P2** peripheral port of the Behavior board has an encoder mode that allows it to read a quadrature encoder (e.g. to track the rotation of a treadmill). Refer to the [connections](./connections.md) article to set up either the [Rotary Encoder](./peripherals/peripherals-rotaryencoder.md) peripheral or attach an external quadrature encoder to the [Breakout](./peripherals/peripherals-portbreakout.md) board.

This article covers enabling the encoder mode, visualizing the reading, and resetting the count.

The complete workflow is shown below. Copy and paste it into Bonsai or build each section by following the step-by-step instructions below.

:::workflow
![Track Rotary Encoder](../workflows/trackrotaryencoder-toplevel.bonsai)
:::

### Enable the Encoder

The encoder mode is enabled with the [`EnableEncoders`] register, its reading mode is selected with [`EncoderMode`], and its value is streamed in the `Encoder` field of the 1 kHz [`AnalogData`](acquire-analog-data.md) events. 

:::workflow
![Enable the Encoder](../workflows/trackrotaryencoder-enable.bonsai)
:::

- Insert a [`KeyDown`] operator and set the `Filter` property to `A`.
- Insert a [`CreateMessage`] operator and configure the following properties:
    - `Payload` - Select `EncoderModePayload`.
    - `EncoderMode` - Select `Position` to stream the accumulated count, or `Displacement` to stream the change since the previous sample.
- Insert a [`MulticastSubject`] operator named `Behavior Commands`.
- Insert a [`CreateMessage`] operator and configure the following properties:
    - `Payload` - Select `EnableEncodersPayload`.
    - `EnableEncoders` - Select `EncoderPort2` to enable the encoder. To disable the encoder in the future, select `None`.
- Insert a [`MulticastSubject`] operator named `Behavior Commands`.

Run the workflow and press <kbd>A</kbd>. The quadrature counter starts and its value is reported in every [`AnalogData`] event.

> [!WARNING]
> While the encoder is enabled, **P2** repurposes its infrared and DIO lines as the quadrature inputs: poke events from **P2** are suspended until the encoder is disabled.

### Visualize Encoder Reading

:::workflow
![Visualize Encoder Reading](../workflows/trackrotaryencoder-visualize.bonsai)
:::

- Insert a [`SubscribeSubject`] operator named `Behavior Events`.
- Insert a [`Parse`] operator and configure the `Register` property to `AnalogData`.
- Right-click on the [`Parse`] operator, select the "Output (Harp.Behavior.AnalogDataPayload)" > "Encoder" option from the context menu. This will create a `Encoder` node.
- Insert a [`VisualizerWindow`] operator. This will automatically open a window displaying the encoder count when the workflow starts.

Run the workflow, press <kbd>A</kbd> to enable the encoder, and turn the encoder shaft. The visualizer plots the count going up in one direction and down in the other.

### Reset the Encoder

The [`EncoderReset`] register sets the counter of the selected encoders back to zero:

:::workflow
![Reset the Encoder](../workflows/trackrotaryencoder-reset.bonsai)
:::

- Insert a [`KeyDown`] operator and set the `Filter` property to `S`.
- Insert a [`CreateMessage`] operator and configure the following properties:
    - `Payload` - Select `EncoderResetPayload`.
    - `EncoderReset` - Select `EncoderPort2`.
- Insert a [`MulticastSubject`] operator named `Behavior Commands`.

Run the workflow and press <kbd>S</kbd>. The `Encoder` value in the analog stream returns to zero.

[!INCLUDE [](version-footer.md)]

<!--Reference Style Links -->
[`KeyDown`]: xref:Bonsai.Windows.Input.KeyDown
[`CreateMessage`]: xref:Harp.Behavior.CreateMessage
[`MulticastSubject`]: xref:Bonsai.Expressions.MulticastSubject
[`SubscribeSubject`]: xref:Bonsai.Expressions.SubscribeSubject
[`Parse`]: xref:Harp.Behavior.Parse
[`MemberSelector`]: xref:Bonsai.Expressions.MemberSelectorBuilder
[`VisualizerWindow`]: xref:Bonsai.Design.VisualizerWindow
[`EnableEncoders`]: xref:Harp.Behavior.EnableEncoders
[`EncoderMode`]: xref:Harp.Behavior.EncoderMode
[`EncoderReset`]: xref:Harp.Behavior.EncoderReset
[`AnalogData`]: xref:Harp.Behavior.AnalogData
