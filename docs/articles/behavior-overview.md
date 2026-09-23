## Behavior Board

![Harp Behavior](../images/OEPS-BehaviorBoard.jpg){width=450}

### Key Features

- 3 peripheral ports for plug-and-play peripherals.
- Hardware-timed pulses for all digital outputs and PWM for the four digital outputs on the **Output** connector.
- Support for camera triggering, servo motor control, and quadrature encoder counting on selected digital outputs.
- Configurable LED drive current (up to 100 mA) and RGB LED driver.

### Specs

- Analog inputs: 2 (0–5 V, 12-bit, sampled at 1 kHz)
    - 1 on the **ADC** connector
    - The second analog input on the **Input** connector is available on hardware v2.0 or later
- Digital inputs: 4 (5 V)
    - 3 on the peripheral ports, accessible with the [Breakout](./peripherals/peripherals-portbreakout.md) board
    - The fourth digital input on the **Input** connector is available on hardware v2.0 or later
- Digital outputs: 7 (5 V)
    - 4 on the **Output** connector
    - 3 on the peripheral ports, accessible with the [Breakout](./peripherals/peripherals-portbreakout.md) board
- Digital output pulse duration: 1–65535 ms (supported on all digital outputs)
- Digital output PWM: 1–10000 Hz, duty cycle 1–99% (supported on the **Output** connector only)
- Peripheral ports: 3 (RJ45)
    - For the [Mice Poke](./peripherals/peripherals-micepoke.md), [Rotary Encoder](./peripherals/peripherals-rotaryencoder.md), or [Breakout](./peripherals/peripherals-portbreakout.md) board
- LED outputs: 2 (2–100 mA drive current)
- RGB LED outputs: 2 (WS2812-type addressable LEDs connector, driven as a serial chain)
- Camera triggers: 2 (**DO0**/**DO1**, 2–600 Hz)
- Servo outputs: 2 (**DO2**/**DO3**)
- Quadrature encoder inputs: 1 (**P2**, sampled at 1 kHz)
- Timestamp resolution: 32 µs
- Synchronization frequency: 1 Hz
- Synchronization accuracy: 22 ± 16 µs (between clock generator and this device)

### Hardware

| Version | Notes |
| ------- | ----- |
| 2.1 | <ul><li> Changed USB connector from Mini-B to USB-C </li></ul> |
| 2.0 | <ul><li> Added an input connector with an extra analog input and digital input. </li></ul> |
| 1.2 | <ul><li> ? </li></ul> |
| 1.1 | <ul><li> Initial version </li></ul> |

> [!WARNING]
> **TODO**: v1.1 pcb files are not available in the repository, v1.2 pcb is the earliest files. Confirm which is the earliest production version.

### Firmware

| Version | Notes |
| ------- | ----- |
| 3.3 | <ul><li> Fixed the read function of the digital inputs register </li></ul> |
| 3.2 | <ul><li> Add limits to ServoMotor registers </li><li> Create displacement reading option for quadrature encoder. </li></ul> |
| 3.1 | <ul><li> Support for hardware 2.0 </li><li> Added serial timestamp registers </li></ul> |
| 3.0 | <ul><li> Initial version </li></ul> |

> [!WARNING]
> **TODO**: There are no earlier firmware versions than 3.0. See if there are any notes for the earlier versions.

[!INCLUDE [](version-footer.md)]
