using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(string.Join(',', Solution.solution(new int[] { 1, 5, 2, 6, 3, 7, 4 }, new int[,] { { 2, 5, 3 }, { 4, 4, 1 }, { 1, 7, 3 } })));
            //Console.WriteLine(string.Join(',', Solution.solution2("2022.05.19", new string[] { "A 6", "B 12", "C 3" }, new string[] { "2021.05.02 A", "2021.07.01 B", "2022.02.19 C", "2022.02.20 C" })));
            //Console.WriteLine(Solution.solution3(4, 5, new int[] { 1, 0, 3, 1, 2 }, new int[] { 0, 3, 0, 4, 0 }));
            //Console.WriteLine(string.Join(',', Solution.solution4(new int[] { 1, 0, 3, 1, 2 }, 1, 4)));
            //Console.WriteLine(string.Join(',', Solution.solution5(new int[,] { { 40, 2900 }, { 23, 10000 }, { 11, 5200 }, { 5, 5900 }, { 40, 3100 }, { 27, 9200 }, { 32, 6900 } }, new int[] { 1300, 1500, 1600, 4900 })));
            Console.WriteLine(string.Join(',', Solution.solution6(new long[] { 63, 111, 95 })));
        }
    }
    public static class Solution
    {
        public static int[] solution(int[] array, int[,] commands)
        {
            int[] answer = new int[commands.GetLength(0)];
            for (int i = 0; i < commands.GetLength(0); i++)
            {
                int firstIdx = commands[i, 0] - 1;
                int count = commands[i, 1] - firstIdx;
                int index = commands[i, 2] - 1;

                int[] newArray = new int[count];
                Array.Copy(array, firstIdx, newArray, 0, count);

                // 빠른 순서
                Array.Sort(newArray);
                answer[i] = newArray[index];
                //answer[i] = newArray.OrderBy(x => x).ToArray()[index];
                //answer[i] = array.Where((value, idx) => idx >= firstIdx && idx < firstIdx + count).OrderBy(x => x).ToArray()[index];
            }
            return answer;
        }
        public static int[] solution2(string today, string[] terms, string[] privacies)
        {
            List<int> answer = new List<int>();

            string[] date = today.Split('.');
            int year = int.Parse(date[0]);
            int month = int.Parse(date[1]);
            int day = int.Parse(date[2]);
            Console.WriteLine($"{year}, {month}, {day}");
            Dictionary<string, int> termList = new Dictionary<string, int>();
            foreach (string term in terms)
            {
                string[] temp = term.Split(' ');
                termList.Add(temp[0], int.Parse(temp[1]));
            }

            for (int i = 0; i < privacies.Length; i++)
            {
                string[] temp = privacies[i].Split(' ');

                string type = temp[1];
                Console.WriteLine(type);
                string[] startDate = temp[0].Split('.');
                int startYear = int.Parse(startDate[0]);
                int startMonth = int.Parse(startDate[1]);
                int startDay = int.Parse(startDate[2]);

                int endYear;
                int endMonth;
                int endDay;

                int addDay = termList[type] * 28 - 1;

                endDay = (startDay + addDay) % 28;
                endDay = endDay == 0 ? 28 : endDay;

                int addMonth = (startDay + addDay - 1) / 28;
                endMonth = (startMonth + addMonth) % 12;
                endMonth = endMonth == 0 ? 12 : endMonth;

                int addYear = (startMonth + addMonth - 1) / 12;
                endYear = startYear + addYear;


                //Console.WriteLine($"{startYear}, {startMonth}, {startDay}");
                //Console.WriteLine($"{endYear}, {endMonth}, {endDay}");

                if (endYear > year)
                {
                    continue;
                }
                else if (endYear < year)
                {
                    answer.Add(i + 1);
                    continue;
                }

                if (endMonth > month)
                {
                    continue;
                }
                else if (endMonth < month)
                {
                    answer.Add(i + 1);
                    continue;
                }

                if (endDay >= day)
                {
                    continue;
                }
                else
                {
                    answer.Add(i + 1);
                    continue;
                }
            }

            return answer.ToArray();
        }

        public static long solution3(int cap, int n, int[] deliveries, int[] pickups)
        {
            long answer = 0;

            int deliverIndex = n;
            int pickupIndex = n;
            while (true)
            {
                #region
                /*int deliverResult = -1;
                int pickupResult = -1;
                int deliverCap = cap;
                int pickupCap = cap;
                for (int i = deliverIndex; i >= 0; i--)
                {
                    if (deliveries[i] > 0)
                    {
                        deliverResult = Math.Max(i + 1, deliverResult);
                        deliverIndex = i;
                        if (deliverCap < deliveries[i])
                        {
                            deliveries[i] = deliveries[i] - deliverCap;
                            deliverCap = 0;
                            break;
                        }
                        else
                        {
                            deliverCap = deliverCap - deliveries[i];
                            deliveries[i] = 0;
                        }
                    }
                }

                for (int i = deliverIndex; i >= 0; i--)
                {
                    if (pickups[i] > 0)
                    {
                        pickupResult = Math.Max(i + 1, pickupResult);
                        if (pickupCap < pickups[i])
                        {
                            pickups[i] = pickups[i] - pickupCap;
                            pickupCap = 0;
                            break;
                        }
                        else
                        {
                            pickupCap = pickupCap - pickups[i];
                            pickups[i] = 0;
                        }
                    }
                }*/
                #endregion
                deliverIndex = deliverLength(deliveries, deliverIndex - 1, cap);
                pickupIndex = deliverLength(pickups, pickupIndex - 1, cap);
                int result = Math.Max(deliverIndex, pickupIndex);
                if (result <= 0)
                    break;
                Console.WriteLine(result);
                answer = answer + result * 2;
            }
            return answer;
        }
        public static int deliverLength(int[] count, int index, int cap)
        {
            if (index < 0) return -1;
            int result = -1;
            int remainCap = cap;
            for (int i = index; i >= 0; i--)
            {
                if (count[i] > 0)
                {
                    result = Math.Max(i + 1, result);
                    if (remainCap < count[i])
                    {
                        count[i] = count[i] - remainCap;
                        remainCap = 0;
                        break;
                    }
                    else
                    {
                        remainCap = remainCap - count[i];
                        count[i] = 0;
                    }
                }
            }
            return result;
        }

        public static int[] solution4(int[] numbers, int num1, int num2)
        {
            int length = num2 - num1 + 1;
            int[] answer = new int[length];
            Array.Copy(numbers, num1, answer, 0, length);
            return answer;
        }

        public static int[] solution5(int[,] users, int[] emoticons)
        {
            int[] saleRate = new int[emoticons.Length];

            Values result = DFS(-1, saleRate, users, emoticons);

            int[] answer = new int[2] { result.subCount, result.revenue };

            return answer;
        }
        public static Values DFS(int count, int[] saleRate, int[,] users , int[] emoticons)
        {
            Values result = new Values(0, 0);
            int Length = count + 1;
            if (Length == emoticons.Length)
            {
                int subCount = 0;
                int revenue = 0;
                for (int i = 0; i < users.GetLength(0); i++)
                {
                    int total = 0;
                    for (int j = 0; j < emoticons.Length; j++)
                        if (saleRate[j] >= users[i, 0])
                            total += emoticons[j] * (100 - saleRate[j]) / 100;

                    if (total >= users[i, 1])
                        subCount++;
                    else
                        revenue += total;
                }
                return new Values(subCount, revenue);
            }
            for(int sale = 0; sale <= 40; sale += 10)
            {
                saleRate[Length] = sale;
                Values value = DFS(Length, saleRate, users, emoticons);
                Console.WriteLine(string.Join(',',saleRate));
                Console.WriteLine($"sub: {value.subCount}, rev: {value.revenue}");
                if (result.subCount < value.subCount)
                    result = value;
                else if (result.subCount == value.subCount)
                    if (result.revenue < value.revenue)
                        result = value;
            }
            return result;
        }
        public struct Values
        {
            public int subCount;
            public int revenue;
            public Values(int subCount, int revenue)
            {
                this.subCount = subCount;
                this.revenue = revenue;
            }
        }

        public static int[] solution6(long[] numbers)
        {
            int[] answer = new int[numbers.Length];
            for (int i = 0; i < numbers.Length; i++)
            {
                string binary = Convert.ToString(numbers[i], 2);
                int result = 1;
                for (int j = (binary.Length / 2) - 1; j >= 0; j -= 2)
                {
                    List<int> list = new List<int>();
                    if (binary[j] == '0')
                    {
                        result = 0;
                        break;
                    }
                }
                answer[i] = result;
            }
            return answer;
        }
        public static void CheckArray(string number)
        {
            if(!number.Length+1))
        }
    }
}
