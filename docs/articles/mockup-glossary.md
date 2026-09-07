## Glossary

Harp and Bonsai give some everyday words a precise meaning. This page defines the terms the User Guide relies on. Articles link here the first time a term appears; each definition links onward to the article that covers the topic in depth.

### Harp Message

Everything a Harp device sends or receives travels as a Harp message. Each message carries the address of the register it concerns, a message type (`Read`, `Write`, or `Event`), the payload data, and a timestamp from the device clock. The full binary format is described in the [Harp protocol documentation](https://harp-tech.org/protocol/BinaryProtocol-8bit.html).

### Command

A Harp message with the `Write` type. It sets the value of a register, instructing the device to act, for example turning on an output line. The device confirms every command with a timestamped reply. In Bonsai, commands are built with the [`CreateMessage`] operator.

### Event

A Harp message the device sends on its own, without a request, whenever something happens on the board: a beam break, a new analog sample, a camera frame trigger. Events carry their own hardware [timestamp](#timestamp) and are the main way measurements reach the computer. Do not confuse them with key presses or other user actions in Bonsai; the articles call those triggers. The [`EventEnable`] register controls which events the device broadcasts.

### Register

A named value at a fixed address on the device, such as [`OutputSet`] (address 34). Registers are the entire interface of a Harp device: you command the device by writing registers, query it by reading them, and receive events that report them. Not related to CPU registers. The [register table](xref:Harp.Behavior.Device) lists every register on the Behavior board.

### Payload

The data carried by a Harp message: the value written to a register by a command, or reported back by a reply or event. Each register defines its payload type, such as a single 8-bit integer or an array of values. In Bonsai, the `Payload` property of [`CreateMessage`] selects the target register and its value together.

### Mask Register

A register where each bit of the payload selects one line, so a single command can act on several lines at once. Most output registers on the Behavior board are mask registers; type comma-separated names (e.g. `DO0, DO1`) to select multiple lines in Bonsai. Contrast value registers, where the payload is a single code and only one option is valid at a time.

### Timestamp

The time a message was created, stamped by the device clock in whole seconds plus 32 µs fractions. Timestamps are applied in hardware, so they are unaffected by USB or operating system delays. Use them, rather than computer time, to align data; see [Logging and Analysis](./logging-analysis.md).

[!INCLUDE [](version-footer.md)]

<!--Reference Style Links -->
[`CreateMessage`]: xref:Harp.Behavior.CreateMessage
[`EventEnable`]: xref:Harp.Behavior.EventEnable
[`OutputSet`]: xref:Harp.Behavior.OutputSet
