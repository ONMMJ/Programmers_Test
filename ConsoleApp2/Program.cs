using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.Intrinsics;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

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
            //Console.WriteLine(string.Join(',', Solution.solution6(new long[] { 63, 111, 95 })));
            Console.WriteLine(string.Join(',', Solution.solution8(new string[] { "UPDATE 1 1 A", "UPDATE 2 2 B", "UPDATE 3 3 C", "UPDATE 4 4 D", "MERGE 1 1 2 2", "MERGE 1 1 2 2", "MERGE 3 3 4 4", "MERGE 1 1 4 4", "UNMERGE 3 3", "PRINT 1 1", "PRINT 2 2", "PRINT 3 3", "PRINT 4 4"})));
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
            List<int> answer = new List<int>();
            for (int i = 0; i < numbers.Length; i++)
            {
                string binary = Convert.ToString(numbers[i], 2);
                answer.Add(Check(binary));
            }
            return answer.ToArray();
        }
        public static int Check(string binary)
        {
            string number = binary;
            StringBuilder stringBuilder = new StringBuilder();
            while (number.Length > 1)
            {
                for (int j = number.Length - 1; j >= 0; j -= 4)
                {
                    int firstIndex = Math.Clamp(j - 2, 0, number.Length);
                    int count = j - firstIndex + 1;
                    string subStr = number.Substring(firstIndex, count);
                    int subNum = Convert.ToInt32(subStr, 2);
                    if (subNum == 0)
                        stringBuilder.Insert(0, "0");
                    else if (subNum == 2 || subNum == 3 || subNum == 6 || subNum == 7)
                        stringBuilder.Insert(0, "1");
                    else
                    {
                        return 0;
                    }

                    if(firstIndex > 0)
                    {
                        stringBuilder.Insert(0, number[firstIndex - 1]);
                    }
                }
                number = stringBuilder.ToString();
                stringBuilder.Clear();
            }
            return 1;
        }

        public static string[] solution7(string[] strings, int n)
        {
            List<string> stringList = strings.ToList();
            stringList.Sort((x, y) =>
            {
                if (x[n] < y[n])
                    return -1;
                else if (x[n] > y[n])
                    return 1;
                else
                    return x.CompareTo(y);
            });

            return stringList.ToArray();
        }

        public static string[] solution8(string[] commands)
        {
            Graph graph = new Graph(4, 4);
            return graph.Command(commands);
        }
        public class Graph
        {
            Cell[,] cells;
            public Graph(int r, int c)
            {
                cells = new Cell[r, c];
                for (int i = 0; i < r; i++)
                    for (int j = 0; j < c; j++)
                        cells[i, j] = new Cell();
            }
            public void Update(int r, int c, string value)
            {
                cells[r, c].Update(value);
            }
            public void Update(string prev, string value)
            {
                foreach(Cell cell in cells)
                {
                    cell.Update(prev, value);
                }
            }
            public void Merge(int r1, int c1, int r2, int c2)
            {
                if (r1 == r2 && c1 == c2)
                    return;
                cells[r1, c1].MergeTo(cells[r2, c2]);
            }
            public void UnMerge(int r, int c)
            {
                cells[r, c].UnMerge();
            }
            public string Print(int r, int c)
            {
                return cells[r, c].ToString();
            }
            public string[] Command(string[] commands)
            {
                List<string> result = new List<string>();
                foreach(string command in commands)
                {
                    string[] coms = command.Split(' ');

                    Console.WriteLine(string.Join(',', coms));
                    switch (coms[0])
                    {
                        case "UPDATE":
                            if (coms.Length > 3)
                            {
                                int r = int.Parse(coms[1]) - 1;
                                int c = int.Parse(coms[2]) - 1;
                                string value = coms[3];
                                Update(r, c, value);
                            }
                            else
                            {
                                string prev = coms[1];
                                string value = coms[2];
                                Update(prev, value);
                            }
                            break;
                        case "MERGE":
                            {
                                int r1 = int.Parse(coms[1]) - 1;
                                int c1 = int.Parse(coms[2]) - 1;
                                int r2 = int.Parse(coms[3]) - 1;
                                int c2 = int.Parse(coms[4]) - 1;
                                Merge(r1, c1, r2, c2);
                            }
                            break;
                        case "UNMERGE":
                            {
                                int r = int.Parse(coms[1]) - 1;
                                int c = int.Parse(coms[2]) - 1;
                                UnMerge(r, c);
                            }
                            break;
                        case "PRINT":
                            {
                                int r = int.Parse(coms[1]) - 1;
                                int c = int.Parse(coms[2]) - 1;
                                result.Add(Print(r, c));
                            }
                            break;
                    }
                    Console.Clear();
                    Console.WriteLine(command);
                    for(int i = 0; i < 4; i++)
                    {
                        for(int j = 0; j < 4; j++)
                        {
                            Console.Write($"{cells[i, j], 10}");
                        }
                        Console.WriteLine();
                    }
                    Console.WriteLine();
                    Console.WriteLine();
                    cells[0, 2].IsMerge();
                    Console.ReadKey();
                }
                return result.ToArray();
            }
        }
        public class Cell
        {
            string value;
            MergeCell mergeCell;
            bool isMerge;
            public Cell()
            {
                Reset();
                isMerge = false;
            }
            public Cell(string value)
            {
                this.value = value;
                isMerge = false;
            }
            public void MergeTo(Cell cell)
            {
                if (isMerge)
                {
                    if (cell.isMerge)
                    {
                        if (mergeCell != cell.mergeCell)
                            cell.mergeCell.MergeFrom(this.mergeCell);
                    }
                    else
                        cell.MergeFrom(this.mergeCell);
                }
                else
                {
                    if (cell.isMerge)
                        MergeFrom(cell.mergeCell);
                    else
                    {
                        MergeFrom(new MergeCell(value));
                        cell.MergeFrom(this.mergeCell);
                    }
                }
            }
            public void MergeFrom(MergeCell mergeCell)
            {
                this.mergeCell = mergeCell;
                this.mergeCell.Add(this);
                isMerge = true;
            }
            public void UnMerge()
            {
                if (isMerge)
                {
                    value = mergeCell.UnMerge();
                    isMerge = false;
                }
            }
            public void Update(string value)
            {
                if (isMerge)
                    mergeCell.Update(value);
                else
                    this.value = value;
            }
            public void Update(string prev, string value)
            {
                if (isMerge)
                    mergeCell.Update(prev, value);
                else
                {
                    if (this.value == prev)
                        this.value = value;
                }
            }
            public void Reset()
            {
                value = "EMPTY";
                mergeCell = null;
                isMerge = false;
            }
            public override string ToString()
            {
                if (isMerge)
                    return mergeCell.ToString();
                else
                    return value;
            }
            public void IsMerge()
            {
                Console.WriteLine($"{value}, {isMerge}, {mergeCell?.ToString()}");
            }
        }
        public class MergeCell
        {
            string value;
            List<Cell> mergeCells;
            public MergeCell(string value)
            {
                this.value = value;
                mergeCells= new List<Cell>();
            }
            public void Add(Cell cell)
            {
                mergeCells.Add(cell);
            }
            public void MergeFrom(MergeCell mergeCell)
            {
                foreach(Cell cell in mergeCells)
                {
                    cell.MergeFrom(mergeCell);
                }
            }
            public string UnMerge()
            {
                foreach(Cell cell in mergeCells)
                {
                    cell.Reset();
                }
                return value;
            }
            public void Update(string value)
            {
                this.value = value;
            }
            public void Update(string prev, string value)
            {
                if (this.value == prev)
                    this.value = value;
            }
            public override string ToString()
            {
                return value;
            }
        }
    }
}
