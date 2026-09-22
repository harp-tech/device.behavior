## About

`Harp.Behavior.App` provides a graphical interface for configuring and testing [Harp Behavior](https://harp-tech.org/device.behavior) devices.

As with other Harp devices, the Behavior device can also be used from [Bonsai](https://bonsai-rx.org/).

## How to Use

The app is distributed as a .NET tool. Installing it requires the [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0):

```text
dotnet tool install -g Harp.Behavior.App
```

Once installed, the app is launched from a terminal:

```text
harp.behavior
```

### Linux

The app accesses the serial port, so the user account needs to belong to the `dialout` group or equivalent. On Ubuntu and Fedora the command to add an account to that group is:

```text
sudo usermod -a -G dialout <USERNAME>
```

## Additional Documentation

For additional documentation and examples, refer to the [official Harp documentation](https://harp-tech.org/device.behavior).

## Feedback & Contributing

`Harp.Behavior.App` is released as open-source under the [MIT license](https://licenses.nuget.org/MIT). Bug reports and contributions are welcome at [the GitHub repository](https://github.com/harp-tech/device.behavior).
