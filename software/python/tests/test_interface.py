from harp.device import behavior, core


def test_device_identity_matches_the_metadata():
    assert behavior.DEVICE_NAME == "Behavior"
    assert behavior.WHO_AM_I == 1216


def test_register_map_covers_the_core_and_application_registers():
    # Application registers start at address 32, so the map is only complete if
    # the core registers were merged into it as well.
    assert behavior.REGISTER_MAP
    assert all(address in behavior.REGISTER_MAP for address in core.REGISTER_MAP)
    assert any(address >= 32 for address in behavior.REGISTER_MAP)
