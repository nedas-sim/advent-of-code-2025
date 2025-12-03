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

- No decrease in performance with an introduced abstraction.
- Performance with a mutable object:

| Method          | Mean     | Error   | StdDev   | Gen0    | Gen1   | Allocated |
|---------------- |---------:|--------:|---------:|--------:|-------:|----------:|
| FirstTaskAsync  | 381.6 μs | 7.42 μs | 10.88 μs | 39.0625 | 0.9766 | 643.99 KB |
| SecondTaskAsync | 309.3 μs | 6.16 μs |  8.01 μs | 39.0625 | 0.9766 |    644 KB |

- No memory efficiency increase.

# Day 03

## Initial

| Method         | DigitsLength | Mean       | Error    | StdDev   | Gen0     | Gen1    | Allocated  |
|--------------- |------------- |-----------:|---------:|---------:|---------:|--------:|-----------:|
| FirstTaskAsync | 2            |   208.0 μs |  3.28 μs |  3.06 μs |  20.5078 |  0.4883 |  337.26 KB |
| FirstTaskAsync | 3            |   240.1 μs |  4.44 μs |  5.77 μs |  29.2969 |  0.9766 |  476.39 KB |
| FirstTaskAsync | 4            |   298.9 μs |  5.59 μs |  5.74 μs |  39.0625 |       - |  642.13 KB |
| FirstTaskAsync | 5            |   379.2 μs |  7.16 μs |  6.70 μs |  50.7813 |  1.9531 |  851.28 KB |
| FirstTaskAsync | 6            |   478.9 μs |  5.99 μs |  5.60 μs |  68.3594 |  1.9531 | 1114.67 KB |
| FirstTaskAsync | 7            |   621.1 μs |  7.38 μs |  6.90 μs |  89.8438 |  3.9063 |  1508.5 KB |
| FirstTaskAsync | 8            |   877.7 μs |  9.11 μs |  8.52 μs | 128.9063 |  3.9063 | 2163.16 KB |
| FirstTaskAsync | 9            | 1,304.8 μs |  8.70 μs |  7.71 μs | 195.3125 |  7.8125 | 3259.95 KB |
| FirstTaskAsync | 10           | 2,056.7 μs | 35.24 μs | 29.43 μs | 312.5000 |       - | 5130.55 KB |
| FirstTaskAsync | 11           | 3,351.8 μs | 44.44 μs | 39.40 μs | 468.7500 |       - |  7867.7 KB |
| FirstTaskAsync | 12           | 4,812.3 μs | 85.01 μs | 70.98 μs | 742.1875 | 31.2500 | 12144.4 KB |