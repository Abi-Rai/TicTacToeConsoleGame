
***
## First Results of Benchmarking the different datastructures to use as game board

* SingleBoard = Normal single dimensional array `char[]`
* JaggedBoard = Multi-Dimensional array `char[,]`
* MultiBoard = Jagged array `char[][]`

``` 
    |                 Method |      Mean |    Error |   StdDev | Ratio | RatioSD | Rank |   Gen0 | Allocated | Alloc Ratio |
    |----------------------- |----------:|---------:|---------:|------:|--------:|-----:|-------:|----------:|------------:|
    |       SingleBoard_Draw |  73.55 ns | 0.214 ns | 0.179 ns |  1.00 |    0.00 |    1 | 0.0191 |      80 B |        1.00 |
    |       JaggedBoard_Draw | 415.83 ns | 8.183 ns | 7.655 ns |  5.63 |    0.09 |    2 | 0.2809 |    1176 B |       14.70 |
    |        MultiBoard_Draw | 498.08 ns | 3.539 ns | 3.137 ns |  6.77 |    0.04 |    3 | 0.2613 |    1096 B |       13.70 |
    |                        |           |          |          |       |         |      |        |           |             |
    | SingleBoard_Player1Win |  45.62 ns | 0.313 ns | 0.277 ns |  1.00 |    0.00 |    1 | 0.0191 |      80 B |        1.00 |
    | JaggedBoard_Player1Win | 345.24 ns | 6.794 ns | 6.355 ns |  7.57 |    0.16 |    2 | 0.2809 |    1176 B |       14.70 |
    |  MultiBoard_Player1Win | 416.75 ns | 8.278 ns | 8.130 ns |  9.13 |    0.21 |    3 | 0.2618 |    1096 B |       13.70 |
```
:old_key: **Legend** 	

    Mean        : Arithmetic mean of all measurements
    Error       : Half of 99.9% confidence interval
    StdDev      : Standard deviation of all measurements
    Ratio       : Mean of the ratio distribution ([Current]/[Baseline])
    RatioSD     : Standard deviation of the ratio distribution ([Current]/[Baseline])
    Rank        : Relative position of current benchmark mean among all benchmarks (Arabic style)
    Gen0        : GC Generation 0 collects per 1000 operations
    Allocated   : Allocated memory per single operation (managed only, inclusive, 1KB = 1024B)
    Alloc Ratio : Allocated memory ratio distribution ([Current]/[Baseline])
    1 ns        : 1 Nanosecond (0.000000001 sec)
***
### Conclusion
Single dimensional array is by far the most performant option.

This could mainly be due to the how i'd implemented array access. For single board I had set it up to simply access the index by passing the player input e.g. `Board[parsedPlayerInput-1]` whereas,
for the Jagged and Multi boards I have added a `Dictionary<char,(int X,int Y)> _pointSelector` collection which stores the index positions relative to the characters in the array, so that they could be easily accessed from playinput. That would've added extra load to the boards. 
So, I ran another benchmark after adding the `Dictionary` to the single board aswell, just for benchmarking accuracy.

## Second results after adding `Dictionary` to single board.
    |                 Method |     Mean |   Error |  StdDev | Ratio | RatioSD | Rank |   Gen0 | Allocated | Alloc Ratio |
    |----------------------- |---------:|--------:|--------:|------:|--------:|-----:|-------:|----------:|------------:|
    |       SingleBoard_Draw | 358.0 ns | 5.92 ns | 6.58 ns |  1.00 |    0.00 |    1 | 0.2065 |     864 B |        1.00 |
    |       JaggedBoard_Draw | 409.6 ns | 1.17 ns | 1.04 ns |  1.14 |    0.02 |    2 | 0.2809 |    1176 B |        1.36 |
    |        MultiBoard_Draw | 494.6 ns | 1.17 ns | 0.98 ns |  1.37 |    0.03 |    3 | 0.2613 |    1096 B |        1.27 |
    |                        |          |         |         |       |         |      |        |           |             |
    | SingleBoard_Player1Win | 279.6 ns | 0.90 ns | 0.80 ns |  1.00 |    0.00 |    1 | 0.2065 |     864 B |        1.00 |
    | JaggedBoard_Player1Win | 338.3 ns | 0.71 ns | 0.60 ns |  1.21 |    0.00 |    2 | 0.2809 |    1176 B |        1.36 |
    |  MultiBoard_Player1Win | 408.4 ns | 0.85 ns | 0.75 ns |  1.46 |    0.00 |    3 | 0.2618 |    1096 B |        1.27 |


Now after adding the `Dictionary` the results show that the difference in performance between the three is less substantial. 