## Acquire Analog Data

The Behavior board has two analog inputs: **ADC0** is located on the **ADC** terminal and **ADC1** is located on the **Input** terminal. The analog input can be tested with an analog output from a [photodiode](./peripherals/peripherals-photodiode.md). Refer to the [connections](./connections.md) article to set up the hardware connection on **ADC0**, which we will use for the rest of these examples. 

This article covers how to visualize the analog input streams and extract a single channel in Bonsai.

The complete workflow is shown below:

:::workflow
![Acquire Analog Data](../workflows/acquireanalogdata-toplevel.bonsai)
:::

> [!NOTE]
> Hardware version 1.0 only has one ADC.

> [!WARNING]
> You can find and add these operators to the workflow from the Bonsai [Toolbox](https://bonsai-rx.org/docs/articles/editor.html?tabs=mouse-controls#toolbox). Make sure to use the device-specific versions, e.g. `Device (Harp.Behavior)` instead of `Device (Harp)`. If correctly selected, the names of these operators in the workflow panel will change to reflect either the name of the device or the selected register/payload.

### Visualize Analog Data

The analog stream is enabled by default and sampled at 1 kHz. To visualize it, filter and decode the [`AnalogData`] events:

:::workflow
![Analog Data Visualize](../workflows/acquireanalogdata-visualizedata.bonsai)
:::

- Insert a [`SubscribeSubject`] operator named `Behavior Events`. This will listen to [`HarpMessages`] broadcast from the [`PublishSubject`] named `Behavior Events` in the Harp device pattern.
- Insert a [`Parse`] operator and configure the `Register` property to `AnalogData`. This filters only messages for that register, and extracts the data into a typed format.
- Insert a [`VisualizerWindow`] operator. This will automatically open a window displaying all the analog data payloads.

To visualize only one of the input channels:

- Right-click on the [`Parse`] operator, hold down the <kbd>Alt</kbd> key, and select the "Output (Harp.Behavior.AnalogDataPayload)" > "AnalogInput0" from the context menu. This will create a `AnalogInput0` node on a separate branch.
- Insert a [`VisualizerWindow`] operator. This will open a second window displaying only the selected channel.

Run the workflow, the first visualizer displays the three payload values:

```text
AnalogDataPayload { AnalogInput0 = 13, Encoder = 0, AnalogInput = 13 }
AnalogDataPayload { AnalogInput0 = 15, Encoder = 0, AnalogInput = 14 }
```
and the second displays the `AnalogInput0` value on its own.

```text
12
13
```

If you are using a photodiode, shine a light on the photodiode to see the analog input value change. If you are using a different signal source, vary the output voltage instead.

> [!NOTE]
> Both analog inputs accept voltages from 0 to 5 V, which are subsequently digitized by an onboard 12-bit analog-to-digital (ADC) converter.

> [!NOTE]
> The `Encoder` value is not an analog voltage reading, it is a [quadrature encoder counter](track-rotary-encoder.md) that is sampled on the same 1 kHz tick so that position and analog data share the same timestamps.

> [!NOTE]
> The analog events are enabled by default, but if nothing appears, they may have been disabled by another [configuration](advanced-configuration.md#select-active-events) setting. Refer to the link to find out how to re-enable them.

[!INCLUDE [](version-footer.md)]

<!--Reference Style Links -->
[`SubscribeSubject`]: xref:Bonsai.Expressions.SubscribeSubject
[`PublishSubject`]: xref:Bonsai.Reactive.PublishSubject
[`Parse`]: xref:Harp.Behavior.Parse
[`VisualizerWindow`]: xref:Bonsai.Design.VisualizerWindow
[`HarpMessages`]: xref:Bonsai.Harp.HarpMessage
[`AnalogData`]: xref:Harp.Behavior.AnalogData
