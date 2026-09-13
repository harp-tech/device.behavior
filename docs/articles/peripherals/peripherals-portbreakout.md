## Breakout

The Harp Breakout is an interface peripheral for the Behavior board. It makes the pins of the Behavior board [peripheral ports](../connections.md) available on screw terminals without building a custom cable.

!["Breakout board"](../../images/OEPS-Breakout.png){width=450}

### Key Features

- Supports 12 V solenoid valves (e.g. Lee LHD series) (v1.1 only).
- Supports [serial timestamp](../advanced-configuration.md#stream-timestamps) output, with the logic level selected by the onboard jumper (v2.x only).

> [!NOTE]
> Pick the board version for which feature you need, the two features are mutually exclusive as they use the same pins.

### Ports

- 1x Digital Input (DI)
- 1x Digital Input / Output (DIO)
- 1x Digital Output (DO)
- 1x 5V supply (+5V)
- 1x Ground (GND)
- 1x 12 V supply (+12V) (v1.1 only)
- 1x Supply return (compatible for +5V and +12V) (SUP_RTN) (v1.1 only)
- 1x Serial TX (v2.x only)

For connection with a quadrature encoder:

| Quadrature Encoder Pin | Breakout Board Pin |
| ---------------------- | ------------------ |	                               
| A                      | DI 	              | 
| B                      | DIO 	              |
| Supply                 | +5V                |
| Ground                 | GND                | 

### Hardware

| Version | Compatible Behavior Board | Notes |
| ------- | ------------------- | ----- |
| 2.x | > 2.0 | <ul><li> Serial TX terminal replaces the +12V and SUP_RTN terminals </li><li> TX logic level jumper-selectable between 3.3 V and 5 V </li></ul> |
| 1.1 | > 1.2 | <ul><li> Initial version </li></ul> |

Assembled units are available from the [Open Ephys store](https://open-ephys.org/harp), or build your own using the hardware design files in the [Breakout](https://github.com/harp-tech/peripheral.portbreakout) repository.

[!INCLUDE [](../version-footer.md)]
