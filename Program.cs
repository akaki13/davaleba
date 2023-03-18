
using RestSharp;
using testi.Data;
using testi.Models;
using Newtonsoft.Json;

Console.WriteLine(IsPalindrome("asa"));
Console.WriteLine(MinSplit(452));
int[] arrr = {2,4,6,8,10};
Console.WriteLine(NotContains(arrr));
Console.WriteLine(IsProperly("(()())"));
Console.WriteLine(CountVarients(2));
GetAllTeachersByStudent("giorgi");
GenerateCountryDataFiles();


bool IsPalindrome(string text)
{
    string cleanText = new string(text.Where(char.IsLetterOrDigit).Select(char.ToLower).ToArray());
    
    return cleanText == new string(cleanText.Reverse().ToArray());
}

int MinSplit(int amount)
{
    int[] coins = { 50, 20, 10, 5, 1 };
    int numCoins = 0;

    for (int i = 0; i < coins.Length; i++)
    {
        while (amount >= coins[i])
        {
            amount -= coins[i];
            numCoins++;
        }
    }

    return numCoins;
}

int NotContains(int[] arr)
{
    Array.Sort(arr);
    int missingNum = 1;
    for (int i = 0; i < arr.Length; i++)
    {
        if (arr[i] == missingNum)
        {
            missingNum +=1;
        }
        else
        {
            break;
        }
        
    }
    return missingNum;
    
}

bool IsProperly(string sesquence)
{
    Stack<char> stack = new Stack<char>();
    
    foreach (char c in sesquence)
    {
        if (c == '(')
        {
            stack.Push(c);
        }
        else if (c == ')')
        {
            if (stack.Count == 0 || stack.Pop() != '(')
            {
                return false;
            }
        }
    }
    return stack.Count == 0;
}

int CountVarients(int stairCount)
{
    if (stairCount == 1 || stairCount == 0)
    {
        return 1;
    }

    int[] dp = new int[stairCount + 1];
    dp[1] = 1;
    dp[2] = 2;

    for (int i = 3; i <= stairCount; i++)
    {
        dp[i] = dp[i - 1] + dp[i - 2];
    }
    return dp[stairCount];
}

List<Teacher> GetAllTeachersByStudent(string student)
{
    var context = new SchoolContext();
    var teachers = context.Teachers.Where(t => t.TeacherPupils.Any(p => p.Pupil.FirstName == student)).ToList();

    return teachers;
}

void GenerateCountryDataFiles()
{
    RestClient client = new RestClient("https://restcountries.com/v3.1");
    RestRequest request = new RestRequest("all", Method.Get);
    RestResponse response = client.Execute(request);
    dynamic countries = JsonConvert.DeserializeObject<object>(response.Content);
    foreach(var countri in countries)
    {
        string filename = "Country/" + countri.name.common + ".txt";
        if (!Directory.Exists(path: "Country"))
        {
             Directory.CreateDirectory("Country");
        }
        using (StreamWriter writer = new StreamWriter(filename))
        {
            writer.WriteLine($"Country Name: {countri.name.common}");
            writer.WriteLine($"Country Region: {countri.region}");
            writer.WriteLine($"Country SubRegion: {countri.subregion}");
            writer.WriteLine($"Country LatNLng: {countri.latlng[0]}, {countri.latlng[1]}");
            writer.WriteLine($"Country Area: {countri.area}");
            writer.WriteLine($"Country Population: {countri.population}");
        }
    }

}
