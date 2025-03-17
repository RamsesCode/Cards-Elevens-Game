using System;
using System.Collections.Generic;

// Enum for Suit and Rank
public enum Suit { Hearts, Diamonds, Clubs, Spades }
public enum Rank { Ace = 1, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King }





// Card Class
public class Card
{
    public Suit Suit { get; set; }
    public Rank Rank { get; set; }
    public int Value => (int)Rank;
    public bool IsFaceUp { get; set; } = true;
    
    public Card(Suit suit, Rank rank)
    {
        Suit = suit;
        Rank = rank;
    }
}




// Deck Class
public class Deck
{
    public List<Card> Cards { get; private set; } = new List<Card>();
    
    public void CreateDeck()
    {
        foreach (Suit suit in Enum.GetValues(typeof(Suit)))
        {
            foreach (Rank rank in Enum.GetValues(typeof(Rank)))
            {
                Cards.Add(new Card(suit, rank));
            }
        }
    }
    





    public void Shuffle()
    {
        Random rng = new Random();
        for (int i = Cards.Count - 1; i > 0; i--)
        {
            int j = rng.Next(i + 1);
            (Cards[i], Cards[j]) = (Cards[j], Cards[i]);
        }
    }
    
    public Card DrawCard()
    {
        if (Cards.Count == 0) return null;
        Card drawnCard = Cards[0];
        Cards.RemoveAt(0);
        return drawnCard;
    }
}




// Game Class
public class Game
{
    private Deck Deck { get; set; }
    
    public Game()
    {
        Deck = new Deck();
    }
    
    public void StartGame()
    {
        Deck.CreateDeck();
        Deck.Shuffle();
        PlayRound();
    }
    


    private void PlayRound()
    {
        Console.WriteLine("\nPress any key to draw your first card...");
        Console.ReadKey();
        Card firstCard = Deck.DrawCard();
        Console.WriteLine($"\nFirst card: {firstCard.Rank} of {firstCard.Suit} (Value: {firstCard.Value})\n");
        
        Console.WriteLine("Press any key to draw your second card...");
        Console.ReadKey();
        Card secondCard = Deck.DrawCard();
        Console.WriteLine($"\nSecond card: {secondCard.Rank} of {secondCard.Suit} (Value: {secondCard.Value})\n");
        
        int totalValue = firstCard.Value + secondCard.Value;
        Console.WriteLine($"Total Value: {totalValue}\n");
        
        if (totalValue >= 11)
            Console.WriteLine("Congratulations! You win!\n");
        else
            Console.WriteLine("Sorry, you lose. Try again!\n");
    }
}




// Main Program
class Program
{
    static void Main()
    {
        Game game = new Game();
        bool playing = true;
        
        while (playing)
        {
            Console.WriteLine("\n=== Elevens Game ===");
            Console.WriteLine("1. Start Game");
            Console.WriteLine("2. Exit");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();
            
            if (choice == "1")
            {
                game.StartGame();
                
                Console.WriteLine("Do you want to play again? (y/n)");
                string response = Console.ReadLine().ToLower();
                
                if (response != "y")
                    playing = false;
            }
            else if (choice == "2")
            {
                playing = false;
            }
            else
            {
                Console.WriteLine("Invalid choice, please try again.");
            }
        }
    }
}
