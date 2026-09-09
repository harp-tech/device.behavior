## Ports and Connections

This article will cover the ports on the Behavior board, as well as how to connect the device to peripherals and external devices.

### Ports

![Harp Behavior Device Pinout](../images/behavior-devicepinout.svg){width=600}

**12V PWR (Barrel Jack)** - This port powers the Behavior board and requires a 12 V power supply.

**USB (Mini-B/USB-C)** - This port connects the device to the control computer running [Bonsai](harp-bonsai.md) or the [GUI](./behavior-gui.md). The connector type depends on the [hardware version](./behavior-overview.md) of the board.

**CLKIN (Stereo Jack)** - This [Harp](https://harp-tech.org/articles/about.html) clock input accepts a clock output from any compatible Harp clock generator or synchronizer (e.g. [Harp Timestamp Generator](https://github.com/harp-tech/device.timestampgeneratorgen3)) for synchronization with other connected devices.

**STATE** - This status LED cycles on and off with a period of:
- 2 seconds when it's communicating with Bonsai
- 4 seconds when in standby
- 100 milliseconds when a catastrophic error occurs

**P0, P1, P2 (RJ45)** - These specialized peripheral ports interface directly with the [Mice Poke](./peripherals/peripherals-micepoke.md) peripheral for poke detection and reward delivery. Alternatively, use the [Breakout](./peripherals/peripherals-portbreakout.md) extension board to interface with other peripherals and accessories. Port **P2** additionally accepts the [Rotary Encoder](./peripherals/peripherals-rotaryencoder.md) peripheral for [rotation tracking](track-rotary-encoder.md).

**ADC (Screw Terminal)** - This connector carries the first analog input (**AD0**), a 5 V supply pin and two ground connections.

**Output (Screw Terminal)** - These general-purpose 5 V digital outputs can be used to communicate with external devices. **DO0**/**DO1** can generate [camera triggers](trigger-cameras.md) and **DO2**/**DO3** can drive [servo motors](drive-servos.md); all four support [PWM](generate-pwm.md).

**Input (Screw Terminal)** - This connector carries the second analog input (**AD1**), the digital input **DI3** and one ground connection.

**RGB (3-pin Flick Lock)** - Connector for up to two WS2812-type (Neopixels) addressable RGB LEDs, driven as a serial chain on a single data line. Colors are set with the [RGB registers](control-leds.md#set-rgb-colors).

**L0, L1 (Screw Terminal)** - These connectors control two regular current-controlled LED outputs. Each terminal pair is marked **A** (anode, the LED's long leg) and **K** (cathode, the short leg). The drive current is [configurable](control-leds.md) from 2 to 100 mA.

> [!NOTE]
> Depending on the [hardware version](./behavior-overview.md) of your device, some of the ports above may be different. Always check the printed label on the PCB.

### Connections

> [!NOTE]
> Work in progress!

#### Connect a Nose Poke

[placeholder - connection-poke.svg]

1. Connect the [Mice Poke](./peripherals/peripherals-micepoke.md) peripheral to port **P0**, **P1**, or **P2** with an RJ45 cable.
2. Refer to the [Control Poke Peripheral](control-poke.md) article to configure the Mice Poke peripheral in Bonsai.

#### Connect an Encoder

[placeholder - connection-encoder.svg]

1. Connect the [Rotary Encoder](./peripherals/peripherals-rotaryencoder.md) peripheral to port **P2** with an RJ45 cable.
2. Refer to the [Track Rotary Encoder](track-rotary-encoder.md) article to read the encoder in Bonsai.

#### Connect a Speaker

[placeholder - connection-speaker.svg]

1. Wire the [Speaker](./peripherals/peripherals-speaker.md) peripheral to the **Output** screw terminal: the signal pin to **DO0** and the ground pin to **GND**.
2. Refer to the [Generate PWM](generate-pwm.md) article to play tones in Bonsai.

#### Connect a Photodiode

[placeholder - connection-photodiode.svg]

1. Wire the [Photodiode](./peripherals/peripherals-photodiode.md) peripheral to the **ADC** screw terminal: the supply wire to the 5 V pin, the ground wire to a **GND** pin, and the output wire to **AD0**.
2. Refer to the [Acquire Analog Data](acquire-analog-data.md) article to read the light intensity in Bonsai.

[!INCLUDE [](version-footer.md)]
