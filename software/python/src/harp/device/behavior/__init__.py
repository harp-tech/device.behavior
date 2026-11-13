# This file was automatically generated and should not be edited directly.
# To make changes, edit the device metadata and regenerate the interface.

import enum
from typing import Any, ClassVar

import numpy as np
from harp.protocol import (
    AnonymousPayload,
    BitMask,
    Field,
    GroupMask,
    IdentityConverter,
    PayloadType,
    RegisterBase,
    RegisterU8,
    StructPayload,
)
from harp.device.core import REGISTER_MAP as _CORE_REGISTER_MAP


__all__ = [
    "DEVICE_NAME",
    "WHO_AM_I",
    "DigitalInputs",
    "DigitalOutputs",
    "PortDIOLines",
    "PwmOutputs",
    "Events",
    "CameraOutputs",
    "ServoOutputs",
    "EncoderInputs",
    "FrameAcquired",
    "SerialTimestampPorts",
    "MimicOutput",
    "EncoderModeMask",
    "DigitalInputStatePayload",
    "OutputSetPayload",
    "OutputClearPayload",
    "OutputTogglePayload",
    "OutputStatePayload",
    "PortDIOSetPayload",
    "PortDIOClearPayload",
    "PortDIOTogglePayload",
    "PortDIOStatePayload",
    "PortDIODirectionPayload",
    "PortDIOStateEventPayload",
    "AnalogDataPayload",
    "OutputPulseEnablePayload",
    "PulseDOPort0Payload",
    "PulseDOPort1Payload",
    "PulseDOPort2Payload",
    "PulseSupplyPort0Payload",
    "PulseSupplyPort1Payload",
    "PulseSupplyPort2Payload",
    "PulseLed0Payload",
    "PulseLed1Payload",
    "PulseRgb0Payload",
    "PulseRgb1Payload",
    "PulseDO0Payload",
    "PulseDO1Payload",
    "PulseDO2Payload",
    "PulseDO3Payload",
    "PwmFrequencyDO0Payload",
    "PwmFrequencyDO1Payload",
    "PwmFrequencyDO2Payload",
    "PwmFrequencyDO3Payload",
    "PwmDutyCycleDO0Payload",
    "PwmDutyCycleDO1Payload",
    "PwmDutyCycleDO2Payload",
    "PwmDutyCycleDO3Payload",
    "PwmStartPayload",
    "PwmStopPayload",
    "RgbAllPayload",
    "RgbPayload",
    "Led0CurrentPayload",
    "Led1CurrentPayload",
    "Led0MaxCurrentPayload",
    "Led1MaxCurrentPayload",
    "EventEnablePayload",
    "StartCamerasPayload",
    "StopCamerasPayload",
    "EnableServosPayload",
    "DisableServosPayload",
    "EnableEncodersPayload",
    "EncoderModePayload",
    "Camera0FramePayload",
    "Camera0FrequencyPayload",
    "Camera1FramePayload",
    "Camera1FrequencyPayload",
    "ServoMotor2PeriodPayload",
    "ServoMotor2PulsePayload",
    "ServoMotor3PeriodPayload",
    "ServoMotor3PulsePayload",
    "EncoderResetPayload",
    "EnableSerialTimestampPayload",
    "MimicPort0IRPayload",
    "MimicPort1IRPayload",
    "MimicPort2IRPayload",
    "MimicPort0ValvePayload",
    "MimicPort1ValvePayload",
    "MimicPort2ValvePayload",
    "PokeInputFilterPayload",
    "DigitalInputState",
    "OutputSet",
    "OutputClear",
    "OutputToggle",
    "OutputState",
    "PortDIOSet",
    "PortDIOClear",
    "PortDIOToggle",
    "PortDIOState",
    "PortDIODirection",
    "PortDIOStateEvent",
    "AnalogData",
    "OutputPulseEnable",
    "PulseDOPort0",
    "PulseDOPort1",
    "PulseDOPort2",
    "PulseSupplyPort0",
    "PulseSupplyPort1",
    "PulseSupplyPort2",
    "PulseLed0",
    "PulseLed1",
    "PulseRgb0",
    "PulseRgb1",
    "PulseDO0",
    "PulseDO1",
    "PulseDO2",
    "PulseDO3",
    "PwmFrequencyDO0",
    "PwmFrequencyDO1",
    "PwmFrequencyDO2",
    "PwmFrequencyDO3",
    "PwmDutyCycleDO0",
    "PwmDutyCycleDO1",
    "PwmDutyCycleDO2",
    "PwmDutyCycleDO3",
    "PwmStart",
    "PwmStop",
    "RgbAll",
    "Rgb0",
    "Rgb1",
    "Led0Current",
    "Led1Current",
    "Led0MaxCurrent",
    "Led1MaxCurrent",
    "EventEnable",
    "StartCameras",
    "StopCameras",
    "EnableServos",
    "DisableServos",
    "EnableEncoders",
    "EncoderMode",
    "Camera0Frame",
    "Camera0Frequency",
    "Camera1Frame",
    "Camera1Frequency",
    "ServoMotor2Period",
    "ServoMotor2Pulse",
    "ServoMotor3Period",
    "ServoMotor3Pulse",
    "EncoderReset",
    "EnableSerialTimestamp",
    "MimicPort0IR",
    "MimicPort1IR",
    "MimicPort2IR",
    "MimicPort0Valve",
    "MimicPort1Valve",
    "MimicPort2Valve",
    "PokeInputFilter",
    "REGISTER_MAP",
]

DEVICE_NAME: str = "Behavior"
WHO_AM_I: int = 1216


class DigitalInputs(enum.IntFlag):
    """Specifies the state of port digital input lines."""

    DI_PORT0 = 0x1
    """Port 0 digital input"""

    DI_PORT1 = 0x2
    """Port 1 digital input"""

    DI_PORT2 = 0x4
    """Port 2 digital input"""

    DI3 = 0x8
    """Digital input DI3"""


class DigitalOutputs(enum.IntFlag):
    """Specifies the state of port digital output lines."""

    DO_PORT0 = 0x1
    """Port 0 digital output"""

    DO_PORT1 = 0x2
    """Port 1 digital output"""

    DO_PORT2 = 0x4
    """Port 2 digital output"""

    SUPPLY_PORT0 = 0x8
    """Port 0 12 V output for valve control"""

    SUPPLY_PORT1 = 0x10
    """Port 1 12 V output for valve control"""

    SUPPLY_PORT2 = 0x20
    """Port 2 12 V output for valve control"""

    LED0 = 0x40
    """LED output L0"""

    LED1 = 0x80
    """LED output L1"""

    RGB0 = 0x100
    """RGB LED 0 on the RGB connector"""

    RGB1 = 0x200
    """RGB LED 1 on the RGB connector"""

    DO0 = 0x400
    """Digital output DO0"""

    DO1 = 0x800
    """Digital output DO1"""

    DO2 = 0x1000
    """Digital output DO2"""

    DO3 = 0x2000
    """Digital output DO3"""


class PortDIOLines(enum.IntFlag):
    """Specifies the state of the port DIO lines."""

    DIO0 = 0x1
    """Port 0 DIO line"""

    DIO1 = 0x2
    """Port 1 DIO line"""

    DIO2 = 0x4
    """Port 2 DIO line"""


class PwmOutputs(enum.IntFlag):
    """Specifies the state of PWM output lines."""

    PWM_DO0 = 0x1
    """PWM on digital output DO0"""

    PWM_DO1 = 0x2
    """PWM on digital output DO1"""

    PWM_DO2 = 0x4
    """PWM on digital output DO2"""

    PWM_DO3 = 0x8
    """PWM on digital output DO3"""


class Events(enum.IntFlag):
    """Specifies the active events in the device."""

    PORT_DI = 0x1
    """Event from register DigitalInputState"""

    PORT_DIO = 0x2
    """Event from register PortDIOStateEvent"""

    ANALOG_DATA = 0x4
    """Event from register AnalogData"""

    CAMERA0 = 0x8
    """Event from register Camera0Frame"""

    CAMERA1 = 0x10
    """Event from register Camera1Frame"""


class CameraOutputs(enum.IntFlag):
    """Specifies camera output enable bits."""

    CAMERA_OUTPUT0 = 0x1
    """Camera on digital output 0"""

    CAMERA_OUTPUT1 = 0x2
    """Camera on digital output 1"""


class ServoOutputs(enum.IntFlag):
    """Specifies servo output enable bits."""

    SERVO_OUTPUT2 = 0x4
    """Servo on digital output 2"""

    SERVO_OUTPUT3 = 0x8
    """Servo on digital output 3"""


class EncoderInputs(enum.IntFlag):
    """Specifies quadrature counter enable bits."""

    ENCODER_PORT2 = 0x4
    """Encoder on port 2"""


class FrameAcquired(enum.IntFlag):
    """Specifies that camera frame was acquired."""

    FRAME_ACQUIRED = 0x1
    """Camera frame was triggered"""


class SerialTimestampPorts(enum.IntFlag):
    """Specifies available timestamp TX ports."""

    TIMESTAMP_PORT2 = 0x4
    """Enable the serial timestamp TX on Port 2"""


class MimicOutput(enum.IntEnum):
    """Specifies the target IO on which to mimic the specified register."""

    NONE = 0
    """Is not reflected on any output"""

    DIO0 = 1
    """Is reflected on DIO0"""

    DIO1 = 2
    """Is reflected on DIO1"""

    DIO2 = 3
    """Is reflected on DIO2"""

    DO0 = 4
    """Is reflected on DO0"""

    DO1 = 5
    """Is reflected on DO1"""

    DO2 = 6
    """Is reflected on DO2"""

    DO3 = 7
    """Is reflected on DO3"""


class EncoderModeMask(enum.IntEnum):
    """Specifies the type of reading made from the quadrature encoder."""

    POSITION = 0
    """The accumulated encoder count"""

    DISPLACEMENT = 1
    """The change in encoder count since the previous sample"""


class DigitalInputStatePayload(AnonymousPayload[np.uint8]):
    """Represents the DigitalInputState payload."""

    __value__: DigitalInputs = BitMask(enum=DigitalInputs)


class OutputSetPayload(AnonymousPayload[np.uint16]):
    """Represents the OutputSet payload."""

    __value__: DigitalOutputs = BitMask(enum=DigitalOutputs)


class OutputClearPayload(AnonymousPayload[np.uint16]):
    """Represents the OutputClear payload."""

    __value__: DigitalOutputs = BitMask(enum=DigitalOutputs)


class OutputTogglePayload(AnonymousPayload[np.uint16]):
    """Represents the OutputToggle payload."""

    __value__: DigitalOutputs = BitMask(enum=DigitalOutputs)


class OutputStatePayload(AnonymousPayload[np.uint16]):
    """Represents the OutputState payload."""

    __value__: DigitalOutputs = BitMask(enum=DigitalOutputs)


class PortDIOSetPayload(AnonymousPayload[np.uint8]):
    """Represents the PortDIOSet payload."""

    __value__: PortDIOLines = BitMask(enum=PortDIOLines)


class PortDIOClearPayload(AnonymousPayload[np.uint8]):
    """Represents the PortDIOClear payload."""

    __value__: PortDIOLines = BitMask(enum=PortDIOLines)


class PortDIOTogglePayload(AnonymousPayload[np.uint8]):
    """Represents the PortDIOToggle payload."""

    __value__: PortDIOLines = BitMask(enum=PortDIOLines)


class PortDIOStatePayload(AnonymousPayload[np.uint8]):
    """Represents the PortDIOState payload."""

    __value__: PortDIOLines = BitMask(enum=PortDIOLines)


class PortDIODirectionPayload(AnonymousPayload[np.uint8]):
    """Represents the PortDIODirection payload."""

    __value__: PortDIOLines = BitMask(enum=PortDIOLines)


class PortDIOStateEventPayload(AnonymousPayload[np.uint8]):
    """Represents the PortDIOStateEvent payload."""

    __value__: PortDIOLines = BitMask(enum=PortDIOLines)


class AnalogDataPayload(StructPayload[np.int16], length=3):
    """Represents the AnalogData payload."""

    analog_input0: np.int16 = Field(IdentityConverter(np.int16))
    """The voltage at the output of the ADC channel 0."""

    encoder: np.int16 = Field(IdentityConverter(np.int16), offset=1)
    """The quadrature counter value on Port 2"""

    analog_input1: np.int16 = Field(IdentityConverter(np.int16), offset=2)
    """The voltage at the output of the ADC channel 1."""


class OutputPulseEnablePayload(AnonymousPayload[np.uint16]):
    """Represents the OutputPulseEnable payload."""

    __value__: DigitalOutputs = BitMask(
        enum=DigitalOutputs,
        default=DigitalOutputs.SUPPLY_PORT0 | DigitalOutputs.SUPPLY_PORT1 | DigitalOutputs.SUPPLY_PORT2,
    )


class PulseDOPort0Payload(AnonymousPayload[np.uint16]):
    """Represents the PulseDOPort0 payload."""

    __value__: np.uint16 = Field(IdentityConverter(np.uint16), default=np.uint16(500))


class PulseDOPort1Payload(AnonymousPayload[np.uint16]):
    """Represents the PulseDOPort1 payload."""

    __value__: np.uint16 = Field(IdentityConverter(np.uint16), default=np.uint16(500))


class PulseDOPort2Payload(AnonymousPayload[np.uint16]):
    """Represents the PulseDOPort2 payload."""

    __value__: np.uint16 = Field(IdentityConverter(np.uint16), default=np.uint16(500))


class PulseSupplyPort0Payload(AnonymousPayload[np.uint16]):
    """Represents the PulseSupplyPort0 payload."""

    __value__: np.uint16 = Field(IdentityConverter(np.uint16), default=np.uint16(15))


class PulseSupplyPort1Payload(AnonymousPayload[np.uint16]):
    """Represents the PulseSupplyPort1 payload."""

    __value__: np.uint16 = Field(IdentityConverter(np.uint16), default=np.uint16(15))


class PulseSupplyPort2Payload(AnonymousPayload[np.uint16]):
    """Represents the PulseSupplyPort2 payload."""

    __value__: np.uint16 = Field(IdentityConverter(np.uint16), default=np.uint16(15))


class PulseLed0Payload(AnonymousPayload[np.uint16]):
    """Represents the PulseLed0 payload."""

    __value__: np.uint16 = Field(IdentityConverter(np.uint16), default=np.uint16(500))


class PulseLed1Payload(AnonymousPayload[np.uint16]):
    """Represents the PulseLed1 payload."""

    __value__: np.uint16 = Field(IdentityConverter(np.uint16), default=np.uint16(500))


class PulseRgb0Payload(AnonymousPayload[np.uint16]):
    """Represents the PulseRgb0 payload."""

    __value__: np.uint16 = Field(IdentityConverter(np.uint16), default=np.uint16(500))


class PulseRgb1Payload(AnonymousPayload[np.uint16]):
    """Represents the PulseRgb1 payload."""

    __value__: np.uint16 = Field(IdentityConverter(np.uint16), default=np.uint16(500))


class PulseDO0Payload(AnonymousPayload[np.uint16]):
    """Represents the PulseDO0 payload."""

    __value__: np.uint16 = Field(IdentityConverter(np.uint16), default=np.uint16(250))


class PulseDO1Payload(AnonymousPayload[np.uint16]):
    """Represents the PulseDO1 payload."""

    __value__: np.uint16 = Field(IdentityConverter(np.uint16), default=np.uint16(250))


class PulseDO2Payload(AnonymousPayload[np.uint16]):
    """Represents the PulseDO2 payload."""

    __value__: np.uint16 = Field(IdentityConverter(np.uint16), default=np.uint16(250))


class PulseDO3Payload(AnonymousPayload[np.uint16]):
    """Represents the PulseDO3 payload."""

    __value__: np.uint16 = Field(IdentityConverter(np.uint16), default=np.uint16(250))


class PwmFrequencyDO0Payload(AnonymousPayload[np.uint16]):
    """Represents the PwmFrequencyDO0 payload."""

    __value__: np.uint16 = Field(IdentityConverter(np.uint16), default=np.uint16(1000))


class PwmFrequencyDO1Payload(AnonymousPayload[np.uint16]):
    """Represents the PwmFrequencyDO1 payload."""

    __value__: np.uint16 = Field(IdentityConverter(np.uint16), default=np.uint16(2000))


class PwmFrequencyDO2Payload(AnonymousPayload[np.uint16]):
    """Represents the PwmFrequencyDO2 payload."""

    __value__: np.uint16 = Field(IdentityConverter(np.uint16), default=np.uint16(3000))


class PwmFrequencyDO3Payload(AnonymousPayload[np.uint16]):
    """Represents the PwmFrequencyDO3 payload."""

    __value__: np.uint16 = Field(IdentityConverter(np.uint16), default=np.uint16(4000))


class PwmDutyCycleDO0Payload(AnonymousPayload[np.uint8]):
    """Represents the PwmDutyCycleDO0 payload."""

    __value__: np.uint8 = Field(IdentityConverter(np.uint8), default=np.uint8(50))


class PwmDutyCycleDO1Payload(AnonymousPayload[np.uint8]):
    """Represents the PwmDutyCycleDO1 payload."""

    __value__: np.uint8 = Field(IdentityConverter(np.uint8), default=np.uint8(50))


class PwmDutyCycleDO2Payload(AnonymousPayload[np.uint8]):
    """Represents the PwmDutyCycleDO2 payload."""

    __value__: np.uint8 = Field(IdentityConverter(np.uint8), default=np.uint8(50))


class PwmDutyCycleDO3Payload(AnonymousPayload[np.uint8]):
    """Represents the PwmDutyCycleDO3 payload."""

    __value__: np.uint8 = Field(IdentityConverter(np.uint8), default=np.uint8(50))


class PwmStartPayload(AnonymousPayload[np.uint8]):
    """Represents the PwmStart payload."""

    __value__: PwmOutputs = BitMask(enum=PwmOutputs)


class PwmStopPayload(AnonymousPayload[np.uint8]):
    """Represents the PwmStop payload."""

    __value__: PwmOutputs = BitMask(enum=PwmOutputs)


class RgbAllPayload(StructPayload[np.uint8], length=6):
    """Represents the RgbAll payload."""

    green0: np.uint8 = Field(IdentityConverter(np.uint8))
    """The intensity of the green channel in the RGB0 LED."""

    red0: np.uint8 = Field(IdentityConverter(np.uint8), offset=1)
    """The intensity of the red channel in the RGB0 LED."""

    blue0: np.uint8 = Field(IdentityConverter(np.uint8), offset=2)
    """The intensity of the blue channel in the RGB0 LED."""

    green1: np.uint8 = Field(IdentityConverter(np.uint8), offset=3)
    """The intensity of the green channel in the RGB1 LED."""

    red1: np.uint8 = Field(IdentityConverter(np.uint8), offset=4)
    """The intensity of the red channel in the RGB1 LED."""

    blue1: np.uint8 = Field(IdentityConverter(np.uint8), offset=5)
    """The intensity of the blue channel in the RGB1 LED."""


class RgbPayload(StructPayload[np.uint8], length=3):
    """Represents the Rgb payload."""

    green: np.uint8 = Field(IdentityConverter(np.uint8))
    """The intensity of the green channel in the RGB LED."""

    red: np.uint8 = Field(IdentityConverter(np.uint8), offset=1)
    """The intensity of the red channel in the RGB LED."""

    blue: np.uint8 = Field(IdentityConverter(np.uint8), offset=2)
    """The intensity of the blue channel in the RGB LED."""


class Led0CurrentPayload(AnonymousPayload[np.uint8]):
    """Represents the Led0Current payload."""

    __value__: np.uint8 = Field(IdentityConverter(np.uint8), default=np.uint8(10))


class Led1CurrentPayload(AnonymousPayload[np.uint8]):
    """Represents the Led1Current payload."""

    __value__: np.uint8 = Field(IdentityConverter(np.uint8), default=np.uint8(10))


class Led0MaxCurrentPayload(AnonymousPayload[np.uint8]):
    """Represents the Led0MaxCurrent payload."""

    __value__: np.uint8 = Field(IdentityConverter(np.uint8), default=np.uint8(30))


class Led1MaxCurrentPayload(AnonymousPayload[np.uint8]):
    """Represents the Led1MaxCurrent payload."""

    __value__: np.uint8 = Field(IdentityConverter(np.uint8), default=np.uint8(30))


class EventEnablePayload(AnonymousPayload[np.uint8]):
    """Represents the EventEnable payload."""

    __value__: Events = BitMask(
        enum=Events,
        default=Events.PORT_DI | Events.PORT_DIO | Events.ANALOG_DATA | Events.CAMERA0 | Events.CAMERA1,
    )


class StartCamerasPayload(AnonymousPayload[np.uint8]):
    """Represents the StartCameras payload."""

    __value__: CameraOutputs = BitMask(enum=CameraOutputs)


class StopCamerasPayload(AnonymousPayload[np.uint8]):
    """Represents the StopCameras payload."""

    __value__: CameraOutputs = BitMask(enum=CameraOutputs)


class EnableServosPayload(AnonymousPayload[np.uint8]):
    """Represents the EnableServos payload."""

    __value__: ServoOutputs = BitMask(enum=ServoOutputs)


class DisableServosPayload(AnonymousPayload[np.uint8]):
    """Represents the DisableServos payload."""

    __value__: ServoOutputs = BitMask(enum=ServoOutputs)


class EnableEncodersPayload(AnonymousPayload[np.uint8]):
    """Represents the EnableEncoders payload."""

    __value__: EncoderInputs = BitMask(enum=EncoderInputs)


class EncoderModePayload(AnonymousPayload[np.uint8]):
    """Represents the EncoderMode payload."""

    __value__: EncoderModeMask = GroupMask(enum=EncoderModeMask, mask=0xFF)


class Camera0FramePayload(AnonymousPayload[np.uint8]):
    """Represents the Camera0Frame payload."""

    __value__: FrameAcquired = BitMask(enum=FrameAcquired)


class Camera0FrequencyPayload(AnonymousPayload[np.uint16]):
    """Represents the Camera0Frequency payload."""

    __value__: np.uint16 = Field(IdentityConverter(np.uint16), default=np.uint16(30))


class Camera1FramePayload(AnonymousPayload[np.uint8]):
    """Represents the Camera1Frame payload."""

    __value__: FrameAcquired = BitMask(enum=FrameAcquired)


class Camera1FrequencyPayload(AnonymousPayload[np.uint16]):
    """Represents the Camera1Frequency payload."""

    __value__: np.uint16 = Field(IdentityConverter(np.uint16), default=np.uint16(30))


class ServoMotor2PeriodPayload(AnonymousPayload[np.uint16]):
    """Represents the ServoMotor2Period payload."""

    __value__: np.uint16 = Field(IdentityConverter(np.uint16), default=np.uint16(20000))


class ServoMotor2PulsePayload(AnonymousPayload[np.uint16]):
    """Represents the ServoMotor2Pulse payload."""

    __value__: np.uint16 = Field(IdentityConverter(np.uint16), default=np.uint16(1500))


class ServoMotor3PeriodPayload(AnonymousPayload[np.uint16]):
    """Represents the ServoMotor3Period payload."""

    __value__: np.uint16 = Field(IdentityConverter(np.uint16), default=np.uint16(20000))


class ServoMotor3PulsePayload(AnonymousPayload[np.uint16]):
    """Represents the ServoMotor3Pulse payload."""

    __value__: np.uint16 = Field(IdentityConverter(np.uint16), default=np.uint16(1500))


class EncoderResetPayload(AnonymousPayload[np.uint8]):
    """Represents the EncoderReset payload."""

    __value__: EncoderInputs = BitMask(enum=EncoderInputs)


class EnableSerialTimestampPayload(AnonymousPayload[np.uint8]):
    """Represents the EnableSerialTimestamp payload."""

    __value__: SerialTimestampPorts = BitMask(enum=SerialTimestampPorts)


class MimicPort0IRPayload(AnonymousPayload[np.uint8]):
    """Represents the MimicPort0IR payload."""

    __value__: MimicOutput = GroupMask(enum=MimicOutput, mask=0xFF)


class MimicPort1IRPayload(AnonymousPayload[np.uint8]):
    """Represents the MimicPort1IR payload."""

    __value__: MimicOutput = GroupMask(enum=MimicOutput, mask=0xFF)


class MimicPort2IRPayload(AnonymousPayload[np.uint8]):
    """Represents the MimicPort2IR payload."""

    __value__: MimicOutput = GroupMask(enum=MimicOutput, mask=0xFF)


class MimicPort0ValvePayload(AnonymousPayload[np.uint8]):
    """Represents the MimicPort0Valve payload."""

    __value__: MimicOutput = GroupMask(enum=MimicOutput, mask=0xFF)


class MimicPort1ValvePayload(AnonymousPayload[np.uint8]):
    """Represents the MimicPort1Valve payload."""

    __value__: MimicOutput = GroupMask(enum=MimicOutput, mask=0xFF)


class MimicPort2ValvePayload(AnonymousPayload[np.uint8]):
    """Represents the MimicPort2Valve payload."""

    __value__: MimicOutput = GroupMask(enum=MimicOutput, mask=0xFF)


class PokeInputFilterPayload(AnonymousPayload[np.uint8]):
    """Represents the PokeInputFilter payload."""

    __value__: np.uint8 = Field(IdentityConverter(np.uint8), default=np.uint8(1))


class DigitalInputState(RegisterBase[DigitalInputs]):
    """Reflects the state of DI digital lines of each Port"""

    address: ClassVar[int] = 32
    payload_type: ClassVar[PayloadType] = PayloadType.U8
    payload_class = DigitalInputStatePayload


class _Reserved0(RegisterU8):
    """Reserved for future use"""

    address: ClassVar[int] = 33


class OutputSet(RegisterBase[DigitalOutputs]):
    """Set the specified digital output lines."""

    address: ClassVar[int] = 34
    payload_type: ClassVar[PayloadType] = PayloadType.U16
    payload_class = OutputSetPayload


class OutputClear(RegisterBase[DigitalOutputs]):
    """Clear the specified digital output lines"""

    address: ClassVar[int] = 35
    payload_type: ClassVar[PayloadType] = PayloadType.U16
    payload_class = OutputClearPayload


class OutputToggle(RegisterBase[DigitalOutputs]):
    """Toggle the specified digital output lines"""

    address: ClassVar[int] = 36
    payload_type: ClassVar[PayloadType] = PayloadType.U16
    payload_class = OutputTogglePayload


class OutputState(RegisterBase[DigitalOutputs]):
    """Write the state of all digital output lines"""

    address: ClassVar[int] = 37
    payload_type: ClassVar[PayloadType] = PayloadType.U16
    payload_class = OutputStatePayload


class PortDIOSet(RegisterBase[PortDIOLines]):
    """Set the specified port DIO lines. This functionality has not yet been implemented."""

    address: ClassVar[int] = 38
    payload_type: ClassVar[PayloadType] = PayloadType.U8
    payload_class = PortDIOSetPayload


class PortDIOClear(RegisterBase[PortDIOLines]):
    """Clear the specified port DIO lines. This functionality has not yet been implemented."""

    address: ClassVar[int] = 39
    payload_type: ClassVar[PayloadType] = PayloadType.U8
    payload_class = PortDIOClearPayload


class PortDIOToggle(RegisterBase[PortDIOLines]):
    """Toggle the specified port DIO lines. This functionality has not yet been implemented."""

    address: ClassVar[int] = 40
    payload_type: ClassVar[PayloadType] = PayloadType.U8
    payload_class = PortDIOTogglePayload


class PortDIOState(RegisterBase[PortDIOLines]):
    """Write the state of all port DIO lines. This functionality has not yet been implemented."""

    address: ClassVar[int] = 41
    payload_type: ClassVar[PayloadType] = PayloadType.U8
    payload_class = PortDIOStatePayload


class PortDIODirection(RegisterBase[PortDIOLines]):
    """Specifies which of the port DIO lines are outputs. This functionality has not yet been implemented."""

    address: ClassVar[int] = 42
    payload_type: ClassVar[PayloadType] = PayloadType.U8
    payload_class = PortDIODirectionPayload


class PortDIOStateEvent(RegisterBase[PortDIOLines]):
    """Specifies the state of the port DIO lines on a line change"""

    address: ClassVar[int] = 43
    payload_type: ClassVar[PayloadType] = PayloadType.U8
    payload_class = PortDIOStateEventPayload


class AnalogData(RegisterBase[AnalogDataPayload]):
    """Voltage at the ADC input and encoder value on Port 2"""

    address: ClassVar[int] = 44
    payload_type: ClassVar[PayloadType] = PayloadType.S16
    payload_class = AnalogDataPayload


class OutputPulseEnable(RegisterBase[DigitalOutputs]):
    """Enables the pulse function for the specified output lines"""

    address: ClassVar[int] = 45
    payload_type: ClassVar[PayloadType] = PayloadType.U16
    payload_class = OutputPulseEnablePayload


class PulseDOPort0(RegisterBase[np.uint16]):
    """Specifies the duration of the output pulse in milliseconds."""

    address: ClassVar[int] = 46
    payload_type: ClassVar[PayloadType] = PayloadType.U16
    payload_class = PulseDOPort0Payload


class PulseDOPort1(RegisterBase[np.uint16]):
    """Specifies the duration of the output pulse in milliseconds."""

    address: ClassVar[int] = 47
    payload_type: ClassVar[PayloadType] = PayloadType.U16
    payload_class = PulseDOPort1Payload


class PulseDOPort2(RegisterBase[np.uint16]):
    """Specifies the duration of the output pulse in milliseconds."""

    address: ClassVar[int] = 48
    payload_type: ClassVar[PayloadType] = PayloadType.U16
    payload_class = PulseDOPort2Payload


class PulseSupplyPort0(RegisterBase[np.uint16]):
    """Specifies the duration of the output pulse in milliseconds."""

    address: ClassVar[int] = 49
    payload_type: ClassVar[PayloadType] = PayloadType.U16
    payload_class = PulseSupplyPort0Payload


class PulseSupplyPort1(RegisterBase[np.uint16]):
    """Specifies the duration of the output pulse in milliseconds."""

    address: ClassVar[int] = 50
    payload_type: ClassVar[PayloadType] = PayloadType.U16
    payload_class = PulseSupplyPort1Payload


class PulseSupplyPort2(RegisterBase[np.uint16]):
    """Specifies the duration of the output pulse in milliseconds."""

    address: ClassVar[int] = 51
    payload_type: ClassVar[PayloadType] = PayloadType.U16
    payload_class = PulseSupplyPort2Payload


class PulseLed0(RegisterBase[np.uint16]):
    """Specifies the duration of the output pulse in milliseconds."""

    address: ClassVar[int] = 52
    payload_type: ClassVar[PayloadType] = PayloadType.U16
    payload_class = PulseLed0Payload


class PulseLed1(RegisterBase[np.uint16]):
    """Specifies the duration of the output pulse in milliseconds."""

    address: ClassVar[int] = 53
    payload_type: ClassVar[PayloadType] = PayloadType.U16
    payload_class = PulseLed1Payload


class PulseRgb0(RegisterBase[np.uint16]):
    """Specifies the duration of the output pulse in milliseconds."""

    address: ClassVar[int] = 54
    payload_type: ClassVar[PayloadType] = PayloadType.U16
    payload_class = PulseRgb0Payload


class PulseRgb1(RegisterBase[np.uint16]):
    """Specifies the duration of the output pulse in milliseconds."""

    address: ClassVar[int] = 55
    payload_type: ClassVar[PayloadType] = PayloadType.U16
    payload_class = PulseRgb1Payload


class PulseDO0(RegisterBase[np.uint16]):
    """Specifies the duration of the output pulse in milliseconds."""

    address: ClassVar[int] = 56
    payload_type: ClassVar[PayloadType] = PayloadType.U16
    payload_class = PulseDO0Payload


class PulseDO1(RegisterBase[np.uint16]):
    """Specifies the duration of the output pulse in milliseconds."""

    address: ClassVar[int] = 57
    payload_type: ClassVar[PayloadType] = PayloadType.U16
    payload_class = PulseDO1Payload


class PulseDO2(RegisterBase[np.uint16]):
    """Specifies the duration of the output pulse in milliseconds."""

    address: ClassVar[int] = 58
    payload_type: ClassVar[PayloadType] = PayloadType.U16
    payload_class = PulseDO2Payload


class PulseDO3(RegisterBase[np.uint16]):
    """Specifies the duration of the output pulse in milliseconds."""

    address: ClassVar[int] = 59
    payload_type: ClassVar[PayloadType] = PayloadType.U16
    payload_class = PulseDO3Payload


class PwmFrequencyDO0(RegisterBase[np.uint16]):
    """Specifies the frequency of the PWM at DO0."""

    address: ClassVar[int] = 60
    payload_type: ClassVar[PayloadType] = PayloadType.U16
    payload_class = PwmFrequencyDO0Payload


class PwmFrequencyDO1(RegisterBase[np.uint16]):
    """Specifies the frequency of the PWM at DO1."""

    address: ClassVar[int] = 61
    payload_type: ClassVar[PayloadType] = PayloadType.U16
    payload_class = PwmFrequencyDO1Payload


class PwmFrequencyDO2(RegisterBase[np.uint16]):
    """Specifies the frequency of the PWM at DO2."""

    address: ClassVar[int] = 62
    payload_type: ClassVar[PayloadType] = PayloadType.U16
    payload_class = PwmFrequencyDO2Payload


class PwmFrequencyDO3(RegisterBase[np.uint16]):
    """Specifies the frequency of the PWM at DO3."""

    address: ClassVar[int] = 63
    payload_type: ClassVar[PayloadType] = PayloadType.U16
    payload_class = PwmFrequencyDO3Payload


class PwmDutyCycleDO0(RegisterBase[np.uint8]):
    """Specifies the duty cycle of the PWM at DO0."""

    address: ClassVar[int] = 64
    payload_type: ClassVar[PayloadType] = PayloadType.U8
    payload_class = PwmDutyCycleDO0Payload


class PwmDutyCycleDO1(RegisterBase[np.uint8]):
    """Specifies the duty cycle of the PWM at DO1."""

    address: ClassVar[int] = 65
    payload_type: ClassVar[PayloadType] = PayloadType.U8
    payload_class = PwmDutyCycleDO1Payload


class PwmDutyCycleDO2(RegisterBase[np.uint8]):
    """Specifies the duty cycle of the PWM at DO2."""

    address: ClassVar[int] = 66
    payload_type: ClassVar[PayloadType] = PayloadType.U8
    payload_class = PwmDutyCycleDO2Payload


class PwmDutyCycleDO3(RegisterBase[np.uint8]):
    """Specifies the duty cycle of the PWM at DO3."""

    address: ClassVar[int] = 67
    payload_type: ClassVar[PayloadType] = PayloadType.U8
    payload_class = PwmDutyCycleDO3Payload


class PwmStart(RegisterBase[PwmOutputs]):
    """Starts the PWM on the selected output lines."""

    address: ClassVar[int] = 68
    payload_type: ClassVar[PayloadType] = PayloadType.U8
    payload_class = PwmStartPayload


class PwmStop(RegisterBase[PwmOutputs]):
    """Stops the PWM on the selected output lines."""

    address: ClassVar[int] = 69
    payload_type: ClassVar[PayloadType] = PayloadType.U8
    payload_class = PwmStopPayload


class RgbAll(RegisterBase[RgbAllPayload]):
    """Specifies the state of all RGB LED channels."""

    address: ClassVar[int] = 70
    payload_type: ClassVar[PayloadType] = PayloadType.U8
    payload_class = RgbAllPayload


class Rgb0(RegisterBase[RgbPayload]):
    """Specifies the state of the RGB0 LED channels."""

    address: ClassVar[int] = 71
    payload_type: ClassVar[PayloadType] = PayloadType.U8
    payload_class = RgbPayload


class Rgb1(RegisterBase[RgbPayload]):
    """Specifies the state of the RGB1 LED channels."""

    address: ClassVar[int] = 72
    payload_type: ClassVar[PayloadType] = PayloadType.U8
    payload_class = RgbPayload


class Led0Current(RegisterBase[np.uint8]):
    """Specifies the configuration of current to drive LED 0."""

    address: ClassVar[int] = 73
    payload_type: ClassVar[PayloadType] = PayloadType.U8
    payload_class = Led0CurrentPayload


class Led1Current(RegisterBase[np.uint8]):
    """Specifies the configuration of current to drive LED 1."""

    address: ClassVar[int] = 74
    payload_type: ClassVar[PayloadType] = PayloadType.U8
    payload_class = Led1CurrentPayload


class Led0MaxCurrent(RegisterBase[np.uint8]):
    """Specifies the maximum current allowed to drive LED 0, in mA."""

    address: ClassVar[int] = 75
    payload_type: ClassVar[PayloadType] = PayloadType.U8
    payload_class = Led0MaxCurrentPayload


class Led1MaxCurrent(RegisterBase[np.uint8]):
    """Specifies the maximum current allowed to drive LED 1, in mA."""

    address: ClassVar[int] = 76
    payload_type: ClassVar[PayloadType] = PayloadType.U8
    payload_class = Led1MaxCurrentPayload


class EventEnable(RegisterBase[Events]):
    """Specifies the active events in the device."""

    address: ClassVar[int] = 77
    payload_type: ClassVar[PayloadType] = PayloadType.U8
    payload_class = EventEnablePayload


class StartCameras(RegisterBase[CameraOutputs]):
    """Specifies the camera outputs to enable in the device."""

    address: ClassVar[int] = 78
    payload_type: ClassVar[PayloadType] = PayloadType.U8
    payload_class = StartCamerasPayload


class StopCameras(RegisterBase[CameraOutputs]):
    """Specifies the camera outputs to disable in the device. An event will be issued when the trigger signal is actually stopped being generated."""

    address: ClassVar[int] = 79
    payload_type: ClassVar[PayloadType] = PayloadType.U8
    payload_class = StopCamerasPayload


class EnableServos(RegisterBase[ServoOutputs]):
    """Specifies the servo outputs to enable in the device."""

    address: ClassVar[int] = 80
    payload_type: ClassVar[PayloadType] = PayloadType.U8
    payload_class = EnableServosPayload


class DisableServos(RegisterBase[ServoOutputs]):
    """Specifies the servo outputs to disable in the device."""

    address: ClassVar[int] = 81
    payload_type: ClassVar[PayloadType] = PayloadType.U8
    payload_class = DisableServosPayload


class EnableEncoders(RegisterBase[EncoderInputs]):
    """Specifies the port quadrature counters to enable in the device."""

    address: ClassVar[int] = 82
    payload_type: ClassVar[PayloadType] = PayloadType.U8
    payload_class = EnableEncodersPayload


class EncoderMode(RegisterBase[EncoderModeMask]):
    """Configures the operation mode of the quadrature encoders."""

    address: ClassVar[int] = 83
    payload_type: ClassVar[PayloadType] = PayloadType.U8
    payload_class = EncoderModePayload


class _Reserved2(RegisterU8):
    """Reserved for future use"""

    address: ClassVar[int] = 84


class _Reserved3(RegisterU8):
    """Reserved for future use"""

    address: ClassVar[int] = 85


class _Reserved4(RegisterU8):
    """Reserved for future use"""

    address: ClassVar[int] = 86


class _Reserved5(RegisterU8):
    """Reserved for future use"""

    address: ClassVar[int] = 87


class _Reserved6(RegisterU8):
    """Reserved for future use"""

    address: ClassVar[int] = 88


class _Reserved7(RegisterU8):
    """Reserved for future use"""

    address: ClassVar[int] = 89


class _Reserved8(RegisterU8):
    """Reserved for future use"""

    address: ClassVar[int] = 90


class _Reserved9(RegisterU8):
    """Reserved for future use"""

    address: ClassVar[int] = 91


class Camera0Frame(RegisterBase[FrameAcquired]):
    """Specifies that a frame was acquired on camera 0."""

    address: ClassVar[int] = 92
    payload_type: ClassVar[PayloadType] = PayloadType.U8
    payload_class = Camera0FramePayload


class Camera0Frequency(RegisterBase[np.uint16]):
    """Specifies the trigger frequency for camera 0."""

    address: ClassVar[int] = 93
    payload_type: ClassVar[PayloadType] = PayloadType.U16
    payload_class = Camera0FrequencyPayload


class Camera1Frame(RegisterBase[FrameAcquired]):
    """Specifies that a frame was acquired on camera 1."""

    address: ClassVar[int] = 94
    payload_type: ClassVar[PayloadType] = PayloadType.U8
    payload_class = Camera1FramePayload


class Camera1Frequency(RegisterBase[np.uint16]):
    """Specifies the trigger frequency for camera 1."""

    address: ClassVar[int] = 95
    payload_type: ClassVar[PayloadType] = PayloadType.U16
    payload_class = Camera1FrequencyPayload


class _Reserved10(RegisterU8):
    """Reserved for future use"""

    address: ClassVar[int] = 96


class _Reserved11(RegisterU8):
    """Reserved for future use"""

    address: ClassVar[int] = 97


class _Reserved12(RegisterU8):
    """Reserved for future use"""

    address: ClassVar[int] = 98


class _Reserved13(RegisterU8):
    """Reserved for future use"""

    address: ClassVar[int] = 99


class ServoMotor2Period(RegisterBase[np.uint16]):
    """Specifies the period of the servo motor in DO2, in microseconds."""

    address: ClassVar[int] = 100
    payload_type: ClassVar[PayloadType] = PayloadType.U16
    payload_class = ServoMotor2PeriodPayload


class ServoMotor2Pulse(RegisterBase[np.uint16]):
    """Specifies the pulse of the servo motor in DO2, in microseconds."""

    address: ClassVar[int] = 101
    payload_type: ClassVar[PayloadType] = PayloadType.U16
    payload_class = ServoMotor2PulsePayload


class ServoMotor3Period(RegisterBase[np.uint16]):
    """Specifies the period of the servo motor in DO3, in microseconds."""

    address: ClassVar[int] = 102
    payload_type: ClassVar[PayloadType] = PayloadType.U16
    payload_class = ServoMotor3PeriodPayload


class ServoMotor3Pulse(RegisterBase[np.uint16]):
    """Specifies the pulse of the servo motor in DO3, in microseconds."""

    address: ClassVar[int] = 103
    payload_type: ClassVar[PayloadType] = PayloadType.U16
    payload_class = ServoMotor3PulsePayload


class _Reserved14(RegisterU8):
    """Reserved for future use"""

    address: ClassVar[int] = 104


class _Reserved15(RegisterU8):
    """Reserved for future use"""

    address: ClassVar[int] = 105


class _Reserved16(RegisterU8):
    """Reserved for future use"""

    address: ClassVar[int] = 106


class _Reserved17(RegisterU8):
    """Reserved for future use"""

    address: ClassVar[int] = 107


class EncoderReset(RegisterBase[EncoderInputs]):
    """Reset the counter of the specified encoders to zero."""

    address: ClassVar[int] = 108
    payload_type: ClassVar[PayloadType] = PayloadType.U8
    payload_class = EncoderResetPayload


class _Reserved18(RegisterU8):
    """Reserved for future use"""

    address: ClassVar[int] = 109


class EnableSerialTimestamp(RegisterBase[SerialTimestampPorts]):
    """Enables the timestamp for serial TX."""

    address: ClassVar[int] = 110
    payload_type: ClassVar[PayloadType] = PayloadType.U8
    payload_class = EnableSerialTimestampPayload


class MimicPort0IR(RegisterBase[MimicOutput]):
    """Specifies the digital output to mimic the Port 0 IR state."""

    address: ClassVar[int] = 111
    payload_type: ClassVar[PayloadType] = PayloadType.U8
    payload_class = MimicPort0IRPayload


class MimicPort1IR(RegisterBase[MimicOutput]):
    """Specifies the digital output to mimic the Port 1 IR state."""

    address: ClassVar[int] = 112
    payload_type: ClassVar[PayloadType] = PayloadType.U8
    payload_class = MimicPort1IRPayload


class MimicPort2IR(RegisterBase[MimicOutput]):
    """Specifies the digital output to mimic the Port 2 IR state."""

    address: ClassVar[int] = 113
    payload_type: ClassVar[PayloadType] = PayloadType.U8
    payload_class = MimicPort2IRPayload


class _Reserved20(RegisterU8):
    """Reserved for future use"""

    address: ClassVar[int] = 114


class _Reserved21(RegisterU8):
    """Reserved for future use"""

    address: ClassVar[int] = 115


class _Reserved22(RegisterU8):
    """Reserved for future use"""

    address: ClassVar[int] = 116


class MimicPort0Valve(RegisterBase[MimicOutput]):
    """Specifies the digital output to mimic the Port 0 valve state."""

    address: ClassVar[int] = 117
    payload_type: ClassVar[PayloadType] = PayloadType.U8
    payload_class = MimicPort0ValvePayload


class MimicPort1Valve(RegisterBase[MimicOutput]):
    """Specifies the digital output to mimic the Port 1 valve state."""

    address: ClassVar[int] = 118
    payload_type: ClassVar[PayloadType] = PayloadType.U8
    payload_class = MimicPort1ValvePayload


class MimicPort2Valve(RegisterBase[MimicOutput]):
    """Specifies the digital output to mimic the Port 2 valve state."""

    address: ClassVar[int] = 119
    payload_type: ClassVar[PayloadType] = PayloadType.U8
    payload_class = MimicPort2ValvePayload


class _Reserved23(RegisterU8):
    """Reserved for future use"""

    address: ClassVar[int] = 120


class _Reserved24(RegisterU8):
    """Reserved for future use"""

    address: ClassVar[int] = 121


class PokeInputFilter(RegisterBase[np.uint8]):
    """Specifies the low pass filter time value for poke inputs, in ms."""

    address: ClassVar[int] = 122
    payload_type: ClassVar[PayloadType] = PayloadType.U8
    payload_class = PokeInputFilterPayload


REGISTER_MAP: dict[int, type[RegisterBase[Any]]] = {
    **_CORE_REGISTER_MAP,
    32: DigitalInputState,
    33: _Reserved0,
    34: OutputSet,
    35: OutputClear,
    36: OutputToggle,
    37: OutputState,
    38: PortDIOSet,
    39: PortDIOClear,
    40: PortDIOToggle,
    41: PortDIOState,
    42: PortDIODirection,
    43: PortDIOStateEvent,
    44: AnalogData,
    45: OutputPulseEnable,
    46: PulseDOPort0,
    47: PulseDOPort1,
    48: PulseDOPort2,
    49: PulseSupplyPort0,
    50: PulseSupplyPort1,
    51: PulseSupplyPort2,
    52: PulseLed0,
    53: PulseLed1,
    54: PulseRgb0,
    55: PulseRgb1,
    56: PulseDO0,
    57: PulseDO1,
    58: PulseDO2,
    59: PulseDO3,
    60: PwmFrequencyDO0,
    61: PwmFrequencyDO1,
    62: PwmFrequencyDO2,
    63: PwmFrequencyDO3,
    64: PwmDutyCycleDO0,
    65: PwmDutyCycleDO1,
    66: PwmDutyCycleDO2,
    67: PwmDutyCycleDO3,
    68: PwmStart,
    69: PwmStop,
    70: RgbAll,
    71: Rgb0,
    72: Rgb1,
    73: Led0Current,
    74: Led1Current,
    75: Led0MaxCurrent,
    76: Led1MaxCurrent,
    77: EventEnable,
    78: StartCameras,
    79: StopCameras,
    80: EnableServos,
    81: DisableServos,
    82: EnableEncoders,
    83: EncoderMode,
    84: _Reserved2,
    85: _Reserved3,
    86: _Reserved4,
    87: _Reserved5,
    88: _Reserved6,
    89: _Reserved7,
    90: _Reserved8,
    91: _Reserved9,
    92: Camera0Frame,
    93: Camera0Frequency,
    94: Camera1Frame,
    95: Camera1Frequency,
    96: _Reserved10,
    97: _Reserved11,
    98: _Reserved12,
    99: _Reserved13,
    100: ServoMotor2Period,
    101: ServoMotor2Pulse,
    102: ServoMotor3Period,
    103: ServoMotor3Pulse,
    104: _Reserved14,
    105: _Reserved15,
    106: _Reserved16,
    107: _Reserved17,
    108: EncoderReset,
    109: _Reserved18,
    110: EnableSerialTimestamp,
    111: MimicPort0IR,
    112: MimicPort1IR,
    113: MimicPort2IR,
    114: _Reserved20,
    115: _Reserved21,
    116: _Reserved22,
    117: MimicPort0Valve,
    118: MimicPort1Valve,
    119: MimicPort2Valve,
    120: _Reserved23,
    121: _Reserved24,
    122: PokeInputFilter,
}
