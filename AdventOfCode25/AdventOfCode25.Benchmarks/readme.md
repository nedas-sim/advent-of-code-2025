# Day 01

## Initial

| Method          | Mean     | Error   | StdDev   | Median   | Gen0    | Gen1   | Allocated |
|---------------- |---------:|--------:|---------:|---------:|--------:|-------:|----------:|
| FirstTaskAsync  | 373.0 μs | 9.01 μs | 26.56 μs | 380.4 μs | 39.0625 | 0.9766 |    644 KB |
| SecondTaskAsync | 384.7 μs | 7.36 μs |  7.23 μs | 384.8 μs | 39.0625 | 0.9766 | 643.99 KB |

## Object reusability

- A new object instance is created with each line read from the file.
  One possible improvement is to reuse the same object instance.
  That will require a different set of logic so an interface will be added.
  A benchmark will be taken to see the difference an abstraction adds.

| Method          | Mean     | Error   | StdDev   | Gen0    | Gen1   | Allocated |
|---------------- |---------:|--------:|---------:|--------:|-------:|----------:|
| FirstTaskAsync  | 309.4 μs | 6.19 μs | 13.18 μs | 39.0625 | 0.9766 |    644 KB |
| SecondTaskAsync | 393.9 μs | 6.59 μs |  6.16 μs | 39.0625 | 0.9766 | 643.99 KB |

- No decrease in performance.