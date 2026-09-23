## Mice Poke

The Harp Behavior Mice Poke is a nose-poke peripheral for the Behavior board. It detects pokes with an infrared beam, drives onboard cue LEDs, and controls a solenoid valve for reward delivery. Refer to the [connections](../connections.md?tabs=poke#connections) article to set up the peripheral and the [Control Poke Peripheral](../control-poke.md) article to use it in Bonsai.

![Mice Poke With Water Valve](../../images/OEPS-MicePokeWaterValve.jpg){width=450}

### Key Features

- Infrared beam poke detection.
- Control of 2 onboard LEDs, with the option of connecting an external LED.
- 12 V solenoid valve control for reward delivery.

### Specs

- Connector: RJ45 for direct connection with Behavior board peripheral port, screw terminals for operating the peripheral with other external devices

### Hardware

| Version | Compatible Behavior Board | Notes |
| ------- | ------------------------- | ----- |
| 1.4 | > 1.0 | <ul><li> Update valve footprint </li><li> Increased LED resistor</li></ul> |
| 1.3 | > 1.0 | <ul><li> Screw terminals consolidated into a single block </li><li> Added infrared beam and photodiode access on the screw terminals </li></ul> |
| 1.2 | > 1.0 | <ul><li> Initial version </li></ul> |

Assembled units are available from the [Open Ephys store](https://open-ephys.org/harp), or build your own using the hardware design files in the [Mice Poke](https://github.com/harp-tech/peripheral.micepoke) repository.


> [!NOTE]
> Three variants are available from the Open Ephys store, choose the right variant for your needs:
> - OEPS-3007 - PCB only
> - OEPS-3008 - Includes mice poke enclosure, no water valve
> - OEPS-3010 - Includes mice poke enclosure and water valve

[!INCLUDE [](../version-footer.md)]
