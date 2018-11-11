using LinqMeta.DataTypes.Statistic;
using LinqMeta.Operators.Number;
using LinqMetaCore;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Extensions.Operators
{
    public static class StatisticOperator
    {
        public static StatisticInfo<T>? GetStatisticMeta<TCollect, T>(this TCollect collect, StatisticValue value)
            where TCollect : struct, ICollectionWrapper<T>
        {
            if (collect.HasIndexOverhead)
            {
                if (collect.HasNext)
                {
                    var item = collect.Value;
                    var count = 1u;
                    var sum = item;
                    var product = item;
                    var minus = item;

                    switch (value)
                    {
                        case StatisticValue.Sum:
                            while (collect.HasNext)
                            {
                                item = collect.Value;
                                checked
                                {
                                    sum = NumberOperators<T>.Sum(sum, item);
                                    count += 1;
                                }
                            }

                            return new StatisticInfo<T>
                            {
                                Sum = new Option<T>(sum),
                                Count = count,
                            };
                        case StatisticValue.Minus:
                            while (collect.HasNext)
                            {
                                item = collect.Value;
                                checked
                                {
                                    minus = NumberOperators<T>.Minus(minus, item);
                                    count += 1;
                                }
                            }

                            return new StatisticInfo<T>
                            {
                                Count = count,
                                Minus = new Option<T>(minus),
                            };
                        case StatisticValue.Product:
                            while (collect.HasNext)
                            {
                                item = collect.Value;
                                checked
                                {
                                    product = NumberOperators<T>.Product(product, item);
                                    count += 1;
                                }
                            }

                            return new StatisticInfo<T>
                            {
                                Count = count,
                                Product = new Option<T>(product),
                            };
                        case StatisticValue.All:
                            while (collect.HasNext)
                            {
                                item = collect.Value;
                                checked
                                {
                                    sum = NumberOperators<T>.Sum(sum, item);
                                    minus = NumberOperators<T>.Minus(minus, item);
                                    product = NumberOperators<T>.Product(product, item);
                                    count += 1;
                                }
                            }

                            return new StatisticInfo<T>
                            {
                                Sum = new Option<T>(sum),
                                Count = count,
                                Minus = new Option<T>(minus),
                                Product = new Option<T>(product),
                            };
                        case StatisticValue.SumMinus:
                            while (collect.HasNext)
                            {
                                item = collect.Value;
                                checked
                                {
                                    sum = NumberOperators<T>.Sum(sum, item);
                                    minus = NumberOperators<T>.Minus(minus, item);
                                    count += 1;
                                }
                            }

                            return new StatisticInfo<T>
                            {
                                Sum = new Option<T>(sum),
                                Count = count,
                                Minus = new Option<T>(minus),
                            };
                        case StatisticValue.SumProduct:
                            while (collect.HasNext)
                            {
                                item = collect.Value;
                                checked
                                {
                                    sum = NumberOperators<T>.Sum(sum, item);
                                    product = NumberOperators<T>.Product(product, item);
                                    count += 1;
                                }
                            }

                            return new StatisticInfo<T>
                            {
                                Sum = new Option<T>(sum),
                                Count = count,
                                Product = new Option<T>(product),
                            };
                        case StatisticValue.MinusProduct:
                            while (collect.HasNext)
                            {
                                item = collect.Value;
                                checked
                                {
                                    minus = NumberOperators<T>.Minus(minus, item);
                                    product = NumberOperators<T>.Product(product, item);
                                    count += 1;
                                }
                            }

                            return new StatisticInfo<T>
                            {
                                Count = count,
                                Minus = new Option<T>(minus),
                                Product = new Option<T>(product),
                            };
                    }
                }
            }
            else
            {
                var size = collect.Size;
                if (size > 0)
                {
                    var item = collect[0];
                    var count = 1u;
                    var sum = item;
                    var minus = item;
                    var product = item;

                    switch (value)
                    {
                        case StatisticValue.Sum:
                            while (count < size)
                            {
                                item = collect[count];
                                checked
                                {
                                    sum = NumberOperators<T>.Sum(sum, item);
                                    count += 1;
                                }
                            }

                            return new StatisticInfo<T>
                            {
                                Sum = new Option<T>(sum),
                                Count = count,
                            };
                        case StatisticValue.Minus:
                            while (count < size)
                            {
                                item = collect[count];
                                checked
                                {
                                    minus = NumberOperators<T>.Minus(minus, item);
                                    count += 1;
                                }
                            }

                            return new StatisticInfo<T>
                            {
                                Count = count,
                                Minus = new Option<T>(minus),
                            };
                        case StatisticValue.Product:
                            while (count < size)
                            {
                                item = collect[count];
                                checked
                                {
                                    product = NumberOperators<T>.Product(product, item);
                                    count += 1;
                                }
                            }

                            return new StatisticInfo<T>
                            {
                                Count = count,
                                Product = new Option<T>(product),
                            };
                        case StatisticValue.All:
                            while (count < size)
                            {
                                item = collect[count];
                                checked
                                {
                                    sum = NumberOperators<T>.Sum(sum, item);
                                    minus = NumberOperators<T>.Minus(minus, item);
                                    product = NumberOperators<T>.Product(product, item);
                                    count += 1;
                                }
                            }

                            return new StatisticInfo<T>
                            {
                                Sum = new Option<T>(sum),
                                Count = count,
                                Minus = new Option<T>(minus),
                                Product = new Option<T>(product),
                            };
                        case StatisticValue.SumMinus:
                            while (count < size)
                            {
                                item = collect[count];
                                checked
                                {
                                    sum = NumberOperators<T>.Sum(sum, item);
                                    minus = NumberOperators<T>.Minus(minus, item);
                                    count += 1;
                                }
                            }

                            return new StatisticInfo<T>
                            {
                                Sum = new Option<T>(sum),
                                Count = count,
                                Minus = new Option<T>(minus),
                            };
                        case StatisticValue.SumProduct:
                            while (count < size)
                            {
                                item = collect[count];
                                checked
                                {
                                    sum = NumberOperators<T>.Sum(sum, item);
                                    product = NumberOperators<T>.Product(product, item);
                                    count += 1;
                                }
                            }

                            return new StatisticInfo<T>
                            {
                                Sum = new Option<T>(sum),
                                Count = count,
                                Product = new Option<T>(product),
                            };
                        case StatisticValue.MinusProduct:
                            while (count < size)
                            {
                                item = collect[count];
                                checked
                                {
                                    minus = NumberOperators<T>.Minus(minus, item);
                                    product = NumberOperators<T>.Product(product, item);
                                    count += 1;
                                }
                            }

                            return new StatisticInfo<T>
                            {
                                Count = count,
                                Minus = new Option<T>(minus),
                                Product = new Option<T>(product),
                            };
                    }
                }
            }
            
            return null;
        }
    }
}