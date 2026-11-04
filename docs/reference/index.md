---
uid: Harp.Behavior.Device
title: Registers
---

# Behavior device

<div class="text-secondary d-flex flex-wrap column-gap-4">
  <dl><div><dt>WhoAmI</dt><dd>1216</dd></div></dl>
  <dl><div><dt>Log files</dt><dd><code>Behavior_&lt;address&gt;.bin</code></dd></div></dl>
</div>

Use the [Harp device pattern](https://harp-tech.org/articles/operators.html#device-pattern) to initialize the device, log data, and send commands to and receive messages from the Behavior device.

:::workflow
![Harp device pattern](../workflows/harp-devicepattern.bonsai)
:::

Check out the following in-depth guides to learn how to access the device functionality with the `Harp.Behavior` package:

- [Acquire analog data](../articles/acquire-analog-data.md)
- [Read digital inputs](../articles/read-digital-inputs.md)
- [Control digital outputs](../articles/control-digital-outputs.md)
- [Generate PWM](../articles/generate-pwm.md)
- [Control LEDs](../articles/control-leds.md)
- [Control poke peripheral](../articles/control-poke.md)
- [Mimic poke signals](../articles/mimic-poke-signals.md)
- [Track rotary encoder](../articles/track-rotary-encoder.md)
- [Trigger cameras](../articles/trigger-cameras.md)
- [Control servo motors](../articles/control-servo-motors.md)
- [Advanced configuration](../articles/advanced-configuration.md)

## Registers

The core registers implemented by every Harp device, such as `WhoAmI` and `OperationControl`, are described in the [Device Registers and Operation specification](https://harp-tech.org/protocol/Device.html).

[!INCLUDE [](../../artifacts/docs/registers/registers.md)]
