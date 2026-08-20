## Track Rotation

Port 2 of the Behavior board can read a quadrature encoder — for instance from a running wheel — instead of a nose poke. The counter is enabled with the [`EnableEncoders`] register, its reading mode is selected with [`EncoderMode`], and its value is streamed in the `Encoder` field of the 1 kHz [`AnalogData`](acquire-analog-data.md) events. This article covers enabling, visualizing, and resetting the encoder.

The complete workflow is shown below:

:::workflow
![Track Rotation](../workflows/trackrotation-toplevel.bonsai)
:::

### Enable the Encoder

:::workflow
![Enable the Encoder](../workflows/trackrotation-enable.bonsai)
:::

- Insert a [`KeyDown`] operator and set the `Filter` property to `A`.
- Insert a [`CreateMessage`] operator and configure the following properties:
    - `Payload` - Select `EncoderModePayload`.
    - `EncoderMode` - Select `Position` to stream the accumulated count, or `Displacement` to stream the change since the previous sample.
- Insert a [`MulticastSubject`] operator named `Behavior Commands`.
- Insert a [`CreateMessage`] operator and configure the following properties:
    - `Payload` - Select `EnableEncodersPayload`.
    - `EnableEncoders` - Select `EncoderPort2`.
- Insert a [`MulticastSubject`] operator named `Behavior Commands`.

Run the workflow and press <kbd>A</kbd> — the quadrature counter starts and its value is reported in every [`AnalogData`] event.

> [!WARNING]
> While the encoder is enabled, Port 2 repurposes its infrared and DIO lines as the quadrature inputs: poke events from Port 2 are suspended until the encoder is disabled.

### Visualize Encoder Data

:::workflow
![Visualize Encoder Data](../workflows/trackrotation-visualize.bonsai)
:::

- Insert a [`SubscribeSubject`] operator named `Behavior Events`.
- Insert a [`FilterMessageType`] operator and configure the `MessageType` property to `Event`.
- Insert a [`Parse`] operator and configure the `Register` property to `AnalogData`.
- Insert a [`MemberSelector`] operator and select the `Encoder` field.
- Insert a [`VisualizerWindow`] operator. This will automatically open a window displaying the encoder count when the workflow starts.

Run the workflow, press <kbd>A</kbd> to enable the encoder, and turn the encoder shaft — the visualizer plots the count going up in one direction and down in the other.

### Reset the Encoder

The [`EncoderReset`] register sets the counter of the selected encoders back to zero:

:::workflow
![Reset the Encoder](../workflows/trackrotation-reset.bonsai)
:::

- Insert a [`KeyDown`] operator and set the `Filter` property to `S`.
- Insert a [`CreateMessage`] operator and configure the following properties:
    - `Payload` - Select `EncoderResetPayload`.
    - `EncoderReset` - Select `EncoderPort2`.
- Insert a [`MulticastSubject`] operator named `Behavior Commands`.

Run the workflow and press <kbd>S</kbd> — the `Encoder` value in the analog stream returns to zero.

[!INCLUDE [](version-footer.md)]

<!--Reference Style Links -->
[`KeyDown`]: xref:Bonsai.Windows.Input.KeyDown
[`CreateMessage`]: xref:Harp.Behavior.CreateMessage
[`MulticastSubject`]: xref:Bonsai.Expressions.MulticastSubject
[`SubscribeSubject`]: xref:Bonsai.Expressions.SubscribeSubject
[`FilterMessageType`]: xref:Bonsai.Harp.FilterMessageType
[`Parse`]: xref:Harp.Behavior.Parse
[`MemberSelector`]: xref:Bonsai.Expressions.MemberSelectorBuilder
[`VisualizerWindow`]: xref:Bonsai.Design.VisualizerWindow
[`EnableEncoders`]: xref:Harp.Behavior.EnableEncoders
[`EncoderMode`]: xref:Harp.Behavior.EncoderMode
[`EncoderReset`]: xref:Harp.Behavior.EncoderReset
[`AnalogData`]: xref:Harp.Behavior.AnalogData
