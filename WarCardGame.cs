using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace CardGame
{
  enum Suit { Hearts, Diamonds, Clubs, Spades };
  enum FaceValue { Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace };
  class Card
	{
        private Suit mySuit;
        private FaceValue myFaceValue;
 
        //default constructor
        public Card()
        {
            mySuit = Suit.Hearts;
            myFaceValue = FaceValue.Two;
        }
 
        //regular constructor
		public Card(Suit s, FaceValue v)
		{
            mySuit = s;
            myFaceValue = v;
		}
 
        //Prints the card, example: Nine of Clubs
        public override string ToString()
		{
            string result = string.Format("{0} of {1}", this.myFaceValue, this.mySuit);
			return result;
		}
 
        public Suit GetSuit()
        {
            return mySuit;
        }
 
        public FaceValue GetFaceValue()
        {
            return myFaceValue;
        }
 
 
        //compare just on face values
        //compare card one to card 2. Is going to return 0, -1, or 1
        // # implements IComparaible.CompareTo
        public int CompareTo(object obj)
        {
            if (obj == null) return 1;
 
            Card secondCard = obj as Card;
 
            if (secondCard != null)
                return this.myFaceValue.CompareTo(secondCard.myFaceValue);
            else
                throw new ArgumentException("Object is not a Card");
        }
        
        //Equal is similar to CompareTo
        public override bool Equals(object obj)
        {
            bool same;
            int result;
            result = this.CompareTo(obj);
            if (result == 0) same = true;
            else same = false;
            return same;
        }
 
        // GetHashCode is part to the IComparable interface
        // Not used here, but provided to get a clean built.
        public override int GetHashCode()
        {
            return ((int)this.myFaceValue);
        }
	}/*class Card*/
  
  class Deck
	{
        private const int CARDSINDECK = 52;
        private List<Card> theDeck = new List<Card>();
        private Random randomCardSelector = new Random();
        private int cardIndex;
 
		public Deck()
		{
            cardIndex = 0;
 
            //for (Suit suit = Suit.Hearts; suit <= Suit.Spades; suit++)
            //{
            //    for (FaceValue faceValue = FaceValue.Two; faceValue <= FaceValue.Ace; faceValue++)
            //    {
            //        this.theDeck.Add(new Card(suit, faceValue));
            //    }
            //}
            
            foreach (Suit s in Enum.GetValues(typeof(Suit)))
            {
                foreach (FaceValue f in Enum.GetValues(typeof(FaceValue)))
                {
                    Card c = new Card(s,f);
                    theDeck.Add(c);
                }
            }
		}
 
        //Function returns the concatenation of all the cards in the deck
        //usefull for debugging: to see if it was properly shuffled
        public String Print()
        {
            String s = "";
            for (int i = 0; i < CARDSINDECK; i++)
            {
                s+= theDeck[i].ToString();
            }
            return s;
        }
 
        //Is the deck empty?
        public bool EmptyDeck()
        {
            if (cardIndex < CARDSINDECK)
            {
                return false;
            }
            else return true;
        }
 
        // make sure you don't take the same card
        // Pre-condition: The deck is not empty.
        // Check if the deck is not empty before calling this function
        public Card DealACard()
        {
            cardIndex++;
            return theDeck[cardIndex - 1];
        }
 
 
        // shuffle the Deck
        public void Shuffle()
        {
            Card temp;           
            int mynum;
            for (int i = 0; i < CARDSINDECK; i++)
            {
                mynum = randomCardSelector.Next(CARDSINDECK);
                temp = theDeck[i];
                theDeck[i] = theDeck[mynum];
                theDeck[mynum] = temp;
                
            }
        }
 
	}/*Class Deck*/
  
    class Program
    {
        static void Main(string[] args)
        {
            //Card card1;
            Deck aDeck = new Deck();
            aDeck.Shuffle();
           
 
            //card1 = aDeck.DealACard();
 
            //Console.WriteLine(card1.GetFaceValue() + " of " + card1.GetSuit());
            //Console.WriteLine("Shuffled");
            //Console.WriteLine(aDeck.Print());
            //Console.ReadKey();
 
            
            int player1 = 0;
            int player2 = 0;
            int pot = 2;
            int round = 0;
            while (!aDeck.EmptyDeck())
            {
                round += 1;
                Console.WriteLine("Round{0}", round);
 
                Card card1 = aDeck.DealACard();
                Card card2 = aDeck.DealACard();
 
                Console.WriteLine("Player One card = {0}", card1.ToString());
                Console.WriteLine("Player Two card = {0}", card2.ToString());
                if (card1.Equals(card2))
                {
                    /*war*/
                    Console.WriteLine("It is a draw");
                    if (aDeck.EmptyDeck())
                    {
                        /*this is last card so war can not continue*/
                        player1 += pot / 2;
                        player2 += pot / 2;
                    }
                    else
                    {
                        pot += 2;
                    }
                }
                else if (card1.CompareTo(card2) == 1){
                    Console.WriteLine("Player One wins this round. They get {0} points!",pot);
                    player1 +=pot;
                    pot = 2;
                }
                else{
                    Console.WriteLine("Player Two wins this round. They get {0} points!",pot);
                    player2 +=pot;
                    pot = 2;
                }
                Console.WriteLine("Current Score");
                Console.WriteLine("Player1 = {0}", player1);
                Console.WriteLine("Player2 = {0}", player2);
                Console.Write("Press return to continue. . .");
                Console.ReadLine(); 
            }
            Console.WriteLine("Thank you for playing!");
            Console.ReadKey();
        }
    }
}
