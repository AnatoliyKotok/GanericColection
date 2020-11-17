using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Home_12
{
    class Task_2
    {
        class Card
        {
            string name;
            public UInt16 Value { get; }
            public const byte QuantityOfCard = 36;
            private Card() { }
            private Card(string name, UInt16 value)
            {
                this.name = name;
                this.Value = value;
            }
            static (string, byte)[] Ranks = new (string, byte)[]
            {
                ( "A", 14 ),
                ("K", 13 ),
                ("Q", 12 ),
                ("J", 11 ),
                ("10", 10 ),
                ("9", 9 ),
                ("8", 8),
                ("7", 7),
                ("6", 6)
            };
            static char[] Suits = { '♥', '♦', '♣', '♠' };
            static public Card[] DeckCard()
            {
                Card[] res = new Card[QuantityOfCard];
                byte counterSuit = 0;
                char suit = Suits[counterSuit];
                for (int i = 0; i < QuantityOfCard; i++)
                {
                    if (i % Ranks.Length == 0 && i != 0)
                    {
                        suit = Suits[++counterSuit];
                    }
                    res[i] = new Card(Ranks[i % Ranks.Length].Item1 + suit, Ranks[i % Ranks.Length].Item2);
                }
                return res;
            }
            public override string ToString()
            {
                return name;
            }
        }
        class Player
        {
            LinkedList<Card> cards = new LinkedList<Card>();
            Point position;
            public Player(Point position)
            {
                this.position = position;
            }
            public int Count
            {
                get => cards.Count;
            }
            public void AddCardRange(Card[] cards)
            {
                foreach (Card card in cards)
                {
                    this.cards.AddLast(card);
                }
            }
            public Card nextCard()
            {
                Card buffer = cards.First.Value;
                cards.RemoveFirst();
                Print(cards,position);
                Console.SetCursorPosition(position.X, position.Y + cards.Count);
                Console.Write("   ");
                return buffer;
            }
            public void Clear()
            {
                cards.Clear();
            }
            public void Print()
            {
                Print(cards, position);
            }
            static public void Print(LinkedList<Card> cards,Point position)
            {
                int counter = 0;
                foreach (var card in cards)
                {
                    Console.SetCursorPosition(position.X, position.Y + counter++);
                    Console.Write($"{card,3}");
                }
            }
            static public void ClearPrint(int length, Point position)
            {
                for (int i = 0; i < length; i++)
                {
                    Console.SetCursorPosition(position.X, position.Y + i);
                    Console.Write($"   ");
                }
            }
        }
        class Game
        {
            readonly Card[] cards = Card.DeckCard();
            Player[] players;
            Random rnd = new Random();
            public Game(UInt16 quantityPlayers)
            {
                ShuffleCards();
                players = new Player[quantityPlayers];
                for (int i = 0; i < quantityPlayers; i++)
                {
                    players[i] = new Player(new Point(i * 5, 10));
                    players[i].AddCardRange(cards.Skip(Card.QuantityOfCard / quantityPlayers * i).Take(Card.QuantityOfCard / quantityPlayers).ToArray());
                }
            }
            public void game()
            {
                LinkedList<Card> bufferCard = new LinkedList<Card>();
                Player playerWinningCard=null;//гравець з виграшною картою
                int maxValueCard = 0;//найбільша карта
                Point befferCardPrint = new Point(0, 0);
                while (players.Count(x => x.Count > 0) > 1)
                {
                    foreach (Player player in players)
                    {
                        if (player.Count != 0)
                        {
                            System.Threading.Thread.Sleep(50 );
                            bufferCard.AddLast(player.nextCard());
                            if (maxValueCard < bufferCard.Last.Value.Value)
                            {
                                playerWinningCard = player;
                                maxValueCard = bufferCard.Last.Value.Value;
                            }
                        }
                    }
                        Player.Print(bufferCard, befferCardPrint);
                    System.Threading.Thread.Sleep(150);
                        Player.ClearPrint(bufferCard.Count, befferCardPrint);
                    maxValueCard = 0;
                    playerWinningCard?.AddCardRange(bufferCard.ToArray());
                    bufferCard.Clear();
                    playerWinningCard?.Print();
                }
            }
            public void ShuffleCards()
            {
                for (int i = cards.Length - 1; i >= 1; i--)//алгоритм фішера(для перемішування )
                {
                    int j = rnd.Next(i + 1);
                    Card temp = cards[j];
                    cards[j] = cards[i];
                    cards[i] = temp;
                }
            }
        }
        static public void main()
        {
            Game a=new Game(4);
            a.game();
        }
    }
}
