using System.IO;
using System.Text;

Console.WriteLine("Введите строку:");
string input = Console.ReadLine();
string[] words = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
string filePath = "../../../TextFile.txt";

int taskNum = 6, num = 1;

while (num <= taskNum)
{
    Console.WriteLine($"\n--- Задание {num} ---\n");

    switch (num)
    {
        case 1:
            executeTask1(words);
            break;
        case 2: 
            executeTask2(words);
            break;
        case 3:
            Console.WriteLine(GetNumReplaceStr(input));
            break;
        case 4:
            Console.WriteLine("Вопросительные предложения:");
            GetSentencesByType(filePath, "?");
            Console.WriteLine("Восклицательные предложения:");
            GetSentencesByType(filePath, "!");
            break; 
        case 5:
            GetSentencesWoSymbol(filePath, ",");
            break;
        case 6:
            ShowWordsWithSameEnds(words);
            break;
    }

    num++;
}

static void executeTask1(string[] arr)
{
    int counter = 0;
    int[] digitCnt = new int[arr.Length];

    for (int i = 0; i < arr.Length; i++)
    {
        foreach (char letter in arr[i])
        {
            if (char.IsDigit(letter))
                counter++;
        }

        digitCnt[i] = counter;

        counter = 0;
    }

    int maxDigitCnt = GetMaxValue(digitCnt);

    for (int i = 0; i < digitCnt.Length; i++)
    {
        if (digitCnt[i] == maxDigitCnt)
        {
            Console.Write(arr[i] + " ");
        }
    }

    Console.WriteLine();
}

static void executeTask2(string[] arr)
{
    int[] wordsLength = new int[arr.Length];
    List<string> sortedArr = arr.ToList();
    sortedArr.Sort();
    
    Dictionary<string, int> maxWordsCnt = new Dictionary<string, int>();

    for (int i = 0; i < sortedArr.Count; i++)
    {
        if (!maxWordsCnt.Keys.Contains(sortedArr[i]))
            maxWordsCnt.Add(sortedArr[i], 1);

        if (i == (sortedArr.Count - 1)) break;
         
        if (sortedArr[i] == sortedArr[i + 1])
           maxWordsCnt[sortedArr[i]]++;
    }

    sortedArr = sortedArr.Distinct().ToList();

    for (int i = 0; i < sortedArr.Count; i++)
    {
        wordsLength[i] = sortedArr[i].Length;
    }

    int maxLength = GetMaxValue(wordsLength);

    for (int i = 0; i < wordsLength.Length; i++)
    {
        if (wordsLength[i] == maxLength)
        {
            Console.WriteLine($"Слово - {sortedArr[i]}, количество вхождений - {maxWordsCnt[sortedArr[i]]}");
        }
    }
}

static int GetMaxValue(int[] arr)
{
    int maxValue = 0;

    foreach (int val in arr)
    {
        if (val > maxValue)
        {
            maxValue = val;
        }
    }

    return maxValue;
}

static string GetNumReplaceStr(string input)
{
    StringBuilder sb = new StringBuilder(input);

    for (int i = 0; i < 10; i++)
    {
        sb.Replace(i.ToString(), GetNumText(i));
    }

    return sb.ToString();
}

static string GetNumText(int num)
{
    return num switch 
    {
        0 => "ноль",
        1 => "один",
        2 => "два",
        3 => "три",
        4 => "четыре",
        5 => "пять",
        6 => "шесть",
        7 => "семь",
        8 => "восемь",
        9 => "девять",
        _ => num.ToString()
    };
}

static void GetSentencesByType(string filePath, string sentenceType)
{
    StreamReader reader = new StreamReader(filePath);

    string? line;

    while ((line = reader.ReadLine()) != null)
    {
        if (line.Trim().EndsWith(sentenceType)) 
            Console.WriteLine(line);
    }

    reader.Close();
}

static void GetSentencesWoSymbol(string filePath, string symbol)
{
    StreamReader reader = new StreamReader(filePath);

    string? line;

    while ((line = reader.ReadLine()) != null)
    {
        if (line.Trim().IndexOf(symbol) == -1)
            Console.WriteLine(line);
    }

    reader.Close();
}

static void ShowWordsWithSameEnds(string[] arr) 
{
    foreach (string word in arr)
    {
        if (word[0] == word[word.Length - 1])
            Console.Write(word + " ");
    }

    Console.WriteLine();
}
