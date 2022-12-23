## Harp Behavior
This is a multi-purpose device tailored to behavioral experiments. It allows control of pokes, RGB LEDs, LEDs, cameras, servo motors and a quadrature counter.

![HarpBehavior](./Assets/pcb.png)

### Key Features ###

* Enable pulse and configure duration for each output
* Support for PWM configuration (frequency up to 10KHz and duty cycle)
* LED current (up to 100mA) and maximum nominal current configuration
* Up to 2 cameras controlling (DO0 and DO1)
* Up to 2 servo motors (DO2 and DO3)
* Up to 1 quadrature counter (Port2) (Note: Port0 and Port1 can also act as quadrature counters but this is a request feature)


### Connectivity ###

* 1x clock sync input (CLKIN) [stereo jack]
* 1x USB (for computer) [USB type B]
* 1x 12V supply [barrel connector jack]
* 3x poke connectors (LED+12V valve) (Poke0, Poke1, Poke2) [RJ45]
* 2x LEDs outputs (5V) (LED0, LED1) [screw terminal]
* 2x RGB LEDs outputs (5V) (RGB0, RGB1) [flick lock 3x male pins]
* 4x general purpose digital outputs with PWM (5V) (DO0-DO3) [screw terminal]
* 1x ADC (5V) [screw terminal]

## Interface ##


The interface with the Harp Behavior can be done through [Bonsai](https://bonsai-rx.org/) or a dedicated GUI (Graphical User Interface).

In order to use this GUI, there are some software that needs to be installed:

1 - Install the [drivers](https://bitbucket.org/fchampalimaud/downloads/downloads/UsbDriver-2.12.26.zip).

2 - Install the [runtime](https://bitbucket.org/fchampalimaud/downloads/downloads/Runtime-1.0.zip).

3 - Reboot the computer.

4 - Install the [GUI](https://bitbucket.org/fchampalimaud/downloads/downloads/Harp%20Behavior%20v2.0.0.zip).

## Licensing ##

Each subdirectory will contain a license or, possibly, a set of licenses if it involves both hardware and software.