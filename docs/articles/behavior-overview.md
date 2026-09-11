## Behavior

![Harp Behavior](../images/OEPS-BehaviorBoard.jpg){width=450}

### Key Features

- 3 peripheral ports for plug-and-play peripherals.
- Hardware-timed pulses on every output line, and PWM (up to 10 kHz with configurable duty cycle) on the four digital outputs on the output connector.
- Support for camera triggering, servo motor control, and quadrature encoder counting.
- Configurable LED drive current (up to 100 mA) and RGB LED driver.

### Specs

- Analog inputs: 2 (12-bit, sampled at 1 kHz; the second analog input is available on hardware 2.0 only)
- Digital inputs: 4 (1 on the input connector available on hardware 2.0 only, 3 on the peripheral ports accessible with [Breakout](./peripherals/peripherals-portbreakout.md) board, 5V)
- Digital outputs: 7 (4 on the output connector, 3 on the peripheral ports accessible with [Breakout](./peripherals/peripherals-portbreakout.md) board)
- Digital output pulse duration: 1 – 65535 ms (supported on all digital outputs)
- Digital output PWM: 1 Hz – 10 kHz, duty cycle 1 – 99% (supported on the output connector only).
- Peripheral ports: 3 (Compatible with [Mice Poke](./peripherals/peripherals-micepoke.md), [Rotary Encoder](./peripherals/peripherals-rotaryencoder.md), or [Breakout](./peripherals/peripherals-portbreakout.md) board)
- LED outputs: 2 (2 – 100 mA drive current)
- RGB LED outputs: 2 (WS2812-type addressable LEDs connector, driven as a serial chain)
- Camera triggers: 2 (**DO0**/**DO1**, 2 – 600 Hz)
- Servo outputs: 2 (**DO2**/**DO3**, period and pulse width in µs)
- Quadrature encoder inputs: 1 (on **P2**, read at 1 kHz)
- Timestamp resolution: 32 µs
- Synchronization frequency: 1 Hz
- Synchronization accuracy: 22 ± 16 µs (between clock generator and this device)

### Hardware

| Version | Notes |
| ------- | ----- |
| 2.0 | <ul><li> Added the input connector with second analog input and the third digital input. </li></ul> |
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
