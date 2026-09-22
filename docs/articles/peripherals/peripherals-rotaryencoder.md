## Rotary Encoder

The Harp Rotary Encoder is a peripheral for the Behavior board. It plugs directly into peripheral port **P2**, the only peripheral port with a quadrature counter. It can be used, for instance, to track the rotation of a running wheel. Refer to the [connections](../connections.md?tabs=encoder#connections) article to set up the peripheral and the [Track Rotary Encoder](../track-rotary-encoder.md) article to use it in Bonsai.

!["Rotary Encoder"](../../images/OEPS-RotaryEncoder.jpg){width=450}

### Key Features

- Onboard quadrature rotary encoder.
- Connects to a Behavior board peripheral port with a standard Ethernet cable (RJ45).
- Solder pads for wiring an external quadrature encoder to use instead of the onboard one.

### Specs

- Encoder: Alps Alpine EC12E24204A9 (mechanical, 24 pulses per revolution, with detents)

### Hardware

| Version | Compatible Behavior Board | Notes |
| ------- | ------------------- | ----- |
| 1.3 | > 1.0 | <ul><li> Fixed encoder direction </li></ul> |
| 1.2 | > 1.0 | <ul><li> Replaced the optical encoder with an Alps Alpine EC12E24204A9 mechanical encoder </li><li> Added pull-ups and filtering on the quadrature channels </li></ul> |
| 1.0 | > 1.0 | <ul><li> Initial version, using a Bourns ENC1J-D28-L00128 optical encoder (128 pulses per revolution) </li></ul> |

Assembled units are available from the [Open Ephys store](https://open-ephys.org/harp).

> [!NOTE]
> The [hardware repository](https://github.com/harp-tech/peripheral.encoder) is private and incomplete, if we want to link to it, it needs to be polished and made public.

[!INCLUDE [](../version-footer.md)]
