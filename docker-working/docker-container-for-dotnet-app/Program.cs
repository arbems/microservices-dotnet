var sum = 0;
var max = args.Length is not 0 ? Convert.ToInt32(args[0]) : -1;
while (max is -1 || sum < max)
{
    int num = new Random().Next(1, 10);

    Console.WriteLine($"Suma {sum} + {num} = {sum += num}");
    await Task.Delay(TimeSpan.FromMilliseconds(1_000));
}