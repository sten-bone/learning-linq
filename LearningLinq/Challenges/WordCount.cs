namespace LearningLinq.Challenges;

// Use a LINQ query to find the count of each word in a paragraph (removing punctuation)
// Words should be considered case-insensitive
public class WordCount
{
    private const string Passage = "It was the best of times, it was the worst of times, it was the age of wisdom, " +
                                   "it was the age of foolishness, it was the epoch of belief, it was the epoch of incredulity, " +
                                   "it was the season of Light, it was the season of Darkness, it was the spring of hope, it was " +
                                   "the winter of despair, we had everything before us, we had nothing before us, we were all going " +
                                   "direct to Heaven, we were all going direct the other way — in short, the period was so far like the " +
                                   "present period, that some of its noisiest authorities insisted on its being received, for good or for evil, " +
                                   "in the superlative degree of comparison only.";

    public static void Run()
    {
        Console.WriteLine("Challenge: Word Counts\n");
        
        var wordCounts = Passage.Split(" ")
            .Select(x => new string(x.Trim().ToLower().Where(c => !char.IsPunctuation(c)).ToArray()))
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .GroupBy(x => x)
            .Select(x => new
            {
                Word = x.Key,
                x.ToList().Count
            })
            .OrderByDescending(x => x.Count);

        foreach (var wordCount in wordCounts)
        {
            Console.WriteLine($"{wordCount.Word}: {wordCount.Count}");
        }
    }
}