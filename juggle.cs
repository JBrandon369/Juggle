using System.IO;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
					
public class Program
{
	public static void Main(string[] args)
	{
		List<Circuit> circuits = new List<Circuit>();
		List<Juggler> jugglers = new List<Juggler>();
		List<Score> scores = new List<Score>();
		
		string[] input = System.IO.File.ReadAllLines(@"input.txt");
		
		Console.WriteLine(input.Length);
		
		foreach(string element in input) {
		    if(element == ""){
		    }
			else if(element[0] == 'C') {
				circuits.Add(new Circuit(element));
			}
			else if(element[0] == 'J') {
				jugglers.Add(new Juggler(element));
			}
		}
        
        foreach(Juggler juggler in jugglers) {
            foreach(Circuit circuit in circuits) {
                scores.Add(new Score(circuit,juggler));
            }
        }
        scores = scores.OrderByDescending(s => s.score).ToList();
        foreach(Score score in scores) {
            Console.WriteLine(score.print());
        }
	}
	
	public static void printData(List<Circuit> circuits, List<Juggler> jugglers) {
	    foreach(Circuit circuit in circuits) {
			Console.WriteLine(circuit.print());
		}
		
		Console.WriteLine();
		
		foreach(Juggler juggler in jugglers) {
			Console.WriteLine(juggler.print());
		}
	}
	
	public class Score {
	    public Circuit Circuit { get; set; }
	    public Juggler Juggler { get; set; }
	    public int score { get; set; }
	    
	    public Score(Circuit circuit, Juggler juggler) {
	        Circuit = circuit;
	        Juggler = juggler;
	        score = (circuit.Hand * juggler.Hand) + (circuit.Endurance * juggler.Endurance) + (circuit.Pizzazz * juggler.Pizzazz);
	    }
	    
	    public string print() {
	        return string.Format("S {0} {1} {2}",Juggler.Name,Circuit.Name,score);
	    }
	}
	
	public class Circuit {
		
		public string Name { get; set; }
		public int Hand { get; set; }
		public int Endurance { get; set; }
		public int Pizzazz { get; set; }
		
		public Circuit(string s) {
			Regex regex = new Regex("C (.*) H:(.*) E:(.*) P:(.*)");
			Match match = regex.Match(s);
			Name = match.Groups[1].Value;
			Hand =  Int32.Parse(match.Groups[2].Value);
			Endurance =  Int32.Parse(match.Groups[3].Value);
			Pizzazz =  Int32.Parse(match.Groups[4].Value);
		}
		
		public string print() {
			return string.Format("J {0} H:{1} E:{2} P:{3}",Name,Hand,Endurance,Pizzazz);
		}
	}
	
	public class Juggler {
		
		public string Name { get; set; }
		public int Hand { get; set; }
		public int Endurance { get; set; }
		public int Pizzazz { get; set; }
		
		public string[] Preference { get; set; }
		
		public Juggler(string s) {
			Regex regex = new Regex("J (.*) H:(.*) E:(.*) P:(.*) (.*)");
			Match match = regex.Match(s);
			Name = match.Groups[1].Value;
			Hand =  Int32.Parse(match.Groups[2].Value);
			Endurance =  Int32.Parse(match.Groups[3].Value);
			Pizzazz =  Int32.Parse(match.Groups[4].Value);
			Preference = match.Groups[5].Value.Split(',');
		}
		
		public string print() {
			return string.Format("J {0} H:{1} E:{2} P:{3} {4}",Name,Hand,Endurance,Pizzazz,String.Join(",",Preference));
		}
	}
}
