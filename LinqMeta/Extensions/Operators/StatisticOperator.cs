using LinqMeta.CollectionWrapper;
using LinqMeta.Core;
using LinqMeta.Core.Statistic;
using LinqMeta.Operators.Numbers;

namespace LinqMeta.Extensions.Operators
{
    public static class StatisticOperator
    {
        public static StatisticInfo<T>? GetStatisticMeta<TCollect, T>(this TCollect collect, StatisticFlags statisticValue)
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
                    
                    if (statisticValue.Has(StatisticValue.Sum) &&
                        statisticValue.Has(StatisticValue.Minus) &&
                        statisticValue.Has(StatisticValue.Product))
                    {
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
                    }
                    else if (statisticValue.Has(StatisticValue.Sum) &&
                             statisticValue.Has(StatisticValue.Minus))
                    {
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
                    }
                    else if (statisticValue.Has(StatisticValue.Sum) &&
                             statisticValue.Has(StatisticValue.Product))
                    {
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
                    }
                    else if (statisticValue.Has(StatisticValue.Minus) &&
                             statisticValue.Has(StatisticValue.Product))
                    {
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
                    else if (statisticValue.Has(StatisticValue.Sum))
                    {
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
                    }
                    else if (statisticValue.Has(StatisticValue.Minus))
                    {
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
                    }
                    else if (statisticValue.Has(StatisticValue.Product))
                    {
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
                    
                    if (statisticValue.Has(StatisticValue.Sum) &&
                        statisticValue.Has(StatisticValue.Minus) &&
                        statisticValue.Has(StatisticValue.Product))
                    {
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
                    }
                    else if (statisticValue.Has(StatisticValue.Sum) &&
                             statisticValue.Has(StatisticValue.Minus))
                    {
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
                    }
                    else if (statisticValue.Has(StatisticValue.Sum) &&
                             statisticValue.Has(StatisticValue.Product))
                    {
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
                    }
                    else if (statisticValue.Has(StatisticValue.Minus) &&
                             statisticValue.Has(StatisticValue.Product))
                    {
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
                    else if (statisticValue.Has(StatisticValue.Sum))
                    {
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
                    }
                    else if (statisticValue.Has(StatisticValue.Minus))
                    {
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
                    }
                    else if (statisticValue.Has(StatisticValue.Product))
                    {
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
                    }
                }
            }
            
            return null;
        }
    }
}