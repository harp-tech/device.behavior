## About These Examples

The following "Bonsai Workflows" section covers how to operate the device grouped into separate articles by functionality.

Each article begins with a complete top level workflow that you can copy and paste into Bonsai to get started immediately. Every workflow is made up of these three basic building blocks:

- A Harp device pattern that sets up the device, present in every workflow 
- Branches that send commands to the device
- Branches that read events from the device

:::workflow
![Workflow Basics](../workflows/workflowbasics-toplevel.bonsai)
:::

Subsequent sections will explain how to configure the operators in each block, with details about the specific functionality. Here, the focus is on the general principle behind each operator.

> [!NOTE]
> Instead of copying the workflow, you can find and add these operators to the workflow from the Bonsai [Toolbox](https://bonsai-rx.org/docs/articles/editor.html?tabs=mouse-controls#toolbox). Make sure to use the device-specific versions, e.g. `Device (Harp.Behavior)` instead of `Device (Harp)`. If correctly selected, the names of these operators in the workflow panel will change to reflect the device name.

### Harp Device Pattern

The [Harp device pattern](https://harp-tech.org/articles/operators.html#device-pattern) is the first block you will need in every workflow. It will initialize the device, log data, and provide hooks to send commands as well as receive messages from the Behavior board using the [Harp communication protocol](https://harp-tech.org/protocol/BinaryProtocol-8bit.html). 

:::workflow
![Harp Device Pattern](../workflows/harp-devicepattern.bonsai)
:::

This is what each operator does, and the important properties that need to be set:

- [`Behavior Commands`] - This is a special type of operator called a [`BehaviorSubject`]. [Subjects](https://bonsai-rx.org/docs/articles/subjects.html) allow sharing of different streams in Bonsai. This operator acts as an entry point, collecting all the commands that are generated in the workflow and forwarding them to the device in the next node.
- [`Behavior`] - This is a [`Device`] type operator, it establishes the serial communication link with the device, and provides an interface to send commands and receive messages from the device. Messages from the device are passed on to the following nodes.
  - `PortName` - Set the property in this operator to the serial communications port for the device (e.g. COM8).
- [`BehaviorDataWriter`] - This is a [`DeviceDataWriter`] type operator that saves the messages or data from the device.
  - `Path` - Set this property to the save folder location (e.g. `Behavior.harp`).
- [`Behavior Events`] - This [`PublishSubject`] operator sends the message stream from the device to any location in the workflow that has a subscriber.

### Send Commands to Device

To send a command to the device, you need:

- A trigger to initiate the sending of the command
- The message or command to send
- A destination to send the command to

There are different ways to trigger commands, but in this guide the examples will use keyboard keys as the trigger and the workflow will look like this:

:::workflow
![Send Commands to Device](../workflows/harp-sendcommands.bonsai)
:::

- [`KeyDown`] - This operator generates an output every time a keyboard key is pressed, which executes the rest of the branch.
  - `Filter` - Set this property to a specific key, such as `A`, so that it only triggers on that keyboard key instead of all keys.  
- [`Behavior.OutputSetPayload`] - This operator is a [`CreateMessage`] operator which generates the message or command to be sent to the device. Once the properties have been configured, the node name changes to reflect the command being sent:
  - `MessageType` - A command could either be a `Write`, which tells the device to update some value (the most common function in the examples), or a `Read`, which tells the device to report some value. Messages that are received from the device are `Events` and are not used for commands.
  - `Payload` - This property specifies the target register for the message. A register is an address on the device with a name for any given functionality. For instance, the register to turn on one of the digital outputs on the Behavior board is named [`OutputSet`], so the corresponding payload is `OutputSetPayload`.
  - `OutputSet` - The remaining property is named after the selected register and holds the values that you can select for the payload. For instance, to turn on a specific output line with [`OutputSet`], you would either enter or select the value here, such as `Led0`, the **L0** LED output line.
- [`Behavior Commands`](xref:Bonsai.Expressions.MulticastSubject) - This is a [`MulticastSubject`] operator which will send the message or command to the subject named [`Behavior Commands`] in the [Harp device pattern](#harp-device-pattern). Even though the two operators are named the same, they can be distinguished by the different logos and colors.

> [!NOTE]
>  Other Bonsai operator [sources] can also be triggers. For instance, a [`Timer`] can be used instead to trigger the command at a set time after the workflow starts.

### Read Events from Device

To receive and read events from the device, you need:

- A subscription to the device event stream
- A parser that picks out the messages corresponding to one register and reads its values
- A display for the decoded values

:::workflow
![Receive Events from Device](../workflows/harp-receiveevents.bonsai)
:::

- [`Behavior Events`](xref:Bonsai.Expressions.SubscribeSubject) - This is a [`SubscribeSubject`] operator which subscribes to the message stream sent from the [`Behavior Events`] subject in the [Harp device pattern](#harp-device-pattern). Even though the two operators are named the same, they can be distinguished by the different logos and colors. Any number of branches can subscribe to the same subject.
- [`Behavior.TimestampedAnalogData`] - This operator is a [`Parse`] operator that filters the message stream for a single register and decodes its messages into a typed value. Similar to the [`CreateMessage`] operator, once the property is configured, the node changes its name to reflect the events that it is decoding. It only has one property to be configured:
  - `Register` - Set this property to the register whose events you wish to read. For instance, to read the analog data stream on the device, you would select the [`AnalogData`] register. Each event also comes in a timestamped form ([`TimestampedAnalogData`]) which includes the device timestamp of the message.
- [`VisualizerWindow`] - This operator opens a window when the workflow starts and displays each value it receives. 

Each example also shows what the visualizer displays. For instance, the [`TimestampedAnalogData`] visualizer should look like this:

```text
AnalogDataPayload { AnalogInput0 = 13, Encoder = 0, AnalogInput1 = 13 }@10.351072
```

[!INCLUDE [](version-footer.md)]

<!--Reference Style Links -->
[`AnalogData`]: xref:Harp.Behavior.AnalogData
[`Behavior`]: xref:Harp.Behavior.Device
[`Behavior Commands`]: xref:Bonsai.Reactive.BehaviorSubject
[`Behavior Events`]: xref:Bonsai.Reactive.PublishSubject
[`Behavior.OutputSetPayload`]: xref:Harp.Behavior.CreateMessage
[`Behavior.TimestampedAnalogData`]: xref:Harp.Behavior.Parse
[`BehaviorDataWriter`]: xref:Harp.Behavior.DeviceDataWriter
[`BehaviorSubject`]: xref:Bonsai.Reactive.BehaviorSubject
[`CreateMessage`]: xref:Harp.Behavior.CreateMessage
[`Device`]: xref:Harp.Behavior.Device
[`DeviceDataWriter`]: xref:Harp.Behavior.DeviceDataWriter
[`KeyDown`]: xref:Bonsai.Windows.Input.KeyDown
[`MulticastSubject`]: xref:Bonsai.Expressions.MulticastSubject
[`OutputSet`]: xref:Harp.Behavior.OutputSet
[`Parse`]: xref:Harp.Behavior.Parse
[`PublishSubject`]: xref:Bonsai.Reactive.PublishSubject
[sources]: https://bonsai-rx.org/docs/articles/operators.html#source
[`SubscribeSubject`]: xref:Bonsai.Expressions.SubscribeSubject
[`Timer`]: xref:Bonsai.Reactive.Timer
[`TimestampedAnalogData`]: xref:Harp.Behavior.Parse 
[`VisualizerWindow`]: xref:Bonsai.Design.VisualizerWindow
