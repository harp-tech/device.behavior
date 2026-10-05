## About These Examples

The following "Bonsai Workflows" section covers how to operate the device grouped into separate articles by functionality.

Each article begins with a complete top level workflow that you can copy and paste into Bonsai to get started immediately. Every workflow is made up of these three basic building blocks:

- A Harp device pattern that sets up the device, present in every workflow 
- Branches that send commands to the device
- Branches that read events from the device

![Workflow Basics](../images/workflowbasics-toplevel.svg)

Subsequent sections will explain how to configure the operators in each block, with details about the specific functionality.

> [!NOTE]
> Instead of copying the workflow, you can follow the step-by-step instructions to find and add these operators to the workflow from the Bonsai [Toolbox](https://bonsai-rx.org/docs/articles/editor.html?tabs=mouse-controls#toolbox). Make sure to use the device-specific versions, e.g. `Device (Harp.Behavior)` instead of `Device (Harp)`. If correctly selected, the names of these operators in the workflow panel will change to reflect the device name.

### Harp Device Pattern

![Harp Device Pattern](../images/workflowbasics-devicepattern.svg)

The [Harp device pattern](https://harp-tech.org/articles/operators.html#device-pattern) is the first block you will need in every workflow. It will initialize the device, log data, and provide hooks to send commands as well as receive messages from a Harp device using the [Harp communication protocol](https://harp-tech.org/protocol/BinaryProtocol-8bit.html). It consists of:

- A [`BehaviorSubject`] operator that collects all the commands in the workflow that are forwarded to the `Device Commands` subject.
- A [`Device`] operator, that establishes the serial communication link with the device.
- A [`MessageWriter`] operator that saves the messages or data from the device. The device-specific package will contain a dedicated [`DeviceDataWriter`].
- A [`PublishSubject`] operator that broadcasts the message stream as a `Device Events` subject.

### Send Commands to Device

![Send Commands to Device](../images/workflowbasics-sendcommands.svg)

To send a command to the device, the examples use:

- A [`KeyDown`] operator that generates the command when a key is pressed.
- A [`CreateMessage`] operator that formats the command to send to the device.
- A [`MulticastSubject`] operator that sends to the command to the `Device Commands` subject.

> [!NOTE]
>  Other Bonsai operator [sources] can also be used as triggers. For instance, a [`Timer`] can be used instead of [`KeyDown`] to trigger the command at a set time after the workflow starts.

### Read Events from Device

![Receive Events from Device](../images/workflowbasics-receiveevents.svg)

To receive and read events from the device, the examples use:

- A [`SubscribeSubject`] operator that subscribes to the `Device Events` message stream.
- A [`Parse`] operator that picks out the messages corresponding to one register and decodes its values.
- A [`VisualizerWindow`] that displays the decoded values when the workflow starts.

Each example also shows what the visualizer displays:

```text
AnalogDataPayload { AnalogInput0 = 13, Encoder = 0, AnalogInput1 = 13 }@10.351072
```

[!INCLUDE [](version-footer.md)]

<!--Reference Style Links -->
[`BehaviorSubject`]: xref:Bonsai.Reactive.BehaviorSubject
[`CreateMessage`]: xref:Bonsai.Harp.CreateMessage
[`Device`]: xref:Bonsai.Harp.Device
[`DeviceDataWriter`]: xref:Harp.Behavior.DeviceDataWriter
[`MessageWriter`]: xref:Bonsai.Harp.MessageWriter
[`KeyDown`]: xref:Bonsai.Windows.Input.KeyDown
[`MulticastSubject`]: xref:Bonsai.Expressions.MulticastSubject
[`Parse`]: xref:Bonsai.Harp.Parse
[`PublishSubject`]: xref:Bonsai.Reactive.PublishSubject
[sources]: https://bonsai-rx.org/docs/articles/operators.html#source
[`SubscribeSubject`]: xref:Bonsai.Expressions.SubscribeSubject
[`Timer`]: xref:Bonsai.Reactive.Timer
[`VisualizerWindow`]: xref:Bonsai.Design.VisualizerWindow
