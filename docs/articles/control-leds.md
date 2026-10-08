## Control LEDs

The Behavior board drives two kinds of light output: two standard current-controlled LEDs on the **L0** and **L1** connectors and up to two WS2812-type addressable RGB LEDs on the **RGB** connector. Refer to the [connections](./connections.md?tabs=led#connections) article to set up the LED on **L0** or the **RGB** connector, which we will use for the rest of these examples.

This article covers configuring LED drive current, setting RGB colors, and turning on both kinds of LED output in Bonsai.

The complete workflow is shown below. Copy and paste it into Bonsai or build each section by following the step-by-step instructions below.

:::workflow
![Control LEDs](../workflows/controlleds-toplevel.bonsai)
:::

### Configure LED Current

The **L0** and **L1** outputs drive standard single-color LEDs with a configurable constant current, so no series resistor is needed. Each output has a working current and protection limit register (e.g. [`Led0Current`] and [`Led0MaxCurrent`] for **L0**). Writes above the limit are rejected by the device.

:::workflow
![Configure LED Current](../workflows/controlleds-current.bonsai)
:::

- Insert a [`KeyDown`] operator and set the `Filter` property to `A`.
- Insert a [`CreateMessage`] operator and configure the following properties:
    - `Payload` - Select `Led0MaxCurrentPayload`.
    - `Led0MaxCurrent` - Set the maximum allowed current according to the rating of the LED in mA (e.g. `20`). Valid values are 2–100 mA.
- Insert a [`MulticastSubject`] operator named `Behavior Commands`.
- Insert a [`CreateMessage`] operator and configure the following properties:
    - `Payload` - Select `Led0CurrentPayload`.
    - `Led0Current` - Set the working current in mA (e.g. `10`). Valid values are 5–100 mA.
- Insert a [`MulticastSubject`] operator named `Behavior Commands`.

Run the workflow and press <kbd>A</kbd> to configure the **L0** drive current, then switch the output on as shown in the next section.

### Turn LEDs On and Off

Once the drive current is configured, the **L0** and **L1** outputs are switched like any other digital output, for instance through the [`OutputSet`](control-digital-outputs.md) and [`OutputClear`](control-digital-outputs.md) registers:

:::workflow
![Turn LEDs On and Off](../workflows/controlleds-ledonoff.bonsai)
:::

- Insert a [`KeyDown`] operator and set the `Filter` property to `S`.
- Insert a [`CreateMessage`] operator and configure the following properties:
    - `Payload` - Select `OutputSetPayload`.
    - `OutputSet` - Select the line to turn on (e.g. `Led0`).
- Insert a [`MulticastSubject`] operator named `Behavior Commands`.

In a separate branch:

- Insert a [`KeyDown`] operator and set the `Filter` property to `D`.
- Insert a [`CreateMessage`] operator and configure the following properties:
    - `Payload` - Select `OutputClearPayload`.
    - `OutputClear` - Select the line to turn off (e.g. `Led0`).
- Insert a [`MulticastSubject`] operator named `Behavior Commands`.

Run the workflow, then press <kbd>S</kbd> to turn on the LED and <kbd>D</kbd> to turn it off. See [Control Digital Outputs](control-digital-outputs.md) for other methods to control the LED.

### Set RGB Colors

The **RGB** connector drives WS2812-type individually addressable LEDs. The two LEDs form a serial chain on a single data line: **Rgb0** is the first LED in the chain and **Rgb1** the second when configuring and controlling these LEDs. Single-color LEDs have no data input and do not work on this connector; drive those from the [L0/L1 outputs](#configure-led-current) instead. Analog RGB LEDs are not supported on either.

The [`RgbAll`] register writes the color of both RGB LEDs in one command. Each channel takes an intensity from 0 to 255.

:::workflow
![Set RGB Colors](../workflows/controlleds-rgbcolors.bonsai)
:::

- Insert a [`KeyDown`] operator and set the `Filter` property to `F`.
- Insert a [`CreateMessage`] operator and configure the following properties:
    - `Payload` - Select `RgbAllPayload`.
    - `Red0` - Set the value for the red channel on **Rgb0** (e.g. `255`).
    - `Blue1` - Set the value for the blue channel on **Rgb1** (e.g. `255`).
- Insert a [`MulticastSubject`] operator named `Behavior Commands`.

Run the workflow and press <kbd>F</kbd> to load the colors. If the LEDs are currently on, the new colors take effect immediately. If not, when you turn them on in the next section, , **Rgb0** should display red and **Rgb1** should be blue.

> [!NOTE]
> To configure one LED at a time, use the [`Rgb0`] or [`Rgb1`] registers instead.

> [!NOTE]
> There is a [bug](https://github.com/harp-tech/device.behavior/issues/41) in the current firmware (v3.3) where the red and green channels are switched (e.g. `Red0` controls the LED green channel instead). As a workaround, swap the two channel values to get the intended color.

### Turn RGB LEDs On and Off

The RGB LEDs are switched like any other digital output, for instance through the [`OutputSet`](control-digital-outputs.md) and [`OutputClear`](control-digital-outputs.md) registers:

:::workflow
![Turn RGB LEDs On and Off](../workflows/controlleds-rgbonoff.bonsai)
:::

- Insert a [`KeyDown`] operator and set the `Filter` property to `G`.
- Insert a [`CreateMessage`] operator and configure the following properties:
    - `Payload` - Select `OutputSetPayload`.
    - `OutputSet` - Set the lines to turn on (e.g. `Rgb0, Rgb1`).
- Insert a [`MulticastSubject`] operator named `Behavior Commands`.

In a separate branch:

- Insert a [`KeyDown`] operator and set the `Filter` property to `H`.
- Insert a [`CreateMessage`] operator and configure the following properties:
    - `Payload` - Select `OutputClearPayload`.
    - `OutputClear` - Set the lines to turn off (e.g.  `Rgb0, Rgb1`).
- Insert a [`MulticastSubject`] operator named `Behavior Commands`.

Run the workflow, then press <kbd>G</kbd> to turn on both RGB LEDs and <kbd>H</kbd> to turn them off.

[!INCLUDE [](version-footer.md)]

<!--Reference Style Links -->
[`KeyDown`]: xref:Bonsai.Windows.Input.KeyDown
[`CreateMessage`]: xref:Harp.Behavior.CreateMessage
[`MulticastSubject`]: xref:Bonsai.Expressions.MulticastSubject
[`RgbAll`]: xref:Harp.Behavior.RgbAll
[`Rgb0`]: xref:Harp.Behavior.Rgb0
[`Rgb1`]: xref:Harp.Behavior.Rgb1
[`Led0Current`]: xref:Harp.Behavior.Led0Current
[`Led1Current`]: xref:Harp.Behavior.Led1Current
[`Led0MaxCurrent`]: xref:Harp.Behavior.Led0MaxCurrent
