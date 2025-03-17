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

// Table Class
public class Table
{
    public List<Card> TableCards { get; private set; } = new List<Card>();
    
    public void SetupTable(Deck deck)
    {
        TableCards.Clear();
        TableCards.Add(deck.DrawCard());
        TableCards.Add(deck.DrawCard());
    }
    
    public bool IsValidMove()
    {
        return TableCards.Count == 2 && (TableCards[0].Value + TableCards[1].Value == 11);
    }
}

// Game Class
public class Game
{
    private Deck Deck { get; set; }
    private Table Table { get; set; }
    
    public Game()
    {
        Deck = new Deck();
        Table = new Table();
    }
    
    public void StartGame()
    {
        Deck.CreateDeck();
        Deck.Shuffle();
        Table.SetupTable(Deck);
        DisplayGameState();
    }
    
    public bool CheckWinCondition()
    {
        return Table.IsValidMove();
    }
    
    public void RestartGame()
    {
        StartGame();
    }
    
    private void DisplayGameState()
    {
        Console.WriteLine("Cards on the table:");
        foreach (var card in Table.TableCards)
        {
            Console.WriteLine($"{card.Rank} of {card.Suit} (Value: {card.Value})");
        }
        
        if (CheckWinCondition())
            Console.WriteLine("Congratulations! You win!");
        else
            Console.WriteLine("Sorry, you lose. Try again!");
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
            game.StartGame();
            
            Console.WriteLine("Do you want to play again? (y/n)");
            string response = Console.ReadLine().ToLower();
            
            if (response != "y")
                playing = false;
        }
    }
}